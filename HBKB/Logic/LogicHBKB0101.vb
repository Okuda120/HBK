Imports Common
Imports System.Web
Imports System.IO
Imports CommonHBK
Imports System.Text
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 共通検索一覧画面Logicクラス
''' </summary>
''' <remarks>共通検索一覧画面のロジックを定義する
''' <para>作成情報：2012/05/31 kuga
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKB0101

    'インスタンス作成
    Private sqlHBKB0101 As New SqlHBKB0101          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    '定数宣言
    '文書用スプレッド：列番号
    Public Const COL_DOC_BTN_OPEN As Integer = 0            '開くボタン
    Public Const COL_DOC_KIND As Integer = 1                '種別
    Public Const COL_DOC_NUM As Integer = 2                 '番号
    Public Const COL_DOC_CLASS1 As Integer = 3              '分類１
    Public Const COL_DOC_CLASS2 As Integer = 4              '分類２
    Public Const COL_DOC_CINM As Integer = 5                '名称
    Public Const COL_DOC_STATUS As Integer = 6              'ステータス
    Public Const COL_DOC_CINAIYO As Integer = 7             '説明
    Public Const COL_DOC_LASTUPDT As Integer = 8            '最終更新日時
    Public Const COL_DOC_LASTUPUSR As Integer = 9           '最終更新者
    Public Const COL_DOC_CIOWNER As Integer = 10            'CIオーナー
    Public Const COL_DOC_SHARETEAMNM As Integer = 11        '文書配布先
    Public Const COL_DOC_CINMB As Integer = 12              'CI番号
    Public Const COL_DOC_EXISTSFILE As Integer = 13         'アップロードファイル有無
    Public Const COL_DOC_FILEPATH As Integer = 14           'アップロードファイルパス
    Public Const COL_DOC_CIKBNCD As Integer = 15            'CI種別コード
    'システム／サポセン／部所有機器用スプレッド：列番号
    Public Const COL_OTHER_KIND As Integer = 0              '種別
    Public Const COL_OTHER_NUM As Integer = 1               '番号
    Public Const COL_OTHER_CLASS1 As Integer = 2            '分類１
    Public Const COL_OTHER_CLASS2 As Integer = 3            '分類２
    Public Const COL_OTHER_CINM As Integer = 4              '名称
    Public Const COL_OTHER_STATUS As Integer = 5            'ステータス
    Public Const COL_OTHER_CINAIYO As Integer = 6           '説明
    Public Const COL_OTHER_LASTUPDT As Integer = 7          '最終更新日時
    Public Const COL_OTHER_LASTUPUSR As Integer = 8         '最終更新者
    Public Const COL_OTHER_CIOWNER As Integer = 9           'CIオーナー
    Public Const COL_OTHER_CINMB As Integer = 10            'CI番号
    Public Const COL_OTHER_KINDSORT As Integer = 11         '種別マスタ並び順
    Public Const COL_OTHER_CIKBNCD As Integer = 12          'CI種別コード

    '開くボタン名
    Public Const BTN_OPEN_TITLE As String = "開く"

    'CI種別コードリスト（マスタデータ取得に使用）
    Public Const CIKBNCD_LIST As String = CI_TYPE_SYSTEM & "," & CI_TYPE_DOC & "," & CI_TYPE_SUPORT & "," & CI_TYPE_KIKI



    ''' <summary>
    ''' フォームロード時のメイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '初期表示データ取得処理
        If GetInitData(dataHBKB0101) = False Then
            Return False
        End If

        'フォームオブジェクトの初期化処理
        If InitFormObject(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果件数取得メイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索結果件数を取得する
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCountMain(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件保存処理
        If SaveSearhCondition(dataHBKB0101) = False Then
            Return False
        End If

        '検索件数取得
        If GetCount(dataHBKB0101) = False Then
            Return False
        End If

        '********************************************************
        ''EXCEL出力ボタン活性化
        'dataHBKB0101.PropBtnOutput.Enabled = True
        '********************************************************

        '件数チェック
        If CheckResultDataCnt(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' リスト変更時時のメイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormList(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '種別の初期表示処理
        If MovePointKind(dataHBKB0101) = False Then
            Return False
        End If

        'ステータスの初期表示処理
        If MovePointStatus(dataHBKB0101) = False Then
            Return False
        End If

        'フォームオブジェクトの初期化処理
        If InitFormObjectList(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ファイルオープンメイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>選択データのアップロードファイルを開く
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OpenFileMain(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルオープン
        If OpenFile(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 初期表示データ取得処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示データを取得する
    ''' <para>作成情報：2012/07/21 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetInitData(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'CI種別マスタデータの取得
            If commonLogicHBK.GetCIKindMastaData(Adapter, Cn, CIKBNCD_LIST, dataHBKB0101.PropDtCiClass) = False Then
                Return False
            End If

            '種別マスタデータの取得
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
            'If commonLogicHBK.GetKindMastaData(Adapter, Cn, CIKBNCD_LIST, dataHBKB0101.PropDtKindAll) = False Then
            '    Return False
            'End If
            '検索画面はダミー用
            If commonLogicHBK.GetKindMastaData(Adapter, Cn, CIKBNCD_LIST, dataHBKB0101.PropDtKindAll, 0) = False Then
                Return False
            End If
            '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

            'CIステータスマスタデータの取得
            If commonLogicHBK.GetCIStatusMastaData(Adapter, Cn, CIKBNCD_LIST, dataHBKB0101.PropDtStatusAll) = False Then
                Return False
            End If

            'CIオーナーの取得
            If GetCIOwnerData(Adapter, Cn, dataHBKB0101) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
    ''' CIオーナーコンボボックス制御処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0101">[IN]共通検索一覧画面Dataクラス</param>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>CIオーナーコンボボックス表示データを取得する
    ''' <para>作成情報：2012/06/01 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetCIOwnerData(ByVal Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Table As New DataTable()

        Try
            'CIオーナーの取得
            If sqlHBKB0101.SelectCiOwner(Adapter, Cn, dataHBKB0101) = False Then
                Return False
            End If

            '開始ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CIオーナーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            '取得データをデータクラスへ保存
            dataHBKB0101.PropDtCiOwner = Table

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 種別コンボボックス制御処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>CI種別に紐づく種別マスタデータを取得し、コンボボックスにセットする
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function MovePointKind(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCIKbnCD As String = dataHBKB0101.PropLstCiClassCD.SelectedValue  'CI種別コード
        Dim dtKind As New DataTable

        Try
            With dataHBKB0101

                'CI種別に紐づく種別を取得
                Dim rcKind = From row In .PropDtKindAll _
                             Where row.Item("CIKbnCD") = strCIKbnCD
                             Select row
                             Order By row.Item("Sort")

                'データテーブルに変換
                dtKind = .PropDtKindAll.Clone
                For Each row In rcKind
                    dtKind.ImportRow(row)
                Next

                'データクラスにセット
                .PropDtKind = dtKind

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtKind.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ステータスコンボボックス制御処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>CI種別に紐づくCIステータスマスタデータを取得し、コンボボックスにセットする
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function MovePointStatus(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

         '変数宣言
        Dim strCIKbnCD As String = dataHBKB0101.PropLstCiClassCD.SelectedValue  'CI種別コード
        Dim dtStatus As New DataTable

        Try
            With dataHBKB0101

                'CI種別に紐づくステータスを取得
                Dim rcStatus = From row In .PropDtStatusAll _
                               Where row.Item("CIKbnCD") = strCIKbnCD
                               Select row
                               Order By row.Item("Sort")

                'データテーブルに変換
                dtStatus = .PropDtStatusAll.Clone
                For Each row In rcStatus
                    dtStatus.ImportRow(row)
                Next

                'データクラスにセット
                .PropDtStatus = dtStatus

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            dtStatus.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームオブジェクトの初期化処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormObject(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0101

                'CIオーナーコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtCiOwner, .PropCmbCiOwnerCD, True, "", "") = False Then
                    Return False
                End If

                'フリーフラグコンボボックス１～５作成
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlag1CD) = False Then
                    Return False
                End If
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlag2CD) = False Then
                    Return False
                End If
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlag3CD) = False Then
                    Return False
                End If
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlag4CD) = False Then
                    Return False
                End If
                If commonLogic.SetCmbBox(FreeFlg, .PropCmbFreeFlag5CD) = False Then
                    Return False
                End If

                'CI種別リスト作成
                .PropLstCiClassCD.ValueMember = "CIKbnCD"
                .PropLstCiClassCD.DisplayMember = "CIKbnNM"
                .PropLstCiClassCD.DataSource = dataHBKB0101.PropDtCiClass
                .PropLstCiClassCD.SelectedValue = dataHBKB0101.PropStrPlmCIKbnCD

                '種別コンボボックス設定
                If MovePointKind(dataHBKB0101) = False Then
                    Return False
                End If
                If SetCmbKind(dataHBKB0101) = False Then
                    Return False
                End If

                'ステータスコンボボックス設定
                If MovePointStatus(dataHBKB0101) = False Then
                    Return False
                End If
                If SetCmbStatus(dataHBKB0101) = False Then
                    Return False
                End If

                '最終更新日時に空白を設定
                dataHBKB0101.PropDtpStartDT.txtDate.Text = ""
                dataHBKB0101.PropDtpEndDT.txtDate.Text = ""

                'EXCEL出力ボタンは非活性
                .PropBtnOutput.Enabled = False

                '検索結果spreadの設定
                If SetInitVw(dataHBKB0101) = False Then
                    Return False
                End If

                'CI種別ごとのコントロール設定
                If SetInitControlPerCIKbn(dataHBKB0101) = False Then
                    Return False
                End If


            End With



            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' 種別コンボボックス設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>種別コンボボックスの初期設定を行う
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbKind(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                '選択されたCI種別に応じてコンボボックスを作成・設定する
                Select Case .PropLstCiClassCD.SelectedValue

                    Case CI_TYPE_SYSTEM                     'システム

                        'コンボボックス作成　※空白行なし
                        If commonLogic.SetCmbBox(.PropDtKind, .PropCmbClassCD, False) = False Then
                            Return False
                        End If

                        '非活性
                        .PropCmbClassCD.Enabled = False

                    Case CI_TYPE_DOC                        '文書

                        'コンボボックス作成　※空白行なし
                        If commonLogic.SetCmbBox(.PropDtKind, .PropCmbClassCD, False) = False Then
                            Return False
                        End If

                        '非活性
                        .PropCmbClassCD.Enabled = False

                    Case CI_TYPE_SUPORT                     'サポセン

                        'コンボボックス作成　※空白行あり
                        If commonLogic.SetCmbBox(.PropDtKind, .PropCmbClassCD, True, "", "") = False Then
                            Return False
                        End If

                        '初期値空白
                        .PropCmbClassCD.SelectedValue = ""
                        '活性
                        .PropCmbClassCD.Enabled = True

                    Case CI_TYPE_KIKI                       '部所有機器

                        'コンボボックス作成　※空白行なし
                        If commonLogic.SetCmbBox(.PropDtKind, .PropCmbClassCD, False) = False Then
                            Return False
                        End If

                        '非活性
                        .PropCmbClassCD.Enabled = False

                End Select

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' ステータスコンボボックス設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>ステータスコンボボックスの初期設定を行う
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCmbStatus(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                'コンボボックス作成　※空白行あり
                If commonLogic.SetCmbBox(.PropDtStatus, .PropCmbStatusCD, True, "", "") = False Then
                    Return False
                End If

                '初期値：空白
                .PropCmbStatusCD.SelectedValue = ""

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' スプレッド初期設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>スプレッドの初期設定を行う
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitVw(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '文書用スプレッド
            With dataHBKB0101.PropVwDoc.Sheets(0)

                'データフィールド設定
                .Columns(COL_DOC_KIND).DataField = "KindNM"                 '種別
                .Columns(COL_DOC_NUM).DataField = "Num"                     '番号
                .Columns(COL_DOC_CLASS1).DataField = "Class1"               '分類１
                .Columns(COL_DOC_CLASS2).DataField = "Class2"               '分類２
                .Columns(COL_DOC_CINM).DataField = "CINM"                   '名称
                .Columns(COL_DOC_STATUS).DataField = "CIStateNM"            'ステータス
                .Columns(COL_DOC_CINAIYO).DataField = "CINaiyo"             '説明
                .Columns(COL_DOC_LASTUPDT).DataField = "UpdateDT"           '最終更新日時
                .Columns(COL_DOC_LASTUPUSR).DataField = "HBKUsrNM"          '最終更新者
                .Columns(COL_DOC_CIOWNER).DataField = "GroupNM"             'CIオーナー
                .Columns(COL_DOC_SHARETEAMNM).DataField = "ShareteamNM"     '文書配布先
                .Columns(COL_DOC_CINMB).DataField = "CINmb"                 'CI番号               ※隠し列
                .Columns(COL_DOC_EXISTSFILE).DataField = "ExistsFile"       'ファイル有無         ※隠し列
                .Columns(COL_DOC_FILEPATH).DataField = "FilePath"           'ファイルパス         ※隠し列
                .Columns(COL_DOC_CIKBNCD).DataField = "CIKbnCD"             'CI種別コード       　※隠し列

                '隠し列非表示設定
                .Columns(COL_DOC_CINMB).Visible = False                     'CI番号
                .Columns(COL_DOC_EXISTSFILE).Visible = False                'ファイル有無
                .Columns(COL_DOC_FILEPATH).Visible = False                  'ファイルパス
                .Columns(COL_DOC_CIKBNCD).Visible = False                   'CI種別コード 

            End With

            'システム／サポセン／部所有機器用スプレッド
            With dataHBKB0101.PropVwOther.Sheets(0)

                'データフィールド設定
                .Columns(COL_OTHER_KIND).DataField = "KindNM"               '種別
                .Columns(COL_OTHER_NUM).DataField = "Num"                   '番号
                .Columns(COL_OTHER_CLASS1).DataField = "Class1"             '分類１
                .Columns(COL_OTHER_CLASS2).DataField = "Class2"             '分類２
                .Columns(COL_OTHER_CINM).DataField = "CINM"                 '名称
                .Columns(COL_OTHER_STATUS).DataField = "CIStateNM"          'ステータス
                .Columns(COL_OTHER_CINAIYO).DataField = "CINaiyo"           '説明
                .Columns(COL_OTHER_LASTUPDT).DataField = "UpdateDT"         '最終更新日時
                .Columns(COL_OTHER_LASTUPUSR).DataField = "HBKUsrNM"        '最終更新者
                .Columns(COL_OTHER_CIOWNER).DataField = "GroupNM"           'CIオーナー
                .Columns(COL_OTHER_CINMB).DataField = "CINmb"               'CI番号               ※隠し列
                .Columns(COL_OTHER_KINDSORT).DataField = "KindSort"         '種別マスタソート順   ※隠し列
                .Columns(COL_OTHER_CIKBNCD).DataField = "CIKbnCD"           'CI種別コード       　※隠し列

                '隠し列非表示設定
                .Columns(COL_OTHER_CINMB).Visible = False                   'CI番号
                .Columns(COL_OTHER_KINDSORT).Visible = False                '種別マスタソート順
                .Columns(COL_OTHER_CIKBNCD).Visible = False                 'CI種別コード 

            End With

            'アクティブスプレッド設定
            If SetAcitiveVw(dataHBKB0101) = False Then
                Return False
            End If


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' CI種別ごとのコントロール設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>CI種別ごとのコントロールの初期設定を行う
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitControlPerCIKbn(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                'CI種別によりコントロール設定
                Select Case .PropLstCiClassCD.SelectedValue

                    Case CI_TYPE_SYSTEM      'システムの場合

                        .PropTxtNumberCD.ImeMode = ImeMode.Disable                  '番号：IMEモードOFF
                        .PropTxtDocCD.Enabled = False                               '文書配付先      ：非活性
                        .PropBtnNewReg.Enabled = True                               '新規登録ボタン　：活性
                        .PropBtnUpPack.Enabled = True   '一括登録ボタン　：活性

                    Case CI_TYPE_DOC         '文書の場合

                        .PropTxtNumberCD.ImeMode = ImeMode.NoControl                '番号：IMEモードON
                        .PropTxtDocCD.Enabled = True                                '文書配付先      ：活性
                        .PropBtnNewReg.Enabled = True                               '新規登録ボタン　：活性
                        .PropBtnUpPack.Enabled = True                               '一括登録ボタン　：活性

                    Case CI_TYPE_SUPORT      'サポセン機器の場合

                        .PropTxtNumberCD.ImeMode = ImeMode.Disable                  '番号：IMEモードOFF
                        .PropTxtDocCD.Enabled = False                               '文書配付先      ：非活性
                        .PropBtnNewReg.Enabled = False                              '新規登録ボタン　：非活性
                        .PropBtnUpPack.Enabled = False                              '一括登録ボタン　：非活性

                    Case CI_TYPE_KIKI        '部所有機器の場合

                        .PropTxtNumberCD.ImeMode = ImeMode.Disable                  '番号：IMEモードOFF
                        .PropTxtDocCD.Enabled = False                               '文書配付先      ：非活性
                        .PropBtnNewReg.Enabled = True                               '新規登録ボタン　：活性
                        .PropBtnUpPack.Enabled = True                               '一括登録ボタン　：活性

                End Select
              
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' アクティブスプレッド設定
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>CI種別に応じてアクティブなスプレッドを設定する
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetAcitiveVw(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0101

                '検索ボタンクリック時のCI種別に応じてアクティブなスプレッドを設定する
                Select Case .PropLstCiClassCD.SelectedValue

                    '文書選択時は文書スプレッド表示
                    'システム／サポセン／部所有機器選択時はその他スプレッド表示
                    Case CI_TYPE_SYSTEM                     'システム

                        .PropVwDoc.Visible = False
                        .PropVwOther.Visible = True

                    Case CI_TYPE_DOC                        '文書

                        .PropVwDoc.Visible = True
                        .PropVwOther.Visible = False

                    Case CI_TYPE_SUPORT                     'サポセン

                        .PropVwDoc.Visible = False
                        .PropVwOther.Visible = True

                    Case CI_TYPE_KIKI                       '部所有機器

                        .PropVwDoc.Visible = False
                        .PropVwOther.Visible = True

                End Select

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' リスト変更時時のフォームオブジェクトの初期化処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormObjectList(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '種別コンボボックス作成
            If SetCmbKind(dataHBKB0101) = False Then
                Return False
            End If

            'ステータスコンボボックス作成
            If SetCmbStatus(dataHBKB0101) = False Then
                Return False
            End If

            'CI種別ごとのコントロール設定
            If SetInitControlPerCIKbn(dataHBKB0101) = False Then
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' 検索結果件数取得作成
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下後の一覧件数取得処理
    ''' <para>作成情報：2012/06/01 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCount(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable()

        Try
            'コネクションを開く
            Cn.Open()

            dataHBKB0101.PropCount = "COUNT"

            'カウントSQLの発行
            If sqlHBKB0101.SelectSearchList(Adapter, Cn, dataHBKB0101) = False Then
                Return False
            End If

            '開始ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            '取得データをデータクラスへ保存
            dataHBKB0101.PropIntResultCnt = Table.Rows(0)(0)

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
            Table.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 件数チェック処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>取得データが1件以上あるかチェックする
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckResultDataCnt(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0101

                '件数が0件の場合
                If .PropIntResultCnt = 0 Then

                    '【EDIT】 2012/09/27 r.hoshino START
                    '挙動修正　検索一覧がクリアされてからメッセージを表示
                    ''インフォメーションメッセージを表示
                    'MsgBox(B0101_I001, MsgBoxStyle.Information, TITLE_INFO)

                    'アクティブな一覧をセット
                    If SetAcitiveVw(dataHBKB0101) = False Then
                        Return False
                    End If

                    '一覧クリア
                    If SheetAllClear(dataHBKB0101) = False Then
                        Return False
                    End If

                    '件数クリア
                    .PropLblCount.Text = "0 件"

                    'インフォメーションメッセージを表示
                    MsgBox(B0101_I001, MsgBoxStyle.Information, TITLE_INFO)
                    '【EDIT】 2012/09/27 r.hoshino END

                    'ボタン活性非活性・処理
                    .PropBlnEnabledFlg = False          '活性／非活性判定用フラグ
                    If ChangeEnabled(dataHBKB0101) = False Then
                        Return False
                    End If

                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' 検索メイン処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下後の一覧取得処理
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SearchListMain(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'アクティブスプレッド設定
        If SetAcitiveVw(dataHBKB0101) = False Then
            Return False
        End If

        '検索処理
        If SearchList(dataHBKB0101) = False Then
            Return False
        End If

        '検索後データ設定
        If SetSearchData(dataHBKB0101) = False Then
            Return False
        End If

        'ボタン活性化処理
        If SetEnabled(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function

    ''' <summary>
    ''' デフォルトソート処理メイン
    ''' </summary>
    ''' <param name="dataHBKB0101">[IN/OUT]共通検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果一覧のソート順をデフォルトに戻す
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'デフォルトソート処理
        If SortSearchData(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 検索条件保存処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下時の検索条件をデータクラスにセットする
    ''' <para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SaveSearhCondition(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '検索条件をデータクラスに保存
            With dataHBKB0101

                .PropStrGroupCD_Search = .PropCmbGroupCD.SelectedValue          'グループCD
                .PropStrCiKbnCD_Search = .PropLstCiClassCD.SelectedValue        'CI種別CD
                .PropStrKindCD_Search = .PropCmbClassCD.SelectedValue           '種別CD
                .PropStrNum_Search = .PropTxtNumberCD.Text                      '番号
                .PropStrStatusCD_Search = .PropCmbStatusCD.SelectedValue        'ステータスCD
                .PropStrCiOwnerCD_Search = .PropCmbCiOwnerCD.SelectedValue      'CIオーナーCD
                .PropStrClass1_Search = .PropTxtCategory1CD.Text                '分類１
                .PropStrClass2_Search = .PropTxtCategory2CD.Text                '分類２
                .PropStrCINM_Search = .PropTxtNameCD.Text                       '名称
                .PropStrFreeWordAimai_Search = .PropTxtFreeWordCD.Text          'フリーワード
                .PropStrUpdateDTFrom_Search = .PropDtpStartDT.txtDate.Text      '最終更新日(FROM)
                .PropStrUpdateDTTo_Search = .PropDtpEndDT.txtDate.Text          '最終更新日(TO)
                .PropStrBikoAimai_Search = .PropTxtFreeTextCD.Text              'フリーテキスト
                .PropStrFreeFlg1_Search = .PropCmbFreeFlag1CD.SelectedValue     'フリーフラグ1
                .PropStrFreeFlg2_Search = .PropCmbFreeFlag2CD.SelectedValue     'フリーフラグ2
                .PropStrFreeFlg3_Search = .PropCmbFreeFlag3CD.SelectedValue     'フリーフラグ3
                .PropStrFreeFlg4_Search = .PropCmbFreeFlag4CD.SelectedValue     'フリーフラグ4
                .PropStrFreeFlg5_Search = .PropCmbFreeFlag5CD.SelectedValue     'フリーフラグ5
                If .PropLstCiClassCD.SelectedValue = CI_TYPE_DOC Then
                    .PropStrShareteamNM_Search = .PropTxtDocCD.Text             '文書配付先
                Else
                    .PropStrShareteamNM_Search = ""
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

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
    ''' 検索結果一覧作成
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索ボタン押下後の一覧取得処理
    ''' <para>作成情報：2012/06/01 abe
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchList(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)
        Dim Adapter As New NpgsqlDataAdapter
        Dim Table As New DataTable()

        Try
            'コネクションを開く
            Cn.Open()

            dataHBKB0101.PropCount = ""

            '検索SQLの発行
            If sqlHBKB0101.SelectSearchList(Adapter, Cn, dataHBKB0101) = False Then
                Return False
            End If

            '開始ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果一覧取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)

            '取得データをデータクラスへ保存
            If dataHBKB0101.PropLstCiClassCD.SelectedValue = CI_TYPE_DOC Then
                dataHBKB0101.PropVwDoc.DataSource = Table
            Else
                dataHBKB0101.PropVwOther.DataSource = Table
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
            Table.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索データ設定処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>検索データを画面に設定する
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSearchData(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                '検索後スプレッド設定
                If SetVwAfterSearch(dataHBKB0101) = False Then
                    Return False
                End If

                '件数設定
                .PropLblCount.Text = .PropIntResultCnt.ToString() & " 件"


            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

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
    ''' 検索後スプレッド設定処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>データに応じてスプレッド内のデータの表示を変更する
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwAfterSearch(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                Select Case .PropLstCiClassCD.SelectedValue

                    Case CI_TYPE_DOC

                        '文書の場合、アップロードファイルの有無により開くボタンを制御する
                        For i As Integer = 0 To .PropVwDoc.Sheets(0).RowCount - 1

                            Dim btnCell As New FarPoint.Win.Spread.CellType.ButtonCellType
                            btnCell.Text = BTN_OPEN_TITLE

                            'ファイル有無をチェック
                            Dim blnExistsFile As Boolean = Boolean.Parse(.PropVwDoc.Sheets(0).GetValue(i, COL_DOC_EXISTSFILE))
                            If blnExistsFile Then
                                'ファイルがある場合、ボタン活性化　※ロック解除及びボタン色をデフォルト設定
                                With .PropVwDoc.Sheets(0).Cells(i, COL_DOC_BTN_OPEN)
                                    .Locked = False
                                    .VisualStyles = FarPoint.Win.VisualStyles.Auto
                                    .CellType = btnCell
                                End With                          
                            Else
                                'ファイルがない場合、ボタン非活性化 ※ロックおよびボタン色変更
                                btnCell.ButtonColor = PropCellBackColorGRAY
                                btnCell.TextColor = PropCellBackColorDARKGRAY
                                With.PropVwDoc.Sheets(0).Cells(i, COL_DOC_BTN_OPEN)
                                    .Locked = True
                                    .VisualStyles = FarPoint.Win.VisualStyles.Off
                                    .CellType = btnCell
                                End With
                            End If

                        Next

                End Select

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.CommonDeclare.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

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
    ''' クリア処理
    ''' </summary>
    ''' <returns>boolean 初期表示    true  成功  false  失敗</returns>
    ''' <remarks>初期表示時に各情報の取得および格納を行う
    ''' <para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearAll(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コントロールを初期化する
            'サポセン機器の場合はブランクを表示する
            If dataHBKB0101.PropLstCiClassCD.SelectedValue = CommonDeclareHBK.CI_TYPE_SUPORT Then
                dataHBKB0101.PropCmbClassCD.SelectedValue = ""
            End If

            '各コントロールの初期設定
            dataHBKB0101.PropCmbGroupCD.SelectedValue = PropEditorGroupCD                   'グループ名
            dataHBKB0101.PropLstCiClassCD.SelectedValue = dataHBKB0101.PropStrPlmCIKbnCD    'CI種別
            dataHBKB0101.PropTxtNumberCD.Text = ""                                          '番号
            dataHBKB0101.PropCmbStatusCD.SelectedIndex = 0                                  'ステータスにブランク表示
            dataHBKB0101.PropCmbCiOwnerCD.SelectedIndex = 0                                 'CIオーナーにブランク表示
            dataHBKB0101.PropTxtCategory1CD.Text = ""                                       '分類１
            dataHBKB0101.PropTxtCategory2CD.Text = ""                                       '分類２
            dataHBKB0101.PropTxtNameCD.Text = ""                                            '名称
            dataHBKB0101.PropTxtFreeWordCD.Text = ""                                        'フリーワード
            dataHBKB0101.PropDtpStartDT.txtDate.Text = ""                                   '最終更新日(FROM)
            dataHBKB0101.PropDtpEndDT.txtDate.Text = ""                                     '最終更新日(TO)
            dataHBKB0101.PropTxtFreeTextCD.Text = ""                                        'フリーテキスト
            dataHBKB0101.PropCmbFreeFlag1CD.SelectedValue = ""                              'フラグにブランク表示 
            dataHBKB0101.PropCmbFreeFlag2CD.SelectedValue = ""                              'フラグにブランク表示
            dataHBKB0101.PropCmbFreeFlag3CD.SelectedValue = ""                              'フラグにブランク表示
            dataHBKB0101.PropCmbFreeFlag4CD.SelectedValue = ""                              'フラグにブランク表示
            dataHBKB0101.PropCmbFreeFlag5CD.SelectedValue = ""                              'フラグにブランク表示
            dataHBKB0101.PropTxtDocCD.Text = ""                                             '文書配付先

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理
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
    ''' デフォルトソート処理
    ''' </summary>
    ''' <param name="dataHBKB0101">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0101

                '選択されたCI種別に応じてソート項目を変更する
                Select Case .PropLstCiClassCD.SelectedValue

                    '文書選択時は文書スプレッド表示
                    'システム／サポセン／部所有機器選択時はその他スプレッド表示
                    Case CI_TYPE_SYSTEM

                        'システムはCI番号順
                        With .PropVwOther.Sheets(0)
                            '昇順にソートする
                            .SortRows(COL_OTHER_CINMB, True, False)
                            'ソートインジケーターの初期化
                            For i = 0 To .Columns.Count - 1
                                .Columns(i).ResetSortIndicator()
                            Next
                        End With

                    Case CI_TYPE_DOC

                        '文書は分類１、分類２、名称順
                        With .PropVwDoc.Sheets(0)
                            '複数のソート項目をセット
                            Dim si(2) As SortInfo
                            si(0) = New SortInfo(COL_DOC_CLASS1, True)
                            si(1) = New SortInfo(COL_DOC_CLASS2, True)
                            si(2) = New SortInfo(COL_DOC_CINM, True)
                            'ソート実行
                            .SortRows(0, .RowCount, si)
                            'ソートインジケーターの初期化
                            For i = 0 To .Columns.Count - 1
                                .Columns(i).ResetSortIndicator()
                            Next
                        End With
                        
                    Case CI_TYPE_SUPORT

                        'サポセンは種別マスタ表示順、番号順
                        With .PropVwOther.Sheets(0)
                            '複数のソート項目をセット
                            Dim si(1) As SortInfo
                            si(0) = New SortInfo(COL_OTHER_KINDSORT, True)
                            si(1) = New SortInfo(COL_OTHER_NUM, True)
                            'ソート実行
                            .SortRows(0, .RowCount, si)
                            'ソートインジケーターの初期化
                            For i = 0 To .Columns.Count - 1
                                .Columns(i).ResetSortIndicator()
                            Next
                        End With

                    Case CI_TYPE_KIKI
                        
                        '部所有機器は種別マスタ表示順、番号順
                        With .PropVwOther.Sheets(0)
                            '複数のソート項目をセット
                            Dim si(1) As SortInfo
                            si(0) = New SortInfo(COL_OTHER_KINDSORT, True)
                            si(1) = New SortInfo(COL_OTHER_NUM, True)
                            'ソート実行
                            .SortRows(0, .RowCount, si)
                            'ソートインジケーターの初期化
                            For i = 0 To .Columns.Count - 1
                                .Columns(i).ResetSortIndicator()
                            Next
                        End With

                End Select

            End With


            '終了ログ出力
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
    ''' Spreadシート全削除
    ''' </summary>
    ''' <remarks>Spreadシートの行をすべて削除する
    ''' <para>作成情報：2012/06/28 kuga
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SheetAllClear(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKB0101

                If .PropLstCiClassCD.SelectedValue = CI_TYPE_DOC AndAlso .PropVwDoc.Sheets(0).RowCount > 0 Then
                    .PropVwDoc.Sheets(0).RowCount = 0
                Else
                    .PropVwOther.Sheets(0).RowCount = 0
                End If

            End With

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' ファイルオープン処理
    ''' </summary>
    ''' <remarks>選択データのファイルを開く
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OpenFile(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        ' 開始ログ出力
        Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strCmd As String = ""               'コマンド文字列
        Dim strDriveName As String = ""         '使用論理ドライブ名
        Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)         'ログ出力フォルダ設定
        Dim strDLFileName As String = ""        'TempDLファイル
        Dim fileName As String                  'ファイル名

        Try
            With dataHBKB0101

                ''選択データのファイル名を取得
                'fileName = Trim(.PropVwDoc.Sheets(0).GetValue(.PropIntSelectedRow, COL_DOC_FILEPATH)) 'ファイル名

                ''ファイル存在チェック
                'If File.Exists(fileName) = False Then
                '    'ファイルが見つからない場合、エラーを返す
                '    puErrMsg = B0101_E002
                '    Return False
                'End If

                ''読取専用にする
                'File.SetAttributes(fileName, File.GetAttributes(fileName) Or FileAttributes.ReadOnly)

                ''ファイルを開く
                'System.Diagnostics.Process.Start(fileName)

                ''読取専用を解除する
                'File.SetAttributes(fileName, FileAttributes.Normal)

                'PCの論理ドライブ名をすべて取得する
                Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                '利用可能な論理ドライブ名を取得する
                For Each strDrive As String In DRIVES
                    If strDrives.Contains(strDrive) = False Then
                        strDriveName = strDrive.Substring(0, 2)
                        Exit For
                    End If
                Next

                '選択データのファイル名を取得
                fileName = Trim(.PropVwDoc.Sheets(0).GetValue(.PropIntSelectedRow, COL_DOC_FILEPATH)) 'ファイル名
                'Temp保存用ファイル名設定
                strDLFileName = System.IO.Path.GetFileNameWithoutExtension(fileName) & Now().ToString("yyyyMMddmmss") & _
                                                System.IO.Path.GetExtension(fileName)

                'NetUse設定
                If commonLogicHBK.NetUseConect(strDriveName) = False Then
                    Return False
                End If

                'Tempにコピー
                Directory.CreateDirectory(strOutputDir)
                FileCopy(strDriveName & "\\" & fileName, strOutputDir & "\\" & strDLFileName)

                'ファイル存在チェック
                If System.IO.File.Exists(strOutputDir & "\\" & strDLFileName) Then

                    Dim fas As System.IO.FileAttributes = System.IO.File.GetAttributes(strOutputDir & "\\" & strDLFileName)
                    ' ファイル属性に読み取り専用を追加
                    fas = fas Or System.IO.FileAttributes.ReadOnly
                    ' ファイル属性を設定
                    System.IO.File.SetAttributes(strOutputDir & "\\" & strDLFileName, fas)
                    'プロセススタート
                    System.Diagnostics.Process.Start(strOutputDir & "\\" & strDLFileName)

                End If

            End With

            ' 終了ログ出力
            Common.CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0101_E002
            Return False

        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)

            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & B0101_E002
            Return False
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)

        End Try
    End Function

    ''' <summary>
    ''' [Excel出力]ボタン活性化処理
    ''' </summary>
    ''' <param name="dataHBKB0101">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>[Excel出力]ボタンを活性化する
    ''' <para>作成情報：2012/09/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetEnabled(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        Try

            '活性／非活性フラグにTrueをセット
            dataHBKB0101.PropBlnEnabledFlg = True
            '活性／非活性処理
            If ChangeEnabled(dataHBKB0101) = False Then
                Return False
            End If

            '終了ログ出力
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
    ''' 閾値チェック時、表示を行わない場合の処理
    ''' </summary>
    ''' <param name="dataHBKB0101">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>閾値チェックを行った際検索結果が閾値を超えており出力しない場合の処理
    ''' <para>作成情報：2012/09/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function IndicateNotResult(ByRef dataHBKB0101 As DataHBKB0101) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッドシート初期化
        If SheetAllClear(dataHBKB0101) = False Then
            Return False
        End If

        'ボタン非活性処理
        dataHBKB0101.PropBlnEnabledFlg = False
        If ChangeEnabled(dataHBKB0101) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' [Excel]ボタン活性・非活性切り替え処理
    ''' </summary>
    ''' <param name="dataHBKB0101">[IN/OUT]部所有機器検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>判定フラグを見て[Excel]出力ボタン活性／非活性を切り替える
    ''' <para>作成情報：2012/09/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function ChangeEnabled(ByRef dataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKB0101

                If .PropBlnEnabledFlg = True Then
                    'ボタンを活性状態にする
                    .PropBtnOutput.Enabled = True
                Else
                    'ボタンを非活性状態にする
                    .PropBtnOutput.Enabled = False
                End If

            End With

            '終了ログ出力
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

End Class
