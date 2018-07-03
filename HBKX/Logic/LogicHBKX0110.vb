Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread
''' <summary>
''' 特権ユーザパスワード変更画面ロジッククラス
''' </summary>
''' <remarks>特権ユーザパスワード変更画面のロジックを定義したクラス
''' <para>作成情報：2012/08/30 y.ikushima
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0110

    'インスタンス生成
    Private sqlHBKX0110 As New SqlHBKX0110
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0110 As DataHBKX0110) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0110) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【共通】トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0110 As DataHBKX0110) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX0110

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtnChange)              '変更ボタン
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
    ''' 入力チェックメイン
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力チェックメイン処理
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputMain(ByRef dataHBKX0110 As DataHBKX0110) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面フォームの入力チェック
        If CheckInputValue(dataHBKX0110) = False Then
            Return False
        End If

        'DBの入力チェック
        If CheckDBValue(dataHBKX0110) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True
    End Function

    ''' <summary>
    ''' 画面フォーム入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面フォーム入力チェックを行う
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKX0110 As DataHBKX0110) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0110

                'IDの未入力チェック
                If .PropTxtID.Text = "" Then
                    puErrMsg = X0110_E001
                    Return False
                End If

                '現在パスワードの未入力チェック
                If .PropTxtPassNow.Text = "" Then
                    puErrMsg = X0110_E002
                    Return False
                End If

                '新しいパスワードの未入力チェック
                If .PropTxtPassNew.Text = "" Then
                    puErrMsg = X0110_E003
                    Return False
                End If

                '現在のパスワードと新しいパスワードの比較チェック
                If .PropTxtPassNow.Text = .PropTxtPassNew.Text Then
                    puErrMsg = X0110_E004
                    Return False
                End If

                '新しいパスワード[再入力]の未入力チェック
                If .PropTxtPassNewRe.Text = "" Then
                    puErrMsg = X0110_E005
                    Return False
                End If

                '新しいパスワードと新しいパスワード[再入力]の比較チェック
                If .PropTxtPassNew.Text <> .PropTxtPassNewRe.Text Then
                    puErrMsg = X0110_E006
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
    ''' 画面DB入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面に入力されたデータのDBチェックを行う
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckDBValue(ByRef dataHBKX0110 As DataHBKX0110) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            'コネクションを開く
            Cn.Open()

            '特権ユーザマスタ件数取得
            If sqlHBKX0110.SetSelectSuperUserSql(Adapter, Cn, dataHBKX0110) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "特権ユーザデータ件数取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResult)

            '1件の場合のみOK
            If dtResult.Rows(0).Item(0) <> 1 Then
                puErrMsg = X0110_E007
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
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            'リソースの開放
            Adapter.Dispose()
            Cn.Dispose()
            dtResult.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録のメイン処理
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SuperUsrUpdateMain(ByRef dataHBKX0110 As DataHBKX0110) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'システム日付取得
        If GetSysDate(dataHBKX0110) = False Then
            Return False
        End If

        '特権ユーザ更新処理
        If SuperUsrUpdate(dataHBKX0110) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' システム日付取得処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付取得する
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysDate(ByRef dataHBKX0110 As DataHBKX0110) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim dtSysDate As New DataTable

        Try

            'システム日付取得
            If sqlHBKX0110.SetSelectSysDateSql(Adapter, Cn, dataHBKX0110) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "システム日付取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスにサーバー日付をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKX0110.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    '''　削除解除処理
    ''' </summary>
    ''' <param name="dataHBKX0110">[IN/OUT]特権ユーザパスワード変更画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザを更新する
    ''' <para>作成情報：2012/08/30 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SuperUsrUpdate(ByRef dataHBKX0110 As DataHBKX0110) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション
        Dim Cmd As New NpgsqlCommand              'SQLコマンド

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            '特権ユーザ更新
            If sqlHBKX0110.SetUpdateSuprUserSql(Cmd, Cn, dataHBKX0110) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "特権ユーザーマスター更新", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Tsx.Dispose()
            Cmd.Dispose()
            Cn.Dispose()
        End Try

    End Function


End Class
