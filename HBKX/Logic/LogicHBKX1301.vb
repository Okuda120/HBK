Imports Common
Imports CommonHBK
Imports System.Windows.Forms
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 設置情報マスター一覧画面ロジッククラス
''' </summary>
''' <remarks>設置情報マスター一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/09/03 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX1301

    'インスタンス生成
    Private sqlHBKX1301 As New SqlHBKX1301

    'Public定数宣言==============================================
    '設置情報マスター一覧列番号
    Public Const SETBUSYO_CD As Integer = 0                 '設置部署ＣＤ
    Public Const SETKYOKU_NM As Integer = 1                 '局名
    Public Const SETBUSYO_NM As Integer = 2                 '部署名
    Public Const SETROOM As Integer = 3                     '番組/部屋名
    Public Const SETBUIL As Integer = 4                     '建物
    Public Const SETFLOOR As Integer = 5                    'フロア
    Public Const JTIFLG_DISP As Integer = 6                 '削除
    Public Const JTIFLG_HIDDEN As Integer = 7               '削除フラグ(隠し項目)

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設置情報マスター一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームオブジェクト設定処理
        If SetFormObj(DataHBKX1301) = False Then
            Return False
        End If

        'スプレッド用データテーブル作成
        If CreateDataTableForVw(DataHBKX1301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(DataHBKX1301) = False Then
            Return False
        End If

        '初期表示用データ設定
        If SetInitData(DataHBKX1301) = False Then
            Return False
        End If

        'スプレッド隠し項目設定処理
        If Setvisible(DataHBKX1301) = False Then
            Return False
        End If

        'スプレッド詳細設定
        If SetSpread(DataHBKX1301) = False Then
            Return False
        End If

        '出力結果背景色変更処理
        If ChangeColor(DataHBKX1301) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(DataHBKX1301) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'オブジェクトの活性非活性設定
            With DataHBKX1301.PropGrpLoginUser
                'グループコンボボックス非活性
                .cmbGroup.Enabled = False
                '変更ボタン非活性
                .btnChange.Enabled = False
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
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSetMaster As New DataTable   '設置情報マスター検索用データテーブル

        Try
            '設置情報マスター検索一覧用テーブル作成
            With dtSetMaster
                .Columns.Add("SetBusyoCD", Type.GetType("System.Int32"))           '設置部署CD
                .Columns.Add("SetKyokuNM", Type.GetType("System.String"))           '局名
                .Columns.Add("SetBusyoNM", Type.GetType("System.String"))           '部署名
                .Columns.Add("SetRoom", Type.GetType("System.String"))              '番組/部屋名
                .Columns.Add("SetBuil", Type.GetType("System.String"))              '建物
                .Columns.Add("SetFloor", Type.GetType("System.String"))             'フロア
                .Columns.Add("JtiFlgDisp", Type.GetType("System.String"))           '削除フラグ表示
                .Columns.Add("JtiFlg", Type.GetType("System.String"))               '削除フラグ(隠し項目)
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With DataHBKX1301
                .PropDtSearchResult = dtSetMaster
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
            dtSetMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示データを取得する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'スプレッド初期表示用データ取得
            If SpreadGetInitData(Adapter, Cn, DataHBKX1301) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SpreadGetInitData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'SQLの作成・設定
            If sqlHBKX1301.SetSelectSoftMasterDataSql(Adapter, Cn) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "設置機器マスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX1301.PropDtSearchResult)

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
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示設定を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '初期表示データをスプレッドに設定
            If SetInitDataSpread(DataHBKX1301) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをスプレッドに設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataSpread(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '設置情報マスター一覧
            With dataHBKX1301.PropVwSetInfoSearch.Sheets(0)
                .DataSource = dataHBKX1301.PropDtSearchResult
                .Columns(SETBUSYO_CD).DataField = "SetBusyoCD"
                .Columns(SETKYOKU_NM).DataField = "SetKyokuNM"
                .Columns(SETBUSYO_NM).DataField = "SetBusyoNM"
                .Columns(SETROOM).DataField = "SetRoom"
                .Columns(SETBUIL).DataField = "SetBuil"
                .Columns(SETFLOOR).DataField = "SetFloor"
                .Columns(JTIFLG_DISP).DataField = "JtiFlgDisp"
                .Columns(JTIFLG_HIDDEN).DataField = "JtiFlg"
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
        End Try

    End Function

    ''' <summary>
    ''' 隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内の隠し項目を設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Setvisible(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1301.PropVwSetInfoSearch.Sheets(0)
                '隠し項目の設定
                .Columns(JTIFLG_HIDDEN).Visible = False             '削除フラグ(隠し項目)
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
        End Try

    End Function

    ''' <summary>
    ''' スプレッド詳細設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド表示の詳細設定を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSpread(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'データ表示非表示設定処理
            If DataVisible(DataHBKX1301) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(DataHBKX1301) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' データ表示非表示設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された項目ごとにデータの表示非表示を設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DataVisible(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 0    '再設定する行番号

        Try

            With dataHBKX1301.PropVwSetInfoSearch.Sheets(0)

                '表示状態を初期化する
                For i = 0 To .RowCount - 1
                    If dataHBKX1301.PropChkDelDis.Checked = False And .Cells(i, JTIFLG_HIDDEN).Value = DATA_MUKO Then
                        'チェックがあり、削除データの場合は表示しない
                        .Rows(i).Visible = False
                    Else
                        .Rows(i).Visible = True
                    End If
                Next

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
        End Try

    End Function

    ''' <summary>
    ''' 行ヘッダ設定処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>行ヘッダを設定する
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRowHearder(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 0    '再設定する行番号

        Try
            '行ヘッダを再設定する
            With dataHBKX1301.PropVwSetInfoSearch.Sheets(0)

                For i = 0 To .RowCount - 1
                    '非表示でなければ行番号を割り振る
                    If .Rows(i).Visible = True Then
                        .RowHeader.Cells(i, 0).Value = intRowHeader + 1
                        intRowHeader += 1
                    End If
                Next

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
        End Try

    End Function

    ''' <summary>
    ''' 出力結果背景色変更処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの背景色をグレーにする
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1301.PropVwSetInfoSearch

                For i = 0 To .Sheets(0).RowCount - 1
                    If .Sheets(0).GetValue(i, JTIFLG_HIDDEN) = DATA_MUKO Then
                        '削除データ行はグレーに変更
                        .Sheets(0).Rows(i).BackColor = Color.Gray
                    End If
                Next

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索件数表示処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の表示を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX1301.PropVwSetInfoSearch
                '表示されている件数分カウントする
                For i = 0 To .Sheets(0).RowCount - 1
                    If .Sheets(0).Rows(i).Visible = True Then
                        intCount += 1
                    End If
                Next

                '検索件数をセット
                dataHBKX1301.PropLblKensu.Text = intCount & "件"

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
        End Try

    End Function

    ''' <summary>
    ''' 削除データ表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータの表示、非表示を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckMain(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド詳細設定
        If SetSpread(DataHBKX1301) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(DataHBKX1301) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソートボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'デフォルトソートを行う
            If DefaultSort(DataHBKX1301) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(DataHBKX1301) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSort(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKX1301.PropVwSetInfoSearch.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(SETBUSYO_CD, True) '設置情報マスター.設置部署CD

                '設置情報マスター.設置部署CDの昇順でソートする
                .SortRows(0, .RowCount, Si)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1
                    .Columns(i).ResetSortIndicator()
                Next

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
        End Try

    End Function

    ''' <summary>
    ''' 行ヘッダ再設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1301">[IN/OUT]設置情報マスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>列ヘッダがクリックされた場合に、行ヘッダの再設定を行う
    ''' <para>作成情報：2012/09/03 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetRowHeaderMain(ByRef dataHBKX1301 As DataHBKX1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '行ヘッダ設定処理
        If SetRowHearder(DataHBKX1301) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

End Class
