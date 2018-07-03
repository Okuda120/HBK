Imports Common
Imports CommonHBK
Imports Npgsql


''' <summary>
''' メールテンプレート選択画面ロジッククラス
''' </summary>
''' <remarks>メールテンプレート選択画面のロジックを定義したクラス
''' <para>作成情報：2012/07/23 t.fukuo
''' <p>改定情報：2012/08/29 t.fukuo 最終お知らせ日更新対応</p>
''' </para></remarks>
Public Class LogicHBKZ1001

    'インスタンス作成
    Private sqlHBKZ1001 As New SqlHBKZ1001
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK


    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1001">[IN/OUT]メールテンプレート選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '初期表示用データ取得
        If GetInitData(dataHBKZ1001) = False Then
            Return False
        End If

        'フォームデータ設定
        If SetDataToForm(dataHBKZ1001) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 選択テンプレート判定メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1001">[IN/OUT]メールテンプレート選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたテンプレートに応じてフラグを設定する
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckSelectedTemplateMain(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択されたテンプレートに応じてフラグを設定
        If CheckSelectedTemplate(dataHBKZ1001) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 対象機器ロックメイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1001">[IN/OUT]メールテンプレート選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象機器のロック状況をチェックし、全て未ロックであれば対象機器をロックする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockCIKikiMain(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '対象機器のロック状況をチェックする
        If CheckKikiBeLocked(dataHBKZ1001) = False Then
            Return False
        End If

        '対象機器をロックする
        If LockCIKiki(dataHBKZ1001) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' メール作成メイン処理
    ''' </summary>
    ''' <param name="dataHBKZ1001">[IN/OUT]メールテンプレート選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択されたテンプレートデータを戻り値にセットする
    ''' <para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateReturnDataMain(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '戻り値作成
        If CreateReturnData(dataHBKZ1001) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKZ1001">[IN/OUT]メールテンプレート選択画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>設定時刻に前画面からのパラメータを設定する
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)    'コネクション
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'メールテンプレートマスタ取得
            If GetMailTemplateMasta(Adapter, Cn, dataHBKZ1001) = False Then
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
    ''' メールテンプレートマスタ取得
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>メールテンプレートマスタデータを取得する
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMailTemplateMasta(ByVal Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMailTemplate As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKZ1001.SetSelectMailTemplateMastaSql(Adapter, Cn, dataHBKZ1001) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレートマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMailTemplate)

            ''データが取得できなかった場合、エラー
            'If dtMailTemplate.Rows.Count = 0 Then
            '    puErrMsg = Z1001_E001
            '    Return False
            'End If

            '取得データをデータクラスにセット
            dataHBKZ1001.PropDtMailTemplateMasta = dtMailTemplate

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
            dtMailTemplate.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' フォームデータ設定処理
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' </summary>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>取得データをフォームに設定する
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToForm(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1001

                'グループ名ラベル
                .PropLblGroupNM.Text = .PropStrGroupNM

                'メールテンプレートの設定が0件だったら
                If .PropDtMailTemplateMasta.Rows.Count = 0 Then
                    'メール作成ボタン(非活性化)
                    .PropbtnCreateMail.Enabled = False
                    'エラーメッセージをセットする
                    puErrMsg = Z1001_E001
                    Return False
                Else
                    'メール作成ボタン(活性化)
                    .PropbtnCreateMail.Enabled = True

                    'コンボボックス作成
                    If CreateCmbBox(dataHBKZ1001) = False Then
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
    ''' コンボボックス作成処理
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>取得マスタデータを基にコンボボックスを作成する
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmbBox(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1001

                'メールテンプレートコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtMailTemplateMasta, .PropCmbMailTemplate, False) = False Then
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
    ''' 選択メールテンプレート判定処理
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>期限切れお知らせ用メールテンプレートが選択されている場合、フラグをセットする
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSelectedTemplate(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKZ1001

                'プロセス区分がインシデントで期限切れ条件CI種別がブランク（スペース削除）でない場合、
                '期限切れお知らせ用メールテンプレートフラグON
                If .PropStrProcessKbn = PROCESS_TYPE_INCIDENT AndAlso _
                   .PropStrKigenCondCIKbnCD.ToString().Trim() <> "" Then
                    .PropBlnIsKigengireTemplate = True
                Else
                    .PropBlnIsKigengireTemplate = False
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
    ''' 対象機器ロック状況チェック
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>対象機器がロックされているかチェックし、1件でもロックされていたらエラーメッセージを返す
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckKikiBeLocked(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean                  'ロックフラグ
        Dim strBeLockedMsg As String = ""           'ロック時メッセージ
        Dim dtResult As DataTable = Nothing         'ロック情報テーブル

        Try
            With dataHBKZ1001

                '対象機器ロック状況チェック
                If commonLogicHBK.CheckDataBeLocked(.PropVwKiki, .PropIntColCINmb, blnBeLocked, strBeLockedMsg, dtResult) = False Then
                    Return False
                End If

                'ロックされている場合、メッセージ用変数にロックメッセージをセットし処理終了
                If blnBeLocked = True Then
                    puErrMsg = strBeLockedMsg
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
        Finally
            If dtResult IsNot Nothing Then
                dtResult.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 対象機器ロック
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>対象機器がロックされているかチェックし、1件でもロックされていたらエラーメッセージを返す
    ''' <para>作成情報：2012/08/29 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function LockCIKiki(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtResult As DataTable = Nothing         'ロック情報テーブル

        Try
            With dataHBKZ1001

                '対象機器ロック
                If commonLogicHBK.LockCIInfo(.PropVwKiki, .PropIntColCINmb, dtResult) = False Then
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
        Finally
            If dtResult IsNot Nothing Then
                dtResult.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' 戻り値用メールテンプレートデータ作成処理
    ''' </summary>
    ''' <paramref name="dataHBKZ1001">[IN/OUT]メールテンプレート選択データクラス</paramref>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>選択されたテンプレートデータを戻り値にセットする
    ''' <para>作成情報：2012/07/24 t.fukuo
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateReturnData(ByRef dataHBKZ1001 As DataHBKZ1001) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtReturnData As DataTable

        Try
            With dataHBKZ1001

                'マスタデータの構造を戻り値用テーブルにコピー
                dtReturnData = .PropDtMailTemplateMasta.Clone()

                '選択されたテンプレートマスタデータを取得
                Dim rowSelected = From row In .PropDtMailTemplateMasta
                                  Where row.Item("ID") = .PropCmbMailTemplate.SelectedValue

                '戻り値用テーブルにデータをコピー
                For Each row In rowSelected
                    dtReturnData.ImportRow(row)
                Next

                'テーブルの変更をコミット
                dtReturnData.AcceptChanges()

                'データクラスに作成テーブルをセット
                .PropDtReturnData = dtReturnData

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

End Class
