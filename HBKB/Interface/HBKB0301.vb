Imports Common
Imports CommonHBK
Imports HBKZ

Public Class HBKB0301

    Public dataHBKB0301 As New DataHBKB0301
    Private logicHBKB0301 As New LogicHBKB0301
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub HBKB0301_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '背景色を変更
        MyBase.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'データクラスにオブジェクトをセット
        With dataHBKB0301
            .PropTxtRegReason = Me.txtRegReason
            .PropVwCauseLink = Me.vwCauseLink
            .PropBtntouroku = Me.btnReg
            .PropBtnLastManageNmb = Me.btnLastManageNmb
        End With

        'システムエラー事前対応処理
        If logicHBKB0301.DoProcForErrorMain(dataHBKB0301) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面初期表示処理
        If logicHBKB0301.InitFormMain(dataHBKB0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0301.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' ＋ボタン押下時処理
    ''' </summary>
    ''' <remarks>＋ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnAddRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAddRow.Click

        'プロセス検索一覧画面インスタンス化
        Dim HBKZ0401 As New HBKZ0401
        'パラメータセット
        With HBKZ0401.dataHBKZ0401
            .PropMode = SELECT_MODE_MULTI   'モード：複数選択
            .PropArgs = ""                                  '検索条件：なし
        End With

        '画面を表示する
        dataHBKB0301.PropDtResultSub = HBKZ0401.ShowDialog()

        '原因リンクに取得データをセット
        If logicHBKB0301.SetProcessToVwCauseLinkMain(dataHBKB0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0301.PropAryTsxCtlList) = False Then
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
    ''' －ボタン押下時処理
    ''' </summary>
    ''' <remarks>－ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnRemoveRow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemoveRow.Click

        '原因リンク選択行削除処理
        If logicHBKB0301.RemoveRowCauseLinkMain(dataHBKB0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0301.PropAryTsxCtlList) = False Then
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
    ''' 最終管理番号ボタン押下時処理
    ''' </summary>
    ''' <remarks>最終管理番号ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnLastManageNmb_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLastManageNmb.Click

        '最終管理番号を取得し原因リンクにセット
        If logicHBKB0301.SetLastManageNmMain(dataHBKB0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0301.PropAryTsxCtlList) = False Then
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
    ''' 決定ボタン押下時処理
    ''' </summary>
    ''' <remarks>決定ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '入力チェック
        If logicHBKB0301.CheckInputPicMain(dataHBKB0301) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB0301.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Return
        End If

        ' 戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK

        '画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' キャンセルボタン押下時処理
    ''' </summary>
    ''' <remarks>キャンセルボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        'この画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' ダイアログオーバーロード押下時処理
    ''' </summary>
    ''' <remarks>ダイアログが閉じられた時の処理
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Integer
        MyBase.ShowDialog()
        ' 戻り値チェック
        If Me.DialogResult = Windows.Forms.DialogResult.Cancel Then
            Return DIALOG_RETURN_CANCEL
        End If
        Return DIALOG_RETURN_OK
    End Function

End Class