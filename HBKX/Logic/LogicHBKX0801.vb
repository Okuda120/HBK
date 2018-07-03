Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 並び順登録画面ロジッククラス
''' </summary>
''' <remarks>並び順登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/16 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0801

    'インスタンス生成
    Private sqlHBKX0801 As New SqlHBKX0801

    'Public定数宣言==============================================


    '並び順登録列番号
    Public Const SORT_SORT As Integer = 0                 '表示順
    Public Const SORT_CORD As Integer = 1                 'コード
    Public Const SORT_NM As Integer = 2                   '名称
    Public Const SORT_JTI_FLG As Integer = 3              '削除フラグ(隠し項目)



    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKX0801) = False Then
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
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKX0801

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
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>並び順登録画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        'フォームオブジェクト設定
        If SetFormObj(dataHBKX0801) = False Then
            Return False
        End If


        'スプレッド用データテーブル作成
        If CreateDataTableForVw(dataHBKX0801) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX0801) = False Then
            Return False
        End If


        '初期表示用データ設定
        If SetInitData(dataHBKX0801) = False Then
            Return False
        End If

       
        'スプレッド隠し項目設定処理
        If Setvisible(dataHBKX0801) = False Then
            Return False
        End If

        ''グループマスターに対する処理の時だけ行う
        'If dataHBKX0801.PropStrTableNM = SORT_GROUP_MTB Then
        'グループマスターまたはメールテンプレートマスターに対する処理の時だけ行う
        If dataHBKX0801.PropStrTableNM = SORT_GROUP_MTB Or dataHBKX0801.PropStrTableNM = SORT_MAILTEMP_MTB Then
            '出力結果背景色変更処理
            If ChangeColor(dataHBKX0801) = False Then
                Return False
            End If
        End If

        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '項目非活性処理
            If ChangeEnable(dataHBKX0801) = False Then
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
    ''' 項目非活性化処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>項目を非活性化する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeEnable(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0801.PropGrpLoginUser

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
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '変更する並び順がグループマスターかCI共通かで処理を分ける
            With dataHBKX0801
                If .PropStrTableNM = SORT_GROUP_MTB Then

                    'グループマスタースプレッド用データテーブル作成処理
                    If CreateGroupMasterDataTableForVw(dataHBKX0801) = False Then
                        Return False
                    End If

                ElseIf .PropStrTableNM = SORT_CI_INFO_TB Then

                    'CI共通情報スプレッド用データテーブル作成処理
                    If CreateCIInfoDataTableForVw(dataHBKX0801) = False Then
                        Return False
                    End If

                ElseIf .PropStrTableNM = SORT_MAILTEMP_MTB Then

                    'メールテンプレートマスタースプレッド用データテーブル作成処理
                    If CreateMailTempMasterDataTableForVw(dataHBKX0801) = False Then
                        Return False
                    End If

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
    ''' グループマスタースプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateGroupMasterDataTableForVw(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGrpMtb As New DataTable   'グループマスター初期表示用データテーブル

        Try

            'グループマスター初期表示用用テーブル作成
            With dtGrpMtb
                .Columns.Add("Sort", Type.GetType("System.Double"))                  '表示順
                .Columns.Add("GroupCD", Type.GetType("System.String"))               'グループコード
                .Columns.Add("GroupNM", Type.GetType("System.String"))               'グループ名
                .Columns.Add("JtiFlg", Type.GetType("System.String"))                '削除フラグ(隠し項目)
                
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX0801
                .PropDtSortList = dtGrpMtb                                         'スプレッド表示用：並び順一覧

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
            dtGrpMtb.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCIInfoDataTableForVw(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtCIInfo As New DataTable   'CI共通情報初期表示用データテーブル

        Try

            'CI共通情報初期表示用用テーブル作成
            With dtCIInfo
                .Columns.Add("Sort", Type.GetType("System.Double"))                 '並び順
                .Columns.Add("CINmb", Type.GetType("System.Int32"))                 'CI番号
                .Columns.Add("CINM", Type.GetType("System.String"))                 '名称
                    'テーブルの変更を確定
                    .AcceptChanges()
                End With

            'データクラスに作成テーブルを格納
            With dataHBKX0801
                .PropDtSortList = dtCIInfo                                              'スプレッド表示用：並び順一覧

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
            dtCIInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' メールテンプレートマスタースプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateMailTempMasterDataTableForVw(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMTmpMtb As New DataTable   'メールテンプレートマスター初期表示用データテーブル

        Try

            'メールテンプレートマスター初期表示用テーブル作成
            With dtMTmpMtb
                .Columns.Add("Sort", Type.GetType("System.Double"))                 '表示順
                .Columns.Add("TemplateNmb", Type.GetType("System.String"))          'テンプレート番号
                .Columns.Add("TemplateNM", Type.GetType("System.String"))           'テンプレート名
                .Columns.Add("JtiFlg", Type.GetType("System.String"))               '削除フラグ(隠し項目)

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX0801
                .PropDtSortList = dtMTmpMtb                                           'スプレッド表示用：並び順一覧

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
            dtMTmpMtb.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '変更する並び順がグループマスターかCI共通かで処理を分ける
            With dataHBKX0801
                If .PropStrTableNM = SORT_GROUP_MTB Then
                    'グループマスターデータ取得
                    If GetGroupMastarData(Adapter, Cn, dataHBKX0801) = False Then
                        Return False
                    End If
                ElseIf .PropStrTableNM = SORT_CI_INFO_TB Then

                    'CI共通情報データ取得
                    If GetCIInfoData(Adapter, Cn, dataHBKX0801) = False Then
                        Return False
                    End If
                ElseIf .PropStrTableNM = SORT_MAILTEMP_MTB Then
                    'メールテンプレートマスターデータ取得
                    If GetMailTempMastarData(Adapter, Cn, dataHBKX0801) = False Then
                        Return False
                    End If
                End If
            End With
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
    ''' グループマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループマスタデータを取得する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetGroupMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtGrpMtb As New DataTable


        Try


            'グループマスターデータ取得

            'グループマスターデータ取得用SQLの作成・設定
            If SqlHBKX0801.SetSelectGroupMasterSql(Adapter, Cn, dataHBKX0801) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGrpMtb)

            '取得データをデータクラスにセット
            dataHBKX0801.PropDtSortList = dtGrpMtb

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
            dtGrpMtb.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' CI共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報データを取得する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCIInfoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言

        Dim dtCIInfoTb As New DataTable


        Try


            'CI共通情報データ取得

            'CI共通情報データ取得用SQLの作成・設定
            If sqlHBKX0801.SetSelectCIInfoSql(Adapter, Cn, dataHBKX0801) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtCIInfoTb)

            '取得データをデータクラスにセット
            dataHBKX0801.PropDtSortList = dtCIInfoTb

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
            dtCIInfoTb.Dispose()
        End Try


    End Function

    ''' <summary>
    ''' メールテンプレートマスターデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メールテンプレートマスターデータを取得する
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMailTempMastarData(ByVal Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMTmpMtb As New DataTable

        Try
            'メールテンプレートマスターデータ取得

            'メールテンプレートマスターデータ取得用SQLの作成・設定
            If sqlHBKX0801.SetSelectMailTempMasterSql(Adapter, Cn, dataHBKX0801) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレートマスターデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtMTmpMtb)

            '取得データをデータクラスにセット
            dataHBKX0801.PropDtSortList = dtMTmpMtb

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
            dtMTmpMtb.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            '登録する並び順がグループマスターかCI共通情報かで処理を分ける
            With dataHBKX0801
                If .PropStrTableNM = SORT_GROUP_MTB Then
                    'グループマスター初期表示設定
                    If SetGroupMasterInitData(dataHBKX0801) = False Then
                        Return False
                    End If
                ElseIf .PropStrTableNM = SORT_CI_INFO_TB Then
                    'CI共通情報初期表示設定
                    If SetCIInfoInitData(dataHBKX0801) = False Then
                        Return False
                    End If
                ElseIf .PropStrTableNM = SORT_MAILTEMP_MTB Then
                    'メールテンプレートマスター初期表示設定
                    If SetMailTempMasterInitData(dataHBKX0801) = False Then
                        Return False
                    End If
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
    ''' グループマスター初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループマスター初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupMasterInitData(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'グループマスター表示順一覧


            With dataHBKX0801.PropVwSortList.Sheets(0)
                .Rows.Clear()
                .DataSource = dataHBKX0801.PropDtSortList
                .Columns(SORT_SORT).DataField = "Sort"
                .Columns(SORT_CORD).DataField = "GroupCD"
                .Columns(SORT_NM).DataField = "GroupNM"
                .Columns(SORT_JTI_FLG).DataField = "JtiFlg"


            End With

            '件数をセット
            dataHBKX0801.PropLblCount.Text = dataHBKX0801.PropVwSortList.Sheets(0).RowCount & "件"

           

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
    ''' CI共通情報初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetCIInfoInitData(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
          

            'CI共通情報表示順一覧


            With dataHBKX0801.PropVwSortList.Sheets(0)
                .Rows.Clear()
                .DataSource = dataHBKX0801.PropDtSortList
                .Columns(SORT_SORT).DataField = "Sort"
                .Columns(SORT_CORD).DataField = "CINmb"
                .Columns(SORT_NM).DataField = "CINM"


            End With

            '件数をセット
            dataHBKX0801.PropLblCount.Text = dataHBKX0801.PropVwSortList.Sheets(0).RowCount & "件"

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
    ''' メールテンプレートマスター初期表示用データ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メールテンプレートマスター初期表示用データをフォームオブジェクトに設定する
    ''' <para>作成情報：2015/08/18 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetMailTempMasterInitData(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'メールテンプレートマスター表示順一覧
            With dataHBKX0801.PropVwSortList.Sheets(0)
                .Rows.Clear()
                .DataSource = dataHBKX0801.PropDtSortList
                .Columns(SORT_SORT).DataField = "Sort"
                .Columns(SORT_CORD).DataField = "TemplateNmb"
                .Columns(SORT_NM).DataField = "TemplateNM"
                .Columns(SORT_JTI_FLG).DataField = "JtiFlg"
            End With

            '件数をセット
            dataHBKX0801.PropLblCount.Text = dataHBKX0801.PropVwSortList.Sheets(0).RowCount & "件"

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
    ''' 隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内の隠し項目を設定する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Setvisible(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0801.PropVwSortList.Sheets(0)

                '隠し項目の設定
                .Columns(SORT_JTI_FLG).Visible = False      '削除フラグ


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
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>出力結果で削除ユーザーが表示された場合に該当行をグレーにする
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0801

                '背景色を初期化する
                .PropVwSortList.Sheets(0).Columns(SORT_SORT).BackColor = Color.White
                For i = 1 To .PropVwSortList.Sheets(0).ColumnCount - 1
                    .PropVwSortList.Sheets(0).Columns(i).BackColor = Color.FromArgb(255, 255, 128)
                Next

                For i = 0 To .PropVwSortList.Sheets(0).RowCount - 1
                    If .PropVwSortList.Sheets(0).GetValue(i, SORT_JTI_FLG) = DATA_MUKO Then
                        '削除フラグがが無効の場合はグレーに変更
                        .PropVwSortList.Sheets(0).Rows(i).BackColor = Color.Gray
                    End If

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
    ''' 並べ替えメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドを入力された並び順で並べ替える
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortMain(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)



        '入力チェック
        If InputCheck(dataHBKX0801) = False Then
            Return False
        End If


        'スプレッド表示並べ替え
        If SpreadSort(dataHBKX0801) = False Then
            Return False
        End If

        '表示順再設定処理
        If ReSetSort(dataHBKX0801) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 入力チェック
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示順の入力チェックを行う
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InputCheck(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try


            '表示順が空白または負の値の場合はエラーメッセージ出力
            With dataHBKX0801.PropVwSortList.Sheets(0)
                For i As Integer = 0 To .RowCount - 1
                    If .GetValue(i, SORT_SORT) = Nothing _
                        Or .GetValue(i, SORT_SORT) < 0 Then
                        'エラーメッセージをセット
                        puErrMsg = X0801_E001
                        '空白箇所にフォーカスセット
                        .SetActiveCell(i, SORT_SORT)

                        Return False
                    End If
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
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' スプレッド表示順並べ替え
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示順を並べ替える
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SpreadSort(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKX0801.PropVwSortList.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(SORT_SORT, True) '表示順

               

                '表示順の昇順でソートする
                .SortRows(0, .RowCount, Si)


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
    ''' 表示順再設定処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドの表示順を再設定する
    ''' <para>作成情報：2012/08/17 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ReSetSort(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try



            With dataHBKX0801.PropVwSortList.Sheets(0)

                For i As Integer = 0 To .RowCount - 1
                    .Cells(i, SORT_SORT).Value = i + 1
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
            puErrMsg = HBK_E001 & ex.Message
            Return False

        End Try

    End Function

    ''' <summary>
    ''' 入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された値のチェックを行う
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InputCheckMain(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '入力チェック
        If InputCheck(dataHBKX0801) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された並び順を登録する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function RegisterMain(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド表示並べ替え
        If SpreadSort(dataHBKX0801) = False Then
            Return False
        End If

        '登録実行
        If Register(dataHBKX0801) = False Then
            Return False
        End If

        '並び順登録画面初期表示メイン呼出
        If InitFormMain(dataHBKX0801) = False Then
            Return False
        End If



        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    '''　登録処理
    ''' </summary>
    ''' <param name="dataHBKX0801">[IN/OUT]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力された並び順を登録する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Register(ByRef dataHBKX0801 As DataHBKX0801) As Boolean

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

            'グループマスターかCI共通情報かで登録先を変更する
            If dataHBKX0801.PropStrTableNM = SORT_GROUP_MTB Then

                'グループマスター登録処理
                If RegisterGroupMaster(Cn, dataHBKX0801) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

            ElseIf dataHBKX0801.PropStrTableNM = SORT_CI_INFO_TB Then

                'CI共通情報登録処理
                If RegisterCIInfo(Cn, dataHBKX0801) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If

                '2015/08/18 ADD Start
            ElseIf dataHBKX0801.PropStrTableNM = SORT_MAILTEMP_MTB Then

                'メールテンプレートマスター登録処理
                If RegisterMailTempMaster(Cn, dataHBKX0801) = False Then
                    'ロールバック
                    Tsx.Rollback()
                    Return False
                End If
                '2015/08/18 ADD End
            End If

            'コミット
            Tsx.Commit()

            'コネクションを閉じる
            Cn.Close()



            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True


        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
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
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' グループマスター並び順登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>並び順をグループマスターに登録する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterGroupMaster(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ件数分Updateを実行
            With dataHBKX0801
                For i As Integer = 0 To .PropVwSortList.Sheets(0).RowCount - 1
                    '更新対象のグループコードをセット
                    .PropStrGrpCD = .PropVwSortList.Sheets(0).GetValue(i, SORT_CORD)
                    'ソート順をセット
                    .PropIntSort = i + 1

                    'グループマスター並び順登録（Update）用SQLを作成
                    If sqlHBKX0801.SetUpdateGroupMasterSql(Cmd, Cn, dataHBKX0801) = False Then
                        Return False
                    End If

                    'ログ出力
                    CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスター並び順登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' CI共通情報並び順登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>並び順をCI共通情報に登録する
    ''' <para>作成情報：2012/08/16 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterCIInfo( ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ件数分Updateを実行
            With dataHBKX0801
                For i As Integer = 0 To .PropVwSortList.Sheets(0).RowCount - 1
                    '更新対象のCI番号をセット
                    .PropIntCInmb = .PropVwSortList.Sheets(0).GetValue(i, SORT_CORD)
                    'ソート順をセット
                    .PropIntSort = i + 1

                    'CI共通情報並び順登録（Update）用SQLを作成
                    If sqlHBKX0801.SetUpdateCIInfoSql(Cmd, Cn, dataHBKX0801) = False Then
                        Return False
                    End If

                    'ログ出力
                    CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CI共通情報並び順登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' メールテンプレートマスター並び順登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKX0801">[IN]並び順登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>並び順をメールテンプレートマスターに登録する
    ''' <para>作成情報：20125/08/18 e.okamura
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RegisterMailTempMaster(ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKX0801 As DataHBKX0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'データ件数分Updateを実行
            With dataHBKX0801
                For i As Integer = 0 To .PropVwSortList.Sheets(0).RowCount - 1
                    '更新対象のテンプレート番号をセット
                    .PropIntTemplateNmb = .PropVwSortList.Sheets(0).GetValue(i, SORT_CORD)
                    'ソート順をセット
                    .PropIntSort = i + 1

                    'メールテンプレートマスター並び順登録（Update）用SQLを作成
                    If sqlHBKX0801.SetUpdateMailTempMasterSql(Cmd, Cn, dataHBKX0801) = False Then
                        Return False
                    End If

                    'ログ出力
                    CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "メールテンプレートマスター並び順登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            Cmd.Dispose()
        End Try

    End Function

End Class
