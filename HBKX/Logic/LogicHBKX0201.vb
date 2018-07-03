Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread
''' <summary>
''' ひびきユーザーマスター登録画面ロジッククラス
''' </summary>
''' <remarks>ひびきユーザーマスター登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/21 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0201

    'インスタンス生成
    Private sqlHBKX0201 As New SqlHBKX0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================

    'ひびきユーザーマスター一覧列番号
    Public Const HBKUSR_ID As Integer = 0                 'ID
    Public Const HBKUSR_NM As Integer = 1                 '氏名
    Public Const HBKUSR_NM_KANA As Integer = 2            '氏名カナ
    Public Const HBKUSR_MAILADD As Integer = 3            'メールアドレス
    Public Const SZK_USRGROUP_FLG As Integer = 4          '管理者
    Public Const SZK_DEFAULT_FLG As Integer = 5           'デフォルト
    Public Const SZK_JTI_FLG As Integer = 6               '削除
    Public Const NEW_DATA As Integer = 7                  '新規データ(隠し項目)
    Public Const SZK_JTI_FLG_KAKUSHI As Integer = 8       '削除フラグ(隠し項目)
    Public Const SORT_KAKUSHI As Integer = 9              'ソート順(隠し項目)
    Public Const TEXT_CHANGE_FLG As Integer = 10          'テキスト更新フラグ(隠し項目)
    Public Const CHECK_CHANGE_FLG As Integer = 11         'チェックボックス更新フラグ(隠し項目)


    'チェックボックス初期状態判定用
    Public Const CHECK_FLG_ON As Integer = 0              '初期状態
    Public Const CHECK_FLG_OFF As Integer = 1             '初期状態ではない

    '新規データ/既存データ判定用
    Public Const DATA_OLD As Integer = 0                  '既存データ
    Public Const DATA_NEW As Integer = 1                  '新規データ

    '初期表示判定フラグ
    Public Const INITFORM_FLG_ON As Integer = 0           '初期表示
    Public Const INITFORM_FLG_OFF As Integer = 1          '初期表示でない

    'テキスト更新判定用
    Public Const TEXT_CHANGE_FLG_OFF As Integer = 0       '更新なし
    Public Const TEXT_CHANGE_FLG_ON As Integer = 1        '更新あり

    'チェックボックス更新判定用
    Public Const CHECK_CHANGE_FLG_OFF As Integer = 0      '更新なし
    Public Const CHECK_CHANGE_FLG_ON As Integer = 1       '更新あり

    



    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0201) = False Then
            Return False
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX0201

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnReg)              '登録ボタン

                'データクラスに作成リストをセット
                .PropAryTsxCtlList = aryCtlList

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
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスター登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'グループ管理者かスーパーユーザーかで初期表示を判断する
        With dataHBKX0201

            If .PropStrUsrAdmin = USR_GROUP_ADMIN Then
                'グループ管理者初期表示
                If GroupAdminInitForm(dataHBKX0201) = False Then
                    Return False
                End If
            ElseIf .PropStrUsrAdmin = USR_SUPER_USER Then
                'スーパーユーザー初期表示
                If SuperUserInitForm(dataHBKX0201) = False Then
                    Return False
                End If
            End If

        End With


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' グループ管理者初期表示処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループ管理者が利用する初期表示を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GroupAdminInitForm(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'グループ管理者：フォームオブジェクト設定処理
            If GroupAdminSetFormObj(dataHBKX0201) = False Then
                Return False
            End If

            'スプレッド用データテーブル作成
            If CreateDataTableForVw(dataHBKX0201) = False Then
                Return False
            End If

            'グループ管理者：初期表示用データ取得
            If GroupAdminGetInitData(dataHBKX0201) = False Then
                Return False
            End If

            'グループ管理者：初期表示用データ設定
            If GroupAdminSetInitData(dataHBKX0201) = False Then
                Return False
            End If

            '削除データ非表示処理
            If DeleteDataVisible(dataHBKX0201) = False Then
                Return False
            End If

            'スプレッド隠し項目設定処理
            If Setvisible(dataHBKX0201) = False Then
                Return False

            End If

            '出力結果背景色変更処理
            If ChangeColor(dataHBKX0201) = False Then
                Return False
            End If

            '検索件数の表示
            If SearchResult(dataHBKX0201) = False Then
                Return False
            End If

            'ソート処理
            If Sort(dataHBKX0201) = False Then
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
    ''' スーパーユーザー初期表示処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スーパーユーザーが利用する初期表示を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SuperUserInitForm(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try


            'スーパーユーザー：フォームオブジェクト設定処理
            If SuperUserSetFormObj(dataHBKX0201) = False Then
                Return False
            End If

            'スプレッド用データテーブル作成
            If CreateDataTableForVw(dataHBKX0201) = False Then
                Return False
            End If

            'スプレッド隠し項目設定処理
            If Setvisible(dataHBKX0201) = False Then
                Return False

            End If

            'スーパーユーザー：初期表示用データ取得
            If SuperUserGetInitData(dataHBKX0201) = False Then
                Return False
            End If

            'コンボボックスの作成
            If Createcmb(dataHBKX0201) = False Then
                Return False
            End If

            '検索件数の表示
            If SearchResult(dataHBKX0201) = False Then
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
    ''' グループ管理者：フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GroupAdminSetFormObj(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX0201) = False Then
                Return False
            End If



            '項目非活性処理
            If ChangeEnableAdmin(dataHBKX0201) = False Then
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
    ''' スーパーユーザー：フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SuperUserSetFormObj(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '前画面から渡されたグループコードを初期化する
            dataHBKX0201.PropStrGroupCD = ""

            'フォームオブジェクト設定共通処理
            If CommonSetFormObj(dataHBKX0201) = False Then
                Return False
            End If

            '項目活性非活性処理(スーパーユーザー)
            If ChangeEnableSuperUser(dataHBKX0201) = False Then
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
    ''' フォームオブジェクト設定共通処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通のフォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CommonSetFormObj(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'オブジェクトの活性非活性設定

            With dataHBKX0201.PropGrpLoginUser

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
    ''' 項目非活性化処理(グループ管理者)
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>項目を非活性化する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeEnableAdmin(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0201
                'グループ選択ラベル
                .PropLblGroupSelect.Visible = False
                'グループ選択コンボボックス
                .PropCmbGroupNM.Visible = False


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
    ''' 項目非活性化処理(スーパーユーザー)
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>項目を非活性化する
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeEnableSuperUser(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0201
                'グループが未選択の場合非活性にする
                If .PropStrGroupCD = "" Then
                    '＋ボタン
                    .PropBtnAddRow.Enabled = False
                    '－ボタン
                    .PropBtnRemoveRow.Enabled = False
                    '登録ボタン
                    .PropBtnReg.Enabled = False
                    'グループが選択されている場合活性状態にする
                Else
                    '＋ボタン
                    .PropBtnAddRow.Enabled = True
                    '－ボタン
                    .PropBtnRemoveRow.Enabled = True
                    '登録ボタン
                    .PropBtnReg.Enabled = True
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
    ''' グループ管理者：スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGroupAdminHBKUsrMaster As New DataTable   'グループ管理者：ひびきユーザーマスター登録用データテーブル

        Try

            'ひびきユーザーマスター登録用テーブル作成
            With dtGroupAdminHBKUsrMaster
                .Columns.Add("HBKUsrID", Type.GetType("System.String"))                 'ひびきユーザーID
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))                 '氏名
                .Columns.Add("HBKUsrNmKana", Type.GetType("System.String"))             '氏名カナ
                .Columns.Add("HBKUsrMailAdd", Type.GetType("System.String"))            'メールアドレス
                .Columns.Add("DefaultFlg", Type.GetType("System.Boolean"))              'ユーザーグループ権限
                .Columns.Add("UsrGroupFlg", Type.GetType("System.Boolean"))             'デフォルトフラグ
                .Columns.Add("JtiFlg", Type.GetType("System.Boolean"))                  '削除フラグ
                .Columns.Add("NewData", Type.GetType("System.String"))                  '新規登録データ(隠し列)
                .Columns.Add("JtiFlgKAKUSHI", Type.GetType("System.String"))            '削除フラグ(隠し列)
                .Columns.Add("Sort", Type.GetType("System.Int32"))                      'ソート順(隠し列)
                .Columns.Add("TextChangeFlg", Type.GetType("System.Int32"))            'テキスト更新フラグ(隠し列)
                .Columns.Add("CheckChangeFlg", Type.GetType("System.Int32"))            'チェックボックス更新フラグ(隠し列)


        
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX0201
                .PropDtHBKUsrMasterList = dtGroupAdminHBKUsrMaster                      'スプレッド表示用：エンドユーザーマスター検索一覧

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
            dtGroupAdminHBKUsrMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' グループ管理者：初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループ管理者が利用する初期表示データを取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GroupAdminGetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



       

        Try
            'スプレッド初期表示用データ取得
            If SpreadGetInitData(dataHBKX0201) = False Then
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
    ''' スーパーユーザー：初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スーパーユーザーが利用する初期表示データを取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SuperUserGetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            'セレクトボックス初期表示用データ取得
            If SelectBoxGetInitData(dataHBKX0201) = False Then
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
    ''' スプレッド初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SpreadGetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()


            With dataHBKX0201

                'ひびきユーザーマスター、所属マスターデータ取得(削除データ含む)
                If GetHBKUsrSZKMasterDeleteData(Adapter, Cn, dataHBKX0201) = False Then
                    Return False
                End If

            End With

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
    ''' セレクトボックス初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectBoxGetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'グループマスターデータ取得
            If GetGroupMasterData(Adapter, Cn, dataHBKX0201) = False Then
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
    ''' グループマスターデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループマスターデータを取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetGroupMasterData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtGroupMtb As New DataTable


        Try


            'グループマスターデータ取得

            'グループマスターデータ取得用SQLの作成・設定
            If sqlHBKX0201.SetSelectGroupMasterSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGroupMtb)

            '取得データをデータクラスにセット
            dataHBKX0201.PropDtGroupMtb = dtGroupMtb

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
            dtGroupMtb.Dispose()

        End Try


    End Function


    ''' <summary>
    ''' グループ管理者：初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループ管理者の初期表示設定を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GroupAdminSetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '初期表示データをスプレッドに設定
            If SetInitData(dataHBKX0201) = False Then
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
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをスプレッドに設定する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            'ひびきユーザーマスター一覧


            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)
                .DataSource = dataHBKX0201.PropDtHBKUsrMasterList
                .Columns(HBKUSR_ID).DataField = "HBKUsrID"
                .Columns(HBKUSR_NM).DataField = "HBKUsrNM"
                .Columns(HBKUSR_NM_KANA).DataField = "HBKUsrNmKana"
                .Columns(HBKUSR_MAILADD).DataField = "HBKUsrMailAdd"
                .Columns(SZK_USRGROUP_FLG).DataField = "UsrGroupFlg"
                .Columns(SZK_DEFAULT_FLG).DataField = "DefaultFlg"
                .Columns(SZK_JTI_FLG).DataField = "JtiFlg"
                .Columns(NEW_DATA).DataField = "NewData"
                .Columns(SZK_JTI_FLG_KAKUSHI).DataField = "JtiFlgKAKUSHI"
                .Columns(SORT_KAKUSHI).DataField = "Sort"
                .Columns(TEXT_CHANGE_FLG).DataField = "TextChangeFlg"
                .Columns(CHECK_CHANGE_FLG).DataField = "CheckChangeFlg"
               
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
    ''' 削除データ非表示設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータのうち論理削除されているものは非表示にする
    ''' <para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteDataVisible(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)

                '削除データも表示にチェックが入ってなければ処理を行う
                If dataHBKX0201.PropChkJtiFlg.Checked = False Then
                    For i = 0 To .RowCount - 1
                        '削除データの場合は表示しない
                        If .Cells(i, SZK_JTI_FLG_KAKUSHI).Value = DATA_MUKO Then
                            .Rows(i).Visible = False
                        End If
                    Next
                    '削除データも表示にチェックが入っていれば処理を行う
                ElseIf dataHBKX0201.PropChkJtiFlg.Checked = True Then
                    For i = 0 To .RowCount - 1

                        .Rows(i).Visible = True

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
    ''' 隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内の隠し項目を設定する
    ''' <para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Setvisible(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)

                '隠し項目の設定
                .Columns(NEW_DATA).Visible = False             '新規データ(隠し項目)
                .Columns(SZK_JTI_FLG_KAKUSHI).Visible = False  '所属マスター：削除フラグ(隠し項目)
                .Columns(SORT_KAKUSHI).Visible = False         'ソート順(隠し項目)
                .Columns(TEXT_CHANGE_FLG).Visible = False      'テキスト更新フラグ(隠し項目)
                .Columns(CHECK_CHANGE_FLG).Visible = False     'チェックボックス更新フラグ(隠し項目)

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
    ''' 検索結果背景色変更処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>出力結果で削除ユーザーが表示された場合に該当行をグレーにする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0201


                '背景色を変更する
                For i = 0 To .PropVwHBKUsrMasterList.Sheets(0).RowCount - 1
                    For j = 0 To .PropVwHBKUsrMasterList.Sheets(0).ColumnCount - 1
                        If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_JTI_FLG_KAKUSHI).Value = DATA_MUKO Then
                            '削除データの場合は背景色をグレーに変更
                            .PropVwHBKUsrMasterList.Sheets(0).Cells(i, j).BackColor = Color.Gray
                        ElseIf .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_JTI_FLG_KAKUSHI).Value = DATA_YUKO Then
                            '削除チェックボックスが無効の場合はIDを薄い黄色、その他を白に変更
                            .PropVwHBKUsrMasterList.Sheets(0).Cells(i, HBKUSR_ID).BackColor = Color.FromArgb(255, 255, 128)
                            For s = 1 To .PropVwHBKUsrMasterList.Sheets(0).ColumnCount - 1
                                .PropVwHBKUsrMasterList.Sheets(0).Cells(i, s).BackColor = Color.White
                            Next
                        End If

                    Next
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
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の表示を行う
    ''' <para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX0201
                '表示されている件数分カウントする
                For i = 0 To .PropVwHBKUsrMasterList.Sheets(0).RowCount - 1
                    If .PropVwHBKUsrMasterList.Sheets(0).Rows(i).Visible = True _
                        And .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_JTI_FLG_KAKUSHI).Value <> Nothing Then
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
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Createcmb(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0201

                
                'グループ選択コンボボックス作成
                If commonLogic.SetCmbBox(.PropDtGroupMtb, .PropCmbGroupNM, True, "", "") = False Then
                    Return False
                End If


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
    ''' グループ選択メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループ選択時に所属するメンバーを表示する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectGroupMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択グループコード設定処理
        If SetSelectGroupCD(dataHBKX0201) = False Then
            Return False
        End If

        '項目活性非活性処理(スーパーユーザー)
        If ChangeEnableSuperUser(dataHBKX0201) = False Then
            Return False
        End If

        'スプレッド初期表示用データ取得
        If SpreadGetInitData(dataHBKX0201) = False Then
            Return False
        End If

        '初期表示データをスプレッドに設定
        If SetInitData(dataHBKX0201) = False Then
            Return False
        End If

        '削除データ非表示処理
        If DeleteDataVisible(dataHBKX0201) = False Then
            Return False
        End If

        '出力結果背景色変更処理
        If ChangeColor(dataHBKX0201) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(dataHBKX0201) = False Then
            Return False
        End If

        'ソート処理
        If Sort(dataHBKX0201) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 選択グループコード設定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたグループコードを検索条件として設定する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSelectGroupCD(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0201
                .PropStrGroupCD = .PropCmbGroupNM.SelectedValue.ToString
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
    ''' 削除データ表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータの表示、非表示を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '削除データ非表示処理
        If DeleteDataVisible(dataHBKX0201) = False Then
            Return False
        End If

        '出力結果背景色変更処理
        If ChangeColor(dataHBKX0201) = False Then
            Return False
        End If
        '検索件数の表示
        If SearchResult(dataHBKX0201) = False Then
            Return False
        End If

        'ソート処理
        If Sort(dataHBKX0201) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ソート処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックボックスの状態によって並び順を変更する
    ''' <para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Sort(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Si(1) As SortInfo 'ソート対象配列

        Try
            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)
                '削除データも表示のチェックが入っていない時
                If dataHBKX0201.PropChkJtiFlg.Checked = False Then
                    'ソート対象列をソートする順番で指定
                    Si(0) = New SortInfo(SORT_KAKUSHI, True) 'ソート順(隠し列)
                    Si(1) = New SortInfo(HBKUSR_ID, True) 'ひびきユーザーマスター：ひびきユーザーID
                    '追加行判定(隠し列)、所属マスター：削除フラグ(隠し列)、ひびきユーザーマスター：ひびきユーザーIDの昇順でソートする
                    .SortRows(0, .RowCount, Si)

                    '削除データも表示のチェックが入っていない時
                ElseIf dataHBKX0201.PropChkJtiFlg.Checked = True Then
                    'ソート対象列をソートする順番で指定
                    Si(0) = New SortInfo(NEW_DATA, True) '追加行判定(隠し列)
                    Si(1) = New SortInfo(HBKUSR_ID, True) 'ひびきユーザーマスター：ひびきユーザーID
                    '追加行判定(隠し列)、ひびきユーザーマスター：ひびきユーザーIDの昇順でソートする
                    .SortRows(0, .RowCount, Si)
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
    ''' 削除データ表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータも含めて取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDeleteData(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'ひびきユーザーマスター、所属マスターデータ取得(削除データ含む)
            If GetHBKUsrSZKMasterDeleteData(Adapter, Cn, dataHBKX0201) = False Then
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
    ''' ひびきユーザーマスター、所属マスターデータ取得(削除データ含む)
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスター、所属マスターデータを削除データを含めて取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetHBKUsrSZKMasterDeleteData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        Try
            'データクリア
            dataHBKX0201.PropDtHBKUsrMasterList.Clear()

            'ひびきユーザーマスター、所属マスターデータ取得(削除データ含む)

            'ひびきユーザーマスター、所属マスターデータ取得用SQLの作成・設定
            If sqlHBKX0201.SetSelectHBKUsrSZKMasterDeleteDataSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター、所属マスターデータ(削除データ含む)取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX0201.PropDtHBKUsrMasterList)

            'データの変更を確定
            dataHBKX0201.PropDtHBKUsrMasterList.AcceptChanges()


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
    ''' データ追加メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を元にデータを追加する処理を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function addDataMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '検索結果追加処理
        If SearchAddData(dataHBKX0201) = False Then
            Return False
        End If

        'ソート処理
        If Sort(dataHBKX0201) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果追加処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>子画面で検索したIDを追加する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchAddData(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnHBKUsrID As Boolean       'ひびきユーザーID存在確認用

        Try

            '子画面で取得したIDを追加していく(すでにそのIDがグループに存在する場合は追加しない)
            With dataHBKX0201
                For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1
                    For t As Integer = 0 To .PropVwHBKUsrMasterList.Sheets(0).RowCount - 1
                        '検索結果から選択したユーザーIDが既にグループに存在するかチェック
                        If .PropDtResultSub.Rows(i).Item(0) = .PropVwHBKUsrMasterList.Sheets(0).GetValue(t, HBKUSR_ID) Then
                            blnHBKUsrID = True
                            Exit For
                        End If

                    Next
                    '新規行追加
                    If blnHBKUsrID = False Then
                        '最終行に空行を1行追加
                        .PropVwHBKUsrMasterList.Sheets(0).Rows.Add(.PropVwHBKUsrMasterList.Sheets(0).RowCount, 1)
                        '追加行にユーザーIDと氏名とメールアドレスと新規データ(隠し項目)を追記する
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, HBKUSR_ID).Value _
                         = .PropDtResultSub.Rows(i).Item(0)
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, HBKUSR_NM).Value _
                         = .PropDtResultSub.Rows(i).Item(3)
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, HBKUSR_NM_KANA).Value _
                         = .PropDtResultSub.Rows(i).Item(9)
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, HBKUSR_MAILADD).Value _
                         = .PropDtResultSub.Rows(i).Item(4)
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, NEW_DATA).Value _
                        = DATA_NEW
                        'ソート用に隠し列に固定値を設定する
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, SORT_KAKUSHI).Value _
                        = "1"
                        'テキスト更新有無を0(無効)で設定する
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, TEXT_CHANGE_FLG).Value _
                        = TEXT_CHANGE_FLG_OFF
                        'チェックボックス更新有無を0(無効)で設定する
                        .PropVwHBKUsrMasterList.Sheets(0).Cells(.PropVwHBKUsrMasterList.Sheets(0).RowCount - 1, CHECK_CHANGE_FLG).Value _
                        = CHECK_CHANGE_FLG_OFF

                    End If

                    blnHBKUsrID = Nothing

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
    ''' 削除チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された行が新規追加行か確認する処理
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckDeleteDataMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '単数行選択チェック処理
        If CheckSingleRows(dataHBKX0201) = False Then
            Return False
        End If

        '新規追加チェック処理
        If CheckNewData(dataHBKX0201) = False Then
            Return False
        End If




        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 選択行削除メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された行を削除する処理
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function DeleteSelectDataMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '選択行削除実行
        If DeleteSelectData(dataHBKX0201) = False Then
            Return False
        End If
      

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function
    ''' <summary>
    ''' 単数行選択チェック処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたデータが1行かチェックする処理
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSingleRows(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言     
        Dim intSelectedRowFrom As Integer                '選択開始行番号
        Dim intSelectedRowTo As Integer                  '選択終了行番号

        Try

            With dataHBKX0201
                '選択開始行、終了行取得
                intSelectedRowFrom = .PropVwHBKUsrMasterList.Sheets(0).Models.Selection.AnchorRow
                intSelectedRowTo = .PropVwHBKUsrMasterList.Sheets(0).Models.Selection.LeadRow

                '[Add] 2012/10/30 s.yamaguchi START
                '行選択を明示的に行う。
                With .PropVwHBKUsrMasterList
                    .ActiveSheet.Models.Selection.AddSelection(.ActiveSheet.ActiveRowIndex, _
                                                               .ActiveSheet.ActiveColumnIndex, _
                                                               1, _
                                                               System.Math.Abs(intSelectedRowTo - intSelectedRowFrom) + 1)
                End With
                '[Add] 2012/10/30 s.yamaguchi END

                'ひびきユーザーの選択数が一件以外の時はエラーメッセージ出力
                If .PropVwHBKUsrMasterList.Sheets(0).SelectionCount <> 1 _
                    Or intSelectedRowTo - intSelectedRowFrom <> 0 _
                    Or .PropVwHBKUsrMasterList.Sheets(0).RowCount = 0 _
                    Or .PropVwHBKUsrMasterList.Sheets(0).Rows(.PropVwHBKUsrMasterList.Sheets(0).ActiveRowIndex).Visible = False Then
                    puErrMsg = X0201_E006
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
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 新規追加データチェック処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除しようとしている行が新規追加されたものかチェックする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckNewData(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intClickRow As Integer = Nothing             '選択された行


        Try
            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)
                '選択行のインデックス取得
                intClickRow = .ActiveRowIndex
                '選択された行が登録済みの場合はエラーメッセージ出力
                If .Cells(intClickRow, NEW_DATA).Value = DATA_OLD Then
                    puErrMsg = X0201_E005
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
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されている行の削除を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteSelectData(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intClickRow As Integer = Nothing             '選択された行

        Try

            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)
                '選択行のインデックス取得
                intClickRow = .ActiveRowIndex
                '選択された行が新規追加された行の場合削除
                If .Cells(intClickRow, NEW_DATA).Value = DATA_NEW Then
                    .Rows(intClickRow).Remove()
                End If
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
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された項目のチェックを行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力エラーチェック処理
        If InputErrorCheck(dataHBKX0201) = False Then
            Return False
        End If




        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力エラーチェック処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力エラーが存在しないかチェックする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InputErrorCheck(dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim intKanrisyaCount As Integer = 0     '管理者権限設定カウント

        Try

            'スプレッドの表示行だけ繰り返す。
            With dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0)
                For i As Integer = 0 To .RowCount - 1
                    '表示されているデータのみチェックを行う
                    If .Rows(i).Visible = True Then

                        '氏名必須チェック
                        If .GetValue(i, HBKUSR_NM) = Nothing Then
                            'エラーメッセージ設定
                            puErrMsg = X0201_E001
                            'フォーカス設定
                            .SetActiveCell(i, HBKUSR_NM)
                            Return False
                        ElseIf .GetValue(i, HBKUSR_NM).Trim = Nothing Then
                            'エラーメッセージ設定
                            puErrMsg = X0201_E001
                            'フォーカス設定
                            .SetActiveCell(i, HBKUSR_NM)
                            Return False
                        End If
                        '氏名カナ必須チェック
                        If .GetValue(i, HBKUSR_NM_KANA) = Nothing Then
                            'エラーメッセージ設定
                            puErrMsg = X0201_E002
                            'フォーカス設定
                            .SetActiveCell(i, HBKUSR_NM_KANA)
                            Return False
                        ElseIf .GetValue(i, HBKUSR_NM_KANA).Trim = Nothing Then
                            'エラーメッセージ設定
                            puErrMsg = X0201_E002
                            'フォーカス設定
                            .SetActiveCell(i, HBKUSR_NM_KANA)
                            Return False
                        End If

                        '[Del] 2012/09/25 m.ibuki 形式チェック削除START
                        ''メールアドレス形式チェック
                        'If .GetValue(i, HBKUSR_MAILADD) <> Nothing Then
                        '    'メールアドレス形式ではない場合、エラー
                        '    If commonLogicHBK.IsMailAddress(.GetValue(i, HBKUSR_MAILADD)) = False Then
                        '        'エラーメッセージ設定
                        '        puErrMsg = X0201_E003
                        '        'フォーカス設定
                        '        .SetActiveCell(i, HBKUSR_MAILADD)
                        '        Return False
                        '    End If

                        'End If
                        '[Del] 2012/09/25 m.ibuki 形式チェック削除END

                        'デフォルト存在チェック
                        'デフォルトにチェックが入っていない場合はチェックを行う
                        If .Cells(i, SZK_DEFAULT_FLG).Value = False Then
                            '現在チェックしている行のIDをセット
                            dataHBKX0201.PropStrInputCheckHBKUsrID = .Cells(i, HBKUSR_ID).Value
                            '所属マスターデータを取得する 
                            If DefaultCheck(dataHBKX0201) = False Then
                                Return False
                            End If

                            If dataHBKX0201.PropDtSZKMtb.Rows(0).Item(0) = 0 Then
                                puErrMsg = X0201_E004
                                'フォーカス設定
                                .SetActiveCell(i, SZK_DEFAULT_FLG)
                                Return False
                            End If

                        End If

                        '管理者チェック
                        If .Cells(i, SZK_USRGROUP_FLG).Value = True Then
                            intKanrisyaCount = intKanrisyaCount + 1
                        End If

                    End If
                Next

                If .RowCount > 0 Then
                    '管理者が一人も設定されていない場合
                    If intKanrisyaCount = 0 Then
                        puErrMsg = X0201_E007
                        'フォーカス設定
                        .SetActiveCell(0, SZK_USRGROUP_FLG)
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
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' デフォルトチェック処理処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトが設定されているかチェックする
    ''' <para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultCheck(ByRef dataHBKX0201 As DataHBKX0201) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtDefaultCheck As New DataTable

        Try

            '所属マスター/デフォルトフラグ有効数取得
            If sqlHBKX0201.SetSelectDefaultFlgSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスター/デフォルトフラグ有効数取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtDefaultCheck)


            '取得データをデータクラスにセット
            dataHBKX0201.PropDtSZKMtb = dtDefaultCheck


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
            dtDefaultCheck.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力されたデータを登録及び更新する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If GetSysDate(dataHBKX0201) = False Then
            Return False
        End If

        '登録/編集実行
        If RegisterEdit(dataHBKX0201) = False Then
            Return False
        End If

     


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付取得する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSysDate(ByRef dataHBKX0201 As DataHBKX0201) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try

            'システム日付取得
            If sqlHBKX0201.SetSelectSysDateSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX0201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
        Finally
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
            dtSysDate.Dispose()
        End Try
    End Function

    ''' <summary>
    '''　登録/編集処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録または編集処理を行う
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterEdit(ByRef dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)




        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            With dataHBKX0201

                'スプレッドのデータ件数分実行する

                For i As Integer = 0 To .PropVwHBKUsrMasterList.Sheets(0).RowCount - 1

                    '表示されているデータだけ実行する
                    If .PropVwHBKUsrMasterList.Sheets(0).Rows(i).Visible = True Then


                        '新規登録/更新に必要なデータをセットする
                        If SetData(dataHBKX0201, i) = False Then
                            'ロールバック
                            Tsx.Rollback()
                            Return False
                        End If


                        '追加ユーザーなら新規登録、登録済みユーザーなら編集を行う。
                        If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, NEW_DATA).Value = DATA_NEW Then

                            'ひびきユーザーが登録済みか確認する
                            'ひびきユーザー登録有無取得処理
                            If GetHBKUsrMaster(Cn, dataHBKX0201) = False Then
                                'ロールバック
                                Tsx.Rollback()
                                Return False
                            End If

                            '登録処理
                            If Register(Cn, dataHBKX0201, i) = False Then
                                'ロールバック
                                Tsx.Rollback()
                                Return False
                            End If

                        ElseIf .PropVwHBKUsrMasterList.Sheets(0).Cells(i, NEW_DATA).Value = DATA_OLD Then

                            '新規登録でなくかつ編集したデータだけ編集処理を行う    
                            If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, TEXT_CHANGE_FLG).Value = TEXT_CHANGE_FLG_ON _
                            Or .PropVwHBKUsrMasterList.Sheets(0).Cells(i, CHECK_CHANGE_FLG).Value = CHECK_CHANGE_FLG_ON Then


                                '編集処理
                                If Edit(Cn, dataHBKX0201) = False Then
                                    'ロールバック
                                    Tsx.Rollback()
                                    Return False
                                End If

                            End If

                        End If

                        'デフォルトを有効にした状態で新規登録/更新を行った場合はもとのデフォルトを無効にする
                        If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_DEFAULT_FLG).Value = True Then


                            '新規登録時または、更新のある行のみ実行
                            If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, NEW_DATA).Value = DATA_NEW Or _
                                .PropVwHBKUsrMasterList.Sheets(0).Cells(i, CHECK_CHANGE_FLG).Value = CHECK_CHANGE_FLG_ON Then                                
                                'デフォルト更新処理
                                If DefaultUpdate(Cn, dataHBKX0201) = False Then
                                    'ロールバック
                                    Tsx.Rollback()
                                    Return False
                                End If

                            End If

                        End If
                        '削除を有効にした状態で新規登録/更新を行った場合に所属マスターに
                        '当該ユーザーの有効データがなければひびきユーザーマスターも論理削除する
                        If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_JTI_FLG).Value = True Then


                            '更新のあった行またはひびきユーザーに新規追加した場合のみ実行する
                            If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, CHECK_CHANGE_FLG).Value = CHECK_CHANGE_FLG_ON Then

                                '所属マスター有効データ取得処理
                                If SelectSZKMasterYUKOData(Cn, dataHBKX0201) = False Then
                                    'ロールバック
                                    Tsx.Rollback()
                                    Return False
                                End If

                            ElseIf .PropdtHBKUsrMasterCheck IsNot Nothing Then
                                If .PropdtHBKUsrMasterCheck.Rows(0).Item(0) = 0 Then


                                    '所属マスター有効データ取得処理
                                    If SelectSZKMasterYUKOData(Cn, dataHBKX0201) = False Then
                                        'ロールバック
                                        Tsx.Rollback()
                                        Return False
                                    End If

                                End If
                            End If
                            'もし取得結果が0件ならばひびきユーザーマスターを論理削除する
                            If .PropDtSZKMtbYUKOCount IsNot Nothing Then
                                If .PropDtSZKMtbYUKOCount.Rows(0).Item(0) = 0 Then
                                    'ひびきユーザーマスター論理削除
                                    If HBKUsrMasterDelete(Cn, dataHBKX0201) = False Then
                                        'ロールバック
                                        Tsx.Rollback()
                                        Return False
                                    End If
                                End If

                            End If

                        End If

                            '削除を無効にした状態で新規登録/更新を行った場合にひびきユーザーマスターが無効であった場合、
                            '有効にする。
                            If .PropVwHBKUsrMasterList.Sheets(0).Cells(i, SZK_JTI_FLG).Value = False Then


                                'ひびきユーザーマスター無効チェック処理
                                If SelectHBKUsrMasterMUKOData(Cn, dataHBKX0201) = False Then
                                    'ロールバック
                                    Tsx.Rollback()
                                    Return False
                                End If
                                'もし取得結果が0件でなければひびきユーザーマスターの論理削除を解除する
                                If .PropDtHBKUsrMtbMUKO.Rows(0).Item(0) <> 0 Then
                                    'ひびきユーザーマスター論理削除解除
                                    If HBKUsrMasterDeleteKaijyo(Cn, dataHBKX0201) = False Then
                                        'ロールバック
                                        Tsx.Rollback()
                                        Return False
                                    End If
                                End If
                            End If
                        End If
                Next
            End With

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Tsx.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' データセット処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録/更新に必要なデータをセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetData(dataHBKX0201 As DataHBKX0201,
                             ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'プロパティに値をセットする。
            With dataHBKX0201
                'ユーザーID
                .PropStrHBKUsrID = .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, HBKUSR_ID).Value
                '氏名
                .PropStrHBKUsrNM = .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, HBKUSR_NM).Value
                '氏名カナ
                .PropStrHBKUsrNmKana = .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, HBKUSR_NM_KANA).Value
                'メールアドレス
                .PropStrHBKUsrMailAdd = .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, HBKUSR_MAILADD).Value
                '管理者
                If .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_USRGROUP_FLG).Value = False Then

                    .PropStrUsrGroupFlg = USR_GROUP_ADMIN_NORMAL
                ElseIf .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_USRGROUP_FLG).Value = True Then

                    .PropStrUsrGroupFlg = USR_GROUP_ADMIN_ADMIN
                End If
                'デフォルト
                If .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_DEFAULT_FLG).Value = False Then

                    .PropStrDefaultFlg = DEFAULT_OFF
                ElseIf .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_DEFAULT_FLG).Value = True Then

                    .PropStrDefaultFlg = DEFAULT_ON
                End If
                '削除
                If .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_JTI_FLG).Value = False Then

                    .PropStrJtiFlg = DATA_YUKO
                ElseIf .PropVwHBKUsrMasterList.Sheets(0).Cells(intIndex, SZK_JTI_FLG).Value = True Then

                    .PropStrJtiFlg = DATA_MUKO
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
            puErrMsg = HBK_E001 + ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスター新規登録有無確認処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスターに既に登録されていないか確認する
    ''' <para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetHBKUsrMaster(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtHBKMaster As New DataTable

        Try
            '所属マスター有効データ件数取得（SELECT）用SQLを作成
            If sqlHBKX0201.SetSelectHBKUsrMasterSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター登録有無取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtHBKMaster)

            'データセット
            dataHBKX0201.PropdtHBKUsrMasterCheck = dtHBKMaster


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
            Adapter.Dispose()
            dtHBKMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をひびきユーザーマスターテーブルと所属マスターテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201, _
                                  ByVal intRowCount As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ひびきユーザーマスターに登録されていない場合のみ新規登録を行う
            If dataHBKX0201.PropdtHBKUsrMasterCheck.Rows(0).Item(0) = 0 Then

                'ひびきユーザーマスター新規登録（INSERT）用SQLを作成
                If sqlHBKX0201.SetInsertHBKUsrMasterSql(Cmd, Cn, dataHBKX0201) = False Then
                    Return False
                End If


                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

            End If

            'ひびきユーザーマスターに登録されていてかつテキストに更新がある場合は更新を行う
            If dataHBKX0201.PropdtHBKUsrMasterCheck.Rows(0).Item(0) <> 0 _
                And dataHBKX0201.PropVwHBKUsrMasterList.Sheets(0).Cells(intRowCount, TEXT_CHANGE_FLG).Value = TEXT_CHANGE_FLG_ON Then

                'ひびきユーザーマスター更新（Update）用SQLを作成
                If sqlHBKX0201.SetUpdateHBKUsrMasterSql(Cmd, Cn, dataHBKX0201) = False Then
                    Return False
                End If


                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター更新", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

            End If

            '所属マスター新規登録(INSERT)用SQLを作成
            If sqlHBKX0201.SetInsertSZKMasterSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If


            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスター新規登録", Nothing, Cmd)

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
    ''' 編集処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でひびきユーザーマスターテーブルを編集（UPDATE）する
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Edit(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ひびきユーザーマスター編集（UPDATE）用SQLを作成
            If sqlHBKX0201.SetUpdateHBKUsrMasterSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター編集", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            '所属マスター編集(UPDATE)用SQLを作成
            If sqlHBKX0201.SetUpdateSZKMasterSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスター編集", Nothing, Cmd)

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
    ''' デフォルト更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトをオフにする処理
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DefaultUpdate(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '所属マスターデフォルト更新（UPDATE）用SQLを作成
            If sqlHBKX0201.SetUpdateSZKMasterDefaultSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスターデフォルト更新", Nothing, Cmd)

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
    ''' 所属マスター有効データ取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>所属マスター有効データが存在するかチェックする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSZKMasterYUKOData(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtSZKMasterYUKO As New DataTable

        Try
            '所属マスター有効データ件数取得（SELECT）用SQLを作成
            If sqlHBKX0201.SetSelectSZKMasterYUKOSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスター有効データ件数取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSZKMasterYUKO)

            'データセット
            dataHBKX0201.PropDtSZKMtbYUKOCount = dtSZKMasterYUKO


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
            Adapter.Dispose()
            dtSZKMasterYUKO.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスター論理削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスターを論理削除する処理
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function HBKUsrMasterDelete(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ひびきユーザーマスター論理削除（UPDATE）用SQLを作成
            If sqlHBKX0201.SetUpdateHBKUsrMasterDeleteSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター論理削除", Nothing, Cmd)

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
    ''' ひびきユーザーマスター無効確認処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスターが無効かチェックする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectHBKUsrMasterMUKOData(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtHBKUsrMasterMUKO As New DataTable

        Try
            'ひびきユーザーマスター無効確認（SELECT）用SQLを作成
            If sqlHBKX0201.SetSelectHBKUsrMasterMUKOSql(Adapter, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター無効データ取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtHBKUsrMasterMUKO)

            'データセット
            dataHBKX0201.PropDtHBKUsrMtbMUKO = dtHBKUsrMasterMUKO


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
            Adapter.Dispose()
            dtHBKUsrMasterMUKO.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスター論理削除解除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスターを論理削除解除する処理
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function HBKUsrMasterDeleteKaijyo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'ひびきユーザーマスター論理削除解除（UPDATE）用SQLを作成
            If sqlHBKX0201.SetUpdateHBKUsrMasterDeleteKaijyoSql(Cmd, Cn, dataHBKX0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスター論理削除解除", Nothing, Cmd)

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
    ''' スプレッド内チェックボックス変更時更新確定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内のチェックボックスを変更した場合に更新を確定させる処理
    ''' <para>作成情報：2012/08/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetModifidMain(ByRef dataHBKX0201 As DataHBKX0201, ByVal intRowIndex As Integer, ByVal intColumnIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新確定処理
        If SetModified(dataHBKX0201, intRowIndex, intColumnIndex) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' チェックボックス更新確定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックボックス変更時の更新処理を確定させる
    ''' <para>作成情報：2012/08/27 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetModified(ByRef dataHBKX0201 As DataHBKX0201, ByVal intRowIndex As Integer, ByVal intColumnIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言


        Try
            With dataHBKX0201

                'チェックボックスがクリックされた時だけ実行
                If intColumnIndex = SZK_USRGROUP_FLG Or SZK_DEFAULT_FLG Or SZK_JTI_FLG Then

                    'チェックボックス更新フラグ(隠し項目)を有効にする
                    .PropVwHBKUsrMasterList.Sheets(0).Cells(intRowIndex, CHECK_CHANGE_FLG).Value = CHECK_CHANGE_FLG_ON

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
    ''' スプレッド内テキストボックス変更時更新確定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザー登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内のテキストボックスを変更した場合に更新を確定させる処理
    ''' <para>作成情報：2012/09/12 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetTextChangeMain(ByRef dataHBKX0201 As DataHBKX0201, ByVal intRowIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '更新確定処理
        If SetTextChange(dataHBKX0201, intRowIndex) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' テキストボックス更新確定処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テキストボックス変更時の更新処理を確定させる
    ''' <para>作成情報：2012/09/12 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTextChange(ByRef dataHBKX0201 As DataHBKX0201, ByVal intRowIndex As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言


        Try
            With dataHBKX0201
                '
                .PropVwHBKUsrMasterList.Sheets(0).Cells(intRowIndex, TEXT_CHANGE_FLG).Value = TEXT_CHANGE_FLG_ON
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
    ''' 特権ログアウトログ出力メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LogoutLogMain(ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '特権ログアウトログ登録処理
            If LogoutLog(dataHBKX0201) = False Then
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
    ''' <param name="dataHBKX0201">[IN/OUT]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LogoutLog(ByVal dataHBKX0201 As DataHBKX0201) As Boolean

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
            If InsertLogoutLog(Tsx, Cn, dataHBKX0201) = False Then
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
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合にログアウトログを出力する
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertLogoutLog(ByRef Tsx As NpgsqlTransaction, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '特権ログインログ（INSERT）用SQLを作成
            If sqlHBKX0201.SetInsertSuperLoginLogSql(Cmd, Cn, dataHBKX0201) = False Then
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
