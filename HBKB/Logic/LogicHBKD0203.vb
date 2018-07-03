Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 問題登録（メール作成）ロジッククラス
''' </summary>
''' <remarks>問題登録（メール作成）のロジックを定義したクラス
''' <para>作成情報：2012/08/16 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKD0203

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private sqlHBKD0301 As New SqlHBKD0203

    '問題登録
    Private logicHBKD0201 As New LogicHBKD0201

    'Public定数宣言
    'パーティション文字
    Private Const MAILPARTITION As String = "----------------------------------------------"
    '区切り文字
    Public Const END_CHAR As String = "]"

    '作業履歴文字列
    Public Const RIREKI_STATUSNM As String = "作業ステータス："
    Public Const RIREKI_SYSTEMNM As String = "対象システム："
    Public Const RIREKI_YOTEIDATENM As String = "作業予定日時："
    Public Const RIREKI_STARTDATENM As String = "作業開始日時："
    Public Const RIREKI_ENDDATENM As String = "作業終了日時："
    Public Const RIREKI_WORK_TANTONM As String = "作業担当者："
    Public Const RIREKI_WORK_NAIYONM As String = "作業内容："
    '作業履歴一覧列番号
    Public Const COL_YOJITSU_WORKSTATENM As Integer = logicHBKD0201.COL_YOJITSU_WORKSTATENM         '作業ステータス
    Public Const COL_YOJITSU_WORKNAIYO As Integer = logicHBKD0201.COL_YOJITSU_WORKNAIYO             '作業内容
    Public Const COL_YOJITSU_WORKSCEDT As Integer = logicHBKD0201.COL_YOJITSU_WORKSCEDT             '作業予定日時
    Public Const COL_YOJITSU_WORKSTDT As Integer = logicHBKD0201.COL_YOJITSU_WORKSTDT               '作業開始日時
    Public Const COL_YOJITSU_WORKEDDT As Integer = logicHBKD0201.COL_YOJITSU_WORKEDDT               '作業終了日時
    Public Const COL_YOJITSU_SYSTEM As Integer = logicHBKD0201.COL_YOJITSU_SYSTEM                   '対象システム
    Public Const COL_YOJITSU_TANTOGRP1 As Integer = logicHBKD0201.COL_YOJITSU_TANTOGRP1             '作業担当G1
    Public Const COL_YOJITSU_PRBTANTONM1 As Integer = logicHBKD0201.COL_YOJITSU_PRBTANTONM1         '作業担当1
    Public Const COL_YOJITSU_PRBTANTO_BTN As Integer = logicHBKD0201.COL_YOJITSU_PRBTANTO_BTN       '担当者ボタン
    Public Const YOJITSU_TANTO_COLCNT As Integer = logicHBKD0201.YOJITSU_TANTO_COLCNT               '1担当分カラム数（スプレッドループに使用）


    '会議情報文字列
    Public Const MEETING_NONM As String = "番号："
    Public Const MEETING_JIBINM As String = "実施日："
    Public Const MEETING_TITLENM As String = "タイトル："
    Public Const MEETING_NINNM As String = "承認："
    '会議情報一覧列番号
    Public Const COL_MEETING_NMB As Integer = logicHBKD0201.COL_MEETING_NMB                         '会議番号
    Public Const COL_MEETING_JISISTDT As Integer = logicHBKD0201.COL_MEETING_JISISTDT               '実施日
    Public Const COL_MEETING_TITLE As Integer = logicHBKD0201.COL_MEETING_TITLE                     'タイトル
    Public Const COL_MEETING_RESULTKBN As Integer = logicHBKD0201.COL_MEETING_RESULTKBN             '承認

    '対応関係者情報用文字列
    Public Const RELATION_USERNM As String = "ユーザ："
    Public Const RELATION_GROUPNM As String = "グループ："
    '対応関係者情報一覧列番号
    Public Const COL_PBMKANKEI_RELATIONKBN As Integer = logicHBKD0201.COL_PBMKANKEI_RELATIONKBN     '区分
    Public Const COL_PBMKANKEI_RELATIONID As Integer = logicHBKD0201.COL_PBMKANKEI_RELATIONID       'ID
    Public Const COL_PBMKANKEI_GRPNM As Integer = logicHBKD0201.COL_PBMKANKEI_GRPNM                 'グループ名
    Public Const COL_PBMKANKEI_HBKUSRNM As Integer = logicHBKD0201.COL_PBMKANKEI_HBKUSRNM           'ユーザー名

    'プロセスリンク一覧列番号
    Public Const COL_PLINK_PLINKKBN As Integer = logicHBKD0201.COL_PLINK_PLINKKBN                   '区分
    Public Const COL_PLINK_PLINKNO As Integer = logicHBKD0201.COL_PLINK_PLINKNO                     '番号

    'CYSPR情報データ
    Public Const COL_CYSPR_CYSPRNMB As Integer = logicHBKD0201.COL_CYSPR_CYSPRNMB                   '番号

    '関連ファイル情報データ
    Public Const COL_PRBFILE_NAIYO As Integer = logicHBKD0201.COL_PRBFILE_NAIYO                     '説明

    '置換用インデックス
    Public Const NOW As Integer = 0                     'NOW：変換日付
    Public Const GROUPNM As Integer = 1                 'グループ名：変換なし
    Public Const USERID As Integer = 2                  'ユーザーID：変換なし
    Public Const USERNM As Integer = 3                  'ユーザー名：変換なし
    Public Const PROBLEM_NMB As Integer = 4             '問題管理番号：変換なし
    Public Const STATUS As Integer = 5                  'ステータス：変換なし
    Public Const START_DT As Integer = 6                '開始日時：変換日付
    Public Const KANRYO_DT As Integer = 7               '完了日時：変換日付
    Public Const SOURCE As Integer = 8                  '発生原因：変換なし
    Public Const TITLE As Integer = 9                   'タイトル：変換なし
    Public Const NAIYO As Integer = 10                  '内容：変換なし
    Public Const REG_DT As Integer = 11                 '登録日時：変換日付
    Public Const REG_TEAM As Integer = 12               '登録者業務チーム：変換なし
    Public Const REG_USER As Integer = 13               '登録者：変換なし
    Public Const LASTREG_DT As Integer = 14             '最終更新日時：変換日付
    Public Const LASTREG_TEAM As Integer = 15           '最終更新者業務チーム：変換なし
    Public Const LASTREG_USER As Integer = 16           '最終更新者：変換なし
    Public Const SYSTEM_NMB As Integer = 17             '対象システム：変換なし
    Public Const TANTO_GROUP As Integer = 18            '担当者業務チーム：変換なし
    Public Const TANTO_USER As Integer = 19             '問題担当者：変換なし
    Public Const TAISYO As Integer = 20                 '対処：変換なし
    Public Const TAISYO_USER As Integer = 21            '対処の認証者：変換なし
    Public Const RECORD_USER As Integer = 22            '承認記録者：変換なし
    Public Const KANRENFILE_INFO As Integer = 23        '関連ファイル情報：N行変換
    Public Const CYSPR As Integer = 24                  'CYSPR：N行変換
    Public Const WORK_RIREKI As Integer = 25            '作業履歴：N行変換（日付）
    Public Const KAIGI_INFO As Integer = 26             '会議情報：N行変換（日付）
    Public Const TEXT_1 As Integer = 27                 'テキスト1：変換なし
    Public Const TEXT_2 As Integer = 28                 'テキスト2：変換なし
    Public Const TEXT_3 As Integer = 29                 'テキスト3：変換なし
    Public Const TEXT_4 As Integer = 30                 'テキスト4：変換なし
    Public Const TEXT_5 As Integer = 31                 'テキスト5：変換なし
    Public Const FLG_1 As Integer = 32                  'フラグ1：変換なし
    Public Const FLG_2 As Integer = 33                  'フラグ2：変換なし
    Public Const FLG_3 As Integer = 34                  'フラグ3：変換なし
    Public Const FLG_4 As Integer = 35                  'フラグ4：変換なし
    Public Const FLG_5 As Integer = 36                  'フラグ5：変換なし
    Public Const TAIOH_KANKEI As Integer = 37           '対応関係者情報：N行変換
    Public Const GROUP_RIREKI As Integer = 38           'グループ履歴：変換なし
    Public Const TANTOH_RIREKI As Integer = 39          '担当者履歴：変換なし
    Public Const PROCESSLINK_INFO As Integer = 40       'プロセスリンク情報：N行変換
    Public Const TANTOUSRSHI As Integer = 41            '担当者氏：変換
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 START
    Public Const COMMON_USERNMSEI As Integer = 42       '（共通）ユーザー名(姓)：変換なし
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 END

    ''' <summary>
    ''' 問題登録（メール作成）本文作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題登録（メール作成）本文作成メイン処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMailMain(ByRef dataHBKD0301 As DataHBKD0203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題登録（メール作成）本文作成処理
        If CreateIncidentMail(dataHBKD0301) = False Then
            Return False
        End If

        'メールソフト(outlook起動)処理
        If StartUpForMail(dataHBKD0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 問題登録（メール作成）本文作成処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>問題登録（メール作成）本文作成処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMail(ByRef dataHBKD0301 As DataHBKD0203) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'メール本文作成処理

            Dim strCheck As String(,) = PROBLEM_PERMUTATION        '登録画面置換え

            With dataHBKD0301

                'メールフォーマット選択画面から受け取ったDataTableからメールフォーマット設定
                .PropStrMailto = .PropDtReturnData.Rows(0).Item("MailTo")
                .PropStrMailFrom = .PropDtReturnData.Rows(0).Item("MailFrom")
                .PropStrMailCc = .PropDtReturnData.Rows(0).Item("CC")
                .PropStrMailBcc = .PropDtReturnData.Rows(0).Item("Bcc")
                .PropIntMailPriority = Integer.Parse(.PropDtReturnData.Rows(0).Item("PriorityKbn"))
                .PropStrMailSubject = .PropDtReturnData.Rows(0).Item("Title")
                .PropStrMailText = .PropDtReturnData.Rows(0).Item("MailText")

                '宛先設定
                If CreateWritingsPermutation(dataHBKD0301, .PropStrMailto, strCheck) = False Then
                    Return False
                End If

                'CC設定
                If CreateWritingsPermutation(dataHBKD0301, .PropStrMailCc, strCheck) = False Then
                    Return False
                End If

                'Bcc設定
                If CreateWritingsPermutation(dataHBKD0301, .PropStrMailBcc, strCheck) = False Then
                    Return False
                End If

                'タイトル設定
                If CreateWritingsPermutation(dataHBKD0301, .PropStrMailSubject, strCheck) = False Then
                    Return False
                End If

                '本文設定
                If CreateWritingsPermutation(dataHBKD0301, .PropStrMailText, strCheck) = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' メールソフト(outlook起動)処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成されたメール本文、タイトルを用いてメールソフトを起動する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function StartUpForMail(ByRef dataHBKD0301 As DataHBKD0203) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0301

                Dim otlApp As Object = Nothing       'Applicationオブジェクト
                Dim otlMail As Object = Nothing 'メールのオブジェクト

                'outlook 起動
                otlApp = CreateObject("Outlook.Application")

                'メールアイテムの作成
                otlMail = otlApp.CreateItem(0)
                otlMail.SentOnBehalfOfName = .PropStrMailFrom           '差出人設定
                otlMail.To = .PropStrMailto                             '宛先設定
                otlMail.CC = .PropStrMailCc                             'Cc設定
                otlMail.BCC = .PropStrMailBcc                           'Bcc設定
                otlMail.Subject = .PropStrMailSubject                   'タイトル設定
                otlMail.Body = .PropStrMailText                         '本文設定
                otlMail.Importance = .PropIntMailPriority               '重要度設定

                otlMail.Display()                                       '画面に表示

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 日付変換処理処理
    ''' </summary>
    ''' <param name="strDateDT">[IN]日付</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="strFormat">[IN]フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>２つの日付を繋げ、日付型に変換できる場合は指定されたフォーマットの文字列に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertDate(ByVal strDateDT As String, ByRef strConvert As String, ByVal strFormat As String) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtConvertForInput As DateTime           '変換用日付型変数

        Try

            '日付と時間を連結
            strConvert = strDateDT

            If DateTime.TryParse(strConvert, dtConvertForInput) = False Then
                strConvert = strConvert
            Else
                strConvert = dtConvertForInput.ToString(strFormat)
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 文字置換処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateWritingsPermutation(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, ByVal StrCheck As String(,)) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '存在チェック用変数
        Dim intStartIndex As Integer = 0
        Dim intEndIndex As Integer = 0
        Dim strRetrunFormat As String = ""
        Dim strReturnPermutation As String = ""
        Dim intCount As Integer = 0

        Try

            '置き換え一覧ループ
            For i As Integer = 0 To (StrCheck.Length / StrCheck.Rank) - 1 Step 1
                '存在チェック
                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0))
                intEndIndex = StrConvert.LastIndexOf(StrCheck(i, 0))
                intCount = 0

                '文字列に置換文字が存在する場合
                If intStartIndex <> -1 Or intEndIndex <> -1 Then

                    If StrCheck(i, 1) = PROBLEM_PERMUTATION_NORMAL Then
                        '置換
                        If SetPermutation_Normal(dataHBKD0301, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If
                    ElseIf StrCheck(i, 1) = PROBLEM_PERMUTATION_DATE Then
                        While (True)
                            If intCount <> 0 Then
                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)
                                If intStartIndex = -1 Then
                                    Exit While
                                End If
                            End If
                            '日付変換後、置換
                            If GetIndex_Format(StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If
                            '置換
                            If SetPermutation_Date(dataHBKD0301, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If
                            intCount = intCount + 1
                        End While
                    ElseIf StrCheck(i, 1) = PROBLEM_PERMUTATION_MULTILINE Then
                        '置換
                        If SetPermutation_Multiline(dataHBKD0301, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If
                    ElseIf StrCheck(i, 1) = PROBLEM_PERMUTATION_MULTDATE Then
                        While (True)
                            If intCount <> 0 Then
                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)
                                If intStartIndex = -1 Then
                                    Exit While
                                End If
                            End If
                            '複数行変換（日付）後、置換
                            If GetIndex_Format(StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If
                            '置換処理
                            If SetPermutation_MultilineDate(dataHBKD0301, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If
                            intCount = intCount + 1
                        End While
                    End If
                End If
            Next

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 日付フォーマット取得処理
    ''' </summary>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]チェック文字列</param>
    ''' <param name="intStringIndex">[IN]置き換え開始インデックス</param>
    ''' <param name="StrRetrunFormat">[IN/OUT]日付型フォーマット</param>
    ''' <param name="StrReturnPermutation">[IN/OUT]置換用文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取った本文、置換用文字列から日付型のフォーマットを取得し、フォーマットと置換用文字列を返す
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIndex_Format(ByRef StrConvert As String, ByVal StrCheck As String, _
                                                   ByVal intStringIndex As Integer, ByRef StrRetrunFormat As String, _
                                                   ByRef StrReturnPermutation As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim intStartIndex As Integer = intStringIndex
        Dim intLastIndex As Integer = 0
        Dim intCount As Integer = 0
        Dim blnIndex As Boolean = False

        Try
            '取得インデックスから本文をループ
            For i As Integer = intStartIndex + StrCheck.Length To StrConvert.Length - 1 Step 1
                '区切り文字の場合は、インデックスを保存して、ループを抜ける
                If StrConvert(i) = END_CHAR Then
                    intLastIndex = i
                    blnIndex = True
                    Exit For
                End If
                intCount = intCount + 1
            Next

            '取得したインデックスから文字列を取得
            If blnIndex = True Then
                StrRetrunFormat = StrConvert.Substring(intStringIndex + StrCheck.Length, intCount)
                StrReturnPermutation = StrConvert.Substring(intStringIndex, intLastIndex - intStringIndex + 1)
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 置換処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え文字</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Normal(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, _
                                                            ByVal StrCheck As String, ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""
        Dim strTemp As String = ""

        Try
            With dataHBKD0301
                If IntCount = GROUPNM Then
                    'グループ名
                    strPermutation = PropWorkGroupCD
                ElseIf IntCount = USERID Then
                    'ユーザーID
                    strPermutation = PropUserId
                ElseIf IntCount = USERNM Then
                    'ユーザー名
                    strPermutation = PropUserName
                ElseIf IntCount = PROBLEM_NMB Then
                    '問題番号
                    strPermutation = .PropStrPrbNmb
                ElseIf IntCount = STATUS Then
                    'ステータス
                    strPermutation = .PropStrProcessStateCD
                ElseIf IntCount = SOURCE Then
                    '発生原因
                    strPermutation = .PropStrSource
                ElseIf IntCount = TITLE Then
                    'タイトル
                    strPermutation = .PropStrTitle
                ElseIf IntCount = NAIYO Then
                    '内容
                    strPermutation = .PropStrNaiyo
                ElseIf IntCount = REG_TEAM Then
                    '登録者業務チーム
                    strPermutation = .PropStrRegGrpNM
                ElseIf IntCount = REG_USER Then
                    '登録者
                    strPermutation = .PropStrRegNM
                ElseIf IntCount = LASTREG_TEAM Then
                    '最終更新者業務チーム
                    strPermutation = .PropStrUpdateGrpNM
                ElseIf IntCount = LASTREG_USER Then
                    '最終更新者
                    strPermutation = .PropStrUpdateNM
                ElseIf IntCount = SYSTEM_NMB Then
                    '対象システム
                    If GetCIInfoSystem(.PropStrSystemNmb, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = TANTO_GROUP Then
                    '担当者業務チーム
                    strPermutation = .PropStrTantoGrp
                ElseIf IntCount = TANTO_USER Then
                    '問題担当者
                    strPermutation = .PropStrPrbTanto
                ElseIf IntCount = TAISYO Then
                    '対処
                    strPermutation = .PropStrTaisyo
                ElseIf IntCount = TAISYO_USER Then
                    '対処の認証者
                    strPermutation = .PropStrTaisyoUser
                ElseIf IntCount = RECORD_USER Then
                    '承認記録者
                    strPermutation = .PropStrRecordUser
                ElseIf IntCount = TEXT_1 Then
                    'テキスト1
                    strPermutation = .PropStrBIko1
                ElseIf IntCount = TEXT_2 Then
                    'テキスト2
                    strPermutation = .PropStrBIko2
                ElseIf IntCount = TEXT_3 Then
                    'テキスト3
                    strPermutation = .PropStrBIko3
                ElseIf IntCount = TEXT_4 Then
                    'テキスト4
                    strPermutation = .PropStrBIko4
                ElseIf IntCount = TEXT_5 Then
                    'テキスト5
                    strPermutation = .PropStrBIko5
                ElseIf IntCount = FLG_1 Then
                    'フラグ1
                    strPermutation = .PropStrFreeFlg1
                ElseIf IntCount = FLG_2 Then
                    'フラグ2
                    strPermutation = .PropStrFreeFlg2
                ElseIf IntCount = FLG_3 Then
                    'フラグ3
                    strPermutation = .PropStrFreeFlg3
                ElseIf IntCount = FLG_4 Then
                    'フラグ4
                    strPermutation = .PropStrFreeFlg4
                ElseIf IntCount = FLG_5 Then
                    'フラグ5
                    strPermutation = .PropStrFreeFlg5
                ElseIf IntCount = GROUP_RIREKI Then
                    'グループ履歴
                    strPermutation = .PropStrGrpHistory
                ElseIf IntCount = TANTOH_RIREKI Then
                    '担当者履歴
                    strPermutation = .PropStrTantoHistory
                ElseIf IntCount = TANTOUSRSHI Then

                    '前後の空白を削除した氏名
                    strTemp = Trim(.PropStrTantoNM)
                    strPermutation = strTemp
                    If strTemp.IndexOf(" ") > 0 Then
                        '担当者氏
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf(" "))
                    ElseIf strTemp.IndexOf("　") > 0 Then
                        '担当者氏
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf("　"))
                    End If
                    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 START
                ElseIf IntCount = COMMON_USERNMSEI Then
                    '前後の空白を削除した氏名
                    strTemp = Trim(PropUserName)
                    strPermutation = strTemp
                    If strTemp.IndexOf(" ") > 0 Then
                        'ユーザー名(姓)
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf(" "))
                    ElseIf strTemp.IndexOf("　") > 0 Then
                        'ユーザー名(姓)
                        strPermutation = strTemp.Substring(0, strTemp.IndexOf("　"))
                    End If
                    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 END
                End If
            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 置換処理_日付
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（日付）
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Date(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, _
                                                        ByVal StrCheck As String, ByVal IntCount As Integer, _
                                                        ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""

        Try
            With dataHBKD0301
                If IntCount = NOW Then
                    'NOW
                    strPermutation = System.DateTime.Now.ToString(StrFormatForDate)
                ElseIf IntCount = START_DT Then
                    '開始日時
                    If SetConvertDate(.PropStrKaisiDT & " " & .PropStrKaisiDT_HM, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = KANRYO_DT Then
                    '完了日時
                    If SetConvertDate(.PropStrKanryoDT & " " & .PropStrKanryoDT_HM, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = REG_DT Then
                    '登録日時
                    If SetConvertDate(.PropStrRegDT, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = LASTREG_DT Then
                    '最終更新日時
                    If SetConvertDate(.PropStrUpdateDT, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                End If

            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 置換処理_複数行
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行）
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Multiline(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, _
                                                             ByVal StrCheck As String, ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""

        Try

            With dataHBKD0301
                If IntCount = KANRENFILE_INFO Then
                    '関連ファイル情報
                    If SetConvertFile(dataHBKD0301, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = CYSPR Then
                    'CYSPR
                    If SetConvertCYSPR(dataHBKD0301, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = TAIOH_KANKEI Then
                    '対応関係者情報
                    If SetConvertRelation(dataHBKD0301, strPermutation) = False Then
                        Return False
                    End If
                ElseIf IntCount = PROCESSLINK_INFO Then
                    'プロセスリンク情報
                    If SetConvertProcessLink(dataHBKD0301, strPermutation) = False Then
                        Return False
                    End If
                End If

            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally

        End Try
    End Function

    ''' <summary>
    ''' 置換処理_複数行変換（日付）
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行変換（日付））
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_MultilineDate(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, _
                                                                   ByVal StrCheck As String, ByVal IntCount As Integer, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""
        Try
            With dataHBKD0301
                If IntCount = WORK_RIREKI Then
                    '作業履歴
                    If SetConvertRireki(dataHBKD0301, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                ElseIf IntCount = KAIGI_INFO Then
                    '会議情報
                    If SetConvertMeeting(dataHBKD0301, strPermutation, StrFormatForDate) = False Then
                        Return False
                    End If
                End If
            End With

            '置換処理
            If SetPermutation(StrConvert, StrCheck, strPermutation) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
        End Try
    End Function

    ''' <summary>
    ''' 置換処理
    ''' </summary>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormat">[IN]置き換えフォーマット</param>
    ''' <param name="StrInput">[IN]置き換え文字</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation(ByRef StrConvert As String, ByVal StrFormat As String, ByVal StrInput As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '置換
            StrConvert = StrConvert.Replace(StrFormat, StrInput)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' CI情報取得
    ''' </summary>
    ''' <param name="StrSystemNmb">[IN]対象システム番号</param>
    ''' <param name="StrPermutation">[IN/OUT]置き換え文字</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCIInfoSystem(ByVal StrSystemNmb As String, ByRef StrPermutation As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim intSysNmb As Integer
        Dim dtResult As New DataTable

        Try
            'コネクションを開く
            Cn.Open()
            '数値変換できる場合のみ、取得
            If Integer.TryParse(StrSystemNmb, intSysNmb) = True Then
                '分類１＋分類２＋名称を取得
                If sqlHBKD0301.SelectCIInfoSql(Adapter, Cn, intSysNmb) = False Then
                    Return False
                End If
                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI情報取得", Nothing, Adapter.SelectCommand)
                'データを取得
                Adapter.Fill(dtResult)

                If dtResult.Rows.Count > 0 Then
                    StrPermutation = dtResult.Rows(0).Item(0).ToString
                Else
                    StrPermutation = ""
                End If
            Else
                StrPermutation = ""
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True
        Catch ex As Exception
            '例外処理
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
            dtResult.Dispose()
        End Try
    End Function

    '複数行変換処理-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 関連ファイル情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertFile(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0301.PropVwFileInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、関連ファイル情報、ファイル説明をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= .GetText(i, COL_PRBFILE_NAIYO) & vbCrLf
                Next
                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' CYSPR情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR情報データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertCYSPR(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0301.PropVwCysprInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、CYSPRをセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    If StrConvert = "" Then
                        StrConvert &= .GetText(i, COL_CYSPR_CYSPRNMB)
                    Else
                        StrConvert &= "、" & .GetText(i, COL_CYSPR_CYSPRNMB)
                    End If
                Next

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 対応関係情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRelation(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0301.PropVwRelation.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、関係者区分・グループ名・ユーザID＋ユーザ名をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    StrConvert &= MAILPARTITION & vbCrLf
                    If .GetText(i, COL_PBMKANKEI_RELATIONKBN) = KBN_GROUP Then
                        StrConvert &= RELATION_GROUPNM & .GetText(i, COL_PBMKANKEI_GRPNM) & vbCrLf
                    Else
                        StrConvert &= RELATION_USERNM & .GetText(i, COL_PBMKANKEI_RELATIONID) & " " & .GetText(i, COL_PBMKANKEI_HBKUSRNM) & vbCrLf
                    End If
                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' プロセスリンク情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertProcessLink(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKD0301.PropVwprocessLinkInfo.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、行カンマ区切りのプロセスリンク情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    If StrConvert = "" Then
                        StrConvert &= .GetText(i, COL_PLINK_PLINKKBN) & " " & .GetText(i, COL_PLINK_PLINKNO)
                    Else
                        StrConvert &= " , " & .GetText(i, COL_PLINK_PLINKKBN) & " " & .GetText(i, COL_PLINK_PLINKNO)
                    End If

                Next
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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

    '複数行変換処理（日付アリ）-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 作業履歴データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRireki(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strYoteiDate As String = ""                 '作業予定日変換用文字列
        Dim strStartDate As String = ""                 '作業開始日変換用文字列
        Dim strEndDate As String = ""                   '作業終了日変換用文字列


        Try
            With dataHBKD0301.PropVwPrbYojitsu.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、種別＋番号＋機器情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '作業予定日、作業予定時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_YOJITSU_WORKSCEDT), strYoteiDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    '作業開始日、作業開始時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_YOJITSU_WORKSTDT), strStartDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    '作業終了日、作業終了時をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_YOJITSU_WORKEDDT), strEndDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= RIREKI_STATUSNM & .GetText(i, COL_YOJITSU_WORKSTATENM) & vbCrLf
                    StrConvert &= RIREKI_SYSTEMNM & .GetText(i, COL_YOJITSU_SYSTEM) & vbCrLf
                    StrConvert &= RIREKI_YOTEIDATENM & strYoteiDate & vbCrLf
                    StrConvert &= RIREKI_STARTDATENM & strStartDate & vbCrLf
                    StrConvert &= RIREKI_ENDDATENM & strEndDate & vbCrLf
                    StrConvert &= RIREKI_WORK_TANTONM
                    For j As Integer = 0 To 49  '列50固定
                        If j = 0 Then
                            StrConvert &= .GetText(i, COL_YOJITSU_TANTOGRP1 + (j * YOJITSU_TANTO_COLCNT)) & " " & .GetText(i, COL_YOJITSU_PRBTANTONM1 + (j * YOJITSU_TANTO_COLCNT))
                        Else
                            If .GetText(i, COL_YOJITSU_TANTOGRP1 + (j * YOJITSU_TANTO_COLCNT)) & .GetText(i, COL_YOJITSU_PRBTANTONM1 + (j * YOJITSU_TANTO_COLCNT)) <> "" Then
                                StrConvert &= "," & .GetText(i, COL_YOJITSU_TANTOGRP1 + (j * YOJITSU_TANTO_COLCNT)) & " " & .GetText(i, COL_YOJITSU_PRBTANTONM1 + (j * YOJITSU_TANTO_COLCNT))
                            End If
                        End If
                    Next
                    StrConvert &= vbCrLf
                    StrConvert &= RIREKI_WORK_NAIYONM & vbCrLf & .GetText(i, COL_YOJITSU_WORKNAIYO) & vbCrLf

                Next
                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
    ''' 会議情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKD0301">[IN/OUT]問題登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データをメール用に変換する
    ''' <para>作成情報：2012/08/16 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertMeeting(ByRef dataHBKD0301 As DataHBKD0203, ByRef StrConvert As String, ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strJisshiDate As String = ""                 '実施日変換用文字列

        Try
            With dataHBKD0301.PropVwMeeting.Sheets(0)

                '0件の場合空文字を返す
                If .Rows.Count = 0 Then
                    StrConvert = ""
                    '終了ログ出力
                    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                    '正常処理終了
                    Return True
                End If

                '行数分ループを行い、種別＋番号＋機器情報をセットする
                For i As Integer = 0 To .Rows.Count - 1 Step 1

                    '実施日をセットし、日付型に変換
                    If SetConvertDate(.GetText(i, COL_MEETING_JISISTDT), strJisshiDate, StrFormatForDate) = False Then
                        Return False
                    End If

                    StrConvert &= MAILPARTITION & vbCrLf
                    StrConvert &= MEETING_NONM & .GetText(i, COL_MEETING_NMB) & vbCrLf
                    StrConvert &= MEETING_JIBINM & strJisshiDate & vbCrLf
                    StrConvert &= MEETING_TITLENM & .GetText(i, COL_MEETING_TITLE) & vbCrLf
                    StrConvert &= MEETING_NINNM & .GetText(i, COL_MEETING_RESULTKBN) & vbCrLf
                Next

                StrConvert &= MAILPARTITION

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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

End Class
