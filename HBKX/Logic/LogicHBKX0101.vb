Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 特権ユーザーログイン（ひびきユーザー登録）画面Logicクラス
''' </summary>
''' <remarks>特権ユーザーログイン（ひびきユーザー登録）画面のロジックを定義する
''' <para>作成情報：2012/08/30 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class LogicHBKX0101

    Private sqlHBKX0101 As New SqlHBKX0101          'SQLクラス
    Private commonLogic As New CommonLogic          '共通ロジッククラス
    Private commonLogicHBK As New CommonLogicHBK    'HBK共通ロジッククラス

    ''' <summary>
    ''' 入力エラーチェック処理
    ''' </summary>
    ''' <paramref name="dataHBKA0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</paramref>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>特権ユーザーログイン（ひびきユーザー登録）画面の入力チェックを行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputForm(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ID入力チェック
        If dataHBKX0101.PropTxtUserId.Text = "" Then
            puErrMsg = X0101_E001
            Return False
        End If

        'パスワード入力チェック
        If dataHBKX0101.PropTxtPassword.Text = "" Then
            puErrMsg = X0101_E002
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        Return True

    End Function

    '**********************************************************
    '処理をまとめるかも
    ''' <summary>
    ''' ログイン処理
    ''' </summary>
    ''' <paramref name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</paramref>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>ログインを行い、各情報の取得および格納を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LoginMain(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ログイン処理
        If Login(dataHBKX0101) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

        '+++++++++++++++++++++++++++++++++++++++++++
        ''開始ログ出力
        'commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        'Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        'Dim Table As New DataTable()                'テーブル

        'Try
        '    'コネクションを開く
        '    Cn.Open()

        '    'エラーメッセージ初期化
        '    puErrMsg = System.String.Empty

        '    '所属マスターの取得
        '    If GetSzkMtbData(Cn, dataHBKX0101) = False Then
        '        Return False
        '    End If

        '    '該当するグループがマスターに存在したか
        '    If dataHBKX0101.PropDtSzkMasta.Rows.Count <= 0 Then
        '        'ユーザグループ権限なし
        '        puErrMsg = X0101_E004
        '        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
        '        Return False
        '    End If

        '    '終了ログ出力
        '    commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '    '正常処理終了
        '    Return True

        'Catch ex As Exception
        '    '例外発生
        '    commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
        '    puErrMsg = HBK_E001 & puErrMsg
        '    Return False
        'Finally
        '    If Cn IsNot Nothing Then
        '        Cn.Close()
        '    End If
        '    Cn.Dispose()
        '    Adapter.Dispose()
        '    Table.Dispose()
        'End Try
        '+++++++++++++++++++++++++++++++++++++++++++

    End Function

    ''' <summary>
    ''' ログイン処理
    ''' </summary>
    ''' <paramref name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</paramref>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>選択されたラジオボタン毎にログインを行い、各情報の取得および格納を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Login(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            If dataHBKX0101.PropRdoGruopUsr.Checked = True Then

                'グループユーザ管理者
                If LoginGruopUsr(dataHBKX0101) = False Then
                    Return False
                End If

            ElseIf dataHBKX0101.PropRdoGruopMaster.Checked = True Then

                'グループマスター登録ユーザー
                If LoginGruopMaster(dataHBKX0101) = False Then
                    Return False
                End If

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
    ''' ログイン処理(グループユーザ管理者)
    ''' </summary>
    ''' <paramref name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</paramref>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>選択されたラジオボタン毎にログインを行い、各情報の取得および格納を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LoginGruopUsr(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            'エラーメッセージ初期化
            puErrMsg = System.String.Empty

            '所属マスターの取得
            If GetSzkMtbData(Cn, dataHBKX0101) = False Then
                Return False
            End If

            '該当するグループがマスターに存在したか
            If dataHBKX0101.PropDtSzkMasta.Rows.Count <= 0 Then
                'ユーザグループ権限なし
                puErrMsg = X0101_E004
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & puErrMsg
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ログイン処理(グループマスター登録ユーザー)
    ''' </summary>
    ''' <paramref name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</paramref>
    ''' <returns>boolean 終了状況    true  正常  false  異常</returns>
    ''' <remarks>選択されたラジオボタン毎にログインを行い、各情報の取得および格納を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LoginGruopMaster(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try
            'コネクションを開く
            Cn.Open()

            'エラーメッセージ初期化
            puErrMsg = System.String.Empty

            '特権ユーザーの取得
            If GetSuperUserData(Cn, dataHBKX0101) = False Then
                Return False
            End If

            '該当する特権ユーザーがマスターに存在したか
            If dataHBKX0101.PropDtSuperUsrMasta.Rows.Count <= 0 Then
                '該当ユーザIDなし
                puErrMsg = X0101_E003
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                Return False
            End If

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & puErrMsg
            Return False
        Finally
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 所属マスタデータ取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターから該当データを取得する。
    ''' <para>作成情報：2012/10/15 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Private Function GetSzkMtbData(ByVal Cn As NpgsqlConnection, ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            '所属マスターの取得SQLの作成・設定
            If sqlHBKX0101.SetSelectSzkMtbSql(Adapter, Cn, dataHBKX0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "所属マスター検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            dataHBKX0101.PropDtSzkMasta = Table

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 特権ユーザーデータ取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ユーザーマスターから該当IDを取得する。
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Private Function GetSuperUserData(ByVal Cn As NpgsqlConnection, ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力()
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Dim Table As New DataTable()                'テーブル

        Try

            '特権ユーザー情報の取得SQLの作成・設定
            If sqlHBKX0101.SetSelectSuperUserSql(Adapter, Cn, dataHBKX0101) = False Then
                Return False
            End If

            'SQLログ
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "特権ユーザーマスター検索", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(Table)
            dataHBKX0101.PropDtSuperUsrMasta = Table

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            '例外発生
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Adapter.Dispose()
            Table.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' ラジオボックス選択時活性、非活性化メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ラジオボタンが選択された場合の活性、非活性を行う
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function rdoAbleMain(ByRef dataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKX0101

            'グループユーザー管理者が選択された場合
            If .PropRdoGruopUsr.Checked = True Then
                .PropTxtUserId.Text = ""
                .PropTxtUserId.Enabled = False
                .PropTxtPassword.Text = ""
                .PropTxtPassword.Enabled = False

                'グループマスター登録ユーザーが選択された場合
            ElseIf .PropRdoGruopMaster.Checked = True Then
                .PropTxtUserId.Enabled = True
                .PropTxtPassword.Enabled = True
                .PropTxtUserId.Select()
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 特権ログインログ出力処理
    ''' </summary>
    ''' <param name="DataHBKX0101">[IN/OUT]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログインログを出力する
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputLogLogin(ByVal DataHBKX0101 As DataHBKX0101) As Boolean

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
            If InsertSuperLoginLog(Tsx, Cn, DataHBKX0101) = False Then
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
            Tsx.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 特権ログインログ登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="DataHBKX0101">[IN]特権ユーザーログイン（ひびきユーザー登録）画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合にログインログを出力する
    ''' <para>作成情報：2012/08/30 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertSuperLoginLog(ByRef Tsx As NpgsqlTransaction, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal DataHBKX0101 As DataHBKX0101) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '特権ログインログ（INSERT）用SQLを作成
            If sqlHBKX0101.SetInsertSuperLoginLogSql(Cmd, Cn, DataHBKX0101) = False Then
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
