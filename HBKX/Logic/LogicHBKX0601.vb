Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' メールテンプレートマスター一覧画面ロジッククラス
''' </summary>
''' <remarks>メールテンプレートマスター一覧画面ロジッククラス
''' <para>作成情報：2012/08/10 s.tsuruta
''' <p>改定情報：2012/08/13 k.ueda</p>
''' </para></remarks>
Public Class LogicHBKX0601

    'インスタンス生成
    Private sqlHBKX00601 As New SqlHBKX0601
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================
    'メールテンプレートマスター一覧
    Public Const MAIL_TEMP_NMB As Integer = 0          '番号
    Public Const MAIL_TEMP_NM As Integer = 1           'テンプレート名
    Public Const MAIL_TEMP_PROCESS_KBN As Integer = 2  '種類
    Public Const MAIL_TEMP_JTIFLG As Integer = 3       '有効/無効

    '定数宣言

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メールテンプレートマスター一覧画面に初期データをセットする
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレット表示テーブル作成
        If CreateDataTableForVw(dataHBKX0601) = False Then
            Return False
        End If


        'データ取得処理
        If GetData(dataHBKX0601) = False Then
            Return False
        End If

        'データ設定処理
        If SetData(dataHBKX0601) = False Then
            Return False
        End If


        'スプレッド詳細設定
        If SetSpread(dataHBKX0601) = False Then
            Return False
        End If

        'コントロール設定処理
        If InitFormControl(dataHBKX0601) = False Then
            Return False
        End If

        '背景色変更処理
        If ChangeSpreadColor(dataHBKX0601) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' チェックボックス変化時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに検索結果を表示する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報：2012/09/06 k.ueda</p>
    ''' </para></remarks>
    Public Function CheckBoxMain(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

       
        'スプレッド詳細設定
        If SetSpread(dataHBKX0601) = False Then
            Return False
        End If

        'ラベル設定(検索件数設定)
        If SetlblControl(dataHBKX0601) = False Then
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
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMailTemplateMasta As New DataTable       '原因リンク用データテーブル


        Try

            '原因リンク用テーブル作成
            With dtMailTemplateMasta
                .Columns.Add("TemplateNmb", Type.GetType("System.Int32"))              'テンプレート番号
                .Columns.Add("TemplateNM", Type.GetType("System.String"))               'テンプレート名
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))               'プロセス区分
                .Columns.Add("JtiFlg", Type.GetType("System.String"))                   '削除フラグ
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            dataHBKX0601.PropDtMailTemplateMasta = dtMailTemplateMasta                    'スプレッド表示用：メールテンプレートマスター一覧

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
            dtMailTemplateMasta.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' コントロール設定処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルからデータを取得する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : 2012/08/13 k.ueda</p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言


        Try

            'ラベル設定
            If SetlblControl(dataHBKX0601) = False Then
                Return False
            End If

            'ヘッダ設定
            If SetHearderControl(dataHBKX0601) = False Then
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
    ''' ラベル設定処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルからデータを取得する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : 2012/08/13 k.ueda</p>
    ''' </para></remarks>
    Private Function SetlblControl(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX0601
                '表示されている件数分カウントする
                For i = 0 To .PropVwMailTmp.Sheets(0).RowCount - 1
                    If .PropVwMailTmp.Sheets(0).Rows(i).Visible = True Then
                        intCount += 1
                    End If
                Next

                '検索件数をセット
                .PropLblItemCount.Text = intCount & "件"

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
    ''' ヘッダ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルからデータを取得する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 :2012/08/13 k.ueda </p>
    ''' </para></remarks>
    Private Function SetHearderControl(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)




        Try
            With dataHBKX0601.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

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
    ''' データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルからデータを取得する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetData(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ


        Try

            'コネクションを開く
            Cn.Open()

            'メールテンプレートマスタデータ取得
            If GetMailTemplateMastaData(Adapter, Cn, dataHBKX0601) = False Then
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
            Cn.Dispose()
            Adapter.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' メールテンプレートマスタデータ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メールテンプレートマスタテーブルからデータを取得する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMailTemplateMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try


            '取得用SQLの作成・設定
            If sqlHBKX00601.SetSelectMailTemplateMastaAllSql(Adapter, Cn, dataHBKX0601) = False Then
                Return False
            End If
            

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレートマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX0601.PropDtMailTemplateMasta)

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
    ''' データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>取得したデータを設定する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : 2012/08/13 k.ueda</p>
    ''' </para></remarks>
    Private Function SetData(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言


        Try
            With dataHBKX0601.PropVwMailTmp.Sheets(0)

                'スプレット代入処理
                .DataSource = dataHBKX0601.PropDtMailTemplateMasta
                .Columns(MAIL_TEMP_NMB).DataField = "TemplateNmb"
                .Columns(MAIL_TEMP_NM).DataField = "TemplateNM"
                .Columns(MAIL_TEMP_PROCESS_KBN).DataField = "ProcessKbn"
                .Columns(MAIL_TEMP_JTIFLG).DataField = "JtiFlg"


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
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド表示の詳細設定を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSpread(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ表示非表示設定処理
            If DataVisible(dataHBKX0601) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0601) = False Then
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
    ''' データ表示非表示設定処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの表示非表示を設定する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DataVisible(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0601.PropVwMailTmp.Sheets(0)

                'チェックボックスの状態で表示状態を変更する
                For i = 0 To .RowCount - 1
                    'チェックボックスにチェックが入ってなくかつ、データが無効の場合は表示しない
                    If dataHBKX0601.PropChkJtiFlg.Checked = False _
                        And .Cells(i, MAIL_TEMP_JTIFLG).Value = DATA_MUKO_NM Then
                        .Rows(i).Visible = False
                        'それ以外は表示する
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
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>行ヘッダを設定する
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRowHearder(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 1    '再設定する行番号

        Try

            '行ヘッダを再設定する
            With dataHBKX0601.PropVwMailTmp.Sheets(0)

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
    ''' スプレット背景色変更処理 
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除行をグレー表示する
    ''' <para>作成情報：2012/08/10 s.tsuruta
    ''' <p>改訂情報 : 2012/08/13 k.ueda</p>
    ''' </para></remarks>
    Private Function ChangeSpreadColor(ByRef dataHBKX0601 As DataHBKX0601) As Boolean


        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言


        Try

            With dataHBKX0601

                For i = 0 To .PropVwMailTmp.Sheets(0).RowCount - 1
                    If .PropVwMailTmp.Sheets(0).GetValue(i, MAIL_TEMP_JTIFLG) = DATA_MUKO_NM Then
                        '有効/無効が無効の場合はグレーに変更
                        .PropVwMailTmp.Sheets(0).Rows(i).BackColor = Color.Gray
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
    ''' 行ヘッダ再設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>列ヘッダがクリックされた場合に、行ヘッダの再設定を行う
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetRowHeaderMain(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '行ヘッダ設定処理
        If SetRowHearder(dataHBKX0601) = False Then
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
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'デフォルトソートを行う
            If DefaultSort(dataHBKX0601) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0601) = False Then
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
    ''' <param name="dataHBKX0601">[IN/OUT]メールテンプレートマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/09/06 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSort(ByRef dataHBKX0601 As DataHBKX0601) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKX0601.PropVwMailTmp.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(MAIL_TEMP_NMB, True) 'メールテンプレートマスター.テンプレート番号

                'メールテンプレートマスター.テンプレート番号の昇順でソートする
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


End Class
