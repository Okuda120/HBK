Imports Common
Imports CommonHBK

''' <summary>
''' 特権ユーザーログイン（ひびきユーザー登録）画面Interfaceクラス
''' </summary>
''' <remarks>特権ユーザーログイン（ひびきユーザー登録）画面の設定を行う
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKX0101

    'インスタンス作成
    Public dataHBKX0101 As New DataHBKX0101         'データクラス
    Private logicHBKX0101 As New LogicHBKX0101      'ロジッククラス
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
    Private Sub HBKX0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'イベント処理制御初期処理
        blnLoadFlg = True

        'フォーム背景色設定
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(CommonHBK.CommonDeclareHBK.PropConfigrationFlag)

        With dataHBKX0101
            .PropRdoGruopUsr = Me.rdoGroupUsr
            .PropRdoGruopMaster = Me.rdoGroupMaster
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

        '選択されたラジオボタン毎にログイン処理を行っていく
        If dataHBKX0101.PropRdoGruopUsr.Checked = True Then

            'ログイン処理
            If logicHBKX0101.LoginMain(dataHBKX0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

        ElseIf dataHBKX0101.PropRdoGruopMaster.Checked = True Then

            'グループマスター登録ユーザーが選択された場合、入力チェックとユーザーID・パスワードを確認する。
            '入力チェック
            If logicHBKX0101.CheckInputForm(dataHBKX0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            'ログイン処理
            If logicHBKX0101.LoginMain(dataHBKX0101) = False Then
                '特権ログインログ出力（ログイン失敗）
                dataHBKX0101.PropStrLogInOutKbn = SUPER_LOGINNG
                If logicHBKX0101.OutputLogLogin(dataHBKX0101) = False Then
                    'エラーメッセージ表示
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Return
                End If
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '特権ログインログ出力（ログイン成功）
            dataHBKX0101.PropStrLogInOutKbn = SUPER_LOGINOK
            If logicHBKX0101.OutputLogLogin(dataHBKX0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

        End If

        Me.Hide()

        'ひびきユーザー登録画面に遷移する
        Dim HBKX0201 As New HBKX0201

        'ひびきユーザー登録画面データクラスに対しプロパティ設定
        If dataHBKX0101.PropRdoGruopMaster.Checked = True Then
            HBKX0201.dataHBKX0201.PropStrUsrAdmin = USR_SUPER_USER
            HBKX0201.dataHBKX0201.PropStrSuperUsrID = dataHBKX0101.PropTxtUserId.Text
        Else
            HBKX0201.dataHBKX0201.PropStrUsrAdmin = USR_GROUP_ADMIN
            HBKX0201.dataHBKX0201.PropStrGroupCD = PropWorkGroupCD
        End If

        HBKX0201.ShowDialog()

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
    ''' [グループユーザー管理者]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ユーザーID、パスワードの非活性の設定を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoGroupUsr_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGroupUsr.CheckedChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            'グループユーザー管理者選択時
            If logicHBKX0101.rdoAbleMain(dataHBKX0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                '処理終了
                Exit Sub
            End If
        End If
    End Sub

    ''' <summary>
    ''' [グループマスター登録ユーザー]ラジオボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>ユーザーID、パスワードの活性の設定を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub rdoGroupMaster_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rdoGroupMaster.CheckedChanged
        'Load処理時かチェックする
        If blnLoadFlg = False Then
            'グループマスター登録ユーザー選択時
            If logicHBKX0101.rdoAbleMain(dataHBKX0101) = False Then
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