Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO

''' <summary>
''' 連携処理実施ロジッククラス
''' </summary>
''' <remarks>連携処理実施のロジッククラス
''' <para>作成情報：2012/09/13 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0211

    'インスタンス生成
    Public dataHBKC0211 As New DataHBKC0211
    Private sqlHBKC0211 As New SqlHBKC0211
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '[Add] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 START
    ''' <summary>
    ''' 連携処理待ちデータチェック処理
    ''' </summary>
    ''' <param name="dataHBKC0211">[IN/OUT]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>連携待ちが存在するかチェックする
    ''' <para>作成情報：2012/10/02 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function IncidentSMrenkeiCheckMain(ByRef dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '連携処理待ちデータ取得
        If GetInitData(dataHBKC0211) = False Then
            Return False
        End If

        '連携待ちが存在する場合、エラーとする
        If dataHBKC0211.PropDtIncidentSMtuti.Rows.Count > 0 Then
            If CLng(dataHBKC0211.PropDtIncidentSMtuti.Rows(0).Item(0)) > 0 Then
                'エラーメッセージ設定
                puErrMsg = C0211_E001
                Return False
            End If
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function
    '[Add] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 END

    ''' <summary>
    ''' 連携処理待ちデータ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0211">[IN/OUT]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>連携処理待ちデータを取得する
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'インシデントSM連携指示テーブル取得
            If GetIncidentSMrenkei(Adapter, Cn, dataHBKC0211) = False Then
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
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデントSM連携指示テーブルデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0211">[IN/OUT]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>連携処理待ちデータを取得する
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetIncidentSMrenkei(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIncidentSMrenkei As New DataTable

        Try
            'SQLの作成・設定
            If sqlHBKC0211.SetSelectIncidentSMrenkeiSql(Adapter, Cn, dataHBKC0211) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデントSM連携指示テーブル取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtIncidentSMrenkei)
            dataHBKC0211.PropDtIncidentSMtuti = dtIncidentSMrenkei

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
        Finally
            dtIncidentSMrenkei.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 連携処理実施メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0211">[IN/OUT]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ServiceManagerに連携するインシデント情報をセットする
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitMain(ByRef dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '[Del] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 START
        ''連携処理待ちデータ取得
        'If GetInitData(dataHBKC0211) = False Then
        '    Return False
        'End If

        ''連携待ちが存在する場合、エラーとする
        'If dataHBKC0211.PropDtIncidentSMtuti.Rows.Count > 0 Then
        '    If CLng(dataHBKC0211.PropDtIncidentSMtuti.Rows(0).Item(0)) > 0 Then
        '        'エラーメッセージ設定
        '        puErrMsg = C0211_E001
        '        Return False
        '    End If
        'End If
        '[Del] 2012/10/02 s.yamaguchi 連携処理中メッセージ出力タイミング修正対応 End

        'データ新規登録処理
        If InsertNewData(dataHBKC0211) = False Then
            Return False
        End If

        '登録完了メッセージ表示
        MsgBox(C0211_I001, MsgBoxStyle.Information, TITLE_INFO)

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKC0211">[IN/OUT]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>特権ユーザーログインしていた場合はログインログを出力する
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InsertNewData(ByVal dataHBKC0211 As DataHBKC0211) As Boolean

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

            '新規sql取得
            If GetNewSeq(Cn, dataHBKC0211) = False Then
                'ロールバック
                Tsx.Rollback()
                Return False
            End If

            'インシデントSM連携指示登録
            If setInsertIncidentSMrenkei(Tsx, Cn, dataHBKC0211) = False Then
                'ロールバック
                Tsx.Rollback()
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
    ''' 新規Seq取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0211">[IN]設置情報マスター登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したSeqを取得（SELECT）する
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewSeq(ByVal Cn As NpgsqlConnection, _
                                ByRef dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規Seq取得（SELECT）用SQLを作成
            If sqlHBKC0211.SetSelectNewSetBusyoCDAndSysDateSql(Adapter, Cn, dataHBKC0211) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規Seq取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKC0211.PropIntSeq = dtResult.Rows(0).Item("Seq")
                dataHBKC0211.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")
            Else
                '取得できなかったときはエラー
                puErrMsg = C0211_E002
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
            Adapter.Dispose()
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' インシデントSM連携指示登録処理
    ''' </summary>
    ''' <param name="Tsx">[IN/OUT]NpgsqlTransaction</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKC0211">[IN]連携処理実施Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデントSM連携指示を新規登録（INSERT）する
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function setInsertIncidentSMrenkei(ByRef Tsx As NpgsqlTransaction, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            'インシデントSM連携指示（INSERT）用SQLを作成
            If sqlHBKC0211.SetInsertIncidentSMrenkeiSql(Cmd, Cn, dataHBKC0211) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデントSM連携指示テーブル登録", Nothing, Cmd)

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
