Imports Common
Imports CommonHBK
Imports HBKZ

''' <summary>
''' レンタル及び部所有機器の期限切れ検索一覧Interfaceクラス
''' </summary>
''' <remarks>期限切れ情報の検索を行う
''' <para>作成情報：2012/07/05 kawate
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKB0801

    'インスタンス生成
    Public dataHBKB0801 As New DataHBKB0801
    Private logicHBKB0801 As New LogicHBKB0801
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnRaiseEventCIKbn As Boolean = False     'CI種別コンボボックス変更時イベント実行可否フラグ

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0801_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'プロパティセット
        With dataHBKB0801
            .PropCmbCIKbn = Me.cmbCIKbn
            .PropCmbType = Me.cmbType
            .PropRdoLimit = Me.rdoLimit
            .PropCmbLimit = Me.cmbLimit
            .PropRdoUsrID = Me.rdoUsrID
            .PropTxtUsrID = Me.txtUsrID
            .PropLblCount = Me.lblCount
            .PropVwCIInfo = Me.vwCIInfo
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKB0801.DoProcForErrorMain(dataHBKB0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面初期表示
        If logicHBKB0801.InitFormMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'CI種別コンボボックス変更時イベント実行可否フラグON
        blnRaiseEventCIKbn = True

    End Sub

    ''' <summary>
    ''' CI種別コンボボックス選択値変更時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>選択されたCI種別に応じてフォームコントロールを設定する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub cmbCIKbn_SelectedIndexChanged(ByVal sender As Object, ByVal e As System.EventArgs) Handles cmbCIKbn.SelectedIndexChanged

        '当イベント実行フラグがONの場合のみ処理　※当判定をしないとCI種別コンボボックス作成時に落ちる
        If blnRaiseEventCIKbn = True Then

            '選択されたCI種別に応じてフォームコントロールを設定
            If logicHBKB0801.BeChangedCIKbnMain(dataHBKB0801) = False Then
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
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
    ''' ユーザID検索ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ひびきユーザ検索一覧画面を表示し、選択されたユーザIDを当画面にセットする
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearchUsrID_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearchUsrID.Click

        'エンドユーザ検索一覧画面のインスタンス
        Dim frmHBKZ0201 As New HBKZ0201

        'パラメータセット
        With frmHBKZ0201.dataHBKZ0201
            .PropMode = SELECT_MODE_SINGLE
            .PropSplitMode = SPLIT_MODE_AND
            .PropArgs = String.Empty
        End With

        'エンドユーザ検索画面を表示し、戻り値としてデータテーブルを取得
        dataHBKB0801.PropDtResultSub = frmHBKZ0201.ShowDialog()

        'ユーザIDをセット
        If dataHBKB0801.PropDtResultSub IsNot Nothing Then
            txtUsrID.Text = dataHBKB0801.PropDtResultSub.Rows(0).ItemArray(0)
        End If

    End Sub

    ''' <summary>
    ''' クリアボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を初期表示の状態に戻す
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        '検索条件初期表示
        If logicHBKB0801.InitSearchConditionMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
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
    ''' 検索ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>検索条件を元に、結果を一覧表示する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSearch_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSearch.Click

        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        '検索件数取得
        If logicHBKB0801.GetResultCntMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
                    'アイコンを元に戻す
                    Me.Cursor = Cursors.Default
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default

            If puErrMsg = "" Then
                MsgBox(B0801_I001, MsgBoxStyle.Information, TITLE_INFO)
            Else
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            End If

            '処理終了
            Exit Sub
        End If

        '該当データが閾値を超える場合、確認メッセージを表示
        If dataHBKB0801.PropIntResultCnt > PropSearchMsgCount Then
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default
            'Noボタン押下時は処理終了
            If MsgBox(String.Format(B0801_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                Exit Sub
            End If
        End If

        'アイコンを砂時計に変更
        Me.Cursor = Cursors.WaitCursor

        '検索結果表示
        If logicHBKB0801.SearchDataMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
                    'アイコンを元に戻す
                    Me.Cursor = Cursors.Default
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'アイコンを元に戻す
            Me.Cursor = Cursors.Default
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        'アイコンを元に戻す
        Me.Cursor = Cursors.Default

    End Sub

    ''' <summary>
    ''' 全選択ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>一覧表示されている全ての行のチェックボックスにチェックを入れる
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAllSelect_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllSelect.Click

        '一覧の全データに選択チェックをつける
        If logicHBKB0801.AllSelectMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
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
    ''' 全解除ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>一覧表示されている全ての行のチェックボックスのチェックを外す
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAllCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAllCancel.Click

        '一覧の全データの選択チェックを解除する
        If logicHBKB0801.AllCancelMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
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
    ''' インシデント登録ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>チェックした行をインシデント登録する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '入力チェック
        If logicHBKB0801.CheckINputValueMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '登録確認メッセージ表示
        If MsgBox(B0801_W002, MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
            'Noボタン押下時、処理終了
            Exit Sub
        End If

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        'インシデント登録処理
        If logicHBKB0801.RegIncMain(dataHBKB0801) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0801.PropAryTsxCtlList) = False Then
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

        '登録完了メッセージ表示
        MsgBox(B0801_I002, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' 戻るボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>前画面に戻る
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        'フォームを閉じる
        Me.Close()

    End Sub

End Class