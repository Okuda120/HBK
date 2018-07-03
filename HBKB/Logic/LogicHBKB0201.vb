Imports Common
Imports CommonHBK

Public Class LogicHBKB0201

    Private CommonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0201">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef DataHBKB0201 As DataHBKB0201) As Boolean

        'ログ出力
        CommonLogic.WriteLog(LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If InputCheck(DataHBKB0201) = False Then
            Return False
        End If

        'ログ出力
        CommonLogic.WriteLog(LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0201">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Private Function InputCheck(ByRef DataHBKB0201 As DataHBKB0201) As Boolean

        'ログ出力
        CommonLogic.WriteLog(LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数を宣言
        Dim strFilePath As String = DataHBKB0201.PropTxtFilePath.Text   'ファイルパス
        Dim strFileExt As String = ""

        Try
            'ファイル未選択チェック
            If strFilePath = "" Then
                'エラーメッセージセット
                puErrMsg = B0201_E001
                Return False
            End If

            'ファイル拡張子取得
            strFileExt = System.IO.Path.GetExtension(strFilePath)

            '拡張子チェック
            If strFileExt = EXTENTION_XLS Or strFileExt = EXTENTION_XLSX Then
            Else
                'エラーメッセージセット
                puErrMsg = B0201_E002
                Return False
            End If

            '取込ファイルの存在チェック
            If System.IO.File.Exists(strFilePath) = False Then
                'エラーメッセージセット
                puErrMsg = B0201_E003
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            CommonLogic.WriteLog(LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try

    End Function

    ''' <summary>
    ''' システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB0201">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/06/14 t.fukuo
    ''' <p>改訂情報 : 2012/07/09 y.ikushima</p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0201 As DataHBKB0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKB0201">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/05 t.fukuo
    ''' <p>改訂情報 : 2012/07/09 y.ikushima</p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0201 As DataHBKB0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With DataHBKB0201

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
End Class
