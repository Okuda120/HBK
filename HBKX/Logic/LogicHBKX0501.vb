Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Text

''' <summary>
''' エンドユーザー取込画面ロジッククラス
''' </summary>
''' <remarks>エンドユーザー取込画面のロジックを定義したクラス
''' <para>作成情報：2012/09/07 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0501

    'インスタンス作成
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK
    Private sqlHBKX0501 As New SqlHBKX0501

    'Public定数宣言
    'CSVのスタート行
    Public Const CSV_START_ROW As Integer = 1
    'CSVの項目インデックス
    Public Const CSV_ENDUSRID_NUM As Integer = 0                    'エンドユーザーID
    Public Const CSV_ENDUSRSEI_NUM As Integer = 1                   '姓
    Public Const CSV_ENDUSRMEI_NUM As Integer = 2                   '名
    Public Const CSV_ENDUSRSEIKANA_NUM As Integer = 3               '姓カナ
    Public Const CSV_ENDUSRMEIKANA_NUM As Integer = 4               '名カナ
    Public Const CSV_ENDUSRCOMPANY_NUM As Integer = 5               '所属会社
    Public Const CSV_ENDUSRBUSYONM_NUM As Integer = 6               '部署名
    Public Const CSV_ENDUSRTEL_NUM As Integer = 7                   '電話番号
    Public Const CSV_ENDUSRMAILADD_NUM As Integer = 8               'メールアドレス
    Public Const CSV_USRKBN_NUM As Integer = 9                      'ユーザー区分
    Public Const CSV_STATENAIYO_NUM As Integer = 10                 '状態説明
    'CSVファイル項目数
    Public Const CSV_COL_COUNT As Integer = 11

    '列名配列
    Private strColNm As String() = COLUMNNAME_ENDUSR

    'ログ出力文言
    Private strOutLog As String

    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力チェック処理
        If InputCheck(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/09/07 k.imayama  
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InputCheck(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数を宣言
        Dim strFilePath As String = dataHBKX0501.PropTxtFilePath.Text   'ファイルパス
        Dim strFileExt As String = ""

        Try
            'ファイル未選択チェック
            If strFilePath = "" Then
                'エラーメッセージセット
                puErrMsg = X0501_E001
                Return False
            End If

            'ファイル拡張子取得
            strFileExt = System.IO.Path.GetExtension(strFilePath)

            '拡張子チェック
            If strFileExt = EXTENTION_CSV Then
            Else
                'エラーメッセージセット
                puErrMsg = X0501_E002
                Return False
            End If

            '取込ファイルの存在チェック
            If System.IO.File.Exists(strFilePath) = False Then
                'エラーメッセージセット
                puErrMsg = X0501_E003
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行う
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputCheckMain(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力ファイルチェック処理
        If FileInputCheck(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ファイルの入力チェックを行い、入力チェックエラーが発生するとログファイルに書き込む
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputCheck(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログ文字列初期化
            strOutLog = ""

            '入力チェック
            If FileCheck(dataHBKX0501) = False Then
                Return False
            End If

            '入力チェックエラー時にログ出力用変数にデータがある場合ログ出力画面へ
            If strOutLog <> "" Then
                'ログ出力処理
                If SetOutLog(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックを行う
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileCheck(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '入力チェック用配列取得・入力チェック
            If SetArryInputForCheck(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェック用の配列をExcelからセットする
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetArryInputForCheck(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strBuffer As String = ""                                                '読込行
        Dim txtParser As Microsoft.VisualBasic.FileIO.TextFieldParser = Nothing     'CSVファイル読込用クラス
        Dim strFilePath As String                                                   '取込対象ファイル
        Dim strAryBuffer As String() = Nothing                                      '読込行データ格納用配列

        '入力チェック用
        Dim blnErrorFlg As Boolean = False                                          '入力チェック用フラグ用

        'DB接続用
        Dim Cn As New NpgsqlConnection(DbString)                                    'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter                                        'アダプタ

        '保存用配列初期化
        With dataHBKX0501
            .PropAryRowCount = New ArrayList                    '行番号
            .PropAryEndUsrID = New ArrayList                    'エンドユーザーID
            .PropAryEndUsrSei = New ArrayList                   '姓
            .PropAryEndUsrMei = New ArrayList                   '名
            .PropAryEndUsrSeikana = New ArrayList               '姓カナ
            .PropAryEndUsrMeikana = New ArrayList               '名カナ
            .PropAryEndUsrCompany = New ArrayList               '所属会社
            .PropAryEndUsrBusyoNM = New ArrayList               '部署名
            .PropAryEndUsrTel = New ArrayList                   '電話番号
            .PropAryEndUsrMailAdd = New ArrayList               'メールアドレス
            .PropAryUsrKbn = New ArrayList                      'ユーザー区分
            .PropAryStateNaiyo = New ArrayList                  '状態説明
            .PropAryProcMode = New ArrayList                    '処理モード
        End With

        Try
            '取込対象ファイルパスを変数にセット
            strFilePath = dataHBKX0501.PropTxtFilePath.Text

            'CSV読込クラスのインスタンス作成
            txtParser = New Microsoft.VisualBasic.FileIO.TextFieldParser(strFilePath, System.Text.Encoding.Default)
            'プロパティセット
            With txtParser
                txtParser.TextFieldType = Microsoft.VisualBasic.FileIO.FieldType.Delimited  '内容は区切り文字形式
                txtParser.SetDelimiters(",")                                                'デリミタはカンマ
            End With

            'ループカウンタセット
            Dim Count As Integer = CSV_START_ROW

            'コネクションを開く
            Cn.Open()

            '読み込む行がなくなるまで繰り返し
            While Not txtParser.EndOfData

                '1行を読み込んで配列に格納
                strAryBuffer = txtParser.ReadFields()

                '読込行の項目数が規定に満たない場合、配列を再定義して項目追加
                If strAryBuffer.Count < CSV_COL_COUNT Then
                    Dim intDiffCnt As Integer = CSV_COL_COUNT - strAryBuffer.Count - 1
                    ReDim Preserve strAryBuffer(CSV_COL_COUNT - 1)
                    For i As Integer = CSV_COL_COUNT - 1 - intDiffCnt To CSV_COL_COUNT - 1
                        strAryBuffer(i) = ""
                    Next
                End If

                'データクラスに保存
                With dataHBKX0501

                    .PropAryRowCount.Add(Count)                                         '行番号
                    .PropAryEndUsrID.Add(strAryBuffer(CSV_ENDUSRID_NUM))                'エンドユーザーID
                    .PropAryEndUsrSei.Add(strAryBuffer(CSV_ENDUSRSEI_NUM))              '姓
                    .PropAryEndUsrMei.Add(strAryBuffer(CSV_ENDUSRMEI_NUM))              '名
                    .PropAryEndUsrSeikana.Add(strAryBuffer(CSV_ENDUSRSEIKANA_NUM))      '姓カナ
                    .PropAryEndUsrMeikana.Add(strAryBuffer(CSV_ENDUSRMEIKANA_NUM))      '名カナ
                    .PropAryEndUsrCompany.Add(strAryBuffer(CSV_ENDUSRCOMPANY_NUM))      '所属会社
                    .PropAryEndUsrBusyoNM.Add(strAryBuffer(CSV_ENDUSRBUSYONM_NUM))      '部署名
                    .PropAryEndUsrTel.Add(strAryBuffer(CSV_ENDUSRTEL_NUM))              '電話番号
                    .PropAryEndUsrMailAdd.Add(strAryBuffer(CSV_ENDUSRMAILADD_NUM))      'メールアドレス
                    .PropAryUsrKbn.Add(strAryBuffer(CSV_USRKBN_NUM))                    'ユーザー区分
                    .PropAryStateNaiyo.Add(strAryBuffer(CSV_STATENAIYO_NUM))            '状態説明
                    .PropAryProcMode.Add(PROCMODE_NEW)                                  '処理モード（新規登録）

                    '入力項目必須、桁数、形式、存在チェック
                    If CheckInputForm(Adapter, Cn, dataHBKX0501, .PropAryEndUsrID.Count - 1) = False Then
                        'エラーフラグを立ててループを抜ける
                        blnErrorFlg = True
                        Exit While
                    End If

                End With

                'カウンタインクリメント
                Count += 1

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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">ループカウンタ</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力項目の必須、桁数、形式、存在チェックを行う
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKX0501 As DataHBKX0501, _
                                    ByRef intIndex As Integer) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0501

                'エンドユーザーIDの必須チェック
                If .PropAryEndUsrID(intIndex) = "" Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRID_NUM)) & vbCrLf
                End If

                ''エンドユーザーID（10文字まで）
                'If CehckLenB(.PropAryEndUsrID(intIndex).ToString) > 10 Then
                '    'メッセージログ設定
                '    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRID_NUM)) & vbCrLf
                'End If

                '----------------------20121003 鶴田修正--------------------------------------------------
                'エンドユーザーID（50文字まで）
                If .PropAryEndUsrID(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRID_NUM)) & vbCrLf
                End If
                '----------------------20121003 鶴田修正--------------------------------------------------

                'エンドユーザーIDの重複チェック
                For i As Integer = 0 To intIndex - 1
                    If .PropAryEndUsrID(i) = .PropAryEndUsrID(intIndex) Then
                        'メッセージログ設定
                        strOutLog &= String.Format(X0501_E008, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRID_NUM)) & vbCrLf
                        Exit For
                    End If
                Next
                'エンドユーザーIDの存在チェック
                If .PropAryEndUsrID(intIndex) <> "" Then
                    If CheckEndUsrID(Adapter, Cn, dataHBKX0501, intIndex) = False Then
                        Return False
                    End If
                End If

                '姓の必須チェック
                If .PropAryEndUsrSei(intIndex) = "" Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRSEI_NUM)) & vbCrLf
                End If
                '姓（50文字まで）
                If .PropAryEndUsrSei(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRSEI_NUM)) & vbCrLf
                End If

                '[del] 2012/10/16 s.yamaguchi 必須チェック要望修正 START
                ''名の必須チェック
                'If .PropAryEndUsrMei(intIndex) = "" Then
                '    'メッセージログ設定
                '    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMEI_NUM)) & vbCrLf
                'End If
                '[del] 2012/10/16 s.yamaguchi 必須チェック要望修正 END

                '名（50文字まで）
                If .PropAryEndUsrMei(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMEI_NUM)) & vbCrLf
                End If

                '[del] 2012/09/24 y.ikushima 必須チェック要望修正START
                ''姓カナの必須チェック
                'If .PropAryEndUsrSeikana(intIndex) = "" Then
                '    'メッセージログ設定
                '    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRSEIKANA_NUM)) & vbCrLf
                'End If
                '[del] 2012/09/24 y.ikushima 必須チェック要望修正END
                '姓カナ（50文字まで）
                If .PropAryEndUsrSeikana(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRSEIKANA_NUM)) & vbCrLf
                End If

                '[del] 2012/09/24 y.ikushima 必須チェック要望修正START
                ''名カナの必須チェック
                'If .PropAryEndUsrMeikana(intIndex) = "" Then
                '    'メッセージログ設定
                '    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMEIKANA_NUM)) & vbCrLf
                'End If
                '[del] 2012/09/24 y.ikushima 必須チェック要望修正END
                '名カナ（50文字まで）
                If .PropAryEndUsrMeikana(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMEIKANA_NUM)) & vbCrLf
                End If

                '[add] 2015/08/21 y.naganuma 要望修正 START
                '氏名（50文字まで）
                If .PropAryEndUsrSei(intIndex).ToString.Length + .PropAryEndUsrMei(intIndex).ToString.Length + 1 > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E010, .PropAryRowCount(intIndex), "氏名", "姓", "名") & vbCrLf
                End If

                '氏名カナ（50文字まで）
                If .PropAryEndUsrSeikana(intIndex).ToString.Length + .PropAryEndUsrMeikana(intIndex).ToString.Length + 1 > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E010, .PropAryRowCount(intIndex), "氏名カナ", "姓カナ", "名カナ") & vbCrLf
                End If
                '[add] 2015/08/21 y.naganuma 要望修正 END

                '所属会社（50文字まで）
                If .PropAryEndUsrCompany(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRCOMPANY_NUM)) & vbCrLf
                End If

                '部署名（50文字まで）
                If .PropAryEndUsrBusyoNM(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRBUSYONM_NUM)) & vbCrLf
                End If

                '電話番号（50文字まで）
                If .PropAryEndUsrTel(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRTEL_NUM)) & vbCrLf
                End If

                'メールアドレス（50文字まで）
                If .PropAryEndUsrMailAdd(intIndex).ToString.Length > 50 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMAILADD_NUM)) & vbCrLf
                End If
                '[del] 2012/09/24 y.ikushima 必須チェック要望修正START
                ''メールアドレスの形式チェック
                'If .PropAryEndUsrMailAdd(intIndex) <> "" Then
                '    If commonLogicHBK.IsMailAddress(.PropAryEndUsrMailAdd(intIndex)) = False Then
                '        'メッセージログ設定
                '        strOutLog &= String.Format(X0501_E007, .PropAryRowCount(intIndex), strColNm(CSV_ENDUSRMAILADD_NUM)) & vbCrLf
                '    End If
                'End If
                '[del] 2012/09/24 y.ikushima 必須チェック要望修正END

                'ユーザー区分の必須チェック
                If .PropAryUsrKbn(intIndex) = "" Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E005, .PropAryRowCount(intIndex), strColNm(CSV_USRKBN_NUM)) & vbCrLf
                End If
                'ユーザー区分（100文字まで）
                If .PropAryUsrKbn(intIndex).ToString.Length > 100 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_USRKBN_NUM)) & vbCrLf
                End If

                '状態説明（100文字まで）
                If .PropAryStateNaiyo(intIndex).ToString.Length > 100 Then
                    'メッセージログ設定
                    strOutLog &= String.Format(X0501_E006, .PropAryRowCount(intIndex), strColNm(CSV_STATENAIYO_NUM)) & vbCrLf
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
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function CehckLenB(ByVal stTarget As String) As Integer
        Return System.Text.Encoding.GetEncoding("Shift_JIS").GetByteCount(stTarget)
    End Function

    ''' <summary>
    ''' エンドユーザーID存在チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたエンドユーザーIDをエンドユーザーマスターからデータを検索し存在するかチェックする
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckEndUsrID(ByVal Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByRef dataHBKX0501 As DataHBKX0501, _
                                            ByRef IntIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable

        Try
            'エンドユーザーIDのデータ有無取得（SELECT）用SQLを作成
            If sqlHBKX0501.SetSelectEndUsrSql(Adapter, Cn, dataHBKX0501, IntIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドデータIDのデータ有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            With dataHBKX0501
                If dtResult.Rows.Count > 0 Then
                    '登録方法「1:画面入力」で存在する場合、エラー
                    If dtResult.Rows(0).Item("RegKbn") = DATA_REG_FROMENTRY Then
                        'エラーメッセージ設定
                        strOutLog &= String.Format(X0501_E009, .PropAryRowCount(IntIndex), strColNm(CSV_ENDUSRID_NUM)) & vbCrLf
                    End If
                    '処理モード（編集）
                    .PropAryProcMode(IntIndex) = PROCMODE_EDIT
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
    ''' エラーログ出力処理
    ''' </summary>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力チェックでエラーとなった内容をログ出力する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 :</p>
    ''' </para></remarks>
    Public Function SetOutLog(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

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
                puErrMsg = String.Format(X0501_E004, strOutputpath)

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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>登録処理を行う
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegMain(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力データ登録処理
        If FileInputDataReg(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>入力データの登録処理を行う
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileInputDataReg(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        'ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)            'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()              'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing              'トランザクション
        Dim blnErrorFlg As Boolean = False                  'エラーフラグ

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'システム日付取得
            If SelectSysDate(Adapter, Cn, dataHBKX0501) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            With dataHBKX0501

                'ループ
                For i As Integer = 0 To .PropAryEndUsrID.Count - 1 Step 1

                    If .PropAryProcMode(i) = PROCMODE_NEW Then
                        'エンドユーザーマスター新規追加
                        If InsertEndUser(Cn, dataHBKX0501, i) = False Then
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            'エラーフラグを立ててループを抜ける
                            blnErrorFlg = True
                            Exit For
                        End If

                    ElseIf .PropAryProcMode(i) = PROCMODE_EDIT Then
                        'エンドユーザーマスター更新
                        If UpdateEndUser(Cn, dataHBKX0501, i) = False Then
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

            'エラーフラグ
            If blnErrorFlg = True Then
                Tsx.Rollback()
            Else
                'コミット
                Tsx.Commit()
            End If

            'エラーフラグがONの場合、Falseを返す
            If blnErrorFlg = True Then
                Return False
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
    ''' サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/09/07 k.imayama 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKX0501 As DataHBKX0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable

        Try
            'SQLを作成
            If sqlHBKX0501.SetSelectSysDateSql(Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバ日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX0501.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
            dtSysDate.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' エンドユーザーマスター新規追加
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスターに取込データの内容を新規登録（INSERT）する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertEndUser(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKX0501 As DataHBKX0501, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'エンドユーザーマスター新規登録（INSERT）用SQLを作成
            If sqlHBKX0501.SetInsertEndUsrSql(Cmd, Cn, dataHBKX0501, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター新規登録", Nothing, Cmd)

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
    ''' エンドユーザーマスター更新
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスターに取込データの内容で更新（UPDATE）する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateEndUser(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKX0501 As DataHBKX0501, _
                                   ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'エンドユーザーマスター更新（UPDATE）用SQLを作成
            If sqlHBKX0501.SetUpdateEndUsrSql(Cmd, Cn, dataHBKX0501, intIndex) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター更新", Nothing, Cmd)

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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0501) = False Then
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
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0501 As DataHBKX0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX0501

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
    ''' 特権ログインログ出力処理
    ''' </summary>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログアウトログを出力する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputLogLogin(ByVal dataHBKX0501 As DataHBKX0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '特権ログインログ登録
            If InsertSuperLoginLog(Tsx, Cn, dataHBKX0501) = False Then
                Return False
            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 特権ログインログ登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0501">[IN]特権ユーザーログイン（エンドユーザ取込）画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合にログアウトログを出力する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSuperLoginLog(ByRef Tsx As NpgsqlTransaction, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0501 As DataHBKX0501) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '特権ログインログ（INSERT）用SQLを作成
            If sqlHBKX0501.SetInsertSuperLoginLogSql(Cmd, Cn, dataHBKX0501) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "特権ログインログ登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
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
            Cmd.Dispose()
        End Try

    End Function
End Class
