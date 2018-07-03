Imports Common
Imports CommonHBK
Imports Npgsql
Imports Microsoft.Office.Interop
Imports System.Runtime.InteropServices
Imports System.IO
Imports System.Text

Public Class LogicHBKB0204

    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKB0204 As New SqlHBKB0204

    'Public定数宣言
    'Excelのスタート行
    Public Const EXL_START_ROW As Integer = 1
    'Excelの行をセット
    Public Const EXL_TORIKOMI_NUM As Integer = 1                                '取込番号
    Public Const EXL_NUM As Integer = 2                                         '番号
    Public Const EXL_GROUPING_1 As Integer = 3                                  '分類1
    Public Const EXL_GROUPING_2 As Integer = 4                                  '分類2
    Public Const EXL_TITLE As Integer = 5                                       '名称
    Public Const EXL_STATUS As Integer = 6                                      'ステータス
    Public Const EXL_CI_OWNER_CD As Integer = 7                                 'CIオーナーCD
    Public Const EXL_EXPLANATION As Integer = 8                                 '説明
    Public Const EXL_FREE_TEXT_1 As Integer = 9                                 'フリーテキスト1
    Public Const EXL_FREE_TEXT_2 As Integer = 10                                'フリーテキスト2
    Public Const EXL_FREE_TEXT_3 As Integer = 11                                'フリーテキスト3
    Public Const EXL_FREE_TEXT_4 As Integer = 12                                'フリーテキスト4
    Public Const EXL_FREE_TEXT_5 As Integer = 13                                'フリーテキスト5
    Public Const EXL_FREE_FLG_1 As Integer = 14                                 'フリーフラグ1
    Public Const EXL_FREE_FLG_2 As Integer = 15                                 'フリーフラグ2
    Public Const EXL_FREE_FLG_3 As Integer = 16                                 'フリーフラグ3
    Public Const EXL_FREE_FLG_4 As Integer = 17                                 'フリーフラグ4
    Public Const EXL_FREE_FLG_5 As Integer = 18                                 'フリーフラグ5
    Public Const EXL_KATABAN As Integer = 19                                    '型番
    Public Const EXL_ALIAU As Integer = 20                                      'エイリアス
    Public Const EXL_SERIAL As Integer = 21                                     '製造番号
    Public Const EXL_MAC_ADDRESS_1 As Integer = 22                              'MACアドレス1
    Public Const EXL_MAC_ADDRESS_2 As Integer = 23                              'MACアドレス2
    Public Const EXL_ZOO_KBN As Integer = 24                                    'zoo参加有無
    Public Const EXL_OS_CD As Integer = 25                                      'OS
    Public Const EXL_ANTI_VIRUS_SOFT As Integer = 26                            'ウィルス対策ソフト
    Public Const EXL_DNS_REG As Integer = 27                                    'DNS登録
    Public Const EXL_NIC_1 As Integer = 28                                      'NIC1
    Public Const EXL_NIC_2 As Integer = 29                                      'NIC2
    Public Const EXL_CONNECT_DT As Integer = 30                                 '接続日
    Public Const EXL_EXPIRATION_DT As Integer = 31                              '有効日
    Public Const EXL_DELETE_DT As Integer = 32                                  '停止日
    Public Const EXL_LAST_INFO_DT As Integer = 33                               '最終お知らせ日
    Public Const EXL_CONNECT_REASON As Integer = 34                             '接続理由
    Public Const EXL_EXPIRATION_UPDT As Integer = 35                            '更新日
    Public Const EXL_INFO_DT As Integer = 36                                    '通知日
    Public Const EXL_NUM_INFO_KBN As Integer = 37                               '番号通知
    Public Const EXL_SEAL_SEND_KBN As Integer = 38                              'シール送付
    Public Const EXL_ANTI_VIRUS_SOFT_CHECK_KBN As Integer = 39                  'ウイルス対策ソフト確認
    Public Const EXL_ANTI_VIRUS_SOFT_CHECK_DT As Integer = 40                   'ウイルス対策ソフトサーバー確認日
    Public Const EXL_BUSYO_KIKI_BIKO As Integer = 41                            '部所有機器備考
    Public Const EXL_MANAGE_KYOKU_NM As Integer = 42                            '管理局
    Public Const EXL_MANAGE_BUSYO_NM As Integer = 43                            '管理部署
    Public Const EXL_IP_USE As Integer = 44                                     'IP割当種類
    Public Const EXL_FIXED_IP As Integer = 45                                   '固定IP
    Public Const EXL_USR_ID As Integer = 46                                     'ユーザーID
    Public Const EXL_USR_NM As Integer = 47                                     'ユーザー氏名
    Public Const EXL_USR_COMPANY As Integer = 48                                'ユーザー所属会社
    Public Const EXL_USR_KYOKU_NM As Integer = 49                               'ユーザー所属局
    Public Const EXL_USR_BUSYO_NM As Integer = 50                               'ユーザー所属部署
    Public Const EXL_USR_TEL As Integer = 51                                    'ユーザー電話番号
    Public Const EXL_USR_MAIL_ADD As Integer = 52                               'ユーザーメールアドレス
    Public Const EXL_USR_CONTACT As Integer = 53                                'ユーザー連絡先
    Public Const EXL_USR_ROOM As Integer = 54                                   'ユーザー番組/部屋
    Public Const EXL_SET_KYOKU_NM As Integer = 55                               '設置局
    Public Const EXL_SET_BUSYO_NM As Integer = 56                               '設置部署
    Public Const EXL_SET_ROOM As Integer = 57                                   '設置番組/部屋
    Public Const EXL_SET_BUIL As Integer = 58                                   '設置建物
    Public Const EXL_SET_FLOOR As Integer = 59                                  '設置フロア

    '列名配列
    Private strColNm As String() = COLUMNNAME_BUSYO
    Private aryNumPrimary As New ArrayList                  '番号重複チェック用
    Private strOutLog As String                             'ログ保存用文字列



    ''' <summary>
    ''' ファイル入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部署機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' <p>改訂情報 : 2012/07/18 k.ueda（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheckMain(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力ファイルチェック処理
        If FileInputCheck(dataHBKB0204) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' <p>改訂情報 : 2012/07/18 k.ueda（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function FileInputCheck(ByRef dataHBKB0204 As DataHBKB0204) As Boolean



        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログ文字列初期化
            strOutLog = ""

            '入力チェック
            If InputCheck(dataHBKB0204) = False Then
                Return False
            End If

            '入力チェックエラー時にログ出力用変数にデータがある場合ログ出力画面へ
            If strOutLog <> "" Then
                'ログ出力処理
                If SetOutLog(dataHBKB0204) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックを行う
    ''' <para>作成情報：2012/07/18 k.ueda
    ''' </para></remarks>
    Public Function InputCheck(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '入力チェック用配列取得・入力チェック
            If SetArryInputForCheck(dataHBKB0204) = False Then
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
    ''' 入力チェック用配列セット処理・入力項目必須チェック・重複チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェック用の配列をExcelからセットする
    ''' <para>作成情報：2012/07/05 y.ikushima
    ''' <p>改訂情報 :2012/07/18 k.ueda(開発引き継ぎ)</p>
    ''' </para></remarks>
    Public Function SetArryInputForCheck(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

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

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                'アダプタ

        '入力チェック
        Dim intColCount As Integer = 0                      '項目数カウンタ
        Dim blnErrorFlg As Boolean = False                  '入力チェック用フラグ用
        aryNumPrimary = New ArrayList                       '番号重複チェック用
        Dim strFreeFlg1 As String = ""                      'フリーフラグ１変換用
        Dim strFreeFlg2 As String = ""                      'フリーフラグ２変換用
        Dim strFreeFlg3 As String = ""                      'フリーフラグ３変換用
        Dim strFreeFlg4 As String = ""                      'フリーフラグ４変換用
        Dim strFreeFlg5 As String = ""                      'フリーフラグ５変換用
        Dim strStatusConvetCD As String = ""                'ステータスコード変換用
        Dim strMacAddress1 As String = ""                   'MCAアドレス1変換用
        Dim strMacAddress2 As String = ""                   'MCAアドレス2変換用
        Dim strZooKbn As String = ""                        'Zoo区分変換用
        Dim strOs As String = ""                            'OS変換用
        Dim strAntiVirus As String = ""                     'ウイルス対策ソフト変換用
        Dim strDNSReg As String = ""                        'DNS登録変換用
        Dim strConnectDT As String = ""                     '接続日変換用
        Dim strExpirationDT As String = ""                  '有効日変換用
        Dim strDeleteDT As String = ""                      '停止日変換用
        Dim strLastinfoDT As String = ""                    '最終お知らせ日変換用
        Dim strExpirationUPDT As String = ""                '更新日変換用
        Dim strInfoDT As String = ""                        '通知日変換用
        Dim strNumnotice As String = ""                     '番号通知変換用
        Dim strSeal As String = ""                          'シール送付
        Dim strAntiVirusCon As String = ""                  'ウイルス対策ソフト確認
        Dim strAntiVirusDT As String = ""                   'ウイルス対策ソフトサーバー確認日
        Dim strIpUse As String = ""                         'IP割当種類

        '保存用配列初期化
        With dataHBKB0204
            .PropAryRowCount = New ArrayList            '行番号
            .PropAryTorikomiNum = New ArrayList         '取込番号
            .PropAryNum = New ArrayList                    '番号
            .PropAryGrouping1 = New ArrayList              '分類１
            .PropAryGrouping2 = New ArrayList              '分類２
            .PropAryTitle = New ArrayList                  '名称
            .PropAryStatsu = New ArrayList                 'ステータス
            .PropAryCIOwnerCD = New ArrayList              'CIオーナーCD
            .PropAryExplanation = New ArrayList            '説明
            .PropAryFreeText1 = New ArrayList              'フリーテキスト１
            .PropAryFreeText2 = New ArrayList              'フリーテキスト２
            .PropAryFreeText3 = New ArrayList              'フリーテキスト３
            .PropAryFreeText4 = New ArrayList              'フリーテキスト４
            .PropAryFreeText5 = New ArrayList              'フリーテキスト５
            .PropAryFreeFlg1 = New ArrayList               'フリーフラグ１
            .PropAryFreeFlg2 = New ArrayList               'フリーフラグ２
            .PropAryFreeFlg3 = New ArrayList               'フリーフラグ３
            .PropAryFreeFlg4 = New ArrayList               'フリーフラグ４
            .PropAryFreeFlg5 = New ArrayList               'フリーフラグ５
            .PropAryKataban = New ArrayList                '型番
            .PropAryAliau = New ArrayList                  'エイリアス
            .PropArySerial = New ArrayList                 '製造番号
            .PropAryMacAddress1 = New ArrayList            'MACアドレス1
            .PropAryMacAddress2 = New ArrayList            'MACアドレス2
            .PropAryZooKbn = New ArrayList                 'zoo参加有無
            .PropAryOSNM = New ArrayList                   'OS
            .PropAryAntiVirusSoftNM = New ArrayList        'ウイルス対策ソフト
            .PropAryDNSRegCD = New ArrayList               'DNS登録
            .PropAryNIC1 = New ArrayList                   'NIC1
            .PropAryNIC2 = New ArrayList                   'NIC2
            .PropAryConnectDT = New ArrayList              '接続日
            .PropAryExpirationDT = New ArrayList           '有効日
            .PropAryDeletDT = New ArrayList                '停止日
            .PropAryLastInfoDT = New ArrayList             '最終お知らせ日
            .PropAryConnectReason = New ArrayList           '接続理由
            .PropAryExpirationUPDT = New ArrayList         '更新日
            .PropAryInfoDT = New ArrayList                 '通知日
            .PropAryNumInfoKbn = New ArrayList             '番号通知
            .PropArySealSendkbn = New ArrayList            'シール送付
            .PropAryAntiVirusSoftCheckKbn = New ArrayList   'ウイルス対策ソフト確認 
            .PropAryAntiVirusSoftCheckDT = New ArrayList    'ウイルス対策ソフトサーバー確認日
            .PropAryBusyoKikiBiko = New ArrayList          '部所有機器備考
            .PropAryManageKyokuNM = New ArrayList          '管理局
            .PropAryManageBusyoNM = New ArrayList          '管理部署
            .PropAryIPUseCD = New ArrayList                'IP割当種類
            .PropAryFixedIP = New ArrayList                '固定IP
            .PropAryUsrID = New ArrayList                  'ユーザーID
            .PropAryUsrNM = New ArrayList                  'ユーザー氏名
            .PropAryUsrCompany = New ArrayList             'ユーザー所属会社
            .PropAryUsrKyokuNM = New ArrayList             'ユーザー所属局
            .PropAryUsrBusyoNM = New ArrayList             'ユーザー所属部署
            .PropAryUsrTel = New ArrayList                 'ユーザー電話番号
            .PropAryUsrMailAdd = New ArrayList             'ユーザーメールアドレス
            .PropAryUsrContact = New ArrayList             'ユーザー連絡先
            .PropAryUsrRoom = New ArrayList                'ユーザー番組/部屋
            .PropArySetKyokuNM = New ArrayList             '設置局
            .PropArySetBusyoNM = New ArrayList             '設置部署
            .PropArySetRoom = New ArrayList                '設置番組/部屋
            .PropArySetBuil = New ArrayList                '設置建物
            .PropArySetFloor = New ArrayList               '設置フロア
        End With

        Try

            'ファイルを開く
            xlApp = CreateObject("Excel.Application")

            'Workbook取得
            xlBooks = xlApp.Workbooks

            '取込ファイルを開く
            xlBook = xlBooks.Open(dataHBKB0204.PropStrFilePath)

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
                If Convert.ToString(xlSheet.Cells(EXL_START_ROW, EXL_TORIKOMI_NUM + i).Value) <> "" Then
                    intColCount = intColCount + 1
                End If
            Next
            'カウンタと列数が等しくない場合エラー
            If intColCount <> strColNm.Length Then
                strOutLog &= B0204_E008
                '保存しないで閉じる 
                xlBook.Close(SaveChanges:=False)
                'エクセルを閉じる
                xlApp.Quit()
                Return True
            End If

            '取込番号入力チェック
            If Convert.ToString(xlSheet.Cells(EXL_START_ROW + 1, EXL_TORIKOMI_NUM).Value) = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, (EXL_START_ROW + 1).ToString, strColNm(EXL_TORIKOMI_NUM - 1)) & vbCrLf
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
                If Convert.ToString(xlSheet.Cells(Count, EXL_TORIKOMI_NUM).Value) = "" Then
                    '処理を抜ける
                    Exit While
                End If

                '番号入力チェック
                If CheckNumLength(dataHBKB0204, Adapter, Cn, Count, Convert.ToString(xlSheet.Cells(Count, EXL_NUM).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '分類1＋分類２＋名称の入力チェック
                If CehckInputGroupAndName(dataHBKB0204, Count, _
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
                If CehckInputStatus(dataHBKB0204, Adapter, Cn, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_STATUS).Value), strStatusConvetCD) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'CIオーナーCDの存在チェック
                If CheckInputCIOwner(dataHBKB0204, Adapter, Cn, Count, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_CI_OWNER_CD).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '説明、フリーテキスト1～5桁数チェック
                If ChekuInputLength(dataHBKB0204, Count, Convert.ToString(xlSheet.Cells(Count, EXL_EXPLANATION).Value), _
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
                If CheckInputForm(dataHBKB0204, Count, strFreeFlg1, strFreeFlg2, strFreeFlg3, strFreeFlg4, strFreeFlg5) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'MACアドレス変換用に保存
                strMacAddress1 = Convert.ToString(xlSheet.Cells(Count, EXL_MAC_ADDRESS_1).Value)
                strMacAddress2 = Convert.ToString(xlSheet.Cells(Count, EXL_MAC_ADDRESS_2).Value)

                '型番、エイリアス、製造番号、MACアドレス1、MACアドレス2入力チェック
                If CheckInputForm_Kata(dataHBKB0204, Count, Convert.ToString(xlSheet.Cells(Count, EXL_KATABAN).Value), _
                   Convert.ToString(xlSheet.Cells(Count, EXL_ALIAU).Value), Convert.ToString(xlSheet.Cells(Count, EXL_SERIAL).Value), _
                   strMacAddress1, strMacAddress2) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                strZooKbn = Convert.ToString(xlSheet.Cells(Count, EXL_ZOO_KBN).Value)               'zoo参加有無
                strOs = Convert.ToString(xlSheet.Cells(Count, EXL_OS_CD).Value)                     'OS
                strAntiVirus = Convert.ToString(xlSheet.Cells(Count, EXL_ANTI_VIRUS_SOFT).Value)    'ウイルス対策ソフト
                strDNSReg = Convert.ToString(xlSheet.Cells(Count, EXL_DNS_REG).Value)               'DNS登録

                'zoo参加有無、OS、ウイルス対策ソフト、DNS登録、NIC1、NIC2入力チェック
                If CheckInputForm_Zoo(dataHBKB0204, Adapter, Cn, Count, strZooKbn, strOs, strAntiVirus, strDNSReg, _
                   Convert.ToString(xlSheet.Cells(Count, EXL_NIC_1).Value), Convert.ToString(xlSheet.Cells(Count, EXL_NIC_1).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                strConnectDT = Convert.ToString(xlSheet.Cells(Count, EXL_CONNECT_DT).Value)     '接続日
                strExpirationDT = Convert.ToString(xlSheet.Cells(Count, EXL_EXPIRATION_DT).Value)   '有効日
                strDeleteDT = Convert.ToString(xlSheet.Cells(Count, EXL_DELETE_DT).Value)       '停止日
                strLastinfoDT = Convert.ToString(xlSheet.Cells(Count, EXL_LAST_INFO_DT).Value)  '最終お知らせ日
                strExpirationUPDT = Convert.ToString(xlSheet.Cells(Count, EXL_EXPIRATION_UPDT).Value) '更新日
                strInfoDT = Convert.ToString(xlSheet.Cells(Count, EXL_INFO_DT).Value)           '通知日

                '接続日、有効日、停止日、最終お知らせ日、接続理由、更新日、通知日入力チェック
                If CheckInputForm_Connect(dataHBKB0204, Count, strConnectDT, strExpirationDT, strDeleteDT, _
                    strLastinfoDT, Convert.ToString(xlSheet.Cells(Count, EXL_CONNECT_REASON).Value), strExpirationUPDT, strInfoDT) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                strNumnotice = Convert.ToString(xlSheet.Cells(Count, EXL_NUM_INFO_KBN).Value)                   '番号通知変換用
                strSeal = Convert.ToString(xlSheet.Cells(Count, EXL_SEAL_SEND_KBN).Value)                       'シール送付
                strAntiVirusCon = Convert.ToString(xlSheet.Cells(Count, EXL_ANTI_VIRUS_SOFT_CHECK_KBN).Value)   'ウイルス対策ソフト確認
                strAntiVirusDT = Convert.ToString(xlSheet.Cells(Count, EXL_ANTI_VIRUS_SOFT_CHECK_DT).Value)     'ウイルス対策ソフトサーバー確認日

                '番号通知、シール送付、ウイルス対策ソフト確認、ウイルス対策ソフトサーバー確認日、部所有機器備考入力チェック
                If CheckInputform_Numnotice(dataHBKB0204, Count, strNumnotice, strSeal, strAntiVirusCon, strAntiVirusDT, _
                    Convert.ToString(xlSheet.Cells(Count, EXL_BUSYO_KIKI_BIKO).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                strIpUse = Convert.ToString(xlSheet.Cells(Count, EXL_IP_USE).Value) 'IP割当種類変換用
                '管理局、管理部署、IP割当種類、固定IP入力チェック
                If Checkinputform_Kanri(dataHBKB0204, Adapter, Cn, Count, Convert.ToString(xlSheet.Cells(Count, EXL_MANAGE_KYOKU_NM).Value), _
                Convert.ToString(xlSheet.Cells(Count, EXL_MANAGE_BUSYO_NM).Value), strIpUse, Convert.ToString(xlSheet.Cells(Count, EXL_FIXED_IP).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                'ユーザーID、ユーザー氏名、ユーザー所属会社、ユーザー所属局、ユーザー所属部署、ユーザー電話番号、ユーザーメールアドレス、ユーザー連絡先、ユーザー番組/部屋入力チェック
                If CheckInputForm_User(dataHBKB0204, Count, Convert.ToString(xlSheet.Cells(Count, EXL_USR_ID).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_USR_NM).Value), Convert.ToString(xlSheet.Cells(Count, EXL_USR_COMPANY).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_USR_KYOKU_NM).Value), Convert.ToString(xlSheet.Cells(Count, EXL_USR_BUSYO_NM).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_USR_TEL).Value), Convert.ToString(xlSheet.Cells(Count, EXL_USR_MAIL_ADD).Value), _
                    Convert.ToString(xlSheet.Cells(Count, EXL_USR_CONTACT).Value), Convert.ToString(xlSheet.Cells(Count, EXL_USR_ROOM).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If

                '設置局、設置部署、設置番組/部屋、設置建物、設置フロア入力チェック
                If CheckInputForm_Set(dataHBKB0204, Count, Convert.ToString(xlSheet.Cells(Count, EXL_SET_KYOKU_NM).Value), _
                     Convert.ToString(xlSheet.Cells(Count, EXL_SET_BUSYO_NM).Value), Convert.ToString(xlSheet.Cells(Count, EXL_SET_ROOM).Value), _
                     Convert.ToString(xlSheet.Cells(Count, EXL_SET_BUIL).Value), Convert.ToString(xlSheet.Cells(Count, EXL_SET_FLOOR).Value)) = False Then
                    'エラーを返す
                    blnErrorFlg = True
                    Exit While
                End If


                'データクラスに保存
                With dataHBKB0204
                    .PropAryRowCount.Add(Count)                                                                                     '行番号
                    .PropAryTorikomiNum.Add(Convert.ToString(xlSheet.Cells(Count, EXL_TORIKOMI_NUM).Value))                         '取込管理番号
                    .PropAryNum.Add(Convert.ToString(xlSheet.Cells(Count, EXL_NUM).Value))                                          '番号
                    .PropAryGrouping1.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_1).Value))                             '分類１
                    .PropAryGrouping2.Add(Convert.ToString(xlSheet.Cells(Count, EXL_GROUPING_2).Value))                             '分類２
                    .PropAryTitle.Add(Convert.ToString(xlSheet.Cells(Count, EXL_TITLE).Value))                                      '名称
                    .PropAryStatsu.Add(strStatusConvetCD)                                                                           'ステータス
                    .PropAryCIOwnerCD.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CI_OWNER_CD).Value))                            'CIオーナー
                    .PropAryExplanation.Add(Convert.ToString(xlSheet.Cells(Count, EXL_EXPLANATION).Value))                          '説明
                    .PropAryFreeText1.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_1).Value))                            'フリーテキスト1
                    .PropAryFreeText2.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_2).Value))                            'フリーテキスト2
                    .PropAryFreeText3.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_3).Value))                            'フリーテキスト3
                    .PropAryFreeText4.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_4).Value))                            'フリーテキスト4
                    .PropAryFreeText5.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FREE_TEXT_5).Value))                            'フリーテキスト5
                    .PropAryFreeFlg1.Add(strFreeFlg1)                                                                               'フリーフラグ1
                    .PropAryFreeFlg2.Add(strFreeFlg2)                                                                               'フリーフラグ2
                    .PropAryFreeFlg3.Add(strFreeFlg3)                                                                               'フリーフラグ3
                    .PropAryFreeFlg4.Add(strFreeFlg4)                                                                               'フリーフラグ4
                    .PropAryFreeFlg5.Add(strFreeFlg5)                                                                               'フリーフラグ5
                    .PropAryKataban.Add(Convert.ToString(xlSheet.Cells(Count, EXL_KATABAN).Value))                                  '型番
                    .PropAryAliau.Add(Convert.ToString(xlSheet.Cells(Count, EXL_ALIAU).Value))                                      'エイリアス
                    .PropArySerial.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SERIAL).Value))                                    '製造番号
                    .PropAryMacAddress1.Add(strMacAddress1)                                                                         'MACアドレス1
                    .PropAryMacAddress2.Add(strMacAddress2)                                                                         'MACアドレス2
                    .PropAryZooKbn.Add(strZooKbn)                                                                                   'zoo参加有無
                    .PropAryOSNM.Add(strOs)                                                                                         'OS
                    .PropAryAntiVirusSoftNM.Add(strAntiVirus)                                                                       'ウイルス対策ソフト
                    .PropAryDNSRegCD.Add(strDNSReg)                                                                                 'DNS登録
                    .PropAryNIC1.Add(Convert.ToString(xlSheet.Cells(Count, EXL_NIC_1).Value))                                       'NIC1
                    .PropAryNIC2.Add(Convert.ToString(xlSheet.Cells(Count, EXL_NIC_2).Value))                                       'NIC2
                    .PropAryConnectDT.Add(strConnectDT)                                                                             '接続日
                    .PropAryExpirationDT.Add(strExpirationDT)                                                                       '有効日
                    .PropAryDeletDT.Add(strDeleteDT)                                                                                '停止日
                    .PropAryLastInfoDT.Add(strLastinfoDT)                                                                           '最終お知らせ日
                    .PropAryConnectReason.Add(Convert.ToString(xlSheet.Cells(Count, EXL_CONNECT_REASON).Value))                     '接続理由
                    .PropAryExpirationUPDT.Add(strExpirationUPDT)                                                                   '更新日
                    .PropAryInfoDT.Add(strInfoDT)                                                                                   '通知日
                    .PropAryNumInfoKbn.Add(strNumnotice)                                                                            '番号通知
                    .PropArySealSendkbn.Add(strSeal)                                                                                'シール送付
                    .PropAryAntiVirusSoftCheckKbn.Add(strAntiVirusCon)                                                              'ウイルス対策ソフト確認
                    .PropAryAntiVirusSoftCheckDT.Add(strAntiVirusDT)                                                                'ウイルス対策ソフトサーバー確認日
                    .PropAryBusyoKikiBiko.Add(Convert.ToString(xlSheet.Cells(Count, EXL_BUSYO_KIKI_BIKO).Value))                    '部所有機器備考
                    .PropAryManageKyokuNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_MANAGE_KYOKU_NM).Value))                    '管理局
                    .PropAryManageBusyoNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_MANAGE_BUSYO_NM).Value))                    '管理部署
                    .PropAryIPUseCD.Add(strIpUse)                                                                                   'IP割当種類
                    .PropAryFixedIP.Add(Convert.ToString(xlSheet.Cells(Count, EXL_FIXED_IP).Value))                                 '固定IP
                    .PropAryUsrID.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_ID).Value))                                     'ユーザーID
                    .PropAryUsrNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_NM).Value))                                     'ユーザー氏名
                    .PropAryUsrCompany.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_COMPANY).Value))                           'ユーザー所属会社
                    .PropAryUsrKyokuNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_KYOKU_NM).Value))                          'ユーザー所属局
                    .PropAryUsrBusyoNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_BUSYO_NM).Value))                          'ユーザー所属部署
                    .PropAryUsrTel.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_TEL).Value))                                   'ユーザー電話番号
                    .PropAryUsrMailAdd.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_MAIL_ADD).Value))                          'ユーザーメールアドレス
                    .PropAryUsrContact.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_CONTACT).Value))                           'ユーザー連絡先
                    .PropAryUsrRoom.Add(Convert.ToString(xlSheet.Cells(Count, EXL_USR_ROOM).Value))                                 'ユーザー番号/部屋
                    .PropArySetKyokuNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SET_KYOKU_NM).Value))                          '設置局
                    .PropArySetBusyoNM.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SET_BUSYO_NM).Value))                          '設置部署
                    .PropArySetRoom.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SET_ROOM).Value))                                 '設置番組/部屋
                    .PropArySetBuil.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SET_BUIL).Value))                                 '設置建物
                    .PropArySetFloor.Add(Convert.ToString(xlSheet.Cells(Count, EXL_SET_FLOOR).Value))                               '設置フロア
                End With

                '番号重複チェック
                aryNumPrimary.Add(xlSheet.Cells(Count, EXL_NUM).Text)

                'カウンタインクリメント
                Count += 1

            End While

            '改行コード変換処理
            If ChangeToVbCrLfForBuy(dataHBKB0204) = False Then
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
    ''' 番号入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strNum">番号</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>番号の入力チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckNumLength(ByRef dataHBKB0204 As DataHBKB0204, ByVal Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strNum As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnNumFlg As Boolean = False                 '番号入力チェックフラグ

        Try
            '番号
            If strNum = "" Then
                blnNumFlg = True
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_NUM - 1)) & vbCrLf
            End If

            '番号（手動）（50文字まで）
            If blnNumFlg = False Then
                If strNum.Length > 50 Then
                    blnNumFlg = True
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_NUM - 1)) & vbCrLf
                End If
            End If

            '番号のファイル内重複チェック
            If blnNumFlg = False Then
                If aryNumPrimary.Contains(strNum) = True Then
                    '同じ要素がある場合
                    blnNumFlg = True
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E005, intIndex.ToString, strColNm(EXL_NUM - 1)) & vbCrLf
                End If
            End If

            '番号のDB重複チェック
            If blnNumFlg = False Then
                If CheckNumPrimary(Adapter, Cn, dataHBKB0204, intIndex, strNum) = False Then
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
    ''' 分類１、分類２、名称の入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strGroup1">分類１</param>
    ''' <param name="strGroup2">分類２</param>
    ''' <param name="strName">名称</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>分類１、分類２、名称の入力チェック、桁数チェック、ファイル内重複チェック、DB重複チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckInputGroupAndName(ByRef dataHBKB0204 As DataHBKB0204,  ByRef intIndex As Integer, ByRef strGroup1 As String, _
                                                                ByRef strGroup2 As String, ByRef strName As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '分類１の入力チェック
            If strGroup1 = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
            Else
                '桁数チェック
                If strGroup1.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_GROUPING_1 - 1)) & vbCrLf
                End If
            End If

            '分類２の入力チェック
            If strGroup2 = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
            Else
                '桁数チェック
                If strGroup2.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_GROUPING_2 - 1)) & vbCrLf
                End If
            End If

            '名称の入力チェック
            If strName = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
            Else
                '桁数チェック[Mod] 2012/08/02 y.ikushima 桁数を1000文字から100文字へ
                If strName.Length > 100 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_TITLE - 1)) & vbCrLf
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
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
    Public Function CehckInputStatus(ByRef dataHBKB0204 As DataHBKB0204, ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strStatus As String, _
                                                  ByRef strStatusConvetCD As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ステータス入力チェック
            If strStatus = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
            Else
                'ステータスのDB存在チェック(ステータスコードを変換）
                If CheckStatusConvert(Adapter, Cn, dataHBKB0204, intIndex.ToString, strStatus, strStatusConvetCD) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strCIOwner">CIオーナーCD</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目の形式チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputCIOwner(ByRef dataHBKB0204 As DataHBKB0204, ByVal Adapter As NpgsqlDataAdapter, _
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
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_CI_OWNER_CD - 1)) & vbCrLf
                    'エラーフラグ設定
                    blnCIOwnerFlg = True
                End If
            Else
                blnCIOwnerFlg = True
            End If

            'グループマスタからCIオーナーコード存在チェック
            If blnCIOwnerFlg = False Then
                If CheckCIOwnerCD(Adapter, Cn, dataHBKB0204, intIndex, strCIOwner) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
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
    Public Function ChekuInputLength(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, _
                                                    ByRef strExplanation As String, ByRef strFreeText1 As String, ByRef strFreeText2 As String, _
                                                    ByRef strFreeText3 As String, ByRef strFreeText4 As String, ByRef strFreeText5 As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '説明
            If strExplanation.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_EXPLANATION - 1)) & vbCrLf
            End If
            'フリーテキスト１
            If strFreeText1.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_1 - 1)) & vbCrLf
            End If
            'フリーテキスト２
            If strFreeText2.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_2 - 1)) & vbCrLf
            End If
            'フリーテキスト３
            If strFreeText3.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_3 - 1)) & vbCrLf
            End If
            'フリーテキスト４
            If strFreeText4.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_4 - 1)) & vbCrLf
            End If
            'フリーテキスト５
            If strFreeText5.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FREE_TEXT_5 - 1)) & vbCrLf
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>フリーフラグの形式チェックを行う
    ''' <para>作成情報：2012/07/25 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef FreeFlg1 As String, _
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
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_1 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_2 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_3 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_4 - 1)) & vbCrLf
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
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_FREE_FLG_5 - 1)) & vbCrLf
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
    ''' 型番、エイリアス、製造番号、MACアドレス1、MACアドレス2入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strKataban">型番</param>
    ''' <param name="strAliau">エイリアス</param>
    ''' <param name="strSerial">製造番号</param>
    ''' <param name="strMacaddress1">MACアドレス１</param>
    ''' <param name="strMacaddress2">MACアドレス２</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>型番、エイリアス、製造番号、MACアドレス1、MACアドレス2入力チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm_Kata(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef strKataban As String, _
                                                  ByRef strAliau As String, ByRef strSerial As String, ByRef strMacaddress1 As String, ByRef strMacaddress2 As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '型番
            If strKataban = "" Then
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_KATABAN - 1)) & vbCrLf
            Else
                If strKataban.Length > 25 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_KATABAN - 1)) & vbCrLf
                End If
            End If

            'エイリアス
            If strAliau.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_ALIAU - 1)) & vbCrLf
            End If

            '製造番号
            If strSerial.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SERIAL - 1)) & vbCrLf
            End If

            'MACアドレス１
            If strMacaddress1.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_MAC_ADDRESS_1 - 1)) & vbCrLf
            Else
                '形式変換
                strMacaddress1 = strMacaddress1.Replace(":", "")
                strMacaddress1 = strMacaddress1.Replace("-", "")
            End If

            'MACアドレス２
            If strMacaddress2.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_MAC_ADDRESS_2 - 1)) & vbCrLf
            Else
                '形式変換
                strMacaddress2 = strMacaddress2.Replace(":", "")
                strMacaddress2 = strMacaddress2.Replace("-", "")
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
    ''' zoo参加有無、OS、ウイルス対策ソフト、DNS登録、NIC1、NIC2入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strZooKbn">zoo参加有無</param>
    ''' <param name="strOs">OS</param>
    ''' <param name="strAntiVirus">ウイルス対策ソフト</param>
    ''' <param name="strDNSReg">DNS登録</param>
    ''' <param name="strNIC1">NIC１</param>
    ''' <param name="strNIC2">NIC２</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>zoo参加有無、OS、ウイルス対策ソフト、DNS登録、NIC1、NIC2入力チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm_Zoo(ByRef dataHBKB0204 As DataHBKB0204, ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strZooKbn As String, _
                                                  ByRef strOs As String, ByRef strAntiVirus As String, ByRef strDNSReg As String, ByRef strNIC1 As String, _
                                                  ByRef strNIC2 As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'zoo参加有無
            If strZooKbn = "" Then
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_ZOO_KBN - 1)) & vbCrLf
            Else
                If strZooKbn = ZOO_NM_UNFIN Then
                    strZooKbn = ZOO_KBN_UNFIN
                Else
                    strZooKbn = ZOO_KBN_FIN
                End If
            End If

            'OS名の桁数チェック
            If strOs.Length > 100 Then
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_OS_CD - 1)) & vbCrLf
            End If

            'ウィルス対策ソフト名の桁数チェック
            If strAntiVirus.Length > 100 Then
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_ANTI_VIRUS_SOFT - 1)) & vbCrLf
            End If

            ''OSの存在チェック、変換
            'If strOs <> "" Then
            '    If CheckOSConvert(Adapter, Cn, dataHBKB0204, intIndex, strOs) = False Then
            '        Return False
            '    End If
            'End If

            ''ウイルス対策ソフトの存在チェック、変換
            'If strAntiVirus <> "" Then
            '    If CheckAntiVirusConvert(Adapter, Cn, dataHBKB0204, intIndex, strAntiVirus) = False Then
            '        Return False
            '    End If
            'End If

            'DNS登録の存在チェック、変換
            If strDNSReg = "" Then
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_DNS_REG - 1)) & vbCrLf
            Else
                If CheckDNSRegConvert(Adapter, Cn, dataHBKB0204, intIndex, strDNSReg) = False Then
                    Return False
                End If
            End If
 
            'NIC１の入力チェック
            If strNIC1.Length > 150 Then
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_NIC_1 - 1)) & vbCrLf
            End If

            'NIC２の入力チェック
            If strNIC2.Length > 150 Then
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_NIC_2 - 1)) & vbCrLf
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
    ''' 接続日、有効日、停止日、最終お知らせ日、接続理由、更新日、通知日入力チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strConnectDT">接続日</param>
    ''' <param name="strExpirationDT">有効日</param>
    ''' <param name="strDeleteDT">停止日</param>
    ''' <param name="strLastinfoDT">最終お知らせ日</param>
    ''' <param name="strConnectReason">接続理由</param>
    ''' <param name="strExpirationUPDT">更新日</param>
    ''' <param name="strInfoDT">通知日</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>接続日、有効日、停止日、最終お知らせ日、接続理由、更新日、通知日入力チェックを行う
    ''' <para>作成情報：2012/07/30 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm_Connect(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef strConnectDT As String, _
                                                  ByRef strExpirationDT As String, ByRef strDeleteDT As String, ByRef strLastinfoDT As String, ByRef strConnectReason As String, _
                                                  ByRef strExpirationUPDT As String, ByRef strInfoDT As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '接続日
            '入力がある場合はチェック
            If strConnectDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strConnectDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strConnectDT) = True Then
                        '形式変換
                        strConnectDT = DateTime.Parse(strConnectDT).ToString("yyyy/MM/dd")
                        strConnectDT = strConnectDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_CONNECT_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_CONNECT_DT - 1)) & vbCrLf
                End If
            End If
            '有効日
            '入力がある場合はチェック
            If strExpirationDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strExpirationDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strExpirationDT) = True Then
                        '形式変換
                        strExpirationDT = DateTime.Parse(strExpirationDT).ToString("yyyy/MM/dd")
                        strExpirationDT = strExpirationDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_EXPIRATION_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_EXPIRATION_DT - 1)) & vbCrLf
                End If
            End If

            '停止日
            '入力がある場合はチェック
            If strDeleteDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strDeleteDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strDeleteDT) = True Then
                        '形式変換
                        strDeleteDT = DateTime.Parse(strDeleteDT).ToString("yyyy/MM/dd")
                        strDeleteDT = strDeleteDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_DELETE_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_DELETE_DT - 1)) & vbCrLf
                End If
            End If

            '最終お知らせ日
            '入力がある場合はチェック
            If strLastinfoDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strLastinfoDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strLastinfoDT) = True Then
                        '形式変換
                        strLastinfoDT = DateTime.Parse(strLastinfoDT).ToString("yyyy/MM/dd")
                        strLastinfoDT = strLastinfoDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_LAST_INFO_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_LAST_INFO_DT - 1)) & vbCrLf
                End If
            End If

            '接続理由
            '接続理由（1000文字まで）
            If strConnectReason.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_CONNECT_REASON - 1)) & vbCrLf
            End If

            '更新日
            '入力がある場合はチェック
            If strExpirationUPDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strExpirationUPDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strExpirationUPDT) = True Then
                        '形式変換
                        strExpirationUPDT = DateTime.Parse(strExpirationUPDT).ToString("yyyy/MM/dd")
                        strExpirationUPDT = strExpirationUPDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_EXPIRATION_UPDT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_EXPIRATION_UPDT - 1)) & vbCrLf
                End If
            End If

            '通知日
            '入力がある場合はチェック
            If strInfoDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strInfoDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strInfoDT) = True Then
                        '形式変換
                        strInfoDT = DateTime.Parse(strInfoDT).ToString("yyyy/MM/dd")
                        strInfoDT = strInfoDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_INFO_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_INFO_DT - 1)) & vbCrLf
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
    ''' 番号通知、シール送付、ウイルス対策ソフト確認、ウイルス対策ソフトサーバー確認日、部所有機器備考入力チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strNumnotice">番号通知</param>
    ''' <param name="strSeal">シール送付</param>
    ''' <param name="strAntiVirusCon">ウイルス対策ソフト確認</param>
    ''' <param name="strAntiVirusDT">ウイルス対策ソフトサーバー確認日</param>
    ''' <param name="strKikiBiko">部所有機器備考</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>番号通知、シール送付、ウイルス対策ソフト確認、ウイルス対策ソフトサーバー確認日、部所有機器備考入力チェックを行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputform_Numnotice(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef strNumnotice As String, _
                                                  ByRef strSeal As String, ByRef strAntiVirusCon As String, ByRef strAntiVirusDT As String, ByRef strKikiBiko As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '番号通知
            If strNumnotice = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_NUM_INFO_KBN - 1)) & vbCrLf
            Else
                If strNumnotice = NUMINFO_NM_UNFIN Then
                    strNumnotice = NUMINFO_KBN_UNFIN
                ElseIf strNumnotice = NUMINFO_NM_FIN Then
                    strNumnotice = NUMINFO_KBN_FIN
                End If
            End If

            'シール送付
            If strSeal = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_SEAL_SEND_KBN - 1)) & vbCrLf
            Else
                If strSeal = SEALSEND_NM_UNFIN Then
                    strSeal = SEALSEND_KBN_UNFIN
                ElseIf strSeal = SEALSEND_NM_FIN Then
                    strSeal = SEALSEND_KBN_FIN
                End If
            End If

            'ウイルス対策ソフト確認
            If strAntiVirusCon = "" Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E002, intIndex.ToString, strColNm(EXL_ANTI_VIRUS_SOFT_CHECK_KBN - 1)) & vbCrLf
            Else
                If strAntiVirusCon.ToString = ANTIVIRUSSOFCHECK_NM_UNFIN Then
                    strAntiVirusCon = ANTIVIRUSSOFCHECK_KBN_UNFIN
                ElseIf strAntiVirusCon = ANTIVIRUSSOFCHECK_NM_FIN Then
                    strAntiVirusCon = ANTIVIRUSSOFCHECK_KBN_FIN
                End If
            End If

            'ウイルス対策ソフトサーバー確認日
            '入力がある場合はチェック
            If strAntiVirusDT <> "" Then
                'YYYY/MM/DD書式チェックを行う
                If RegularExpressions.Regex.IsMatch(strAntiVirusDT, "(19|20)[0-9][0-9]/(0[1-9]|1[0-2])/(0[1-9]|[1-2][0-9]|3[0-1])") = True Then
                    '日付型チェック
                    If IsDate(strAntiVirusDT) = True Then
                        '形式変換
                        strAntiVirusDT = DateTime.Parse(strAntiVirusDT).ToString("yyyy/MM/dd")
                        strAntiVirusDT = strAntiVirusDT.Replace("/", "")
                    Else
                        'メッセージログ設定
                        strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_ANTI_VIRUS_SOFT_CHECK_DT - 1)) & vbCrLf
                    End If
                Else
                    'メッセージログ設定
                    strOutLog &= String.Format(B0204_E004, intIndex.ToString, strColNm(EXL_ANTI_VIRUS_SOFT_CHECK_DT - 1)) & vbCrLf
                End If
            End If

            '部所有機器備考（1000文字まで）
            If strKikiBiko.Length > 1000 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_BUSYO_KIKI_BIKO - 1)) & vbCrLf
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
    ''' 管理局、管理部署、IP割当種類、固定IP入力チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strManageKyoku">管理局</param>
    ''' <param name="strManageBusyo">管理部署</param>
    ''' <param name="strIpUse">IP割当種類</param>
    ''' <param name="strFixedIP">固定IP</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>管理局、管理部署、IP割当種類、固定IP入力チェックを行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function Checkinputform_Kanri(ByRef dataHBKB0204 As DataHBKB0204, ByVal Adapter As NpgsqlDataAdapter, _
                                                  ByVal Cn As NpgsqlConnection, ByRef intIndex As Integer, ByRef strManageKyoku As String, _
                                                  ByRef strManageBusyo As String, ByRef strIpUse As String, ByRef strFixedIP As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '管理局（50文字まで）
            If strManageKyoku.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_MANAGE_KYOKU_NM - 1)) & vbCrLf
            End If

            '管理部署（50文字まで）
            If strManageBusyo.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_MANAGE_BUSYO_NM - 1)) & vbCrLf
            End If

            'IP割当種類の存在チェック、変換
            If strIpUse <> "" Then
                If CheckIPUseConvert(Adapter, Cn, dataHBKB0204, intIndex, strIpUse) = False Then
                    Return False
                End If
            End If

            '固定IP（25文字まで）
            If strFixedIP.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_FIXED_IP - 1)) & vbCrLf
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
    ''' ユーザーID、ユーザー氏名、ユーザー所属会社、ユーザー所属局、ユーザー所属部署、ユーザー電話番号、ユーザーメールアドレス、ユーザー連絡先、ユーザー番組/部屋入力チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strUserID">ユーザーID</param>
    ''' <param name="strUserNM">ユーザー氏名</param>
    ''' <param name="strUserCompany">ユーザー所属会社</param>
    ''' <param name="strKyokuNM">ユーザー所属局</param>
    ''' <param name="strUserBuSyoNM">ユーザー所属部署</param>
    ''' <param name="strUserTel">ユーザー電話番号</param>
    ''' <param name="strUserMailAdd">ユーザーメールアドレス</param>
    ''' <param name="strUserContact">ユーザー連絡先</param>
    ''' <param name="strUserRoom">ユーザー番組/部屋</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ユーザーID、ユーザー氏名、ユーザー所属会社、ユーザー所属局、ユーザー所属部署、ユーザー電話番号、ユーザーメールアドレス、ユーザー連絡先、ユーザー番組/部屋入力チェックを行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function Checkinputform_User(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef strUserID As String, _
                                                  ByRef strUserNM As String, ByRef strUserCompany As String, ByRef strKyokuNM As String, _
                                                  ByRef strUserBuSyoNM As String, ByRef strUserTel As String, ByRef strUserMailAdd As String, _
                                                  ByRef strUserContact As String, ByRef strUserRoom As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 START
            'ユーザーID（50文字まで）
            If strUserID.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_ID - 1)) & vbCrLf
            End If
            ''ユーザーID（25文字まで）
            'If strUserID.Length > 25 Then
            '    'メッセージログ設定
            '    strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_ID - 1)) & vbCrLf
            'End If
            '[Mod] 2012/10/03 s.yamaguchi チェック桁数25→50へ変更 END

            'ユーザー氏名（25文字まで）
            If strUserNM.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_NM - 1)) & vbCrLf
            End If

            'ユーザー所属会社（50文字まで）
            If strUserCompany.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_COMPANY - 1)) & vbCrLf
            End If

            'ユーザー所属局（50文字まで）
            If strKyokuNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_KYOKU_NM - 1)) & vbCrLf
            End If

            'ユーザー所属部署（50文字まで）
            If strUserBuSyoNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_BUSYO_NM - 1)) & vbCrLf
            End If

            'ユーザー電話番号（25文字まで）
            If strUserTel.Length > 25 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_TEL - 1)) & vbCrLf
            End If

            'ユーザーメールアドレス（500文字まで）
            If strUserMailAdd.Length > 500 Then
                'メッセージログ設定 
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_MAIL_ADD - 1)) & vbCrLf
            End If

            'ユーザー連絡先（50文字まで）
            If strUserContact.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_CONTACT - 1)) & vbCrLf
            End If

            'ユーザー番組/部屋（100文字まで）
            If strUserRoom.Length > 100 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_USR_ROOM - 1)) & vbCrLf
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
    ''' 設置局、設置部署、設置番組/部屋、設置建物、設置フロア入力チェック
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <param name="strSetKyokuNM">設置局</param>
    ''' <param name="strSetBuSyoNM">設置部署</param>
    ''' <param name="strSetRoom">設置番組/部屋</param>
    ''' <param name="strSetBuil">設置建物</param>
    ''' <param name="strSetFloor">設置フロア</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>設置局、設置部署、設置番組/部屋、設置建物、設置フロア入力チェックを行う
    ''' <para>作成情報：2012/07/31 y.ikushima
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm_Set(ByRef dataHBKB0204 As DataHBKB0204, ByRef intIndex As Integer, ByRef strSetKyokuNM As String, _
                                                  ByRef strSetBuSyoNM As String, ByRef strSetRoom As String, ByRef strSetBuil As String, _
                                                  ByRef strSetFloor As String) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '設置局（50文字まで）
            If strSetKyokuNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SET_KYOKU_NM - 1)) & vbCrLf
            End If

            '設置部署（50文字まで）
            If strSetBuSyoNM.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SET_BUSYO_NM - 1)) & vbCrLf
            End If

            '設置番組/部屋（100文字まで）
            If strSetRoom.Length > 100 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SET_ROOM - 1)) & vbCrLf
            End If

            '設置建物（50文字まで）
            If strSetBuil.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SET_BUIL - 1)) & vbCrLf
            End If

            '設置フロア（50文字まで）
            If strSetFloor.Length > 50 Then
                'メッセージログ設定
                strOutLog &= String.Format(B0204_E003, intIndex.ToString, strColNm(EXL_SET_FLOOR - 1)) & vbCrLf
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
    ''' 番号重複チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strNum">[IN]番号</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された番号をCI共通情報テーブルからデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : 2012/07/30 y.ikushima</p>
    ''' </para></remarks>
    Public Function CheckNumPrimary(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strNum As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            '番号のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectNumSql(Adapter, Cn, dataHBKB0204, IntIndex, strNum) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "番号のデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'すでにデータが存在する場合、エラー
                If dtResult.Rows(0).Item(0) > 0 Then
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E007, IntIndex.ToString, strColNm(EXL_NUM - 1)) & vbCrLf
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
    ''' ステータスコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたステータス名をCIステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : 2012/07/30 y.ikushima</p>
    ''' </para></remarks>
    Public Function CheckStatusConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef intIndex As Integer, _
                                 ByRef strStatus As String, ByRef strStatusConvetCD As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ステータスコードのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectCountCIStateCDSql(Adapter, Cn, dataHBKB0204, intIndex, strStatus) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスコードのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strStatusConvetCD = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, intIndex.ToString, strColNm(EXL_STATUS - 1)) & vbCrLf
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
    ''' CIオーナーCD存在チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strCIOwner">[IN]CIオーナーCD</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたCIオーナーコードがグループマスターに存在するかチェックする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckCIOwnerCD(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strCIOwner As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'CIオーナーCDのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectCIOwnerCDSql(Adapter, Cn, dataHBKB0204, IntIndex, strCIOwner) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIオーナーCDのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count = 0 Then
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, IntIndex.ToString, strColNm(EXL_CI_OWNER_CD - 1)) & vbCrLf
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
    ''' OSコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strOs">[IN/OUT]入力されたソフト名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたOS名をソフトマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckOSConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strOs As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ソフトコードのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectOSSoftCDSql(Adapter, Cn, dataHBKB0204, IntIndex, strOs) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "OSソフトコードのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strOs = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, IntIndex.ToString, strColNm(EXL_OS_CD - 1)) & vbCrLf
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
    ''' ウイルス対策ソフトコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strAntiVirus">[IN/OUT]入力されたウイルス対策ソフト</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたウイルス対策ソフト名をソフトマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckAntiVirusConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strAntiVirus As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ソフトコードのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectAntiVirusSoftCDSql(Adapter, Cn, dataHBKB0204, IntIndex, strAntiVirus) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ウイルス対策ソフトコードのデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strAntiVirus = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, IntIndex.ToString, strColNm(EXL_ANTI_VIRUS_SOFT - 1)) & vbCrLf

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
    ''' DNS登録変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strDNSReg">[IN/OUT]入力されたDNS登録</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたDNS登録状態を機器ステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckDNSRegConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strDNSReg As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'DNS登録のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectDNSRegSql(Adapter, Cn, dataHBKB0204, IntIndex, strDNSReg) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "DNS登録のデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strDNSReg = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, IntIndex.ToString, strColNm(EXL_DNS_REG - 1)) & vbCrLf
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
    ''' IP割当種類変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strIpUse">[IN/OUT]IP割当種類</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたIP割当種類を機器ステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckIPUseConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKB0204 As DataHBKB0204, _
                                 ByRef IntIndex As Integer, ByRef strIpUse As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'IP割当種類のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectIPUseSql(Adapter, Cn, dataHBKB0204, IntIndex, strIpUse) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "IP割当種類のデータ有無取得", Nothing, Adapter.SelectCommand)
            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKB0204
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    strIpUse = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(B0204_E006, IntIndex.ToString, strColNm(EXL_IP_USE - 1)) & vbCrLf
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
    ''' エラーログ出力処理
    ''' </summary>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックでエラーとなった内容をログ出力する
    ''' <para>作成情報：2012/07/18 k.ueda
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetOutLog(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

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
                puErrMsg = String.Format(B0204_E001, strOutputpath)
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部署機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>登録処理を行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/05 y.ikushima（開発引継ぎ）</p>
    ''' <p>改訂情報 : 2012/07/20 k.ueda（開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function RegMain(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力データ登録処理
        If FileInputDataReg(dataHBKB0204) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力データの登録処理を行う
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputDataReg(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)                                                            'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing                                                                  'トランザクション
        Dim aryCINmb As New ArrayList                                                                           'CI番号保存用
        Dim blnErrorFlg As Boolean = False                                                                      'エラーフラグ

        Try

            '履歴Noを１で固定
            dataHBKB0204.PropIntRirekiNo = 1

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKB0204
                '取込番号分ループ
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1


                    '新規CI番号取得
                    If SelectNewCINmb(Cn, dataHBKB0204) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI共通情報新規追加
                    If InsertCIInfo(Cn, dataHBKB0204, i) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If
                    'CI共通情報履歴情報新規追加
                    If InsertCIINfoR(Cn, dataHBKB0204) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI部所有機器新規追加
                    If InsertCIBuy(Cn, dataHBKB0204, i) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    'CI部所有機器履歴テーブル登録
                    If InsertCIBuyR(Cn, dataHBKB0204) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '登録理由履歴新規追加
                    If InsertRegReasonR(Cn, dataHBKB0204) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                    '原因リンク履歴情報新規追加
                    If InsertCauseLinkR(Cn, dataHBKB0204) = False Then
                        'ロールバック
                        If Tsx IsNot Nothing Then
                            Tsx.Rollback()
                        End If
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit For
                    End If

                Next

            End With


            'エラーフラグがONの場合、Falseを返す
            If blnErrorFlg = True Then
                Tsx.Rollback()
                Return False
            Else
                'コミット
                Tsx.Commit()
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
            'ネクションが閉じられていない場合は閉じる
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したCI番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewCINmb(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規CI番号取得（SELECT）用SQLを作成
            If sqlHBKB0204.SetSelectNewCINmbAndSysDateSql(Adapter, Cn, dataHBKB0204) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規CI番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0204.PropIntCINmb = dtResult.Rows(0).Item("CINmb")      '新規CI番号
                dataHBKB0204.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = B0204_E009
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0204 As DataHBKB0204, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0204.SetInsertCIInfoSql(Cmd, Cn, dataHBKB0204, intIndex) = False Then
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
            'ロールバック
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIINfoR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0204.SetInsertCIInfoRSql(Cmd, Cn, dataHBKB0204) = False Then
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
    ''' 登録理由履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録理由履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertRegReasonR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0204.SetInsertRegReasonRSql(Cmd, Cn, dataHBKB0204) = False Then
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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンク履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCauseLinkR(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ数分繰り返し、登録を行う
            For i As Integer = 0 To dataHBKB0204.PropDtCauseLink.Rows.Count - 1
                '登録条件セット
                dataHBKB0204.PropStrMngNmb = dataHBKB0204.PropDtCauseLink.Rows(i).Item("MngNmb")
                dataHBKB0204.PropStrProcessKbn = dataHBKB0204.PropDtCauseLink.Rows(i).Item("ProcessKbn")
                'SQLを作成
                If sqlHBKB0204.SetInsertCauseLinkRSql(Cmd, Cn, dataHBKB0204) = False Then
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
    ''' CI部所有機器新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0204">[IN]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をCI部所有機器テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIBuy(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKB0204 As DataHBKB0204, _
                                    ByVal intIndex As Integer) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'CI部所有機器新規登録（INSERT）用SQLを作成
            If sqlHBKB0204.SetInsertCIBuySql(Cmd, Cn, dataHBKB0204, intIndex) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器新規登録", Nothing, Cmd)

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
    ''' CI部所有機器履歴テーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0204">[IN]一括登録　部所有機器Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI部所有機器履歴テーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCIBuyR(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKB0204.SetInsertCIBuyRSql(Cmd, Cn, dataHBKB0204) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI部所有機器履歴新規登録", Nothing, Cmd)

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
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>取込データの改行コードを変換する
    ''' <para>作成情報：2012/09/21 s.yamaguchi 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ChangeToVbCrLfForBuy(ByRef dataHBKB0204 As DataHBKB0204) As Boolean

        'ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0204

                '改行コードを再設定
                For i As Integer = 0 To .PropAryRowCount.Count - 1 Step 1

                    .PropAryTorikomiNum(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryTorikomiNum(i))                     '取込管理番号
                    .PropAryNum(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryNum(i))                                     '番号
                    .PropAryGrouping1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryGrouping1(i))                         '分類１
                    .PropAryGrouping2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryGrouping2(i))                         '分類２
                    .PropAryTitle(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryTitle(i))                                 '名称
                    .PropAryStatsu(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryStatsu(i))                               'ステータス
                    .PropAryCIOwnerCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryCIOwnerCD(i))                         'CIオーナー
                    .PropAryExplanation(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryExplanation(i))                     '説明
                    .PropAryFreeText1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeText1(i))                         'フリーテキスト1
                    .PropAryFreeText2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeText2(i))                         'フリーテキスト2
                    .PropAryFreeText3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeText3(i))                         'フリーテキスト3
                    .PropAryFreeText4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeText4(i))                         'フリーテキスト4
                    .PropAryFreeText5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeText5(i))                         'フリーテキスト5
                    .PropAryFreeFlg1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg1(i))                           'フリーフラグ1
                    .PropAryFreeFlg2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg2(i))                           'フリーフラグ2
                    .PropAryFreeFlg3(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg3(i))                           'フリーフラグ3
                    .PropAryFreeFlg4(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg4(i))                           'フリーフラグ4
                    .PropAryFreeFlg5(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFreeFlg5(i))                           'フリーフラグ5
                    .PropAryKataban(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryKataban(i))                             '型番
                    .PropAryAliau(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryAliau(i))                                 'エイリアス
                    .PropArySerial(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySerial(i))                               '製造番号
                    .PropAryMacAddress1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryMacAddress1(i))                     'MACアドレス1
                    .PropAryMacAddress2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryMacAddress2(i))                     'MACアドレス2
                    .PropAryZooKbn(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryZooKbn(i))                               'zoo参加有無
                    .PropAryOSNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryOSNM(i))                                   'OS
                    .PropAryAntiVirusSoftNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryAntiVirusSoftNM(i))             'ウイルス対策ソフト
                    .PropAryDNSRegCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryDNSRegCD(i))                           'DNS登録
                    .PropAryNIC1(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryNIC1(i))                                   'NIC1
                    .PropAryNIC2(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryNIC2(i))                                   'NIC2
                    .PropAryConnectDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryConnectDT(i))                         '接続日
                    .PropAryExpirationDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryExpirationDT(i))                   '有効日
                    .PropAryDeletDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryDeletDT(i))                             '停止日
                    .PropAryLastInfoDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryLastInfoDT(i))                       '最終お知らせ日
                    .PropAryConnectReason(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryConnectReason(i))                 '接続理由
                    .PropAryExpirationUPDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryExpirationUPDT(i))               '更新日
                    .PropAryInfoDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryInfoDT(i))                               '通知日
                    .PropAryNumInfoKbn(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryNumInfoKbn(i))                       '番号通知
                    .PropArySealSendkbn(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySealSendkbn(i))                     'シール送付
                    .PropAryAntiVirusSoftCheckKbn(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryAntiVirusSoftCheckKbn(i)) 'ウイルス対策ソフト確認
                    .PropAryAntiVirusSoftCheckDT(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryAntiVirusSoftCheckDT(i))   'ウイルス対策ソフトサーバー確認日
                    .PropAryBusyoKikiBiko(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryBusyoKikiBiko(i))                 '部所有機器備考
                    .PropAryManageKyokuNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryManageKyokuNM(i))                 '管理局
                    .PropAryManageBusyoNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryManageBusyoNM(i))                 '管理部署
                    .PropAryIPUseCD(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryIPUseCD(i))                             'IP割当種類
                    .PropAryFixedIP(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryFixedIP(i))                             '固定IP
                    .PropAryUsrID(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrID(i))                                 'ユーザーID
                    .PropAryUsrNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrNM(i))                                 'ユーザー氏名
                    .PropAryUsrCompany(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrCompany(i))                       'ユーザー所属会社
                    .PropAryUsrKyokuNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrKyokuNM(i))                       'ユーザー所属局
                    .PropAryUsrBusyoNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrBusyoNM(i))                       'ユーザー所属部署
                    .PropAryUsrTel(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrTel(i))                               'ユーザー電話番号
                    .PropAryUsrMailAdd(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrMailAdd(i))                       'ユーザーメールアドレス
                    .PropAryUsrContact(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrContact(i))                       'ユーザー連絡先
                    .PropAryUsrRoom(i) = commonLogicHBK.ChangeToVbCrLf(.PropAryUsrRoom(i))                             'ユーザー番号/部屋
                    .PropArySetKyokuNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySetKyokuNM(i))                       '設置局
                    .PropArySetBusyoNM(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySetBusyoNM(i))                       '設置部署
                    .PropArySetRoom(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySetRoom(i))                             '設置番組/部屋
                    .PropArySetBuil(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySetBuil(i))                             '設置建物
                    .PropArySetFloor(i) = commonLogicHBK.ChangeToVbCrLf(.PropArySetFloor(i))                           '設置フロア

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
