Imports Common
Imports CommonHBK
Imports HBKZ

''' <summary>
''' 一括登録画面Interfaceクラス
''' </summary>
''' <remarks>一括登録画面の設定を行う
''' <para>作成情報：2012/07/24 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKC0601

    'インスタンス作成
    Public dataHBKC0601 As New DataHBKC0601
    Private logicHBKC0601 As New LogicHBKC0601
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKC0601_load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '背景色を変更
        MyBase.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        'データをクリア
        Me.lblProcessKbnNM.Text = PROCESS_TYPE_INCIDENT_NAME
        Me.txtFilePath.Text = ""

        'プロパティセット   
        With dataHBKC0601
            .PropLblProcessKbnNM = Me.lblProcessKbnNM
            .PropTxtFilePath = Me.txtFilePath
            .PropBtnReg = Me.btnReg
        End With

        'システムエラー事前対応処理
        If logicHBKC0601.DoProcForErrorMain(dataHBKC0601) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [登録]ボタンクリック時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>入力チェックを行い、取込ファイルフォーマットに従い登録処理を行う
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '変数宣言
        Dim frmHBKZ1201 As New HBKZ1201                 '登録処理中メッセージ画面

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '登録処理中メッセージフォームの表示
        frmHBKZ1201.Show()
        'メッセージフォームの再描画
        frmHBKZ1201.Refresh()

        '画面入力チェック
        If logicHBKC0601.InputCheckMain(dataHBKC0601) = False Then

            '登録処理中メッセージフォームを閉じる
            frmHBKZ1201.Close()

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'ファイルの入力チェック
        If logicHBKC0601.FileInputCheckMain(dataHBKC0601) = False Then

            '登録処理中メッセージフォームを閉じる
            frmHBKZ1201.Close()

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録処理
        If logicHBKC0601.RegMain(dataHBKC0601) = False Then

            '登録処理中メッセージフォームを閉じる
            frmHBKZ1201.Close()

            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If commonLogicHBK.SetCtlUnabled(dataHBKC0601.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        '登録処理中メッセージフォームを閉じる
        frmHBKZ1201.Close()

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(C0601_I001, MsgBoxStyle.Information, TITLE_INFO)
        Me.Close()

    End Sub

    ''' <summary>
    ''' [参照]ボタン押下時処理
    ''' </summary>
    ''' <remarks>ファイル選択ダイアログを表示し、ファイルパスを取得する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnFileDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDialog.Click

        Dim ofdTorikomiFile As New OpenFileDialog                       'ファイル選択ダイアログ
        Dim strInitFile As String = ""                                  '初期表示ファイル名
        Dim strInitPath As String = ""                                  '初期表示ディレクトリ
        Dim strFileType As String = "すべてのファイル(*.*)|*.*"         '選択可能なファイル形式
        Dim intSelFileType As Integer = 1                               '選択されているファイル形式（インデックス）

        '初期表示ファイル名設定
        ofdTorikomiFile.FileName = strInitFile

        '初期表示ディレクトリ設定
        ofdTorikomiFile.InitialDirectory = strInitPath

        '選択ファイル形式設定
        ofdTorikomiFile.Filter = strFileType

        '選択されているファイル形式を設定
        ofdTorikomiFile.FilterIndex = intSelFileType

        'ダイアログを閉じる前に現在のディレクトリを復元
        ofdTorikomiFile.RestoreDirectory = True

        'ファイル選択ダイアログの名前を設定
        ofdTorikomiFile.Title = "ファイルを開く"

        'ダイアログを表示
        If ofdTorikomiFile.ShowDialog() = Windows.Forms.DialogResult.OK Then
            'データをセット
            Me.txtFilePath.Text = ofdTorikomiFile.FileName
        End If

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下時処理
    ''' </summary>
    ''' <remarks>当画面を閉じて呼び出し元画面へ戻る
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click
        '画面を閉じる
        Me.Close()
    End Sub

End Class