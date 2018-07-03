Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' エンドユーザーマスター検索一覧画面ロジッククラス
''' </summary>
''' <remarks>エンドユーザーマスター検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/08/06 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0301

    'インスタンス生成
    Private sqlHBKX0301 As New SqlHBKX0301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    'エンドユーザーマスター検索結果列番号
    Public Const ENDUSR_ID As Integer = 0                 'エンドユーザーID
    Public Const ENDUSR_NM As Integer = 1                 'エンドユーザー氏名
    Public Const ENDUSR_NM_KANA As Integer = 2            'エンドユーザー氏名カナ
    Public Const ENDUSR_COMPANY As Integer = 3            '所属会社
    Public Const ENDUSR_BUSYONM As Integer = 4            '部署名
    Public Const ENDUSR_TEL As Integer = 5                '電話番号
    Public Const ENDUSR_MAILADD As Integer = 6            'メールアドレス
    Public Const USR_KBN As Integer = 7                   'ユーザー区分
    Public Const REG_KBN As Integer = 8                   '登録方法
    Public Const STATE_NAIYO As Integer = 9               '状態説明
    Public Const REG_KBN_SORT As Integer = 10             '登録方法(ソート用)


    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスター検索一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド用データテーブル作成
        If CreateDataTableForVw(dataHBKX0301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX0301) = False Then
            Return False
        End If

        'コンボボックスの作成
        If Createcmb(dataHBKX0301) = False Then
            Return False
        End If

        '項目初期化
        If ClearSearch(dataHBKX0301) = False Then
            Return False
        End If

        '項目非活性処理
        If ChangeEnable(dataHBKX0301) = False Then
            Return False
        End If

        'スプレッド隠し項目設定処理
        If Setvisible(dataHBKX0301) = False Then
            Return False

        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtEndUsrMaster As New DataTable   'エンドユーザーマスター検索用データテーブル

        Try

            'エンドユーザーマスター検索一覧用テーブル作成
            With dtEndUsrMaster
                .Columns.Add("EndUsrID", Type.GetType("System.String"))                 'エンドユーザーID
                .Columns.Add("EndUsrNM", Type.GetType("System.String"))                 'エンドユーザー氏名
                .Columns.Add("EndUsrNMkana", Type.GetType("System.String"))             'エンドユーザー氏名カナ
                .Columns.Add("EndUsrCompany", Type.GetType("System.String"))            '所属会社
                .Columns.Add("EndUsrBusyoNM", Type.GetType("System.String"))            '部署名
                .Columns.Add("EndUsrTel", Type.GetType("System.String"))                '電話番号
                .Columns.Add("EndUsrMailAdd", Type.GetType("System.String"))            'メールアドレス
                .Columns.Add("UsrKbn", Type.GetType("System.String"))                   'ユーザー区分
                .Columns.Add("RegKbn", Type.GetType("System.String"))                   '登録方法
                .Columns.Add("StateNaiyo", Type.GetType("System.String"))               '状態説明
                .Columns.Add("RegKbnSort", Type.GetType("System.String"))               '登録方法(ソート用、隠し項目)
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX0301
                .PropDtEndUsrMaster = dtEndUsrMaster                                     'スプレッド表示用：エンドユーザーマスター検索一覧
              
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
            dtEndUsrMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示データを取得する
    ''' <para>作成情報：2012/09/07 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter


        Try

            'コネクションを開く
            Cn.Open()
            'ユーザー区分セレクトボックス初期表示用データ取得
            If UsrKbnSelectBoxGetInitData(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()

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
    ''' ユーザー区分セレクトボックス初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/07 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UsrKbnSelectBoxGetInitData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtEndUsrMtb As New DataTable

        Try


            'SQLの作成・設定
            If sqlHBKX0301.SetSelectEndUsrMasterUsrKbnSql(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター/ユーザー区分取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtEndUsrMtb)

            '取得データをデータクラスにセット
            dataHBKX0301.PropDtEndUsrMasterUsrKbn = dtEndUsrMtb


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
            dtEndUsrMtb.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 項目初期化処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各項目を初期化する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearSearch(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            '検索項目初期化
            If ClearText(dataHBKX0301) = False Then
                Return False
            End If

            '検索件数初期化
            If SearchResult(dataHBKX0301) = False Then
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
    ''' 【共通】検索条件初期化処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上の検索条件を初期化する
    ''' <para>作成情報：2012/07/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ClearText(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0301
                'エンドユーザーIDを初期化
                .PropTxtEndUsrID.Text = Nothing
                'エンドユーザー氏名を初期化
                .PropTxtEndUsrNM.Text = Nothing
                '部署名を初期化
                .PropTxtBusyoNM.Text = Nothing
                'ユーザー区分を初期化(ブランク)
                .PropcmbUsrKbn.SelectedIndex = 0
                '登録方法を初期化(ブランク)
                .PropCmbRegKbn.SelectedIndex = 0
                '削除データも表示チェックボックスをチェックなしに設定
                .PropChkJtiFlg.Checked = False

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
    ''' 検索件数初期表示処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の初期表示を行う
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            With dataHBKX0301

                '検索件数に0件を表示
                .PropLblCount.Text = "0件"

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
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Createcmb(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0301

                'ユーザー区分コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtEndUsrMasterUsrKbn, .PropcmbUsrKbn, True, "", "") = False Then
                    Return False
                End If

                '登録方法コンボボックス作成
                If commonLogic.SetCmbBox(Regtype, .PropCmbRegKbn) = False Then
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
    ''' 項目非活性化処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>項目を非活性化する
    ''' <para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeEnable(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'ログインユーザーグループボックスの項目非活性
            With dataHBKX0301.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

            End With

            'ログイン時のモードが閲覧モードなら登録画面へ遷移するボタンを非活性にする
            With dataHBKX0301

                If .PropStrLoginMode = LOGIN_MODE_END_USR_ETURAN Then

                    '新規登録ボタン非活性
                    .PropBtnReg.Enabled = False
                    '詳細確認ボタン非活性
                    .PropBtnInfo.Enabled = False
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
    ''' 隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内の隠し項目を設定する
    ''' <para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Setvisible(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0301.PropVwEndUsrMasterList.Sheets(0)

                '隠し項目の設定
                .Columns(REG_KBN_SORT).Visible = False     '登録方法(ソート用)

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
    ''' エンドユーザーマスター検索結果表示処理メイン
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスター検索を行い結果を表示する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SearchDataMain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索件数取得処理(削除データも含む)
        If GetResultAllcount(dataHBKX0301) = False Then
            Return False
        End If

        '検索件数判定処理
        If CheckCount(dataHBKX0301, True) = False Then
            Return False
        End If


        '削除データも表示チェックボックスが選択されていない場合は実行する
        If dataHBKX0301.PropChkJtiFlg.Checked = False Then
            '件数取得処理(削除データ除く)
            If GetResultCount(dataHBKX0301) = False Then
                Return False
            End If
        End If

        '件数判定(判定を行い表示しない場合処理を抜ける)
        If dataHBKX0301.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

            '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
            If MsgBox(String.Format(X0301_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                'データソースの初期化
                dataHBKX0301.PropDtEndUsrMaster.Clear()
                '検索件数初期化
                dataHBKX0301.PropLblCount.Text = "0件"
                '終了ログ出力
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                '正常終了
                Return True
            End If

        End If



        '検索結果取得処理
        If GetSearchData(dataHBKX0301) = False Then
            Return False
        End If

        'スプレッド出力データ設定処理
        If SetVwData(dataHBKX0301) = False Then
            Return False
        End If

        'スプレッド詳細設定
        If SetSpread(dataHBKX0301) = False Then
            Return False
        End If

        '件数表示処理
        If SetResultCount(dataHBKX0301) = False Then
            Return False
        End If

        '検索結果背景色変更処理
        If ChangeColor(dataHBKX0301) = False Then
            Return False
        End If

        '削除データも表示チェックボックスにチェックが入っていない場合のみ行う
        If dataHBKX0301.PropChkJtiFlg.Checked = False Then
            '検索件数判定処理
            If CheckCount(dataHBKX0301, False) = False Then
                Return False
            End If
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果件数データ(削除含む)取得
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果件数(削除含む)を取得する
    ''' <para>作成情報：2012/09/11 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultAllCount(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKX0301.SetResultAllCountSql(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数(削除含む)", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKX0301.PropResultCount = dtResultCount

            'コネクションのクローズ
            Cn.Close()

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
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 検索結果件数データ取得
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果件数を取得する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCount(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKX0301.SetResultCountSql(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKx0301.PropResultCount = dtResultCount

            'コネクションのクローズ
            Cn.Close()

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
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスター検索一覧画面の検索結果表示に必要なデータを取得する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'エンドユーザーマスター取得（スプレッド用）
            If GetEndUsrMaster(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'コネクションを閉じる
            Cn.Close()

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
    ''' エンドユーザーマスター検索結果取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>エンドユーザーマスター検索一覧画面の検索結果を取得する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetEndUsrMaster(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            'データクリア
            dataHBKX0301.PropDtEndUsrMaster.Clear()

            'SQLの作成・設定
            If sqlHBKX0301.SetSelectEndUsrMasterSql(Adapter, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX0301.PropDtEndUsrMaster)


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
    '''スプレッドの出力データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'エンドユーザーマスター検索結果一覧


            With dataHBKX0301.PropVwEndUsrMasterList.Sheets(0)
                .DataSource = dataHBKX0301.PropDtEndUsrMaster
                .Columns(ENDUSR_ID).DataField = "EndUsrID"
                .Columns(ENDUSR_NM).DataField = "EndUsrNM"
                .Columns(ENDUSR_NM_KANA).DataField = "EndUsrNMkana"
                .Columns(ENDUSR_COMPANY).DataField = "EndUsrCompany"
                .Columns(ENDUSR_BUSYONM).DataField = "EndUsrBusyoNM"
                .Columns(ENDUSR_TEL).DataField = "EndUsrTel"
                .Columns(ENDUSR_MAILADD).DataField = "EndUsrMailAdd"
                .Columns(USR_KBN).DataField = "UsrKbn"
                .Columns(REG_KBN).DataField = "RegKbn"
                .Columns(STATE_NAIYO).DataField = "StateNaiyo"
                .Columns(REG_KBN_SORT).DataField = "RegKbnSort"

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
    ''' スプレッド詳細設定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド表示の詳細設定を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSpread(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ表示非表示設定処理
            If DataVisible(dataHBKX0301) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0301) = False Then
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
    ''' 検索件数判定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <param name="blnDeleteCheckFlg">[IN]trueの時データソース削除/falseのとき処理しない</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の判定を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckCount(ByRef dataHBKX0301 As DataHBKX0301, ByVal blnDeleteCheckFlg As Boolean) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0301
                If blnDeleteCheckFlg = False Then
                    '件数の判定
                    If .PropResultCount.Rows(0).Item(0) = 0 Then
                        'メッセージ変数に空白セット
                        puErrMsg = ""
                        .PropLblCount.Text = "0件"
                        '終了ログ出力
                        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                        Return False
                    End If
                Else

                    '件数の判定
                    If .PropResultCount.Rows(0).Item(0) = 0 Then
                        'メッセージ変数に空白セット
                        puErrMsg = ""
                        .PropLblCount.Text = "0件"
                        'データソースの初期化
                        .PropDtEndUsrMaster.Clear()
                        '終了ログ出力
                        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
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
    ''' データ表示非表示設定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの表示非表示を設定する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DataVisible(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0301.PropVwEndUsrMasterList.Sheets(0)


                'チェックボックスの状態で表示状態を変更する
                For i = 0 To .RowCount - 1
                    'チェックボックスにチェックが入ってなくかつ、状態説明に｢削除｣が含まれる場合は表示しない
                    If dataHBKX0301.PropChkJtiFlg.Checked = False Then
                        If .Cells(i, STATE_NAIYO).Value = "" Then
                            .Rows(i).Visible = True
                        ElseIf .Cells(i, STATE_NAIYO).Value.IndexOf(STATE_NAIYO_DELETE) <> -1 Then
                            .Rows(i).Visible = False
                        Else
                            .Rows(i).Visible = True
                        End If

                    Else
                        .Rows(i).Visible = True
                    End If
                Next


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
    ''' 行ヘッダ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>行ヘッダを設定する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRowHearder(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 1    '再設定する行番号

        Try

            '行ヘッダを再設定する
            With dataHBKX0301.PropVwEndUsrMasterList.Sheets(0)

                For i = 0 To .RowCount - 1
                    '非表示でなければ行番号を割り振る
                    If .Rows(i).Visible = True Then
                        .RowHeader.Cells(i, 0).Value = intRowHeader
                        intRowHeader += 1
                    End If

                Next

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
    ''' 件数表示処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の件数を表示する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultCount(dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX0301
                '表示されている件数分カウントする
                For i = 0 To .PropVwEndUsrMasterList.Sheets(0).RowCount - 1
                    If .PropVwEndUsrMasterList.Sheets(0).Rows(i).Visible = True Then
                        intCount += 1
                    End If
                Next

                '検索件数をセット
                .PropLblCount.Text = intCount & "件"

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索結果背景色変更処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果で削除ユーザーが表示された場合に該当行をグレーにする
    ''' <para>作成情報：2012/08/07 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0301

                '背景色を初期化する
                For i = 0 To .PropVwEndUsrMasterList.Sheets(0).RowCount - 1

                    .PropVwEndUsrMasterList.Sheets(0).Rows(i).BackColor = Color.White

                Next

                For i = 0 To .PropVwEndUsrMasterList.Sheets(0).RowCount - 1
                    If .PropVwEndUsrMasterList.Sheets(0).Cells(i, STATE_NAIYO).Value.IndexOf(STATE_NAIYO_DELETE) <> -1 Then
                        '状態説明に｢削除｣の文字を含む行はグレーに設定
                        .PropVwEndUsrMasterList.Sheets(0).Rows(i).BackColor = Color.Gray
                    End If

                Next



            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' クリアボタン押下メイン時処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件を初期表示に戻す
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    ''' 
    Public Function ClearSearchMain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '検索項目初期化
        If ClearText(dataHBKX0301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソートボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/08/08 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'デフォルトソートを行う

            If DefaultSort(dataHBKX0301) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0301) = False Then
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
    ''' デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/08/08 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSort(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim Si(1) As SortInfo 'ソート対象配列

            With dataHBKX0301.PropVwEndUsrMasterList.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(REG_KBN_SORT, True) 'エンドユーザーマスター.登録方法
                Si(1) = New SortInfo(ENDUSR_ID, True)    'エンドユーザーマスター.エンドユーザーID

                '登録方法＋エンドユーザーIDの昇順でソートする
                .SortRows(0, .RowCount, Si)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1

                    .Columns(i).ResetSortIndicator()

                Next

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
    ''' 行ヘッダ再設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>列ヘッダがクリックされた場合に、行ヘッダの再設定を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetRowHeaderMain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '行ヘッダ設定処理
        If SetRowHearder(dataHBKX0301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 削除データ表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータの表示、非表示を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckMain(ByRef dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド詳細設定
        If SetSpread(dataHBKX0301) = False Then
            Return False
        End If

        '検索件数の表示
        If SetResultCount(dataHBKX0301) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 特権ログアウトログ出力メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LogoutLogMain(dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'ログアウトログ登録処理
            If LogoutLog(dataHBKX0301) = False Then
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
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 特権ログアウトログ出力処理
    ''' </summary>
    ''' <param name="dataHBKX0301">[IN/OUT]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LogoutLog(dataHBKX0301 As DataHBKX0301) As Boolean

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

            '特権ログアウトログ登録
            If InsertLogoutLog(Tsx, Cn, dataHBKX0301) = False Then
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
    ''' 特権ログアウトログ登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合にログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertLogoutLog(ByRef Tsx As NpgsqlTransaction, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '特権ログインログ（INSERT）用SQLを作成
            If sqlHBKX0301.SetInsertSuperLoginLogSql(Cmd, Cn, dataHBKX0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "特権ログアウトログ登録", Nothing, Cmd)

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
