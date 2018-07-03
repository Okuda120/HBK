Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 変更検索一覧Excel出力ロジッククラス
''' </summary>
''' <remarks>変更検索一覧Excel出力Logicクラス
''' <para>作成情報：2012/08/24 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKE0102

    'インスタンス作成
    Private sqlHBKE0102 As New SqlHBKE0102
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' 変更検索一覧（EXCEL出力）メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0102">[IN/OUT]変更検索一覧（EXCEL出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題検索一覧（EXCEL出力）メイン処理を行う
    ''' <para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutPutFileMain(ByRef dataHBKE0102 As DataHBKE0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変更データ取得
        If CreateOutFileForSearch(dataHBKE0102) = False Then
            Return False
        End If

        'ファイル作成処理
        If SetOutPutDataForExcelChange(dataHBKE0102) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 変更データ取得処理
    ''' </summary>
    ''' <param name="dataHBKE0102">[IN/OUT]変更検索一覧（EXCEL出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント検索一覧出出力処理を行う
    ''' <para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutFileForSearch(ByRef dataHBKE0102 As DataHBKE0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtOutPut As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            '変更検索一覧マスターデータ取得SQLの作成・設定
            If sqlHBKE0102.SetSelectChangeInfoSql(Adapter, Cn, dataHBKE0102) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "変更検索一覧データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutPut)

            'データクラスに保存
            dataHBKE0102.PropDtResult = dtOutPut

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
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            dtOutPut.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 変更検索一覧Excel出力データ出力処理
    ''' </summary>
    ''' <param name="dataHBKE0102">[IN/OUT]変更検索一覧（EXCEL出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBから取得した変更検索一覧Excel出力データをExcelにセットする
    ''' <para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOutPutDataForExcelChange(ByRef dataHBKE0102 As DataHBKE0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartRow As Integer = 3              '先頭の行番号
        Dim intStartColumns As Integer = 1          '先頭の列番号
        Dim intLineStyle As Integer = 1             '罫線の種類【実線：xlContinuous】
        Dim xlApp As Object = Nothing               'Applicationオブジェクト
        Dim xlBooks As Object = Nothing             'Workbooksオブジェクト
        Dim xlBook As Object = Nothing              'Workbookオブジェクト
        Dim xlSheets As Object = Nothing            'Worksheetsオブジェクト
        Dim xlSheet As Object = Nothing             'Worksheetオブジェクト
        Dim xlRange As Object = Nothing             'Rangeオブジェクト
        Dim strBkNm As String                       'OriginalBook名
        Dim strDriveName As String = ""             '割当てドライブ文字列
        Dim strFormatFilePath As String             'フォーマットファイルパス
        Dim strOutPutFilePath As String = dataHBKE0102.PropStrOutPutFilePath '出力先ファイルパス
        Dim strOutPutLogFilePath As String = ""     'ログ出力先パス
        Dim strOutPutLogFileName As String = ""     '出力ログファイル名

        strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)
        strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_CHANGE_SEARCH)

        Try

            'フォーマットファイル存在チェック
            If File.Exists(strFormatFilePath) = False Then
                puErrMsg = HBK_E001
                Return False
            End If

            'ファイルを開く
            xlApp = CreateObject("Excel.Application")

            'Workbook取得
            xlBooks = xlApp.Workbooks

            '取込ファイルを開く
            xlBook = xlBooks.Open(strFormatFilePath)

            'OriginalBook名を取得
            strBkNm = xlBook.name

            'シート(すべて)のコピー
            xlBook.Sheets.Copy()

            'コピー元(Original)xlsを閉じる
            xlApp.Application.Windows(strBkNm).Close()

            'コピー先(出力先)のエクセルを開く
            xlBook = xlApp.Workbooks(1)

            'シートオブジェクトに格納
            xlSheets = xlBook.Worksheets

            '対象シートをセット
            xlSheet = xlBook.Sheets(1)

            'データ行数分ループ
            With dataHBKE0102.PropDtResult
                For i = 0 To .Rows.Count - 1
                    'セルに値をセット
                    xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count)).Value = .Rows(i).ItemArray
                    '罫線の設定
                    xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count)).Borders.LineStyle = intLineStyle
                Next
            End With

            'ファイルの保存
            xlBook.SaveAs(strOutPutFilePath)

            'エクセルを閉じる
            xlApp.Quit()

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

            'コピー先ディレクトリ存在チェック
            If Directory.Exists(strOutPutLogFilePath) = False Then
                'コピー先ディレクトリが見つからない場合は作成
                Directory.CreateDirectory(strOutPutLogFilePath)
            End If

            'ログファイル名を設定
            strOutPutLogFileName = PropUserId & "_" & _
                                    DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & _
                                    dataHBKE0102.PropStrOutPutFileName

            'ログの出力先を設定
            strOutPutLogFilePath = Path.Combine(strOutPutLogFilePath, strOutPutLogFileName)

            'ログファイルの出力
            FileCopy(strOutPutFilePath, strOutPutLogFilePath)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
            dataHBKE0102.PropDtResult.Dispose()
        End Try
    End Function

End Class
