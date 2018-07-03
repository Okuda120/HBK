Imports Common
Imports CommonHBK
Imports Npgsql
Imports Microsoft.Office.Interop
Imports System.IO

Public Class LogicHBKB0202

    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKB0202 As New SqlHBKB0202

    'Public定数宣言
    'Excelのスタート行
    Public Const EXL_START_ROW As Integer = 1
    'Excelの行をセット
    Public Const EXL_ACQUISITION_NUM As Integer = 1                                 '取込番号
    Public Const EXL_GROUPING_1 As Integer = 2                                      '分類1
    Public Const EXL_GROUPING_2 As Integer = 3                                      '分類2
    Public Const EXL_TITLE As Integer = 4                                           '名称
    Public Const EXL_STATUS As Integer = 5                                          'ステータス
    Public Const EXL_CIOWNER_CD As Integer = 6                                      'オーナーCD
    Public Const EXL_EXPLANATION As Integer = 7                                     '説明
    Public Const EXL_FREE_TEXT_1 As Integer = 8                                     'フリーテキスト1
    Public Const EXL_FREE_TEXT_2 As Integer = 9                                     'フリーテキスト2
    Public Const EXL_FREE_TEXT_3 As Integer = 10                                    'フリーテキスト3
    Public Const EXL_FREE_TEXT_4 As Integer = 11                                    'フリーテキスト4
    Public Const EXL_FREE_TEXT_5 As Integer = 12                                    'フリーテキスト5
    Public Const EXL_FREE_FLG_1 As Integer = 13                                     'フリーフラグ1
    Public Const EXL_FREE_FLG_2 As Integer = 14                                     'フリーフラグ2
    Public Const EXL_FREE_FLG_3 As Integer = 15                                     'フリーフラグ3
    Public Const EXL_FREE_FLG_4 As Integer = 16                                     'フリーフラグ4
    Public Const EXL_FREE_FLG_5 As Integer = 17                                     'フリーフラグ5
    Public Const EXL_INFO_SHAR As Integer = 18                                      '情報共有先
    Public Const EXL_KNOWHOW_URL As Integer = 19                                    'ノウハウURL
    Public Const EXL_KNOWHOW_EXPLANATION As Integer = 20                            'ノウハウURL説明
    Public Const EXL_SERVERMANAGER_NUM As Integer = 21                              'サーバー管理番号
    Public Const EXL_SERVERMANAGER_EXPLANATION As Integer = 22                      'サーバー管理番号説明
    Public Const EXL_RELATION_KBN As Integer = 23                                   '関係者区分
    Public Const EXL_RELATION_ID As Integer = 24                                    '関係者ID

    Private aryClassTitle As New ArrayList                      '分類１、分類２、名称重複チェック
    Private aryURLRepetition As New ArrayList                   '取込番号＋URL重複チェック
    Private aryManageRepetition As New ArrayList                '取込番号＋サーバ管理番号重複チェック

    '列名配列
    Private strColNm As String() = COLUMNNAME_SYS

    Private strOutLog As String                                                  'ログ保存用文字列

    ''' <summary>
    ''' ファイル入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheckMain(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力ファイルチェック処理
        If FileInputCheck(dataHBKB0202) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行い、入力チェックエラーが発生するとログファイルに書き込む
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheck(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログ文字列初期化
            strOutLog = ""

            '入力チェック
            If InputCheck(dataHBKB0202) = False Then
                Return False
            End If

            '入力チェックエラー時にログ出力用変数にデータがある場合ログ出力画面へ
            If strOutLog <> "" Then
                'ログ出力処理
                If SetOutLog(dataHBKB0202) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function InputCheck(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '入力チェック用配列取得・入力チェック
            If SetArryInputForCheck(dataHBKB0202) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェック用の配列をExcelからセットする
    ''' <para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetArryInputForCheck(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        'Excelオブジェクト
        Dim xlApp As Object = Nothing       'Applicationオブジェクト
        Dim xlBooks As Object = Nothing     'Workbooksオブジェクト
        Dim xlBook As Object = Nothing      'Workbookオブジェクト
        Dim xlSheets As Object = Nothing    'Worksheetsオブジェクト
        Dim xlSheet As Object = Nothing     'Worksheetオブジェクト
        Dim xlRange As Object = Nothing     'Rangeオブジェクト
        Dim strBkNm As String = ""          'OriginalBook名

        '入力チェック用
        Dim intColCount As Integer = 0                      '項目数カウンタ
        Dim strStatusConvetCD As String = ""                'ステータスコード変換用
        Dim strFreeFlg1 As String = ""                      'フリーフラグ１変換用
        Dim strFreeFlg2 As String = ""                      'フリーフラグ２変換用
        Dim strFreeFlg3 As String = ""                      'フリーフラグ３変換用
        Dim strFreeFlg4 As String = ""                      'フリーフラグ４変換用
        Dim strFreeFlg5 As String = ""                      'フリーフラグ５変換用
        Dim blnErrorFlg As Boolean = False                  '入力チェック用フラグ用
        aryURLRepetition = New ArrayList                    '取込番号＋URL重複チェック用
        aryManageRepetition = New ArrayList                 '取込番号＋サーバ管理番号重複チェック用
        aryClassTitle = New ArrayList                       '分類１、分類２、名称重複チェック用

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ

        'DB登録用配列初期化
        With dataHBKB0202
            .PropAryRowCount = New ArrayList                '行番号
            .PropAryTorikomiNum = New ArrayList             '取込管理番号
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
            .PropAryInfShareteamNM = New ArrayList          '情報共有先
            .PropAryUrl = New ArrayList                     'ノウハウURL
            .PropAryUrlNaiyo = New ArrayList                'ノウハウURL説明
            .PropAryManageNmb = New ArrayList               'サーバー管理番号
            .PropAryManageNmbNaiyo = New ArrayList          'サーバー管理番号説明
            .PropAryRelationKbn = New ArrayList             '関係者区分
            .PropAryRelationID = New ArrayList              '関係ID
            '.PropAryRelationUsrID = New ArrayList           '関係ユーザーID
        End With

        Try
            'ファイルを開く
            xlApp = CreateObject("Excel.Application")

            'Workbook取得
            xlBooks = xlApp.Workbooks

            '取込ファイルを開く
            xlBook = xlBooks.Open(dataHBKB0202.PropStrFilePath)

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
                strOutLog &= B0202_E008
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                'エクセルを閉じる
                xlApp.Quit()
                Return True
            End If

            '取込番号入力チェック
            If Convert.ToString(xlSheet.Cells(EXL_START_ROW + 1, EXL_ACQUISITION_NUM).Value) = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, (EXL_START_ROW + 1).ToString, strColNm(EXL_ACQUISITION_NUM - 1)) & vbCrLf
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

                '変換ステータスCD初期化
                strStatusConvetCD = ""

                '分類～ステータスまでは前行の取込番号が違う場合チェックする
                If Count >= EXL_START_ROW + 1 And Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value) <> _
                   Convert.ToString(xlSheet.Cells(Count - 1, EXL_ACQUISITION_NUM).Value) Then

                    '分類1＋分類２＋名称の入力チェック
                    If CehckInputGroupAndName(dataHBKB0202, Adapter, Cn, Count, _
                                                            Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value), _
                                                            Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value), _
                                                            Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value)) = False Then
                        'エラーを返す
                        blnErrorFlg = True
                        Exit While
                    End If

                    'ステータスの入力チェック
                    If CehckInputStatus(dataHBKB0202, Adapter, Cn, Count, _
                        Convert.ToString(xlSheet.Cells(Count, EXL_STATUS).Value), strStatusConvetCD) = False Then
                        'エラーを返す
                        blnErrorFlg = True
                        Exit While
                    End If

                    'CIオーナーCDの存在チェック
                    If CheckInputCIOwner(dataHBKB0202, Adapter, Cn, Count, _
                        Convert.ToString(xlSheet.Cells(Count, EXL_CIOWNER_CD).Value)) = False Then
                        'エラーを返す
                        blnErrorFlg = True
                        Exit While
                    End If

                    '説明、フリーテキスト1～5、情報共有先桁数チェック
                    If ChekuInputLength(dataHBKB0202, Count, Convert.ToString(xlSheet.Cells(Count, EXL_EXPLANATION).Value), _
                        Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_1).Value), Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_2).Value), _
                        Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_3).Value), Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_4).Value), _
                        Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_5).Value), Convert.ToString(xlSheet.Cells(Count, EXL_INFO_SHAR).Value)) = False Then
                        'エラーを返す
                        blnErrorFlg = True
                        Exit While
                    End If

                End If

                'フリーフラグ格納
                strFreeFlg1 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_1).Value)
                strFreeFlg2 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_2).Value)
                strFreeFlg3 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_3).Value)
                strFreeFlg4 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_4).Value)
                strFreeFlg5 = Convert.ToString(xlSheet.Cells(Count, EXL_FREE_FLG_5).Value)

                'フリーフラグ１～フリーフラグ５の形式チェック
                If CheckInputForm(dataHBKB0202, Count, strFreeFlg1, strFreeFlg2, strFreeFlg3, strFreeFlg4, strFreeFlg5) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'ノウハウURL入力チェック
                If CheckInputKnowHowURL(dataHBKB0202, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_URL).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_EXPLANATION).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'サーバー管理番号入力チェック
                If CheckInputServerNum(dataHBKB0202, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_NUM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_EXPLANATION).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '関係者区分情報入力チェック
                If CheckInputRelation(dataHBKB0202, Adapter, Cn, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_RELATION_KBN).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_RELATION_ID).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'データクラスに保存
                With dataHBKB0202

                    'データ更新用配列-------------------------------------------------------------------------------------------------------------------------------------
                    .PropAryRowCount.Add(Count)                                                                                     '行番号
                    .PropAryTorikomiNum.Add(Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value))                      '取込管理番号
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
                    .PropAryInfShareteamNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_INFO_SHAR).Value))                         '情報共有先
                    .PropAryUrl.Add(Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_URL).Value))                                  'ノウハウURL
                    .PropAryUrlNaiyo.Add(Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_EXPLANATION).Value))                     'ノウハウURL説明
                    .PropAryManageNmb.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_NUM).Value))                      'サーバー管理番号
                    .PropAryManageNmbNaiyo.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_EXPLANATION).Value))         'サーバー管理番号説明
                    .PropAryRelationKbn.Add(Convert.ToString(xlSheet.Cells(Count, EXL_RELATION_KBN).Value))                         '関係者区分
                    .PropAryRelationID.Add(Convert.ToString(xlSheet.Cells(Count, EXL_RELATION_ID).Value))                           '関係ID

                    '入力チェック用配列--------------------------------------------------------------------------------------------------------------------------------------------------
                    '分類１+分類２＋名称重複チェック用
                    aryClassTitle.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value) & _
                    Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value) & _
                    Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value))
                    'ノウハウURL重複チェック用
                    If Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_URL).Value) <> "" Then
                        aryURLRepetition.Add(Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value) & _
                                                       Convert.ToString(xlSheet.Cells(Count, EXL_KNOWHOW_URL).Value))
                    End If
                    'サーバー管理番号重複チェック用
                    If Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_NUM).Value) <> "" Then
                        aryManageRepetition.Add(Convert.ToString(xlSheet.Cells(Count, EXL_ACQUISITION_NUM).Value) & _
                                                            Convert.ToString(xlSheet.Cells(Count, EXL_SERVERMANAGER_NUM).Value))
                    End If

                End With

                'カウンタインクリメント
                Count += 1

            End While

            '改行コード変換処理
            If ChangeToVbCrLfForSystem(dataHBKB0202) = False Then
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
    ''' 分類１、分類２、名称の入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strGroup1">分類１</param>
    ''' <param name="strGroup2">分類２</param>
    ''' <param name="strName">名称</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>分類１、分類２、名称の入力チェック、桁数チェック、ファイル内重複チェック、DB重複チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckInputGroupAndName(ByRef dataHBKB0202 As DataHBKB0202, ByVal Adapter As NpgsqlDataAdapter, _
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
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
                'エラーフラグセット
                blnGroup1Flg = True
            Else
                '桁数チェック
                If strGroup1.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
                    'エラーフラグセット
                    blnGroup1Flg = True
                End If
            End If

            '分類２の入力チェック
            If strGroup2 = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
                'エラーフラグセット
                blnGroup2Flg = True
            Else
                '桁数チェック
                If strGroup2.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
                    'エラーフラグセット
                    blnGroup2Flg = True
                End If
            End If

            '名称の入力チェック
            If strName = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
                'エラーフラグセット
                blnNameFlg = True
            Else
                '桁数チェック[Mod] 2012/08/02 y.ikushima 桁数を1000文字から100文字へ
                If strName.Length > 100 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0202_E005, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1) & "," _
                                               & strColNm(EXL_GROUPING_2 - 1) & "," & strColNm(EXL_TITLE - 1)) & vbCrLf
                    blnRepetitionFlg = True
                End If
            End If

            '分類1＋分類2＋名称のDB内重複チェック
            If blnRepetitionFlg = False Then
                If CheckRepetitionClassTitle(Adapter, Cn, dataHBKB0202, intIndex.ToString, strGroup1, strGroup2, strName) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <param name="strStatusConvetCD">ステータス変換文字列</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ステータスの必須チェック、存在チェックを行い、コードへ変換する
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckInputStatus(ByRef dataHBKB0202 As DataHBKB0202, ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strStatus As String, _
                                                  ByRef strStatusConvetCD As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ステータス入力チェック
            If strStatus = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
            Else
                'ステータスのDB存在チェック(ステータスコードを変換）
                If CheckStatusConvert(Adapter, Cn, dataHBKB0202, intIndex.ToString, strStatus, strStatusConvetCD) = False Then
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
    ''' CIオーナー入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strCIOwner">CIオーナーCD</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目の形式チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputCIOwner(ByRef dataHBKB0202 As DataHBKB0202, ByVal Adapter As NpgsqlDataAdapter, _
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
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_CIOWNER_CD - 1)) & vbCrLf
                    'エラーフラグ設定
                    blnCIOwnerFlg = True
                End If
            Else
                blnCIOwnerFlg = True
            End If

            'グループマスタからCIオーナーコード存在チェック
            If blnCIOwnerFlg = False Then
                If CheckRelationIDForGroup(Adapter, Cn, dataHBKB0202, intIndex, strCIOwner, strColNm(EXL_CIOWNER_CD - 1)) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strExplanation">説明</param>
    ''' <param name="strFreeText1">フリーテキスト1</param>
    ''' <param name="strFreeText2">フリーテキスト2</param>
    ''' <param name="strFreeText3">フリーテキスト3</param>
    ''' <param name="strFreeText4">フリーテキスト4</param>
    ''' <param name="strFreeText5">フリーテキスト5</param>
    ''' <param name="strInfoShare">情報共有先</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>説明、フリーテキスト１～５、情報共有先の桁数チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function ChekuInputLength(ByRef dataHBKB0202 As DataHBKB0202, ByRef intIndex As Integer, _
                                                    ByRef strExplanation As String, ByRef strFreeText1 As String, ByRef strFreeText2 As String, _
                                                    ByRef strFreeText3 As String, ByRef strFreeText4 As String, ByRef strFreeText5 As String, _
                                                    ByRef strInfoShare As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '説明
            If strExplanation.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_EXPLANATION - 1)) & vbCrLf
            End If
            'フリーテキスト１
            If strFreeText1.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_1 - 1)) & vbCrLf
            End If
            'フリーテキスト２
            If strFreeText2.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_2 - 1)) & vbCrLf
            End If
            'フリーテキスト３
            If strFreeText3.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_3 - 1)) & vbCrLf
            End If
            'フリーテキスト４
            If strFreeText4.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_4 - 1)) & vbCrLf
            End If
            'フリーテキスト５
            If strFreeText5.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_5 - 1)) & vbCrLf
            End If
            '情報共有先
            If strInfoShare.Length > 500 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_INFO_SHAR - 1)) & vbCrLf
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>フリーフラグの形式チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByRef dataHBKB0202 As DataHBKB0202, ByRef intIndex As Integer, ByRef FreeFlg1 As String, _
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
                    strOutLog &= String.Format(B0202_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_1 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0202_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_2 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0202_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_3 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0202_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_4 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0202_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_5 - 1)) & vbCrLf
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
    ''' ノウハウURL入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strTorikomiNum">取込番号</param>
    ''' <param name="strKnowHowURL">ノウハウURL</param>
    ''' <param name="strKnowHowURLEx">ノウハウURL説明</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ノウハウURLの入力チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputKnowHowURL(ByRef dataHBKB0202 As DataHBKB0202, ByRef intIndex As Integer, ByRef strTorikomiNum As String, _
                                                             ByRef strKnowHowURL As String, ByRef strKnowHowURLEx As String) As Boolean
        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnKnowHowURLFlg As Boolean = False                 'ノウハウURL入力チェックフラグ
        Dim blnKnowHowURLExFlg As Boolean = False               'ノウハウURL説明入力チェックフラグ

        Try

            'ノウハウURL説明の入力チェック
            If strKnowHowURL <> "" And strKnowHowURLEx = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_KNOWHOW_EXPLANATION - 1)) & vbCrLf
                'エラーフラグセット
                blnKnowHowURLFlg = True
            Else
                'ノウハウURL重複チェック
                If aryURLRepetition.Contains(strTorikomiNum & strKnowHowURL) = True Then
                    '同じ要素がある場合
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E005, intIndex.ToString, strColNm(EXL_KNOWHOW_URL - 1)) & vbCrLf
                    'エラーフラグセット
                    blnKnowHowURLFlg = True
                End If
            End If

            'ノウハウURL桁数チェック
            If blnKnowHowURLFlg = False And blnKnowHowURLExFlg = False Then
                'ノウハウURL
                If strKnowHowURL.Length > 500 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_KNOWHOW_URL - 1)) & vbCrLf
                End If
                'ノウハウURL説明
                If strKnowHowURLEx.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_KNOWHOW_EXPLANATION - 1)) & vbCrLf
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
    ''' サーバ管理番号入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strTorikomiNum">取込番号</param>
    ''' <param name="strServerNum">サーバ管理番号</param>
    ''' <param name="strServerNumEx">サーバ管理番号説明</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>サーバ管理番号の入力チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputServerNum(ByRef dataHBKB0202 As DataHBKB0202, ByRef intIndex As Integer, ByRef strTorikomiNum As String, _
                                                          ByRef strServerNum As String, ByRef strServerNumEx As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnManageNmbFlg As Boolean = False                      'サーバ管理番号入力チェックフラグ
        Dim blnManageNmbNaiyoFlg As Boolean = False                 'サーバ管理番号説明入力チェックフラグ

        Try
            'サーバー管理番号が入力されていた場合は説明の入力チェック
            If strServerNum <> "" And strServerNumEx = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0202_E002, intIndex.ToString, strColNm(EXL_SERVERMANAGER_EXPLANATION - 1)) & vbCrLf
                'エラーフラグセット
                blnManageNmbFlg = True
            Else
                'サーバー管理番号重複チェック
                If aryManageRepetition.Contains(strTorikomiNum & strServerNum) = True Then
                    '同じ要素がある場合
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E005, intIndex.ToString, strColNm(EXL_SERVERMANAGER_NUM - 1)) & vbCrLf
                    'エラーフラグセット
                    blnManageNmbFlg = True
                End If
            End If


            'サーバ管理番号桁数チェック
            If blnManageNmbFlg = False And blnManageNmbNaiyoFlg = False Then
                'サーバー管理番号
                If strServerNum.Length > 6 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_SERVERMANAGER_NUM - 1)) & vbCrLf
                End If
                'サーバー管理番号説明（500文字まで）
                If strServerNumEx.Length > 500 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_SERVERMANAGER_EXPLANATION - 1)) & vbCrLf
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
    ''' 関係者情報入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strRelationKbn">関係者区分</param>
    ''' <param name="strRelationID">関係者ID</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>関係者区分からグループ、ユーザの存在チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputRelation(ByRef dataHBKB0202 As DataHBKB0202, ByVal Adapter As NpgsqlDataAdapter, _
                                                     ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strRelationKbn As String, _
                                                     ByRef strRelationID As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '関係者区分による存在チェック
            If strRelationKbn = KBN_GROUP Then
                '関係者グループID桁数チェック
                If strRelationID.Length > 3 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_RELATION_ID - 1)) & vbCrLf
                Else
                    'グループマスタから関係者ID存在チェック
                    If CheckRelationIDForGroup(Adapter, Cn, dataHBKB0202, intIndex.ToString, strRelationID, strColNm(EXL_RELATION_ID - 1)) = False Then
                        Return False
                    End If
                End If

            ElseIf strRelationKbn = KBN_USER Then

                '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 START
                '関係者ユーザIDの桁数チェック
                If strRelationID.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_RELATION_ID - 1)) & vbCrLf
                Else
                    'ユーザマスタから関係者ID存在チェック
                    If CheckRelationIDForUser(Adapter, Cn, dataHBKB0202, intIndex.ToString, strRelationID) = False Then
                        Return False
                    End If
                End If
                ''関係者ユーザIDの桁数チェック
                'If strRelationID.Length > 10 Then
                '    'メッセージログ設定
                '    strOutLog &= String.Format(B0202_E003, intIndex.ToString, strColNm(EXL_RELATION_ID - 1)) & vbCrLf
                'Else
                '    'ユーザマスタから関係者ID存在チェック
                '    If CheckRelationIDForUser(Adapter, Cn, dataHBKB0202, intIndex.ToString, strRelationID) = False Then
                '        Return False
                '    End If
                'End If
                '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 START

            Else
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strGroup1">[IN]分類１</param>
    ''' <param name="strGroup2">[IN]分類２</param>
    ''' <param name="strName">[IN]名称</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>分類１DB+分類２DB+名称DB重複チェックをCI共通情報テーブルからデータを検索し重複チェックを行う
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 :2012/07/25 y.ikushima </p>
    ''' </para></remarks>
    Public Function CheckRepetitionClassTitle(ByVal Adapter As NpgsqlDataAdapter, _
                                                             ByVal Cn As NpgsqlConnection, _
                                                             ByRef dataHBKB0202 As DataHBKB0202, _
                                                             ByRef IntIndex As Integer, ByRef strGroup1 As String, _
                                                             ByRef strGroup2 As String, ByRef strName As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            '分類１、分類２、名称のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0202.SetSelectCountSameKeySql(Adapter, Cn, dataHBKB0202, strGroup1, strGroup2, strName) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "分類１、分類２、名称のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            '重複データがある場合、エラー
            If dtResult.Rows(0).Item(0) > 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(B0202_E007, IntIndex, strColNm(EXL_GROUPING_1 - 1) & "," & strColNm(EXL_GROUPING_2 - 1) & "," & strColNm(EXL_TITLE - 1)) & vbCrLf
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
    ''' ステータスコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strStatus">入力ステータス文字列</param>
    ''' <param name="strStatusConvetCD">ステータス変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたステータス名をCIステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 :2012/07/25 y.ikushima </p>
    ''' </para></remarks>
    Public Function CheckStatusConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                                      ByVal Cn As NpgsqlConnection, _
                                                      ByRef dataHBKB0202 As DataHBKB0202, _
                                                      ByRef IntIndex As Integer, _
                                                      ByRef strStatus As String, ByRef strStatusConvetCD As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ステータスコードのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0202.SetSelectCountCIStateCDSql(Adapter, Cn, dataHBKB0202, strStatus) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスコードのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0202
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strStatusConvetCD = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0202_E006, IntIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
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
    ''' CIオーナーコード、関係者ID存在チェック処理（グループ）
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strSearchID">[IN]検索用文字列</param>
    ''' <param name="strMessage">[IN]エラーログ用列名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたCIオーナーコード、関係者IDをグループマスタからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckRelationIDForGroup(ByVal Adapter As NpgsqlDataAdapter, _
                                                              ByVal Cn As NpgsqlConnection, _
                                                              ByRef dataHBKB0202 As DataHBKB0202, _
                                                              ByRef IntIndex As Integer, _
                                                              ByRef strSearchID As String, _
                                                              ByRef strMessage As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '検索IDセット
            dataHBKB0202.PropStrGroupCD = strSearchID

            '関係者IDのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0202.SetSelectRelationIDForGroup(Adapter, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタから関係者IDのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            'データが存在しない場合、エラー
            If dtResult.Rows(0).Item(0) = 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(B0202_E006, IntIndex.ToString, strMessage) & vbCrLf
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
    ''' 関係者ID存在チェック処理（ユーザ）
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strRelationID">関係者ID</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された関係者IDをユーザマスタ、所属マスタからデータを検索し存在チェック
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckRelationIDForUser(ByVal Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, _
                                                           ByRef dataHBKB0202 As DataHBKB0202, ByRef IntIndex As Integer, _
                                                           ByRef strRelationID As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '関係者ユーザIDのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0202.SetSelectRelationIDForUser(Adapter, Cn, dataHBKB0202, strRelationID) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ユーザマスタから関係者IDのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            'データが存在しない場合、エラー
            If dtResult.Rows(0).Item(0) = 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(B0202_E006, IntIndex.ToString, strColNm(EXL_RELATION_ID - 1)) & vbCrLf
            End If

            '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更START
            ''関係者ユーザIDと関係者グループCDで所属マスタの存在チェックを行う
            'If sqlHBKB0202.SetSelectRelationIDForSzk(Adapter, Cn, dataHBKB0202, strRelationGrpCD, strRelationUsrID) = False Then
            '    Return False
            'End If

            ''ログ出力
            'CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属スタから関係者IDのデータ有無取得", Nothing, Adapter.SelectCommand)
            ''SQL実行
            'Adapter.Fill(dtResult)

            ''データが存在しない場合、エラー
            'If dtResult.Rows(0).Item(0) = 0 Then
            '    'エラーメッセージ設定
            '    strOutLog &= String.Format(B0202_E006, IntIndex.ToString, strColNm(EXL_RELATION_ID - 1)) & vbCrLf
            'End If
            '[Mod] 2012/08/02 y.ikushima 関係者情報DB定義変更END

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
    ''' エラーログ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックでエラーとなった内容をログ出力する
    ''' <para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetOutLog(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strLogFilePath As String = Nothing                                      'ログファイルパス
        Dim strLogFileName As String = Nothing                                      'ログファイル名
        Dim strOutputDir As String = Nothing                                        'ログ出力フォルダ
        Dim stwWriteLog As System.IO.StreamWriter = Nothing                  'ファイル書込用クラス
        Dim strOutputpath As String = Nothing                                       '出力ファイル名

        Try
            'ログ出力内容が存在しない状態で遷移してきた場合
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
                puErrMsg = String.Format(B0202_E001, strOutputpath)
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>登録処理を行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function RegMain(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力データ登録処理
        If FileInputDataReg(dataHBKB0202) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力データの登録処理を行う
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputDataReg(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)                                                            'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing                                                                  'トランザクション
        Dim aryCINmb As New ArrayList                                                                           'CI番号保存用
        Dim blnErrorFlg As Boolean = False                                                                      'エラーフラグ

        Try

            '履歴Noを１で固定
            dataHBKB0202.PropIntRirekiNo = 1

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKB0202
                '取込番号分ループ
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1

                    If i = 0 Then
                        '1週目の場合
                        '新規CI番号取得
                        If SelectNewCINmb(Cn, dataHBKB0202) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'CI共通情報新規追加
                        If InsertCIInfo(Cn, dataHBKB0202, i) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'CI共通情報履歴情報新規追加
                        If InsertCIINfoR(Cn, dataHBKB0202) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'CIシステム情報新規追加
                        If InsertCISystem(Cn, dataHBKB0202, i) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If
                        'CIシステム履歴情報新規追加
                        If InsertCISystemR(Cn, dataHBKB0202) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'ノウハウURL情報新規追加
                        If .PropAryUrl(i) <> "" Then
                            If InsertKnowHowUrl(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'ノウハウURL履歴情報新規追加
                            If InsertKnowHowUrlR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'サーバ管理番号情報新規追加
                        If .PropAryManageNmb(i) <> "" Then
                            If InsertMngSrv(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'サーバー管理情報履歴情報新規追加
                            If InsertMngSrvR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        '関係者情報新規追加
                        If .PropAryRelationKbn(i) <> "" Then
                            If InsertRelation(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            '関係者履歴情報新規追加
                            If InsertRelationR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        '登録理由履歴新規追加
                        If InsertRegReasonR(Cn, dataHBKB0202) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        '原因リンク履歴情報新規追加
                        If InsertCauseLinkR(Cn, dataHBKB0202) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                    Else
                        '2週目以降
                        If .PropAryTorikomiNum(i) = .PropAryTorikomiNum(i - 1) Then
                            '前回と同じ取込番号の場合、新規CI番号取得、CI共通情報新規追加、CIシステム情報新規追加を行わない

                            'CI番号をキーにノウハウURL履歴情報、サーバー管理情報履歴情報、関係者履歴情報を削除する
                            'ウハウURL履歴情報削除
                            If .PropAryUrl(i) <> "" Then
                                If DeleteKnowHowUrlR(Cn, dataHBKB0202) = False Then
                                    'ロールバック
                                    If Tsx IsNot Nothing Then
                                        Tsx.Rollback()
                                    End If
                                    'エラーフラグを立ててループを抜ける
                                    blnErrorFlg = True
                                    Exit For
                                End If
                            End If

                            'サーバー管理情報履歴情報削除
                            If .PropAryManageNmb(i) <> "" Then
                                If DeleteMngSrvR(Cn, dataHBKB0202) = False Then
                                    'ロールバック
                                    If Tsx IsNot Nothing Then
                                        Tsx.Rollback()
                                    End If
                                    'エラーフラグを立ててループを抜ける
                                    blnErrorFlg = True
                                    Exit For
                                End If
                            End If

                            '関係者履歴情報削除
                            If .PropAryRelationKbn(i) <> "" Then
                                If DeleteKankeiR(Cn, dataHBKB0202) = False Then
                                    'ロールバック
                                    If Tsx IsNot Nothing Then
                                        Tsx.Rollback()
                                    End If
                                    'エラーフラグを立ててループを抜ける
                                    blnErrorFlg = True
                                    Exit For
                                End If
                            End If

                        Else
                            '新規CI番号取得
                            If SelectNewCINmb(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If

                            'CI共通情報新規追加
                            If InsertCIInfo(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'CI共通情報履歴情報新規追加
                            If InsertCIINfoR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If

                            'CIシステム情報新規追加
                            If InsertCISystem(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'CIシステム履歴情報新規追加
                            If InsertCISystemR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If

                            '登録理由履歴新規追加
                            If InsertRegReasonR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If

                            '原因リンク履歴情報新規追加
                            If InsertCauseLinkR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'ノウハウURL情報新規追加
                        If .PropAryUrl(i) <> "" Then
                            If InsertKnowHowUrl(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'ノウハウURL履歴情報新規追加
                            If InsertKnowHowUrlR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'サーバ管理番号情報新規追加
                        If .PropAryManageNmb(i) <> "" Then
                            If InsertMngSrv(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            'サーバー管理履歴情報新規追加
                            If InsertMngSrvR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        '関係者情報新規追加
                        If .PropAryRelationKbn(i) <> "" Then
                            If InsertRelation(Cn, dataHBKB0202, i) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                            '関係者履歴情報新規追加
                            If InsertRelationR(Cn, dataHBKB0202) = False Then
                                'ロールバック
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If
                    End If

                Next

            End With


            'エラーフラグ
            If blnErrorFlg = False Then
                'コミット
                Tsx.Commit()
            ElseIf blnErrorFlg = True Then
                Return False
            End If

            ''エラーフラグがONの場合、Falseを返す
            'If blnErrorFlg = True Then
            '    Return False
            'End If

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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmb(ByVal Cn As NpgsqlConnection, _
                                                   ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0202.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0202.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB0202.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0202_E009
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0202 As DataHBKB0202, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0202.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0202, intIndex) = False Then
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
    ''' CIシステム情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCIシステムテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISystem(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0202 As DataHBKB0202, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CIシステム新規登録（INSERT）用SQLを作成
            If sqlHBKB0202.SetInsertCISystemSql(Cmd, Cn, dataHBKB0202, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIシステム新規登録", Nothing, Cmd)

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
    ''' ノウハウURL新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をノウハウURLテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKnowHowUrl(ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKB0202 As DataHBKB0202, _
                                                     ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'ノウハウURL新規登録（INSERT）用SQLを作成
            If sqlHBKB0202.SetInsertKnowHowUrlSql(Cmd, Cn, dataHBKB0202, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL新規登録", Nothing, Cmd)

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
    ''' サーバー管理情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をサーバー管理情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMngSrv(ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0202 As DataHBKB0202, _
                                              ByVal intIndex As Integer) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'サーバー管理情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0202.SetInsertMngSrvSql(Cmd, Cn, dataHBKB0202, intIndex) = False Then
                Return False
            End If


            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理情報新規登録", Nothing, Cmd)

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
    ''' 関係者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelation(ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0202 As DataHBKB0202, _
                                               ByVal intIndex As Integer) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '関係者情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0202.SetInsertRelationSql(Cmd, Cn, dataHBKB0202, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者情報新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0202.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0202) = False Then
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
    ''' CIシステム履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CIシステム履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCISystemR(ByVal Cn As NpgsqlConnection, _
                                                  ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0202.SetInsertCISystemRSql(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIシステム履歴新規登録", Nothing, Cmd)

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
    ''' ノウハウURL履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKnowHowUrlR(ByVal Cn As NpgsqlConnection, _
                                                       ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0202.SetInsertKnowHowUrlRSql(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL履歴新規登録", Nothing, Cmd)

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
    ''' サーバー管理情報履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMngSrvR(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0202.SetInsertMngSrvRSql(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理情報履歴新規登録", Nothing, Cmd)

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
    ''' 関係者履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRelationR(ByVal Cn As NpgsqlConnection, _
                                                 ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'SQLを作成
            If sqlHBKB0202.SetInsertRelationRSql(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者履歴新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                                     ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0202.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB0202) = False Then
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
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0202.PropDtCauseLink.Rows.Count - 1
                '登録条件セット
                dataHBKB0202.PropStrMngNmb = dataHBKB0202.PropDtCauseLink.Rows(i).Item("MngNmb")
                dataHBKB0202.PropStrProcessKbn = dataHBKB0202.PropDtCauseLink.Rows(i).Item("ProcessKbn")
                'SQLを作成
                If sqlHBKB0202.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0202) = False Then
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
    ''' サーバー管理履歴情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>サーバー管理履歴情報テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteMngSrvR(ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'サーバー管理情報物理削除（DELETE）用SQLを作成
            If sqlHBKB0202.SetDeleteMngSrvSqlR(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー管理履歴情報物理削除", Nothing, Cmd)

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
    ''' ノウハウURL履歴情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ノウハウURL履歴テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteKnowHowUrlR(ByVal Cn As NpgsqlConnection, _
                                                        ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'ノウハウURL物理削除（DELETE）用SQLを作成
            If sqlHBKB0202.SetDeleteKnowHowUrlSqlR(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ノウハウURL履歴物理削除", Nothing, Cmd)

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
    ''' 関係履歴情報削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係履歴テーブルを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/09 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteKankeiR(ByVal Cn As NpgsqlConnection, _
                                               ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '関係履歴削除（DELETE）用SQLを作成
            If sqlHBKB0202.SetDeleteKankeiSqlR(Cmd, Cn, dataHBKB0202) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係履歴物理削除", Nothing, Cmd)

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
    ''' 改行コード変換処理
    ''' </summary>
    ''' <param name="dataHBKB0202">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>取込データの改行コードを変換する
    ''' <para>作成情報：2012/09/21 s.yamaguchi 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeToVbCrLfForSystem(ByRef dataHBKB0202 As DataHBKB0202) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0202

                '改行コードを再設定
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1

                    'データ更新用配列-------------------------------------------------------------------------------------------------------------------------------------
                    .PropAryTorikomiNum(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryTorikomiNum(i))            '取込管理番号
                    .PropAryClass1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryClass1(i))                      '分類１
                    .PropAryClass2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryClass2(i))                      '分類２
                    .PropAryCINM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCINM(i))                          'タイトル
                    .PropAryCIStatusCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCIStatusCD(i))              'ステータス
                    .PropAryCIOwnerCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCIOwnerCD(i))                'CIオーナー
                    .PropAryCINaiyo(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCINaiyo(i))                    '説明
                    .PropAryBIko1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko1(i))                        'フリーテキスト1
                    .PropAryBIko2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko2(i))                        'フリーテキスト2
                    .PropAryBIko3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko3(i))                        'フリーテキスト3
                    .PropAryBIko4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko4(i))                        'フリーテキスト4
                    .PropAryBIko5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBIko5(i))                        'フリーテキスト5
                    .PropAryFreeFlg1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg1(i))                  'フリーフラグ1
                    .PropAryFreeFlg2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg2(i))                  'フリーフラグ2
                    .PropAryFreeFlg3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg3(i))                  'フリーフラグ3
                    .PropAryFreeFlg4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg4(i))                  'フリーフラグ4
                    .PropAryFreeFlg5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg5(i))                  'フリーフラグ5
                    .PropAryInfShareteamNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryInfShareteamNM(i))      '情報共有先
                    .PropAryUrl(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUrl(i))                            'ノウハウURL
                    .PropAryUrlNaiyo(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUrlNaiyo(i))                  'ノウハウURL説明
                    .PropAryManageNmb(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryManageNmb(i))                'サーバー管理番号
                    .PropAryManageNmbNaiyo(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryManageNmbNaiyo(i))      'サーバー管理番号説明
                    .PropAryRelationKbn(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryRelationKbn(i))            '関係者区分
                    .PropAryRelationID(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryRelationID(i))              '関係ID

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
