Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Net

''' <summary>
''' 最新連携情報表示画面ロジッククラス
''' </summary>
''' <remarks>最新連携情報表示画面のロジックを定義したクラス
''' <para>作成情報：2012/09/12 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0210

    'インスタンス作成
    Private sqlHBKC0210 As New SqlHBKC0210
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0210">[IN/OUT]最新連携情報表示画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示設定を行う
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームコントロール設定
        If InitFormControl(dataHBKC0210) = False Then
            Return False
        End If

        '初期表示データ取得
        If GetInitData(dataHBKC0210) = False Then
            Return False
        End If

        '初期表示データ設定
        If SetInitDataToControl(dataHBKC0210) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームコントロール設定処理
    ''' </summary>
    ''' <param name="dataHBKC0210">[IN/OUT]最新連携情報表示画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'オブジェクトの活性非活性設定
            With dataHBKC0210.PropGrpLoginUser

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
    ''' 初期表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0210">[IN/OUT]最新連携情報表示画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>最新連携情報表示画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'インシデントSM通知テーブル取得
            If GetIncidentSMtuti(Adapter, Cn, dataHBKC0210) = False Then
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
    ''' インシデントSM通知テーブルデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0210">[IN/OUT]最新連携情報表示画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>インシデントSM通知テーブルデータを取得する
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetIncidentSMtuti(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtIncidentSMtuti As New DataTable

        Try
            'SQLの作成・設定
            If sqlHBKC0210.SetSelectIncidentSMtutiSql(Adapter, Cn, dataHBKC0210) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "インシデントSM通知テーブル取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtIncidentSMtuti)
            dataHBKC0210.PropDtIncidentSMtuti = dtIncidentSMtuti

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
            dtIncidentSMtuti.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示データ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0210">[IN/OUT]最新連携情報表示画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKC0210 As DataHBKC0210) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0210

                If .PropDtIncidentSMtuti.Rows.Count > 0 Then
                    .PropTxtSMNmb.Text = .PropDtIncidentSMtuti.Rows(0).Item("SMNmb").ToString
                    .PropTxtIncNmb.Text = .PropDtIncidentSMtuti.Rows(0).Item("IncNmb").ToString
                    .PropTxtRenkeiKbn.Text = .PropDtIncidentSMtuti.Rows(0).Item("RenkeiKbn").ToString
                    .PropTxtRenkeiDT.Text = .PropDtIncidentSMtuti.Rows(0).Item("RenkeiDT").ToString
                    .PropTxtIncState.Text = .PropDtIncidentSMtuti.Rows(0).Item("IncState").ToString

                    .PropTxtTitle.Text = .PropDtIncidentSMtuti.Rows(0).Item("Title").ToString
                    .PropTxtUkeNaiyo.Text = .PropDtIncidentSMtuti.Rows(0).Item("UkeNaiyo").ToString
                    .PropTxtGenin.Text = .PropDtIncidentSMtuti.Rows(0).Item("Genin").ToString
                    .PropTxtZanteisyotiNaiyo.Text = .PropDtIncidentSMtuti.Rows(0).Item("ZanteisyotiNaiyo").ToString
                    .PropTxtSolution.Text = .PropDtIncidentSMtuti.Rows(0).Item("Solution").ToString
                    .PropTxtUsrBusyoNM.Text = .PropDtIncidentSMtuti.Rows(0).Item("UsrBusyoNM").ToString
                    .PropTxtIraiUsr.Text = .PropDtIncidentSMtuti.Rows(0).Item("IraiUsr").ToString
                    .PropTxtTel.Text = .PropDtIncidentSMtuti.Rows(0).Item("Tel").ToString
                    .PropTxtMailAdd.Text = .PropDtIncidentSMtuti.Rows(0).Item("MailAdd").ToString
                    .PropTxtKind.Text = .PropDtIncidentSMtuti.Rows(0).Item("Kind").ToString
                    .PropTxtCategory.Text = .PropDtIncidentSMtuti.Rows(0).Item("Category").ToString
                    .PropTxtSubCategory.Text = .PropDtIncidentSMtuti.Rows(0).Item("SubCategory").ToString
                    .PropTxtImpact.Text = .PropDtIncidentSMtuti.Rows(0).Item("Impact").ToString
                    .PropTxtUsrSyutiClass.Text = .PropDtIncidentSMtuti.Rows(0).Item("UsrSyutiClass").ToString

                    .PropTxtBikoS1.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoS1").ToString
                    .PropTxtBikoS2.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoS2").ToString
                    .PropTxtBikoM1.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoM1").ToString
                    .PropTxtBikoM2.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoM2").ToString
                    .PropTxtBikoL1.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoL1").ToString
                    .PropTxtBikoL2.Text = .PropDtIncidentSMtuti.Rows(0).Item("BikoL2").ToString
                    .PropTxtYobiDT1.Text = .PropDtIncidentSMtuti.Rows(0).Item("YobiDT1").ToString
                    .PropTxtYobiDT2.Text = .PropDtIncidentSMtuti.Rows(0).Item("YobiDT2").ToString
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

End Class
