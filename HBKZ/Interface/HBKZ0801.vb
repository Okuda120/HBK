Imports Common
Imports CommonHBK

''' <summary>
''' 日時設定画面Interfaceクラス
''' </summary>
''' <remarks>日時設定画面の設定を行う
''' <para>作成情報：2012/07/05 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class HBKZ0801

    'インスタンス作成
    Public dataHBKZ0801 As New DataHBKZ0801
    Private logicHBKZ0801 As New LogicHBKZ0801
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '変数宣言
    Private blnDoSetTime As Boolean    '設定フラグ

    ''' <summary>
    ''' 画面表示時の処理
    ''' </summary>
    ''' <remarks>フラグの制御と画面のポップアップ表示を行う
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Overloads Function ShowDialog() As Boolean

        '時刻設定フラグ初期化
        blnDoSetTime = False

        '当画面をポップアップ表示
        MyBase.ShowDialog()

        '時刻設定フラグを返す
        Return blnDoSetTime

    End Function

    ''' <summary>
    ''' フォームロード時の処理
    ''' </summary>
    ''' <param name="sender">[IN]</param>
    ''' <param name="e">[IN]</param>
    ''' <remarks>画面の初期設定を行う
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub HBKZ0801_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        'データクラスの初期設定を行う
        With dataHBKZ0801

            'フォームオブジェクト
            .PropDtpSetDate = Me.dtpSetDate                 '設定時刻：設定日DateTimePickerEx
            .PropTxtSetTime = Me.txtSetTime.PropTxtTime     '設定時刻：設定時分テキストボックス

        End With

        'フォーム背景色設定
        Me.BackColor = commonLogicHBK.SetFormBackColor(PropConfigrationFlag)

        '画面初期表示を行う
        If logicHBKZ0801.InitFormMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [-10]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻から10分減算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMinus10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinus10.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_MINUS                '加減符号：-1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_10   '加減時間：10分
        End With

        '設定時刻から10分減算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [-5]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻から5分減算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMinus5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinus5.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_MINUS                '加減符号：-1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_5    '加減時間：5分
        End With

        '設定時刻から5分減算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [-1]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻から1分減算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnMinus1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnMinus1.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_MINUS                '加減符号：-1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_1    '加減時間：5分
        End With

        '設定時刻から1分減算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [0]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻に現在日時を設定する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSetNow_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSetNow.Click


        '設定時刻に現在日時を設定する
        If logicHBKZ0801.SetNowTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [+1]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻に1分加算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAdd1_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd1.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_PLUS                 '加減符号：+1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_1    '加減時間：1分
        End With

        '設定時刻に1分加算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [+5]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻に5分加算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAdd5_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd5.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_PLUS                 '加減符号：+1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_5    '加減時間：5分
        End With

        '設定時刻に5分加算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [+10]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>設定時刻に10分加算する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnAdd10_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd10.Click

        'データクラスに加減符号および加減時間をセット
        With dataHBKZ0801
            .PropIntFugou = logicHBKZ0801.FUGOU_PLUS                 '加減符号：+1
            .PropIntAddSubtrTime = logicHBKZ0801.TIME_ADD_SUBTR_10   '加減時間：10分
        End With

        '設定時刻に10分加算する
        If logicHBKZ0801.AddSubtrTimeMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

    End Sub

    ''' <summary>
    ''' [設定]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>入力チェックを行い、問題なければ時刻設定フラグをONにして当画面を閉じる
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnSet_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSet.Click

        '入力チェック
        If logicHBKZ0801.CheckInputValueMain(dataHBKZ0801) = False Then
            'エラーメッセージ表示
            MsgBox(puErrMsg, MsgBoxStyle.Critical, TITLE_ERROR)
            '処理終了
            Exit Sub
        End If

        '時刻設定フラグをON
        blnDoSetTime = True

        '当画面を閉じる
        Me.Close()

    End Sub

    ''' <summary>
    ''' [閉じる]ボタンクリック時の処理
    ''' </summary>
    ''' <remarks>当画面を閉じる
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClose.Click

        '当画面を閉じる
        Me.Close()

    End Sub
End Class