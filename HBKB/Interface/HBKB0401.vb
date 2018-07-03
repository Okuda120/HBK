Imports Common
Imports CommonHBK
Imports HBKZ
Imports FarPoint.Win.Spread

''' <summary>
''' システム登録画面Interfaceクラス
''' </summary>
''' <remarks>システム登録画面の設定を行う
''' <para>作成情報：2012/06/13 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0401

    'インスタンス作成
    Public dataHBKB0401 As New DataHBKB0401
    Private logicHBKB0401 As New LogicHBKB0401
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoRollBack As Boolean    'ロールバック実行フラグ

    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>フラグの制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/06/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
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
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0401_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKB0401
            .PropGrpLoginUser = Me.grpLoginUser                     'ログイン：ログイン情報グループボックス
            .PropGrpCIKhn = Me.grpCIKhn                             'ヘッダ：CI基本情報グループボックス
            .PropLblCINmb = Me.lblCINmb                             'ヘッダ：CI番号ラベル
            .PropLblCIKbnNM = Me.lblCIKbnNM                         'ヘッダ：CI種別名ラベル
            .PropLblTitleRirekiNo = Me.lblTitleRirekiNo             'ヘッダ：履歴番号タイトルラベル
            .PropLblValueRirekiNo = Me.lblValueRirekiNo             'ヘッダ：履歴番号値ラベル
            .PropTbInput = Me.tbInput                               'タブ
            .PropCmbKind = Me.cmbKind                               '基本情報：種別コンボボックス
            .PropTxtCINmb = Me.txtCINmb                             '基本情報：CI番号テキストボックス
            .PropTxtClass1 = Me.txtClass1                           '基本情報：分類１テキストボックス
            .PropTxtClass2 = Me.txtClass2                           '基本情報：分類２テキストボックス
            .PropTxtCINM = Me.txtCINM                               '基本情報：CI種別名称テキストボックス
            .PropCmbCIStatus = Me.cmbCIStatus                       '基本情報：ステータスコンボボックス
            .PropTxtInfShareteamNM = Me.txtInfShareteamNM           '基本情報：情報共有先テキストボックス
            .PropTxtCINaiyo = Me.txtCINaiyo                         '基本情報：説明テキストボックス
            .PropVwKnowHowUrl = Me.vwKnowHowUrl                     '基本情報：ノウハウURLスプレッド
            .PropBtnAddRow_Url = Me.btnAddRow_Url                   '基本情報：ノウハウURL行削除ボタン
            .PropBtnRemoveRow_Url = Me.btnRemoveRow_Url             '基本情報：ノウハウURL行追加ボタン
            .PropVwSrvMng = Me.vwSrvMng                             '基本情報：サーバー管理情報スプレッド
            .PropBtnAddRow_Srv = Me.btnAddRow_Srv                   '基本情報：サーバー管理情報行削除ボタン
            .PropBtnRemoveRow_Srv = Me.btnRemoveRow_Srv             '基本情報：サーバー管理情報行追加ボタン
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
            .PropTxtCIOwnerNM = Me.txtCIOwnerNM                     '関係情報：CIオーナー名テキストボックス
            .PropLblCIOwnerCD = Me.lblCIOwerCD                      '関係情報：CIオーナーコードラベル
            .PropBtnSearchGrp = Me.btnSearchGrp                     '関係情報：検索ボタン
            .PropVwRelation = Me.vwRelation                         '関係情報：関係者情報スプレッド
            .PropBtnAddRow_Grp = Me.btnAddRow_Grp                   '関係情報：グループ行追加ボタン
            .PropBtnAddRow_Usr = Me.btnAddRow_Usr                   '関係情報：ユーザー行追加ボタン
            .PropBtnRemoveRow_Relation = Me.btnRemoveRow_Relation   '関係情報：関係者情報行削除ボタン
            .PropLblRirekiNo = Me.lblRirekiNo                       'フッタ：履歴番号（更新ID）ラベル
            .PropTxtRegReason = Me.txtRegReason                     'フッタ：理由テキストボックス
            .PropVwMngNmb = Me.vwMngNmb                             'フッタ：原因リンク管理番号スプレッド
            .PropVwRegReason = Me.vwRegReason                       'フッタ：履歴情報スプレッド
            .PropBtnReg = Me.btnReg                                 'フッタ：登録ボタン
            .PropBtnRollBack = Me.btnRollback                       'フッタ：ロールバックボタン
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKB0401.DoProcForErrorMain(dataHBKB0401) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '処理モードに応じて画面初期表示を行う
        If dataHBKB0401.PropStrProcMode = PROCMODE_NEW Then             '新規モード

            '新規モード画面初期表示メイン処理
            If logicHBKB0401.InitFormNewModeMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKB0401.PropStrProcMode = PROCMODE_EDIT Then        '編集／参照（ロック）モード  

            'ロック設定メイン処理
            If logicHBKB0401.LockMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

            If dataHBKB0401.PropBlnBeLockedFlg = False Then           '編集モード

                '編集モード画面初期表示メイン処理
                If logicHBKB0401.InitFormEditModeMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
                            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                            Exit Sub
                        End If
                    End If
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    '処理終了
                    Exit Sub
                End If

            ElseIf dataHBKB0401.PropBlnBeLockedFlg = True Then       '参照（ロック）モード

                '参照モード画面初期表示メイン処理
                If logicHBKB0401.InitFormRefModeMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0401.PropStrBeLockedMsg, MsgBoxStyle.Information + MsgBoxStyle.OkOnly, TITLE_INFO)

            End If

        ElseIf dataHBKB0401.PropStrProcMode = PROCMODE_RIREKI Then      '履歴モード

            '画面名設定
            Me.Text = B0401_NAME_RIREKI

            '履歴モード画面初期表示メイン処理
            If logicHBKB0401.InitFormRirekiModeMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/06/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnUnlock_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles grpLoginUser.btnUnlockClick

        'ロック解除処理を行う
        If logicHBKB0401.UnlockWhenClickBtnUnlockMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' ノウハウURL：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ノウハウURL一覧に空行を1行追加する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Url_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Url.Click

        'ノウハウURL一覧空行追加処理
        If logicHBKB0401.AddRowKnowHowUrlMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' ノウハウURL：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ノウハウURL一覧の選択行を削除する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Url_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Url.Click

        'ノウハウURL一覧選択行削除処理
        If logicHBKB0401.RemoveRowKnowHowUrlMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' サーバー管理情報：[＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>サーバー管理情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Srv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Srv.Click

        'サーバー管理情報一覧空行追加処理
        If logicHBKB0401.AddRowKnowMngSrvMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' サーバー管理情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>サーバー管理情報一覧の選択行を削除する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Srv_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Srv.Click

        'サーバー管理情報一覧選択行削除処理
        If logicHBKB0401.RemoveRowMngSrvMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearchGrp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchGrp.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_SINGLE            'モード：単一選択
            .PropArgs = Me.txtCIOwnerNM.Text          '検索条件：CIオーナー名
            .PropSplitMode = SPLIT_MODE_AND           '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0401.PropDtResultSub = HBKZ0301.ShowDialog()

        'CIオーナー名、コードを更新
        If dataHBKB0401.PropDtResultSub IsNot Nothing Then
            Me.txtCIOwnerNM.Text = dataHBKB0401.PropDtResultSub.Rows(0).Item("グループ名")
            Me.lblCIOwerCD.Text = dataHBKB0401.PropDtResultSub.Rows(0).Item("グループCD")
        End If

    End Sub

    ''' <summary>
    ''' 関係者情報：[＋グループ]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>グループ検索画面を表示し、選択されたグループを当画面にセットする
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Grp_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Grp.Click

        'グループ検索画面インスタンス作成
        Dim HBKZ0301 As New HBKZ0301

        'パラメータセット
        With HBKZ0301.dataHBKZ0301
            .PropMode = SELECT_MODE_MULTI       'モード：複数選択
            .PropArgs = String.Empty            '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND     '検索条件区切り：AND
        End With

        'グループ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0401.PropDtResultSub = HBKZ0301.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKB0401.SetGroupToVwRelationMain(dataHBKB0401) = False Then
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
    ''' <remarks>ひびきユーザー検索画面を表示し、選択されたグループ・ユーザーを当画面にセットする
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Usr_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow_Usr.Click

        'ひびきユーザー検索画面インスタンス作成
        Dim HBKZ0101 As New HBKZ0101

        'パラメータセット
        With HBKZ0101.dataHBKZ0101
            .PropMode = SELECT_MODE_MULTI           'モード：複数選択
            .PropArgs = String.Empty                '検索条件：なし
            .PropSplitMode = SPLIT_MODE_AND         '検索条件区切り：AND
        End With

        'ひびきユーザー検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0401.PropDtResultSub = HBKZ0101.ShowDialog()

        '関係者情報一覧に取得データをセット
        If logicHBKB0401.SetUserToVwRelationMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' 関係者情報：[－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Relation_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow_Relation.Click

        '関係者情報一覧選択行削除処理
        If logicHBKB0401.RemoveRowRelationMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwMngNmb_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwMngNmb.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0401.PropStrProcMode = PROCMODE_RIREKI Then
            Exit Sub
        End If

        '変数宣言
        Dim strSelectKbn As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB0401.COL_CAUSELINK_KBN).Value    '選択行の区分（コード）
        Dim strSelectNo As String = _
            Me.vwMngNmb.Sheets(0).Cells(e.Row, logicHBKB0401.COL_CAUSELINK_NO).Value     '選択行の管理番号

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
    ''' <remarks>選択した行のシステム履歴画面へ遷移する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwRegReason_CellDoubleClick(ByVal sender As System.Object, ByVal e As FarPoint.Win.Spread.CellClickEventArgs) Handles vwRegReason.CellDoubleClick

        '列ヘッダーがクリックされた場合は処理しない
        'また、履歴モード時も処理しない
        '１行目が選択されても処理を行わない
        If e.ColumnHeader = True Or e.RowHeader = True Or dataHBKB0401.PropStrProcMode = PROCMODE_RIREKI Or e.Row = 0 Then
            Exit Sub
        End If

        '変数宣言
        Dim HBKB0401_R As HBKB0401 = Nothing                                                                'システム登録（履歴）画面
        Dim intSelectRirekiNo As Integer = _
            Integer.Parse(Me.vwRegReason.Sheets(0).Cells(e.Row, logicHBKB0401.COL_REGREASON_UPID).Value)    '選択行の履歴番号

        'システム登録（履歴）画面のインスタンスを作成
        HBKB0401_R = New HBKB0401

        'システム登録（履歴）画面のデータクラスにパラメータをセット
        With HBKB0401_R.dataHBKB0401
            .PropStrProcMode = PROCMODE_RIREKI                      '処理モード：履歴
            .PropIntCINmb = dataHBKB0401.PropIntCINmb               'CI番号
            .PropIntRirekiNo = intSelectRirekiNo                    '履歴番号
            .PropIntFromRegSystemFlg = 1                            'システム登録画面遷移フラグON
            .PropBlnBeLockedFlg = dataHBKB0401.PropBlnBeLockedFlg   'ロックフラグ
            .PropStrEdiTime = Me.grpLoginUser.PropLockDate          '編集開始日時
        End With

        'システム登録（履歴）画面へ遷移し、戻り値としてロールバック実行フラグを取得
        Me.Hide()
        Dim blnDoRollBack As Boolean = HBKB0401_R.ShowDialog()

        'ロールバック実行フラグがONの場合、編集モードで画面再描画
        If blnDoRollBack = True Then
            '編集モードで画面再描画
            dataHBKB0401.PropStrProcMode = PROCMODE_EDIT
            HBKB0401_Load(Me, New EventArgs)
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
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '処理モードに応じた入力チェックを行う
        If dataHBKB0401.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '入力チェック処理
            If logicHBKB0401.CheckInputValueMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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

        ElseIf dataHBKB0401.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロック）モード

            'ロック解除チェック
            If logicHBKB0401.CheckBeUnlockedMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
            If dataHBKB0401.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、入力チェックを行う
                If logicHBKB0401.CheckInputValueMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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

            ElseIf dataHBKB0401.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0401.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0401.SetFormRefModeFromEditModeMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0401.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
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
            dataHBKB0401.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB0401.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor


        '処理モードに応じた登録処理を行う
        If dataHBKB0401.PropStrProcMode = PROCMODE_NEW Then                 '新規登録モード

            '新規登録処理
            If logicHBKB0401.RegistDataOnNewModeMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
            dataHBKB0401.PropStrProcMode = PROCMODE_EDIT
            HBKB0401_Load(Me, New EventArgs)

        ElseIf dataHBKB0401.PropStrProcMode = PROCMODE_EDIT Then            '編集／参照（ロックモード）

            'ロック解除チェック
            If logicHBKB0401.CheckBeUnlockedMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
            If dataHBKB0401.PropBlnBeLockedFlg = False Then             '編集モード

                'ロック解除されていない時、更新処理を行う
                If logicHBKB0401.RegistDataOnEditModeMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
                dataHBKB0401.PropStrProcMode = PROCMODE_EDIT
                HBKB0401_Load(Me, New EventArgs)

            ElseIf dataHBKB0401.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

                'フラグをON
                dataHBKB0401.PropBlnLockCompare = True

                'ロック解除時、ログ出力処理と画面の再描画を行う
                If logicHBKB0401.SetFormRefModeFromEditModeMain(dataHBKB0401) = False Then
                    'システムエラー発生時はトランザクション系コントロールを非活性にする
                    If puErrMsg.StartsWith(HBK_E001) Then
                        If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
                MsgBox(dataHBKB0401.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
                Exit Sub

            End If

        End If


        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(B0401_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' [ロールバック]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>変更理由登録画面へ遷移する
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRollback_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRollback.Click

        '変数宣言
        Dim HBKB0301 As HBKB0301 = Nothing  '変更理由登録画面

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'ロック解除チェック
        If logicHBKB0401.CheckBeUnlockedMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
        If dataHBKB0401.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

            'ロック解除時、ログ出力処理と画面の再描画を行う
            If logicHBKB0401.SetFormRirekiModeBeUnlockedMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
            MsgBox(dataHBKB0401.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
            Exit Sub

        End If

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

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
            dataHBKB0401.PropStrRegReason = .PropStrRegReason   '理由格納用
            dataHBKB0401.PropDtCauseLink = .PropDtCauseLink     '原因リンクデータ格納用
        End With
        Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'ロック解除チェック
        If logicHBKB0401.CheckBeUnlockedMain(dataHBKB0401) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
        If dataHBKB0401.PropBlnBeLockedFlg = False Then             '編集モード

            'ロールバック処理
            If logicHBKB0401.RollBackDataMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
            End If

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default

            'ロールバック完了メッセージ表示
            MsgBox(B0401_I002, MsgBoxStyle.Information, TITLE_INFO)

            'ロールバック実行フラグON
            blnDoRollBack = True

            '当画面を閉じる
            Me.Close()

        ElseIf dataHBKB0401.PropBlnBeLockedFlg = True Then         '参照（ロック）モード

            'ロック解除時、ログ出力処理と画面の再描画を行う
            If logicHBKB0401.SetFormRirekiModeBeUnlockedMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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
            MsgBox(dataHBKB0401.PropStrBeUnlockedMsg, MsgBoxStyle.Exclamation, TITLE_WARNING)
            Exit Sub

        End If

    End Sub

    ''' <summary>
    ''' [戻る]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ遷移する
    ''' <para>作成情報：2012/06/13 t.fukuo
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
    ''' <para>作成情報：2012/06/13 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0401_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        '編集モードの場合はロック解除を行う
        If dataHBKB0401.PropStrProcMode = PROCMODE_EDIT And _
            dataHBKB0401.PropBlnBeLockedFlg = False Then

            '画面クローズ時ロック解除処理
            If logicHBKB0401.UnlockWhenCloseMain(dataHBKB0401) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0401.PropAryTsxCtlList) = False Then
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

End Class