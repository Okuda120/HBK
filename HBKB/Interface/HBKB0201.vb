Imports Common
Imports CommonHBK
Imports System.IO
Imports HBKZ

Public Class HBKB0201

    Public dataHBKB0201 As New DataHBKB0201
    Private logicHBKB0201 As New LogicHBKB0201
    Private CommonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' フォーム読み込み時処理
    ''' </summary>
    ''' <remarks>フォームを読み込んだ際に行われる処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKB0201_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        '背景色を変更
        MyBase.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)
        '[Add] 2012/08/02 y.ikushima START
        ''データをクリア
        'lblCIKbnNM.Text = ""
        'txtFilePath.Text = ""
        '[Add] 2012/08/02 y.ikushima END

        'データをセット
        lblCIKbnNM.Text = dataHBKB0201.PropStrCIKbnNm

        'プロパティセット   
        With dataHBKB0201
            .PropBtnReg = Me.btnReg
            .PropLblCIKbnNM = Me.lblCIKbnNM
            .PropTxtFilePath = Me.txtFilePath
        End With

        'システムエラー事前対応処理
        If logicHBKB0201.DoProcForErrorMain(dataHBKB0201) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' 参照ボタン押下時処理
    ''' </summary>
    ''' <remarks>ファイル選択ダイアログを表示、ファイルパスを取得する。
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnFileDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDialog.Click

        Dim ofdTorikomiFile As New OpenFileDialog                             'ファイル選択ダイアログ
        Dim strInitFile As String = ""                                        '初期表示ファイル名
        Dim strInitPath As String = ""                                        '初期表示ディレクトリ
        Dim strFileType As String = "すべてのファイル(*.*)|*.*"                '選択可能なファイル形式
        Dim intSelFileType As Integer = 1                                     '選択されているファイル形式（インデックス）

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
            txtFilePath.Text = ofdTorikomiFile.FileName
        End If

    End Sub

    ''' <summary>
    ''' 登録ボタン押下時処理
    ''' </summary>
    ''' <remarks>登録ボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Private Sub btnReg_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReg.Click

        '変数宣言
        Dim frmHBKZ1201 As New HBKZ1201                 '登録処理中メッセージフォーム

        Dim LogicHBKB0202 As LogicHBKB0202 = Nothing    'システム
        Dim LogicHBKB0203 As LogicHBKB0203 = Nothing    '文書
        Dim LogicHBKB0204 As LogicHBKB0204 = Nothing    '部所有機器

        Dim dataHBKB0202 As DataHBKB0202 = Nothing  'システム   
        Dim dataHBKB0203 As DataHBKB0203 = Nothing  '文書
        Dim dataHBKB0204 As DataHBKB0204 = Nothing  '部所有機器

        Dim strCIKbnCd = dataHBKB0201.PropStrCIKbnCd        'CI種別コード

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '画面入力チェック
        If logicHBKB0201.InputCheckMain(dataHBKB0201) = False Then
            'マウスポインタ変更(砂時計→通常)
            Me.Cursor = Windows.Forms.Cursors.Default
            'システムエラー発生時はトランザクション系コントロールを非活性にする
            If puErrMsg.StartsWith(HBK_E001) Then
                If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                    MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                    Exit Sub
                End If
            End If
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            Exit Sub
        End If

        'ファイルチェック（CI種別毎）START===================================
        If strCIKbnCd = CI_TYPE_SYSTEM Then
            'システム
            LogicHBKB0202 = New LogicHBKB0202
            dataHBKB0202 = New DataHBKB0202
            'データのセット
            dataHBKB0202.PropStrFilePath = txtFilePath.Text
            'ファイルの入力チェック
            If LogicHBKB0202.FileInputCheckMain(dataHBKB0202) = False Then
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

        ElseIf strCIKbnCd = CI_TYPE_DOC Then
            '文章
            LogicHBKB0203 = New LogicHBKB0203
            dataHBKB0203 = New DataHBKB0203
            'データのセット
            dataHBKB0203.PropStrFilePath = txtFilePath.Text
            'ファイルの入力チェック
            If LogicHBKB0203.FileInputCheckMain(dataHBKB0203) = False Then
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

        ElseIf strCIKbnCd = CI_TYPE_KIKI Then
            '部所有機器
            LogicHBKB0204 = New LogicHBKB0204
            dataHBKB0204 = New DataHBKB0204
            'データのセット
            dataHBKB0204.PropStrFilePath = txtFilePath.Text
            'ファイル入力チェック
            If LogicHBKB0204.FileInputCheckMain(dataHBKB0204) = False Then
                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If
        End If
        'ファイルチェック（CI種別毎）END===================================

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '次の画面に遷移
        Me.Hide()

        '変更理由設定処理START===================================
        '変更理由登録画面のインスタンス化
        Dim HBKB0301 As New HBKB0301
        'プロパティセット
        With HBKB0301.dataHBKB0301
            .PropStrRegMode = REG_MODE_BLANK
        End With
        '変更理由登録へ遷移する
        If HBKB0301.ShowDialog() = DIALOG_RETURN_CANCEL Then
            'キャンセルが押された場合、この画面を表示する
            Me.Show()
            Exit Sub
        End If
        '変更理由登録入力データをセット
        With HBKB0301.dataHBKB0301
            dataHBKB0201.PropStrRegReason = .PropStrRegReason
            dataHBKB0201.PropDtCauseLink = .PropDtCauseLink
        End With
        '変更理由設定処理END===================================

        'マウスポインタ変更(通常→砂時計)
        Me.Cursor = Windows.Forms.Cursors.WaitCursor

        '登録処理中メッセージフォームの表示
        frmHBKZ1201.Show()
        'メッセージフォームの再描画
        frmHBKZ1201.Refresh()

        '一括登録（CI種別毎）START===================================
        If strCIKbnCd = CI_TYPE_SYSTEM Then
            'システム
            'データのセット
            dataHBKB0202.PropStrRegReason = dataHBKB0201.PropStrRegReason
            dataHBKB0202.PropDtCauseLink = dataHBKB0201.PropDtCauseLink
            '登録処理
            If LogicHBKB0202.RegMain(dataHBKB0202) = False Then

                '登録処理中メッセージフォームを閉じる
                frmHBKZ1201.Close()

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

        ElseIf strCIKbnCd = CI_TYPE_DOC Then
            '文書
            'データのセット
            dataHBKB0203.PropStrRegReason = dataHBKB0201.PropStrRegReason
            dataHBKB0203.PropDtCauseLink = dataHBKB0201.PropDtCauseLink
            '登録処理
            If LogicHBKB0203.RegMain(dataHBKB0203) = False Then

                '登録処理中メッセージフォームを閉じる
                frmHBKZ1201.Close()

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If

        ElseIf strCIKbnCd = CI_TYPE_KIKI Then
            '部所有機器
            'データのセット
            dataHBKB0204.PropStrRegReason = dataHBKB0201.PropStrRegReason
            dataHBKB0204.PropDtCauseLink = dataHBKB0201.PropDtCauseLink
            '登録処理
            If LogicHBKB0204.RegMain(dataHBKB0204) = False Then

                '登録処理中メッセージフォームを閉じる
                frmHBKZ1201.Close()

                'マウスポインタ変更(砂時計→通常)
                Me.Cursor = Windows.Forms.Cursors.Default
                'システムエラー発生時はトランザクション系コントロールを非活性にする
                If puErrMsg.StartsWith(HBK_E001) Then
                    If CommonLogicHBK.SetCtlUnabled(dataHBKB0201.PropAryTsxCtlList) = False Then
                        MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                        Exit Sub
                    End If
                End If
                MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
                Exit Sub
            End If
        End If
        '一括登録（CI種別毎）END===================================

        '登録処理中メッセージフォームを閉じる
        frmHBKZ1201.Close()

        'マウスポインタ変更(砂時計→通常)
        Me.Cursor = Windows.Forms.Cursors.Default

        '登録完了メッセージ表示
        MsgBox(B0201_I001, MsgBoxStyle.Information, TITLE_INFO)

    End Sub

    ''' <summary>
    ''' キャンセルボタン押下時処理
    ''' </summary>
    ''' <remarks>キャンセルボタンを押下した時の処理
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '画面を閉じる
        Me.Close()

    End Sub
End Class