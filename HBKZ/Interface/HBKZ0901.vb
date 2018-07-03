Imports Common
Imports CommonHBK

''' <summary>
''' 出力形式選択画面Interfaceクラス
''' </summary>
''' <remarks>出力形式選択画面の設定を行う
''' <para>作成情報：2012/07/09 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ0901


    '変数宣言
    Private intOutputKbn As Integer    '出力区分


    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>出力区分の制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Integer

        '出力区分初期化
        intOutputKbn = OUTPUT_RETURN_CANCEL

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        '出力区分を返す
        Return intOutputKbn

    End Function

    ''' <summary>
    ''' [出力]ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>出力形式を親画面に返し、当画面を閉じる
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnOutput_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnOutput.Click

        '選択された出力形式に応じて出力区分をセット
        If Me.rdoPrinter.Checked Then

            'プリンター出力
            intOutputKbn = OUTPUT_RETURN_PRINTER

        ElseIf Me.rdoFile.Checked Then

            'ファイル出力
            intOutputKbn = OUTPUT_RETURN_FILE

        ElseIf Me.rdoPrinterAndFile.Checked Then

            'プリンター＆ファイル出力
            intOutputKbn = OUTPUT_RETURN_PRINTER_FILE

        End If

        ' 戻り値をOKにする
        Me.DialogResult = Windows.Forms.DialogResult.OK

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [キャンセル]ボタン押下時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>当画面を閉じて呼び出し元画面へ戻る
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnCancel_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnCancel.Click

        '当画面を閉じる
        Me.Close()

    End Sub

End Class