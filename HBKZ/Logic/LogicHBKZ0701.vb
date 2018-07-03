Imports Common
Imports CommonHBK
Imports System.Text
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 機器検索一覧画面Logicクラス
''' </summary>
''' <remarks>機器検索一覧索画面のロジックを定義する
''' <para>作成情報：2012/07/06 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKZ0701

    'インスタンス作成
    Private sqlHBKZ0701 As New SqlHBKZ0701
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言========================
    '列番号
    Public Const COL_SELECT As Integer = 0          '選択
    Public Const COL_KINDNM As Integer = 1          '種別名
    Public Const COL_NUM As Integer = 2             '番号
    Public Const COL_CLASS2 As Integer = 3          '分類２（メーカー）
    Public Const COL_CINM As Integer = 4            '名称（機種）
    Public Const COL_KINDCD As Integer = 5          '種別コード　     　 ※非表示
    Public Const COL_CINMB As Integer = 6           'CI番号　　　        ※非表示
    Public Const COL_KIKIUSEKBN As Integer = 7      '機器利用区分　　　　※非表示
    Public Const COL_SETUPFLG As Integer = 8        'セットアップフラグ　※非表示
    Public Const COL_CIKBNCD As Integer = 9         'CI種別CD　　　　　　※非表示
    '検索機器区分
    Public Const SEARCH_KIKI_SAP As String = "1"    'サポセン機器
    Public Const SEARCH_KIKI_BUY As String = "2"    '部所有機器
    Public Const SEARCH_KIKI_SAPBUY As String = "3" 'サポセン／部所有機器


    ''' <summary>
    ''' フォームロード時メイン処理
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索条件プロパティ初期設定
            If SetInitSearchProp(dataHBKZ0701) = False Then
                Return False
            End If

            'スプレッド表示用データテーブル作成
            If CreateDataTableForVw(dataHBKZ0701) = False Then
                Return False
            End If

            'フォームコントロール設定
            If SetFormControl(dataHBKZ0701) = False Then
                Return False
            End If

            'コンボボックス用マスタデータ取得
            If GetMastaData(dataHBKZ0701) = False Then
                Return False
            End If

            '初期取得データ設定
            If SetInitDataToControl(dataHBKZ0701) = False Then
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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行い結果件数を取得する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetKikiCountMain(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '件数取得
        If GetKikiCount(dataHBKZ0701) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 検索メイン処理
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索処理
        If Search(dataHBKZ0701) = False Then
            Return False
        End If

        '取得データ設定
        If SetSheet(dataHBKZ0701) = False Then
            Return False
        End If

        ''取得データチェック
        'If CheckGetData(dataHBKZ0701) = False Then
        '    Return False
        'End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 選択ボタンクリック時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ0701">[IN/OUT]機器検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckWhenBtnSelectClickMain(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '一覧入力チェック
            If CheckInputVw(dataHBKZ0701) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' マスタデータ取得
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスリスト用のマスタデータを取得する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKZ0701

                '種別マスタ取得
                '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
                'If commonLogicHBK.GetKindMastaData(Adapter, Cn, .PropStrCIKbnCD, .PropDtKindMasta) = False Then
                '    Return False
                'End If
                If commonLogicHBK.GetKindMastaData(Adapter, Cn, .PropStrCIKbnCD, .PropDtKindMasta, 0) = False Then
                    Return False
                End If
                '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

                'CIステータスマスタ取得
                If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, .PropStrCIKbnCD, .PropDtCIStatusMasta, .PropStrCIStatusCD) = False Then
                    Return False
                End If

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKikiCount(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            '検索SQLの作成・設定
            If sqlHBKZ0701.SetSelectCountKikiSearchSql(Adapter, Cn, dataHBKZ0701) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン機器／部所有機器検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ0701.PropIntKikiCount = Table.Rows(0)(0)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>データが取得できたかチェックする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckGetData(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索結果が１件以上存在するか
            If dataHBKZ0701.PropIntKikiCount <= 0 Then
                '1件も取得できなかった場合、メッセージ表示
                puErrMsg = Z0701_I001
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Search(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        'データ初期化
        dataHBKZ0701.PropDtKiki.Clear()

        Try
            'コネクションを開く
            Cn.Open()

            '検索用SQLの作成・設定
            If sqlHBKZ0701.SetSelectKikiSearchSql(Adapter, Cn, dataHBKZ0701) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サポセン／部所有機器情報検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKZ0701.PropDtKiki)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '取得データを基にコンボボックスリストをセット
            If SetCmbData(dataHBKZ0701) = False Then
                Return False
            End If

            '検索条件初期化
            If SetInitSearchCond(dataHBKZ0701) = False Then
                Return False
            End If

            '一覧スプレッドデータ設定
            If SetSheet(dataHBKZ0701) = False Then
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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbData(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0701

                '種別コンボボックスセット
                If commonLogic.SetCmbBox(.PropDtKindMasta, .PropCmbKind, True, "", "") = False Then
                    Return False
                End If

                'ステータスコンボボックスセット　※CI種別がサポセンでCIステータス設定時は空白行なし
                If .PropStrCIKbnCD = CI_TYPE_SUPORT And .PropStrCIStatusCD <> "" Then
                    If commonLogic.SetCmbBox(.PropDtCIStatusMasta, .PropCmbCIStatus, False) = False Then
                        Return False
                    End If
                Else
                    If commonLogic.SetCmbBox(.PropDtCIStatusMasta, .PropCmbCIStatus, True, "", "") = False Then
                        Return False
                    End If
                End If


            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索条件初期設定
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchCond(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0701

                '種別コンボボックス
                .PropCmbKind.SelectedValue = ""

                '番号テキストボックス
                .PropTxtNum.Text = ""

                'CIステータスコンボボックス
                .PropCmbCIStatus.SelectedIndex = 0

                '名称（機種）
                .PropTxtCINM.Text = ""

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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>シートに情報をセットする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSheet(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ0701

                'シートにデータをセット
                .PropVwList.DataSource = .PropDtKiki

                '件数設定
                .PropLblCount.Text = .PropIntKikiCount.ToString & "件"

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
    ''' 検索条件プロパティ初期設定
    ''' </summary>
    ''' <param name="dataHBKZ0701">[IN/OUT]機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件として用いるプロパティの初期設定を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchProp(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701

                'CI種別コード
                If SetInitSearchPropCIKbnCD(dataHBKZ0701) = False Then
                    Return False
                End If

                'CIステータスコード
                If SetInitSearchPropCIStatusCD(dataHBKZ0701) = False Then
                    Return False
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
    ''' 検索条件プロパティ初期設定：CI種別コード
    ''' </summary>
    ''' <param name="dataHBKZ0701">[IN/OUT]機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件として用いるCI種別コードの初期設定を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchPropCIKbnCD(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701

                'CI種別コード
                If .PropStrCIKbnCD Is Nothing Or .PropStrCIKbnCD = "" Then

                    '呼び出し時に指定されていない場合、サポセン機器と部所有機器のCI種別コードをセット
                    .PropStrCIKbnCD = CI_TYPE_SUPORT & "," & CI_TYPE_KIKI

                End If

                'CI種別コード配列作成
                .PropStrAryCIKbnCD = .PropStrCIKbnCD.Split(",")

                '検索機器区分を設定
                If .PropStrAryCIKbnCD.Length = 1 Then
                    'CI種別コードが1つのみ指定されている場合
                    If .PropStrAryCIKbnCD(0) = CI_TYPE_SUPORT Then
                        '検索機器区分にサポセン機器をセット
                        .PropStrSearchKikiKbn = SEARCH_KIKI_SAP
                    ElseIf .PropStrAryCIKbnCD(0) = CI_TYPE_KIKI Then
                        '検索機器区分に部所有機器をセット
                        .PropStrSearchKikiKbn = SEARCH_KIKI_BUY
                    End If
                ElseIf .PropStrAryCIKbnCD.Length = 2 Then
                    'CI種別コードが2つ指定されている場合、検索機器区分にサポセン／部所有をセット
                    .PropStrSearchKikiKbn = SEARCH_KIKI_SAPBUY
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
    ''' 検索条件プロパティ初期設定：CIステータスコード
    ''' </summary>
    ''' <param name="dataHBKZ0701">[IN/OUT]機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件として用いるCIステータスコードの初期設定を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchPropCIStatusCD(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701

                'CIステータスコード
                If .PropStrCIStatusCD Is Nothing Or .PropStrCIStatusCD = "" Then

                    '呼び出し時に指定されていない場合、空白をセット
                    .PropStrCIStatusCD = ""

                End If

                'CIステータスコード配列作成
                .PropStrAryCIStatusCD = .PropStrCIStatusCD.Split(",")

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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKZ0701">[IN/OUT]機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtKiki As New DataTable   '結果一覧用データテーブル

        Try
            '結果一覧用テーブル作成
            With dtKiki
                .Columns.Add("Select", Type.GetType("System.Boolean"))       '選択
                .Columns.Add("Num", Type.GetType("System.String"))           '番号
                .Columns.Add("KindNM", Type.GetType("System.String"))        '種別名
                .Columns.Add("Class2", Type.GetType("System.String"))        '分類２（メーカー）
                .Columns.Add("CINM", Type.GetType("System.String"))          '名称（機器）
                .Columns.Add("KindCD", Type.GetType("System.String"))        '種別コード
                .Columns.Add("CINmb", Type.GetType("System.String"))         'CI番号
                .Columns.Add("KikiUseKbn", Type.GetType("System.String"))    '機器利用区分
                .Columns.Add("SetupFlg", Type.GetType("System.String"))      'セットアップフラグ
                .Columns.Add("CIKbnCD", Type.GetType("System.String"))       'CI種別CD
                .Columns.Add("RowNmb", Type.GetType("System.Int32"))         '行番号
                .Columns.Add("SetKikiID", Type.GetType("System.String"))     'セットID
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '作成テーブルをデータクラスにセット
            dataHBKZ0701.PropDtKiki = dtKiki

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
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControl(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701

                'スプレッドの設定
                If SetInitVwList(dataHBKZ0701) = False Then
                    Return False
                End If

                '呼び出し元にてステータスが1件のみ設定時はステータスを非活性にする
                If .PropStrCIStatusCD <> "" AndAlso .PropStrAryCIStatusCD.Length = 1 Then
                    .PropCmbCIStatus.Enabled = False
                End If

                ''処理モードに応じて全選択／全解除ボタン活性状態を切替る
                'If .PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
                '    '複数選択ありの場合、活性
                '    .PropBtnAllCheck.Enabled = True
                '    .PropBtnAllUnCheck.Enabled = True
                'Else
                '    '複数選択なしの場合、非活性
                '    .PropBtnAllCheck.Enabled = False
                '    .PropBtnAllUnCheck.Enabled = False
                'End If

                '処理モードに応じて全選択／全解除ボタン活性状態を切替る
                If .PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
                    '複数選択ありの場合、活性
                    .PropBtnAllCheck.Enabled = True
                    .PropBtnAllUnCheck.Enabled = True
                    .PropBtnAllCheck.Visible = True
                    .PropBtnAllUnCheck.Visible = True
                Else
                    '複数選択なしの場合、非活性
                    .PropBtnAllCheck.Enabled = False
                    .PropBtnAllUnCheck.Enabled = False
                    .PropBtnAllCheck.Visible = False
                    .PropBtnAllUnCheck.Visible = False
                End If

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 一覧スプレッド設定
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの初期設定を行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetInitVwList(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701.PropVwList

                '列のデータフィールドの設定
                .Columns(COL_SELECT).DataField = "Select"           '選択
                .Columns(COL_NUM).DataField = "Num"                 '番号
                .Columns(COL_KINDNM).DataField = "KindNM"           '種別名
                .Columns(COL_CLASS2).DataField = "Class2"           '分類２（メーカー）
                .Columns(COL_CINM).DataField = "CINM"               '名称（機種）
                .Columns(COL_KINDCD).DataField = "KindCD"           '種別コード
                .Columns(COL_CINMB).DataField = "CINmb"             'CI番号
                .Columns(COL_KIKIUSEKBN).DataField = "KikiUseKbn"   '機器利用区分
                .Columns(COL_SETUPFLG).DataField = "SetupFlg"       'セットアップフラグ
                .Columns(COL_CIKBNCD).DataField = "CIKbnCD"         'CI種別CD

                '隠し列非表示
                .Columns(COL_KINDCD).Visible = False                '種別コード
                .Columns(COL_CINMB).Visible = False                 'CI番号
                .Columns(COL_KIKIUSEKBN).Visible = False            '機器利用区分
                .Columns(COL_SETUPFLG).Visible = False              'セットアップフラグ
                .Columns(COL_CIKBNCD).Visible = False               'CI種別CD


                ''処理モードに応じてチェックボックス活性状態を切替える
                'If dataHBKZ0701.PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
                '    '複数選択ありの場合、チェックボックス活性
                '    .Columns(COL_SELECT).Locked = False
                'Else
                '    '複数選択なしの場合、チェックボックス非活性
                '    .Columns(COL_SELECT).Locked = True
                'End If


                '処理モードに応じてチェックボックス活性状態を切替える
                If dataHBKZ0701.PropStrMode = CommonDeclareHBKZ.SELECT_MODE_MULTI Then
                    '複数選択ありの場合、チェックボックス活性
                    .Columns(COL_SELECT).Locked = False
                    .Columns(COL_SELECT).Visible = True
                Else
                    '複数選択なしの場合、チェックボックス非活性
                    .Columns(COL_SELECT).Locked = True
                    .Columns(COL_SELECT).Visible = False
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
    ''' 一覧入力チェック処理
    ''' <paramref name="dataHBKZ0701">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの入力チェックを行う
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputVw(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ0701

                '1行も選択されていない場合、エラー
                If .PropIntCheckIndex.Length = 0 Then
                    puErrMsg = Z0701_E001
                    Return False
                End If

                '単一選択で複数行選択している場合、エラー
                If .PropStrMode = SELECT_MODE_SINGLE AndAlso .PropIntCheckIndex.Length > 1 Then
                    puErrMsg = Z0701_E002
                    Return False
                End If

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
