Imports Common
Imports CommonHBK

''' <summary>
''' 特権ユーザーログイン（エンドユーザ検索）画面Interfaceクラス
''' </summary>
''' <remarks>特権ユーザーログイン（エンドユーザ検索）画面の設定を行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0103

    'インスタンス作成
    Public dataHBKX0103 As New DataHBKX0103         'データクラス
    Private logicHBKX0103 As New LogicHBKX0103      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0103_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'フォーム背景色設定
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)

        With dataHBKX0103
            .PropRdoEndUsrImp = Me.rdoEndUsrImp
            .PropTxtUserId = Me.txtUserId
            .PropTxtPassword = Me.txtPassword
            .PropStrProgramID = Me.GetType.Name
        End With

        'ユーザーIDにフォーカスセット
        Me.txtUserId.Select()

    End Sub

    ''' <summary>
    ''' [ログイン]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        '入力チェック
        If logicHBKX0103.CheckInputForm(dataHBKX0103) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        'ログイン処理
        If logicHBKX0103.Login(dataHBKX0103) = False Then
            '特権ログインログ出力（ログイン失敗）
            dataHBKX0103.PropStrLogInOutKbn = SUPER_LOGINNG
            If logicHBKX0103.OutputLogLogin(dataHBKX0103) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '特権ログインログ出力（ログイン成功）
        dataHBKX0103.PropStrLogInOutKbn = SUPER_LOGINOK
        If logicHBKX0103.OutputLogLogin(dataHBKX0103) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        Me.Hide()

        'エンドユーザーマスター取込画面に遷移する
        Dim HBKX0501 As New HBKX0501
        HBKX0501.dataHBKX0501.PropStrSuperUsrID = dataHBKX0103.PropTxtUserId.Text
        HBKX0501.ShowDialog()

        'フォームを閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じる
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click
        'フォームを閉じる
        Me.Close()
    End Sub

    ''' <summary>
    ''' [Password]テキストボックスEnter押下時処理
    ''' </summary>
    ''' <remarks>[Password]テキストボックスにてEnterが押された時の処理
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtPassword_PreviewKeyDown(ByVal sender As System.Object, ByVal e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPassword.PreviewKeyDown
        '入力キー判定
        If e.KeyValue = Keys.Enter Then
            btnLogin_Click(sender, e)
        End If
    End Sub

End Class