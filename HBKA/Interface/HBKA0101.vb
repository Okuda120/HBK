Imports Common
Imports CommonHBK

''' <summary>
''' ログイン画面Interfaceクラス
''' </summary>
''' <remarks>ログイン画面の設定を行う
''' <para>作成情報：2012/05/28 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKA0101

    'インスタンス作成
    Public dataHBKA0101 As New DataHBKA0101         'データクラス
    Private logicHBKA0101 As New LogicHBKA0101      'ロジッククラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKA0101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        With dataHBKA0101
            .PropTxtUserId = Me.txtUserId
            .PropTxtPassword = Me.txtPassword
        End With

        dataHBKA0101.PropLblVersion = lblVersion

        If commonLogic.InitCommonSetting(Nothing) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Application.Exit()
            Exit Sub
        End If

        'Iniファイル取得
        If logicHBKA0101.GetVersion(dataHBKA0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Application.Exit()
            Exit Sub
        End If

        'システム情報の取得
        If logicHBKA0101.GetSystemData(dataHBKA0101) = False Then
            'エラーメッセージ表示
            'エラー番号42P01の場合はメッセージを変更する
            If 0 <= puErrMsg.IndexOf("ERROR: 42P01") Then
                puErrMsg = A0101_E008
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Application.Exit()
            Exit Sub
        End If

        '稼働状態の確認
        If dataHBKA0101.PropBolSystemFlg = False Then
            '稼働状態ではない
            MsgBox(A0101_E001, MsgBoxStyle.Critical, TITLE_ERROR)
            Application.Exit()
            Exit Sub
        End If

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(dataHBKA0101.PropDtSystemMasta.Rows(0)(1))

    End Sub
    ''' <summary>
    ''' ログインボタン押下時処理
    ''' </summary>
    ''' <remarks>ログインボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnLogin_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnLogin.Click

        '入力チェック
        If logicHBKA0101.CheckInputForm(dataHBKA0101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '保持しているログイン情報の初期化
        logicHBKA0101.ClearLoginData()

        'ログイン処理
        If logicHBKA0101.Login(dataHBKA0101) = False Then
            'エラーメッセージ表示
            'エラー番号42P01の場合はメッセージを変更する
            If 0 <= puErrMsg.IndexOf("ERROR: 42P01") Then
                puErrMsg = A0101_E008
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        'ログインの成否確認
        If dataHBKA0101.PropBolLoginResultFlg = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '画面遷移処理
        Dim nextForm As Object       '次回遷移先画面

        If CommonHBK.CommonDeclareHBK.PropGroupDataList.Count >= 2 Then
            '所属しているグループが２つ以上
            nextForm = New HBKA0201
        Else
            '所属しているグループが１つ
            nextForm = New HBKA0301
        End If

        'ログインログ出力
        If logicHBKA0101.OutputLogLogin() = False Then
            'エラー番号42P01の場合はメッセージを変更する
            If 0 <= puErrMsg.IndexOf("ERROR: 42P01") Then
                puErrMsg = A0101_E008
            End If
            'エラー　ログイン情報を初期化する
            logicHBKA0101.ClearLoginData()
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Return
        End If

        '遷移処理
        nextForm.Show()
        Me.Close()

    End Sub

    ''' <summary>
    ''' [Password]テキストボックスEnter押下時処理
    ''' </summary>
    ''' <remarks>[Password]テキストボックスにてEnterが押された時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub txtPassword_PreviewKeyDown(sender As System.Object, e As System.Windows.Forms.PreviewKeyDownEventArgs) Handles txtPassword.PreviewKeyDown

        '入力キー判定
        If e.KeyValue = Keys.Enter Then

            '入力チェック
            If logicHBKA0101.CheckInputForm(dataHBKA0101) = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '保持しているログイン情報の初期化
            logicHBKA0101.ClearLoginData()

            'ログイン処理
            If logicHBKA0101.Login(dataHBKA0101) = False Then
                'エラーメッセージ表示
                'エラー番号42P01の場合はメッセージを変更する
                If 0 <= puErrMsg.IndexOf("ERROR: 42P01") Then
                    puErrMsg = A0101_E008
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            'ログインの成否確認
            If dataHBKA0101.PropBolLoginResultFlg = False Then
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '画面遷移処理
            Dim nextForm As Object       '次回遷移先画面

            If CommonHBK.CommonDeclareHBK.PropGroupDataList.Count >= 2 Then
                '所属しているグループが２つ以上
                nextForm = New HBKA0201
            Else
                '所属しているグループが１つ
                nextForm = New HBKA0301
            End If

            'ログインログ出力
            If logicHBKA0101.OutputLogLogin() = False Then
                'エラー番号42P01の場合はメッセージを変更する
                If 0 <= puErrMsg.IndexOf("ERROR: 42P01") Then
                    puErrMsg = A0101_E008
                End If
                'エラー　ログイン情報を初期化する
                logicHBKA0101.ClearLoginData()
                'エラーメッセージ表示
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Return
            End If

            '遷移処理
            nextForm.Show()
            Me.Close()

        End If

    End Sub

    ''' <summary>
    ''' 閉じるボタン押下時処理
    ''' </summary>
    ''' <remarks>閉じるボタンを押下した時の処理
    ''' <para>作成情報：2012/05/28 matsuoka
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnEnd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnEnd.Click

        'フォームを閉じる
        Me.Close()

    End Sub

End Class