Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 部所有機器検索一覧(月次報告用出力)ロジッククラス
''' </summary>
''' <remarks>部所有機器検索一覧(月次報告用出力)のロジックを定義したクラス
''' <para>作成情報：2012/07/06 s.yamaguchi
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB1203

    'インスタンス生成
    Private sqlHBKB1203 As New SqlHBKB1203
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const SHEET_NAME_GETUJIHOUKOKUYOU As String = "月次報告用"

    ''' <summary>
    ''' 部所有機器検索一覧(月次報告用出力)メイン
    ''' </summary>
    ''' <param name="dataHBKB1203">[IN/OUT]部所有機器検索一覧Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧(月次報告用出力)メイン処理
    ''' <para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function MakeGetujiHoukokuMain(dataHBKB1203 As DataHBKB1203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データ取得処理
        If GetMakeData(dataHBKB1203) = False Then
            Return False
        End If

        'データ出力処理
        If MakeGetujiHoukokuFile(dataHBKB1203) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function


    ''' <summary>
    ''' 部所有機器検索一覧(月次報告用出力)データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB1203">[IN/OUT]部所有機器検索一覧Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>部所有機器検索一覧(月次報告用出力)に必要なデータを取得する
    ''' <para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetMakeData(dataHBKB1203 As DataHBKB1203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'コネクションを開く
            Cn.Open()

            '出力用月次報告データ取得処理
            If GetGetujiHoukokuData(Adapter, Cn, dataHBKB1203) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 出力用月次報告データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKB1203">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>月次報告用出力に必要なCI部所有機器データを取得する
    ''' <para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetGetujiHoukokuData(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKB1203 As DataHBKB1203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGetujiHoukokuTable As New DataTable '月次報告用データテーブル

        Try

            'SQLの作成・設定
            If sqlHBKB1203.SetSelectCIBuyTableSql(Adapter, Cn, dataHBKB1203) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGetujiHoukokuTable)

            '取得データをデータクラスにセット
            dataHBKB1203.PropDtGetujiHoukokuTable = dtGetujiHoukokuTable

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally
            'リソースの解放
            dtGetujiHoukokuTable.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 部所有機器検索一覧(人事連絡用出力)データ出力処理
    ''' </summary>
    ''' <param name="dataHBKB1203">[IN/OUT]部所有機器検索一覧Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/06 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function MakeGetujiHoukokuFile(dataHBKB1203 As DataHBKB1203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartRow As Integer = 2                          '先頭の行番号
        Dim intStartColumns As Integer = 1                      '先頭の列番号
        Dim intLineStyle As Integer = 1                         '罫線の種類【実線：xlContinuous】
        Dim clrLineColor As Color = Color.FromArgb(0, 0, 255)   '罫線の色【青】

        Dim strDriveName As String = ""                         '割当てドライブ文字列
        Dim strFormatFilePath As String                         'フォーマットファイルパス

        Dim strOutPutFilePath As String = ""                    '出力先ファイルパス
        Dim strOutPutLogFilePath As String = ""                 'ログ出力先パス
        Dim strOutPutLogFileName As String = ""                 '出力ログファイル名

        Dim strBookNm As String
        Dim xlApp As Object = Nothing
        Dim xlBooks As Object = Nothing
        Dim xlBook As Object = Nothing
        Dim xlSheets As Object = Nothing
        Dim xlSheet As Object = Nothing
        Dim xlRange As Object = Nothing

        Try
            'フォーマットファイルパスの設定
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)
            strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_BUY_GETUJIHOUKOKU)
            '出力先ファイルパスの設定
            strOutPutFilePath = dataHBKB1203.PropStrOutPutFilePath '出力先ファイルパス
            'ログファイル名を設定
            strOutPutLogFileName = PropUserId & "_" & _
                                    DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & _
                                    dataHBKB1203.PropStrOutPutFileName

            'フォーマットファイル存在チェック
            If File.Exists(strFormatFilePath) = False Then
                puErrMsg = HBK_E001
                Return False
            End If

            'ファイルを開く
            xlApp = CreateObject("Excel.Application")
            xlBooks = xlApp.Workbooks
            'コピー元のフォーマットファイルを開く
            xlBook = xlBooks.Open(strFormatFilePath)
            'OriginalBook名を取得
            strBookNm = xlBook.name
            'シート(すべて)のコピー
            xlBook.Sheets.Copy()
            'コピー元(Original)xlsを閉じる
            xlApp.Application.Windows(strBookNm).Close()

            'コピー先(出力先)のエクセルを開く
            xlBook = xlApp.Workbooks(1)
            'コピー先のエクセル名を取得
            strBookNm = xlBook.Name

            'シートオブジェクトに格納
            xlSheets = xlBook.Worksheets

            '対象シートをセット
            xlSheet = xlSheets(SHEET_NAME_GETUJIHOUKOKUYOU)

            'Excel操作
            With dataHBKB1203.PropDtGetujiHoukokuTable

                'データ行数分ループ
                For i = 0 To .Rows.Count - 1
                    'セルに値をセット
                    xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count)).Value = .Rows(i).ItemArray
                    '罫線の設定
                    xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count)).Borders.LineStyle = intLineStyle
                    xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count)).Borders.Color = clrLineColor
                Next

            End With

            'ファイルの保存
            xlBook.SaveAs(strOutPutFilePath)

            ''★★★--------------------------------------------------------
            'ログ出力処理

            'PCの論理ドライブ名をすべて取得する
            Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
            '利用可能な論理ドライブ名を取得する
            For Each strDrive As String In DRIVES
                If strDrives.Contains(strDrive) = False Then
                    strDriveName = strDrive.Substring(0, 2)
                    Exit For
                End If
            Next

            'NetUse設定
            If commonLogicHBK.NetUseConect(strDriveName) = False Then
                Return False
            End If

            'アップロード先のディレクトリセット
            strOutPutLogFilePath = Path.Combine(strDriveName, PropOutputLogSavePath, DateTime.Now.ToString("yyyyMMdd"))

            'ログ出力先フォルダの存在チェック
            If Directory.Exists(strOutPutLogFilePath) = False Then
                'ログ出力先フォルダが存在していない場合フォルダを作成する
                Directory.CreateDirectory(strOutPutLogFilePath)
            End If

            'ログの出力先を設定
            strOutPutLogFilePath = Path.Combine(strOutPutLogFilePath, strOutPutLogFileName)

            'ログファイルの出力
            FileCopy(strOutPutFilePath, strOutPutLogFilePath)

            'Excelを閉じる
            xlApp.Quit()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'Excelを閉じる
            If Not xlApp Is Nothing Then
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                xlApp.Quit()                           'Excelを閉じる
            End If

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False

        Finally

            commonLogic.MRComObject(xlApp)      'xlAppの解放
            commonLogic.MRComObject(xlBooks)    'xlBooksの解放
            commonLogic.MRComObject(xlBook)     'xlBookの解放
            commonLogic.MRComObject(xlSheets)   'xlSheetsの解放
            commonLogic.MRComObject(xlSheet)    'xlSheetの解放
            commonLogic.MRComObject(xlRange)    'xlRangeの解放
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)

        End Try

    End Function

End Class
