Imports Common
Imports CommonHBK
Imports HBKZ
''' <summary>
''' ひびきユーザーマスター登録画面Interfaceクラス
''' </summary>
''' <remarks>ひびきユーザーマスター登録画面の設定を行う
''' <para>作成情報：2012/08/21 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0201

    'インスタンス生成

    'Dataクラス
    Public dataHBKX0201 As New DataHBKX0201 'ひびきユーザーマスター登録

    'ロジッククラス
    Private logicHBKX0201 As New LogicHBKX0201 'ひびきユーザーマスター登録
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0201_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKX0201
            .PropGrpLoginUser = Me.grpLoginUser                 'ログイン情報グループボックス
            .PropLblGroupSelect = Me.lblGroupSelect             'グループ選択ラベル
            .PropLblCount = Me.lblCount                         '件数ラベル
            .PropBtnAddRow = Me.btnAddRow                       '+ボタン
            .PropBtnRemoveRow = Me.btnRemoveRow                 '-ボタン
            .PropBtnReg = Me.btnReg                             '登録ボタン
            .PropBtnBack = Me.btnBack                           '戻るボタン
            .PropCmbGroupNM = Me.cmbGroupNM                     'グループ選択コンボボックス
            .PropChkJtiFlg = Me.chkJtiFlg                       '削除データも表示チェックボックス
            .PropVwHBKUsrMasterList = Me.vwHBKUsrMasterList     'ひびきユーザーマスター登録一覧スプレッド

            .PropStrProgramID = Me.GetType.Name
        End With


        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKX0201.DoProcForErrorMain(dataHBKX0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'ひびきユーザーマスター登録画面初期表示メイン呼出
        If logicHBKX0201.InitFormMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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
    ''' [＋]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドに1行追加する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow.Click

        'エンドユーザー検索
        'エンドユーザーマスター検索子画面呼出

        'エンドユーザーマスター検索子画面のインスタンス
        Dim frmHBKZ0201 As New HBKZ0201
        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            '単一検索か複数検索か確認する
            .PropMode = SELECT_MODE_MULTI
            .PropArgs = String.Empty
        End With



        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKX0201.PropDtResultSub = frmHBKZ0201.ShowDialog()

        If dataHBKX0201.PropDtResultSub IsNot Nothing Then



            'データ追加メイン呼出
            If logicHBKX0201.addDataMain(dataHBKX0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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
    ''' [－]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>スプレッドの選択行を削除する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow.Click


        '削除チェック処理呼出
        If logicHBKX0201.CheckDeleteDataMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If



        '選択行削除メイン呼出
        If logicHBKX0201.DeleteSelectDataMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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
    ''' 戻るボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>メニュー画面に遷移する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click
        Me.Close()
    End Sub
    ''' <summary>
    ''' 登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>登録及び更新を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click


        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '入力エラーチェックメイン
        If logicHBKX0201.InputCheckMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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

        'ひびきユーザーを登録します。よろしいですか？でNoを選んだ場合
        If MsgBox(String.Format(X0201_W001), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            Exit Sub
        End If

        '登録メイン
        If logicHBKX0201.RegisterMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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

        '登録後再表示を行う
        If dataHBKX0201.PropStrUsrAdmin = USR_GROUP_ADMIN Then
            'ひびきユーザーマスター登録画面初期表示メイン呼出(再表示)
            If logicHBKX0201.InitFormMain(dataHBKX0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If

        ElseIf dataHBKX0201.PropStrUsrAdmin = USR_SUPER_USER Then
            'グループ選択メイン処理呼出(再表示)
            If logicHBKX0201.SelectGroupMain(dataHBKX0201) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default
        '登録完了メッセージ表示
        MsgBox(X0201_I001, MsgBoxStyle.Information, TITLE_INFO)


    End Sub
    ''' <summary>
    ''' チェックボックス変化時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>削除データを含めたデータを表示するか判断する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub chkJtiFlg_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkJtiFlg.CheckedChanged


        '削除データ表示メイン処理呼出
        If logicHBKX0201.CheckMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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
    ''' セレクトボックス項目選択時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択したグループに所属するメンバーのデータを取得、表示する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub cmbGroupNM_SelectionChangeCommitted(sender As System.Object, e As System.EventArgs) Handles cmbGroupNM.SelectionChangeCommitted
 

        'グループ選択メイン処理呼出
        If logicHBKX0201.SelectGroupMain(dataHBKX0201) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0201.PropAryTsxCtlList) = False Then
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
    ''' スプレッド内チェックボックスクリック時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>クリックした行の更新を確定させる
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwHBKUsrMasterList_ButtonClicked(sender As Object, e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwHBKUsrMasterList.ButtonClicked

        '変数宣言
        Dim intClickRow As Integer = e.Row      'クリックされた行
        Dim intClickColumn As Integer = e.Column 'クリックされた列



        'スプレッド内チェックボックス変更時更新確定処理
        If logicHBKX0201.SetModifidMain(dataHBKX0201, intClickRow, intClickColumn) = False Then
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
    ''' <remarks>特権ログイン情報をDBにログとして出力する。
    ''' <para>作成情報：2012/09/11 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0201_FormClosing(ByVal sender As System.Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles MyBase.FormClosing
        'ユーザー権限がスーパーユーザーの場合
        If dataHBKX0201.PropStrUsrAdmin = USR_SUPER_USER Then
            'ログアウトログ出力処理メイン呼出
            If logicHBKX0201.LogoutLogMain(dataHBKX0201) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub
    ''' <summary>
    ''' スプレッド内テキストボックス編集時処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>編集した行の更新を確定させる
    ''' <para>作成情報：2012/09/12 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub vwHBKUsrMasterList_EditChange(sender As Object, e As FarPoint.Win.Spread.EditorNotifyEventArgs) Handles vwHBKUsrMasterList.EditChange
        '変数宣言
        Dim intClickRow As Integer = e.Row      'クリックされた行

        'スプレッド内チェックボックス変更時更新確定処理
        If logicHBKX0201.SetTextChangeMain(dataHBKX0201, intClickRow) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub
End Class