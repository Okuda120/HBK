Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 共通検索一覧(出力)画面Logicクラス
''' </summary>
''' <remarks>共通検索一覧(出力)画面のロジックを定義する
''' <para>作成情報：2012/06/14 kuga
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKB0102

    Private sqlHBKB0102 As New SqlHBKB0102          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス


    ''' <summary>
    ''' エクセルエクスポートメイン
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN]エクセル出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エクセルエクスポートメイン処理
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function ExcelExportMain(ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データ取得処理
        If GetMakeData(dataHBKB0102) = False Then
            Return False
        End If

        '取得データ加工処理
        If CreateGetData(dataHBKB0102) = False Then
            Return False
        End If

        'データ出力処理
        If ExcelExport(dataHBKB0102) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 共通検索（エクセル出力）データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通検索（エクセル出力）に必要なデータを取得する
    ''' <para>作成情報：2012/07/20 kawate
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetMakeData(ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCiKbnCD As String = ""       'CI種別

        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim table As New DataTable()

        Try
            'コネクションを開く
            Cn.Open()

            'CI種別取得
            strCiKbnCD = dataHBKB0102.PropStrCiKbnCD_Search

            'CI種別によりデータ取得
            Select Case strCiKbnCD

                Case CI_TYPE_SYSTEM

                    '共通検索EXCEL出力：システムデータ取得
                    If GetOutputDataForSys(Adapter, Cn, dataHBKB0102) = False Then
                        Return False
                    End If

                Case CI_TYPE_DOC

                    '共通検索EXCEL出力：文書データ取得
                    If GetOutputDataForDoc(Adapter, Cn, dataHBKB0102) = False Then
                        Return False
                    End If

                Case CI_TYPE_SUPORT

                    '共通検索EXCEL出力：サポセンデータ取得
                    If GetOutputDataForSap(Adapter, Cn, dataHBKB0102) = False Then
                        Return False
                    End If

                Case CI_TYPE_KIKI

                    '共通検索EXCEL出力：部所有機器データ取得
                    If GetOutputDataForBuy(Adapter, Cn, dataHBKB0102) = False Then
                        Return False
                    End If

            End Select

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
            table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【システム】共通検索EXCEL出力：システムデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別がシステムの共通検索EXCEL出力用のデータを取得する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOutputDataForSys(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutput As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKB0102.SetSelectSystemSql(Adapter, Cn, dataHBKB0102) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通検索EXCEL出力：システムデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutput)

            '取得データをデータクラスにセット
            dataHBKB0102.PropDtOutput = dtOutput


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
            dtOutput.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【文書】共通検索EXCEL出力：文書データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別が文書の共通検索EXCEL出力用のデータを取得する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOutputDataForDoc(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutput As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKB0102.SetSelectDocSql(Adapter, Cn, dataHBKB0102) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通検索EXCEL出力：文書データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutput)

            '取得データをデータクラスにセット
            dataHBKB0102.PropDtOutput = dtOutput


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
            dtOutput.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン】共通検索EXCEL出力：サポセンデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別がサポセンの共通検索EXCEL出力用のデータを取得する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOutputDataForSap(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutput As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKB0102.SetSelectSapSql(Adapter, Cn, dataHBKB0102) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通検索EXCEL出力：サポセンデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutput)

            '取得データをデータクラスにセット
            dataHBKB0102.PropDtOutput = dtOutput


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
            dtOutput.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【部所有機器】共通検索EXCEL出力：部所有機器データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別が部所有機器の共通検索EXCEL出力用のデータを取得する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOutputDataForBuy(ByVal Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtOutput As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKB0102.SetSelectBuySql(Adapter, Cn, dataHBKB0102) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通検索EXCEL出力：部所有機器データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtOutput)

            '取得データをデータクラスにセット
            dataHBKB0102.PropDtOutput = dtOutput


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
            dtOutput.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】取得データ加工処理
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別に応じて、取得データを出力用に加工する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateGetData(ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0102

                'CI種別によりデータ取得
                Select Case .PropStrCiKbnCD_Search

                    Case CI_TYPE_SYSTEM     'システム

                        '加工処理なし

                    Case CI_TYPE_DOC

                        '文書用加工処理
                        If CreateGetDataForDoc(dataHBKB0102) = False Then
                            Return False
                        End If

                    Case CI_TYPE_SUPORT

                        'サポセン用加工処理
                        If CreateGetDataForDoc(dataHBKB0102) = False Then
                            Return False
                        End If

                    Case CI_TYPE_KIKI

                        '部所有機器用加工処理
                        If CreateGetDataForDoc(dataHBKB0102) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【文書】取得データ加工処理
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別が文書の取得データを出力用に加工する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateGetDataForDoc(ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0102.PropDtOutput

                '不要な取得項目を削除する
                .Columns.Remove("CISort")   '並び順（CI共通情報）

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
    ''' 【サポセン】取得データ加工処理
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別がサポセンの取得データを出力用に加工する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateGetDataForSap(ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0102.PropDtOutput

                '不要な取得項目を削除する
                .Columns.Remove("CISort")   '並び順（CI共通情報）

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
    ''' 【部所有機器】取得データ加工処理
    ''' </summary>
    ''' <param name="dataHBKB0102">[IN/OUT]共通検索一覧EXCEL出力ロジックDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI種別が部所有機器の取得データを出力用に加工する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateGetDataForBuy(ByRef dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0102.PropDtOutput

                '不要な取得項目を削除する
                .Columns.Remove("CISort")   '並び順（CI共通情報）

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
    ''' エクセルエクスポート
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>エクセルのエクスポート処理
    ''' <para>作成情報：2012/06/05 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ExcelExport(ByVal dataHBKB0102 As DataHBKB0102) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCIKbnCD As String = ""                                   'CI種別

        Dim strFormatFilePath As String = ""        'フォーマットファイルパス
        Dim strOutPutFilePath As String = ""        '出力先ファイルパス
        Dim strBookNm As String = ""                'EXCELブック名

        Dim xlApp As Object = Nothing               'EXCELアプリケーション
        Dim xlBooks As Object = Nothing             'EXCELブック配列
        Dim xlBook As Object = Nothing              'EXCELブック
        Dim xlSheets As Object = Nothing            'EXCELシート配列
        Dim xlSheet As Object = Nothing             'EXCELシート
        Dim xlRange As Object = Nothing             'EXCELセル範囲

        Dim intStartRow As Integer = 2              '先頭の行番号
        Dim intStartColumns As Integer = 1          '先頭の列番号
        Dim intLineStyle As Integer = 1             '罫線の種類【実線：xlContinuous】

        Dim strDriveName As String = ""             '割当てドライブ文字列
        Dim strOutPutLogFilePath As String = ""     'ログ出力先パス
        Dim strOutPutLogFileName As String = ""     '出力ログファイル名

        Try

            '出力先ファイルパスの設定
            strOutPutFilePath = dataHBKB0102.PropStrOutPutFilePath
            'ログファイル名を設定
            strOutPutLogFileName = PropUserId & "_" & _
                                    DateTime.Now.ToString("yyyyMMddHHmmss") & "_" & _
                                    dataHBKB0102.PropStrOutPutFileName

            'CI種別取得
            strCiKbnCD = dataHBKB0102.PropStrCiKbnCD_Search

            'エクセルフォーマットパス取得
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH)

            'CI種別に応じたフォーマットファイルを取得
            Select Case strCiKbnCD

                Case CI_TYPE_SYSTEM     'システム

                    strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_COMMON_SYSTEM)

                Case CI_TYPE_DOC        '文書

                    strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_COMMON_DOC)

                Case CI_TYPE_SUPORT     'サポセン

                    strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_COMMON_SUPPORT)

                Case CI_TYPE_KIKI       '部所有機器

                    strFormatFilePath = Path.Combine(strFormatFilePath, FORMAT_COMMON_BUY)

            End Select

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

            'シートオブジェクトに格納
            xlSheets = xlBook.Worksheets
            '対象シートをセット
            xlSheet = xlSheets(1)

            'Excel操作
            With dataHBKB0102.PropDtOutput

                'データ行数分ループ
                For i = 0 To .Rows.Count - 1
                    'セル範囲設定
                    xlRange = xlSheet.Range(xlSheet.Cells(i + intStartRow, intStartColumns), xlSheet.Cells(i + intStartRow, .Columns.Count))
                    'セルに値をセット
                    xlRange.Value = .Rows(i).ItemArray
                    '罫線の設定
                    xlRange.Borders.LineStyle = intLineStyle
                Next

            End With


            'EXCELを保存
            xlBook.SaveAs(strOutPutFilePath)

            'Excelを閉じる
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

            'ログ出力先フォルダの存在チェック
            If Directory.Exists(strOutPutLogFilePath) = False Then
                'ログ出力先フォルダが存在していない場合フォルダを作成する
                Directory.CreateDirectory(strOutPutLogFilePath)
            End If

            'ログの出力先を設定
            strOutPutLogFilePath = Path.Combine(strOutPutLogFilePath, strOutPutLogFileName)

            'ログファイルの出力
            FileCopy(strOutPutFilePath, strOutPutLogFilePath)

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

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

            commonLogic.MRComObject(xlApp)          'xlAppの解放
            commonLogic.MRComObject(xlBooks)        'xlBooksの解放
            commonLogic.MRComObject(xlBook)         'xlBookの解放
            commonLogic.MRComObject(xlSheets)       'xlSheetsの解放
            commonLogic.MRComObject(xlSheet)        'xlSheetの解放
            commonLogic.MRComObject(xlRange)        'xlRangeの解放
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)

        End Try

    End Function

End Class
