Imports Common
Imports CommonHBK

''' <summary>
''' 特権ユーザーログイン（エンドユーザ検索）画面Interfaceクラス
''' </summary>
''' <remarks>特権ユーザーログイン（エンドユーザ検索）画面の設定を行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0102

    'インスタンス作成
    Public dataHBKX0102 As New DataHBKX0102         'データクラス
    Private logicHBKX0102 As New LogicHBKX0102      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    '変数宣言
    Private blnLoadFlg As Boolean = True            'Load時フラグ

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKX0102_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'イベント処理制御初期処理
        blnLoadFlg = True

        'フォーム背景色設定
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)

        With dataHBKX0102
            .PropRdoReading = Me.rdoReading
            .PropRdoEndUsrMod = Me.rdoEndUsrMod
            .PropTxtUserId = Me.txtUserId
            .PropTxtPassword = Me.txtPassword
            .PropStrProgramID = Me.GetType.Name
        End With

        'イベント処理制御後処理
        blnLoadFlg = False

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

        'エンドユーザーマスター編集ユーザーが選択された場合、入力チェックとユーザーID・パスワードを確認する。
        If dataHBKX0102.PropRdoEndUsrMod.Checked = True Then

            '入力チェック
            If logicHBKX0102.CheckInputForm(dataHBKX0102) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            'ログイン処理
            If logicHBKX0102.Login(dataHBKX0102) = False Then
                '特権ログインログ出力（ログイン失敗）
                dataHBKX0102.PropStrLogInOutKbn = SUPER_LOGINNG
                If logicHBKX0102.OutputLogLogin(dataHBKX0102) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Return
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '特権ログインログ出力（ログイン成功）
            dataHBKX0102.PropStrLogInOutKbn = SUPER_LOGINOK
            If logicHBKX0102.OutputLogLogin(dataHBKX0102) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If
        End If

        Me.Hide()

        'エンドユーザーマスター検索一覧画面に遷移する
        Dim HBKX0301 As New HBKX0301

        'エンドユーザーマスター検索一覧画面データクラスに対しプロパティ設定
        If dataHBKX0102.PropRdoEndUsrMod.Checked = True Then
            HBKX0301.dataHBKX0301.PropStrLoginMode = LOGIN_MODE_END_USR_REG
            HBKX0301.dataHBKX0301.PropStrSuperUsrID = dataHBKX0102.PropTxtUserId.Text
        Else
            HBKX0301.dataHBKX0301.PropStrLoginMode = LOGIN_MODE_END_USR_ETURAN
        End If

        HBKX0301.ShowDialog()

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
    ''' [閲覧のみ]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ユーザーID、パスワードの非活性の設定を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoReading_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoReading.CheckedChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            '閲覧のみ選択時
            If logicHBKX0102.rdoAbleMain(dataHBKX0102) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' [エンドユーザーマスター編集ユーザー]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ユーザーID、パスワードの活性の設定を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoEndUsrMod_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoEndUsrMod.CheckedChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            'エンドユーザーマスター編集ユーザー選択時
            If logicHBKX0102.rdoAbleMain(dataHBKX0102) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
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