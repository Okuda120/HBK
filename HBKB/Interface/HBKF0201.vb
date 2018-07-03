Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread

Public Class HBKF0201

    Public dataHBKF0201 As New DataHBKF0201
    Private logicHBKF0201 As New LogicHBKF0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    Dim bln_chg_flg As Boolean          '内部更新フラグ
    Dim add_row_cnt As Integer          '新規追加をカウント、行削除はマイナスする
    Dim bln_update_flg As Boolean       '最終的な更新フラグ
    Dim init_row_cnt As Integer         '初期表示カウント

    ''' <summary>
    ''' フォーム終了時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面情報の保存を行う
    ''' <para>作成情報：2012/10/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKF0201_FormClosed(sender As Object, e As System.Windows.Forms.FormClosedEventArgs) Handles Me.FormClosed
        '設定を変更する
        With Settings.Instance
            'ウィンドウが最大化、最小化以外は保存
            If Me.WindowState = FormWindowState.Normal Then
                .propHBKF0201_Height = Me.Size.Height
                .propHBKF0201_Width = Me.Size.Width
                .propHBKF0201_Y = Me.Location.Y
                .propHBKF0201_X = Me.Location.X
                .propHBKF0201_WindowState = Me.WindowState
            ElseIf Me.WindowState = FormWindowState.Maximized Then
                '最大化は状態を保存
                .propHBKF0201_WindowState = Me.WindowState
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
    Private Sub HBKF0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'XMLファイルから設定を読み込む
        Settings.LoadFromXmlFile()
        '最大化とか
        Me.WindowState = Settings.Instance.propHBKF0201_WindowState
        'サイズが0だったら初期状態とみなし通常の表示をする。
        If Settings.Instance.propHBKF0201_Height <> 0 Then
            'Me.StartPosition = FormStartPosition.Manual
            'サイズ変更
            Me.Size = New Point(Settings.Instance.propHBKF0201_Width, Settings.Instance.propHBKF0201_Height)
            Me.Location = New Point(Settings.Instance.propHBKF0201_X, Settings.Instance.propHBKF0201_Y)
        End If


        'データクラスの初期設定
        With dataHBKF0201

            .PropGrpLoginUser = Me.grpLoginUser                             'ログイン：ログイン情報グループボックス

            .PropTxtRelNmb = Me.txtRelNmb                                   'ヘッダ：リリース管理番号
            .PropLblRegInfo = Me.lblRegInfo                                 'ヘッダ：登録番号
            .PropLblFinalUpdateInfo = Me.lblFinalUpdateInfo                 'ヘッダ：最終更新情報
            .PropLblkanryoMsg = Me.LblkanryoMsg                             'ヘッダ：完了メッセージ

            .PropTbInput = Me.tbInput                                       'タブ

            .PropTxtRelUkeNmb = Me.txtRelUkeNmb                             '基本情報：リリース受付番号テキストボックス
            .PropCmbProcessState = Me.cmbProcessState                       '基本情報：ステータスコンボボックス
            .PropDtpIraiDT = Me.dtpIraiDT                                   '基本情報：依頼日（起票日）デートタイムピッカー
            .PropCmbTujyoKinkyuKbn = Me.cmbTujyoKinkyuKbn                   '基本情報：通常・緊急コンボボックス
            .PropCmbUsrSyutiKbn = Me.cmbUsrSyutiKbn                         '基本情報：ユーザー周知必要有無コンボボックッス
            .PropVwIrai = Me.vwIrai                                         '基本情報：リリース依頼受領システムスプレッド
            .PropBtnAddRow_Irai = Me.btnAddRow_Irai                         '基本情報：リリース依頼受領行追加ボタン
            .PropBtnRemoveRow_Irai = Me.btnRemoveRow_Irai                   '基本情報：リリース依頼受領行削除ボタン
            .PropVwJissi = Me.vwJissi                                       '基本情報：リリース実施対象システムスプレッド
            .PropBtnAddRow_Jissi = Me.btnAddRow_Jissi                       '基本情報：リリース実施対象行追加ボタン
            .PropBtnRemoveRow_Jissi = Me.btnRemoveRow_Jissi                 '基本情報：リリース実施対象行削除ボタン
            .PropDtpRelSceDT = Me.dtpRelSceDT                               '基本情報：リリース予定日時（目安）デートタイムピッカー
            .PropTxtRelSceDT_HM = Me.txtRelSceDT_HM                         '基本情報：リリース予定日時時分表示テキストボックス
            .PropBtnRelSceDT_HM = Me.btnRelSceDT_HM                         '基本情報：リリース予定日時時分表示ボタン
            .PropCmbTantoGrpCD = Me.cmbTantoGrpCD                           '基本情報：担当グループコンボボックス
            .PropTxtRelTantoID = Me.txtRelTantoID                           '基本情報：担当IDテキストボックス
            .PropTxtRelTantoNM = Me.txtRelTantoNM                           '基本情報：担当氏名テキストボックス
            .PropBtnSearch = Me.btnSearch                                   '基本情報：検索ボタン
            .PropBtnMy = Me.btnMy                                           '基本情報：私ボタン
            .PropDtpRelStDT = Me.dtpRelStDT                                 '基本情報：リリース着手日時デートタイムピッカー
            .PropTxtRelStDT_HM = Me.txtRelStDT_HM                           '基本情報：リリース着手日時時分表示テキストボックス
            .PropBtnRelStDT_HM = Me.btnRelStDT_HM                           '基本情報：リリース着手日時時分表示ボタン
            .PropDtpRelEdDT = Me.dtpRelEdDT                                 '基本情報：リリース終了日時デートタイムピッカー
            .PropTxtRelEdDT_HM = Me.txtRelEdDT_HM                           '基本情報：リリース終了日時時分表示テキストボックス
            .PropBtnRelEdDT_HM = Me.btnRelEdDT_HM                           '基本情報：リリース終了日時時分表示ボタン
            .PropVwRelationFileInfo = Me.vwRelationFileInfo                 '基本情報：関連ファイル情報スプレッド
            .PropBtnAddRow_RelationFile = Me.btnAddRow_RelationFile         '基本情報：関連ファイル情報行追加
            .PropBtnRemoveRow_RelationFile = Me.btnRemoveRow_RelationFile   '基本情報：関連ファイル情報行削除
            .PropBtnRelationFileOpen = Me.btnRelationFileOpen               '基本情報：関連ファイル開くボタン
            .PropBtnRelationFileDownLoad = Me.btnRelationFileDownLoad       '基本情報：関連ファイルダウンロードボタン
            .PropTxtTitle = Me.txtTitle                                     '基本情報：タイトル
            .PropTxtGaiyo = Me.txtGaiyo                                     '基本情報：概要
            .PropVwMeeting = Me.vwMeeting                                   '会議情報：会議情報スプレッド
            .PropBtnAddRow_Meeting = Me.btnAddRow_Meeting                   '会議情報：会議情報行追加ボタン
            .PropBtnRemoveRow_Meeting = Me.btnRemoveRow_Meeting             '会議情報：会議情報行削除ボタン
            .PropTxtBIko1 = Me.txtBIko1                                     'フリー入力情報：テキスト１テキストボックス
            .PropTxtBIko2 = Me.txtBIko2                                     'フリー入力情報：テキスト２テキストボックス
            .PropTxtBIko3 = Me.txtBIko3                                     'フリー入力情報：テキスト３テキストボックス
            .PropTxtBIko4 = Me.txtBIko4                                     'フリー入力情報：テキスト４テキストボックス
            .PropTxtBIko5 = Me.txtBIko5                                     'フリー入力情報：テキスト５テキストボックス
            .PropChkFreeFlg1 = Me.chkFreeFlg1                               'フリー入力情報：フリーフラグ１チェックボックス
            .PropChkFreeFlg2 = Me.chkFreeFlg2                               'フリー入力情報：フリーフラグ２チェックボックス
            .PropChkFreeFlg3 = Me.chkFreeFlg3                               'フリー入力情報：フリーフラグ３チェックボックス
            .PropChkFreeFlg4 = Me.chkFreeFlg4                               'フリー入力情報：フリーフラグ４チェックボックス
            .PropChkFreeFlg5 = Me.chkFreeFlg5                               'フリー入力情報：フリーフラグ５チェックボックス
            .PropVwRelationInfo = Me.vwRelationInfo                         'フッタ：対応関係者情報スプレッド
            .PropBtnAddRow_relaG = Me.btnAddRow_relaG                       'フッタ：対応関係者情報グループ行追加ボタン
            .PropBtnAddRow_relaU = Me.btnAddRow_relaU                       'フッタ：対応関係者情報ユーザ行追加ボタン
            .PropBtnRemoveRow_rela = Me.btnRemoveRow_rela                   'フッタ：対応関係者情報行削除ボタン
            .PropVwProcessLinkInfo = Me.vwProcessLinkInfo                   'フッタ：プロセスリンク情報スプレッド
            .PropBtnAddRow_Plink = Me.btnAddRow_Plink                       'フッタ：プロセスリンク情報行追加ボタン
            .PropBtnRemoveRow_Plink = Me.btnRemoveRow_Plink                 'フッタ：プロセスリンク情報行削除ボタン
            .PropTxtGroupRireki = Me.txtGroupRireki                         'フッタ：グループ履歴テキストボックス
            .PropTxtTantoRireki = Me.txtTantoRireki                         'フッタ：担当履歴テキストボックス
            .PropBtnReg = Me.btnReg                                         'フッタ：登録ボタン
            .PropBtnMail = Me.btnMail                                       'フッタ：メール作成ボタン
            .PropBtnBack = Me.btnBack                                       'フッタ：戻るボタン

            'システムエラー事前対応処理
            If logicHBKF0201.DoProcForErrorMain(dataHBKF0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If


            '★初期化設定

            'スプレッド行のクリアを行う
            ' -- 2017/08/17 e.okuda 行数が0ではない条件付加 --
            With .PropVwRelationInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwRelationFileInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwProcessLinkInfo.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwIrai.Sheets(0)
                If .Rows.Count > 0 Then
                    .RemoveRows(0, .Rows.Count)
                End If
            End With
            With .PropVwJissi.Sheets(0)
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

            '★プロパティ設定

            '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 START
            'タイマーのインターバル設定
            Me.timKanryo.Interval = MSG_DISP_TIMER
            .PropLblkanryoMsg.Font = New Font(Me.Font.Name, Me.Font.Size, FontStyle.Bold)
            '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 END

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)


        '新規登録モードの場合
        If dataHBKF0201.PropStrProcMode = PROCMODE_NEW Then
            '関係 -、ロック -
            Me.Text = "ひびき：リリース登録"
            '新規モード画面初期表示メイン処理
            If logicHBKF0201.InitFormNewModeMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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

            '対応関係者チェック処理(dataHBKF0201.PropBlnChkKankei)
            If logicHBKF0201.KankeiCheckMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
            If dataHBKF0201.PropIntOwner <> SCR_CALLMOTO_HOKA Then

                '(ア)	編集モード
                If dataHBKF0201.PropIntChkKankei = KANKEI_CHECK_EDIT Then

                    'ロック設定メイン処理(dataHBKF0201.PropBlnBeLockedFlg )
                    If logicHBKF0201.LockMain(dataHBKF0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
                                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                                Exit Sub
                            End If
                        End If
                        'エラーメッセージ表示
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        '処理終了
                        Exit Sub
                    End If

                    If dataHBKF0201.PropBlnBeLockedFlg = False Then
                        '関係○、ロック○
                        Me.Text = "ひびき：リリース登録"
                        '編集モード画面初期表示メイン処理
                        If logicHBKF0201.InitFormEditModeMain(dataHBKF0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
                        Me.Text = "ひびき：リリース登録"
                        '参照モード変更
                        dataHBKF0201.PropStrProcMode = PROCMODE_REF

                        '参照モード画面初期表示メイン処理
                        If logicHBKF0201.InitFormRefModeMain(dataHBKF0201) = False Then
                            'システムエラー発生時はトランザクション系コントロールを非活性にする
                            If puErrMsg.StartsWith(HBK_E001) Then
                                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
                        MsgBox(dataHBKF0201.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

                    End If

                Else
                    '(イ)	参照モード
                    Me.Text = "ひびき：リリース登録"
                    '参照モード変更
                    dataHBKF0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKF0201.InitFormRefModeMain(dataHBKF0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
                If dataHBKF0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then

                    Me.Text = "ひびき：リリース登録"
                    'CLOSING処理の回避用に参照モードとする
                    dataHBKF0201.PropStrProcMode = PROCMODE_REF

                    '参照モード画面初期表示メイン処理
                    If logicHBKF0201.InitFormRefModeMain(dataHBKF0201) = False Then
                        'システムエラー発生時はトランザクション系コントロールを非活性にする
                        If puErrMsg.StartsWith(HBK_E001) Then
                            If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
                    dataHBKF0201.PropStrProcMode = PROCMODE_REF
                    'エラーメッセージ設定
                    puErrMsg = F0201_E002
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
        dataHBKF0201.PropTxtRelUkeNmb.Focus()

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
        cmbTantoGrpCD.DataSourceChanged, _
        cmbProcessState.DataSourceChanged, _
        cmbUsrSyutiKbn.DataSourceChanged, _
        cmbTujyoKinkyuKbn.DataSourceChanged

        'コンボボックスサイズ変更メイン処理
        If logicHBKF0201.ComboBoxResizeMain(sender) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub


    ''' <summary>
    ''' リソース依頼受領システム：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リソース依頼受領システムに空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Irai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Irai.Click

        vwIrai.Sheets(0).AddRows(vwIrai.Sheets(0).Rows.Count, 1)

    End Sub

    ''' <summary>
    ''' リソース依頼受領システム：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リソース依頼受領システム行を1行削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Irai_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Irai.Click

        'リソース依頼受領システム選択行削除処理
        If logicHBKF0201.RemoveRowIraiMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' 基本情報：リリース依頼受領システム編集開始時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>階層コンボボックスの非表示設定を行う。
    ''' <para>作成情報：2012/09/03 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwIrai_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles vwIrai.EditModeOn

        'アクティブセルのセル型を判断します
        Dim row As Integer = vwIrai.ActiveSheet.ActiveRowIndex
        Dim col As Integer = vwIrai.ActiveSheet.ActiveColumnIndex
        If row >= 0 And col = logicHBKF0201.COL_IRAI Then
            ' ドロップダウンリストのオブジェクトを取得します
            Dim cmbSpread As FarPoint.Win.Spread.FpSpread = _
                CType(CType(vwIrai.EditingControl, FarPoint.Win.Spread.CellType.GeneralEditor).SubEditor, FarPoint.Win.Spread.FpSpread)

            ' ドロップダウンリストの 1列目を非表示にします
            cmbSpread.ActiveSheet.Columns(0).Visible = False
            '[mod] 2012/09/07 y.ikushima 表示対応 START
            cmbSpread.ActiveSheet.Columns(1).Visible = False
            '[mod] 2012/09/07 y.ikushima 表示対応 END
        End If
    End Sub

    ''' <summary>
    ''' リソース実施対象システム：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リソース実施対象システム行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Jissi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Jissi.Click

        vwJissi.Sheets(0).AddRows(vwJissi.Sheets(0).Rows.Count, 1)

    End Sub

    ''' <summary>
    ''' リソース実施対象システム：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>リソース実施対象システム行を1行削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Jissi_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Jissi.Click

        'リソース実施対象システム選択行削除処理
        If logicHBKF0201.RemoveRowJissiMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' 基本情報：リリース実施対象システム編集開始時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>階層コンボボックスの非表示設定を行う。
    ''' <para>作成情報：2012/09/03 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwJissi_EditModeOn(ByVal sender As Object, ByVal e As System.EventArgs) Handles vwJissi.EditModeOn

        'アクティブセルのセル型を判断します
        Dim row As Integer = vwJissi.ActiveSheet.ActiveRowIndex
        Dim col As Integer = vwJissi.ActiveSheet.ActiveColumnIndex
        If row >= 0 And col = logicHBKF0201.COL_JISSI Then
            ' ドロップダウンリストのオブジェクトを取得します
            Dim cmbSpread As FarPoint.Win.Spread.FpSpread = _
                CType(CType(vwJissi.EditingControl, FarPoint.Win.Spread.CellType.GeneralEditor).SubEditor, FarPoint.Win.Spread.FpSpread)

            ' ドロップダウンリストの 1列目を非表示にします
            cmbSpread.ActiveSheet.Columns(0).Visible = False
            '[mod] 2012/09/07 y.ikushima 表示対応 START
            cmbSpread.ActiveSheet.Columns(1).Visible = False
            '[mod] 2012/09/07 y.ikushima 表示対応 END
        End If
    End Sub

    ''' <summary>
    ''' リリース予定日時（予定）[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>日時設定画面を表示する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRelSceDT_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelSceDT_HM.Click

        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKF0201.PropDtpRelSceDT.txtDate.Text
            .PropStrTime = dataHBKF0201.PropTxtRelSceDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKF0201.PropDtpRelSceDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKF0201.PropTxtRelSceDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' 基本情報：担当IDの入力後Enter時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>IDをキーに氏名を取得し設定する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtRelTantoID_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtRelTantoID.PreviewKeyDown
        If e.KeyValue = Keys.Enter Then

            '検索用パラメータ設定
            If logicHBKF0201.GetTantoDataMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
            dataHBKF0201.PropTxtRelTantoNM.Text = ""
            dataHBKF0201.PropCmbTantoGrpCD.SelectedValue = ""
            If dataHBKF0201.PropDtResultSub IsNot Nothing Then
                If dataHBKF0201.PropDtResultSub.Rows.Count > 0 Then
                    dataHBKF0201.PropTxtRelTantoNM.Text = dataHBKF0201.PropDtResultSub.Rows(0).Item("hbkusrnm")
                End If
                If dataHBKF0201.PropDtResultSub.Rows.Count = 1 Then
                    dataHBKF0201.PropCmbTantoGrpCD.SelectedValue = dataHBKF0201.PropDtResultSub.Rows(0).Item("groupcd")
                End If
            End If

        End If
    End Sub

    ''' <summary>
    ''' 基本情報：担当者検索ボタンクリック時
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザー検索一覧画面へ遷移する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        '「ひびきユーザー検索一覧」インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_SINGLE                           'モード：単一選択
            .PropArgs = dataHBKF0201.PropTxtRelTantoNM.Text             '検索条件：担当氏名
            .PropSplitMode = SPLIT_MODE_AND                          '検索条件区切り
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKF0201.PropDtResultSub = HBKZ0101.ShowDialog()

        'グループ名、ID、NMを更新
        If dataHBKF0201.PropDtResultSub IsNot Nothing Then
            dataHBKF0201.PropCmbTantoGrpCD.SelectedValue = dataHBKF0201.PropDtResultSub.Rows(0).Item("グループID")
            dataHBKF0201.PropTxtRelTantoID.Text = dataHBKF0201.PropDtResultSub.Rows(0).Item("ユーザーID")
            dataHBKF0201.PropTxtRelTantoNM.Text = dataHBKF0201.PropDtResultSub.Rows(0).Item("ユーザー氏名")
        End If


    End Sub

    ''' <summary>
    ''' [私]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>担当グループ、ID、氏名にログイン者の値を代入する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMy_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMy.Click

        '「私」ボタンクリック時
        'パラメータセット
        With dataHBKF0201
            .PropCmbTantoGrpCD.SelectedValue = PropWorkGroupCD
            .PropTxtRelTantoID.Text = PropUserId
            .PropTxtRelTantoNM.Text = PropUserName
        End With

    End Sub

    ''' <summary>
    ''' リリース着手日時[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>日時設定画面を表示する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRelStDT_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelStDT_HM.Click

        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKF0201.PropDtpRelStDT.txtDate.Text
            .PropStrTime = dataHBKF0201.PropTxtRelStDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKF0201.PropDtpRelStDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKF0201.PropTxtRelStDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' リリース終了日時[時]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>日時設定画面を表示する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRelEdDT_HM_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelEdDT_HM.Click

        Dim HBKZ0801 As New HBKZ0801

        'パラメータセット
        With HBKZ0801.dataHBKZ0801
            .PropStrDate = dataHBKF0201.PropDtpRelEdDT.txtDate.Text
            .PropStrTime = dataHBKF0201.PropTxtRelEdDT_HM.PropTxtTime.Text
        End With

        '画面開く
        If HBKZ0801.ShowDialog Then
            dataHBKF0201.PropDtpRelEdDT.txtDate.Text = HBKZ0801.dataHBKZ0801.PropDtpSetDate.txtDate.Text
            dataHBKF0201.PropTxtRelEdDT_HM.PropTxtTime.Text = HBKZ0801.dataHBKZ0801.PropTxtSetTime.Text
        End If

    End Sub

    ''' <summary>
    ''' 関連ファイル情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル情報行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelationFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_RelationFile.Click

        Dim HBKZ1101 As New HBKZ1101
        'パラメータセット
        With HBKZ1101.dataHBKZ1101

        End With


        '関連ファイル検索画面を表示し、戻り値としてデータテーブルを取得
        If HBKZ1101.ShowDialog() Then

            dataHBKF0201.PropStrFileNaiyo = HBKZ1101.dataHBKZ1101.PropTxtFileNaiyo.Text
            dataHBKF0201.PropStrFilePath = HBKZ1101.dataHBKZ1101.PropTxtFilePath.Text

            '関係ファイル一覧に取得データをセット
            If logicHBKF0201.AddRowFileinfoMain(dataHBKF0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        End If

    End Sub

    ''' <summary>
    ''' 関連ファイル情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関連ファイル情報行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_RelationFile_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_RelationFile.Click

        '関係ファイル一覧選択行削除処理
        If logicHBKF0201.RemoveRowFileInfoMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' [開]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルを読み取り専用で開く
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRelationFileOpen_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelationFileOpen.Click

        'Dim strSpdFileNM As String = ""             'ファイル名


        'With dataHBKF0201.PropVwRelationFileInfo.Sheets(0)
        '    If .RowCount > 0 Then
        '        strSpdFileNM = .GetText(.ActiveRowIndex, logicHBKF0201.COL_RELFILE_PATH)
        '        'システムファイル管理パスと結合
        '        strSpdFileNM = System.IO.Path.Combine(PropFileStorageRootPath, PropFileManagePath, strSpdFileNM)
        '    End If
        'End With

        ''ファイル存在していたら開く
        'If System.IO.File.Exists(strSpdFileNM) Then
        '    System.Diagnostics.Process.Start(strSpdFileNM)
        'End If

        If dataHBKF0201.PropStrProcMode = PROCMODE_EDIT OrElse _
            dataHBKF0201.PropStrProcMode = PROCMODE_REF Then        '編集モード  、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwRelationFileInfo.Visible = True) AndAlso (Me.vwRelationFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwRelationFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(F0201_E012, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(F0201_E012, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKF0201.PropIntSelectedRow = Me.vwRelationFileInfo.ActiveSheet.ActiveRowIndex

            '            'ファイルオープン処理
            '            If logicHBKF0201.FileOpenMain(dataHBKF0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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

            If (Me.vwRelationFileInfo.Visible = True) AndAlso (Me.vwRelationFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwRelationFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwRelationFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwRelationFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwRelationFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = F0201_E012
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKF0201.PropIntSelectedRow = Me.vwRelationFileInfo.ActiveSheet.ActiveRowIndex

                'ファイルオープン処理
                If logicHBKF0201.FileOpenMain(dataHBKF0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' [ダ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録済みのファイルをローカルのデスクトップにダウンロードする。
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRelationFileDownLoad_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRelationFileDownLoad.Click

        If dataHBKF0201.PropStrProcMode = PROCMODE_EDIT OrElse _
             dataHBKF0201.PropStrProcMode = PROCMODE_REF Then        '編集モード  、参照モード

            '[Del] 2012/10/30 s.yamaguchi START
            'If (Me.vwRelationFileInfo.Visible = True) AndAlso (Me.vwRelationFileInfo.Sheets(0).Rows.Count <> 0) Then

            '    Dim cr() As FarPoint.Win.Spread.Model.CellRange
            '    cr = Me.vwRelationFileInfo.ActiveSheet.GetSelections()

            '    ' 未選択の場合エラーメッセージを表示する
            '    If cr.Length = 0 Then
            '        'エラーメッセージ表示
            '        MsgBox(F0201_E012, MsgBoxStyle.Critical, TITLE_ERROR)
            '        Return
            '    End If

            '    'フォーカスが移動したときの行数を取得する（列ヘッダ＆全選択が選択されている場合はエラー）
            '    For i As Integer = 0 To cr.Length - 1

            '        '行数が１以外のときはエラー
            '        If (cr(i).RowCount() <> 1) Then
            '            'エラーメッセージ表示
            '            MsgBox(F0201_E012, MsgBoxStyle.Critical, TITLE_ERROR)
            '            Return
            '        ElseIf (cr(i).RowCount() = 1) Then

            '            '選択行番号をデータクラスにセット
            '            dataHBKF0201.PropIntSelectedRow = Me.vwRelationFileInfo.ActiveSheet.ActiveRowIndex

            '            '編集モード画面処理
            '            If logicHBKF0201.FileDownLoadMain(dataHBKF0201) = False Then
            '                'システムエラー発生時はトランザクション系コントロールを非活性にする
            '                If puErrMsg.StartsWith(HBK_E001) Then
            '                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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

            If (Me.vwRelationFileInfo.Visible = True) AndAlso (Me.vwRelationFileInfo.Sheets(0).Rows.Count <> 0) Then

                '[Add] 2012/10/30 s.yamaguchi START
                '変数宣言
                Dim intSelectedRowFrom As Integer                   '選択開始行番号
                Dim intSelectedRowTo As Integer                     '選択終了行番号

                '選択開始行、終了行取得
                intSelectedRowFrom = Me.vwRelationFileInfo.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = Me.vwRelationFileInfo.Sheets(0).Models.Selection.LeadRow

                '行選択を明示的に行う。
                With Me.vwRelationFileInfo
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With

                '検索結果の選択数が一件以外の時はエラーメッセージ出力
                If Me.vwRelationFileInfo.Sheets(0).SelectionCount <> 1 _
                   Or intSelectedRowTo - intSelectedRowFrom <> 0 Then
                    puErrMsg = F0201_E012
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
                '[Add] 2012/10/30 s.yamaguchi END

                '選択行番号をデータクラスにセット
                dataHBKF0201.PropIntSelectedRow = Me.vwRelationFileInfo.ActiveSheet.ActiveRowIndex

                '編集モード画面処理
                If logicHBKF0201.FileDownLoadMain(dataHBKF0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' 会議情報：会議情報スプレッドセルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議記録登録画面へ遷移する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMeeting_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMeeting.CellDoubleClick
        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKF0201.COL_MEETING_RESULTKBN).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwMeeting.Sheets(0).Cells(e.Row, logicHBKF0201.COL_MEETING_NMB).Value     '選択行の管理番号

        '会議記録登録
        Dim HBKC0401 As New HBKC0401

        '会議記録登録画面データクラスに対しプロパティ設定
        With HBKC0401.dataHBKC0401
            .PropBlnTranFlg = 0                             'メニュー遷移フラグ（0:メニュー以外から遷移、1:メニューから遷移）
            .PropProcessKbn = PROCESS_TYPE_RELEASE           'プロセス区分
            .PropProcessNmb = dataHBKF0201.PropIntRelNmb    'プロセス番号
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
    ''' 会議情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議情報行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Meeting.Click

        Dim HBKC0301 As New HBKC0301
        'パラメータセット
        With HBKC0301.dataHBKC0301
            .PropBlnTranFlg = SELECT_MODE_NOTMENU                               '前画面パラメータ：メニュー遷移フラグ
            .PropProcessKbn = PROCESS_TYPE_RELEASE                              'プロセス区分
            .PropProcessNmb = dataHBKF0201.PropIntRelNmb                        'プロセス番号
            .PropTitle = dataHBKF0201.PropTxtTitle.Text                         'タイトル
        End With

        'クローズ処理の追加
        AddHandler CType(HBKC0301, Form).FormClosed, AddressOf Meeting_FormClosed

        '会議情報検索画面を表示
        If HBKC0301.ShowDialog = DIALOG_RETURN_OK Then

            '検索結果を取得
            dataHBKF0201.PropDtResultSub = HBKC0301.dataHBKC0301.PropDtReturnSub

            '会議情報一覧に取得データをセット
            If logicHBKF0201.AddRowMeetingMain(dataHBKF0201) = False Then
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
        If logicHBKF0201.RefreshMeetingMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' 会議情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>会議情報行を1行削除する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Meeting_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Meeting.Click

        '会議情報一覧選択行削除処理
        If logicHBKF0201.RemoveRowMeetingMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' 対応関係者情報：[＋グループ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/08/31 s.tsurutra
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
        dataHBKF0201.PropDtResultSub = HBKZ0301.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKF0201.SetGroupToVwRelationMain(dataHBKF0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 対応関係者情報：[＋ユーザー]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザー検索画面を表示し、選択されたグループ・ユーザーを当画面にセットする
    ''' <para>作成情報：2012/08/31 s.tsurutra
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_RelaU_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_relaU.Click

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
        dataHBKF0201.PropDtResultSub = HBKZ0101.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKF0201.SetUserToVwRelationMain(dataHBKF0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 対応関係者情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/31 s.tsurutra
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Rela_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_rela.Click

        '関係者情報一覧選択行削除処理
        If logicHBKF0201.RemoveRowRelationMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' <remarks>プロセスリンク情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/31 s.tsurutra
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Plink.Click

        Dim HBKZ0401 As New HBKZ0401
        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI                               'モード：単一選択
            .PropArgs = String.Empty                                    '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND                             '検索条件区切り
            .PropStrFromProcessKbn = PROCESS_TYPE_RELEASE               'プロセス区分
            .PropStrFromProcessNmb = dataHBKF0201.PropIntRelNmb         'プロセス番号
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKF0201.PropDtResultSub = HBKZ0401.ShowDialog()

        'プロセスリンク一覧に取得データをセット
        If logicHBKF0201.AddRowpLinkMain(dataHBKF0201) = False Then
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
    ''' <para>作成情報：2012/08/31 s.tsurutra
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_plink_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Plink.Click

        'プロセスリンク情報一覧選択行削除処理
        If logicHBKF0201.RemoveRowpLinkMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、登録内容を保持して変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor
        dataHBKF0201.PropLblkanryoMsg.Text = ""
        Application.DoEvents()

        If dataHBKF0201.PropStrProcMode = PROCMODE_NEW Then                     '新規登録モード

            '入力チェック処理      
            If logicHBKF0201.CheckInputValueMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKF0201.PropStrProcMode = PROCMODE_EDIT Then                '編集モード

            'ロック解除チェック(dataHBKF0201.PropBlnBeLockedFlg)
            If logicHBKF0201.CheckBeUnlockedMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
            If dataHBKF0201.PropBlnBeLockedFlg = False Then                     '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKF0201.CheckInputValueMain(dataHBKF0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKF0201.PropBlnBeLockedFlg = True Then                  '参照モード

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKF0201.OutputUnlockLogMain(dataHBKF0201) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKF0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '編集モードで画面再描画
                dataHBKF0201.PropStrProcMode = PROCMODE_EDIT
                HBKF0201_Load(Me, New EventArgs)
                Exit Sub

            End If

        End If

        If dataHBKF0201.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKF0201.RegistDataOnNewModeMain(dataHBKF0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default

                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)


                'ログ出力を行う
                If logicHBKF0201.OutputUnlockLogMain(dataHBKF0201) = True Then
                    'メッセージ表示
                    MsgBox(dataHBKF0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_RELEASE
            PropLastProcessNmb = dataHBKF0201.PropIntRelNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(D0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKF0201.PropLblkanryoMsg.Text = F0201_I001

            '編集モードで画面再描画
            dataHBKF0201.PropStrProcMode = PROCMODE_EDIT
            '呼び出し元画面を検索一覧にする
            dataHBKF0201.PropIntOwner = SCR_CALLMOTO_ICHIRAN
            HBKF0201_Load(Me, New EventArgs)

        ElseIf dataHBKF0201.PropStrProcMode = PROCMODE_EDIT Then            '編集モード

            '更新処理を行う
            If logicHBKF0201.RegistDataOnEditModeMain(dataHBKF0201) = False Then

                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)

                'ログ出力を行う
                If logicHBKF0201.OutputUnlockLogMain(dataHBKF0201) = True Then
                    'メッセージ表示
                    MsgBox(dataHBKF0201.PropStrBeUnlockedMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                End If

                '処理終了
                Exit Sub
            End If

            '最終作業データ格納
            PropLastProcessKbn = PROCESS_TYPE_RELEASE
            PropLastProcessNmb = dataHBKF0201.PropIntRelNmb

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            '登録完了メッセージ表示
            'MsgBox(D0201_I001, MsgBoxStyle.Information, TITLE_INFO)
            dataHBKF0201.PropLblkanryoMsg.Text = F0201_I001

            '編集モードで画面再描画
            HBKF0201_Load(Me, New EventArgs)

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
    ''' <remarks>リリース登録（メール作成）画面へ遷移する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMail_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMail.Click

        'メールテンプレート選択画面
        Dim HBKZ1001 As New HBKZ1001
        '変更登録（メール作成）処理クラスインスタンス化
        Dim logicHBKF0301 As New LogicHBKF0301
        Dim dataHBKF0301 As New DataHBKF0301

        'パラメータ設定
        With HBKZ1001.dataHBKZ1001
            .PropStrGroupCD = PropWorkGroupCD                    '前画面パラメータ：グループCD
            .PropStrGroupNM = PropWorkGroupName                  '前画面パラメータ：グループ名
            .PropStrProcessKbn = PROCESS_TYPE_RELEASE            '前画面パラメータ：プロセス区分：リリース
            .PropStrKigenCondCIKbnCD = ""                        '前画面パラメータ：期限切れ条件CI種別
            .PropStrKigenCondTypeKbn = ""                        '前画面パラメータ：期限切れ条件タイプ
            .PropStrKigenCondKigen = ""                          '前画面パラメータ：期限切れ条件期限
            .PropStrKigenCondKbn = ""                            '前画面パラメータ：期限切れ条件ユーザID
        End With

        'メールフォーマット選択
        HBKZ1001.ShowDialog()

        'メールフォーマットデータ取得
        dataHBKF0301.PropDtReturnData = HBKZ1001.dataHBKZ1001.PropDtReturnData

        '当画面表示
        Me.Show()

        '戻り値のDataTableがNullだった場合、処理を中断
        If dataHBKF0301.PropDtReturnData Is Nothing Then
            Exit Sub
        End If

        '入力項目をデータクラスにセット
        With dataHBKF0301

            .PropStrRelNmb = dataHBKF0201.PropTxtRelNmb.Text                                'リリース管理番号
            .PropStrRelUkeNmb = dataHBKF0201.PropTxtRelUkeNmb.Text                          'リリース受付番号
            .PropStrProcessState = dataHBKF0201.PropCmbProcessState.Text                    'ステータス
            .PropStrTitle = dataHBKF0201.PropTxtTitle.Text                                  'タイトル
            .PropStrGaiyo = dataHBKF0201.PropTxtGaiyo.Text                                  '概要
            .PropStrIraiDT = dataHBKF0201.PropDtpIraiDT.txtDate.Text                        '依頼日（起票日）
            .PropStrTujyoKinkyuKbn = dataHBKF0201.PropCmbTujyoKinkyuKbn.Text                '通常・緊急
            .PropStrUsrSyutiKbn = dataHBKF0201.PropCmbUsrSyutiKbn.Text                      'ユーザー周知必要有無
            .PropStrRelSceDT = dataHBKF0201.PropDtpRelSceDT.txtDate.Text                    'リリース予定日時（目安）：日付
            .PropStrRelSceDT_HM = dataHBKF0201.PropTxtRelSceDT_HM.PropTxtTime.Text          'リリース予定日時（目安）：時分

            .PropVwIrai = dataHBKF0201.PropVwIrai                                           'リリース依頼受領システムスプレッド
            .PropVwJissi = dataHBKF0201.PropVwJissi                                         'リリース実施対象システムスプレッド

            .PropStrTantoGrpCD = dataHBKF0201.PropCmbTantoGrpCD.Text                        'リリース担当業務グループ
            .PropStrRelTantoID = dataHBKF0201.PropTxtRelTantoID.Text                        'リリース担当者ID
            .PropStrRelTantoNM = dataHBKF0201.PropTxtRelTantoNM.Text                        'リリース担当者名
            .PropStrRelStDT = dataHBKF0201.PropDtpRelStDT.txtDate.Text                      'リリース着手日時：日付
            .PropStrRelStDT_HM = dataHBKF0201.PropTxtRelStDT_HM.PropTxtTime.Text            'リリース着手日時：時分
            .PropStrRelEdDT = dataHBKF0201.PropDtpRelEdDT.txtDate.Text                      'リリース終了日時：日付
            .PropStrRelEdDT_HM = dataHBKF0201.PropTxtRelEdDT_HM.PropTxtTime.Text            'リリース終了日時：時分

            If dataHBKF0201.PropDtReleaseInfo IsNot Nothing AndAlso dataHBKF0201.PropDtReleaseInfo.Rows.Count > 0 Then
                .PropStrRegGrpNM = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RegGrpNM")      '登録者グループ名
                .PropStrRegNM = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RegID")            '登録者ユーザ名
                .PropStrRegDT = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("RegDT")            '登録日時
                .PropStrUpdateGrpNM = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("UpGrpNM")    '最終更新グループ名
                .PropStrUpdateNM = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("UpdateID")      '最終更新者
                .PropStrUpdateDT = dataHBKF0201.PropDtReleaseInfo.Rows(0).Item("UpdateDT")      '最終更新日時
            Else
                .PropStrRegGrpNM = ""                                                           '登録者グループ名
                .PropStrRegNM = ""                                                              '登録者ユーザ名
                .PropStrRegDT = ""                                                              '登録日時
                .PropStrUpdateGrpNM = ""                                                        '最終更新グループ名
                .PropStrUpdateNM = ""                                                           '最終更新者
                .PropStrUpdateDT = ""                                                           '最終更新日時
            End If

            .PropVwRelationFileInfo = dataHBKF0201.PropVwRelationFileInfo                   '関連情報スプレッド(ファイル,ファイル説明)
            .PropVwMeeting = dataHBKF0201.PropVwMeeting                                     '会議情報スプレッド

            .PropStrBIko1 = dataHBKF0201.PropTxtBIko1.Text                                  'テキスト１
            .PropStrBIko2 = dataHBKF0201.PropTxtBIko2.Text                                  'テキスト２
            .PropStrBIko3 = dataHBKF0201.PropTxtBIko3.Text                                  'テキスト３
            .PropStrBIko4 = dataHBKF0201.PropTxtBIko4.Text                                  'テキスト４
            .PropStrBIko5 = dataHBKF0201.PropTxtBIko5.Text                                  'テキスト５
            If dataHBKF0201.PropChkFreeFlg1.Checked = True Then                             'フリーフラグ1
                .PropStrFreeFlg1 = FLG_ON_NM
            Else
                .PropStrFreeFlg1 = FLG_OFF_NM
            End If
            If dataHBKF0201.PropChkFreeFlg2.Checked = True Then                             'フリーフラグ2
                .PropStrFreeFlg2 = FLG_ON_NM
            Else
                .PropStrFreeFlg2 = FLG_OFF_NM
            End If
            If dataHBKF0201.PropChkFreeFlg3.Checked = True Then                             'フリーフラグ3
                .PropStrFreeFlg3 = FLG_ON_NM
            Else
                .PropStrFreeFlg3 = FLG_OFF_NM
            End If
            If dataHBKF0201.PropChkFreeFlg4.Checked = True Then                             'フリーフラグ4
                .PropStrFreeFlg4 = FLG_ON_NM
            Else
                .PropStrFreeFlg4 = FLG_OFF_NM
            End If
            If dataHBKF0201.PropChkFreeFlg5.Checked = True Then                             'フリーフラグ5
                .PropStrFreeFlg5 = FLG_ON_NM
            Else
                .PropStrFreeFlg5 = FLG_OFF_NM
            End If

            .PropVwRelationInfo = dataHBKF0201.PropVwRelationInfo                           '対応関係者情報データ(区分,ID,グループ名,ユーザー名)
            .PropStrGroupRireki = dataHBKF0201.PropTxtGroupRireki.Text                      'グループ履歴
            .PropStrTantoRireki = dataHBKF0201.PropTxtTantoRireki.Text                      '担当者履歴
            .PropVwProcessLinkInfo = dataHBKF0201.PropVwProcessLinkInfo                     'プロセスリンク管理番号(区分,番号)

        End With

        'メール作成処理呼び出し
        If logicHBKF0301.CreateIncidentMailMain(dataHBKF0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/08/31 s.tsuruta
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
    ''' <remarks>関係者＋編集モードの場合はロック解除を行う
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKF0201_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing


        ''別画面が開いている場合、クローズ処理を行わない
        'If dataHBKF0201.PropfrmInstance IsNot Nothing Then
        '    'クローズ処理キャンセル
        '    e.Cancel = True
        '    'エラーメッセージ設定
        '    puErrMsg = E0201_E001
        '    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
        '    Exit Sub
        'End If

        '編集モードの場合はロック解除を行う
        If dataHBKF0201.PropStrProcMode = PROCMODE_EDIT And _
            (dataHBKF0201.PropBlnBeLockedFlg = False And dataHBKF0201.PropIntChkKankei = KANKEI_CHECK_EDIT) Then

            '画面クローズ時ロック解除処理
            If logicHBKF0201.UnlockWhenCloseMain(dataHBKF0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKF0201.UnlockWhenClickBtnUnlockMain(dataHBKF0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
    ''' プロセスリンク一覧：セルダブルクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択した管理番号の登録画面へ参照モードで遷移する
    ''' <para>作成情報：2012/08/31 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwProcessLinkInfo_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwProcessLinkInfo.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKF0201.COL_PLINK_KBN).Value  '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwProcessLinkInfo.Sheets(0).Cells(e.Row, logicHBKF0201.COL_PLINK_NO).Value   '選択行の管理番号

        '関係者チェック_結果格納用
        Dim intchkkankei As Integer = 0

        'プロセスリンク対応関係者チェック処理(dataHBKF0201.PropintChkKankei) 
        If logicHBKF0201.PlinkKankeiCheckMain(intchkkankei, strSelectNo, strSelectKbn) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKF0201.PropAryTsxCtlList) = False Then
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
            puErrMsg = F0201_E002
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
                .PropIntINCNmb = strSelectNo        '管理番号
            End With
            '画面遷移
            Me.Hide()
            HBKC0201.ShowDialog()
            Me.Show()

        ElseIf strSelectKbn = PROCESS_TYPE_QUESTION Then

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

        End If

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
        dataHBKF0201.PropLblkanryoMsg.Text = ""

        'タイマーを停止する
        Me.timKanryo.Stop()

    End Sub
    '[add] 2012/09/24 s.tsuruta 完了メッセージ表示修正 END

End Class