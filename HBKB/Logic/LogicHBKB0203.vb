Imports Common
Imports CommonHBK
Imports Npgsql
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

''' <summary>
''' 一括登録　文書ロジッククラス
''' </summary>
''' <remarks>一括登録のロジックを定義したクラス
''' <para>作成情報：2012/07/20 s.tsuruta
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0203

    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKB0203 As New SqlHBKB0203

    'Public定数宣言
    'Excelのスタート行
    Public Const EXL_START_ROW As Integer = 1
    'Excelの行をセット
    Public Const EXL_ACQUISITION_NUM As Integer = 1                                 '取込番号
    Public Const EXL_NUM As Integer = 2                                             '番号（手動）
    Public Const EXL_GROUPING_1 As Integer = 3                                      '分類1
    Public Const EXL_GROUPING_2 As Integer = 4                                      '分類2
    Public Const EXL_TITLE As Integer = 5                                           '名称
    Public Const EXL_STATUS As Integer = 6                                          'ステータス
    Public Const EXL_CIOWNER_CD As Integer = 7                                      'オーナーCD
    Public Const EXL_EXPLANATION As Integer = 8                                     '説明
    Public Const EXL_FREE_TEXT_1 As Integer = 9                                     'フリーテキスト1
    Public Const EXL_FREE_TEXT_2 As Integer = 10                                     'フリーテキスト2
    Public Const EXL_FREE_TEXT_3 As Integer = 11                                    'フリーテキスト3
    Public Const EXL_FREE_TEXT_4 As Integer = 12                                    'フリーテキスト4
    Public Const EXL_FREE_TEXT_5 As Integer = 13                                    'フリーテキスト5
    Public Const EXL_FREE_FLG_1 As Integer = 14                                     'フリーフラグ1
    Public Const EXL_FREE_FLG_2 As Integer = 15                                     'フリーフラグ2
    Public Const EXL_FREE_FLG_3 As Integer = 16                                     'フリーフラグ3
    Public Const EXL_FREE_FLG_4 As Integer = 17                                     'フリーフラグ4
    Public Const EXL_FREE_FLG_5 As Integer = 18                                     'フリーフラグ5
    Public Const EXL_VERSION As Integer = 19                                        '版（手動）
    Public Const EXL_CRATEID As Integer = 20                                        '作成者ID
    Public Const EXL_CRATENM As Integer = 21                                        '作成者名
    Public Const EXL_CREATEDT As Integer = 22                                       '作成年月日
    Public Const EXL_LASTUPID As Integer = 23                                       '最終更新者ID
    Public Const EXL_LASTUPNM As Integer = 24                                       '最終更新者名
    Public Const EXL_LASTUPDT As Integer = 25                                       '最終更新日時
    Public Const EXL_FILEPATH As Integer = 26                                       '取込ファイルパス
    Public Const EXL_CHARGEID As Integer = 27                                       '文書責任者ID
    Public Const EXL_CHARGENM As Integer = 28                                       '文書責任者名
    Public Const EXL_SHARETEAMNM As Integer = 29                                    '文書配布先
    Public Const EXL_OFFERNM As Integer = 30                                        '文書提供者
    Public Const EXL_DELDT As Integer = 31                                          '文書廃棄年月日
    Public Const EXL_DELREASON As Integer = 32                                      '文書廃棄理由

    Private strOutLog As String                                                     'ログ保存用文字列
    Private aryClassTitle As New ArrayList                      '分類１、分類２、名称重複チェック

    '列名配列
    Private strColNm As String() = COLUMNNAME_DOC



    ''' <summary>
    ''' ファイル入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/20 s.tsuruta（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheckMain(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力ファイルチェック処理
        If FileInputCheck(dataHBKB0203) = False Then
            Return False
        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' ファイル入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行い、入力チェックエラーが発生するとログファイルに書き込む
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/20 s.tsuruta（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheck(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログ文字列初期化
            strOutLog = ""

            '入力チェック
            If InputCheck(dataHBKB0203) = False Then
                Return False
            End If

            '入力チェックエラー時にログ出力用変数にデータがある場合ログ出力画面へ
            If strOutLog <> "" Then
                'ログ出力処理
                If SetOutLog(dataHBKB0203) = False Then
                    Return False
                End If
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/20 s.tsuruta（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function InputCheck(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '入力チェック用配列取得・・入力項目必須チェック、入力項目重複チェック（ファイル内）処理
            If SetArryInputForCheck(dataHBKB0203) = False Then
                Return False
            End If


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 入力チェック用配列セット・入力項目必須チェック、入力項目重複チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェック用の配列をExcelからセットする
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetArryInputForCheck(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim xlApp As Object = Nothing       'Applicationオブジェクト
        Dim xlBooks As Object = Nothing     'Workbooksオブジェクト
        Dim xlBook As Object = Nothing      'Workbookオブジェクト
        Dim xlSheets As Object = Nothing    'Worksheetsオブジェクト
        Dim xlSheet As Object = Nothing     'Worksheetオブジェクト
        Dim xlRange As Object = Nothing     'Rangeオブジェクト
        Dim strBkNm As String               'OriginalBook名

        Dim strStatusConvetCD As String = ""                'ステータスコード変換用
        Dim intColCount As Integer = 0                      '項目数カウンタ
        Dim blnErrorFlg As Boolean = False                  '入力チェック用フラグ用
        aryClassTitle = New ArrayList                       '分類１、分類２、名称重複チェック用
        Dim strFreeFlg1 As String = ""                      'フリーフラグ１変換用
        Dim strFreeFlg2 As String = ""                      'フリーフラグ２変換用
        Dim strFreeFlg3 As String = ""                      'フリーフラグ３変換用
        Dim strFreeFlg4 As String = ""                      'フリーフラグ４変換用
        Dim strFreeFlg5 As String = ""                      'フリーフラグ５変換用

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ

        '保存用配列初期化
        With dataHBKB0203

            .PropAryRowCount = New ArrayList                '行番号
            .PropAryTorikomiNum = New ArrayList             '取込管理番号
            .PropAryNum = New ArrayList                     '番号（手動）
            .PropAryClass1 = New ArrayList                  '分類1
            .PropAryClass2 = New ArrayList                  '分類2
            .PropAryCINM = New ArrayList                    '名称
            .PropAryCIStatusCD = New ArrayList              'ステータス
            .PropAryCIOwnerCD = New ArrayList               'CIオーナー
            .PropAryCINaiyo = New ArrayList                 '説明
            .PropAryBIko1 = New ArrayList                   'フリーテキスト1
            .PropAryBIko2 = New ArrayList                   'フリーテキスト2
            .PropAryBIko3 = New ArrayList                   'フリーテキスト3
            .PropAryBIko4 = New ArrayList                   'フリーテキスト4
            .PropAryBIko5 = New ArrayList                   'フリーテキスト5
            .PropAryFreeFlg1 = New ArrayList                'フリーフラグ1
            .PropAryFreeFlg2 = New ArrayList                'フリーフラグ2
            .PropAryFreeFlg3 = New ArrayList                'フリーフラグ3
            .PropAryFreeFlg4 = New ArrayList                'フリーフラグ4
            .PropAryFreeFlg5 = New ArrayList                'フリーフラグ5
            .PropAryVersion = New ArrayList                 '版（手動）
            .PropAryCrateID = New ArrayList                 '作成者ID
            .PropAryCrateNM = New ArrayList                 '作成者名
            .PropAryCreateDT = New ArrayList                '作成年月日
            .PropAryLastUpID = New ArrayList                '最終更新者ID
            .PropAryLastUpNM = New ArrayList                '最終更新者名
            .PropAryLastUpDT = New ArrayList                '最終更新日時
            .PropAryFilePath = New ArrayList                '取込ファイルパス
            .PropAryChargeID = New ArrayList                '文書責任者ID
            .PropAryChargeNM = New ArrayList                '文書責任者名
            .PropAryShareteamNM = New ArrayList             '文書配布先
            .PropAryOfferNM = New ArrayList                 '文書提供者
            .PropAryDelDT = New ArrayList                   '文書廃棄年月日
            .PropAryDelReason = New ArrayList               '文書廃棄理由

        End With

        Try
            'ファイルを開く
            xlApp = CreateObject("Excel.Application")

            'Workbook取得
            xlBooks = xlApp.Workbooks

            '取込ファイルを開く
            xlBook = xlBooks.Open(DataHBKB0203.PropStrFilePath)

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
            xlSheet = CType(xlBook.Sheets(1), Excel.Worksheet)

            '取込ファイル項目数チェック
            intColCount = 0 'カウンタ初期化
            For i As Integer = 0 To strColNm.Length Step 1
                If Convert.ToString(xlSheet.Cells(EXL_START_ROW, EXL_ACQUISITION_NUM + i).Value) <> "" Then
                    intColCount = intColCount + 1
                End If
            Next
            'カウンタと列数が等しくない場合エラー
            If intColCount <> strColNm.Length Then
                strOutLog &= B0203_E009
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                'エクセルを閉じる
                xlApp.Quit()
                Return True
            End If

            '取込番号入力チェック
            If Convert.ToString(xlSheet.Cells(EXL_START_ROW + 1, EXL_ACQUISITION_NUM).Value) = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E002, (EXL_START_ROW + 1).ToString, strColNm(EXL_ACQUISITION_NUM - 1)) & vbCrLf
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                'エクセルを閉じる
                xlApp.Quit()
                Return True
            End If

            'コネクションを開く
            Cn.Open()

            'ループ
            Dim Count As Integer = EXL_START_ROW + 1
            While (True)
                '取込番号入力チェック
                If Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value) = "" Then
                    '処理を抜ける
                    Exit While
                End If
                '番号（手動）～ステータスまでは前行の取込番号が違う場合チェックする

                '番号入力チェック
                If CheckNumLength(dataHBKB0203, Count, Convert.ToString(xlSheet.Cells(Count, EXL_NUM).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '分類1＋分類２＋名称の入力チェック
                If CehckInputGroupAndName(dataHBKB0203, Adapter, Cn, Count, _
                                                        Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value), _
                                                        Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value), _
                                                        Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '変換ステータスCD初期化
                strStatusConvetCD = ""

                'ステータスの入力チェック
                If CehckInputStatus(dataHBKB0203, Adapter, Cn, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_STATUS).Value), strStatusConvetCD) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'CIオーナーCDの存在チェック
                If CheckInputCIOwner(dataHBKB0203, Adapter, Cn, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_CIOWNER_CD).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '説明、フリーテキスト1～5桁数チェック
                If ChekuInputLength(dataHBKB0203, Count, Convert.ToString(xlSheet.Cells(Count, EXL_EXPLANATION).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_1).Value), Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_2).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_3).Value), Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_4).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_5).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'フリーフラグ格納
                strFreeFlg1 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_1).Value)
                strFreeFlg2 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_2).Value)
                strFreeFlg3 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_3).Value)
                strFreeFlg4 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_4).Value)
                strFreeFlg5 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_5).Value)

                'フリーフラグ１～フリーフラグ５の形式チェック
                If CheckInputForm(dataHBKB0203, Count, strFreeFlg1, strFreeFlg2, strFreeFlg3, strFreeFlg4, strFreeFlg5) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '版（手動）、作成者ID、作成者名、最終更新者ID、最終更新者桁数チェック、作成年月日、最終更新日時形式チェック
                If ChekuInputLength_User(dataHBKB0203, Count, Convert.ToString(xlSheet.Cells(Count, EXL_VERSION).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_CRATEID).Value), Convert.ToString(xlSheet.Cells(Count, EXL_CRATENM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPID).Value), Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPNM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_CREATEDT).Value), Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPDT).Value)
                                    ) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'ファイル存在チェック
                If CheckInputExistenceFile(dataHBKB0203, Count, Convert.ToString(xlSheet.Cells(Count, EXL_FILEPATH).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '文書責任者ID、文書責任者名、文書配付先、文書提供者、文書廃棄理由桁数チェック、文書廃棄年月日、ファイル名形式チェック
                If ChekuInputLength_Doc(dataHBKB0203, Count, Convert.ToString(xlSheet.Cells(Count, EXL_CHARGEID).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_CHARGENM).Value), Convert.ToString(xlSheet.Cells(Count, EXL_SHARETEAMNM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_OFFERNM).Value), Convert.ToString(xlSheet.Cells(Count, EXL_DELREASON).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_DELDT).Value), Convert.ToString(xlSheet.Cells(Count, EXL_FILEPATH).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'データクラスに保存
                With dataHBKB0203
                    .PropAryRowCount.Add(Count)                                                                                     '行番号
                    .PropAryTorikomiNum.Add(Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value))                      '取込管理番号
                    .PropAryNum.Add(Convert.ToString(xlSheet.Cells(Count, EXL_NUM).Value))                                          '番号（手動）
                    .PropAryClass1.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value))                                '分類１
                    .PropAryClass2.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value))                                '分類２
                    .PropAryCINM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value))                                       'タイトル
                    .PropAryCIStatusCD.Add(strStatusConvetCD)                                                                       'ステータス
                    .PropAryCIOwnerCD.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CIOWNER_CD).Value))                             'CIオーナー
                    .PropAryCINaiyo.Add(Convert.ToString(xlSheet.Cells(Count, EXL_EXPLANATION).Value))                              '説明
                    .PropAryBIko1.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_1).Value))                                'フリーテキスト1
                    .PropAryBIko2.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_2).Value))                                'フリーテキスト2
                    .PropAryBIko3.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_3).Value))                                'フリーテキスト3
                    .PropAryBIko4.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_4).Value))                                'フリーテキスト4
                    .PropAryBIko5.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_5).Value))                                'フリーテキスト5
                    .PropAryFreeFlg1.Add(strFreeFlg1)                                                                               'フリーフラグ1
                    .PropAryFreeFlg2.Add(strFreeFlg2)                                                                               'フリーフラグ2
                    .PropAryFreeFlg3.Add(strFreeFlg3)                                                                               'フリーフラグ3
                    .PropAryFreeFlg4.Add(strFreeFlg4)                                                                               'フリーフラグ4
                    .PropAryFreeFlg5.Add(strFreeFlg5)                                                                               'フリーフラグ5
                    .PropAryVersion.Add(Convert.ToString(xlSheet.Cells(Count, EXL_VERSION).Value))                                  '版（手動）
                    .PropAryCrateID.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CRATEID).Value))                                  '作成者ID
                    .PropAryCrateNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CRATENM).Value))                                  '作成者名
                    .PropAryCreateDT.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CREATEDT).Value))                                '作成年月日
                    .PropAryLastUpID.Add(Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPID).Value))                                '最終更新者ID
                    .PropAryLastUpNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPNM).Value))                                '最終更新者名
                    .PropAryLastUpDT.Add(Convert.ToString(xlSheet.Cells(Count, EXL_LASTUPDT).Value))                                '最終更新日時
                    .PropAryFilePath.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FILEPATH).Value))                                '取込ファイルパス
                    .PropAryChargeID.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CHARGEID).Value))                                '文書責任者ID
                    .PropAryChargeNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CHARGENM).Value))                                '文書責任者名
                    .PropAryShareteamNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SHARETEAMNM).Value))                          '文書配布先
                    .PropAryOfferNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_OFFERNM).Value))                                  '文書提供者
                    .PropAryDelDT.Add(Convert.ToString(xlSheet.Cells(Count, EXL_DELDT).Value))                                      '文書廃棄年月日
                    .PropAryDelReason.Add(Convert.ToString(xlSheet.Cells(Count, EXL_DELREASON).Value))                              '文書廃棄理由

                    '分類１+分類２＋名称重複チェック用
                    aryClassTitle.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value) & _
                    Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value) & _
                    Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value))

                End With
                'カウンタインクリメント
                Count += 1

            End While

            '改行コード変換処理
            If ChangeToVbCrLfForBunsyo(dataHBKB0203) = False Then
                Return False
            End If

            '保存しないで閉じる 
            xlBook.Close(SaveChanges:=False)

            'エクセルを閉じる
            xlApp.Quit()

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            'フラグによって戻り値を設定する
            If blnErrorFlg = True Then
                Return False
            Else
                '正常終了
                Return True
            End If

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)

            If Not xlApp Is Nothing Then
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                xlApp.Quit()                           'Excelを閉じる
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'オブジェクト解放
            Adapter.Dispose()
            Cn.Dispose()
            '終了処理
            CommonLogic.MRComObject(xlSheet)       'xlSheetの解放
            CommonLogic.MRComObject(xlBook)        'xlBookの解放
            CommonLogic.MRComObject(xlBooks)       'xlBooksの解放
            CommonLogic.MRComObject(xlApp)         'xlAppの解放
            CommonLogic.MRComObject(xlRange)       'xlRangeの解放

        End Try
    End Function

    ''' <summary>
    ''' 番号入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strNum">番号</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>番号の入力チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckNumLength(ByRef dataHBKB0203 As DataHBKB0203, ByRef intIndex As Integer, ByRef strNum As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '番号（手動）（50文字まで）
            If strNum.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_NUM - 1)) & vbCrLf
            End If


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 分類１、分類２、名称の入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strGroup1">分類１</param>
    ''' <param name="strGroup2">分類２</param>
    ''' <param name="strName">名称</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>分類１、分類２、名称の入力チェック、桁数チェック、ファイル内重複チェック、DB重複チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckInputGroupAndName(ByRef dataHBKB0203 As DataHBKB0203, ByVal Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strGroup1 As String, _
                                                                ByRef strGroup2 As String, ByRef strName As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数登録
        Dim blnGroup1Flg As Boolean = False                 '分類１入力チェックフラグ
        Dim blnGroup2Flg As Boolean = False                 '分類２入力チェックフラグ
        Dim blnNameFlg As Boolean = False                   '名称入力チェックフラグ
        Dim blnRepetitionFlg As Boolean = False             '分類１＋分類２＋名称入力チェックフラグ

        Try
            '分類１の入力チェック
            If strGroup1 = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E002, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
                'エラーフラグセット
                blnGroup1Flg = True
            Else
                '桁数チェック
                If strGroup1.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
                    'エラーフラグセット
                    blnGroup1Flg = True
                End If
            End If

            '分類２の入力チェック
            If strGroup2 = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E002, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
                'エラーフラグセット
                blnGroup2Flg = True
            Else
                '桁数チェック
                If strGroup2.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
                    'エラーフラグセット
                    blnGroup2Flg = True
                End If
            End If

            '名称の入力チェック
            If strName = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E002, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
                'エラーフラグセット
                blnNameFlg = True
            Else
                '桁数チェック[Mod] 2012/08/02 y.ikushima 桁数を1000文字から100文字へ
                If strName.Length > 100 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
                    'エラーフラグセット
                    blnNameFlg = True
                End If
            End If

            'フラグチェック（３つのチェックのうち一つでも入力エラーがある場合、重複チェックエラーフラグをセットする
            If blnGroup1Flg = True Or blnGroup2Flg = True Or blnNameFlg = True Then
                blnRepetitionFlg = True
            End If

            '分類1＋分類2＋名称のファイル内重複チェック
            If blnRepetitionFlg = False Then
                If aryClassTitle.Contains(strGroup1 & strGroup2 & strName) = True Then
                    '同じ要素がある場合
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E005, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1) & "," _
                                               & strColNm(EXL_GROUPING_2 - 1) & "," & strColNm(EXL_TITLE - 1)) & vbCrLf
                    blnRepetitionFlg = True
                End If
            End If

            '分類1＋分類2＋名称のDB内重複チェック
            If blnRepetitionFlg = False Then
                If CheckRepetitionClassTitle(Adapter, Cn, dataHBKB0203, intIndex.ToString, strGroup1, strGroup2, strName) = False Then
                    Return False
                End If
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' ステータス入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <param name="strStatusConvetCD">ステータス変換文字列</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ステータスの必須チェック、存在チェックを行い、コードへ変換する
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckInputStatus(ByRef dataHBKB0203 As DataHBKB0203, ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strStatus As String, _
                                                  ByRef strStatusConvetCD As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ステータス入力チェック
            If strStatus = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E002, intIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
            Else
                'ステータスのDB存在チェック(ステータスコードを変換）
                If CheckStatusConvert(Adapter, Cn, dataHBKB0203, intIndex.ToString, strStatus, strStatusConvetCD) = False Then
                    Return False
                End If
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' ステータスコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <param name="strStatusConvetCD">ステータス変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたステータス名をCIステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 :2012/07/30 y.ikushima </p>
    ''' </para></remarks>
    Public Function CheckStatusConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByRef dataHBKB0203 As DataHBKB0203, _
                                                      ByRef IntIndex As Integer, _
                                                      ByRef strStatus As String, ByRef strStatusConvetCD As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ステータスコードのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0203.SetSelectCountCIStateCDSql(Adapter, Cn, dataHBKB0203, strStatus) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスコードのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0203
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strStatusConvetCD = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0203_E006, IntIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
                End If
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
        Finally
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' CIオーナー入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strCIOwner">CIオーナーCD</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目の形式チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputCIOwner(ByRef dataHBKB0203 As DataHBKB0203, ByVal Adapter As NpgsqlDataAdapter, _
                                                      ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strCIOwner As String) As Boolean
        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数登録
        Dim blnCIOwnerFlg As Boolean = False                 ''CIオーナーコード入力チェックフラグ

        Try

            'CIオーナーコードの桁数チェック
            If strCIOwner <> "" Then
                If strCIOwner.Length > 3 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CIOWNER_CD - 1)) & vbCrLf
                    'エラーフラグ設定
                    blnCIOwnerFlg = True
                End If
            Else
                blnCIOwnerFlg = True
            End If

            'グループマスタからCIオーナーコード存在チェック
            If blnCIOwnerFlg = False Then
                If CheckRelationIDForGroup(Adapter, Cn, dataHBKB0203, intIndex, strCIOwner, strColNm(EXL_CIOWNER_CD - 1)) = False Then
                    Return False
                End If
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 入力項目桁数チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strExplanation">説明</param>
    ''' <param name="strFreeText1">フリーテキスト1</param>
    ''' <param name="strFreeText2">フリーテキスト2</param>
    ''' <param name="strFreeText3">フリーテキスト3</param>
    ''' <param name="strFreeText4">フリーテキスト4</param>
    ''' <param name="strFreeText5">フリーテキスト5</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>説明、フリーテキスト１～５、情報共有先の桁数チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function ChekuInputLength(ByRef dataHBKB0203 As DataHBKB0203, ByRef intIndex As Integer, _
                                                    ByRef strExplanation As String, ByRef strFreeText1 As String, ByRef strFreeText2 As String, _
                                                    ByRef strFreeText3 As String, ByRef strFreeText4 As String, ByRef strFreeText5 As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '説明
            If strExplanation.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_EXPLANATION - 1)) & vbCrLf
            End If
            'フリーテキスト１
            If strFreeText1.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_1 - 1)) & vbCrLf
            End If
            'フリーテキスト２
            If strFreeText2.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_2 - 1)) & vbCrLf
            End If
            'フリーテキスト３
            If strFreeText3.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_3 - 1)) & vbCrLf
            End If
            'フリーテキスト４
            If strFreeText4.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_4 - 1)) & vbCrLf
            End If
            'フリーテキスト５
            If strFreeText5.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_5 - 1)) & vbCrLf
            End If


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 入力項目桁数チェック処理(ユーザ項目）
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strVersion">版（手動）</param>
    ''' <param name="strCrateID">作成者ID</param>
    ''' <param name="strCrateNM">作成者名</param>
    ''' <param name="strLastupID">最終更新者ID</param>
    ''' <param name="strLastupNM">最終更新者名</param>
    ''' <param name="strCreateDT">作成年月日</param>
    ''' <param name="strLastupDT">最終更新日時</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>版（手動）、作成者ID、作成者名、最終更新者ID、最終更新者の桁数チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function ChekuInputLength_User(ByRef dataHBKB0203 As DataHBKB0203, ByRef intIndex As Integer, _
                                                    ByRef strVersion As String, ByRef strCrateID As String, ByRef strCrateNM As String, _
                                                    ByRef strLastupID As String, ByRef strLastupNM As String, _
                                                    ByRef strCreateDT As String, ByRef strLastupDT As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '版（手動）
            If strVersion.Length > 10 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_VERSION - 1)) & vbCrLf
            End If

            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 START
            '作成者ID
            If strCrateID.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CRATEID - 1)) & vbCrLf
            End If
            ''作成者ID
            'If strCrateID.Length > 25 Then
            '    'メッセージログ設定
            '    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CRATEID - 1)) & vbCrLf
            'End If
            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 END

            '作成者名
            If strCrateNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CRATENM - 1)) & vbCrLf
            End If

            '作成年月日
            If strCreateDT <> "" Then
                'YYYY/MM/DDまたはYYYYMMDD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strCreateDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strCreateDT) = False Then
                        'メッセージログ設定
                        strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_CREATEDT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_CREATEDT - 1)) & vbCrLf
                End If
            End If

            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 START
            '最終更新者ID
            If strLastupID.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_LASTUPID - 1)) & vbCrLf
            End If
            ''最終更新者ID
            'If strLastupID.Length > 25 Then
            '    'メッセージログ設定
            '    strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_LASTUPID - 1)) & vbCrLf
            'End If
            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 END

            '最終更新者
            If strLastupNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_LASTUPNM - 1)) & vbCrLf
            End If

            '最終更新日時
            If strLastupDT <> "" Then
                'YYYY/MM/DDまたはYYYYMMDD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strLastupDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strLastupDT) = False Then
                        'メッセージログ設定
                        strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_LASTUPDT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_LASTUPDT - 1)) & vbCrLf
                End If
            End If


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 入力項目桁数チェック処理(文章項目）
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strVersion">版（手動）</param>
    ''' <param name="strCrateID">作成者ID</param>
    ''' <param name="strCrateNM">作成者名</param>
    ''' <param name="strLastupID">最終更新者ID</param>
    ''' <param name="strLastupNM">最終更新者名</param>
    ''' <param name="strDelDT">文書廃棄年月日</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>版（手動）、作成者ID、作成者名、最終更新者ID、最終更新者の桁数チェック、文書廃棄年月日の形式チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function ChekuInputLength_Doc(ByRef dataHBKB0203 As DataHBKB0203, ByRef intIndex As Integer, _
                                                    ByRef strVersion As String, ByRef strCrateID As String, ByRef strCrateNM As String, _
                                                    ByRef strLastupID As String, ByRef strLastupNM As String, ByRef strDelDT As String, ByRef strFileNM As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '版（手動）
            If strVersion.Length > 10 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_VERSION - 1)) & vbCrLf
            End If
            '作成者ID
            If strCrateID.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CRATEID - 1)) & vbCrLf
            End If
            '作成者名
            If strCrateNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_CRATENM - 1)) & vbCrLf
            End If
            '最終更新者ID
            If strLastupID.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_LASTUPID - 1)) & vbCrLf
            End If
            '文書廃棄年月日
            If strDelDT <> "" Then
                'YYYY/MM/DDまたはYYYYMMDD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strDelDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strDelDT) = False Then
                        'メッセージログ設定
                        strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_DELDT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_DELDT - 1)) & vbCrLf
                End If
            End If

            '最終更新者
            If strLastupNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_LASTUPNM - 1)) & vbCrLf
            End If

            'ファイル名桁数チェック
            If Path.GetFileName(strFileNM).Length > 174 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0203_E003, intIndex.ToString, strColNm(EXL_FILEPATH - 1)) & vbCrLf
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' フリーフラグ形式チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>フリーフラグの形式チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByRef dataHBKB0203 As DataHBKB0203, ByRef intIndex As Integer, ByRef FreeFlg1 As String, _
                                                  ByRef FreeFlg2 As String, ByRef FreeFlg3 As String, ByRef FreeFlg4 As String, ByRef FreeFlg5 As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'フリーフラグ１
            If FreeFlg1 = "" Then
                '入力がない場合はOFF
                FreeFlg1 = FREE_FLG_OFF
            Else
                '入力がある場合
                If FreeFlg1 = FREE_FLG_ON_NM Then
                    FreeFlg1 = FREE_FLG_ON
                ElseIf FreeFlg1 = FREE_FLG_OFF_NM Then
                    FreeFlg1 = FREE_FLG_OFF
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_1 - 1)) & vbCrLf
                    '空白を設定
                    FreeFlg1 = ""
                End If

            End If
            'フリーフラグ２
            If FreeFlg2 = "" Then
                '入力がない場合はOFF
                FreeFlg2 = FREE_FLG_OFF
            Else
                '入力がある場合
                If FreeFlg2 = FREE_FLG_ON_NM Then
                    FreeFlg2 = FREE_FLG_ON
                ElseIf FreeFlg2 = FREE_FLG_OFF_NM Then
                    FreeFlg2 = FREE_FLG_OFF
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_2 - 1)) & vbCrLf
                    '空文字を設定
                    FreeFlg2 = ""
                End If
            End If
            'フリーフラグ３
            If FreeFlg3 = "" Then
                '入力がない場合はOFF
                FreeFlg3 = FREE_FLG_OFF
            Else
                '入力がある場合
                If FreeFlg3 = FREE_FLG_ON_NM Then
                    FreeFlg3 = FREE_FLG_ON
                ElseIf FreeFlg3 = FREE_FLG_OFF_NM Then
                    FreeFlg3 = FREE_FLG_OFF
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_3 - 1)) & vbCrLf
                    '空文字を設定
                    FreeFlg3 = ""
                End If
            End If
            'フリーフラグ４
            If FreeFlg4 = "" Then
                '入力がない場合はOFF
                FreeFlg4 = FREE_FLG_OFF
            Else
                '入力がある場合
                If FreeFlg4 = FREE_FLG_ON_NM Then
                    FreeFlg4 = FREE_FLG_ON
                ElseIf FreeFlg4 = FREE_FLG_OFF_NM Then
                    FreeFlg4 = FREE_FLG_OFF
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_4 - 1)) & vbCrLf
                    '空文字を設定
                    FreeFlg4 = ""
                End If
            End If
            'フリーフラグ５
            If FreeFlg5 = "" Then
                '入力がない場合はOFF
                FreeFlg5 = FREE_FLG_OFF
            Else
                '入力がある場合
                If FreeFlg5 = FREE_FLG_ON_NM Then
                    FreeFlg5 = FREE_FLG_ON
                ElseIf FreeFlg5 = FREE_FLG_OFF_NM Then
                    FreeFlg5 = FREE_FLG_OFF
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_5 - 1)) & vbCrLf
                    '空白を設定
                    FreeFlg5 = ""
                End If
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' 分類１DB+分類２DB+名称DB重複チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strGroup1">[IN]分類１</param>
    ''' <param name="strGroup2">[IN]分類２</param>
    ''' <param name="strName">[IN]名称</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>分類１DB+分類２DB+名称DB重複チェックをCI共通情報テーブルからデータを検索し重複チェックを行う
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報：2012/07/25 y.ikushima</p>
    ''' </para></remarks>
    Public Function CheckRepetitionClassTitle(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0203 As DataHBKB0203, _
                                 ByRef IntIndex As Integer, ByRef strGroup1 As String, _
                                 ByRef strGroup2 As String, ByRef strName As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            '分類１、分類２、名称のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0203.SetSelectCountSameKeySql(Adapter, Cn, dataHBKB0203, strGroup1, strGroup2, strName) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "分類１、分類２、名称のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            '重複データがある場合、エラー
            If dtResult.Rows(0).Item(0) > 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(B0203_E007, IntIndex.ToString, strColNm(EXL_GROUPING_1 - 1) & "," & strColNm(EXL_GROUPING_2 - 1) & "," & strColNm(EXL_TITLE - 1)) & vbCrLf
            End If

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
        Finally
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' CIオーナーコード、関係者ID存在チェック処理（グループ）
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strSearchID">[IN]検索用文字列</param>
    ''' <param name="strMessage">[IN]エラーログ用列名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたCIオーナーコード、関係者IDをグループマスタからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckRelationIDForGroup(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0203 As DataHBKB0203, _
                                 ByRef IntIndex As Integer, _
                                 ByRef strSearchID As String, _
                                 ByRef strMessage As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '検索IDセット
            dataHBKB0203.PropStrGroupCD = strSearchID

            '関係者IDのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0203.SetSelectRelationIDForGroup(Adapter, Cn, dataHBKB0203, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタから関係者IDのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            'データが存在しない場合、エラー
            If dtResult.Rows(0).Item(0) = 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(B0203_E006, IntIndex.ToString, strMessage) & vbCrLf
            End If

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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 入力項目のファイル存在チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目のファイル存在チェックを行う
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 :2012/07/30 y.ikushima</p>
    ''' </para></remarks>
    Public Function CheckInputExistenceFile(ByRef dataHBKB0203 As DataHBKB0203, ByVal intindex As Integer, _
                                           ByRef strFailePath As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            If strFailePath <> "" Then
                If System.IO.File.Exists(strFailePath) = False Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0203_E008, intindex.ToString, strColNm(EXL_FILEPATH - 1)) & vbCrLf
                End If
            End If

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try
    End Function

    ''' <summary>
    ''' エラーログ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックでエラーとなった内容をログ出力する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetOutLog(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strLogFilePath As String = Nothing                                      'ログファイルパス
        Dim strLogFileName As String = Nothing                                      'ログファイル名
        Dim strOutputDir As String = Nothing                                        'ログ出力フォルダ
        Dim stwWriteLog As System.IO.StreamWriter = Nothing                         'ファイル書込用クラス
        Dim strOutputpath As String = Nothing                                       '出力ファイル名

        Try
            'もしログ出力内容が存在しない状態で遷移してきた場合
            If strOutLog <> "" Then

                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_IMPORTERRLOG)

                'ログファイル名設定
                strLogFileName = Now.ToString("yyyyMMddHHmmss") & ".log"

                'ディレクトリ作成
                System.IO.Directory.CreateDirectory(strOutputDir)

                strOutputpath = Path.Combine(strOutputDir, strLogFileName)

                '書き込みファイル指定(Falseで新規作成）
                stwWriteLog = New System.IO.StreamWriter(strOutputpath, False, System.Text.Encoding.GetEncoding("Shift_JIS"))

                'ファイル書き込み
                stwWriteLog.WriteLine(strOutLog)

                'フラッシュ（出力）
                stwWriteLog.Flush()

                'ファイルクローズ
                stwWriteLog.Close()

                'エラーメッセージをセット
                puErrMsg = String.Format(B0203_E001, strOutputpath)
            Else

                'エラーメッセージをセット
                puErrMsg = HBK_E001
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If stwWriteLog IsNot Nothing Then
                stwWriteLog.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            If stwWriteLog IsNot Nothing Then
                stwWriteLog.Dispose()
            End If
        End Try
    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>登録処理を行う
    ''' <para>作成情報：2012/07/20 s.tsuruta 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegMain(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力データ登録処理
        If FileInputDataReg(dataHBKB0203) = False Then
            Return False
        End If

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 入力データ登録処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力データの登録処理を行う
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputDataReg(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)                                                                'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing                                                                  'トランザクション
        Dim aryCINmb As New ArrayList                                                                           'CI番号保存用
        Dim blnErrorFlg As Boolean = False                                                                      'エラーフラグ

        Try

            '履歴Noを１で固定
            dataHBKB0203.PropIntRirekiNo = 1

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKB0203
                '取込番号分ループ
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1

                    '1週目の場合
                    '新規CI番号取得
                    If SelectNewCINmb(Cn, dataHBKB0203) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI共通情報新規追加
                    If InsertCIInfo(Cn, dataHBKB0203, i) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI共通情報履歴情報新規追加
                    If InsertCIINfoR(Cn, dataHBKB0203) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '取込ファイルパス未入力時には、ファイル番号新規採番、新規登録を行わない
                    If dataHBKB0203.PropAryFilePath(i) <> "" Then
                        '新規ファイル番号採番
                        If SelectNewFileMngNmb(Cn, dataHBKB0203) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            Return False
                        End If

                        'ファイル管理テーブル新規登録
                        If InsertFileMng(Cn, dataHBKB0203, i) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            Return False
                        End If
                    Else
                        'ファイル番号初期化＝０
                        dataHBKB0203.PropIntFileMngNmb = Nothing
                    End If


                    'CI文書情報新規追加
                    If InsertCIDoc(Cn, dataHBKB0203, i) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI文書履歴情報新規追加
                    If InsertCIDocR(Cn, dataHBKB0203) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '登録理由履歴新規追加
                    If InsertRegReasonR(Cn, dataHBKB0203) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '原因リンク履歴情報新規追加
                    If InsertCauseLinkR(Cn, dataHBKB0203) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '取込ファイルパスが入力されているか
                    If Not (dataHBKB0203.PropAryFilePath(i) = "") Then
                        'ファイルアップロード処理
                        If FileUpLoad(dataHBKB0203, i) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            Return False
                        End If
                    End If

                Next

            End With

            'エラーフラグがONの場合、Falseを返す
            If blnErrorFlg = False Then
                'コミット
                Tsx.Commit()
            Else
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            '終了処理
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 新規CI番号、システム日付取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0203.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0203) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0203.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB0203.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0203_E010
                Return False
            End If

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
        Finally
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 新規ファイル管理番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="datahbkb0203">[IN]一括登録文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したファイル管理番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewFileMngNmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef datahbkb0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規ファイル管理番号取得（SELECT）用SQLを作成
            If sqlHBKB0203.SetSelectNewFileMngNmbSql(Adapter, Cn, datahbkb0203) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ファイル番号取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                DataHBKB0203.PropIntFileMngNmb = dtResult.Rows(0).Item("FileMngNmb")      '新規ファイル番号
            Else
                '取得できなかったときはエラー
                puErrMsg = B0203_E010
                Return False
            End If

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
        Finally
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' CI共通情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0203 As DataHBKB0203, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0203.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0203, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイル管理テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をファイル管理テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFileMng(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0203 As DataHBKB0203, _
                                  ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ファイル管理テーブル新規登録（INSERT）用SQLを作成
            If sqlHBKB0203.SetInsertFileMngSql(Cmd, Cn, dataHBKB0203, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ファイル管理テーブル新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI文書テーブル新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI文書テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIDoc(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0203 As DataHBKB0203, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI文書新規登録（INSERT）用SQLを作成
            If sqlHBKB0203.SetInsertCIDocSql(Cmd, Cn, dataHBKB0203, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0203.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0203) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI文書履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI文書履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIDocR(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0203.SetInsertCIDocRSql(Cmd, Cn, dataHBKB0203) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI文書履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 登録理由履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0203.SetInsertRegReasonRSql(Cmd, Cn, DataHBKB0203) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "登録理由履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()


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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 原因リンク履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0203 As DataHBKB0203) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0203.PropDtCauseLink.Rows.Count - 1
                '登録条件セット
                dataHBKB0203.PropStrMngNmb = dataHBKB0203.PropDtCauseLink.Rows(i).Item("MngNmb")
                dataHBKB0203.PropStrProcessKbn = dataHBKB0203.PropDtCauseLink.Rows(i).Item("ProcessKbn")
                'SQLを作成
                If sqlHBKB0203.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0203) = False Then
                    Return False
                End If

                'ログ出力
                CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンク履歴新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

            Next


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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ファイルアップロード処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN]dataHBKB0203クラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/20 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileUpLoad(ByVal dataHBKB0203 As DataHBKB0203, _
                               ByVal intindex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim strSystemDirpath As String
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名
        Dim strFileName As String = ""           '出力
        'プロセスクラスの宣言
        Dim p As Process = Nothing                              'プロセスクラス
        Dim psi As New System.Diagnostics.ProcessStartInfo()    'プロセススタートインフォクラス

        Try
            ''登録ファイルパス取得
            'strFilePath = dataHBKB0203.PropAryFilePath(intindex)

            ''登録先パス
            'strSystemDirpath = Path.Combine(PropFileStorageRootPath, PropFileManagePath, OUTPUT_FILE_DIR_DOC)
            'strSystemDirpath = strSystemDirpath & "\" & dataHBKB0203.PropIntCINmb & "\" & dataHBKB0203.PropIntFileMngNmb

            ''コピー先ディレクトリ存在チェック
            'If Directory.Exists(strSystemDirpath) = False Then
            '    'コピー先ディレクトリが見つからない場合は作成
            '    Directory.CreateDirectory(strSystemDirpath)
            'End If

            ''ファイルパスとファイル名の結合
            'strSystemDirpath = strSystemDirpath & "\" & Path.GetFileName(strFilePath)

            ''ファイルコピー　※同名のファイルがあった場合は上書きする
            'System.IO.File.Copy(strFilePath, strSystemDirpath, True)

            '登録ファイルパス取得
            strFilePath = dataHBKB0203.PropAryFilePath(intindex)

            'PCの論理ドライブ名をすべて取得する
            Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
            '利用可能な論理ドライブ名を取得する
            For Each strDrive As String In DRIVES
                If strDrives.Contains(strDrive) = False Then
                    strDriveName = strDrive.Substring(0, 2)
                    Exit For
                End If
            Next

            psi.FileName = System.Environment.GetEnvironmentVariable("ComSpec")

            '出力を読み取れるようにする
            psi.RedirectStandardInput = False
            psi.RedirectStandardOutput = True
            psi.UseShellExecute = False
            'ウィンドウを非表示にする
            psi.CreateNoWindow = True

            'コマンドの設定
            strCmd = "/C net use " & strDriveName & " " & PropFileStorageRootPath & " " & NET_USE_PASSWORD & " /user:" & NET_USE_USERID

            psi.Arguments = strCmd
            p = Process.Start(psi)
            p.WaitForExit()

            'アップロード先のディレクトリセット
            strSystemDirpath = Path.Combine(strDriveName, PropFileManagePath, OUTPUT_FILE_DIR_DOC)
            strSystemDirpath = strSystemDirpath & "\" & dataHBKB0203.PropIntCINmb & "\" & dataHBKB0203.PropIntFileMngNmb

            'コピー先ディレクトリ存在チェック
            If Directory.Exists(strSystemDirpath) = False Then
                'コピー先ディレクトリが見つからない場合は作成
                Directory.CreateDirectory(strSystemDirpath)
            End If

            'ファイル存在チェック
            If System.IO.File.Exists(strFilePath) Then
                'ファイルのコピー
                FileCopy(strFilePath, strSystemDirpath & "\" & Path.GetFileName(strFilePath))
            End If

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
        Finally
            '接続の解除
            strCmd = "/C net use " & strDriveName & " /delete /y"
            psi.Arguments = strCmd
            p = Process.Start(psi)
        End Try

    End Function

    ''' <summary>
    ''' 改行コード変換処理
    ''' </summary>
    ''' <param name="dataHBKB0203">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>取込データの改行コードを変換する
    ''' <para>作成情報：2012/09/21 s.yamaguchi 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeToVbCrLfForBunsyo(ByRef dataHBKB0203 As DataHBKB0203) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0203

                '改行コードを再設定
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1

                    .PropAryTorikomiNum(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryTorikomiNum(i))   '取込管理番号
                    .PropAryNum(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryNum(i))                   '番号（手動）
                    .PropAryClass1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryClass1(i))             '分類１
                    .PropAryClass2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryClass2(i))             '分類２
                    .PropAryCINM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCINM(i))                 'タイトル
                    .PropAryCIStatusCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCIStatusCD(i))     'ステータス
                    .PropAryCIOwnerCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCIOwnerCD(i))       'CIオーナー
                    .PropAryCINaiyo(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCINaiyo(i))           '説明
                    .PropAryBIko1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko1(i))               'フリーテキスト1
                    .PropAryBIko2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko2(i))               'フリーテキスト2
                    .PropAryBIko3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko3(i))               'フリーテキスト3
                    .PropAryBIko4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko4(i))               'フリーテキスト4
                    .PropAryBIko5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko5(i))               'フリーテキスト5
                    .PropAryFreeFlg1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg1(i))         'フリーフラグ1
                    .PropAryFreeFlg2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg2(i))         'フリーフラグ2
                    .PropAryFreeFlg3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg3(i))         'フリーフラグ3
                    .PropAryFreeFlg4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg4(i))         'フリーフラグ4
                    .PropAryFreeFlg5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg5(i))         'フリーフラグ5
                    .PropAryVersion(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryVersion(i))           '版（手動）
                    .PropAryCrateID(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCrateID(i))           '作成者ID
                    .PropAryCrateNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCrateNM(i))           '作成者名
                    .PropAryCreateDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCreateDT(i))         '作成年月日
                    .PropAryLastUpID(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryLastUpID(i))         '最終更新者ID
                    .PropAryLastUpNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryLastUpNM(i))         '最終更新者名
                    .PropAryLastUpDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryLastUpDT(i))         '最終更新日時
                    .PropAryFilePath(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFilePath(i))         '取込ファイルパス
                    .PropAryChargeID(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryChargeID(i))         '文書責任者ID
                    .PropAryChargeNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryChargeNM(i))         '文書責任者名
                    .PropAryShareteamNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryShareteamNM(i))   '文書配布先
                    .PropAryOfferNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryOfferNM(i))           '文書提供者
                    .PropAryDelDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryDelDT(i))               '文書廃棄年月日
                    .PropAryDelReason(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryDelReason(i))       '文書廃棄理由

                Next

            End With

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
        End Try

    End Function


End Class
