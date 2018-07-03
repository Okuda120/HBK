Imports Common
Imports CommonHBK
Imports HBKZ

Public Class HBKB1102

    'インスタンス作成
    Private commonLogic As New CommonLogic          'CommonLogic
    Private commonLogicHBK As New CommonLogicHBK    'CommonLogicHBK
    Public dataHBKB1102 As New DataHBKB1102         'Dataクラス呼び出し(一括更新_一括セットアップ)
    Private logicHBKB1102 As New LogicHBKB1102      'ロジッククラス呼び出し(一括更新_一括セットアップ)

    ''' <summary>
    ''' フォームロード時処理
    ''' </summary>
    ''' <remarks>フォームが呼び出された際に呼ばれる処理
    ''' <para>作成情報：2012/07/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB1102_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'データクラスの初期設定を行う
        'コントロール
        With dataHBKB1102
            'コントロール
            .PropGrpLoginUser = Me.grpLoginUser         'ログインコントロール
            .PropVwIkkatsu = Me.vwIkkatsu               '一括スプレッド
            .PropBtntouroku = Me.btnTouroku             '登録ボタン
            .PropBtnBack = Me.btnBack                   '戻るボタン
        End With

        'システムエラー事前対応処理
        If logicHBKB1102.DoProcForErrorMain(dataHBKB1102) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '画面初期表示処理()
        If logicHBKB1102.InitFormMain(dataHBKB1102) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1102.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>登録ボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/07/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnTouroku_Click(sender As System.Object, e As System.EventArgs) Handles btnTouroku.Click

        '変数宣言
        Dim frmHBKZ1201 As New HBKZ1201                 '登録処理中メッセージフォーム

        '入力チェック
        If logicHBKB1102.CheckInputValueMain(dataHBKB1102) = False Then
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1102.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録データ保存処理
        If logicHBKB1102.RegisterInputValueSaveMain(dataHBKB1102) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1102.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'この画面を隠す
        Me.Hide()
        '変更理由登録画面のインスタンス化
        Dim frmHBKB0301 As New HBKB0301

        'プロパティセット
        With frmHBKB0301.dataHBKB0301
            .PropStrRegMode = REG_MODE_PACKAGE
        End With

        '変更理由登録へ遷移する
        If frmHBKB0301.ShowDialog() = DIALOG_RETURN_CANCEL Then
            'キャンセルが押された場合、この画面を表示する
            Me.Show()
            Exit Sub
        End If

        'プロパティセット
        With frmHBKB0301.dataHBKB0301
            dataHBKB1102.PropStrRegReason = .PropStrRegReason
            dataHBKB1102.PropDtCauseLink = .PropDtCauseLink
        End With

        ''この画面を表示する
        'Me.Show()

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '登録処理中メッセージフォームの表示
        frmHBKZ1201.Show()
        'メッセージフォームの再描画
        frmHBKZ1201.Refresh()

        '登録処理
        If logicHBKB1102.UpdateRegDataMain(dataHBKB1102) = False Then

            '登録処理中メッセージフォームを閉じる
            frmHBKZ1201.Close()

            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKB1102.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録処理中メッセージフォームを閉じる
        frmHBKZ1201.Close()

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(String.Format(B1102_I001, dataHBKB1102.PropIntRowCount.ToString()), MsgBoxStyle.Information, TITLE_INFO)

        '画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' 戻るボタン押下時処理
    ''' </summary>
    ''' <remarks>戻るボタンを押下した際に行われる処理
    ''' <para>作成情報：2012/07/13 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(sender As System.Object, e As System.EventArgs) Handles btnBack.Click
        'この画面を閉じる
        Me.Close()
    End Sub

End Class