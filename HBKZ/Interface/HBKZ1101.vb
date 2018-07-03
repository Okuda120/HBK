Imports Common
Imports CommonHBK
Imports System.IO

''' <summary>
''' 関連ファイル設定画面Interfaceクラス
''' </summary>
''' <remarks>関連ファイル設定画面の設定を行う
''' <para>作成情報：2012/07/09 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ1101

    'インスタンス作成
    Public dataHBKZ1101 As New DataHBKZ1101
    Private logiHBKZ1101 As New LogicHBKZ1101
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoSetFile As Boolean    '設定フラグ

    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>フラグの制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Boolean

        'ファイル設定フラグ初期化
        blnDoSetFile = False

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        'ファイル設定フラグを返す
        Return blnDoSetFile

    End Function

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKZ1101_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラス初期設定
        With dataHBKZ1101
            .PropTxtFilePath = Me.txtFilePath       '格納ファイルパステキストボックス
            .PropTxtFileNaiyo = Me.txtFileNaiyo     '説明テキストボックス
            .PropBtnFileDialog = Me.btnFileDialog   '参照ボタン
        End With

        'フォーム背景色設定
        Me.BackColor = CommonLogicHBK.SetFormBackColor(PropConfigrationFlag)

    End Sub

    ''' <summary>
    ''' [参照]ボタン押下時処理
    ''' </summary>
    ''' <remarks>ファイル選択ダイアログを表示し、ファイルパスを取得する
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnFileDialog_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnFileDialog.Click

        Dim ofdTorikomiFile As New OpenFileDialog                             'ファイル選択ダイアログ
        Dim strInitFile As String = ""                                        '初期表示ファイル名
        Dim strFileType As String = "すべてのファイル(*.*)|*.*"               '選択可能なファイル形式
        Dim intSelFileType As Integer = 1                                     '選択されているファイル形式（インデックス）

        '初期表示ファイル名設定
        ofdTorikomiFile.FileName = strInitFile

        '初期表示ディレクトリ設定
        ofdTorikomiFile.InitialDirectory = ""

        '選択ファイル形式設定
        ofdTorikomiFile.Filter = strFileType

        '選択されているファイル形式を設定
        ofdTorikomiFile.FilterIndex = intSelFileType

        'ダイアログを閉じる前に現在のディレクトリを復元
        ofdTorikomiFile.RestoreDirectory = True

        'ダイアログを表示
        If ofdTorikomiFile.ShowDialog() = Windows.Forms.DialogResult.OK Then

            'データをセット
            txtFilePath.Text = ofdTorikomiFile.FileName

        End If

    End Sub

    ''' <summary>
    ''' [クリア]ボタン押下時処理
    ''' </summary>
    ''' <remarks>画面入力値をクリアする
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click

        '格納ファイルパスをクリアする
        If logiHBKZ1101.ClearFormMain(dataHBKZ1101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [設定]ボタン押下時処理
    ''' </summary>
    ''' <remarks>入力チェックを行い、問題がなければ入力値を呼び出し元に返す
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click

        '入力チェックを行う
        If logiHBKZ1101.CheckInputValueMain(dataHBKZ1101) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '設定フラグをONにする
        blnDoSetFile = True

        '戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [戻る]ボタン押下時処理
    ''' </summary>
    ''' <remarks>当画面を閉じて呼び出し元画面へ戻る
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnBack_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnBack.Click

        '当画面を閉じる
        Me.Close()

    End Sub

End Class