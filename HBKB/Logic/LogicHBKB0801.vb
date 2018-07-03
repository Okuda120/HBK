Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' レンタル及び部所有機器の期限切れ検索一覧画面ロジッククラス
''' </summary>
''' <remarks>レンタル及び部所有機器の期限切れ検索一覧画面のロジッククラス
''' <para>作成情報：2012/07/05 kawate
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKB0801

    'インスタンス生成
    Private sqlHBKB0801 As New SqlHBKB0801
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    '件数ラベル
    Public Const LABEL_CNT_FORMAT As String = "{0}名({1}件)"
    'スプレッド列番号
    Public Const COL_SELECT As Integer = 0              '選択チェックボックス
    Public Const COL_BUSYONM As Integer = 1             '部署名
    Public Const COL_USRID As Integer = 2               'ユーザーID
    Public Const COL_USRNM As Integer = 3               'ユーザー氏名
    Public Const COL_SHINAMONOSU As Integer = 4         '品物数
    Public Const COL_TARGETKIKI As Integer = 5          '対象機器
    Public Const COL_RENTALBUSYONM As Integer = 6       '貸出時部署名
    Public Const COL_TYPE As Integer = 7                'タイプ
    Public Const COL_LIMITDATE_FROM As Integer = 8      '開始日
    Public Const COL_LIMITDATE_TO As Integer = 9        '期限日
    Public Const COL_LASTINFODATE As Integer = 10       '最終お知らせ日
    Public Const COL_FUKUSURENTAL As Integer = 11       '複数人貸出
    Public Const COL_CINMB As Integer = 12              'CI番号　　　　　　　　　※非表示
    Public Const COL_KIKITYPE_CD As Integer = 13        '機器タイプCD　　　　　　※非表示
    Public Const COL_KIKI_KINDCD As Integer = 14        '対象機器種別CD　　　　　※非表示
    Public Const COL_KIKI_NUM As Integer = 15           '対象機器番号　　　　　　※非表示
    Public Const COL_USRCOMPANY As Integer = 16         '所属会社　　　　　　　　※非表示
    Public Const COL_USRNMKANA As Integer = 17          'ユーザー氏名カナ　　　　※非表示
    Public Const COL_USRCONTACT As Integer = 18         'ユーザー連絡先　　　　　※非表示
    Public Const COL_USRMAILADD As Integer = 20         'ユーザーメールアドレス　※非表示


    ''' <summary>
    ''' システムエラー事前対応メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0801) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面に初期データをセットする
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド用データテーブル作成処理
        If CreateDataTableForVw(dataHBKB0801) = False Then
            Return False
        End If

        'スプレッド初期化処理
        If SetInitVw(dataHBKB0801) = False Then
            Return False
        End If

        '初期データ取得処理
        If GetInitData(dataHBKB0801) = False Then
            Return False
        End If

        'コンボボックス作成処理
        If CreateCmb(dataHBKB0801) = False Then
            Return False
        End If

        '検索項目初期化処理
        If InitSearchCondition(dataHBKB0801) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' CI種別変更時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたCI種別に応じてフォームコントロールを設定する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function BeChangedCIKbnMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'タイプコンボボックス初期化
        If SetInitType(dataHBKB0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索条件初期表示メイン
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に入力された内容を初期状態に戻す
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitSearchConditionMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件初期化処理
        If InitSearchCondition(dataHBKB0801) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果件数取得処理メイン
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索を行い結果を表示する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetResultCntMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件保存
        If SaveSearchCond(dataHBKB0801) = False Then
            Return False
        End If

        '検索結果件数取得
        If GetResultCnt(dataHBKB0801) = False Then
            Return False
        End If

        '結果件数チェック
        If CheckResultCnt(dataHBKB0801) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果表示処理メイン
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索を行い結果を表示する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SearchDataMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '一覧クリア
        If ClearVw(dataHBKB0801) = False Then
            Return False
        End If

        '検索処理
        If SearchData(dataHBKB0801) = False Then
            Return False
        End If

        '取得データを表示
        If SetDataOnVw(dataHBKB0801) = False Then
            Return False
        End If

        'WHERE句クリア
        dataHBKB0801.PropStrWhereCmd = Nothing


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 一覧全選択メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧の全データに選択チェックをつける
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function AllSelectMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データクラスにパラメータをセット：全選択
        dataHBKB0801.PropBlnCheckedTo = True

        '一覧データ選択状態変更
        If ChangeAllDataSelectedStatus(dataHBKB0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 一覧全解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧の全データの選択チェックを解除する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function AllCancelMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データクラスにパラメータをセット：全解除
        dataHBKB0801.PropBlnCheckedTo = False

        '一覧データ選択状態変更
        If ChangeAllDataSelectedStatus(dataHBKB0801) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' インシデント登録入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデントテーブルに登録する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckINputValueMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索結果選択数のチェック
        If CheckExistsSelectedData(dataHBKB0801) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' インシデント登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデントテーブルに登録する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegIncMain(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'インシデントデータを登録
        If RegInc(dataHBKB0801) = False Then
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
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0801

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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド用のデータテーブルを作成する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable

        Try
            '結果一覧用データテーブル作成
            With dtCIInfo
                '列設定
                .Columns.Add("CINmb", Type.GetType("System.String"))            'CI番号
                .Columns.Add("Select", Type.GetType("System.Boolean"))          'チェックボックス選択有無
                .Columns.Add("EndUsrBusyoNM", Type.GetType("System.String"))    '部署名
                .Columns.Add("UsrID", Type.GetType("System.String"))            'ユーザーID
                .Columns.Add("EndUsrNM", Type.GetType("System.String"))         'ユーザー氏名
                .Columns.Add("DataCnt", Type.GetType("System.String"))          '品物数
                .Columns.Add("TargetKiki", Type.GetType("System.String"))       '対象機器
                .Columns.Add("UsrBusyoNM", Type.GetType("System.String"))       '貸出部署名
                .Columns.Add("SCKikiType", Type.GetType("System.String"))       'タイプ
                .Columns.Add("LimitDateFrom", Type.GetType("System.String"))    '開始日
                .Columns.Add("LimitDateTo", Type.GetType("System.String"))      '期限日
                .Columns.Add("LastInfoDT", Type.GetType("System.String"))       '最終お知らせ日
                .Columns.Add("ShareExists", Type.GetType("System.String"))      '複数人貸出
                .Columns.Add("TypeKbn", Type.GetType("System.String"))          '機器タイプCD
                .Columns.Add("KikiKindCD", Type.GetType("System.String"))       '対象機器種別CD
                .Columns.Add("KikiNum", Type.GetType("System.String"))          '対象機器番号
                .Columns.Add("EndUsrCompany", Type.GetType("System.String"))    'ユーザー所属会社
                .Columns.Add("EndUsrNMkana", Type.GetType("System.String"))     'ユーザー氏名カナ
                .Columns.Add("EndUsrContact", Type.GetType("System.String"))    'ユーザー連絡先
                .Columns.Add("EndUsrMailAdd", Type.GetType("System.String"))    'ユーザーメールアドレス
                '変更をコミット
                .AcceptChanges()
            End With

            '作成したテーブルをデータクラスにセット
            dataHBKB0801.PropDtCIInfo = dtCIInfo

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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの初期設定を行う
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitVw(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '結果一覧スプレッド設定
            With dataHBKB0801.PropVwCIInfo.Sheets(0)

                'データフィールド設定
                .Columns(COL_SELECT).DataField = "Select"                   '選択チェックボックス
                .Columns(COL_BUSYONM).DataField = "EndUsrBusyoNM"           '部署名
                .Columns(COL_USRID).DataField = "UsrID"                     'ユーザーID
                .Columns(COL_USRNM).DataField = "EndUsrNM"                  'ユーザー氏名
                .Columns(COL_SHINAMONOSU).DataField = "DataCnt"             '品物数
                .Columns(COL_TARGETKIKI).DataField = "TargetKiki"           '対象機器
                .Columns(COL_RENTALBUSYONM).DataField = "UsrBusyoNM"        '貸出部署名
                .Columns(COL_TYPE).DataField = "SCKikiType"                 'タイプ
                .Columns(COL_LIMITDATE_FROM).DataField = "LimitDateFrom"    '開始日
                .Columns(COL_LIMITDATE_TO).DataField = "LimitDateTo"        '期限日
                .Columns(COL_LASTINFODATE).DataField = "LastInfoDT"         '最終お知らせ日
                .Columns(COL_FUKUSURENTAL).DataField = "ShareExists"        '複数人貸出
                .Columns(COL_CINMB).DataField = "CINmb"                     'CI番号
                .Columns(COL_KIKITYPE_CD).DataField = "TypeKbn"             '機器タイプCD
                .Columns(COL_KIKI_KINDCD).DataField = "KikiKindCD"          '対象機器種別CD
                .Columns(COL_KIKI_NUM).DataField = "KikiNum"                '対象機器番号
                .Columns(COL_USRCOMPANY).DataField = "EndUsrCompany"        'ユーザー所属会社
                .Columns(COL_USRNMKANA).DataField = "EndUsrNMkana"          'ユーザー氏名カナ
                .Columns(COL_USRCONTACT).DataField = "EndUsrContact"        'ユーザー連絡先
                .Columns(COL_USRMAILADD).DataField = "EndUsrMailAdd"        'ユーザーメールアドレス

                '隠し列非表示
                .Columns(COL_CINMB).Visible = False                         'CI番号
                .Columns(COL_KIKITYPE_CD).Visible = False                   '機器タイプCD
                .Columns(COL_KIKI_KINDCD).Visible = False                   '対象機器種別CD
                .Columns(COL_KIKI_NUM).Visible = False                      '対象機器番号
                .Columns(COL_USRCOMPANY).Visible = False                    'ユーザー所属会社
                .Columns(COL_USRNMKANA).Visible = False                     'ユーザー氏名カナ
                .Columns(COL_USRCONTACT).Visible = False                    'ユーザー連絡先
                .Columns(COL_USRMAILADD).Visible = False                    'ユーザーメールアドレス

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
    ''' 初期データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]期限切れ検索検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim strCIKbnCD As String

        Try
            'コネクションを開く
            Cn.Open()

            'CI種別マスタデータ取得（※サポセン機器と部所有マスタのレコードのみ取得）
            strCIKbnCD = CI_TYPE_SUPORT & "," & CI_TYPE_KIKI
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, strCIKbnCD, dataHBKB0801.PropDtCIKind) = False Then
                Return False
            End If

            'サポセン機器タイプマスタデータ取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKB0801.PropDtSapKikiType) = False Then
            '    Return False
            'End If
            If commonLogicHBK.GetSapKikiTypeMastaData(Adapter, Cn, dataHBKB0801.PropDtSapKikiType, 0) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    '''コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                'CI種別コンポボックス作成　※空行なし
                If commonLogic.SetCmbBox(.PropDtCIKind, .PropCmbCIKbn, False) = False Then
                    Return False
                End If

                'タイプコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtSapKikiType, .PropCmbType, True, "", "") = False Then
                    Return False
                End If

                '期限コンボボックス作成
                If commonLogic.SetCmbBox(strCmbLimit, .PropCmbLimit) = False Then
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
    ''' 検索条件初期化処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件入力フォームに入力された内容を初期化する
    ''' <para>作成情報：2012/07/05 kawate
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitSearchCondition(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable               '検索結果用データテーブル

        Try

            '各検索用のコントロールを初期化する
            With dataHBKB0801

                'CI種別
                .PropCmbCIKbn.SelectedValue = .PropStrCIKbnCd

                'タイプ
                If SetInitType(dataHBKB0801) = False Then
                    Return False
                End If

                '期限ラジオボタン
                .PropRdoLimit.Checked = True
                .PropCmbLimit.SelectedValue = LIMIT_THISMONTH_ONLY

                'ユーザーIDラジオボタン
                .PropRdoUsrID.Checked = False
                .PropTxtUsrID.Text = ""

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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' タイプクリア処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたCI種別に応じてタイプコンボボックスを初期化する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitType(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable               '検索結果用データテーブル

        Try

            '各検索用のコントロールを初期化する
            With dataHBKB0801

                If .PropCmbCIKbn.SelectedValue = CI_TYPE_SUPORT Then    'サポセン機器の場合

                    'Normalを初期設定・選択可能
                    .PropCmbType.SelectedValue = SAP_TYPE_NORMAL
                    .PropCmbType.Enabled = True

                    '空行削除したデータソースをセット
                    If .PropCmbType.FindStringExact("") = 0 Then
                        .PropDtSapKikiType.Rows.RemoveAt(0)
                        .PropCmbType.DataSource = .PropDtSapKikiType
                    End If

                ElseIf .PropCmbCIKbn.SelectedValue = CI_TYPE_KIKI Then  '部所有機器の場合

                    '空行追加したデータソースをセット
                    If .PropCmbType.FindStringExact("") <> 0 Then
                        Dim rowBlank As DataRow = .PropDtSapKikiType.NewRow
                        rowBlank.Item("ID") = ""
                        rowBlank.Item("Text") = ""
                        .PropDtSapKikiType.Rows.InsertAt(rowBlank, 0)
                        .PropCmbType.DataSource = .PropDtSapKikiType
                    End If
                    
                    'ブランク・選択不可
                    .PropCmbType.SelectedValue = ""
                    .PropCmbType.Enabled = False

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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 一覧クリア処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧及び件数のクリアを行う
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearVw(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0801

                '一覧データをクリアし、一覧にセット
                .PropDtCIInfo.Clear()
                .PropVwCIInfo.Sheets(0).DataSource = .PropDtCIInfo

                '件数をセット
                dataHBKB0801.PropIntResultUsrCnt = 0
                If SetDataCntToLabel(dataHBKB0801) = False Then
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
    ''' 件数ラベル設定処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>件数ラベルに取得件数を設定する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataCntToLabel(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intUsrCnt As Integer    'ユーザー数（名）
        Dim intRowCnt As Integer    '行数（件）


        Try

            With dataHBKB0801

                '一覧ユーザー件数取得
                intUsrCnt = dataHBKB0801.PropIntResultUsrCnt

                '一覧行数取得
                intRowCnt = .PropVwCIInfo.Sheets(0).RowCount


                '件数をセット
                .PropLblCount.Text = String.Format(LABEL_CNT_FORMAT, intUsrCnt.ToString(), intRowCnt.ToString())

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
    ''' データ検索結果件数取得処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたCI種別に応じた検索結果件数の取得を行う
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCnt(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                '選択されたCI種別に応じて検索処理を行う
                If .PropCmbCIKbn.SelectedValue = CI_TYPE_SUPORT Then        'サポセン

                    'CI共通情報／サポセン機器情報の検索結果件数を取得する
                    If GetResultCntForSap(dataHBKB0801) = False Then
                        Return False
                    End If


                ElseIf .PropCmbCIKbn.SelectedValue = CI_TYPE_KIKI Then      '部所有機器

                    'CI共通情報／部所有機器情報の検索結果件数を取得する
                    If GetResultCntForBuy(dataHBKB0801) = False Then
                        Return False
                    End If

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
    ''' データ検索処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたCI種別に応じて検索処理を行う
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SearchData(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                '選択されたCI種別に応じて検索処理を行う
                If .PropCmbCIKbn.SelectedValue = CI_TYPE_SUPORT Then        'サポセン

                    'CI共通情報／サポセン機器情報を検索し、取得する
                    If SearchDataForSap(dataHBKB0801) = False Then
                        Return False
                    End If


                ElseIf .PropCmbCIKbn.SelectedValue = CI_TYPE_KIKI Then      '部所有機器

                    'CI共通情報／部所有機器情報を検索し、取得する
                    If SearchDataForBuy(dataHBKB0801) = False Then
                        Return False
                    End If

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
    ''' 【サポセン】データ検索結果件数取得処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に基づいたCI共通情報／サポセン機器情報データの検索結果件数を取得する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCntForSap(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            '取得用SQLの作成・設定
            If sqlHBKB0801.SetSelectMainDataCntSqlForSap(Adapter, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報／サポセン機器情報検索結果件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '結果件数を取得
            If dtResult.Rows.Count > 0 Then
                dataHBKB0801.PropIntResultCnt = dtResult.Rows(0).Item(0)
            Else
                dataHBKB0801.PropIntResultCnt = 0
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            dtResult.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索条件保存処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件をデータクラスに保存する
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SaveSearchCond(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                .PropStrCIKbnCD_Search = .PropCmbCIKbn.SelectedValue    'CI種別コード
                .PropBlnKigenChecked_Search = .PropRdoLimit.Checked     '期限ラジオボタン選択状態
                .PropStrKigenCD_Search = .PropCmbLimit.SelectedValue    '期限コード
                .PropStrKigenText_Search = .PropCmbLimit.Text           '期限テキスト

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
    ''' 【部所有機器】データ検索結果件数取得処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に基づいたCI共通情報／部所有機器情報データの検索結果件数を取得する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCntForBuy(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            '取得用SQLの作成・設定
            If sqlHBKB0801.SetSelectMainDataCntSqlForBuy(Adapter, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報／部所有機器情報検索結果件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '結果件数を取得
            If dtResult.Rows.Count > 0 Then
                dataHBKB0801.PropIntResultCnt = dtResult.Rows(0).Item(0)
            Else
                dataHBKB0801.PropIntResultCnt = 0
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            dtResult.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' データ検索結果件数チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>結果件数が0件の場合エラーメッセージを返す
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CheckResultCnt(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                '結果件数が0件の場合、一覧をクリアしメッセージを返す
                If .PropIntResultCnt = 0 Then
                    '件数0件の場合エラーメッセージに空白をセット
                    puErrMsg = ""
                    If ClearVw(dataHBKB0801) = False Then
                        Return False
                    End If
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
    ''' 【サポセン】データ検索処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に基づいてCI共通情報／サポセン機器情報データを検索し、取得する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SearchDataForSap(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '取得用SQLの作成・設定
            If sqlHBKB0801.SetSelectMainDataSqlForSap(Adapter, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報／サポセン機器情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0801.PropDtCIInfo)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            
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
    ''' 【部所有機器】データ検索処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件に基づいてCI共通情報／部所有機器情報データを検索し、取得する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SearchDataForBuy(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '取得用SQLの作成・設定
            If sqlHBKB0801.SetSelectMainDataSqlForBuy(Adapter, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報／部所有機器情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKB0801.PropDtCIInfo)

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
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
    ''' 取得データ設定処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得データおよび件数を画面にセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetDataOnVw(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0801

                '検索結果スプレッドに取得データをセット
                .PropVwCIInfo.Sheets(0).DataSource = .PropDtCIInfo

                'データの結合設定を行う
                If AddSpanData(dataHBKB0801) = False Then
                    Return False
                End If

                '件数ラベル設定
                If SetDataCntToLabel(dataHBKB0801) = False Then
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
    ''' 一覧セル結合処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ユーザーIDごとに、チェックボックス、部署名、ユーザーID、ユーザー氏名、品物数を結合する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function AddSpanData(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strBefUsrID As String               '前行のユーザーID
        Dim strCurUsrID As String               'カレント行のユーザーID
        Dim intStartSpanUsrID As Integer        'ユーザーIDの結合スタート行
        Dim intCountSpanUsrID As Integer        'ユーザーIDの結合行数
        Dim intUsrCnt As Integer                'ユーザー数

        Try
            With dataHBKB0801.PropVwCIInfo.Sheets(0)

                '変数初期化
                strBefUsrID = .GetValue(0, COL_USRID)
                strCurUsrID = ""
                intStartSpanUsrID = 0
                intCountSpanUsrID = 0
                intUsrCnt = 1

                '一覧全行チェック
                For i As Integer = 0 To .RowCount

                    '最終行以前の場合
                    If i < .RowCount Then

                        'カレント行のユーザーIDを取得
                        strCurUsrID = .GetValue(i, COL_USRID)

                        'カレント行のユーザーIDが前行と等しいかチェック
                        If strCurUsrID = strBefUsrID Then

                            '等しい場合はユーザーID結合行数をカウントアップ
                            intCountSpanUsrID += 1

                        Else

                            'カレント行のユーザーIDが前行と異なり、結合行数が1行以上の場合、
                            'チェックボックス、部署名、ユーザーID、ユーザー氏名、品物数のセル結合を行う
                            If intCountSpanUsrID > 0 Then

                                .AddSpanCell(intStartSpanUsrID, COL_SELECT, intCountSpanUsrID, 1)           'チェックボックス
                                .AddSpanCell(intStartSpanUsrID, COL_BUSYONM, intCountSpanUsrID, 1)          '部署名
                                .AddSpanCell(intStartSpanUsrID, COL_USRID, intCountSpanUsrID, 1)            'ユーザーID
                                .AddSpanCell(intStartSpanUsrID, COL_USRNM, intCountSpanUsrID, 1)            'ユーザー氏名
                                .AddSpanCell(intStartSpanUsrID, COL_SHINAMONOSU, intCountSpanUsrID, 1)      '品物数

                                '結合スタート行、結合行数初期化
                                intStartSpanUsrID = i
                                intCountSpanUsrID = 1

                            End If

                            'ユーザー数カウントアップ
                            intUsrCnt += 1

                        End If

                        '前行のユーザーIDをカレント行の値で更新
                        strBefUsrID = strCurUsrID

                    Else

                        '最終行まで処理した後、結合行数が1行以上の場合、
                        'チェックボックス、部署名、ユーザーID、ユーザー氏名、品物数のセル結合を行う
                        If intCountSpanUsrID > 0 Then

                            .AddSpanCell(intStartSpanUsrID, COL_SELECT, intCountSpanUsrID, 1)           'チェックボックス
                            .AddSpanCell(intStartSpanUsrID, COL_BUSYONM, intCountSpanUsrID, 1)          '部署名
                            .AddSpanCell(intStartSpanUsrID, COL_USRID, intCountSpanUsrID, 1)            'ユーザーID
                            .AddSpanCell(intStartSpanUsrID, COL_USRNM, intCountSpanUsrID, 1)            'ユーザー氏名
                            .AddSpanCell(intStartSpanUsrID, COL_SHINAMONOSU, intCountSpanUsrID, 1)      '品物数

                        End If

                    End If

                Next

                'ユーザー数をデータクラスにセット
                dataHBKB0801.PropIntResultUsrCnt = intUsrCnt

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
    ''' 一覧データ全選択／解除処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧の全データの選択チェックボックスのチェック状態をパラメータに応じて変更する
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function ChangeAllDataSelectedStatus(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnCheckedTo As Boolean = dataHBKB0801.PropBlnCheckedTo         '変更後チェック状態

        Try
            With dataHBKB0801.PropVwCIInfo.Sheets(0)

                'データが1件以上ある場合のみ処理
                If .RowCount > 0 Then

                    '一覧データ件数分繰り返し、チェック状態を変更する
                    For i As Integer = 0 To .RowCount - 1
                        .SetValue(i, COL_SELECT, blnCheckedTo)
                    Next

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
    ''' 検索結果選択数チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>一覧データが1件以上選択されているかチェックし、否であればエラーを返す
    ''' <para>作成情報：2012/07/20 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CheckExistsSelectedData(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnExistsCheck As Boolean = False   '選択チェック有無フラグ

        Try
            With dataHBKB0801.PropVwCIInfo.Sheets(0)

                'データが1件以上ある場合のみ処理
                If .RowCount > 0 Then

                    '一覧データ件数分繰り返し、選択状態をチェックする
                    For i As Integer = 0 To .RowCount - 1
                        '選択されている場合はフラグをONにして処理を抜ける
                        If .GetValue(i, COL_SELECT) = True Then
                            blnExistsCheck = True
                            Exit For
                        End If
                    Next

                End If

                '選択データがない場合はエラーを返す
                If blnExistsCheck = False Then
                    puErrMsg = B0801_E001
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
    ''' インシデント登録処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデントテーブルに登録する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function RegInc(ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim dtCIInfo As New DataTable             '一覧データソース
        Dim strCurUsrID As String = ""            '今回ユーザーID
        Dim strBefUsrID As String = ""            '前回ユーザーID
        Dim strCurSelected As Boolean = False     '今回選択状態
        Dim strBefSelected As Boolean = False     '前回選択状態

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKB0801

                ''ログNoに初期値をセット
                '.PropIntLogNo = 1

                '一覧のデータソースをデータテーブルに変換
                dtCIInfo = DirectCast(.PropVwCIInfo.Sheets(0).DataSource, DataTable)

                '一覧件分繰り返し
                For i As Integer = 0 To dtCIInfo.Rows.Count - 1

                    '今回ユーザーIDを取得
                    strCurUsrID = dtCIInfo.Rows(i).Item("UsrID").ToString()
                    '今回の選択状態を取得（ユーザーIDが同じ場合は前回の選択状態をセット）
                    If strBefUsrID = strCurUsrID Then
                        strCurSelected = strBefSelected
                        '改行フラグにFalseをセット
                        .PropBlnCheckRowChange = False
                        ''インシデント機器情報ログ用ログNoカウントアップ
                        '.PropIntLogNo += 1
                    Else
                        strCurSelected = dtCIInfo.Rows(i).Item("Select")
                        '改行フラグにTrueをセット
                        .PropBlnCheckRowChange = True
                    End If

                    '選択されている場合、登録を実行
                    If strCurSelected = True Then
                        .PropRowReg = dtCIInfo.Rows(i)
                        If DoRegInc(Adapter, Cn, dataHBKB0801) = False Then
                            'ロールバック
                            If Tsx IsNot Nothing Then
                                Tsx.Rollback()
                            End If
                            Return False
                        End If
                    End If

                    '前回ユーザーID、選択状態を今回ユーザーID、選択状態で更新
                    strBefUsrID = strCurUsrID
                    strBefSelected = strCurSelected

                Next

            End With

            'コミット
            Tsx.Commit()

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
            dtCIInfo.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function
    '+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++

    ''' <summary>
    ''' インシデント登録実行処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN/OUT]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデントテーブル登録処理を実行する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function DoRegInc(ByVal Adapter As NpgsqlDataAdapter, _
                              ByVal Cn As NpgsqlConnection, _
                              ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '1行目かどうか判定（複数行にまたぐ場合1件目のみインシデントを登録）
            If dataHBKB0801.PropBlnCheckRowChange = True Then

                '新規インシデント番号、システム日付取得
                If SelectNewIncNmbAndSysDate(Adapter, Cn, dataHBKB0801) = False Then
                    Return False
                End If

                'インシデント共通情報新規登録
                If InsertIncInfo(Cn, dataHBKB0801) = False Then
                    Return False
                End If

                'インシデント担当履歴情報新規登録
                If InsertIncTantoRireki(Cn, dataHBKB0801) = False Then
                    Return False
                End If

                'インシデント対応関係新規登録
                If InsertIncKankei(Cn, dataHBKB0801) = False Then
                    Return False
                End If

            End If

            'インシデント機器情報新規登録
            If InsertIncKiki(Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ情報新規登録
            If InsertIncLog(Adapter, Cn, dataHBKB0801) = False Then
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
        End Try

    End Function

    ''' <summary>
    ''' 新規インシデント番号、サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewIncNmbAndSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As New DataTable         '取得データ格納用テーブル

        Try

            'SQLを作成
            If sqlHBKB0801.SetSelectNewIncNmbAndSysDateSql(Adapter, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規インシデント番号、サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに新規インシデント番号、サーバー日付をセット
            If dtResult.Rows.Count > 0 Then
                dataHBKB0801.PropIntIncNmb = dtResult.Rows(0).Item("IncNmb")
                dataHBKB0801.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")
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
    ''' インシデント共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデント共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncInfo(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncInfoSql(Cmd, Cn, dataHBKB0801) = False Then
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
    ''' インシデント担当履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデント担当履歴情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/09/10 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncTantoRireki(ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント担当履歴情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncTantoRirekiSql(Cmd, Cn, dataHBKB0801) = False Then
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
    ''' インシデント対応関係新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデント対応関係テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankei(ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント対応関係新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncKankeiSql(Cmd, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント対応関係新規登録", Nothing, Cmd)

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
    ''' インシデント機器情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータをインシデント対応関係テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKiki(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント機器情報新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncKikiSql(Cmd, Cn, dataHBKB0801) = False Then
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
    ''' ログ情報新規登録処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録のログ情報を各ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/06/19 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncLog(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '1行目かどうか判定（複数行にまたぐ場合1件目のみインシデントを登録）
            If dataHBKB0801.PropBlnCheckRowChange = True Then

                '新規ログ番号取得
                If GetNewLogNo(dataHBKB0801) = False Then
                    Return False
                End If

                'インシデント共通情報ログテーブル登録
                If InsertIncInfoLog(Cn, dataHBKB0801) = False Then
                    Return False
                End If

                'インシデント対応関係ログテーブル登録
                If InsertIncKankeiLog(Cn, dataHBKB0801) = False Then
                    Return False
                End If

            End If
            

            'インシデント対応機器ログテーブル登録
            If InsertIncKikiLog(Cn, dataHBKB0801) = False Then
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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 新規ログ番号取得処理
    ''' </summary>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規にログ番号を採番して取得する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewLogNo(ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '新規ログ番号をデータクラスに設定
            dataHBKB0801.PropIntLogNo = 1       '1固定

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
    ''' インシデント共通情報ログ新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータのログをインシデント共通情報ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncInfoLog(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント共通情報ログ新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncInfoLogSql(Cmd, Cn, dataHBKB0801) = False Then
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
    ''' インシデント対応関係ログ新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータのログをインシデント対応関係ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKankeiLog(ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント対応関係ログ新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncKankeiLogSql(Cmd, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント対応関係ログ新規登録", Nothing, Cmd)

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
    ''' インシデント対応機器ログ新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータのログをインシデント対応機器ログテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertIncKikiLog(ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデント対応機器ログ新規登録（INSERT）用SQLを作成
            If sqlHBKB0801.SetInsertIncKikiLogSql(Cmd, Cn, dataHBKB0801) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデント対応機器ログ新規登録", Nothing, Cmd)

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
