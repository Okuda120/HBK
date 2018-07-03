Imports Common
Imports CommonHBK
Imports System.IO


''' <summary>
''' 関連ファイル設定画面ロジッククラス
''' </summary>
''' <remarks>関連ファイル設定画面のロジックを定義したクラス
''' <para>作成情報：2012/07/09 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKZ1101

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK


    ''' <summary>
    ''' 入力値クリアメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1101">[IN/OUT]関連ファイル設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面入力値をクリアする
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearFormMain(ByRef dataHBKZ1101 As DataHBKZ1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力値クリア
        If ClearForm(dataHBKZ1101) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力値チェック処理
    ''' </summary>
    ''' <param name="dataHBKZ1101">[IN/OUT]関連ファイル設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面入力値のチェックを行う
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKZ1101 As DataHBKZ1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力値チェック
        If CheckInputValue(dataHBKZ1101) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力値クリア処理
    ''' </summary>
    ''' <param name="dataHBKZ1101">[IN/OUT]関連ファイル設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面入力値をクリアする
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearForm(ByRef dataHBKZ1101 As DataHBKZ1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1101

                '格納ファイルパス
                .PropTxtFilePath.Text = ""
                

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
    ''' 入力値チェック処理
    ''' </summary>
    ''' <param name="dataHBKZ1101">[IN/OUT]関連ファイル設定画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面入力値のチェックを行い、問題があればエラーを返す
    ''' <para>作成情報：2012/07/09 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKZ1101 As DataHBKZ1101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1101

                '格納ファイルパス
                With .PropTxtFilePath
                    '未入力の場合、エラー
                    If .Text = "" Then
                        'メッセージセット
                        puErrMsg = Z1101_E001
                        '参照ボタンにフォーカスセット
                        dataHBKZ1101.PropBtnFileDialog.Focus()
                        Return False
                    End If
                End With

                '説明
                With .PropTxtFileNaiyo
                    '未入力の場合、エラー
                    If .Text.Trim() = "" Then
                        'メッセージセット
                        puErrMsg = Z1101_E002
                        'フォーカスセット
                        .Focus()
                        Return False
                    End If
                End With

                '格納ファイルパス桁数チェック
                With .PropTxtFilePath
                    '未入力の場合、エラー
                    If Path.GetFileName(.Text).Length > 174 Then
                        'メッセージセット
                        puErrMsg = Z1101_E003
                        '参照ボタンにフォーカスセット
                        dataHBKZ1101.PropBtnFileDialog.Focus()
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
