Imports Common
Imports CommonHBK
Imports System.Text
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' セット選択画面Logicクラス
''' </summary>
''' <remarks>セット選択索画面のロジックを定義する
''' <para>作成情報：2012/09/19 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKC0701

    'インスタンス作成
    Private sqlHBKC0701 As New SqlHBKC0701
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言========================
    '列番号
    Public Const COL_SELECT As Integer = 0          '選択
    Public Const COL_KIKINM As Integer = 1          '機器（種別名＋番号）
    Public Const COL_USRID As Integer = 2           'ユーザーID
    Public Const COL_USRNM As Integer = 3           'ユーザー氏名
    Public Const COL_CISTATUSNM As Integer = 4      'CIステータス名
    Public Const COL_CINMB As Integer = 5           'CI番号　　　        ※非表示
    Public Const COL_KINDCD As Integer = 6          '種別CD  　　　　　　※非表示
    Public Const COL_NUM As Integer = 7             '番号  　　　　　　　※非表示
    Public Const COL_CISTATUSCD As Integer = 8      'CIステータスCD  　　※非表示
    Public Const COL_SETKIKIID As Integer = 9       'セットID          　※非表示

    '件数ラベルフォーマット
    Private Const LABEL_CNT_FORMAT As String = "{0}件({1}台)"


    ''' <summary>
    ''' フォームロード時メイン処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド表示用データテーブル作成
            If CreateDataTableForVw(dataHBKC0701) = False Then
                Return False
            End If

            'フォームコントロール設定
            If SetFormControl(dataHBKC0701) = False Then
                Return False
            End If

            'コンボボックス用マスタデータ取得
            If GetMastaData(dataHBKC0701) = False Then
                Return False
            End If

            '初期取得データ設定
            If SetInitDataToControl(dataHBKC0701) = False Then
                Return False
            End If


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索の件数取得メイン
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行い結果件数を取得する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetKikiCountMain(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '件数取得
        If GetKikiCount(dataHBKC0701) = False Then
            Return False
        End If

        '取得データチェック
        If CheckGetData(dataHBKC0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 検索メイン処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索処理
        If Search(dataHBKC0701) = False Then
            Return False
        End If

        '取得データ設定
        If SetSheet(dataHBKC0701) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 一覧セルクリック時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0701">[IN/OUT]セット選択画面データクラス</param>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧のチェックボックス状態を制御する
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClickVwCellMain(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'チェックボックスをラジオボタンのように制御する
            If SetCheckAsRadio(dataHBKC0701) = False Then
                Return False
            End If

            '選択フラグON
            dataHBKC0701.PropBlnSelected = True


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 戻り値作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0701">[IN/OUT]セット選択画面データクラス</param>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>入力チェックを行い、問題がなければ戻り値用のテーブルを作成する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateReturnDataMain(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '一覧入力チェック
            If CheckInputVw(dataHBKC0701) = False Then
                Return False
            End If

            '戻り値作成
            If CreateReturnData(dataHBKC0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' マスタデータ取得
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスリスト用のマスタデータを取得する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKC0701

                '種別マスタ取得
                '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
                'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, .PropDtKindMasta) = False Then
                '    Return False
                'End If
                If commonLogicHBK.GetKindMastaData(Adapter, Cn, CI_TYPE_SUPORT, .PropDtKindMasta, 0) = False Then
                    Return False
                End If
                '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索の件数取得
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKikiCount(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '検索SQLの作成・設定
            If sqlHBKC0701.SetSelectCountSetKikiSql(Adapter, Cn, dataHBKC0701) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            '取得データをデータクラスへ保存
            dataHBKC0701.PropIntKikiCount = Table.Rows(0)(0)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 取得データチェック
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>データが取得できたかチェックする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckGetData(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索結果が１件以上存在するか
            If dataHBKC0701.PropIntKikiCount <= 0 Then
                '1件も取得できなかった場合、メッセージ表示
                puErrMsg = C0701_E003
                '一覧クリア
                If ClearVw(dataHBKC0701) = False Then
                    Return False
                End If
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 一覧クリア処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>一覧をクリアする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearVw(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0701

                'データをクリアして一覧にセット
                .PropDtKiki.Clear()
                .PropVwList.DataSource = .PropDtKiki

                '件数ラベルセット
                If SetLblCount(dataHBKC0701) = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' セル結合処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>セット毎に一覧のセルを結合する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddSpanCell(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intBefSetKikiID As Integer              '前行のセットID
        Dim intCurSetKikiID As Integer              'カレント行のセットID
        Dim intStartSpanSetKikiID As Integer        'セットIDの結合スタート行
        Dim intCountSpanSetKikiID As Integer        'セットIDの結合行数
        Dim intSetKikiCnt As Integer                'セット数

        Try
            With dataHBKC0701.PropVwList
                If .RowCount > 0 Then

                    '変数初期化
                    intBefSetKikiID = .GetValue(0, COL_SETKIKIID)
                    intCurSetKikiID = 0
                    intStartSpanSetKikiID = 0
                    intCountSpanSetKikiID = 0
                    intSetKikiCnt = 1

                    '一覧全行チェック
                    For i As Integer = 0 To .RowCount

                        '最終行以前の場合
                        If i < .RowCount Then

                            'カレント行のセットIDを取得
                            intCurSetKikiID = .GetValue(i, COL_SETKIKIID)

                            'カレント行のセットIDが前行と等しいかチェック
                            If intCurSetKikiID <> 0 AndAlso intBefSetKikiID <> 0 AndAlso intCurSetKikiID = intBefSetKikiID Then

                                '等しい場合はセットID結合行数をカウントアップ
                                intCountSpanSetKikiID += 1

                            Else

                                'カレント行のセットIDが前行と異なり、結合行数が1行以上の場合、
                                'チェックボックス、ユーザーID、ユーザー氏名のセル結合を行う
                                If intCountSpanSetKikiID > 0 Then

                                    .AddSpanCell(intStartSpanSetKikiID, COL_SELECT, intCountSpanSetKikiID, 1)           'チェックボックス
                                    .AddSpanCell(intStartSpanSetKikiID, COL_USRID, intCountSpanSetKikiID, 1)            'ユーザーID
                                    .AddSpanCell(intStartSpanSetKikiID, COL_USRNM, intCountSpanSetKikiID, 1)            'ユーザー氏名

                                    '結合スタート行、結合行数初期化
                                    intStartSpanSetKikiID = i
                                    intCountSpanSetKikiID = 1

                                End If

                                'セット機器数カウントアップ
                                intSetKikiCnt += 1

                            End If

                            '前行のセットIDをカレント行の値で更新
                            intBefSetKikiID = intCurSetKikiID

                        Else

                            '最終行まで処理した後、結合行数が1行以上の場合、
                            'チェックボックス、ユーザーID、ユーザー氏名のセル結合を行う
                            If intCountSpanSetKikiID > 0 Then

                                .AddSpanCell(intStartSpanSetKikiID, COL_SELECT, intCountSpanSetKikiID, 1)           'チェックボックス
                                .AddSpanCell(intStartSpanSetKikiID, COL_USRID, intCountSpanSetKikiID, 1)            'ユーザーID
                                .AddSpanCell(intStartSpanSetKikiID, COL_USRNM, intCountSpanSetKikiID, 1)            'ユーザー氏名

                            End If

                        End If

                    Next

                    'セット機器数をデータクラスにセット
                    dataHBKC0701.PropIntSetCnt = intSetKikiCnt

                End If
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 件数ラベルセット処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>件数ラベルに値をセットする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLblCount(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0701

                '件数ラベルを設定
                .PropLblCount.Text = _
                    String.Format(LABEL_CNT_FORMAT, .PropIntSetCnt.ToString(), .PropIntKikiCount.ToString())

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Search(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        'データ初期化
        dataHBKC0701.PropDtKiki.Clear()

        Try
            'コネクションを開く
            Cn.Open()

            '検索用SQLの作成・設定
            If sqlHBKC0701.SetSelectSetKikiSql(Adapter, Cn, dataHBKC0701) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "セット機器情報検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0701.PropDtKiki)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期取得データ設定
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '取得データを基にコンボボックスリストをセット
            If SetCmbData(dataHBKC0701) = False Then
                Return False
            End If

            '検索条件初期化
            If SetInitSearchCond(dataHBKC0701) = False Then
                Return False
            End If

            '一覧スプレッドデータ設定
            If SetSheet(dataHBKC0701) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' コンボボックスリスト設定
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbData(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0701

                '種別コンボボックスセット
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, True, "", "") = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索条件初期設定
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchCond(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0701

                '種別コンボボックス
                .PropCmbKind.SelectedValue = ""

                '番号テキストボックス
                .PropTxtNum.Text = ""

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' シート情報セット
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>シートに情報をセットする
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSheet(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0701

                'シートにデータをセット
                .PropVwList.DataSource = .PropDtKiki

            End With

            'セル結合
            If AddSpanCell(dataHBKC0701) = False Then
                Return False
            End If

            '件数設定
            If SetLblCount(dataHBKC0701) = False Then
                Return False
            End If


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKC0701">[IN/OUT]セット選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKiki As New DataTable   '結果一覧用データテーブル

        Try
            '結果一覧用テーブル作成
            With dtKiki
                .Columns.Add("Select", Type.GetType("System.Boolean"))          '選択
                .Columns.Add("KikiNM", Type.GetType("System.String"))           '機器（種別名＋番号）
                .Columns.Add("EndUsrID", Type.GetType("System.String"))         'ユーザーID
                .Columns.Add("EndUsrNM", Type.GetType("System.String"))         'ユーザー氏名
                .Columns.Add("CIStateNM", Type.GetType("System.String"))        'CIステータス名
                .Columns.Add("CINmb", Type.GetType("System.String"))            'CI番号
                .Columns.Add("KindCD", Type.GetType("System.String"))           '種別コード
                .Columns.Add("Num", Type.GetType("System.String"))              '番号
                .Columns.Add("CIStatusCD", Type.GetType("System.String"))       'CIステータスCD
                .Columns.Add("SetKikiID", Type.GetType("System.String"))        'セットID
                .Columns.Add("WorkCD", Type.GetType("System.String"))           '作業CD
                .Columns.Add("WorkNM", Type.GetType("System.String"))           '作業名
                'テーブルの変更を確定
                .AcceptChanges()
            End With


            '作成テーブルをデータクラスにセット
            dataHBKC0701.PropDtKiki = dtKiki

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
            dtKiki.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームコントロール設定
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControl(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0701

                'スプレッドの設定
                If SetInitVwList(dataHBKC0701) = False Then
                    Return False
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 一覧スプレッド設定
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの初期設定を行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetInitVwList(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0701.PropVwList

                '列のデータフィールドの設定
                .Columns(COL_SELECT).DataField = "Select"               '選択
                .Columns(COL_KIKINM).DataField = "KikiNM"               '機器（種別名＋番号）
                .Columns(COL_USRID).DataField = "EndUsrID"              'ユーザーID
                .Columns(COL_USRNM).DataField = "EndUsrNM"              'ユーザー氏名
                .Columns(COL_CISTATUSNM).DataField = "CIStateNM"        'CIステータス名
                .Columns(COL_CINMB).DataField = "CINmb"                 'CI番号
                .Columns(COL_KINDCD).DataField = "KindCD"               '種別コード
                .Columns(COL_NUM).DataField = "Num"                     '番号
                .Columns(COL_CISTATUSCD).DataField = "CIStatusCD"       'CIステータスCD
                .Columns(COL_SETKIKIID).DataField = "SetKikiID"         'セットID

                '隠し列非表示
                .Columns(COL_CINMB).Visible = False                     'CI番号
                .Columns(COL_KINDCD).Visible = False                    '種別コード
                .Columns(COL_NUM).Visible = False                       '番号
                .Columns(COL_CISTATUSCD).Visible = False                'CIステータスCD
                .Columns(COL_SETKIKIID).Visible = False                 'セットID

                '単一選択のみ可
                .Columns(COL_SELECT).Locked = True                      '選択

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' チェックボックス疑似ラジオボタン制御処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>既にチェックの入っている行のチェックを外し、選択行のチェックをつける
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCheckAsRadio(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedSetKikiID As Integer = 0

        Try
            With dataHBKC0701

                '選択行（セット）にはチェックをつけ、それ以外はチェックを外す
                For i As Integer = 0 To .PropVwList.RowCount - 1

                    'チェックを外す
                    .PropVwList.SetValue(i, COL_SELECT, False)

                    '選択行および選択行と同じセットの機器にはチェックをつける
                    If i = .PropIntCheckIndex Or _
                       (intSelectedSetKikiID > 0 And intSelectedSetKikiID = .PropVwList.GetValue(i, COL_SETKIKIID)) Then
                        .PropVwList.SetValue(i, COL_SELECT, True)
                        intSelectedSetKikiID = .PropVwList.GetValue(i, COL_SETKIKIID)
                    End If

                Next

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 一覧入力チェック処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの入力チェックを行う
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputVw(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0701

                '選択フラグがOFFの場合、エラー
                If .PropBlnSelected = False Then
                    puErrMsg = C0701_E001
                    Return False
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 戻り値データ作成処理
    ''' <paramref name="dataHBKC0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>選択データを戻り値のデータテーブルに設定する
    ''' <para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateReturnData(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetKiki As DataTable = Nothing    '一覧データテーブル
        Dim dtReturn As DataTable = Nothing     '戻り値用データテーブル

        Try
            With dataHBKC0701

                '一覧のデータソースをデータテーブルに変換
                dtSetKiki = DirectCast(.PropVwList.DataSource, DataTable)

                '戻り値用テーブルに構造をコピー
                dtReturn = dtSetKiki.Clone()

                '選択データを戻り値用テーブルにインポート
                Dim rowsSelect = From row As DataRow In dtSetKiki _
                                 Where row.Item("Select") = True
                For Each row As DataRow In rowsSelect
                    dtReturn.ImportRow(row)
                Next

                'テーブルの変更を確定
                dtReturn.AcceptChanges()

                'データクラスにセット
                .PropDtReturn = dtReturn

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If dtSetKiki IsNot Nothing Then
                dtSetKiki.Dispose()
            End If
            If dtReturn IsNot Nothing Then
                dtReturn.Dispose()
            End If
        End Try

    End Function

End Class
