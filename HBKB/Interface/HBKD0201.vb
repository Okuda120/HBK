Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread
Imports System.Windows.Forms

Public Class HBKD0201

    'インスタンス作成
    Public dataHBKD0201 As New DataHBKD0201
    Private logicHBKD0201 As New LogicHBKD0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    Private intSelectedTabIdx As Integer = logicHBKD0201.TAB_KHN                    '前回選択タブ（初期値：基本情報タブ）

    Dim WithEvents datamodel As FarPoint.Win.Spread.Model.DefaultSheetDataModel     '作業履歴データモデル（更新判定用）
    Dim bln_chg_flg As Boolean                                                      '内部更新フラグ
    Dim add_row_cnt As Integer                                                      '新規追加をカウント、行削除はマイナスする
    Dim bln_update_flg As Boolean                                                   '最終的な更新フラグ
    Dim init_row_cnt As Integer                                                     '初期表示カウント
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
    Private Sub HBKD0201_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKD0201_Height = Me.Size.Height
                .propHBKD0201_Width = Me.Size.Width
                .propHBKD0201_Y = Me.Location.Y
                .propHBKD0201_X = Me.Location.X
                .propHBKD0201_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKD0201_WindowState = Me.WindowState
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
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : 2017/08/17 e.okuda</p>
    ''' </para></remarks>
    Private Sub HBKD0201_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'ボタンコントロール非活性対象リスト作成
        AryNotfrmCtlList.Clear()
        AryNotfrmCtlList.Add(btnOpenFile.Name)
        AryNotfrmCtlList.Add(btnSaveFile.Name)


        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKD0201_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKD0201_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKD0201_Width, Settings.Instance.propHBKD0201_Height)
            Me.Location = New Point(Settings.Instance.propHBKD0201_X, Settings.Instance.propHBKD0201_Y)
        End If
        '拡大処理
        kakudai(Settings.Instance.propHBKD0201_Expantion_wkRireki)

        'データクラスへプロパティをセット
        With dataHBKD0201
            'フォームオブジェクト
            .PropLblKanryoMsg = Me.lblKanryoMsg                         'ヘッダ：完了メッセージ
            .PropTxtPrbNmb = Me.txtPrbNmb                               'ヘッダ：番号
            .PropLblRegInfo_out = Me.lblRegInfo_out                     'ヘッダ：登録情報_出力
            .PropLblUpdateInfo_out = Me.lblUpdateInfo_out               'ヘッダ：最終更新情報_出力
            .PropGrpLoginUser = Me.grpLoginUser                         'ヘッダ：ログインユーザ情報
            .PropTbInput = Me.tbInput                                   'タブ
            .PropCmbStatus = Me.cmbStatus                               '基本情報タブ：ステータス
            .PropDtpStartDT = Me.dtpStartDT                             '基本情報タブ：開始日時（日付）
            .PropTxtStartDT_HM = Me.txtStartDT_HM                       '基本情報タブ：開始日時（時刻）
            .PropBtnStartDT_HM = Me.btnStartDT_HM                       '基本情報タブ：時（開始日時）
            .PropDtpKanryoDT = Me.dtpKanryoDT                           '基本情報タブ：完了日時（日付）
            .PropTxtKanryoDT_HM = Me.txtKanryoDT_HM                     '基本情報タブ：完了日時（時刻）
            .PropBtnKanryoDT_HM = Me.btnKanryoDT_HM                     '基本情報タブ：時（完了日時）
            .PropCmbTargetSystem = Me.cmbTargetSystem                   '基本情報タブ：対象システム
            .PropCmbPrbCase = Me.cmbPrbCase                             '基本情報タブ：発生原因
            .PropTxtTitle = Me.txtTitle                                 '基本情報タブ：タイトル
            .PropTxtNaiyo = Me.txtNaiyo                                 '基本情報タブ：内容
            .PropTxtTaisyo = Me.txtTaisyo                               '基本情報タブ：対処
            .PropCmbTantoGrp = Me.cmbTantoGrp                           '基本情報タブ：担当グループ
            .PropTxtPrbTantoID = Me.txtPrbTantoID                       '基本情報タブ：担当ID
            .PropTxtPrbTantoNM = Me.txtPrbTantoNM                       '基本情報タブ：担当氏名
            .PropBtnTantoSearch = Me.btnTantoSearch                     '基本情報タブ：検索（担当者）
            .PropBtnTantoMe = Me.btnTantoMe                             '基本情報タブ：私（担当者）
            .PropTxtApproverID = Me.txtApproverID                       '基本情報タブ：対処承認者ID
            .PropTxtApproverNM = Me.txtApproverNM                       '基本情報タブ：対処承認者氏名
            .PropBtnApproverSearch = Me.btnApproverSearch               '基本情報タブ：検索（対処承認者）
            .PropBtnApproverMe = Me.btnApproverMe                       '基本情報タブ：私（対処承認者）
            .PropTxtRecorderID = Me.txtRecorderID                       '基本情報タブ：承認記録者ID
            .PropTxtRecorderNM = Me.txtRecorderNM                       '基本情報タブ：承認記録者氏名
            .PropBtnRecorder = Me.btnRecorder                           '基本情報タブ：検索（承認記録者）
            .PropBtnRecorderMe = Me.btnRecorderMe                       '基本情報タブ：私（承認記録者）
            .PropBtnKakudai = Me.btnKakudai                             '基本情報タブ：拡大
            .PropBtnRefresh = Me.btnRefresh                             '基本情報タブ：リフレッシュ
            .PropVwPrbYojitsu = Me.vwPrbYojitsu                         '基本情報タブ：作業予実スプレッド
            .PropBtnAddRow_Yojitsu = Me.btnAddRow_Yojitsu               '基本情報タブ：（作業予実）「+」
            .PropBtnRemoveRow_Yojitsu = Me.btnRemoveRow_Yojitsu         '基本情報タブ：（作業予実）「-」
            .PropVwMeeting = Me.vwMeeting                               '会議情報タブ：会議情報スプレッド
            .PropBtnAddRow_Meeting = Me.btnAddRow_Meeting               '会議情報タブ：（会議情報）「+」
            .PropBtnRemoveRow_Meeting = Me.btnRemoveRow_Meeting         '会議情報タブ：（会議情報）「-」
            .PropTxtFreeText1 = Me.txtFreeText1                         'フリー入力情報タブ：フリーテキスト1
            .PropTxtFreeText2 = Me.txtFreeText2                         'フリー入力情報タブ：フリーテキスト2
            .PropTxtFreeText3 = Me.txtFreeText3                         'フリー入力情報タブ：フリーテキスト3
            .PropTxtFreeText4 = Me.txtFreeText4                         'フリー入力情報タブ：フリーテキスト4
            .PropTxtFreeText5 = Me.txtFreeText5                         'フリー入力情報タブ：フリーテキスト5
            .PropChkFreeFlg1 = Me.chkFreeFlg1                           'フリー入力情報タブ：フリーフラグ1
            .PropChkFreeFlg2 = Me.chkFreeFlg2                           'フリー入力情報タブ：フリーフラグ2
            .PropChkFreeFlg3 = Me.chkFreeFlg3                           'フリー入力情報タブ：フリーフラグ3
            .PropChkFreeFlg4 = Me.chkFreeFlg4                           'フリー入力情報タブ：フリーフラグ4
            .PropChkFreeFlg5 = Me.chkFreeFlg5                           'フリー入力情報タブ：フリーフラグ5
            .PropVwRelationInfo = Me.vwRelationInfo                     '対応関係者情報：対応関係者情報スプレッド
            .PropBtnAddRow_RelaG = Me.btnAddRow_RelaG                   '対応関係者情報：「+G」
            .PropBtnAddRow_RelaU = Me.btnAddRow_RelaU                   '対応関係者情報：「+U」
            .PropBtnRemoveRow_Rela = Me.btnRemoveRow_Rela               '対応関係者情報：「-」
            .PropVwProcessLinkInfo = Me.vwProcessLinkInfo               'プロセスリンク情報：プロセスリンク情報スプレッド
            .PropBtnAddRow_Plink = Me.btnAddRow_Plink                   'プロセスリンク情報：「+」
            .PropBtnRemoveRow_Plink = Me.btnRemoveRow_Plink             'プロセスリンク情報：「-」
            .PropTxtGrpRireki = Me.txtGrpRireki                         '対応履歴情報：グループ履歴
            .PropTxtTantoRireki = Me.txtTantoRireki                     '対応履歴情報：担当者履歴
            .PropVwCysprInfo = Me.vwCysprInfo                           'CYSPR情報：CYSPR情報スプレッド
            .PropBtnAddRow_Cyspr = Me.btnAddRow_Cyspr                   'CYSPR情報：「+」
            .PropBtnRemoveRow_Cyspr = Me.btnRemoveRow_Cyspr             'CYSPR情報：「-」
            .PropVwPrbFileInfo = Me.vwPrbFileInfo                       '関連ファイル情報：関連ファイル情報スプレッド
            .PropBtnAddRow_File = Me.btnAddRow_File                     '関連ファイル情報：「+」
            .PropBtnRemoveRow_File = Me.btnRemoveRow_File               '関連ファイル情報：「-」
            .PropBtnOpenFile = Me.btnOpenFile                           '関連ファイル情報：「開」
            .PropBtnSaveFile = Me.btnSaveFile                           '関連ファイル情報：「ダ」
            .PropBtnReg = Me.btnReg                                     'フッター：登録／作業予実登録
            .PropBtnMail = Me.btnMail                                   'フッター：メール作成
            .PropBtnHenkou = Me.btnHenkou                               'フッター：変更登録
            .PropBtnPrint = Me.btnPrint                                 'フッター：単票出力
            .PropBtnReturn = Me.btnReturn                               'フッター：戻る／閉じる

            'システムエラー事前対応処理(非活性対象のコントロールリストを作成)
            If logicHBKD0201.DoProcForErrorMain(dataHBKD0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '★初期化設定

            'スプレッド行のクリアを行う
            ' 2017/08/17 e.okuda 削除対象行数が0でない条件付加
            With .PropVwPrbYojitsu.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwRelationInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwProcessLinkInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwCysprInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwPrbFileInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwMeeting.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With

            '表示初期化（担当ID1の以降）
            For j As Integer = logicHBKD0201.COL_YOJITSU_TANTOGRPCD1 To logicHBKD0201.COL_YOJITSU_PRBTANTO_BTN - 1
                .PropVwPrbYojitsu.Sheets(0).Columns(j).Visible = False
            Next

            'データテーブル
            .PropDtTantoRireki = Nothing
            .PropDtwkRireki = Nothing                               '作業履歴情報


            ''拡大判定初期化
            '.PropBlnKakudaiFlg = True
            'btnKakudai_Click(Me, New EventArgs)


            '★プロパティ設定

            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblKanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)

            '作業予実スプレッドの行高設定
            .PropIntVwYojitsuRowHeight = 40

            'コンボボックスMaxDrop取得設定
            Dim intMaxdrop As Integer
            If commonLogicHBK.ChangeListSize(.PropCmbPrbCase.Font.Height, Screen.GetWorkingArea(Me).Height, intMaxdrop) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
            .PropCmbPrbCase.MaxDropDownItems = intMaxdrop           '発生原因
            .PropCmbTargetSystem.PropMaxDrop = intMaxdrop - 10      '対象システム

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        '画面初期表示処理（処理モードによって分岐する）
        If dataHBKD0201.PropStrProcMode = PROCMODE_NEW Then
            'インシデント画面からの呼出かどうか判定する
            If dataHBKD0201.PropBlnFromCheckFlg = True Then
                'インシデント登録画面からの呼出
                '新規モード画面初期表示メイン処理
                If logicHBKD0201.InitFormNewModeFromIncMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
                'インシデント登録画面以外からの呼出
                '新規モード画面初期表示メイン処理
                If logicHBKD0201.InitFormNewModeMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
        Else

            '対応関係のチェック処理
            If logicHBKD0201.KankeiCheckMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '呼び出し元が問題検索一覧の場合
            If dataHBKD0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN Or
                dataHBKD0201.PropIntOwner = SCR_CALLMOTO_MENU Then
                ' 2017/08/30 e.okuda メニュー画面からの遷移を条件追加（クイックアクセス対応）

                '(ア)	編集モード
                If dataHBKD0201.PropIntChkKankei = KANKEI_CHECK_EDIT Then

                    'ロック設定メイン処理(dataHBKD0201.PropBlnBeLockedFlg )
                    If logicHBKD0201.LockMain(dataHBKD0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                    'ロック状況によって処理分岐
                    If dataHBKD0201.PropBlnBeLockedFlg = False Then
                        '編集モード（ロックされていない）
                        '作業予実編集モード変更
                        dataHBKD0201.PropStrProcMode = PROCMODE_EDIT
                        '編集モード画面初期表示メイン処理
                        If logicHBKD0201.InitFormEditModeMain(dataHBKD0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
                        '作業履歴モード（ロックされている）
                        '作業予実編集モード変更
                        dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI

                        '参照モード画面初期表示メイン処理
                        If logicHBKD0201.InitFormRirekiModeMain(dataHBKD0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
                        MsgBox(dataHBKD0201.PropStrBeLockedMsg.Replace("参照画面", "作業履歴編集画面"), MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)
                    End If

                Else
                    '参照モードに変更
                    dataHBKD0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKD0201.InitFormRefModeMain(dataHBKD0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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


            Else    '2.呼出し元画面が「検索一覧」画面以外の場合

                '(ア)	参照モード 
                If dataHBKD0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then
                    '参照モードに変更
                    dataHBKD0201.PropStrProcMode = PROCMODE_REF
                    '参照モード画面初期表示メイン処理
                    If logicHBKD0201.InitFormRefModeMain(dataHBKD0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
                    dataHBKD0201.PropStrProcMode = PROCMODE_REF
                    'エラーメッセージ設定
                    puErrMsg = D0201_E017
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '画面閉じる
                    Me.Close()
                    '処理終了
                    Exit Sub
                End If
            End If

        End If

        ''初期カーソル位置
        'dataHBKD0201.PropCmbStatus.Select()

        '変更判定用のデータを設定（作業履歴編集モード用）
        datamodel = vwPrbYojitsu.ActiveSheet.Models.Data         '作業履歴スプレッドモデルデータ
        init_row_cnt = datamodel.RowCount                       '初期表示時のスプレッド表示数


    End Sub

    ''' <summary>
    ''' フォーム初期表示時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>カーソル位置の設定を行う
    ''' <para>作成情報：2012/09/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0201_Shown(sender As Object, e As System.EventArgs) Handles Me.Shown
        '初期カーソル
        Me.dtpStartDT.txtDate.Focus()
        Me.dtpStartDT.txtDate.SelectAll()

    End Sub


    ''' <summary>
    ''' 基本情報：ComboBoxデータソース変更時の処理
    ''' </summary>
    ''' <param name="sender">引数sender</param>
    ''' <param name="e">引数e</param>
    ''' <remarks>コンボボックスのサイズを計算し、設定する
    ''' <para>作成情報：
    ''' <p>改定情報：2010/09/07 r.hoshino</p>
    ''' </para></remarks>
    Private Sub cmbbox_DataSourceChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles _
        cmbStatus.DataSourceChanged, _
        cmbPrbCase.DataSourceChanged, _
        cmbTantoGrp.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKD0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' 開始日時：[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>日時設定画面を呼出入力された日時をセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnStartDT_HM_Click(sender As System.Object, e As System.EventArgs) Handles btnStartDT_HM.Click

        '日時設定画面の生成
        Dim frmHBKZ0801 As New HBKZ0801

        'パラメータセット
        With frmHBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKD0201.PropDtpStartDT.txtDate.Text
            .PropStrTime = dataHBKD0201.PropTxtStartDT_HM.PropTxtTime.Text
        End With

        '画面を開く
        If frmHBKZ0801.ShowDialog Then
            dataHBKD0201.PropDtpStartDT.txtDate.Text = frmHBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKD0201.PropTxtStartDT_HM.PropTxtTime.Text = frmHBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' 完了日時：[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>日時設定画面を呼出入力された日時をセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKanryoDT_HM_Click(sender As System.Object, e As System.EventArgs) Handles btnKanryoDT_HM.Click

        '日時設定画面の生成
        Dim frmHBKZ0801 As New HBKZ0801

        'パラメータセット
        With frmHBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKD0201.PropDtpKanryoDT.txtDate.Text
            .PropStrTime = dataHBKD0201.PropTxtKanryoDT_HM.PropTxtTime.Text
        End With

        '画面を開く
        If frmHBKZ0801.ShowDialog Then
            dataHBKD0201.PropDtpKanryoDT.txtDate.Text = frmHBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKD0201.PropTxtKanryoDT_HM.PropTxtTime.Text = frmHBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：担当IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当IDをキーに担当名を取得し設定する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtPrbTantoID_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPrbTantoID.PreviewKeyDown

        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKD0201.PropStrTantoIdForSearch = dataHBKD0201.PropTxtPrbTantoID.Text         '担当ID

            If logicHBKD0201.GetPrbTantoDataMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
            dataHBKD0201.PropTxtPrbTantoNM.Text = ""
            dataHBKD0201.PropCmbTantoGrp.SelectedValue = ""
            If dataHBKD0201.PropDtResultTanto IsNot Nothing Then
                If dataHBKD0201.PropDtResultTanto.Rows.Count > 0 Then
                    dataHBKD0201.PropTxtPrbTantoNM.Text = dataHBKD0201.PropDtResultTanto.Rows(0).Item("hbkusrnm")
                End If
                If dataHBKD0201.PropDtResultTanto.Rows.Count = 1 Then
                    dataHBKD0201.PropCmbTantoGrp.SelectedValue = dataHBKD0201.PropDtResultTanto.Rows(0).Item("groupcd")
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 担当者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTantoSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnTantoSearch.Click

        'ひびきユーザー検索一覧画面の生成
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                              'モード：単一選択
            .PropArgs = dataHBKD0201.PropTxtPrbTantoNM.Text             '検索条件：担当氏名
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultTanto = frmHBKZ0101.ShowDialog()

        '担当グループ名,担当ID,担当氏名セット
        If dataHBKD0201.PropDtResultTanto IsNot Nothing Then
            dataHBKD0201.PropCmbTantoGrp.SelectedValue = dataHBKD0201.PropDtResultTanto.Rows(0).Item(3)
            dataHBKD0201.PropTxtPrbTantoID.Text = dataHBKD0201.PropDtResultTanto.Rows(0).Item(0)
            dataHBKD0201.PropTxtPrbTantoNM.Text = dataHBKD0201.PropDtResultTanto.Rows(0).Item(2)
        End If

    End Sub

    ''' <summary>
    ''' 担当者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログイン者のユーザ情報を担当者情報にセットする
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTantoMe_Click(sender As System.Object, e As System.EventArgs) Handles btnTantoMe.Click

        'パラメータセット
        With dataHBKD0201
            .PropCmbTantoGrp.SelectedValue = PropWorkGroupCD        '担当者グループ
            .PropTxtPrbTantoID.Text = PropUserId                    '担当者ID
            .PropTxtPrbTantoNM.Text = PropUserName                  '担当者氏名
        End With

    End Sub

    ''' <summary>
    ''' 基本情報：対処承認者IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>対処承認者IDをキーに対処承認者氏名を取得し設定する
    ''' <para>作成情報：2012/08/15 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtApproverID_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtApproverID.PreviewKeyDown

        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKD0201.PropStrTSyouninSyaIdForSearch = dataHBKD0201.PropTxtApproverID.Text         '対処承認者ID

            If logicHBKD0201.GetPrbApproverDataMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
            dataHBKD0201.PropTxtApproverNM.Text = ""
            If dataHBKD0201.PropDtResultApprover IsNot Nothing Then
                If dataHBKD0201.PropDtResultApprover.Rows.Count > 0 Then
                    dataHBKD0201.PropTxtApproverNM.Text = dataHBKD0201.PropDtResultApprover.Rows(0).Item(0)
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 対処承認者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「エンドユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnApproverSearch_Click(sender As System.Object, e As System.EventArgs) Handles btnApproverSearch.Click

        'エンドユーザー検索一覧画面の生成
        Dim frmHBKZ0201 As New HBKZ0201

        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE                              'モード：単一選択
            .PropArgs = dataHBKD0201.PropTxtApproverNM.Text             '検索条件：対処承認者氏名
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultApprover = frmHBKZ0201.ShowDialog()

        '担当グループ名,担当ID,担当氏名セット
        If dataHBKD0201.PropDtResultApprover IsNot Nothing Then
            dataHBKD0201.PropTxtApproverID.Text = dataHBKD0201.PropDtResultApprover.Rows(0).Item("EndUsrID")
            dataHBKD0201.PropTxtApproverNM.Text = dataHBKD0201.PropDtResultApprover.Rows(0).Item("EndUsrNM")
        End If

    End Sub

    ''' <summary>
    ''' 対処承認者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログイン者のユーザ情報を対処承認者情報にセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnApproverMe_Click(sender As System.Object, e As System.EventArgs) Handles btnApproverMe.Click

        'パラメータセット
        With dataHBKD0201
            .PropTxtApproverID.Text = PropUserId                    '対処承認者ID
            .PropTxtApproverNM.Text = PropUserName                  '対処承認者氏名
        End With

    End Sub

    ''' <summary>
    ''' 基本情報：承認記録者IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>承認記録者IDをキーに対処承認者氏名を取得し設定する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtRecorderID_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtRecorderID.PreviewKeyDown

        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            dataHBKD0201.PropStrRecorderIdForSearch = dataHBKD0201.PropTxtRecorderID.Text         '承認記録者ID

            If logicHBKD0201.GetPrbRecorderDataMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
            dataHBKD0201.PropTxtRecorderNM.Text = ""
            If dataHBKD0201.PropDtResultRecorder IsNot Nothing Then
                If dataHBKD0201.PropDtResultRecorder.Rows.Count > 0 Then
                    dataHBKD0201.PropTxtRecorderNM.Text = dataHBKD0201.PropDtResultRecorder.Rows(0).Item(0)
                End If
            End If

        End If

    End Sub

    ''' <summary>
    ''' 承認記録者：[検索]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>「ひびきユーザー検索一覧」画面を表示する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRecorder_Click(sender As System.Object, e As System.EventArgs) Handles btnRecorder.Click

        'ひびきユーザー検索一覧画面の生成
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                              'モード：単一選択
            .PropArgs = dataHBKD0201.PropTxtRecorderNM.Text             '検索条件：承認記録者氏名
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultRecorder = frmHBKZ0101.ShowDialog()

        '担当グループ名,担当ID,担当氏名セット
        If dataHBKD0201.PropDtResultRecorder IsNot Nothing Then
            dataHBKD0201.PropTxtRecorderID.Text = dataHBKD0201.PropDtResultRecorder.Rows(0).Item(0)
            dataHBKD0201.PropTxtRecorderNM.Text = dataHBKD0201.PropDtResultRecorder.Rows(0).Item(2)
        End If

    End Sub

    ''' <summary>
    ''' 承認記録者：[私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ログイン者のユーザ情報を承認記録者情報にセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRecorderMe_Click(sender As System.Object, e As System.EventArgs) Handles btnRecorderMe.Click

        'パラメータセット
        With dataHBKD0201
            .PropTxtRecorderID.Text = PropUserId                    '承認記録者ID
            .PropTxtRecorderNM.Text = PropUserName                  '承認記録者氏名
        End With

    End Sub

    ''' <summary>
    ''' 作業予実：[拡大]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業予実の入力枠を拡大する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnKakudai_Click(sender As System.Object, e As System.EventArgs) Handles btnKakudai.Click

        If Settings.Instance.propHBKD0201_Expantion_wkRireki = False Then
            '設定を変更する
            With Settings.Instance
                .propHBKD0201_Expantion_wkRireki = True
            End With
            '拡大
            kakudai(True)
        Else
            '設定を変更する
            With Settings.Instance
                .propHBKD0201_Expantion_wkRireki = False
            End With
            '戻す
            kakudai(False)
        End If

    End Sub

    ''' <summary>
    ''' 拡大処理
    ''' </summary>
    ''' <param name="setFlg">[IN]</param>
    ''' <remarks>作業予実の入力枠を拡大する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub kakudai(ByVal setFlg As Boolean)

        If setFlg = True Then
            '増加サイズ（登録ボタンから上15px分まで)
            tbInput.Height = Me.btnReg.Location.Y - 15 - Me.tbInput.Location.Y - Me.vwPrbYojitsu.Location.X
            tbInput.Anchor = AnchorStyles.Bottom + AnchorStyles.Left + AnchorStyles.Top

        Else
            'デフォルトに戻す（grpRelationから上2px分まで)
            tbInput.Anchor = AnchorStyles.Left + AnchorStyles.Top
            tbInput.Height = Me.grpRelation.Location.Y - 2 - Me.tbInput.Location.Y - Me.vwPrbYojitsu.Location.X

        End If

    End Sub

    ''' <summary>
    ''' 作業予実：[リフレッシュ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業予実の表示を最新の状態に更新する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : 2017/08/17 e.okuda</p>
    ''' </para></remarks>
    Private Sub btnRefresh_Click(sender As System.Object, e As System.EventArgs) Handles btnRefresh.Click

        '新規登録モードの場合は処理しない
        If dataHBKD0201.PropStrProcMode = PROCMODE_NEW Then
            Exit Sub
        End If

        'データテーブルの内容をチェックする
        Dim bln_henkou_flg As Boolean = False
        For i As Integer = 0 To dataHBKD0201.PropDtwkRireki.Rows.Count - 1
            If dataHBKD0201.PropDtwkRireki.Rows(i).RowState = DataRowState.Modified Then
                bln_henkou_flg = True
                Exit For
            End If
            If dataHBKD0201.PropDtwkRireki.Rows(i).RowState = DataRowState.Added Then
                bln_henkou_flg = True
                Exit For
            End If
        Next

        '変更なしの場合
        If bln_henkou_flg = False Then
            Exit Sub
        End If

        'リフレッシュ実行の確認メッセージを表示
        If MsgBox(D0201_W002, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            '処理終了
            Exit Sub
        End If

        'スプレッドのクリア
        With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)
            ' 2017/08/17 e.okuda 削除対象行が0でない条件付加
            If .Rows.Count > 0 Then
                .RemoveRows(0, .Rows.Count)
            End If
        End With

        If logicHBKD0201.RefrashPrbWkYojitsuMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 作業予実：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業予実一覧の一番上に一行追加する。
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Yojitsu_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_Yojitsu.Click

        '作業履歴一覧空行追加処理
        If logicHBKD0201.AddRowPrbWkYojitsuMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'データRowAdd時はハンドラ解除
        RemoveHandler datamodel.Changed, AddressOf datamodel_Changed

        '追加した行に値をセットする。
        With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)
            .SetValue(0, logicHBKD0201.COL_YOJITSU_SYSTEM, dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue)   '対象システム
            .SetValue(0, logicHBKD0201.COL_YOJITSU_TANTOGRP1, PropWorkGroupName)            'グループ名
            .SetValue(0, logicHBKD0201.COL_YOJITSU_PRBTANTONM1, PropUserName)               'ユーザ名
            .SetValue(0, logicHBKD0201.COL_YOJITSU_TANTOGRPCD1, PropWorkGroupCD)            'グループCD
            .SetValue(0, logicHBKD0201.COL_YOJITSU_PRBTANTOID1, PropUserId)                 'ユーザID
            .RowHeader.Cells(0, 0).Text = " "
        End With

        'ハンドラ元に戻す
        AddHandler datamodel.Changed, AddressOf datamodel_Changed

    End Sub

    ''' <summary>
    ''' 作業予実一覧：データモデル変更時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>FarPoint.Win.Spread.Model
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub datamodel_Changed(ByVal sender As Object, ByVal e As FarPoint.Win.Spread.Model.SheetDataModelEventArgs) Handles datamodel.Changed

        'セルの値が変更されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.CellsUpdated Then
            bln_chg_flg = True
        End If
        '行追加されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.RowsAdded Then
            add_row_cnt += 1
        End If
        '行削除されたとき
        If e.Type = FarPoint.Win.Spread.Model.SheetDataModelEventType.RowsRemoved Then
            add_row_cnt -= 1
        End If

    End Sub

    ''' <summary>
    ''' 作業予実：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>作業予実一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Yojitsu_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_Yojitsu.Click

        '作業履歴一覧選択行削除処理
        If logicHBKD0201.RemoveRowPrbWkYojitsuMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 作業予実：スプレッド内ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した行毎のボタンイベントへ遷移する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwPrbYojitsu_ButtonClicked(sender As System.Object, e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwPrbYojitsu.ButtonClicked

        '選択されたRow,Colのindexを設定
        dataHBKD0201.PropIntRowSelect = e.Row
        dataHBKD0201.PropIntColSelect = e.Column

        Select Case e.Column
            Case logicHBKD0201.COL_YOJITSU_WORKSCEDT_BTN, logicHBKD0201.COL_YOJITSU_WORKSTDT_BTN, logicHBKD0201.COL_YOJITSU_WORKEDDT_BTN
                '「日付設定画面」インスタンス作成
                Dim HBKZ0801 As New HBKZ0801

                With dataHBKD0201.PropVwPrbYojitsu.Sheets(0)

                    'パラメータセット
                    If Not .GetText(e.Row, e.Column - 1).Equals("") Then
                        '日付、時間を設定
                        HBKZ0801.dataHBKZ0801.PropStrDate = Mid(.GetText(e.Row, e.Column - 1), 1, InStr(.GetText(e.Row, e.Column - 1), " ") - 1)
                        HBKZ0801.dataHBKZ0801.PropStrTime = Mid(.GetText(e.Row, e.Column - 1), InStr(.GetText(e.Row, e.Column - 1), " ") + 1)
                    End If

                    If HBKZ0801.ShowDialog Then
                        .SetValue(e.Row, e.Column - 1, HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text + " " + HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text)
                        '更新フラグを立てる
                        dataHBKD0201.PropDtwkRireki.Rows(e.Row).EndEdit()
                    End If

                End With

            Case logicHBKD0201.COL_YOJITSU_PRBTANTO_BTN

                '「ひびきユーザー検索一覧」インスタンス作成
                Dim HBKZ0101 As New HBKZ0101

                '検索一覧受け渡し用データ作成
                If logicHBKD0201.CreateDtPrbYojitsuTantoMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                'パラメータセット
                With HBKZ0101.dataHBKZ0101
                    .PropMode = SELECT_MODE_MULTI                              'モード：複数選択
                    .PropArgs = String.Empty                                   '検索条件：選択されたユーザーID、GP
                    .PropSplitMode = SPLIT_MODE_AND                            '検索条件区切り：AND
                    .PropDataTable = dataHBKD0201.PropDtResultWkTanto          '設定済みデータ
                    .PropInitMode = 1                                          '専用フラグ
                End With

                'グループ検索画面を表示し、戻り値としてデータテーブルを取得
                dataHBKD0201.PropDtResultWkTanto = HBKZ0101.ShowDialog()

                '選択件数が設定範囲を超える場合エラー
                If dataHBKD0201.PropDtResultWkTanto IsNot Nothing AndAlso dataHBKD0201.PropDtResultWkTanto.Rows.Count > PRB_WKRIREKI_MAXTANTO Then
                    puErrMsg = String.Format(D0201_E030, PRB_WKRIREKI_MAXTANTO)
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '作業予実スプレッド内担当者追加処理
                If logicHBKD0201.AddPrbYojitsuTantoMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

                '更新フラグを立てる
                dataHBKD0201.PropDtwkRireki.Rows(e.Row).EndEdit()

        End Select

    End Sub

    ''' <summary>
    ''' 会議情報：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議検索一覧を表示し選択されたデータを画面に設定する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub btnAddRow_Meeting_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_Meeting.Click

        Dim frmHBKC0301 As New HBKC0301
        'パラメータセット
        With frmHBKC0301.dataHBKC0301
            .PropBlnTranFlg = SELECT_MODE_NOTMENU                               '前画面パラメータ：メニュー遷移フラグ
            .PropProcessKbn = PROCESS_TYPE_QUESTION                             'プロセス区分
            .PropProcessNmb = dataHBKD0201.PropIntPrbNmb                        'プロセス番号
            .PropTitle = dataHBKD0201.PropTxtTitle.Text                         'タイトル
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0301, Form).FormClosed, AddressOf Meeting_FormClosed

        '会議情報検索画面を表示
        If frmHBKC0301.ShowDialog = DIALOG_RETURN_OK Then

            '検索結果を取得
            dataHBKD0201.PropDtResultTemp = frmHBKC0301.dataHBKC0301.PropDtReturnSub

            '会議情報一覧に取得データをセット
            If logicHBKD0201.AddRowMeetingMain(dataHBKD0201) = False Then
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
    ''' <para>作成情報：2012/09/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Meeting_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        '会議結果情報再取得処理
        If logicHBKD0201.RefreshMeetingMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 会議情報：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択したスプレッド行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Meeting_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_Meeting.Click

        '会議情報一覧選択行削除処理
        If logicHBKD0201.RemoveRowMeetingMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 対応関係者情報：[+G]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelaG_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_RelaG.Click

        'グループ検索画面インスタンス作成
        Dim frmHBKZ0301 As New HBKZ0301

        'パラメータセット
        With frmHBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_MULTI       'モード：複数選択
            .PropArgs = String.Empty            '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND     '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultPrbKankei = frmHBKZ0301.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKD0201.SetGroupToVwRelationMain(dataHBKD0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 対応関係者情報：[+U]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザー検索画面を表示し、選択されたグループ・ユーザーを当画面にセットする
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelaU_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_RelaU.Click

        'ひびきユーザー検索画面インスタンス作成
        Dim frmHBKZ0101 As New HBKZ0101

        'パラメータセット
        With frmHBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'ひびきユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultPrbKankei = frmHBKZ0101.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKD0201.SetUserToVwRelationMain(dataHBKD0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 対応関係者情報：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>対応関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Rela_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_Rela.Click

        '対応関係者情報一覧選択行削除処理
        If logicHBKD0201.RemoveRowRelationMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' プロセスリンク情報：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセスリンク情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Plink_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_Plink.Click

        Dim frmHBKZ0401 As New HBKZ0401
        'パラメータセット
        With frmHBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI                               'モード：単一選択
            .PropArgs = String.Empty                                    '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
            .PropStrFromProcessKbn = PROCESS_TYPE_QUESTION               'プロセス区分
            .PropStrFromProcessNmb = dataHBKD0201.PropIntPrbNmb         'プロセス番号
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKD0201.PropDtResultPLink = frmHBKZ0401.ShowDialog()

        'プロセスリンク一覧に取得データをセット
        If logicHBKD0201.AddRowpLinkMain(dataHBKD0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' プロセスリンク情報：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>プロセスリンク情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Plink_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_Plink.Click

        'プロセスリンク情報一覧選択行削除処理
        If logicHBKD0201.RemoveRowpLinkMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' CYSPR情報：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>CYSPR情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Cyspr_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_Cyspr.Click

        'プロセスリンク一覧に取得データをセット
        If logicHBKD0201.AddRowCysprInfoMain(dataHBKD0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' CYSPR情報：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>CYSPR情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/20 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Cyspr_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_Cyspr.Click

        'CYSPR情報一覧選択行削除処理
        If logicHBKD0201.RemoveRowCysprInfoMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 関連ファイル情報：[+]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_File_Click(sender As System.Object, e As System.EventArgs) Handles btnAddRow_File.Click

        Dim frmHBKZ1101 As New HBKZ1101
        'パラメータセット
        With frmHBKZ1101.dataHBKZ1101

        End With

        '関連ファイル検索画面を表示し、戻り値としてデータテーブルを取得
        If frmHBKZ1101.ShowDialog() Then

            dataHBKD0201.PropStrFileNaiyo = frmHBKZ1101.dataHBKZ1101.PropTxtFileNaiyo.Text
            dataHBKD0201.PropStrFilePath = frmHBKZ1101.dataHBKZ1101.PropTxtFilePath.Text

            '関係ファイル一覧に取得データをセット
            If logicHBKD0201.AddRowFileInfoMain(dataHBKD0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub

    ''' <summary>
    ''' 関連ファイル情報：[-]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_File_Click(sender As System.Object, e As System.EventArgs) Handles btnRemoveRow_File.Click

        '関連ファイル一覧選択行削除処理
        If logicHBKD0201.RemoveRowFileInfoMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 関連ファイル情報：[開]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルを読み取り専用で開く
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOpenFile_Click(sender As System.Object, e As System.EventArgs) Handles btnOpenFile.Click

        If dataHBKD0201.PropStrProcMode = PROCMODE_EDIT OrElse _
            dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI OrElse _
            dataHBKD0201.PropStrProcMode = PROCMODE_REF Then        '編集モード 、作業予実モード 、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwPrbFileInfo.Visible = True) AndAlso (Me.vwPrbFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwPrbFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(D0201_E026, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(D0201_E026, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKD0201.PropIntSelectedRow = Me.vwPrbFileInfo.ActiveSheet.ActiveRowIndex

            '            'ファイルオープン処理
            '            If logicHBKD0201.FileOpenMain(dataHBKD0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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

            If (Me.vwPrbFileInfo.Visible = True) AndAlso (Me.vwPrbFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwPrbFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwPrbFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwPrbFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwPrbFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = D0201_E026
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKD0201.PropIntSelectedRow = Me.vwPrbFileInfo.ActiveSheet.ActiveRowIndex

                'ファイルオープン処理
                If logicHBKD0201.FileOpenMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' 関連ファイル情報：[ダ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルをローカルのデスクトップにダウンロードする。
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSaveFile_Click(sender As System.Object, e As System.EventArgs) Handles btnSaveFile.Click

        If dataHBKD0201.PropStrProcMode = PROCMODE_EDIT OrElse _
           dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI OrElse _
           dataHBKD0201.PropStrProcMode = PROCMODE_REF Then        '編集モード 、作業予実モード 、参照モード

            '[Del] 2012/10/30 s.yamaguchi END
            'If (Me.vwPrbFileInfo.Visible = True) AndAlso (Me.vwPrbFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwPrbFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(D0201_E026, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(D0201_E026, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKD0201.PropIntSelectedRow = Me.vwPrbFileInfo.ActiveSheet.ActiveRowIndex

            '            '編集モード画面処理
            '            If logicHBKD0201.FileDownLoadMain(dataHBKD0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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

            If (Me.vwPrbFileInfo.Visible = True) AndAlso (Me.vwPrbFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwPrbFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwPrbFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwPrbFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwPrbFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = D0201_E026
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKD0201.PropIntSelectedRow = Me.vwPrbFileInfo.ActiveSheet.ActiveRowIndex

                '編集モード画面処理
                If logicHBKD0201.FileDownLoadMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' [登録／作業予実登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、モードに応じて登録処理を行う。
    ''' <para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(sender As System.Object, e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        dataHBKD0201.PropLblKanryoMsg.Text = ""
        Application.DoEvents()

        '***************************************************
        '処理モードに応じた入力チェックを行う
        '***************************************************
        If dataHBKD0201.PropStrProcMode = PROCMODE_NEW Then                     '新規登録モード

            '入力チェック処理      
            If logicHBKD0201.CheckInputValueMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKD0201.PropStrProcMode = PROCMODE_EDIT Then                '編集モード

            'ロック解除チェック(dataHBKC0201.PropBlnBeLockedFlg)
            If logicHBKD0201.CheckBeUnlockedMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
            If dataHBKD0201.PropBlnBeLockedFlg = False Then                     '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKD0201.CheckInputValueMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKD0201.PropBlnBeLockedFlg = True Then                  '参照モード

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKD0201.SetFormRefModeFromEditModeMain(dataHBKD0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
                If dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_W001, dataHBKD0201.PropStrLogFilePath)) Then
                    'ロック解除メッセージ表示
                    MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                ElseIf dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_E021, dataHBKD0201.PropStrLogFilePath)) Then
                    'データ更新エラーメッセージ
                    MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                End If

                '編集モードで画面再描画
                dataHBKD0201.PropStrProcMode = PROCMODE_EDIT
                HBKD0201_Load(Me, New EventArgs)
                Exit Sub
            End If

        ElseIf dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI Then             '作業予実モード

            '更新有無チェック
            If add_row_cnt = 0 AndAlso bln_update_flg = False Then
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ設定
                puErrMsg = D0201_E023
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            '入力チェックを行う
            If logicHBKD0201.CheckInputValueMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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

        '***************************************************
        '処理モードに応じた登録処理を行う
        '***************************************************
        If dataHBKD0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKD0201.RegistDataOnNewModeMain(dataHBKD0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default

                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKD0201.SetFormRefModeFromEditModeMain(dataHBKD0201) = True Then
                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_W001, dataHBKD0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                    ElseIf dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_E021, dataHBKD0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If
                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_QUESTION
            PropLastProcessNmb = dataHBKD0201.PropIntPrbNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(D0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKD0201.PropLblKanryoMsg.Text = D0201_I001


            '編集モードで画面再描画
            dataHBKD0201.PropStrProcMode = PROCMODE_EDIT
            '呼び出し元画面を検索一覧にする
            dataHBKD0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN
            HBKD0201_Load(Me, New EventArgs)

        ElseIf dataHBKD0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '更新処理を行う
            If logicHBKD0201.RegistDataOnEditModeMain(dataHBKD0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKD0201.SetFormRefModeFromEditModeMain(dataHBKD0201) = True Then
                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_W001, dataHBKD0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                    ElseIf dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_E021, dataHBKD0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_QUESTION
            PropLastProcessNmb = dataHBKD0201.PropIntPrbNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(D0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKD0201.PropLblKanryoMsg.Text = D0201_I001

            '編集モードで画面再描画
            HBKD0201_Load(Me, New EventArgs)


        ElseIf dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI Then      '作業履歴モード

            '更新処理を行う
            If logicHBKD0201.RegistDataOnYojitsuModeMain(dataHBKD0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKD0201.SetFormRefModeFromEditModeMain(dataHBKD0201) = True Then
                    'セットされているメッセージによってメッセージボックスのスタイルを変更する
                    If dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_W001, dataHBKD0201.PropStrLogFilePath)) Then
                        'ロック解除メッセージ表示
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                    ElseIf dataHBKD0201.PropStrBeUnlockedMsg.StartsWith(String.Format(D0201_E021, dataHBKD0201.PropStrLogFilePath)) Then
                        'データ更新エラーメッセージ
                        MsgBox(dataHBKD0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    End If
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_QUESTION
            PropLastProcessNmb = dataHBKD0201.PropIntPrbNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(D0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKD0201.PropLblKanryoMsg.Text = D0201_I001

            '編集モードで画面再描画
            dataHBKD0201.PropStrProcMode = PROCMODE_EDIT
            HBKD0201_Load(Me, New EventArgs)

        End If

        'タイマーを開始する
        Me.timKanryo.Start()

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/08/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReturn_Click(sender As System.Object, e As System.EventArgs) Handles btnReturn.Click

        '当画面を閉じる
        Me.Close()

    End Sub



    '★★--各種制御イベント

    ''' <summary>
    ''' 作業履歴一覧：編集モード解除時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>内部フラグを参照し、変更されたかを確認する
    ''' <para>作成情報：2012/08/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwPrbYojitsu_EditModeOff(sender As Object, e As System.EventArgs) Handles vwPrbYojitsu.EditModeOff
        If bln_chg_flg Then
            If add_row_cnt > 0 AndAlso add_row_cnt >= vwPrbYojitsu.Sheets(0).ActiveRowIndex Then
                '新規追加行を変更した
            Else
                bln_update_flg = True
            End If
        End If
    End Sub


    ''' <summary>
    ''' 作業予実一覧：編集モード開始時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>内部フラグを初期化する
    ''' <para>作成情報：2012/08/21 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwPrbYojitsu_EditModeOn(sender As System.Object, e As System.EventArgs) Handles vwPrbYojitsu.EditModeOn

        bln_chg_flg = False

        'アクティブセルのセル型を判断します
        Dim intRow As Integer = vwPrbYojitsu.ActiveSheet.ActiveRowIndex
        Dim intCol As Integer = vwPrbYojitsu.ActiveSheet.ActiveColumnIndex
        If intRow >= 0 And intCol = logicHBKD0201.COL_YOJITSU_SYSTEM Then
            ' ドロップダウンリストのオブジェクトを取得します
            Dim cmbSpread As FarPoint.Win.Spread.FpSpread = _
                CType(CType(vwPrbYojitsu.EditingControl, FarPoint.Win.Spread.CellType.GeneralEditor).SubEditor, FarPoint.Win.Spread.FpSpread)

            ' ドロップダウンリストの 1,2列目を非表示にします
            cmbSpread.ActiveSheet.Columns(0).Visible = False
            cmbSpread.ActiveSheet.Columns(1).Visible = False

        End If

    End Sub

    ''' <summary>
    ''' [解除]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面のロックを解除し、編集モードで表示する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKD0201.UnlockWhenClickBtnUnlockMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' フォームクローズ時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者＋編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/07/13 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKD0201_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '別画面が開いている場合、クローズ処理を行わない
        If dataHBKD0201.PropfrmInstance IsNot Nothing Then
            'クローズ処理キャンセル
            e.Cancel = True
            'エラーメッセージ設定
            puErrMsg = D0201_E025
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '編集モードの場合はロック解除を行う
        If dataHBKD0201.PropStrProcMode = PROCMODE_EDIT And _
            (dataHBKD0201.PropBlnBeLockedFlg = False) Then

            '画面クローズ時ロック解除処理
            If logicHBKD0201.UnlockWhenCloseMain(dataHBKD0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' [メール作成]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMail_Click(sender As System.Object, e As System.EventArgs) Handles btnMail.Click
        'メールテンプレート選択画面
        Dim HBKZ1001 As New HBKZ1001
        '問題登録（メール作成）処理インスタンス化
        Dim logicHBKD0301 As New LogicHBKD0203
        Dim dataHBKD0301 As New DataHBKD0203

        'パラメータ設定
        With HBKZ1001.dataHBKZ1001
            .PropStrGroupCD = PropWorkGroupCD                                                                       '前画面パラメータ：グループCD
            .PropStrGroupNM = PropWorkGroupName                                                                     '前画面パラメータ：グループ名
            .PropStrProcessKbn = PROCESS_TYPE_QUESTION                                                              '前画面パラメータ：プロセス区分
            .PropStrKigenCondCIKbnCD = ""
            .PropStrKigenCondTypeKbn = ""
            .PropStrKigenCondKigen = ""
            .PropStrKigenCondKbn = ""
        End With
        'メールフォーマット選択
        HBKZ1001.ShowDialog()

        'メールフォーマットデータ取得
        dataHBKD0301.PropDtReturnData = HBKZ1001.dataHBKZ1001.PropDtReturnData

        '当画面表示
        Me.Show()

        '戻り値のDataTableがNullだった場合、処理を中断
        If dataHBKD0301.PropDtReturnData Is Nothing Then
            Exit Sub
        End If


        '入力項目をデータクラスにセット
        With dataHBKD0301
            .PropStrPrbNmb = dataHBKD0201.PropTxtPrbNmb.Text                                                        '問題番号
            .PropStrProcessStateCD = dataHBKD0201.PropCmbStatus.Text                                                'ステータス
            .PropStrKaisiDT = dataHBKD0201.PropDtpStartDT.txtDate.Text                                              '発生日時
            .PropStrKaisiDT_HM = dataHBKD0201.PropTxtStartDT_HM.PropTxtTime.Text                                    '発生日時時分
            .PropStrKanryoDT = dataHBKD0201.PropDtpKanryoDT.txtDate.Text                                            '完了日時
            .PropStrKanryoDT_HM = dataHBKD0201.PropTxtKanryoDT_HM.PropTxtTime.Text                                  '完了日時時分
            .PropStrTitle = dataHBKD0201.PropTxtTitle.Text                                                          'タイトル
            .PropStrSource = dataHBKD0201.PropCmbPrbCase.Text                                                       '発生原因
            .PropStrNaiyo = dataHBKD0201.PropTxtNaiyo.Text                                                          '内容
            .PropStrRegGrpNM = dataHBKD0201.PropStrRegGp                                                            '登録者グループ名
            .PropStrRegNM = dataHBKD0201.PropStrRegUsr                                                              '登録者ユーザ名
            .PropStrRegDT = dataHBKD0201.PropStrRegDT                                                               '登録日時
            .PropStrUpdateGrpNM = dataHBKD0201.PropStrUpdateGp                                                      '最終更新グループ名
            .PropStrUpdateNM = dataHBKD0201.PropStrUpdateUsr                                                        '最終更新者
            .PropStrUpdateDT = dataHBKD0201.PropStrUpdateDT                                                         '最終更新日時
            '対象システム
            .PropStrSystemNmb = dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue.ToString
            .PropStrTantoGrp = dataHBKD0201.PropCmbTantoGrp.Text                                                    '担当グループ
            .PropStrPrbTanto = dataHBKD0201.PropTxtPrbTantoID.Text & " " & dataHBKD0201.PropTxtPrbTantoNM.Text      '担当者
            .PropStrTantoNM = dataHBKD0201.PropTxtPrbTantoNM.Text                                                   '担当者氏名
            .PropStrTaisyo = dataHBKD0201.PropTxtTaisyo.Text                                                        '対処
            .PropStrTaisyoUser = dataHBKD0201.PropTxtApproverNM.Text & " " & dataHBKD0201.PropTxtApproverNM.Text    '対象の承認者
            .PropStrRecordUser = dataHBKD0201.PropTxtRecorderID.Text & " " & dataHBKD0201.PropTxtRecorderNM.Text    '承認記録者
            .PropVwCysprInfo = dataHBKD0201.PropVwCysprInfo                                                         'CYSPR
            .PropVwFileInfo = dataHBKD0201.PropVwPrbFileInfo                                                        '関連情報(ファイル,ファイル説明)
            '作業履歴データ(経過種別,対象オブジェクト,作業予定日時,作業開始日時,作業終了日時,作業内容,作業担当者業務チーム,作業担当者)
            .PropVwPrbYojitsu = dataHBKD0201.PropVwPrbYojitsu
            .PropVwMeeting = dataHBKD0201.PropVwMeeting                                                             '会議情報データ(番号,実施日,タイトル,承認)
            .PropStrBIko1 = dataHBKD0201.PropTxtFreeText1.Text                                                      'テキスト１
            .PropStrBIko2 = dataHBKD0201.PropTxtFreeText2.Text                                                      'テキスト２
            .PropStrBIko3 = dataHBKD0201.PropTxtFreeText3.Text                                                      'テキスト３
            .PropStrBIko4 = dataHBKD0201.PropTxtFreeText4.Text                                                      'テキスト４
            .PropStrBIko5 = dataHBKD0201.PropTxtFreeText5.Text                                                      'テキスト５
            'フリーフラグ1
            If dataHBKD0201.PropChkFreeFlg1.Checked = True Then
                .PropStrFreeFlg1 = FLG_ON_NM
            Else
                .PropStrFreeFlg1 = FLG_OFF_NM
            End If
            'フリーフラグ2
            If dataHBKD0201.PropChkFreeFlg2.Checked = True Then
                .PropStrFreeFlg2 = FLG_ON_NM
            Else
                .PropStrFreeFlg2 = FLG_OFF_NM
            End If
            'フリーフラグ3
            If dataHBKD0201.PropChkFreeFlg3.Checked = True Then
                .PropStrFreeFlg3 = FLG_ON_NM
            Else
                .PropStrFreeFlg3 = FLG_OFF_NM
            End If
            'フリーフラグ4
            If dataHBKD0201.PropChkFreeFlg4.Checked = True Then
                .PropStrFreeFlg4 = FLG_ON_NM
            Else
                .PropStrFreeFlg4 = FLG_OFF_NM
            End If
            'フリーフラグ5
            If dataHBKD0201.PropChkFreeFlg5.Checked = True Then
                .PropStrFreeFlg5 = FLG_ON_NM
            Else
                .PropStrFreeFlg5 = FLG_OFF_NM
            End If
            .PropVwRelation = dataHBKD0201.PropVwRelationInfo                                                       '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
            .PropStrGrpHistory = dataHBKD0201.PropTxtGrpRireki.Text                                                 'グループ履歴
            .PropStrTantoHistory = dataHBKD0201.PropTxtTantoRireki.Text                                             '担当者履歴
            .PropVwprocessLinkInfo = dataHBKD0201.PropVwProcessLinkInfo                                             'プロセスリンク管理番号(区分,番号)

        End With

        'メール作成処理呼び出し
        If logicHBKD0301.CreateIncidentMailMain(dataHBKD0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMeeting_CellDoubleClick(sender As System.Object, e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMeeting.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKD0201.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKD0201.COL_MEETING_RESULTKBNCD).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKD0201.COL_MEETING_NMB).Value     '選択行の管理番号

        '会議記録登録
        Dim HBKC0401 As New HBKC0401

        '会議記録登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = 0                             'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
            .PropProcessKbn = PROCESS_TYPE_QUESTION         'プロセス区分
            .PropProcessNmb = dataHBKD0201.PropIntIncNmb    'プロセス番号
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
    ''' プロセスリンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/27 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwProcessLinkInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwProcessLinkInfo.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKD0201.COL_PLINK_PLINKKBNCD).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKD0201.COL_PLINK_PLINKNO).Value   '選択行の管理番号

        '関係者チェック_結果格納用
        Dim intchkkankei As Integer = 0

        'プロセスリンク対応関係者チェック処理(dataHBKE0201.PropintChkKankei) 
        If logicHBKD0201.PlinkKankeiCheckMain(intchkkankei, strSelectNo, strSelectKbn) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
            puErrMsg = D0201_E017
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

            '問題登録画面インスタンス作成
            Dim HBKD0201 As New HBKD0201
            'インシデント登録画面データクラスにパラメータをセット
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

            ''★★★DEBUG★★★
            'MsgBox("リリース登録画面に遷移します")

        End If

    End Sub

    ''' <summary>
    ''' タブページ切替時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>サポセン機器情報タブ選択時、基本情報タブの相手情報をサポセン機器情報タブの相手情報へコピーする
    ''' <para>作成情報：2012/08/28 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub tbInput_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles tbInput.SelectedIndexChanged

        ''基本情報：拡大時、リサイズする
        'If dataHBKD0201.PropBlnKakudaiFlg = True Then
        '    btnKakudai_Click(Me, New EventArgs)
        'End If

        '前回選択タブが基本情報の場合は拡大をもどす
        Select Case intSelectedTabIdx
            Case logicHBKD0201.TAB_KHN
                kakudai(False)

            Case Else
                '基本情報タブに戻る場合
                If Me.tbInput.SelectedIndex = logicHBKD0201.TAB_KHN Then
                    kakudai(Settings.Instance.propHBKD0201_Expantion_wkRireki)
                End If

        End Select

        '前回選択タブにカレントタブを設定
        intSelectedTabIdx = Me.tbInput.SelectedIndex

    End Sub

    ''' <summary>
    ''' [単票出力]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>[出力形式選択]画面を表示し選択された出力形式で単票出力を行う
    ''' <para>作成情報：2012/08/30 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnPrint_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPrint.Click

        '出力形式選択画面
        Dim HBKZ0901 As New HBKZ0901

        Dim intOutputKbn As Integer = HBKZ0901.ShowDialog()
        If intOutputKbn <> OUTPUT_RETURN_CANCEL Then

            Dim logicHBKD0202 As New LogicHBKD0202
            If logicHBKD0202.InitMain(dataHBKD0201, intOutputKbn) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If

    End Sub

    ''' <summary>
    ''' [変更登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リリース登録画面を開く
    ''' <para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnHenkou_Click(sender As System.Object, e As System.EventArgs) Handles btnHenkou.Click
        '変更登録画面
        dataHBKD0201.PropfrmInstance = New HBKE0201

        'パラメータ設定
        With dataHBKD0201.PropfrmInstance.dataHBKE0201
            .PropStrProcMode = PROCMODE_NEW
            .PropIntPrbNmb = dataHBKD0201.PropIntPrbNmb
            .PropVwProcessLinkInfo_Save = dataHBKD0201.PropVwProcessLinkInfo
            .PropIntTSystemNmb = dataHBKD0201.PropCmbTargetSystem.PropCmbColumns.SelectedValue
            '★閉じるボタン表示用のフラグを渡す
            .PropIntOwner = SCR_CALLMOTO_REG
        End With

        'クローズ処理の追加
        AddHandler CType(dataHBKD0201.PropfrmInstance, Form).FormClosed, AddressOf frm_FormClosed

        '画面制御開始
        Scr_Enabled_Start()

        '別画面として表示
        dataHBKD0201.PropfrmInstance.Show()
    End Sub

    ''' <summary>
    ''' [変更登録]ボタン非活性処理
    ''' </summary>
    ''' <param name="Ctl">[IN]コントロール</param>
    ''' <param name="aryList">[IN/OUT]活性リスト</param>
    ''' <remarks>活性ボタンオブジェクトの活性リスト作成し非活性とする
    ''' <para>作成情報：2012/09/07 r.hoshino
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
    ''' <para>作成情報：2012/09/07 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub frm_FormClosed(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosedEventArgs)

        'プロセスリンク再取得処理
        If logicHBKD0201.RefreshPLinkMain(dataHBKD0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKD0201.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/09/07 r.hoshino
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
    ''' 共通_画面制御処理_別画面表示前処理
    ''' </summary>
    ''' <remarks>ボタン非活性など画面制御を行う。
    ''' <para>作成情報：2012/09/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub Scr_Enabled_Start()
        '親画面のすべてのボタンを非活性とする
        dataHBKD0201.PropAryfrmCtlList = New ArrayList
        SetButonEnabledFalse(Me.Controls, dataHBKD0201.PropAryfrmCtlList)

        'イベント停止
        RemoveHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        RemoveHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick
        RemoveHandler vwPrbYojitsu.ButtonClicked, AddressOf vwPrbYojitsu_ButtonClicked

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
        dataHBKD0201.PropfrmInstance = Nothing

        '親画面の非活性の状態を戻す
        SetButonEnabledTrue(Me.Controls, dataHBKD0201.PropAryfrmCtlList)
        dataHBKD0201.PropAryfrmCtlList = Nothing

        'イベント復活
        AddHandler vwMeeting.CellDoubleClick, AddressOf vwMeeting_CellDoubleClick
        AddHandler vwProcessLinkInfo.CellDoubleClick, AddressOf vwProcessLinkInfo_CellDoubleClick
        AddHandler vwPrbYojitsu.ButtonClicked, AddressOf vwPrbYojitsu_ButtonClicked

    End Sub

    ''' <summary>
    ''' インタバール経過後の処理の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/09/24 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub Timer1_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles timKanryo.Tick
        '登録完了ラベルを初期化する 
        dataHBKD0201.PropLblKanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub

    ''' <summary>
    ''' 内容フォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtUkeNaiyo_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtNaiyo.Validating

        With dataHBKD0201

            If .PropTxtNaiyo.Text.Length > 3000 Then

                .PropStrLostFucs = .PropTxtNaiyo.Text

                '桁数チェック
                If logicHBKD0201.CheckLostFocus(dataHBKD0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                    ''3000文字以上の場合、先頭から3000文字だけ切り取って入れなおす
                    '.PropTxtNaiyo.Text = .PropStrLostFucs.ToString.Substring(0, 3000)
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If

        End With
    End Sub

    ''' <summary>
    ''' 結果フォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub txtTaioKekka_Validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles txtTaisyo.Validating

        With dataHBKD0201
            If .PropTxtTaisyo.Text.Length > 3000 Then

                .PropStrLostFucs = .PropTxtTaisyo.Text

                '桁数チェック
                If logicHBKD0201.CheckLostFocus(dataHBKD0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 START
                    ''3000文字以上の場合、先頭から3000文字だけ切り取って入れなおす
                    '.PropTxtTaisyo.Text = .PropStrLostFucs.ToString.Substring(0, 3000)
                    '[DELETE]2013/03/21 t.fukuo 閾値超過の場合文字削除しないよう修正 END
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If
        End With

    End Sub

    ''' <summary>
    ''' 作業内容セルフォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwIncRireki_LeaveCell(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.LeaveCellEventArgs) Handles vwPrbYojitsu.LeaveCell

        With dataHBKD0201
            '作業内容セルからフォーカスが離れた時
            If e.Column = logicHBKD0201.COL_YOJITSU_WORKNAIYO Then

                '桁数チェック
                If logicHBKD0201.CheckLostFocusSpread(dataHBKD0201) = False Then
                    'フォーカス移動キャンセル
                    e.Cancel = True
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
                    '処理終了
                    Exit Sub
                End If

            End If
        End With
    End Sub

    ''' <summary>
    ''' スプレッドフォーカス遷移後の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>フォーカス遷移を行った時に入力チェックをする
    ''' <para>作成情報：2012/10/24 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Sub vwIncRireki_validating(ByVal sender As System.Object, ByVal e As System.ComponentModel.CancelEventArgs) Handles vwPrbYojitsu.Validating

        '桁数チェック
        If logicHBKD0201.CheckLostFocusSpread(dataHBKD0201) = False Then
            'フォーカス移動キャンセル
            e.Cancel = True
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Exclamation, TITLE_INFO)
            '処理終了
            Exit Sub
        End If
    End Sub

End Class