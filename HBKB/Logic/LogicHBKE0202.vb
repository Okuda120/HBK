Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 変更登録（メール作成）ロジッククラス
''' </summary>
''' <remarks>変更登録（メール作成）のロジックを定義したクラス
''' <para>作成情報：2012/08/22 t.fukuo
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKE0202

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKE0202 As New SqlHBKE0202
    '変更登録ロジッククラス
    Private logicHBKE0201 As LogicHBKE0201

    'Public定数宣言

    '対応関係者情報ヘッダ文字列
    Public Const RELATION_KBN_NM_GROUP As String = "グループ："
    Public Const RELATION_KBN_NM_USER As String = "ユーザー："
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = logicHBKE0201.COL_RELATION_KBN               '区分
    Public Const COL_RELATION_ID As Integer = logicHBKE0201.COL_RELATION_ID                 'ID
    Public Const COL_RELATION_GROUPNM As Integer = logicHBKE0201.COL_RELATION_GROUPNM       'グループ名
    Public Const COL_RELATION_USERNM As Integer = logicHBKE0201.COL_RELATION_USERNM         'ユーザー名

    'プロセスリンク一覧列番号
    Public Const COL_PROCESSLINK_KBN_NMR As Integer = logicHBKE0201.COL_processLINK_KBN_NMR '区分
    Public Const COL_PROCESSLINK_NO As Integer = logicHBKE0201.COL_processLINK_NO           '番号

    '関連ファイル一覧列番号
    Public Const COL_FILE_NAIYO As Integer = logicHBKE0201.COL_FILE_NAIYO                   '説明
    Public Const COL_FILE_REGDT As Integer = logicHBKE0201.COL_FILE_REGDT                   '登録日時

    '会議情報文字列
    Public Const MEETING_NONM As String = "番号："
    Public Const MEETING_JIBINM As String = "実施日："
    Public Const MEETING_TITLENM As String = "タイトル："
    Public Const MEETING_NINNM As String = "承認："
    '会議情報一覧列番号
    Public Const COL_MEETING_NO As Integer = logicHBKE0201.COL_MEETING_NO                   '番号
    Public Const COL_MEETING_JIBI As Integer = logicHBKE0201.COL_MEETING_JIBI               '実施日
    Public Const COL_MEETING_TITLE As Integer = logicHBKE0201.COL_MEETING_TITLE             'タイトル
    Public Const COL_MEETING_NIN As Integer = logicHBKE0201.COL_MEETING_NIN                 '承認

    'CYSPR情報一覧列番号
    Public Const COL_CYSPR_NO As Integer = logicHBKE0201.COL_CYSPR_NO                       '番号

    '置換用インデックス
    Public Const COMMON_NOW As Integer = 0              '（共通）NOW：変換日付
    Public Const COMMON_GROUPNM As Integer = 1          '（共通）グループ名：変換なし
    Public Const COMMON_USERID As Integer = 2           '（共通）ユーザーID：変換なし
    Public Const COMMON_USERNM As Integer = 3           '（共通）ユーザー名：変換なし
    Public Const KHN_CHGNMB As Integer = 4              '（基本情報）変更管理番号：変換なし
    Public Const KHN_STATUS As Integer = 5              '（基本情報）ステータス：変換なし
    Public Const KHN_KAISI_DT As Integer = 6            '（基本情報）開始日付：変換日付
    Public Const KHN_KANRYO_DT As Integer = 7           '（基本情報）完了日付：変換日付
    Public Const KHN_TITLE As Integer = 8               '（基本情報）タイトル：変換なし
    Public Const KHN_NAIYO As Integer = 9               '（基本情報）内容：変換なし
    Public Const KHN_REGDT As Integer = 10              '（基本情報）登録日時：変換日付
    Public Const KHN_REGGRPNM As Integer = 11           '（基本情報）登録者業務チーム：変換なし
    Public Const KHN_REGUSERNM As Integer = 12          '（基本情報）登録者：変換なし
    Public Const KHN_UPDATEDT As Integer = 13           '（基本情報）最終更新日時：変換日付
    Public Const KHN_UPGRPNM As Integer = 14            '（基本情報）最終更新者業務チーム：変換なし
    Public Const KHN_UPUSERNM As Integer = 15           '（基本情報）最終更新者：変換なし
    Public Const KHN_SYSTEM As Integer = 16             '（基本情報）対象システム：変換なし
    Public Const KHN_TANTOGRPNM As Integer = 17         '（基本情報）担当者業務グループ：変換なし
    Public Const KHN_TANTOUSERNM As Integer = 18        '（基本情報）変更担当者氏名：変換なし
    Public Const KHN_TAISYO As Integer = 19             '（基本情報）対処：変換なし
    Public Const KHN_APPROVAL_USERNM As Integer = 20    '（基本情報）変更の承認者：変換なし
    Public Const KHN_RECORDERUSERNM As Integer = 21     '（基本情報）変更承認記録者：変換なし
    Public Const RELATIONFILE_INFO As Integer = 22      '（関連情報）ファイル情報：N行変換（日付）
    Public Const CYSPR_CYSPR As Integer = 23            '（CYSPR）CYSPR：変換なし
    Public Const MEETING_INFO As Integer = 24           '（会議情報）会議番号：N行変換（日付）
    Public Const FREE_TEXT_1 As Integer = 25            '（フリー入力情報）テキスト1：変換なし
    Public Const FREE_TEXT_2 As Integer = 26            '（フリー入力情報）テキスト2：変換なし
    Public Const FREE_TEXT_3 As Integer = 27            '（フリー入力情報）テキスト3：変換なし
    Public Const FREE_TEXT_4 As Integer = 28            '（フリー入力情報）テキスト4：変換なし
    Public Const FREE_TEXT_5 As Integer = 29            '（フリー入力情報）テキスト5：変換なし
    Public Const FREE_FLG_1 As Integer = 30             '（フリー入力情報）フラグ1：変換なし
    Public Const FREE_FLG_2 As Integer = 31             '（フリー入力情報）フラグ2：変換なし
    Public Const FREE_FLG_3 As Integer = 32             '（フリー入力情報）フラグ3：変換なし
    Public Const FREE_FLG_4 As Integer = 33             '（フリー入力情報）フラグ4：変換なし
    Public Const FREE_FLG_5 As Integer = 34             '（フリー入力情報）フラグ5：変換なし
    Public Const RELATION_INFO As Integer = 35          '（対応関係者）区分：N行変換
    Public Const GROUP_RIREKI As Integer = 36           '（グループ履歴）グループ履歴：変換なし
    Public Const TANTOH_RIREKI As Integer = 37          '（担当者履歴）担当者履歴：変換なし
    Public Const PROCESSLINK_INFO As Integer = 38       '（プロセスリンク）区分：N行変換
    Public Const TANTOUSRSHI As Integer = 39            '（基本情報）変更担当者氏：変換なし
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 START
    Public Const COMMON_USERNMSEI As Integer = 40       '（共通）ユーザー名(姓)：変換なし
    '[ADD] 2015/08/24 y.naganuma メールテンプレート条件追加対応 END


    ''' <summary>
    ''' 変更登録（メール作成）本文作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更登録（メール作成）本文作成メイン処理を行う
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMailMain(ByRef dataHBKE0202 As DataHBKE0202) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変更登録（メール作成）本文作成処理
        If CreateIncidentMail(DataHBKE0202) = False Then
            Return False
        End If

        'メールソフト(OutLook起動)処理
        If StartUpForMail(dataHBKE0202) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 変更登録（メール作成）本文作成処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>変更登録（メール作成）本文作成処理を行う
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIncidentMail(ByRef dataHBKE0202 As DataHBKE0202) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'メール本文作成処理

            Dim strCheck As String(,) = CHANGE_PERMUTATION        '登録画面置換え配列

            With dataHBKE0202

                'メールフォーマット選択画面から受け取ったDataTableからメールフォーマット設定
                .PropStrMailto = .PropDtReturnData.Rows(0).Item("MailTo")
                .PropStrMailFrom = .PropDtReturnData.Rows(0).Item("MailFrom")
                .PropStrMailCc = .PropDtReturnData.Rows(0).Item("CC")
                .PropStrMailBcc = .PropDtReturnData.Rows(0).Item("Bcc")
                .PropStrMailPriority = Integer.Parse(.PropDtReturnData.Rows(0).Item("PriorityKbn"))
                .PropStrMailSubject = .PropDtReturnData.Rows(0).Item("Title")
                .PropStrMailText = .PropDtReturnData.Rows(0).Item("MailText")

                '宛先設定
                If CreateWritingsPermutation(dataHBKE0202, .PropStrMailto, strCheck) = False Then
                    Return False
                End If

                'CC設定
                If CreateWritingsPermutation(dataHBKE0202, .PropStrMailCc, strCheck) = False Then
                    Return False
                End If

                'Bcc設定
                If CreateWritingsPermutation(dataHBKE0202, .PropStrMailBcc, strCheck) = False Then
                    Return False
                End If

                'タイトル設定
                If CreateWritingsPermutation(dataHBKE0202, .PropStrMailSubject, strCheck) = False Then
                    Return False
                End If

                '本文設定
                If CreateWritingsPermutation(dataHBKE0202, .PropStrMailText, strCheck) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' メールソフト(OutLook起動)処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成されたメール本文、タイトルを用いてメールソフトを起動する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function StartUpForMail(ByRef dataHBKE0202 As DataHBKE0202) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0202

                Dim otlApp As Object = Nothing          'Applicationオブジェクト
                Dim otlMail As Object = Nothing         'メールのオブジェクト

                'OutLook 起動
                otlApp = CreateObject("OutLook.Application")

                'メールアイテムの作成
                otlMail = otlApp.CreateItem(0)
                otlMail.SentOnBehalfOfName = .PropStrMailFrom           '差出人設定
                otlMail.To = .PropStrMailto                             '宛先設定
                otlMail.CC = .PropStrMailCc                             'Cc設定
                otlMail.BCC = .PropStrMailBcc                           'Bcc設定
                otlMail.Subject = .PropStrMailSubject                   'タイトル設定
                otlMail.Body = .PropStrMailText                         '本文設定
                otlMail.Importance = .PropStrMailPriority               '重要度設定

                '画面に表示
                otlMail.Display()

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
        End Try

    End Function

    ''' <summary>
    ''' 日付変換処理処理
    ''' </summary>
    ''' <param name="strDateDT">[IN]日付</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="strFormat">[IN]フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>２つの日付を繋げ、日付型に変換できる場合は"yyyy/MM/dd(ddd) HH:mm"の文字列に、変換できなければ空文字を返す
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertDate(ByVal strDateDT As String, _
                                   ByRef strConvert As String, _
                                   ByVal strFormat As String) As Boolean
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
                strConvert = Format(DateTime.Parse(dtConvertForInput), strFormat)
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
        End Try

    End Function

    ''' <summary>
    ''' 文字置換処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateWritingsPermutation(ByRef dataHBKE0202 As DataHBKE0202, _
                                              ByRef StrConvert As String, _
                                              ByVal StrCheck As String(,)) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '存在チェック用変数
        Dim intStartIndex As Integer = 0            '開始インデックス
        Dim intEndIndex As Integer = 0              '終了インデックス
        Dim strRetrunFormat As String = ""          '別ファンクションからの戻り値：フォーマット形式
        Dim strReturnPermutation As String = ""     '別ファンクションからの戻り値：残りの本文
        Dim intCount As Integer = 0                 'カウント用変数

        Try

            '置き換え一覧ループ
            For i As Integer = 0 To (StrCheck.Length / StrCheck.Rank) - 1 Step 1

                '存在チェック
                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0))
                intEndIndex = StrConvert.LastIndexOf(StrCheck(i, 0))
                intCount = 0

                '文字列に置換文字が存在する場合
                If intStartIndex <> -1 Or intEndIndex <> -1 Then

                    '置換形式に応じて置換処理を行う
                    If StrCheck(i, 1) = CHANGE_PERMUTATION_NORMAL Then      '通常置換形式の場合

                        '通常置換
                        If SetPermutation_Normal(dataHBKE0202, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If

                    ElseIf StrCheck(i, 1) = CHANGE_PERMUTATION_DATE Then    '日付型置換形式の場合

                        While (True)

                            If intCount <> 0 Then

                                'インデックス番号が文字列の長さを超える場合はループ処理終了
                                If StrConvert.Length < intStartIndex + 1 Then
                                    Exit While
                                End If

                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)

                                '存在しない場合はループ処理終了
                                If intStartIndex = -1 Then
                                    Exit While
                                End If

                            End If

                            '日付変換後、置換
                            If GetIndex_Format(dataHBKE0202, StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If

                            '置換
                            If SetPermutation_Date(dataHBKE0202, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If

                            'カウントアップ
                            intCount = intCount + 1

                        End While

                    ElseIf StrCheck(i, 1) = CHANGE_PERMUTATION_MULTILINE Then   'N行置換形式の場合

                        'N行置換
                        If SetPermutation_Multiline(dataHBKE0202, StrConvert, StrCheck(i, 0), i) = False Then
                            Return False
                        End If

                    ElseIf StrCheck(i, 1) = CHANGE_PERMUTATION_MULTDATE Then    'N行日付型置換形式の場合

                        While (True)

                            If intCount <> 0 Then

                                'インデックス番号が文字列の長さを超える場合はループ処理終了
                                If StrConvert.Length < intStartIndex + 1 Then
                                    Exit While
                                End If

                                '存在チェック
                                intStartIndex = StrConvert.IndexOf(StrCheck(i, 0), intStartIndex + 1)

                                '存在しない場合はループ処理終了
                                If intStartIndex = -1 Then
                                    Exit While
                                End If

                            End If

                            '複数行変換（日付）後、置換
                            If GetIndex_Format(dataHBKE0202, StrConvert, StrCheck(i, 0), intStartIndex, strRetrunFormat, strReturnPermutation) = False Then
                                Return False
                            End If

                            '置換処理
                            If SetPermutation_MultilineDate(dataHBKE0202, StrConvert, strReturnPermutation, i, strRetrunFormat) = False Then
                                Return False
                            End If

                            'カウントアップ
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
        End Try

    End Function

    ''' <summary>
    ''' 日付フォーマット取得処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]チェック文字列</param>
    ''' <param name="intStringIndex">[IN]置き換え開始インデックス</param>
    ''' <param name="StrRetrunFormat">[IN/OUT]日付型フォーマット</param>
    ''' <param name="StrReturnPermutation">[IN/OUT]置換用文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取った本文、置換用文字列から日付型のフォーマットを取得し、フォーマットと置換用文字列を返す
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetIndex_Format(ByRef dataHBKE0202 As DataHBKE0202, _
                                    ByRef StrConvert As String, _
                                    ByVal StrCheck As String, _
                                    ByVal intStringIndex As Integer, _
                                    ByRef StrRetrunFormat As String, _
                                    ByRef StrReturnPermutation As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intStartIndex As Integer = intStringIndex   '開始インデックス
        Dim intLastIndex As Integer = 0                 '終了インデックス
        Dim intCount As Integer = 0                     'カウント用変数
        Dim blnIsEndChar As Boolean = False             '区切り文字フラグ

        Try
            '取得インデックスから本文終了まで1文字ずつループ
            For i As Integer = intStartIndex + StrCheck.Length To StrConvert.Length - 1 Step 1

                '区切り文字の場合は、インデックスを保存して、ループを抜ける
                If StrConvert(i) = END_CHAR Then
                    intLastIndex = i
                    blnIsEndChar = True
                    Exit For
                End If

                'カウントアップ
                intCount = intCount + 1

            Next

            '区切り文字が見つかった場合、取得したインデックスから文字列を取得
            If blnIsEndChar = True Then
                StrRetrunFormat = StrConvert.Substring(intStringIndex + StrCheck.Length, intCount)              'フォーマット形式
                StrReturnPermutation = StrConvert.Substring(intStringIndex, intLastIndex - intStringIndex + 1)  '残りの本文
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
        End Try

    End Function

    ''' <summary>
    ''' 置換処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え文字</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>置換処理を行う
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Normal(ByRef dataHBKE0202 As DataHBKE0202, _
                                          ByRef StrConvert As String, _
                                          ByVal StrCheck As String, _
                                          ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""
        Dim strTemp As String = ""

        Try
            With dataHBKE0202

                Select Case IntCount

                    Case COMMON_GROUPNM                             '（共通）グループ名

                        strPermutation = PropWorkGroupName

                    Case COMMON_USERID                              '（共通）ユーザーID

                        strPermutation = PropUserId

                    Case COMMON_USERNM                              '（共通）ユーザー名

                        strPermutation = PropUserName

                    Case KHN_CHGNMB                                 '（基本情報）変更管理番号

                        strPermutation = .PropStrNmb

                    Case KHN_STATUS                                 '（基本情報）ステータス

                        strPermutation = .PropStrProcessStateCD

                    Case KHN_TITLE                                  '（基本情報）タイトル

                        strPermutation = .PropStrTitle

                    Case KHN_NAIYO                                  '（基本情報）内容

                        strPermutation = .PropStrNaiyo

                    Case KHN_REGGRPNM                               '（基本情報）登録者業務チーム

                        strPermutation = .PropStrRegGrpNM

                    Case KHN_REGUSERNM                              '（基本情報）登録者

                        strPermutation = .PropStrRegNM

                    Case KHN_UPGRPNM                                '（基本情報）最終更新者業務チーム

                        strPermutation = .PropStrUpdateGrpNM

                    Case KHN_UPUSERNM                               '（基本情報）最終更新者

                        strPermutation = .PropStrUpdateNM

                    Case KHN_SYSTEM                                 '（基本情報）対象システム

                        'システム番号より対象システム取得し、設定
                        If GetCIInfoSystem(.PropStrSystemNmb, strPermutation) = False Then
                            Return False
                        End If

                    Case KHN_TANTOGRPNM                             '（基本情報）担当者業務グループ

                        strPermutation = .PropStrTantoGrpNM

                    Case KHN_TANTOUSERNM                            '（基本情報）変更担当者氏名

                        strPermutation = .PropStrTantoID
                        strPermutation &= " " & .PropStrTantoNM

                    Case KHN_TAISYO                                 '（基本情報）対処

                        strPermutation = .PropStrTaisyo

                    Case KHN_APPROVAL_USERNM                        '（基本情報）変更の承認者

                        strPermutation = .PropStrHenkouID
                        strPermutation &= " " & .PropStrHenkouNM

                    Case KHN_RECORDERUSERNM                         '（基本情報）変更承認記録者

                        strPermutation = .PropStrSyoninID
                        strPermutation &= " " & .PropStrSyoninNM

                    Case FREE_TEXT_1                                '（フリー入力情報）テキスト1

                        strPermutation = .PropStrBIko1

                    Case FREE_TEXT_2                                '（フリー入力情報）テキスト2

                        strPermutation = .PropStrBIko2

                    Case FREE_TEXT_3                                '（フリー入力情報）テキスト3

                        strPermutation = .PropStrBIko3

                    Case FREE_TEXT_4                                '（フリー入力情報）テキスト4

                        strPermutation = .PropStrBIko4

                    Case FREE_TEXT_5                                '（フリー入力情報）テキスト5

                        strPermutation = .PropStrBIko5

                    Case FREE_FLG_1                                 '（フリー入力情報）フラグ1

                        strPermutation = .PropStrFreeFlg1

                    Case FREE_FLG_2                                 '（フリー入力情報）フラグ2

                        strPermutation = .PropStrFreeFlg2

                    Case FREE_FLG_3                                 '（フリー入力情報）フラグ3

                        strPermutation = .PropStrFreeFlg3

                    Case FREE_FLG_4                                 '（フリー入力情報）フラグ4

                        strPermutation = .PropStrFreeFlg4

                    Case FREE_FLG_5                                 '（フリー入力情報）フラグ5

                        strPermutation = .PropStrFreeFlg5

                    Case GROUP_RIREKI                               '（グループ履歴）グループ履歴

                        strPermutation = .PropStrGrpHistory

                    Case TANTOH_RIREKI                              '（担当者履歴）担当者履歴

                        strPermutation = .PropStrTantoHistory
                    Case TANTOUSRSHI

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
                    Case COMMON_USERNMSEI
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
                End Select

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
        End Try

    End Function

    ''' <summary>
    ''' 置換処理_日付
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（日付）
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Date(ByRef dataHBKE0202 As DataHBKE0202, _
                                        ByRef StrConvert As String, _
                                        ByVal StrCheck As String, _
                                        ByVal IntCount As Integer, _
                                        ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""

        Try
            With dataHBKE0202

                Select Case IntCount

                    Case COMMON_NOW                                 '（共通）NOW

                        strPermutation = System.DateTime.Now.ToString(StrFormatForDate)

                    Case KHN_KAISI_DT                               '（基本情報）開始日時

                        '指定された日時形式に変換
                        If SetConvertDate(.PropStrKaisiDT & " " & .PropStrKaisiDT_HM, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                    Case KHN_KANRYO_DT                              '（基本情報）完了日時

                        '指定された日時形式に変換
                        If SetConvertDate(.PropStrKanryoDT & " " & .PropStrKanryoDT_HM, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                    Case KHN_REGDT                                  '（基本情報）登録日時

                        '指定された日時形式に変換
                        If SetConvertDate(.PropStrRegDT, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                    Case KHN_UPDATEDT                               '（基本情報）最終更新日時

                        '指定された日時形式に変換
                        If SetConvertDate(.PropStrUpdateDT, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                End Select

            End With

            '指定された形式で置換する
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
        End Try

    End Function

    ''' <summary>
    ''' 置換処理_複数行
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行）
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_Multiline(ByRef dataHBKE0202 As DataHBKE0202, _
                                             ByRef StrConvert As String, _
                                             ByVal StrCheck As String, _
                                             ByVal IntCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strPermutation As String = ""

        Try

            With dataHBKE0202

                Select Case IntCount

                    Case CYSPR_CYSPR                                    '（CYSPR）CYSPR

                        'CYSPR用変換処理
                        If SetConvertCYSPR(dataHBKE0202, strPermutation) = False Then
                            Return False
                        End If

                    Case RELATION_INFO                                  '（対応関係者情報）対応関係者

                        '対応関係者情報用変換処理
                        If SetConvertRelation(dataHBKE0202, strPermutation) = False Then
                            Return False
                        End If

                    Case PROCESSLINK_INFO                               '（プロセスリンク情報）プロセスリンク

                        'プロセスリンク用変換処理
                        If SetConvertProcessLink(dataHBKE0202, strPermutation) = False Then
                            Return False
                        End If

                End Select

            End With


            '指定された形式で置換する
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
        End Try

    End Function

    ''' <summary>
    ''' 置換処理_複数行変換（日付）
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrCheck">[IN]置き換え一覧</param>
    ''' <param name="IntCount">[IN]カウンタ</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>受け取ったテンプレートをメール用に変換する（複数行変換（日付））
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetPermutation_MultilineDate(ByRef dataHBKE0202 As DataHBKE0202, _
                                                 ByRef StrConvert As String, _
                                                 ByVal StrCheck As String, _
                                                 ByVal IntCount As Integer, _
                                                 ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim BlnGetFlg As Boolean = False                'CI番号取得フラグ

        '変数宣言
        Dim strPermutation As String = ""

        Try

            With dataHBKE0202

                Select Case IntCount

                    Case RELATIONFILE_INFO                              '（関連情報）関連ファイル

                        '関連ファイル用変換処理
                        If SetConvertRelationFile(dataHBKE0202, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                    Case MEETING_INFO                                   '（会議情報）会議

                        '会議情報用変換処理
                        If SetConvertMeeting(dataHBKE0202, strPermutation, StrFormatForDate) = False Then
                            Return False
                        End If

                End Select

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
    ''' <para>作成情報：2012/08/22 t.fukuo
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
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報取得
    ''' </summary>
    ''' <param name="StrSystemNmb">[IN]対象システム番号</param>
    ''' <param name="StrPermutation">[IN/OUT]置き換え文字</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報より対象システム（分類１＋分類２＋名称）を取得する
    ''' <para>作成情報：2012/08/22 t.fukuo
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

            '数値変換できる場合のみ、取得
            If Integer.TryParse(StrSystemNmb, intSysNmb) = True Then

                'コネクションを開く
                Cn.Open()

                '分類１＋分類２＋名称を取得
                If sqlHBKE0202.SelectCIInfoSql(Adapter, Cn, intSysNmb) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報取得", Nothing, Adapter.SelectCommand)

                'データを取得
                Adapter.Fill(dtResult)

                '取得データがある場合は戻り値に設定
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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
                Cn.Dispose()
            End If
            'リソースの開放
            Adapter.Dispose()
            dtResult.Dispose()
        End Try
    End Function

    '複数行変換処理-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 対応関係情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報データをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRelation(ByRef dataHBKE0202 As DataHBKE0202, ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0202.PropVwKankei.Sheets(0)

                If .Rows.Count = 0 Then

                    'データが0件の場合は戻り値に空文字を設定
                    StrConvert = ""

                Else

                    '行数分ループを行い、メール用テキストをセットする
                    For i As Integer = 0 To .Rows.Count - 1 Step 1

                        '区切り線を追加
                        StrConvert &= MAILPARTITION & vbCrLf

                        'スプレッドより区分、グループ名、ユーザIDを設定
                        If .GetText(i, COL_RELATION_KBN) = KBN_GROUP Then
                            StrConvert &= RELATION_KBN_NM_GROUP & .GetText(i, COL_RELATION_GROUPNM) & vbCrLf
                        Else
                            StrConvert &= RELATION_KBN_NM_USER & .GetText(i, COL_RELATION_ID) & " " & .GetText(i, COL_RELATION_USERNM) & vbCrLf
                        End If

                    Next

                    '最後に区切り線（-----）を追加
                    StrConvert &= MAILPARTITION

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
    ''' CYSPRデータ加工処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換対象文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRデータをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertCYSPR(ByRef dataHBKE0202 As DataHBKE0202, _
                                    ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0202.PropVwCYSPR.Sheets(0)

                If .Rows.Count = 0 Then

                    'データが0件の場合は戻り値に空文字を設定
                    StrConvert = ""

                Else

                    '行数分ループを行い、メール用テキストをセットする
                    For i As Integer = 0 To .Rows.Count - 1 Step 1

                        'スプレッドよりCYSPRを取得
                        Dim strData As String = .GetText(i, COL_CYSPR_NO)

                        '対象文字列に既に値がセットされている場合はカンマを追加
                        If StrConvert <> "" Then
                            StrConvert &= ","
                        End If

                        StrConvert &= strData

                    Next

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
        End Try

    End Function

    ''' <summary>
    ''' プロセスリンク情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換対象文字列</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertProcessLink(ByRef dataHBKE0202 As DataHBKE0202, _
                                          ByRef StrConvert As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0202.PropVwProcessLinkInfo.Sheets(0)

                If .Rows.Count = 0 Then

                    'データが0件の場合は戻り値に空文字を設定
                    StrConvert = ""

                Else

                    '行数分ループを行い、メール用テキストをセットする
                    For i As Integer = 0 To .Rows.Count - 1 Step 1

                        'スプレッドより区分名略称と番号を取得
                        Dim strData As String = .GetText(i, COL_PROCESSLINK_KBN_NMR) & " " & .GetText(i, COL_PROCESSLINK_NO)

                        '対象文字列に既に値がセットされている場合はカンマを追加
                        If StrConvert <> "" Then
                            StrConvert &= ","
                        End If

                        StrConvert &= strData

                    Next

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
        End Try

    End Function

    '複数行変換処理（日付アリ）-----------------------------------------------------------------------------------------------------------------
    ''' <summary>
    ''' 関連ファイル情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報データをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertRelationFile(ByRef dataHBKE0202 As DataHBKE0202, _
                                           ByRef StrConvert As String, _
                                           ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFileRegDt As String = ""                 'ファイル登録日時変換用文字列

        Try
            With dataHBKE0202.PropVwFileInfo.Sheets(0)

                If .Rows.Count = 0 Then

                    'データが0件の場合は戻り値に空文字を設定
                    StrConvert = ""

                Else

                    '行数分ループを行い、メール用テキストをセットする
                    For i As Integer = 0 To .Rows.Count - 1 Step 1

                        'ファイル登録日時をセットし、日付型に変換
                        If SetConvertDate(.GetText(i, COL_FILE_REGDT), strFileRegDt, StrFormatForDate) = False Then
                            Return False
                        End If

                        '区切り線（-----）を追加
                        StrConvert &= MAILPARTITION & vbCrLf

                        '関連ファイル情報データを追加
                        StrConvert &= .GetText(i, COL_MEETING_NO) & " "                 'ファイル説明
                        StrConvert &= strFileRegDt & vbCrLf                             'ファイル登録日時

                    Next

                    '最後に区切り線（-----）を追加
                    StrConvert &= MAILPARTITION

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
        End Try

    End Function

    ''' <summary>
    ''' 会議情報データ加工処理
    ''' </summary>
    ''' <param name="dataHBKE0202">[IN/OUT]変更登録（メール作成）Dataクラス</param>
    ''' <param name="StrConvert">[IN/OUT]変換文字列</param>
    ''' <param name="StrFormatForDate">[IN]日付フォーマット</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データをメール用に変換する
    ''' <para>作成情報：2012/08/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetConvertMeeting(ByRef dataHBKE0202 As DataHBKE0202, _
                                      ByRef StrConvert As String, _
                                      ByVal StrFormatForDate As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strJisshiDate As String = ""                 '実施日変換用文字列

        Try
            With dataHBKE0202.PropVwMeeting.Sheets(0)

                If .Rows.Count = 0 Then

                    'データが0件の場合は戻り値に空文字を設定
                    StrConvert = ""

                Else

                    '行数分ループを行い、メール用テキストをセットする
                    For i As Integer = 0 To .Rows.Count - 1 Step 1

                        '実施日をセットし、日付型に変換
                        If SetConvertDate(.GetText(i, COL_MEETING_JIBI), strJisshiDate, StrFormatForDate) = False Then
                            Return False
                        End If

                        '区切り線（-----）を追加
                        StrConvert &= MAILPARTITION & vbCrLf

                        '会議情報データを追加
                        StrConvert &= MEETING_NONM & .GetText(i, COL_MEETING_NO) & vbCrLf           '会議番号
                        StrConvert &= MEETING_JIBINM & strJisshiDate & vbCrLf                       '実施日
                        StrConvert &= MEETING_TITLENM & .GetText(i, COL_MEETING_TITLE) & vbCrLf     'タイトル
                        StrConvert &= MEETING_NINNM & .GetText(i, COL_MEETING_NIN) & vbCrLf         '承認

                    Next

                    '最後に区切り線（-----）を追加
                    StrConvert &= MAILPARTITION


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
        End Try

    End Function

End Class
