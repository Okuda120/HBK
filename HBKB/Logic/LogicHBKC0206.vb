Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' インシデント登録（チェックリスト出力）ロジッククラス
''' </summary>
''' <remarks>インシデント登録（チェックリスト出力）のロジッククラス
''' <para>作成情報：2012/07/30 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0206

    'インスタンス生成
    Public dataHBKC0206 As New DataHBKC0206
    Private sqlHBKC0206 As New SqlHBKC0206
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（チェックリスト出力）に初期データをセットする
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitMain(ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データ取得処理
        If GetInitData(dataHBKC0206) = False Then
            Return False
        End If

        'Excelデータ出力処理
        If OutputExcelFile(dataHBKC0206) = False Then
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
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>出力用のデータ取得する。
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'データアダプター

        Try
            'コネクションを開く
            Cn.Open()

            'CIサポセン機器履歴テーブル取得処理
            If GetCISupport(Adapter, Cn, dataHBKC0206) = False Then
                Return False
            End If

            'セット機器履歴テーブル取得処理
            If GetSetKiki(Adapter, Cn, dataHBKC0206) = False Then
                Return False
            End If

            'オプションソフト履歴テーブル取得処理
            If GetOptionSoft(Adapter, Cn, dataHBKC0206) = False Then
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
    ''' CIサポセン機器履歴テーブル取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（チェックリスト出力）に必要なCIサポセン機器履歴テーブルを取得する
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetCISupport(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtExcelTable As New DataTable 'Excel用データテーブル

        Try

            'SQLの作成・設定
            If sqlHBKC0206.SetSelectCISupportSql(Adapter, Cn, dataHBKC0206) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIサポセン機器履歴", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtExcelTable)

            '取得データをデータクラスにセット
            dataHBKC0206.PropDtCISupport = dtExcelTable

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
            dtExcelTable.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' セット機器履歴テーブル取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（チェックリスト出力）に必要なセット機器履歴テーブルを取得する
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSetKiki(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtExcelTable As New DataTable 'Excel用データテーブル

        Try

            'SQLの作成・設定
            If sqlHBKC0206.SetSelectSetKikiSql(Adapter, Cn, dataHBKC0206) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器履歴", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtExcelTable)

            '取得データをデータクラスにセット
            dataHBKC0206.PropDtSetKiki = dtExcelTable

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
            dtExcelTable.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' オプションソフト履歴テーブル取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント登録（チェックリスト出力）に必要なオプションソフト履歴テーブルを取得する
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetOptionSoft(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtExcelTable As New DataTable 'Excel用データテーブル

        Try

            'SQLの作成・設定
            If sqlHBKC0206.SetSelectOptionSoftSql(Adapter, Cn, dataHBKC0206) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "オプションソフト履歴", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtExcelTable)

            '取得データをデータクラスにセット
            dataHBKC0206.PropDtOptionSoft = dtExcelTable

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
            dtExcelTable.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' Excel用ファイル出力処理
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータを基にExcel用ファイルを出力する
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputExcelFile(ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFormatFilePath As String                         'フォーマットファイルパス

        'Excel用変数
        Dim strBookNm As String
        Dim xlApp As Object = Nothing
        Dim xlBooks As Object = Nothing
        Dim xlBook As Object = Nothing
        Dim xlSheets As Object = Nothing
        Dim xlSheet As Object = Nothing
        Dim xlRange As Object = Nothing

        Try

            'フォーマットファイルパスの設定
            strFormatFilePath = Path.Combine(Application.StartupPath, FORMAT_FOLDER_PATH, FORMAT_INCIDENT_CHECK)

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

            'check_lend_mobの操作を行う(セルに値をセット)
            If OutputCheckLendMob(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_lend_pressの操作を行う(セルに値をセット)
            If OutputCheckLendPress(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_lend_token_normalの操作を行う(セルに値をセット)
            If OutputCheckLendTokenNormal(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_lend_token_sendの操作を行う(セルに値をセット)
            If OutputCheckLendTokenSend(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_set_pcの操作を行う(セルに値をセット)
            If OutputCheckSetPC(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_set_disの操作を行う(セルに値をセット)
            If OutputCheckSetDis(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_remove_pcの操作を行う(セルに値をセット)
            If OutputCheckRemovePC(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_remove_disの操作を行う(セルに値をセット)
            If OutputCheckRemoveDis(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_return_mobの操作を行う(セルに値をセット)
            If OutputCheckReturnMob(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_return_tokenの操作を行う(セルに値をセット)
            If OutputCheckReturnToken(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'check_extendの操作を行う(セルに値をセット)
            If OutputCheckExtend(xlSheets, xlSheet, dataHBKC0206) = False Then
                Return False
            End If

            'エクセルを表示する
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
    ''' Excel操作(check_lend_mob)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_lend_mobのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckLendMob(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット  
            xlSheet = xlSheets(SHEETNAME_CHECK_LEND_MOB)                                                                            'シート名:（チェックシート）MOB


            With dataHBKC0206
                
                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If


                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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

    ''' <summary>
    ''' Excel操作(check_lend_press)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_lend_pressのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckLendPress(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_LEND_PRESS)                                                                              'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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


    ''' <summary>
    ''' Excel操作(check_lend_token_normal)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_lend_token_normalのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckLendTokenNormal(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_LEND_TOKEN_NORMAL)                                                                       'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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

    ''' <summary>
    ''' Excel操作(check_lend_token_send)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_lend_token_sendのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckLendTokenSend(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_LEND_TOKEN_SEND)                                                                         'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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

    ''' <summary>
    ''' Excel操作(check_set_pc)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_set_pcのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckSetPC(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_SET_PC)                                                                                  'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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

    ''' <summary>
    ''' Excel操作(check_set_dis)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_set_disのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckSetDis(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_SET_DIS)                                                                                 'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")
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

    ''' <summary>
    ''' Excel操作(check_remove_pc)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_remove_pcのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckRemovePC(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_REMOVE_PC)                                                                               'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")


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


    ''' <summary>
    ''' Excel操作(check_remove_dis)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_remove_disのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckRemoveDis(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_REMOVE_DIS)                                                                              'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")

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


    ''' <summary>
    ''' Excel操作(check_return_mob)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_return_mobのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckReturnMob(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_RETURN_MOB)                                                                              'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")



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

    ''' <summary>
    ''' Excel操作(check_return_token)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>check_return_tokenのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckReturnToken(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_RETURN_TOKEN)                                                                            'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")

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

    ''' <summary>
    ''' Excel操作(check_extend)
    ''' </summary>
    ''' <param name="dataHBKC0206">[IN/OUT]インシデント登録（チェックリスト出力）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>agree_lend_pcのExcel出力処理を行う
    ''' <para>作成情報：2012/08/06 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function OutputCheckExtend(ByVal xlSheets As Object, _
                                       ByVal xlSheet As Object, _
                                       ByRef dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '対象シートをセット
            xlSheet = xlSheets(SHEETNAME_CHECK_EXTEND)                                                                                  'シート名:（貸出チェックシート）MOB


            With dataHBKC0206

                'シートにデータをセット
                xlSheet.Range(CELLNAM_CHECK_INCNMB).Value = .PropIntIncNmb                                                              'インシデント番号
                xlSheet.Range(CELLNAM_CHECK_TITLE).Value = .PropStrTitle                                                                'タイトル
                xlSheet.Range(CELLNAM_CHECK_MAKER).Value = .PropStrMaker                                                                'メーカー
                xlSheet.Range(CELLNAM_CHECK_KISYU).Value = .PropStrKisyuNM                                                              '機種
                xlSheet.Range(CELLNAM_CHECK_KIKIKIND_KIKINMB).Value = .PropStrKindNM & .PropStrKikiNmb                                  '機器種別名+機器番号

                If .PropDtCISupport.Rows.Count() > 0 Then
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = .PropDtCISupport.Rows(0).Item("UsrID")                             '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = .PropDtCISupport.Rows(0).Item("UsrNM")                             '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = .PropDtCISupport.Rows(0).Item("UsrCompany")                  '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = .PropDtCISupport.Rows(0).Item("UsrBusyoNM")                   '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = .PropDtCISupport.Rows(0).Item("UsrMailAdd")                   '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = .PropDtCISupport.Rows(0).Item("UsrContact")                   '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = .PropDtCISupport.Rows(0).Item("UsrRoom")                         '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = .PropDtCISupport.Rows(0).Item("FixedIP")                             '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = .PropDtCISupport.Rows(0).Item("Serial")                               '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = .PropDtCISupport.Rows(0).Item("SetBuil")                             '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = .PropDtCISupport.Rows(0).Item("SetFloor")                           '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = .PropDtCISupport.Rows(0).Item("SetDeskNo")                         '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = .PropDtCISupport.Rows(0).Item("SetKyokuNM")                         '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = .PropDtCISupport.Rows(0).Item("SetBusyoNM")                         '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = .PropDtCISupport.Rows(0).Item("SetRoom")                             '設置番組/部屋
                Else
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERID).Value = ""                                                                 '相手ID
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERNM).Value = ""                                                                 '相手氏名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERICOMPANY).Value = ""                                                           '相手会社名
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERBUSYONM).Value = ""                                                            '相手部署
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERMAILADD).Value = ""                                                            '相手メールアドレス
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERCONTACT).Value = ""                                                            '相手連絡先
                    xlSheet.Range(CELLNAM_CHECK_SPPERTNERROOM).Value = ""                                                               '相手番組/部屋
                    xlSheet.Range(CELLNAM_CHECK_SPFIXEDIP).Value = ""                                                                   '固定IP
                    xlSheet.Range(CELLNAM_CHECK_SPSERIAL).Value = ""                                                                    '製造番号（シリアル）
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUIL).Value = ""                                                                   '設置建物
                    xlSheet.Range(CELLNAM_CHECK_SPSETFLOOR).Value = ""                                                                  '設置フロア
                    xlSheet.Range(CELLNAM_CHECK_SPSETDESKNO).Value = ""                                                                 '設置デスクNo
                    xlSheet.Range(CELLNAM_CHECK_SPSETKYOKU).Value = ""                                                                  '設置局
                    xlSheet.Range(CELLNAM_CHECK_SPSETBUSYO).Value = ""                                                                  '設置部署
                    xlSheet.Range(CELLNAM_CHECK_SPSETROOM).Value = ""                                                                   '設置番組/部屋
                End If

                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = ""                                                                         'セット機器
                For SetIndex = 0 To .PropDtSetKiki.Rows.Count - 1
                    If SetIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"             'セット機器
                    Else
                        xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value += .PropDtSetKiki.Rows(SetIndex).Item("SetKikiNo") & "，"            'セット機器
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value = xlSheet.Range(CELLNAM_CHECK_SETKIKI).Value.TrimEnd("，")

                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = ""                                                                      'オプションソフト
                For OptIndex = 0 To .PropDtOptionSoft.Rows.Count - 1
                    If OptIndex = 0 Then
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"          'オプションソフト
                    Else
                        xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value += .PropDtOptionSoft.Rows(OptIndex).Item("SoftNM") & "，"         'オプションソフト
                    End If
                Next
                '最後の,の消去
                xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value = xlSheet.Range(CELLNAM_CHECK_OPTIONSOFT).Value.TrimEnd("，")

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
