Imports Common
Imports CommonHBK
Imports Npgsql
Imports Microsoft.Office.Interop
Imports System.IO
Imports System.Text

''' <summary>
''' 一括登録画面ロジッククラス
''' </summary>
''' <remarks>一括登録画面のロジックを定義したクラス
''' <para>作成情報：2012/07/24 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0601

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKC0601 As New SqlHBKC0601

    'Public定数宣言
    'CSVのスタート行
    Public Const CSV_START_ROW As Integer = 1
    'CSVの項目インデックス
    Public Const CSV_ACQUISITION_NUM As Integer = 0                 'No
    Public Const CSV_UKEKBNCD_NUM As Integer = 1                    '受付手段
    Public Const CSV_INCKBNCD_NUM As Integer = 2                    'インシデント種別
    Public Const CSV_PROCESSSTATUSCD_NUM As Integer = 3             'ステータス
    Public Const CSV_HASSEIDT_NUM As Integer = 4                    '発生日時
    Public Const CSV_KAITODT_NUM As Integer = 5                     '回答日時
    Public Const CSV_KANRYODT_NUM As Integer = 6                    '完了日時
    Public Const CSV_PRIORITY_NUM As Integer = 7                    '重要度
    Public Const CSV_ERRLEVEL_NUM As Integer = 8                    '障害レベル
    Public Const CSV_TITLE_NUM As Integer = 9                       'タイトル
    Public Const CSV_UKENAIYO_NUM As Integer = 10                   '受付内容
    Public Const CSV_TAIOKEKKA_NUM As Integer = 11                  '対応結果
    Public Const CSV_SYSTEMNMB_NUM As Integer = 12                  '対象システム
    Public Const CSV_OUTSIDETOOLNMB_NUM As Integer = 13             '外部ツール番号
    Public Const CSV_EVENTID_NUM As Integer = 14                    'イベントID
    Public Const CSV_SOURCE_NUM As Integer = 15                     'ソース
    Public Const CSV_OPCEVENTID_NUM As Integer = 16                 'OPCイベントID
    Public Const CSV_EVENTCLASS_NUM As Integer = 17                 'イベントクラス
    Public Const CSV_TANTOGRPCD_NUM As Integer = 18                 '担当者業務チーム
    Public Const CSV_INCTANTOID_NUM As Integer = 19                 '担当者ID
    Public Const CSV_INCTANTONM_NUM As Integer = 20                 'インシデント担当者
    Public Const CSV_DOMAINCD_NUM As Integer = 21                   'ドメイン
    Public Const CSV_PARTNERCOMPANY_NUM As Integer = 22             '相手会社名
    Public Const CSV_PARTNERID_NUM As Integer = 23                  '相手ID
    Public Const CSV_PARTNERNM_NUM As Integer = 24                  '相手氏名
    Public Const CSV_PARTNERKANA_NUM As Integer = 25                '相手シメイ
    Public Const CSV_PARTNERKYOKUNM_NUM As Integer = 26             '相手局
    Public Const CSV_USRBUSYONM_NUM As Integer = 27                 '相手部署
    Public Const CSV_PARTNERTEL_NUM As Integer = 28                 '相手電話番号
    Public Const CSV_PARTNERMAILADD_NUM As Integer = 29             '相手メールアドレス
    Public Const CSV_PARTNERCONTACT_NUM As Integer = 30             '相手連絡先
    Public Const CSV_PARTNERBASE_NUM As Integer = 31                '相手拠点
    Public Const CSV_PARTNERROOM_NUM As Integer = 32                '相手番組/部屋
    Public Const CSV_SHIJISYOFLG_NUM As Integer = 33                '指示書
    Public Const CSV_KINDCD_NUM As Integer = 34                     '機器種別
    Public Const CSV_NUM_NUM As Integer = 35                        '機器番号
    Public Const CSV_KEIKAKBNCD_NUM As Integer = 36                 '経過種別
    Public Const CSV_SYSTEMNMB2_NUM As Integer = 37                 '対象システム（作業内容）
    Public Const CSV_WORKSCEDT_NUM As Integer = 38                  '作業予定日時
    Public Const CSV_WORKSTDT_NUM As Integer = 39                   '作業開始日時
    Public Const CSV_WORKEDDT_NUM As Integer = 40                   '作業終了日時
    Public Const CSV_WORKNAIYO_NUM As Integer = 41                  '作業内容
    Public Const CSV_WORKTANTOGRPCD1_NUM As Integer = 42            '作業担当者業務チーム1
    Public Const CSV_WORKTANTOID1_NUM As Integer = 43               '作業担当者ID1
    Public Const CSV_WORKTANTONM1_NUM As Integer = 44               '作業担当者1
    Public Const CSV_WORKTANTOGRPCD2_NUM As Integer = 45            '作業担当者業務チーム2
    Public Const CSV_WORKTANTOID2_NUM As Integer = 46               '作業担当者ID2
    Public Const CSV_WORKTANTONM2_NUM As Integer = 47               '作業担当者2
    Public Const CSV_WORKTANTOGRPCD3_NUM As Integer = 48            '作業担当者業務チーム3
    Public Const CSV_WORKTANTOID3_NUM As Integer = 49               '作業担当者ID3
    Public Const CSV_WORKTANTONM3_NUM As Integer = 50               '作業担当者3
    Public Const CSV_WORKTANTOGRPCD4_NUM As Integer = 51            '作業担当者業務チーム4
    Public Const CSV_WORKTANTOID4_NUM As Integer = 52               '作業担当者ID4
    Public Const CSV_WORKTANTONM4_NUM As Integer = 53               '作業担当者4
    Public Const CSV_WORKTANTOGRPCD5_NUM As Integer = 54            '作業担当者業務チーム5
    Public Const CSV_WORKTANTOID5_NUM As Integer = 55               '作業担当者ID5
    Public Const CSV_WORKTANTONM5_NUM As Integer = 56               '作業担当者5
    'CSVファイル項目数
    Public Const CSV_COL_COUNT As Integer = 57

    '列名配列
    Private strColNm As String() = COLUMNNAME_INC

    'ログ出力文言
    Private strOutLog As String

    'チェック実行判定用
    Private strTorikomiNum_Bef As String = ""                       '前行の取込番号
    Private strTorikomiNum_Cur As String = ""                       'カレント取込番号
    Private blnRegImpAry As Boolean                                 '取込データ配列追加フラグ
    Private blnRegWorkRireki As Boolean                             '作業履歴登録フラグ
    Private AryCheckNo As New ArrayList                             '取込Noチェック用


    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If InputCheck(dataHBKC0601) = False Then
            Return False
        End If

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/07/24 k.imayama  
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InputCheck(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数を宣言
        Dim strFilePath As String = dataHBKC0601.PropTxtFilePath.Text   'ファイルパス
        Dim strFileExt As String = ""

        Try
            'ファイル未選択チェック
            If strFilePath = "" Then
                'エラーメッセージセット
                puErrMsg = C0601_E001
                Return False
            End If

            'ファイル拡張子取得
            strFileExt = System.IO.Path.GetExtension(strFilePath)

            '拡張子チェック
            If strFileExt = EXTENTION_CSV Then
            Else
                'エラーメッセージセット
                puErrMsg = C0601_E002
                Return False
            End If

            '取込ファイルの存在チェック
            If System.IO.File.Exists(strFilePath) = False Then
                'エラーメッセージセット
                puErrMsg = C0601_E003
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try

    End Function

    ''' <summary>
    ''' ファイル入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputCheckMain(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力ファイルチェック処理
        If FileInputCheck(dataHBKC0601) = False Then
            Return False
        End If

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' ファイル入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行い、入力チェックエラーが発生するとログファイルに書き込む
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputCheck(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログ文字列初期化
            strOutLog = ""

            '入力チェック
            If FileCheck(dataHBKC0601) = False Then
                Return False
            End If

            '入力チェックエラー時にログ出力用変数にデータがある場合ログ出力画面へ
            If strOutLog <> "" Then
                'ログ出力処理
                If SetOutLog(dataHBKC0601) = False Then
                    Return False
                End If
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
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
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックを行う
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileCheck(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '入力チェック用配列取得・入力チェック
            If SetArryInputForCheck(dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
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
    ''' 入力チェック用配列セット・入力項目必須チェック、入力項目重複チェック処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェック用の配列をExcelからセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetArryInputForCheck(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strBuffer As String = ""                                                '読込行
        Dim txtParser As Microsoft.VisualBasic.FileIO.TextFieldParser = Nothing     'CSVファイル読込用クラス
        Dim strFilePath As String                                                   '取込対象ファイル
        Dim strAryBuffer As String() = Nothing                                      '読込行データ格納用配列

        '入力チェック用
        Dim blnErrorFlg As Boolean = False                                          '入力チェック用フラグ用
        Dim blnRegImpAry As Boolean = False                                         '配列追加フラグ

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)                                    'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                                        'アダプタ

        '保存用配列初期化
        With dataHBKC0601
            .PropAryRowCount = New ArrayList                    '行番号
            .PropAryTorikomiNum = New ArrayList                 'No
            .PropAryUkeKbnCD = New ArrayList                    '受付手段
            .PropAryIncKbnCD = New ArrayList                    'インシデント種別
            .PropAryProcessStatusCD = New ArrayList             'ステータス
            .PropAryHasseiDT = New ArrayList                    '発生日時
            .PropAryKaitoDT = New ArrayList                     '回答日時
            .PropAryKanryoDT = New ArrayList                    '完了日時
            .PropAryPriority = New ArrayList                    '重要度
            .PropAryErrLevel = New ArrayList                    '障害レベル
            .PropAryTitle = New ArrayList                       'タイトル
            .PropAryUkeNaiyo = New ArrayList                    '受付内容
            .PropAryTaioKekka = New ArrayList                   '対応結果
            .PropArySystemNmb = New ArrayList                   '対象システム
            .PropAryOutSideToolNmb = New ArrayList              '外部ツール番号
            .PropAryEventID = New ArrayList                     'イベントID
            .PropArySource = New ArrayList                      'ソース
            .PropAryOPCEventID = New ArrayList                  'OPCイベントID
            .PropAryEventClass = New ArrayList                  'イベントクラス
            .PropAryTantoGrpCD = New ArrayList                  '担当者業務チーム
            .PropAryIncTantoID = New ArrayList                  '担当者ID
            .PropAryIncTantoNM = New ArrayList                  'インシデント担当者
            .PropAryDomainCD = New ArrayList                    'ドメイン
            .PropAryPartnerCompany = New ArrayList              '相手会社名
            .PropAryPartnerID = New ArrayList                   '相手ID
            .PropAryPartnerNM = New ArrayList                   '相手氏名
            .PropAryPartnerKana = New ArrayList                 '相手シメイ
            .PropAryPartnerKyokuNM = New ArrayList              '相手局
            .PropAryUsrBusyoNM = New ArrayList                  '相手部署
            .PropAryPartnerTel = New ArrayList                  '相手電話番号
            .PropAryPartnerMailAdd = New ArrayList              '相手メールアドレス
            .PropAryPartnerContact = New ArrayList              '相手連絡先
            .PropAryPartnerBase = New ArrayList                 '相手拠点
            .PropAryPartnerRoom = New ArrayList                 '相手番組/部屋
            .PropAryShijisyoFlg = New ArrayList                 '指示書
            .PropAryKindCD = New ArrayList                      '機器種別
            .PropAryNum = New ArrayList                         '機器番号
            .PropAryKeikaKbnCD = New ArrayList                  '経過種別
            .PropArySystemNmb2 = New ArrayList                  '対象システム（作業内容）
            .PropAryWorkSceDT = New ArrayList                   '作業予定日時
            .PropAryWorkStDT = New ArrayList                    '作業開始日時
            .PropAryWorkEdDT = New ArrayList                    '作業終了日時
            .PropAryWorkNaiyo = New ArrayList                   '作業内容
            .PropAryWorkTantoGrpCD1 = New ArrayList             '作業担当者業務チーム1
            .PropAryWorkTantoID1 = New ArrayList                '作業担当者ID1
            .PropAryWorkTantoNM1 = New ArrayList                '作業担当者1
            .PropAryWorkTantoGrpCD2 = New ArrayList             '作業担当者業務チーム2
            .PropAryWorkTantoID2 = New ArrayList                '作業担当者ID2
            .PropAryWorkTantoNM2 = New ArrayList                '作業担当者2
            .PropAryWorkTantoGrpCD3 = New ArrayList             '作業担当者業務チーム3
            .PropAryWorkTantoID3 = New ArrayList                '作業担当者ID3
            .PropAryWorkTantoNM3 = New ArrayList                '作業担当者3
            .PropAryWorkTantoGrpCD4 = New ArrayList             '作業担当者業務チーム4
            .PropAryWorkTantoID4 = New ArrayList                '作業担当者ID4
            .PropAryWorkTantoNM4 = New ArrayList                '作業担当者4
            .PropAryWorkTantoGrpCD5 = New ArrayList             '作業担当者業務チーム5
            .PropAryWorkTantoID5 = New ArrayList                '作業担当者ID5
            .PropAryWorkTantoNM5 = New ArrayList                '作業担当者5

            .PropAryKikiCINmb = New ArrayList                   '機器CI番号

            .PropAryRegWorkFlg = New ArrayList                  '作業履歴追加フラグ
        End With

        'チェック実行判定用Private変数初期化
        strTorikomiNum_Bef = ""
        strTorikomiNum_Cur = ""
        AryCheckNo.Clear()

        Try
            '取込対象ファイルパスを変数にセット
            strFilePath = dataHBKC0601.PropTxtFilePath.Text

            'CSV読込クラスのインスタンス作成
            txtParser = New Microsoft.VisualBasic.FileIO.TextFieldParser(strFilePath, System.Text.Encoding.Default)
            'プロパティセット
            With txtParser
                txtParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited  '内容は区切り文字形式
                txtParser.SetDelimiters(",")                                                'デリミタはカンマ
            End With

            'ループカウンタセット
            Dim Count As Integer = CSV_START_ROW
            '取込番号毎のカウンタセット
            Dim intCountSameNo As Integer = 0

            'コネクションを開く
            Cn.Open()

            '読み込む行がなくなるまで繰り返し
            While Not txtParser.EndOfData

                '1行を読み込んで配列に格納
                strAryBuffer = txtParser.ReadFields()

                '改行コード変換　※これをしないと画面上で改行コードが表示されない
                If strAryBuffer.Count > 0 Then
                    For i As Integer = 0 To strAryBuffer.Count - 1
                        Dim str As String = strAryBuffer(i)
                        strAryBuffer(i) = commonLogicHBK.ChangeToVbCrLf(str)
                    Next
                End If

                '読込行の項目数が規定に満たない場合、配列を再定義して項目追加
                If strAryBuffer.Count < CSV_COL_COUNT Then
                    Dim intDiffCnt As Integer = CSV_COL_COUNT - strAryBuffer.Count - 1
                    ReDim Preserve strAryBuffer(CSV_COL_COUNT - 1)
                    For i As Integer = CSV_COL_COUNT - 1 - intDiffCnt To CSV_COL_COUNT - 1
                        strAryBuffer(i) = ""
                    Next
                End If

                '今回取込番号取得
                strTorikomiNum_Cur = strAryBuffer(CSV_ACQUISITION_NUM)

                '配列追加フラグ、作業履歴登録フラグ初期化
                blnRegImpAry = False
                blnRegWorkRireki = False

                'データクラスに保存
                With dataHBKC0601

                    '前回取込番号と今回取込番号が異なる場合、配列追加フラグをON
                    If strTorikomiNum_Bef <> strTorikomiNum_Cur Then
                        blnRegImpAry = True
                        '取込番号毎のカウンタを初期化
                        intCountSameNo = 0
                    Else
                        '取込番号毎のカウンタをカウントアップ
                        intCountSameNo += 1
                    End If

                    '作業履歴に1項目でも入力がある場合は配列追加フラグと作業履歴登録フラグをON
                    If strAryBuffer(CSV_KEIKAKBNCD_NUM) <> "" Or strAryBuffer(CSV_SYSTEMNMB2_NUM) <> "" Or strAryBuffer(CSV_WORKSCEDT_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKSTDT_NUM) <> "" Or strAryBuffer(CSV_WORKEDDT_NUM) <> "" Or strAryBuffer(CSV_WORKNAIYO_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKTANTOGRPCD1_NUM) <> "" Or strAryBuffer(CSV_WORKTANTOID1_NUM) <> "" Or strAryBuffer(CSV_WORKTANTONM1_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKTANTOGRPCD2_NUM) <> "" Or strAryBuffer(CSV_WORKTANTOID2_NUM) <> "" Or strAryBuffer(CSV_WORKTANTONM2_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKTANTOGRPCD3_NUM) <> "" Or strAryBuffer(CSV_WORKTANTOID3_NUM) <> "" Or strAryBuffer(CSV_WORKTANTONM3_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKTANTOGRPCD4_NUM) <> "" Or strAryBuffer(CSV_WORKTANTOID4_NUM) <> "" Or strAryBuffer(CSV_WORKTANTONM4_NUM) <> "" Or _
                       strAryBuffer(CSV_WORKTANTOGRPCD5_NUM) <> "" Or strAryBuffer(CSV_WORKTANTOID5_NUM) <> "" Or strAryBuffer(CSV_WORKTANTONM5_NUM) <> "" Then
                        blnRegImpAry = True
                        blnRegWorkRireki = True
                        '取込番号毎のカウンタをマイナス
                        If intCountSameNo > 0 Then
                            intCountSameNo -= 1
                        End If
                    End If

                    '作業履歴フラグがONの場合、配列に取得データを追加し、入力チェックを行う
                    If blnRegImpAry = True Then

                        .PropAryRowCount.Add(Count)                                         '行番号

                        .PropAryTorikomiNum.Add(strTorikomiNum_Cur)                         'No
                        .PropAryUkeKbnCD.Add(strAryBuffer(CSV_UKEKBNCD_NUM))                '受付手段
                        .PropAryIncKbnCD.Add(strAryBuffer(CSV_INCKBNCD_NUM))                'インシデント種別
                        .PropAryProcessStatusCD.Add(strAryBuffer(CSV_PROCESSSTATUSCD_NUM))  'ステータス
                        .PropAryHasseiDT.Add(strAryBuffer(CSV_HASSEIDT_NUM))                '発生日時
                        .PropAryKaitoDT.Add(strAryBuffer(CSV_KAITODT_NUM))                  '回答日時
                        .PropAryKanryoDT.Add(strAryBuffer(CSV_KANRYODT_NUM))                '完了日時
                        .PropAryPriority.Add(strAryBuffer(CSV_PRIORITY_NUM))                '重要度
                        .PropAryErrLevel.Add(strAryBuffer(CSV_ERRLEVEL_NUM))                '障害レベル
                        .PropAryTitle.Add(strAryBuffer(CSV_TITLE_NUM))                      'タイトル
                        .PropAryUkeNaiyo.Add(strAryBuffer(CSV_UKENAIYO_NUM))                '受付内容
                        .PropAryTaioKekka.Add(strAryBuffer(CSV_TAIOKEKKA_NUM))              '対応結果
                        .PropArySystemNmb.Add(strAryBuffer(CSV_SYSTEMNMB_NUM))              '対象システム
                        .PropAryOutSideToolNmb.Add(strAryBuffer(CSV_OUTSIDETOOLNMB_NUM))    '外部ツール番号
                        .PropAryEventID.Add(strAryBuffer(CSV_EVENTID_NUM))                  'イベントID
                        .PropArySource.Add(strAryBuffer(CSV_SOURCE_NUM))                    'ソース
                        .PropAryOPCEventID.Add(strAryBuffer(CSV_OPCEVENTID_NUM))            'OPCイベントID
                        .PropAryEventClass.Add(strAryBuffer(CSV_EVENTCLASS_NUM))            'イベントクラス
                        .PropAryTantoGrpCD.Add(strAryBuffer(CSV_TANTOGRPCD_NUM))            '担当者業務チーム
                        .PropAryIncTantoID.Add(strAryBuffer(CSV_INCTANTOID_NUM))            '担当者ID
                        .PropAryIncTantoNM.Add(strAryBuffer(CSV_INCTANTONM_NUM))            'インシデント担当者
                        .PropAryDomainCD.Add(strAryBuffer(CSV_DOMAINCD_NUM))                'ドメイン
                        .PropAryPartnerCompany.Add(strAryBuffer(CSV_PARTNERCOMPANY_NUM))    '相手会社名
                        .PropAryPartnerID.Add(strAryBuffer(CSV_PARTNERID_NUM))              '相手ID
                        .PropAryPartnerNM.Add(strAryBuffer(CSV_PARTNERNM_NUM))              '相手氏名
                        .PropAryPartnerKana.Add(strAryBuffer(CSV_PARTNERKANA_NUM))          '相手シメイ
                        .PropAryPartnerKyokuNM.Add(strAryBuffer(CSV_PARTNERKYOKUNM_NUM))    '相手局
                        .PropAryUsrBusyoNM.Add(strAryBuffer(CSV_USRBUSYONM_NUM))            '相手部署
                        .PropAryPartnerTel.Add(strAryBuffer(CSV_PARTNERTEL_NUM))            '相手電話番号
                        .PropAryPartnerMailAdd.Add(strAryBuffer(CSV_PARTNERMAILADD_NUM))    '相手メールアドレス
                        .PropAryPartnerContact.Add(strAryBuffer(CSV_PARTNERCONTACT_NUM))    '相手連絡先
                        .PropAryPartnerBase.Add(strAryBuffer(CSV_PARTNERBASE_NUM))          '相手拠点
                        .PropAryPartnerRoom.Add(strAryBuffer(CSV_PARTNERROOM_NUM))          '相手番組/部屋
                        .PropAryShijisyoFlg.Add(strAryBuffer(CSV_SHIJISYOFLG_NUM))          '指示書
                        .PropAryKindCD.Add(strAryBuffer(CSV_KINDCD_NUM))                    '機器種別
                        .PropAryNum.Add(strAryBuffer(CSV_NUM_NUM))                          '機器番号

                        .PropAryKeikaKbnCD.Add(strAryBuffer(CSV_KEIKAKBNCD_NUM))            '経過種別
                        .PropArySystemNmb2.Add(strAryBuffer(CSV_SYSTEMNMB2_NUM))            '対象システム（作業内容）
                        .PropAryWorkSceDT.Add(strAryBuffer(CSV_WORKSCEDT_NUM))              '作業予定日時
                        .PropAryWorkStDT.Add(strAryBuffer(CSV_WORKSTDT_NUM))                '作業開始日時
                        .PropAryWorkEdDT.Add(strAryBuffer(CSV_WORKEDDT_NUM))                '作業終了日時
                        .PropAryWorkNaiyo.Add(strAryBuffer(CSV_WORKNAIYO_NUM))              '作業内容
                        .PropAryWorkTantoGrpCD1.Add(strAryBuffer(CSV_WORKTANTOGRPCD1_NUM))  '作業担当者業務チーム1
                        .PropAryWorkTantoID1.Add(strAryBuffer(CSV_WORKTANTOID1_NUM))        '作業担当者ID1
                        .PropAryWorkTantoNM1.Add(strAryBuffer(CSV_WORKTANTONM1_NUM))        '作業担当者1
                        .PropAryWorkTantoGrpCD2.Add(strAryBuffer(CSV_WORKTANTOGRPCD2_NUM))  '作業担当者業務チーム2
                        .PropAryWorkTantoID2.Add(strAryBuffer(CSV_WORKTANTOID2_NUM))        '作業担当者ID2
                        .PropAryWorkTantoNM2.Add(strAryBuffer(CSV_WORKTANTONM2_NUM))        '作業担当者2
                        .PropAryWorkTantoGrpCD3.Add(strAryBuffer(CSV_WORKTANTOGRPCD3_NUM))  '作業担当者業務チーム3
                        .PropAryWorkTantoID3.Add(strAryBuffer(CSV_WORKTANTOID3_NUM))        '作業担当者ID3
                        .PropAryWorkTantoNM3.Add(strAryBuffer(CSV_WORKTANTONM3_NUM))        '作業担当者3
                        .PropAryWorkTantoGrpCD4.Add(strAryBuffer(CSV_WORKTANTOGRPCD4_NUM))  '作業担当者業務チーム4
                        .PropAryWorkTantoID4.Add(strAryBuffer(CSV_WORKTANTOID4_NUM))        '作業担当者ID4
                        .PropAryWorkTantoNM4.Add(strAryBuffer(CSV_WORKTANTONM4_NUM))        '作業担当者4
                        .PropAryWorkTantoGrpCD5.Add(strAryBuffer(CSV_WORKTANTOGRPCD5_NUM))  '作業担当者業務チーム5
                        .PropAryWorkTantoID5.Add(strAryBuffer(CSV_WORKTANTOID5_NUM))        '作業担当者ID5
                        .PropAryWorkTantoNM5.Add(strAryBuffer(CSV_WORKTANTONM5_NUM))        '作業担当者5

                        .PropAryKikiCINmb.Add(0)                                            '機器CI番号

                        .PropAryRegWorkFlg.Add(blnRegWorkRireki)                            '作業履歴追加フラグ


                        '入力項目必須、桁数、形式、存在チェック
                        If CheckInputForm(Adapter, Cn, dataHBKC0601, .PropAryTorikomiNum.Count - 1, intCountSameNo) = False Then
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit While
                        End If

                    End If

                End With


                'カウンタインクリメント
                Count += 1

                '前回取込番号をカレント取込番号で更新
                strTorikomiNum_Bef = strTorikomiNum_Cur

            End While

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            'フラグによって戻り値を設定する
            If blnErrorFlg = True Then
                Return False
            Else
                '正常終了
                Return True
            End If

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'オブジェクト解放
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
                Cn.Dispose()
            End If
            If txtParser IsNot Nothing Then
                txtParser.Close()
                txtParser.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 入力項目必須、桁数、形式、存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目の必須、桁数、形式、存在チェックを行う
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 :2012/09/04 t.fukuo 受付手段が「メール自動発報」時のみの必須チェック削除</p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0601 As DataHBKC0601, _
                                    ByRef intIndex As Integer, _
                                    ByVal intCountSameNo As Integer) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0601

                'No（取込番号）の必須チェック
                If .PropAryTorikomiNum(intIndex) = "" Then
                    'メッセージログ設定
                    strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_ACQUISITION_NUM)) & vbCrLf
                End If

                '前回取込番号と今回取込番号が異なる場合のみチェック
                If strTorikomiNum_Bef <> strTorikomiNum_Cur Then

                    '取込番号チェック
                    If strTorikomiNum_Cur <> "" Then
                        If AryCheckNo.Count = 0 Then
                            '登録済みリストに登録
                            AryCheckNo.Add(strTorikomiNum_Cur)
                        Else
                            If AryCheckNo.IndexOf(strTorikomiNum_Cur) <> -1 Then
                                '過去に取込した番号あり
                                puErrMsg = String.Format(C0601_E007, .PropAryRowCount(intIndex), "取込番号")
                                Return False
                            Else
                                AryCheckNo.Add(strTorikomiNum_Cur)
                            End If
                        End If
                    End If

                    '受付手段の存在チェック、変換
                    If .PropAryUkeKbnCD(intIndex) <> "" Then
                        If CheckUketsukewayConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                            Return False
                        End If
                    End If

                        'インシデント種別の存在チェック、変換
                        If .PropAryIncKbnCD(intIndex) <> "" Then
                            If CheckIncidentKindConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        'ステータスの存在チェック、変換
                        If .PropAryProcessStatusCD(intIndex) <> "" Then
                            If CheckProcessStateConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        'ステータスが完了の場合、受付手段の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryUkeKbnCD(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_UKEKBNCD_NUM)) & vbCrLf
                        End If

                        'ステータスが完了の場合、インシデント種別の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryIncKbnCD(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_INCKBNCD_NUM)) & vbCrLf
                        End If

                        'ステータスが完了の場合、発生日時の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryHasseiDT(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_HASSEIDT_NUM)) & vbCrLf
                        End If
                        '発生日時のフォーマットチェック
                        If .PropAryHasseiDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryHasseiDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_HASSEIDT_NUM)) & vbCrLf
                            End If
                        End If

                        '回答日時のフォーマットチェック
                        If .PropAryKaitoDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryKaitoDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_KAITODT_NUM)) & vbCrLf
                            End If
                        End If

                        'ステータスが完了の場合、完了日時の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryKanryoDT(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_KANRYODT_NUM)) & vbCrLf
                        End If
                        '完了日時のフォーマットチェック
                        If .PropAryKanryoDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryKanryoDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_KANRYODT_NUM)) & vbCrLf
                            End If
                        End If

                        '重要度（10文字まで）
                    If .PropAryPriority(intIndex).ToString.Length > 10 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PRIORITY_NUM)) & vbCrLf
                    End If

                        '障害レベル（50文字まで）
                    If .PropAryErrLevel(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_ERRLEVEL_NUM)) & vbCrLf
                    End If

                        'ステータスが完了の場合、タイトルの必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryTitle(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_TITLE_NUM)) & vbCrLf
                        End If
                        'タイトル（100文字まで）
                    If .PropAryTitle(intIndex).ToString.Length > 100 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_TITLE_NUM)) & vbCrLf
                    End If

                    'ステータスが完了の場合、受付内容の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryUkeNaiyo(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_UKENAIYO_NUM)) & vbCrLf
                        End If
                    '受付内容（3000文字まで）
                    If .PropAryUkeNaiyo(intIndex).ToString.Length > 3000 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_UKENAIYO_NUM)) & vbCrLf
                    End If

                        'ステータスが完了の場合、対応結果の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryTaioKekka(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_TAIOKEKKA_NUM)) & vbCrLf
                        End If
                    '対応結果（3000文字まで）
                    If .PropAryTaioKekka(intIndex).ToString.Length > 3000 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_TAIOKEKKA_NUM)) & vbCrLf
                    End If

                        'ステータスが完了の場合、対象システムの必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropArySystemNmb(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_SYSTEMNMB_NUM)) & vbCrLf
                        End If
                        '対象システムの存在チェック、変換
                        If .PropArySystemNmb(intIndex) <> "" Then
                            If CheckSystemNmbConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If


                        '外部ツール番号（50文字まで）
                    If .PropAryOutSideToolNmb(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_OUTSIDETOOLNMB_NUM)) & vbCrLf
                    End If

                        'イベントID（50文字まで）
                    If .PropAryEventID(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_EVENTID_NUM)) & vbCrLf
                    End If

                        'ソース（50文字まで）
                    If .PropArySource(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_SOURCE_NUM)) & vbCrLf
                    End If

                        'OPCイベントID（50文字まで）
                    If .PropAryOPCEventID(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_OPCEVENTID_NUM)) & vbCrLf
                    End If

                        ''イベントクラス（50文字まで）
                        'If CehckLenB(.PropAryEventClass(intIndex).ToString) > 50 Then
                        '    'メッセージログ設定
                        '    strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_EVENTCLASS_NUM)) & vbCrLf
                        'End If

                        '------------------------------------鶴田修正--------------------------------------------------
                        'イベントクラス（100文字まで）
                    If .PropAryEventClass(intIndex).ToString.Length > 100 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_EVENTCLASS_NUM)) & vbCrLf
                    End If
                        '------------------------------------鶴田修正--------------------------------------------------

                        'ステータスが完了の場合、担当者業務チームの必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryTantoGrpCD(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_TANTOGRPCD_NUM)) & vbCrLf
                        End If
                        '担当者業務チームの存在チェック
                        If .PropAryTantoGrpCD(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryTantoGrpCD(intIndex), strColNm(CSV_TANTOGRPCD_NUM)) = False Then
                                Return False
                            End If
                        End If

                        'ステータスが完了の場合、担当者IDの必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryIncTantoID(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_INCTANTOID_NUM)) & vbCrLf
                        End If
                        '担当者IDの存在チェック
                        If .PropAryIncTantoID(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryIncTantoID(intIndex), strColNm(CSV_INCTANTOID_NUM)) = False Then
                                Return False
                            End If
                        End If

                        'ステータスが完了の場合、インシデント担当者の必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryIncTantoNM(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_INCTANTONM_NUM)) & vbCrLf
                        End If
                        'インシデント担当者（25文字まで）
                    If .PropAryIncTantoNM(intIndex).ToString.Length > 25 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_INCTANTONM_NUM)) & vbCrLf
                    End If

                        'ステータスが完了の場合、ドメインの必須チェック
                        If .PropAryProcessStatusCD(intIndex) = PROCESS_STATUS_INCIDENT_KANRYOU AndAlso .PropAryDomainCD(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_DOMAINCD_NUM)) & vbCrLf
                        End If
                        'ドメインの存在チェック、変換
                        If .PropAryDomainCD(intIndex) <> "" Then
                            If CheckDomainConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        '相手会社（50文字まで）
                    If .PropAryPartnerCompany(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERCOMPANY_NUM)) & vbCrLf
                    End If

                        ''相手ID（25文字まで）
                        'If CehckLenB(.PropAryPartnerID(intIndex).ToString) > 25 Then
                        '    'メッセージログ設定
                        '    strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERID_NUM)) & vbCrLf
                        'End If

                        '----------------------20121003 鶴田修正--------------------------------------------------
                        '相手ID（50文字まで）
                    If .PropAryPartnerID(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERID_NUM)) & vbCrLf
                    End If
                        '----------------------20121003 鶴田修正--------------------------------------------------

                        '相手氏名（25文字まで）
                    If .PropAryPartnerNM(intIndex).ToString.Length > 25 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERNM_NUM)) & vbCrLf
                    End If

                        '相手シメイ（50文字まで）
                    If .PropAryPartnerKana(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERKANA_NUM)) & vbCrLf
                    End If

                        '相手局（50文字まで）
                    If .PropAryPartnerKyokuNM(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERKYOKUNM_NUM)) & vbCrLf
                    End If

                        '相手部署（50文字まで）
                    If .PropAryUsrBusyoNM(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_USRBUSYONM_NUM)) & vbCrLf
                    End If

                        '相手電話番号（25文字まで）
                    If .PropAryPartnerTel(intIndex).ToString.Length > 25 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERTEL_NUM)) & vbCrLf
                    End If

                        '相手メールアドレス（50文字まで）
                    If .PropAryPartnerMailAdd(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERMAILADD_NUM)) & vbCrLf
                    End If

                        '相手連絡先（50文字まで）
                    If .PropAryPartnerContact(intIndex).ToString.Length > 50 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERCONTACT_NUM)) & vbCrLf
                    End If

                        '相手拠点（100文字まで）
                    If .PropAryPartnerBase(intIndex).ToString.Length > 100 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERBASE_NUM)) & vbCrLf
                    End If

                        '相手番組/部屋（100文字まで）
                    If .PropAryPartnerRoom(intIndex).ToString.Length > 100 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_PARTNERROOM_NUM)) & vbCrLf
                    End If

                        '指示書
                        If .PropAryShijisyoFlg(intIndex).ToString = "" Then
                            '入力がない場合はOFF
                            .PropAryShijisyoFlg(intIndex) = SHIJISYO_FLG_OFF
                        Else
                            '入力がある場合
                            If .PropAryShijisyoFlg(intIndex).ToString = SHIJISYO_FLG_OFF_NM Then
                                .PropAryShijisyoFlg(intIndex) = SHIJISYO_FLG_OFF
                            ElseIf .PropAryShijisyoFlg(intIndex).ToString = SHIJISYO_FLG_ON_NM Then
                                .PropAryShijisyoFlg(intIndex) = SHIJISYO_FLG_ON
                            Else
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_SHIJISYOFLG_NUM)) & vbCrLf
                            End If
                        End If

                        '機器種別の存在チェック、変換
                        If .PropAryKindCD(intIndex) <> "" Then
                            If CheckKikiKindConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        '機器番号桁数チェック、変換
                        If .PropAryNum(intIndex) <> "" Then
                            If CheckKikiNmbConvert(dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                            '機器存在チェック（機器種別、機器番号が空では無い）
                            If .PropAryKindCD(intIndex) <> "" Then
                                If CheckKikiConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                    Return False
                                End If
                            End If
                        End If

                    End If

                    '作業履歴関連項目は作業履歴登録フラグがONの場合のみチェック
                    If Boolean.Parse(.PropAryRegWorkFlg(intIndex)) = True Then

                        'ステータスの比較用値
                        Dim strProcessStatusCD As String = .PropAryProcessStatusCD(intIndex - intCountSameNo)
                        Dim blnIsComplete As Boolean = False
                        'ステータスが完了の場合、ステータス完了フラグON
                        If strProcessStatusCD = PROCESS_STATUS_INCIDENT_KANRYOU Then
                            blnIsComplete = True
                        End If

                        'ステータスが完了の場合、経過種別の必須チェック
                        If blnIsComplete AndAlso .PropAryKeikaKbnCD(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_KEIKAKBNCD_NUM)) & vbCrLf
                        End If
                        '経過種別の存在チェック、変換
                        If .PropAryKeikaKbnCD(intIndex) <> "" Then
                            If CheckKeikaKindConvert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        'ステータスが完了の場合、対象システム（作業内容）の必須チェック
                        If blnIsComplete AndAlso .PropArySystemNmb2(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_SYSTEMNMB2_NUM)) & vbCrLf
                        End If
                        '対象システム（作業内容）の存在チェック、変換
                        If .PropArySystemNmb2(intIndex) <> "" Then
                            If CheckSystemNmb2Convert(Adapter, Cn, dataHBKC0601, intIndex) = False Then
                                Return False
                            End If
                        End If

                        '作業予定日時
                        '入力がある場合はチェック
                        If .PropAryWorkSceDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryWorkSceDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_WORKSCEDT_NUM)) & vbCrLf
                            End If
                        End If

                        'ステータスが完了の場合、作業開始日時の必須チェック
                        If blnIsComplete AndAlso .PropAryWorkStDT(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_WORKSTDT_NUM)) & vbCrLf
                        End If
                        '作業開始日時
                        '入力がある場合はチェック
                        If .PropAryWorkStDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryWorkStDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_WORKSTDT_NUM)) & vbCrLf
                            End If
                        End If

                        '作業終了日時
                        '入力がある場合はチェック
                        If .PropAryWorkEdDT(intIndex) <> "" Then
                            '日付型チェック
                            If IsDate(.PropAryWorkEdDT(intIndex)) = False Then
                                'メッセージログ設定
                                strOutLog &= String.Format(C0601_E007, .PropAryRowCount(intIndex), strColNm(CSV_WORKEDDT_NUM)) & vbCrLf
                            End If
                            'ステータスが完了の場合、作業開始日時との前後チェック
                            If blnIsComplete AndAlso .PropAryWorkStDT(intIndex).ToString() <> "" Then
                                Dim strDateFrom As String = .PropAryWorkStDT(intIndex).ToString()
                                Dim strDateTo As String = .PropAryWorkEdDT(intIndex).ToString()
                                '作業終了日時＞作業開始日時の場合、メッセージログ設定
                                If IsDate(strDateFrom) AndAlso IsDate(strDateTo) Then
                                    If DateTime.Parse(strDateFrom) > DateTime.Parse(strDateTo) Then
                                        strOutLog &= String.Format(C0601_E011, .PropAryRowCount(intIndex), strColNm(CSV_WORKSTDT_NUM)) & vbCrLf
                                    End If
                                End If
                            End If
                        End If

                        'ステータスが完了の場合、作業内容の必須チェック
                        If blnIsComplete AndAlso .PropAryWorkNaiyo(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_WORKNAIYO_NUM)) & vbCrLf
                        End If
                    '作業内容（3000文字まで）
                    If .PropAryWorkNaiyo(intIndex).ToString.Length > 3000 Then
                        'メッセージログ設定
                        strOutLog &= String.Format(C0601_E006, .PropAryRowCount(intIndex), strColNm(CSV_WORKNAIYO_NUM)) & vbCrLf
                    End If

                        'ステータスが完了の場合、作業担当者業務チーム1の必須チェック
                        If blnIsComplete AndAlso .PropAryWorkTantoGrpCD1(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_WORKTANTOGRPCD1_NUM)) & vbCrLf
                        End If
                        '作業担当者業務チーム1の存在チェック
                        If .PropAryWorkTantoGrpCD1(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoGrpCD1(intIndex), strColNm(CSV_WORKTANTOGRPCD1_NUM)) = False Then
                                Return False
                            End If
                        End If

                        'ステータスが完了の場合、作業担当者ID1の必須チェック
                        If blnIsComplete AndAlso .PropAryWorkTantoID1(intIndex) = "" Then
                            'メッセージログ設定
                            strOutLog &= String.Format(C0601_E005, .PropAryRowCount(intIndex), strColNm(CSV_WORKTANTOID1_NUM)) & vbCrLf
                        End If
                        '作業担当者ID1の存在チェック
                        If .PropAryWorkTantoID1(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoID1(intIndex), strColNm(CSV_WORKTANTOID1_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者業務チーム2の存在チェック
                        If .PropAryWorkTantoGrpCD2(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoGrpCD2(intIndex), strColNm(CSV_WORKTANTOGRPCD2_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者ID2の存在チェック
                        If .PropAryWorkTantoID2(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoID2(intIndex), strColNm(CSV_WORKTANTOID2_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者業務チーム3の存在チェック
                        If .PropAryWorkTantoGrpCD3(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoGrpCD3(intIndex), strColNm(CSV_WORKTANTOGRPCD3_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者ID3の存在チェック
                        If .PropAryWorkTantoID3(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoID3(intIndex), strColNm(CSV_WORKTANTOID3_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者業務チーム4の存在チェック
                        If .PropAryWorkTantoGrpCD4(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoGrpCD4(intIndex), strColNm(CSV_WORKTANTOGRPCD4_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者ID4の存在チェック
                        If .PropAryWorkTantoID4(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoID4(intIndex), strColNm(CSV_WORKTANTOID4_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者業務チーム5の存在チェック
                        If .PropAryWorkTantoGrpCD5(intIndex) <> "" Then
                            If CheckGroupConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoGrpCD5(intIndex), strColNm(CSV_WORKTANTOGRPCD5_NUM)) = False Then
                                Return False
                            End If
                        End If

                        '作業担当者ID5の存在チェック
                        If .PropAryWorkTantoID5(intIndex) <> "" Then
                            If CheckUsrConvert(Adapter, Cn, dataHBKC0601, intIndex, .PropAryWorkTantoID5(intIndex), strColNm(CSV_WORKTANTOID5_NUM)) = False Then
                                Return False
                            End If
                        End If

                    End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
        End Try


    End Function

    ''' <summary>
    ''' バイト数チェック処理
    ''' </summary>
    ''' <param name="stTarget">[IN]バイト数取得の対象となる文字列</param>
    ''' <returns>半角 1 バイト、全角 2 バイトでカウントされたバイト数。</returns>
    ''' <remarks>半角 1 バイト、全角 2 バイトとして、指定された文字列のバイト数を返します。
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckLenB(ByVal stTarget As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)
    End Function

    ''' <summary>
    ''' 受付手段コード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された受付手段名を受付手段マスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckUketsukewayConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            '受付手段のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectUketsukewayCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "受付手段のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryUkeKbnCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_UKEKBNCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント種別コード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたインシデント種別名をインシデント種別マスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckIncidentKindConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKC0601 As DataHBKC0601, _
                                                ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'インシデント種別のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectIncidentKindCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント種別のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryIncKbnCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_INCKBNCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' プロセスステータスコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたステータス名をプロセスステータスマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckProcessStateConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKC0601 As DataHBKC0601, _
                                                ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Try

            'ステータスのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectProcessStateCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryProcessStatusCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_PROCESSSTATUSCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 対象システム番号変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された対象システムをCI共通情報テーブルからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckSystemNmbConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '対象システムのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectSystemNmbSql(Adapter, Cn, dataHBKC0601, IntIndex, dataHBKC0601.PropArySystemNmb(IntIndex).ToString) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropArySystemNmb(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E010, .PropAryRowCount(IntIndex), strColNm(CSV_SYSTEMNMB_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ドメインコード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたドメイン名をドメインマスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckDomainConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0601 As DataHBKC0601, _
                                        ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            'ドメインのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectDomainCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ドメインのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryDomainCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_DOMAINCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 機器種別コード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された機器種別名を種別マスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckKikiKindConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '機器種別のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectKikiKindCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器種別のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryKindCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_KINDCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 機器番号変換チェック
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]インシデント一括登録Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された機器番号の桁数をチェックし、5桁以下の場合ゼロ埋めする。
    ''' <para>作成情報：2012/08/16 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckKikiNmbConvert(ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim intKikiNmbCount As Integer = 0

        Try

            With dataHBKC0601
                intKikiNmbCount = .PropAryNum(IntIndex).ToString.Length
                '機器番号の桁数チェック
                If intKikiNmbCount > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(C0601_E006, .PropAryRowCount(IntIndex), strColNm(CSV_NUM_NUM)) & vbCrLf
                ElseIf intKikiNmbCount < 5 Then
                    'コードを5桁以下の場合0埋め
                    .PropAryNum(IntIndex) = (.PropAryNum(IntIndex).ToString).PadLeft(5, "0"c)
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
    ''' 機器存在チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]インシデント一括登録Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された機器種別名、機器番号をもとにCI基本情報からデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/08/16 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckKikiConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '機器のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectKikiSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "機器のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count < 1 Then
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E010, .PropAryRowCount(IntIndex), strColNm(CSV_NUM_NUM)) & vbCrLf
                Else
                    'CI番号リストに取得データを追加
                    .PropAryKikiCINmb(IntIndex) = dtResult.Rows(0).Item(0)
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 経過種別コード変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された経過種別名を経過種別マスターからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckKeikaKindConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '経過種別のデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectKeikaKindCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "経過種別のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropAryKeikaKbnCD(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E008, .PropAryRowCount(IntIndex), strColNm(CSV_KEIKAKBNCD_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 対象システム番号（作業内容）変換チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された対象システム（作業内容）をCI共通情報テーブルからデータを検索し存在するならコードへ変換する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckSystemNmb2Convert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '対象システムのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectSystemNmbSql(Adapter, Cn, dataHBKC0601, IntIndex, dataHBKC0601.PropArySystemNmb2(IntIndex).ToString) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム（作業内容）のデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKC0601
                'データが存在しない場合、エラー
                If dtResult.Rows.Count > 0 Then
                    '名称をコードへ変換
                    .PropArySystemNmb2(IntIndex) = dtResult.Rows(0).Item(0)
                Else
                    'エラーメッセージ設定
                    strOutLog &= String.Format(C0601_E010, .PropAryRowCount(IntIndex), strColNm(CSV_SYSTEMNMB2_NUM)) & vbCrLf
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
        Finally
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' グループコード、存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strSearchID">[IN]検索用文字列</param>
    ''' <param name="strMessage">[IN]エラーログ用列名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたグループコードをグループマスタからデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckGroupConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer, _
                                            ByRef strSearchID As String, _
                                            ByRef strMessage As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable
        Dim intLength As Integer

        Try
            '桁数取得
            intLength = strSearchID.Length

            '３桁ゼロ埋め
            If intLength < 3 Then
                strSearchID = strSearchID.PadLeft(3, "0"c)
            End If

            '検索IDセット
            dataHBKC0601.PropStrGroupCD = strSearchID

            'グループのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectGroupCDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが存在しない場合、エラー
            If dtResult.Rows(0).Item(0) = 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(C0601_E008, dataHBKC0601.PropAryRowCount(IntIndex), strMessage) & vbCrLf
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ユーザーID、存在チェック処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　文書Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strSearchID">[IN]検索用文字列</param>
    ''' <param name="strMessage">[IN]エラーログ用列名</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたユーザーIDをひびきユーザーマスタからデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckUsrConvert(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKC0601 As DataHBKC0601, _
                                            ByRef IntIndex As Integer, _
                                            ByRef strSearchID As String, _
                                            ByRef strMessage As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try

            '検索IDセット
            dataHBKC0601.PropStrUsrID = strSearchID

            'ユーザーのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectUsrIDSql(Adapter, Cn, dataHBKC0601, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ユーザーのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが存在しない場合、エラー
            If dtResult.Rows(0).Item(0) = 0 Then
                'エラーメッセージ設定
                strOutLog &= String.Format(C0601_E008, dataHBKC0601.PropAryRowCount(IntIndex), strMessage) & vbCrLf
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' エラーログ出力処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックでエラーとなった内容をログ出力する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetOutLog(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strLogFilePath As String = Nothing                      'ログファイルパス
        Dim strLogFileName As String = Nothing                      'ログファイル名
        Dim strOutputDir As String = Nothing                        'ログ出力フォルダ
        Dim stwWriteLog As System.IO.StreamWriter = Nothing         'ファイル書込用クラス
        Dim strOutputpath As String = Nothing                       '出力ファイル名

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
                puErrMsg = String.Format(C0601_E004, strOutputpath)

            Else
                'エラーメッセージをセット
                puErrMsg = HBK_E001
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
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
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>登録処理を行う
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegMain(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力データ登録処理
        If FileInputDataReg(dataHBKC0601) = False Then
            Return False
        End If

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 入力データ登録処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力データの登録処理を行う
    ''' <para>作成情報：2012/07/24 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputDataReg(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing              'トランザクション
        Dim blnErrorFlg As Boolean = False                  'エラーフラグ
        Dim blnUpdCheck As Boolean = False                  '共通情報更新判定フラグ(初期値False)
        Try

            'ログNoを１で固定
            dataHBKC0601.PropIntLogNo = 1

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKC0601

                '取込番号配列数分ループ
                For i As Integer = 0 To .PropAryTorikomiNum.Count - 1 Step 1

                    '最初の1件目、または取込番号が変わったら場合に共通情報を更新する
                    If i = 0 Then
                        blnUpdCheck = True
                    Else
                        '取込番号が変わった場合
                        If .PropAryTorikomiNum(i) <> .PropAryTorikomiNum(i - 1) Then
                            blnUpdCheck = True
                        Else
                            blnUpdCheck = False
                        End If
                    End If

                    If blnUpdCheck Then

                        '作業履歴番号をクリアする
                        dataHBKC0601.PropIntRirekiNo = 0

                        '新規インシデント番号取得
                        If SelectNewIncNmb(Cn, dataHBKC0601) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント共通情報新規追加
                        If InsertIncInfo(Cn, dataHBKC0601, i) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント担当履歴情報新規追加
                        If .PropAryTantoGrpCD(i) <> "" Or .PropAryIncTantoID(i) <> "" Or .PropAryIncTantoNM(i) <> "" Then
                            If InsertIncTantoRireki(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント機器情報新規追加
                        If .PropAryNum(i) <> "" And .PropAryKindCD(i) <> "" Then
                            If InsertIncKikiInfo(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント共通情報ログテーブル登録
                        If InserIncInfoL(Cn, dataHBKC0601) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント機器情報ログテーブル登録
                        If .PropAryNum(i) <> "" And .PropAryKindCD(i) <> "" Then
                            If InserIncKikiL(Cn, dataHBKC0601) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント対応関係テーブル登録
                        If InsertIncKankei(Cn, dataHBKC0601, i) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント対応関係ログテーブル登録
                        If InsertIncKankeiL(Cn, dataHBKC0601) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                    End If

                    '作業履歴登録フラグがONの場合のみ登録
                    If Boolean.Parse(.PropAryRegWorkFlg(i)) = True Then

                        '作業履歴番号を設定する
                        dataHBKC0601.PropIntRirekiNo = dataHBKC0601.PropIntRirekiNo + 1

                        'インシデント作業履歴情報新規追加
                        If InsertIncWkRireki(Cn, dataHBKC0601, i) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント作業担当情報新規追加
                        If .PropAryWorkTantoGrpCD1(i) <> "" And .PropAryWorkTantoID1(i) <> "" Then
                            .PropStrGroupCD = .PropAryWorkTantoGrpCD1(i).ToString
                            .PropStrUsrID = .PropAryWorkTantoID1(i).ToString
                            .PropStrUsrNM = .PropAryWorkTantoNM1(i).ToString
                            .PropIntTantoNo = 1

                            If InsertIncWkTanto(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント作業担当情報新規追加
                        If .PropAryWorkTantoGrpCD2(i) <> "" And .PropAryWorkTantoID2(i) <> "" Then
                            .PropStrGroupCD = .PropAryWorkTantoGrpCD2(i).ToString
                            .PropStrUsrID = .PropAryWorkTantoID2(i).ToString
                            .PropStrUsrNM = .PropAryWorkTantoNM2(i).ToString
                            .PropIntTantoNo = 2

                            If InsertIncWkTanto(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント作業担当情報新規追加
                        If .PropAryWorkTantoGrpCD3(i) <> "" And .PropAryWorkTantoID3(i) <> "" Then
                            .PropStrGroupCD = .PropAryWorkTantoGrpCD3(i).ToString
                            .PropStrUsrID = .PropAryWorkTantoID3(i).ToString
                            .PropStrUsrNM = .PropAryWorkTantoNM3(i).ToString
                            .PropIntTantoNo = 3

                            If InsertIncWkTanto(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント作業担当情報新規追加
                        If .PropAryWorkTantoGrpCD4(i) <> "" And .PropAryWorkTantoID4(i) <> "" Then
                            .PropStrGroupCD = .PropAryWorkTantoGrpCD4(i).ToString
                            .PropStrUsrID = .PropAryWorkTantoID4(i).ToString
                            .PropStrUsrNM = .PropAryWorkTantoNM4(i).ToString
                            .PropIntTantoNo = 4

                            If InsertIncWkTanto(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント作業担当情報新規追加
                        If .PropAryWorkTantoGrpCD5(i) <> "" And .PropAryWorkTantoID5(i) <> "" Then
                            .PropStrGroupCD = .PropAryWorkTantoGrpCD5(i).ToString
                            .PropStrUsrID = .PropAryWorkTantoID5(i).ToString
                            .PropStrUsrNM = .PropAryWorkTantoNM5(i).ToString
                            .PropIntTantoNo = 5

                            If InsertIncWkTanto(Cn, dataHBKC0601, i) = False Then
                                If Tsx IsNot Nothing Then
                                    Tsx.Rollback()
                                End If
                                'エラーフラグを立ててループを抜ける
                                blnErrorFlg = True
                                Exit For
                            End If
                        End If

                        'インシデント作業履歴ログテーブル登録
                        If InserIncWkRirekiL(Cn, dataHBKC0601) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                        'インシデント作業担当ログテーブル登録
                        If InserIncWkTantoL(Cn, dataHBKC0601) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                    End If

                Next

            End With

            'エラーフラグがONの場合、Falseを返す
            If blnErrorFlg = True Then
                Return False
            Else
                'コミット
                Tsx.Commit()
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'ロールバック
            If Tsx IsNot Nothing Then
                Tsx.Rollback()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '終了処理
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
                Cn.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 新規インシデント番号、システム日付取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したInc番号を取得（SELECT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewIncNmb(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try

            '新規インシデント番号取得（SELECT）用SQLを作成
            If sqlHBKC0601.SetSelectNewIncNmbAndSysDateSql(Adapter, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規インシデント番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0601.PropIntIncNmb = dtResult.Rows(0).Item("IncNmb")        '新規インシデント番号
                dataHBKC0601.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")      'サーバー日付
            Else
                '取得できなかったときはエラー
                puErrMsg = C0601_E009
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
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント共通情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント共通情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0601 As DataHBKC0601, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncInfoSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント共通情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント担当履歴情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント担当履歴情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/09/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncTantoRireki(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0601 As DataHBKC0601, _
                                          ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント担当履歴情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncTantoRirekiSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント担当履歴情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function
    ''' <summary>
    ''' インシデント機器情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント機器情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKikiInfo(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント機器情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncKikiSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント機器情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント作業履歴新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント作業履歴新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncWkRireki(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncWkRirekiSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント作業履歴新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント作業担当新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント作業担当新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncWkTanto(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncWkTantoSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント作業担当新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデント共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncInfoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0601.SetInsertIncInfoLSql(Cmd, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント共通情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデント作業履歴ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント作業履歴ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncWkRirekiL(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0601.SetInsertIncWkRirekiLSql(Cmd, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント作業履歴ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデント作業担当ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント作業担当ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncWkTantoL(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0601.SetInsertIncWkTantoLSql(Cmd, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント作業担当ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】インシデント機器情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント機器情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserIncKikiL(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKC0601.SetInsertIncKikiLSql(Cmd, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント機器情報ログ新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKC0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKC0601

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

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
    ''' インシデント対応関係情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント対応関係情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/13 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankei(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKC0601 As DataHBKC0601, _
                                     ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント対応関係情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertINCKankeiSql(Cmd, Cn, dataHBKC0601, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント対応関係情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデント対応関係ログ情報新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデント対応関係ログ情報新規追加にデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/13 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankeiL(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'インシデント対応関係ログ情報新規登録（INSERT）用SQLを作成
            If sqlHBKC0601.SetInsertIncKankeiLSql(Cmd, Cn, dataHBKC0601) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント対応関係ログ情報新規登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

End Class
