Imports Common
Imports CommonHBK


''' <summary>
''' 日時設定画面ロジッククラス
''' </summary>
''' <remarks>日時設定画面のロジックを定義したクラス
''' <para>作成情報：2012/07/05 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKZ0801

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Public Const FUGOU_PLUS As Integer = 1             '符号：＋
    Public Const FUGOU_MINUS As Integer = -1           '符号：－
    Public Const TIME_ADD_SUBTR_10 As Integer = 10     '加減時間：10分
    Public Const TIME_ADD_SUBTR_5 As Integer = 5       '加減時間：5分
    Public Const TIME_ADD_SUBTR_1 As Integer = 1       '加減時間：1分
    Public Const FORMAT_DATE As String = "yyyy/MM/dd"  '日付フォーマット


    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'フォームデータ設定
        If SetDataToForm(dataHBKZ0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 加減時間設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻より指定時分を加減する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddSubtrTimeMain(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '設定時刻より指定時分を加減する
        If AddSubtrTime(dataHBKZ0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 現在時刻設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻より指定時分を加減する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetNowTimeMain(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '現在日時を設定時刻に設定する
        If SetNowTime(dataHBKZ0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面項目の入力チェックを行う
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力チェック
        If CheckInputValue(dataHBKZ0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻に前画面からのパラメータを設定する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToForm(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0801

                '設定日付
                .PropDtpSetDate.txtDate.Text = .PropStrDate


                '設定時分
                .PropTxtSetTime.Text = .PropStrTime

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 加減時間設定処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻または現在日時より指定時分を加減する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddSubtrTime(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strDate As String = dataHBKZ0801.PropDtpSetDate.txtDate.Text     '設定日付
        Dim strTime As String = dataHBKZ0801.PropTxtSetTime.Text             '設定時分
        Dim dtmDateTime As DateTime = Nothing                               '設定日時

        Try
            With dataHBKZ0801

                '設定日時を取得
                If strDate <> "" Then

                    If strTime <> "" Then

                        '設定日付および時分が入力されている場合、設定日付と設定時分より設定日時を取得
                        dtmDateTime = DateTime.Parse(strDate & " " & strTime)

                    Else

                        '設定日付のみが入力されている場合、設定日付と現在時分より設定日時を取得
                        dtmDateTime = DateTime.Parse(strDate).AddHours(DateTime.Now().Hour).AddMinutes(DateTime.Now().Minute)

                    End If

                Else

                    If strTime <> "" Then

                        '設定時分のみが入力されている場合、現在日付と設定時分より設定日時を取得
                        dtmDateTime = DateTime.Parse(String.Format(DateTime.Now().ToShortDateString, FORMAT_DATE) & " " & strTime)

                    Else

                        '設定日付および時分が未入力の場合、現在日時を取得
                        dtmDateTime = DateTime.Now()

                    End If

                End If

                '設定日時から指定分数を加減する
                dtmDateTime = dtmDateTime.AddMinutes(.PropIntFugou * .PropIntAddSubtrTime)

                '加減した値より設定日付および時分を取得する
                strDate = String.Format(dtmDateTime.ToShortDateString(), FORMAT_DATE)
                strTime = FormatDateTime(dtmDateTime, DateFormat.ShortTime)

                'フォームコントロールに値をセット
                .PropDtpSetDate.txtDate.Text = strDate
                .PropTxtSetTime.Text = strTime

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 現在時刻設定処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻に現在日時を設定する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetNowTime(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0801

                'フォームコントロールに値をセット
                .PropDtpSetDate.txtDate.Text = String.Format(Now().ToShortDateString, FORMAT_DATE)
                .PropTxtSetTime.Text = FormatDateTime(Now(), DateFormat.ShortTime)

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKZ0801">[IN/OUT]日時設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面項目の入力チェックを行い、問題があればエラーを返す
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKZ0801 As DataHBKZ0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0801

                '設定日付
                With .PropDtpSetDate.txtDate

                    '未入力の場合
                    If .Text = "" Then
                        'エラーメッセージセット
                        puErrMsg = Z0801_E002
                        'フォーカスセット
                        .Focus()
                        'エラーを返す
                        Return False
                    End If

                End With

                '設定時分
                With .PropTxtSetTime

                    '未入力の場合
                    If .Text = "" Then
                        'エラーメッセージセット
                        puErrMsg = Z0801_E001
                        'フォーカスセット
                        .Focus()
                        'エラーを返す
                        Return False
                    End If

                End With

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
