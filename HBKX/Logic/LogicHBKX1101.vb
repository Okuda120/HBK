Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread
''' <summary>
''' イメージマスター一覧画面ロジッククラス
''' </summary>
''' <remarks>イメージマスター一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/09/03 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX1101

    'インスタンス生成
    Private sqlHBKX1101 As New SqlHBKX1101

    'Public定数宣言==============================================

    'イメージマスター一覧列番号
    Public Const IMAGE_IMAGE_NMB As Integer = 0               'イメージ番号
    Public Const IMAGE_IMAGE_NM As Integer = 1                'イメージ名称
    Public Const IMAGE_KIND As Integer = 2                    '種別
    Public Const IMAGE_MAKER As Integer = 3                   'メーカー
    Public Const IMAGE_KISYU_NM As Integer = 4                '機種名
    Public Const IMAGE_OS_NM As Integer = 5                   'OS
    Public Const IMAGE_SP As Integer = 6                      'SP
    Public Const IMAGE_TYPE As Integer = 7                    'タイプ
    Public Const IMAGE_NOTES As Integer = 8                   '注意
    Public Const IMAGE_JTI_FLG As Integer = 9                 '削除フラグ

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>イメージマスター一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームオブジェクト設定処理
        If SetFormObj(dataHBKX1101) = False Then
            Return False
        End If

        'スプレッド用データテーブル作成
        If CreateDataTableForVw(dataHBKX1101) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX1101) = False Then
            Return False
        End If

        '初期表示用データ設定
        If SetInitData(dataHBKX1101) = False Then
            Return False
        End If

        'スプレッド詳細設定
        If SetSpread(dataHBKX1101) = False Then
            Return False
        End If

        '出力結果背景色変更処理
        If ChangeColor(dataHBKX1101) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(dataHBKX1101) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'オブジェクトの活性非活性設定

            With dataHBKX1101.PropGrpLoginUser

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
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtImageMaster As New DataTable   'イメージマスター検索用データテーブル

        Try

            'イメージマスター検索一覧用テーブル作成
            With dtImageMaster
                .Columns.Add("ImageNmb", Type.GetType("System.Int32"))                  '番号
                .Columns.Add("ImageNM", Type.GetType("System.String"))                  'イメージ名称
                .Columns.Add("Kind", Type.GetType("System.String"))                     '種別
                .Columns.Add("Maker", Type.GetType("System.String"))                    'メーカー
                .Columns.Add("KisyuNM", Type.GetType("System.String"))                  '機種名
                .Columns.Add("OSNM", Type.GetType("System.String"))                     'OS
                .Columns.Add("SP", Type.GetType("System.String"))                       'SP
                .Columns.Add("Type", Type.GetType("System.String"))                     'タイプ
                .Columns.Add("Notes", Type.GetType("System.String"))                    '注意
                .Columns.Add("JtiFlg", Type.GetType("System.String"))                   '削除
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX1101
                .PropDtImageMasterList = dtImageMaster                                  'スプレッド表示用：イメージマスター検索一覧

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
            dtImageMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示データを取得する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter


        Try

            'コネクションを開く
            Cn.Open()
            'スプレッド初期表示用データ取得
            If SpreadGetInitData(Adapter, Cn, dataHBKX1101) = False Then
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
    ''' スプレッド初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SpreadGetInitData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            'SQLの作成・設定
            If sqlHBKX1101.SetSelectImageMasterDataSql(Adapter, Cn, dataHBKX1101) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "イメージマスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX1101.PropDtImageMasterList)


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
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '初期表示データをスプレッドに設定
            If SetInitDataSpread(dataHBKX1101) = False Then
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
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをスプレッドに設定する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataSpread(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            'イメージマスター一覧


            With dataHBKX1101.PropvwImageMasterList.Sheets(0)
                .DataSource = dataHBKX1101.PropDtImageMasterList
                .Columns(IMAGE_IMAGE_NMB).DataField = "ImageNmb"
                .Columns(IMAGE_IMAGE_NM).DataField = "ImageNM"
                .Columns(IMAGE_KIND).DataField = "Kind"
                .Columns(IMAGE_MAKER).DataField = "Maker"
                .Columns(IMAGE_KISYU_NM).DataField = "KisyuNM"
                .Columns(IMAGE_OS_NM).DataField = "OSNM"
                .Columns(IMAGE_SP).DataField = "SP"
                .Columns(IMAGE_TYPE).DataField = "Type"
                .Columns(IMAGE_NOTES).DataField = "Notes"
                .Columns(IMAGE_JTI_FLG).DataField = "JtiFlg"
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
    ''' スプレッド詳細設定処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド表示の詳細設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSpread(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ表示非表示設定処理
            If DataVisible(dataHBKX1101) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX1101) = False Then
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
    ''' データ表示非表示設定処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの表示非表示を設定する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DataVisible(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX1101.PropvwImageMasterList.Sheets(0)

                'チェックボックスの状態で表示状態を変更する
                For i = 0 To .RowCount - 1
                    'チェックボックスにチェックが入ってなくかつ、データが無効の場合は表示しない
                    If dataHBKX1101.PropChkJtiFlg.Checked = False _
                        And .Cells(i, IMAGE_JTI_FLG).Value = DELDATA_DISPLAY_NM Then
                        .Rows(i).Visible = False
                        'それ以外は表示する
                    Else
                        .Rows(i).Visible = True
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
    ''' 行ヘッダ設定処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>行ヘッダを設定する
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRowHearder(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 1    '再設定する行番号

        Try

            '行ヘッダを再設定する
            With dataHBKX1101.PropvwImageMasterList.Sheets(0)

                For i = 0 To .RowCount - 1
                    '非表示でなければ行番号を割り振る
                    If .Rows(i).Visible = True Then
                        .RowHeader.Cells(i, 0).Value = intRowHeader
                        intRowHeader += 1
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
    ''' 出力結果背景色変更処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの背景色をグレーにする
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX1101



                For i = 0 To .PropvwImageMasterList.Sheets(0).RowCount - 1
                    If .PropvwImageMasterList.Sheets(0).GetValue(i, IMAGE_JTI_FLG) = DELDATA_DISPLAY_NM Then
                        '削除データ行はグレーに変更
                        .PropvwImageMasterList.Sheets(0).Rows(i).BackColor = Color.Gray
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
    ''' 検索件数表示処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の表示を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX1101
                '表示されている件数分カウントする
                For i = 0 To .PropvwImageMasterList.Sheets(0).RowCount - 1
                    If .PropvwImageMasterList.Sheets(0).Rows(i).Visible = True Then
                        intCount += 1
                    End If
                Next

                '検索件数をセット
                .PropLblCount.Text = intCount & "件"

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
    ''' 削除データ表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータの表示、非表示を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckMain(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド詳細設定
        If SetSpread(dataHBKX1101) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(dataHBKX1101) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 行ヘッダ再設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>列ヘッダがクリックされた場合に、行ヘッダの再設定を行う
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetRowHeaderMain(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '行ヘッダ設定処理
        If SetRowHearder(dataHBKX1101) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' デフォルトソートボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'デフォルトソートを行う
            If DefaultSort(DataHBKX1101) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(DataHBKX1101) = False Then
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
    ''' デフォルトソート
    ''' </summary>
    ''' <param name="dataHBKX1101">[IN/OUT]イメージマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSort(ByRef dataHBKX1101 As DataHBKX1101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKX1101.PropvwImageMasterList.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(IMAGE_IMAGE_NMB, True) 'イメージマスター.イメージ番号

                'イメージマスター.イメージ番号の昇順でソートする
                .SortRows(0, .RowCount, Si)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1

                    .Columns(i).ResetSortIndicator()

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


End Class
