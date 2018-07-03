Imports Common
Imports CommonHBK
Imports HBKZ
''' <summary>
''' 特権ユーザパスワード変更画面Interfaceクラス
''' </summary>
''' <remarks>特権ユーザパスワード変更画面の設定を行う
''' <para>作成情報：2012/08/30 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0110
    'インスタンス生成
    'Dataクラス
    Public dataHBKX0110 As New DataHBKX0110
    'ロジッククラス
    Private logicHBKX0110 As New LogicHBKX0110
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0110_Load(sender As System.Object, e As System.EventArgs) Handles MyBase.Load
        'データクラス初期設定
        With dataHBKX0110
            .PropTxtID = Me.txtID
            .PropTxtPassNow = Me.txtPassNow
            .PropTxtPassNew = Me.txtPassNew
            .PropTxtPassNewRe = Me.txtPassNewRe
            .PropBtnChange = Me.btnChange
            .PropBtnCansel = Me.btnCansel
        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'システムエラー事前対応処理
        If logicHBKX0110.DoProcForErrorMain(dataHBKX0110) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If
    End Sub

    ''' <summary>
    ''' 変更ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>パスワードを変更する
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnChange_Click(sender As System.Object, e As System.EventArgs) Handles btnChange.Click

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '入力チェック
        If logicHBKX0110.CheckInputMain(dataHBKX0110) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0110.PropAryTsxCtlList) = False Then
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

        '登録処理
        If logicHBKX0110.SuperUsrUpdateMain(dataHBKX0110) = False Then
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKX0110.PropAryTsxCtlList) = False Then
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
        MsgBox(X0110_I001, MsgBoxStyle.Information, TITLE_INFO)

        '画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' キャンセルボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面を閉じる
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCansel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCansel.Click
        Me.Close()
    End Sub
End Class