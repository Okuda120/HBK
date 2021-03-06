﻿Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' インシデント登録画面(返却確認書出力)ロジッククラス
''' </summary>
''' <remarks>インシデント登録画面(返却確認書出力)のロジックを定義したクラス
''' <para>作成情報：2012/07/26 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0205

    'インスタンス生成
    Public dataHBKC0205 As New DataHBKC0205
    Private sqlHBKC0205 As New SqlHBKC0205
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK


    ''' <summary>
    ''' 返却確認書出力メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0205">[IN/OUT]インシデント登録（返却確認書出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（返却確認書出力）に初期データをセットする
    ''' <para>作成情報：2012/07/26 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitMain(ByRef dataHBKC0205 As DataHBKC0205) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データ取得処理
        If GetInitData(dataHBKC0205) = False Then
            Return False
        End If

        'Excelデータ出力処理
        If OutputExcelFile(dataHBKC0205) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0205">[IN/OUT]インシデント登録（返却確認書出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>出力用のデータ取得する。
    ''' <para>作成情報：2012/07/26 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0205 As DataHBKC0205) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'データアダプター

        Try
            'コネクションを開く
            Cn.Open()

            '出力用Excelデータ取得処理
            If GetExcelData(Adapter, Cn, dataHBKC0205) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

            ''終了ログ出力
            'commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            ''正常処理終了
            'Return True

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
    ''' Excel出力用データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0205">[IN/OUT]インシデント登録（返却確認書出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（返却確認書出力）に必要なデータを取得する
    ''' <para>作成情報：2012/07/26 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetExcelData(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKC0205 As DataHBKC0205) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCISupport As New DataTable 'Excel用データテーブル

        Try

            'SQLの作成・設定
            If sqlHBKC0205.SetSelectCISupportSql(Adapter, Cn, dataHBKC0205) = False Then
                Return (False)
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "EXCEL出力用データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCISupport)

            '取得データをデータクラスにセット
            dataHBKC0205.PropDtCISupport = dtCISupport

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
            dtCISupport.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Excel用ファイル出力処理
    ''' </summary>
    ''' <param name="dataHBKC0205">[IN/OUT]インシデント登録（返却確認書出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータを基にExcel用ファイルを出力する
    ''' <para>作成情報：2012/07/26 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputExcelFile(ByRef dataHBKC0205 As DataHBKC0205) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFormatFilePath As String                         'フォーマットファイルパス


        Dim strBookNm As String
        Dim xlApp As Object = Nothing
        Dim xlBooks As Object = Nothing
        Dim xlBook As Object = Nothing
        Dim xlSheets As Object = Nothing
        Dim xlSheet As Object = Nothing
        Dim xlRange As Object = Nothing

        Try

            'フォーマットファイルパスの設定
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH, FORMAT_INCIDENT_HENKYAKU)

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

            'agree_lend_pcの操作を行う(セルに値をセット)
            If OutputAgreeReturn(xlSheets, xlSheet, dataHBKC0205) = False Then
                Return False
            End If

            'エクセルを開く
            xlApp.Visible = True



            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'ファイルを閉じる
            If Not xlApp Is Nothing Then
                xlApp.DisplayAlerts = False
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

        End Try

    End Function

    ''' <summary>
    ''' Excel操作(agree_return)
    ''' </summary>
    ''' <param name="dataHBKC0205">[IN/OUT]インシデント登録（返却確認書出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>agree_returnのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputAgreeReturn(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0205 As DataHBKC0205) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_HENKYAKU)                                                         'シート名（PC）

            With dataHBKC0205

                'シートにデータをセット
                xlSheet.Range(CELLNAME_INCNMB).Value = .PropIntIncNmb                                           '管理番号 
                xlSheet.Range(CELLNAME_KINDCD_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                 '機器管理番号：種別名＋番号
                xlSheet.Range(CELLNAME_MAKER_KISYUNM).Value = .PropStrMaker & .PropStrKisyuNM                   '貸出品名
                xlSheet.PageSetup.RightFooter = xlSheet.PageSetup.RightFooter & PropUserName                    '作業担当(フッタ)

                'サポセン機器テーブルを取得できなければ出力しない
                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAME_RENTALSTDT).Value = .PropDtCISupport.Rows(0).Item("RentalStDT")      '貸出開始日（申請日）
                    xlSheet.Range(CELLNAME_USRBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")      '所属部署
                    xlSheet.Range(CELLNAME_PARTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")        '番組名/所属班
                    xlSheet.Range(CELLNAME_PERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")            'PrismID
                    xlSheet.Range(CELLNAME_PERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")            '氏名
                    xlSheet.Range(CELLNAME_FUZOKUHIN).Value = .PropDtCISupport.Rows(0).Item("Fuzokuhin")        '付属品
                    xlSheet.Range(CELLNAME_RENTALEDDT).Value = .PropDtCISupport.Rows(0).Item("RentalEdDT")      'レンタル期限日
                Else
                    xlSheet.Range(CELLNAME_RENTALSTDT).Value = ""                                               '貸出開始日（申請日）
                    xlSheet.Range(CELLNAME_USRBUSYONM).Value = ""                                               '所属部署
                    xlSheet.Range(CELLNAME_PARTNERROOM).Value = ""                                              '番組名/所属班
                    xlSheet.Range(CELLNAME_PERTNERID).Value = ""                                                'PrismID
                    xlSheet.Range(CELLNAME_PERTNERNM).Value = ""                                                '氏名
                    xlSheet.Range(CELLNAME_FUZOKUHIN).Value = ""                                                '付属品
                    xlSheet.Range(CELLNAME_RENTALEDDT).Value = ""                                               'レンタル期限日
                End If

            End With

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

        End Try

    End Function


End Class
