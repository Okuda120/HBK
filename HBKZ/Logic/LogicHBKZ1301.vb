Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 対象システム検索一覧画面Logicクラス
''' </summary>
''' <remarks>対象システム検索一覧索画面のロジックを定義する
''' <para>作成情報：2012/10/23 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKZ1301

    'インスタンス作成
    Private sqlHBKZ1301 As New SqlHBKZ1301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言========================
    '列番号
    Public Const COL_SELECT As Integer = 0              '選択
    Public Const COL_CLASS1 As Integer = 1              '分類1
    Public Const COL_CLASS2 As Integer = 2              '分類2
    Public Const COL_CINM As Integer = 3                '名称
    Public Const COL_CISTATUS As Integer = 4            'ステータス
    Public Const COL_CINMB As Integer = 5               'CI番号　　　※隠し項目

    ''' <summary>
    ''' フォームロード時メイン処理
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッド表示用データテーブル作成
            If CreateDataTableForVw(dataHBKZ1301) = False Then
                Return False
            End If

            'フォームコントロール設定
            If SetFormControl(dataHBKZ1301) = False Then
                Return False
            End If

            'コンボボックス用マスタデータ取得
            If GetMastaData(dataHBKZ1301) = False Then
                Return False
            End If

            '初期取得データ設定
            If SetInitDataToControl(dataHBKZ1301) = False Then
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
    ''' <param name="dataHBKZ1301">[IN/OUT]対象システム検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateDataTableForVw(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtTaisyouSystem As New DataTable   '結果一覧用データテーブル

        Try
            '結果一覧用テーブル作成
            With dtTaisyouSystem
                .Columns.Add("Select", Type.GetType("System.Boolean"))          '選択
                .Columns.Add("Class1", Type.GetType("System.String"))           '分類1
                .Columns.Add("Class2", Type.GetType("System.String"))           '分類2
                .Columns.Add("CINM", Type.GetType("System.String"))             '名称
                .Columns.Add("CIStateNM", Type.GetType("System.String"))        'ステータス
                .Columns.Add("CINmb", Type.GetType("System.String"))            'CI番号
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '作成テーブルをデータクラスにセット
            dataHBKZ1301.PropDtTaisyouSystem = dtTaisyouSystem

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
            dtTaisyouSystem.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームコントロール設定
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControl(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'スプレッドの設定
            If SetInitVwList(dataHBKZ1301) = False Then
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
    ''' 一覧スプレッド設定
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの初期設定を行う
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetInitVwList(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1301.PropVwList.Sheets(0)
                '列のデータフィールドの設定
                .Columns(COL_SELECT).DataField = "Select"           '選択
                .Columns(COL_CLASS1).DataField = "Class1"           '分類1
                .Columns(COL_CLASS2).DataField = "Class2"           '分類2
                .Columns(COL_CINM).DataField = "CINM"               '名称
                .Columns(COL_CISTATUS).DataField = "CIStateNM"      'ステータス
                .Columns(COL_CINMB).DataField = "CINmb"             'CI番号

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
    ''' マスタデータ取得
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>初期表示用のマスタデータを取得する
    ''' <para>作成情報：2012/10/23 s.yamguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKZ1301

                'CIステータスマスタ取得
                If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CI_TYPE_SYSTEM, .PropDtCIStatus) = False Then
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
    ''' <param name="dataHBKZ0701">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '取得データを基にコンボボックスリストをセット
            If SetCmbData(dataHBKZ1301) = False Then
                Return False
            End If

            '検索条件初期化
            If SetInitSearchCond(dataHBKZ1301) = False Then
                Return False
            End If

            '一覧スプレッドデータ設定
            If SetSheet(dataHBKZ1301) = False Then
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
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>コンボボックスにデータをセットする
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbData(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ1301

                'ステータスコンボボックスセット
                If commonLogic.SetCmbBox(.PropDtCIStatus, .PropCmbStatus, True, "", "") = False Then
                    Return False
                End If

                'フリーフラグ1
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg1) = False Then
                    Return False
                End If

                'フリーフラグ2
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg2) = False Then
                    Return False
                End If

                'フリーフラグ3
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg3) = False Then
                    Return False
                End If

                'フリーフラグ4
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg4) = False Then
                    Return False
                End If

                'フリーフラグ5
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlg5) = False Then
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
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>検索条件を初期化する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitSearchCond(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ1301

                'CIステータスコンボボックス
                .PropCmbStatus.SelectedIndex = 0
                '分類1
                .PropTxtClass1.Text = ""
                '分類2
                .PropTxtClass2.Text = ""
                '名称
                .PropTxtCINm.Text = ""

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
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>シートに情報をセットする
    ''' <para>作成情報：2012/10/23 s.yamguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetSheet(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKZ1301

                'シートにデータをセット
                .PropVwList.Sheets(0).DataSource = .PropDtTaisyouSystem

                '件数設定
                .PropLblCount.Text = .PropIntTaisyouSystemCount.ToString & "件"

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
    ''' 検索の件数取得メイン
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行い結果件数を取得する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetTaisyouSystemCountMain(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '件数取得
        If GetTaisyouSystemCount(dataHBKZ1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 検索の件数取得
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTaisyouSystemCount(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

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
            If sqlHBKZ1301.SetSelectCountTaisyouSystemSql(Adapter, Cn, dataHBKZ1301) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム検索件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            '取得データをデータクラスへ保存
            dataHBKZ1301.PropIntTaisyouSystemCount = Table.Rows(0)(0)

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
    ''' 検索メイン処理
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchMain(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索処理
        If Search(dataHBKZ1301) = False Then
            Return False
        End If

        '取得データ設定
        If SetSheet(dataHBKZ1301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    ''' <summary>
    ''' 検索処理
    ''' <param name="dataHBKZ1301">[IN/OUT]データクラス</param>
    ''' </summary>
    ''' <returns>Boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>フォームから取得した値をもとに検索を行う
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Search(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        'データ初期化
        dataHBKZ1301.PropDtTaisyouSystem.Clear()

        Try
            'コネクションを開く
            Cn.Open()

            '検索用SQLの作成・設定
            If sqlHBKZ1301.SetSelectTaisyouSystemSql(Adapter, Cn, dataHBKZ1301) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKZ1301.PropDtTaisyouSystem)

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
    ''' 選択ボタンクリック時入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1301">[IN/OUT]対象システム検索一覧画面データクラス</param>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>フォームの情報を初期化する
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckWhenBtnSelectClickMain(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '一覧入力チェック
            If CheckInputVw(dataHBKZ1301) = False Then
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
    ''' 一覧入力チェック処理
    ''' <paramref name="dataHBKZ1301">[IN/OUT]データクラス</paramref>
    ''' </summary>
    ''' <returns>Boolean エラーコード    True:正常  False:異常</returns>
    ''' <remarks>一覧スプレッドの入力チェックを行う
    ''' <para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputVw(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1301

                '1行も選択されていない場合、エラー
                If .PropIntCheckIndex.Length = 0 Then
                    puErrMsg = Z1301_E001
                    Return False
                End If

                '複数行選択している場合、エラー
                If .PropIntCheckIndex.Length > 1 Then
                    puErrMsg = Z1301_E002
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

End Class
