Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 機器一括検索一覧（EXCEL出力）ロジッククラス
''' </summary>
''' <remarks>機器一括検索一覧（EXCEL出力）のロジックを定義したクラス
''' <para>作成情報：2012/07/17 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0702

    'インスタンス作成
    Private sqlHBKB0702 As New SqlHBKB0702
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' 機器一括検索一覧（EXCEL出力）メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器一括検索一覧（EXCEL出力）メイン処理を行う
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutPutFileMain(ByRef dataHBKB0702 As dataHBKB0702) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '出力ファイル作成処理
        If CreateOutPutFile(dataHBKB0702) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 機器一括検索一覧（EXCEL出力）処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器一括検索一覧（EXCEL出力）処理を行う
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutPutFile(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'チェックグラフによって出力する内容を分岐する
            With dataHBKB0702
                If .PropBolMaster = True Then
                    'マスタ
                    If CreateOutFileForMaster(dataHBKB0702) = False Then
                        Return False
                    End If
                ElseIf .PropBolIntroduct = True Then
                    '導入
                    If CreateOutFileForIntroduct(dataHBKB0702) = False Then
                        Return False
                    End If
                ElseIf .PropBolRireki = True Then
                    '履歴
                    If CreateOutFileForRireki(dataHBKB0702) = False Then
                        Return False
                    End If
                End If
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
    ''' 機器一括検索一覧出力_マスター処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器一括検索一覧出力_マスター出力処理を行う
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutFileForMaster(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter


        Try
            'コネクションを開く
            Cn.Open()

            '対象データ取得
            If GetOutPutDateForExcelMaster(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ファイル作成処理
            If SetOutPutDataForExcelMaster(dataHBKB0702) = False Then
                Return False
            End If

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
    ''' 機器一括検索一覧Excel出力データ取得_マスター処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBに接続し、Excel出力用のデータを取得する（マスター）
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetOutPutDateForExcelMaster(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutPut As New DataTable

        Try
            '機器一括検索一覧マスターデータ取得SQLの作成・設定
            If sqlHBKB0702.SetSelectMasterForExcel(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器一括検索一覧マスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutPut)

            'データクラスに保存
            dataHBKB0702.PropDtExcelMaster = dtOutPut

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
            dtOutPut.Dispose()

        End Try

    End Function

    ''' <summary>
    ''' 機器一括検索一覧出力_マスターデータ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBから取得したマスターデータをExcelにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOutPutDataForExcelMaster(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartRow As Integer = 2              '先頭の行番号
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
        Dim strOutPutFilePath As String = dataHBKB0702.PropStrOutPutFilePath '出力先ファイルパス
        Dim strOutPutLogFilePath As String = ""     'ログ出力先パス
        Dim strOutPutLogFileName As String = ""     '出力ログファイル名

        strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)
        strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_BUY_KIKIIKKATSUKENSAKU_MASTER)

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
            With dataHBKB0702.PropDtExcelMaster
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
                                    dataHBKB0702.PropStrOutPutFileName

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
        End Try
    End Function

    ''' <summary>
    ''' 機器一括検索一覧出力_導入処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器一括検索一覧出力_導入出力処理を行う
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutFileForIntroduct(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '対象データ取得
            If GetOutPutDateForExcelIntroduct(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ファイル作成処理
            If SetOutPutDataForExcelIntroduct(dataHBKB0702) = False Then
                Return False
            End If

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
    ''' 機器一括検索一覧Excel出力データ取得_導入処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBに接続し、Excel出力用のデータを取得する（導入
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetOutPutDateForExcelIntroduct(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutPut As New DataTable

        Try
            '機器一括検索一覧マスターデータ取得SQLの作成・設定
            If sqlHBKB0702.SetSelectIntroductForExcel(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器一括検索一覧導入データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutPut)

            'データクラスに保存
            dataHBKB0702.PropDtExcelIntroduct = dtOutPut

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
            dtOutPut.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 機器一括検索一覧出力_導入データ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBから取得した導入データをExcelにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOutPutDataForExcelIntroduct(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartRow As Integer = 2              '先頭の行番号
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
        Dim strOutPutFilePath As String = dataHBKB0702.PropStrOutPutFilePath '出力先ファイルパス
        Dim strOutPutLogFilePath As String = ""     'ログ出力先パス
        Dim strOutPutLogFileName As String = ""     '出力ログファイル名

        strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)
        strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_BUY_KIKIIKKATSUKENSAKU_DOUNYU)

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
            With dataHBKB0702.PropDtExcelIntroduct
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
                                    dataHBKB0702.PropStrOutPutFileName

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
                xlBook.Close(SaveChanges:=False)
                xlApp.Quit()
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

    ''' <summary>
    ''' 機器一括検索一覧出力_履歴処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>機器一括検索一覧出力_履歴出力処理を行う
    ''' <para>作成情報：2012/07/17 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateOutFileForRireki(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '対象データ取得
            If GetOutPutDateForExcelRireki(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ファイル作成処理
            If SetOutPutDataForExcelRireki(dataHBKB0702) = False Then
                Return False
            End If

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
    ''' 機器一括検索一覧Excel出力データ取得_履歴処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBに接続し、Excel出力用のデータを取得する（履歴）
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetOutPutDateForExcelRireki(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB0702 As DataHBKB0702) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutPut As New DataTable

        Try
            '機器一括検索一覧マスターデータ取得SQLの作成・設定
            If sqlHBKB0702.SetSelectRirekiForExcel(Adapter, Cn, dataHBKB0702) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器一括検索一覧履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutPut)

            'データクラスに保存
            dataHBKB0702.PropDtExcelRireki = dtOutPut

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
    ''' 機器一括検索一覧出力_履歴データ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0702">[IN/OUT]機器一括検索一覧Excel出力Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>DBから取得した履歴データをExcelにセットする
    ''' <para>作成情報：2012/07/19 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetOutPutDataForExcelRireki(ByRef dataHBKB0702 As DataHBKB0702) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartRow As Integer = 2              '先頭の行番号
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
        Dim strOutPutFilePath As String = dataHBKB0702.PropStrOutPutFilePath '出力先ファイルパス
        Dim strOutPutLogFilePath As String = ""     'ログ出力先パス
        Dim strOutPutLogFileName As String = ""     '出力ログファイル名

        strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)
        strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_BUY_KIKIIKKATSUKENSAKU_RIREKI)

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
            With dataHBKB0702.PropDtExcelRireki
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
                                    dataHBKB0702.PropStrOutPutFileName

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
                xlBook.Close(SaveChanges:=False)
                xlApp.Quit()
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
