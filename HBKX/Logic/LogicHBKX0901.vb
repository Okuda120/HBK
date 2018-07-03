Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' ソフトマスター一覧画面ロジッククラス
''' </summary>
''' <remarks>ソフトマスター一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/08/29 k.ueda
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKX0901

    'インスタンス生成
    Private sqlHBKX0901 As New SqlHBKX0901

    'Public定数宣言==============================================

    'ソフトマスター一覧列番号
    Public Const SOFT_SOFT_CD As Integer = 0                  'コード
    Public Const SOFT_SOFT_KBN As Integer = 1                 'ソフト区分
    Public Const SOFT_SOFT_NM As Integer = 2                  'ソフト名称
    Public Const SOFT_JTI_FLG As Integer = 3                  '削除
    Public Const SOFT_JTI_FLG_KAKUSHI As Integer = 4          '削除フラグ(隠し項目)
    Public Const SOFT_SOFT_KBN_KAKUSHI As Integer = 5         'ソフト区分(隠し項目)
   
    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ソフトマスター一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'フォームオブジェクト設定処理
        If SetFormObj(dataHBKX0901) = False Then
            Return False
        End If


        'スプレッド用データテーブル作成
        If CreateDataTableForVw(dataHBKX0901) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKX0901) = False Then
            Return False
        End If

        '初期表示用データ設定
        If SetInitData(dataHBKX0901) = False Then
            Return False
        End If

        'スプレッド隠し項目設定処理
        If Setvisible(dataHBKX0901) = False Then
            Return False
        End If

        'スプレッド詳細設定
        If SetSpread(dataHBKX0901) = False Then
            Return False
        End If

        '出力結果背景色変更処理
        If ChangeColor(dataHBKX0901) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(dataHBKX0901) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' フォームオブジェクト設定処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームオブジェクトの設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormObj(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'オブジェクトの活性非活性設定

            With dataHBKX0901.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

            End With

            '全て表示ラジオボタンにチェックを入れる
            dataHBKX0901.PropRdoAll.Checked = True


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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSoftMaster As New DataTable   'ソフトマスター検索用データテーブル

        Try

            'ソフトマスター検索一覧用テーブル作成
            With dtSoftMaster
                .Columns.Add("SoftCD", Type.GetType("System.Int32"))                    'コード
                .Columns.Add("SoftKbn", Type.GetType("System.String"))                  'ソフト区分
                .Columns.Add("SoftNM", Type.GetType("System.String"))                   'ソフト名称
                .Columns.Add("JtiFlg", Type.GetType("System.String"))                   '削除
                .Columns.Add("JtiFlgKAKUSHI", Type.GetType("System.String"))            '削除フラグ(隠し項目)
                .Columns.Add("SoftKbnKAKUSHI", Type.GetType("System.String"))           'ソフト区分(隠し項目)
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスに作成テーブルを格納
            With dataHBKX0901
                .PropDtSoftMasterList = dtSoftMaster                                    'スプレッド表示用：ソフトマスター検索一覧

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
            dtSoftMaster.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示データを取得する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter


        Try

            'コネクションを開く
            Cn.Open()
            'スプレッド初期表示用データ取得
            If SpreadGetInitData(Adapter, Cn, dataHBKX0901) = False Then
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SpreadGetInitData(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try


            'SQLの作成・設定
            If sqlHBKX0901.SetSelectSoftMasterDataSql(Adapter, Cn, dataHBKX0901) = False Then
                Return False
            End If

            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ソフトマスター取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKX0901.PropDtSoftMasterList)


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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitData(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            '初期表示データをスプレッドに設定
            If SetInitDataSpread(dataHBKX0901) = False Then
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示用データをスプレッドに設定する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataSpread(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try
            'ソフトマスター一覧


            With dataHBKX0901.PropVwSoftMasterList.Sheets(0)
                .DataSource = dataHBKX0901.PropDtSoftMasterList
                .Columns(SOFT_SOFT_CD).DataField = "SoftCD"
                .Columns(SOFT_SOFT_KBN).DataField = "SoftKbn"
                .Columns(SOFT_SOFT_NM).DataField = "SoftNM"
                .Columns(SOFT_JTI_FLG).DataField = "JtiFlg"
                .Columns(SOFT_JTI_FLG_KAKUSHI).DataField = "JtiFlgKAKUSHI"
                .Columns(SOFT_SOFT_KBN_KAKUSHI).DataField = "SoftKbnKAKUSHI"
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
    ''' 隠し項目設定処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド内の隠し項目を設定する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Setvisible(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0901.PropVwSoftMasterList.Sheets(0)

                '隠し項目の設定
                .Columns(SOFT_JTI_FLG_KAKUSHI).Visible = False             '削除フラグ(隠し項目)
                .Columns(SOFT_SOFT_KBN_KAKUSHI).Visible = False            'ソフト区分(隠し項目)

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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッド表示の詳細設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetSpread(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'データ表示非表示設定処理
            If DataVisible(dataHBKX0901) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0901) = False Then
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択された項目ごとにデータの表示非表示を設定する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DataVisible(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKX0901.PropVwSoftMasterList.Sheets(0)

                'チェックボックスの状態で表示状態を変更する
                For i = 0 To .RowCount - 1
                    'チェックボックスにチェックが入ってなくかつ、データが無効の場合は表示しない
                    If dataHBKX0901.PropChkJtiFlg.Checked = False _
                        And .Cells(i, SOFT_JTI_FLG_KAKUSHI).Value = DATA_MUKO Then
                        .Rows(i).Visible = False
                        'それ以外は表示する
                    Else
                        .Rows(i).Visible = True
                    End If
                Next


            End With
            'ラジオボタン判定(全て表示が選択されている場合は何もしない)
            With dataHBKX0901
                'OSのみ表示が選択されている場合
                If .PropRdoOS.Checked = True Then
                    'データ件数分ループする
                    For i = 0 To .PropVwSoftMasterList.Sheets(0).RowCount - 1
                        '非表示でなくかつソフト区分がOSではないものを非表示にする
                        If .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = True _
                        And .PropVwSoftMasterList.Sheets(0).Cells(i, SOFT_SOFT_KBN_KAKUSHI).Value <> SOFTKBN_OS Then
                            'ソフト区分がOSのもの以外を非表示にする
                            .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = False
                        End If

                    Next
                    'オプションソフトのみ表示が選択されている場合
                ElseIf .PropRdoOptSoft.Checked = True Then
                    'データ件数分ループする
                    For i = 0 To .PropVwSoftMasterList.Sheets(0).RowCount - 1
                        '非表示でなくかつソフト区分がオプションソフトではないものを非表示にする
                        If .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = True _
                        And .PropVwSoftMasterList.Sheets(0).Cells(i, SOFT_SOFT_KBN_KAKUSHI).Value <> SOFTKBN_OPTIONSOFT Then
                            'ソフト区分がオプションソフトのもの以外を非表示にする
                            .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = False
                        End If

                    Next
                    'ウイルス対策ソフトのみ表示が選択されている場合
                ElseIf .PropRdoAntiVirus.Checked = True Then
                    'データ件数分ループする
                    For i = 0 To .PropVwSoftMasterList.Sheets(0).RowCount - 1
                        '非表示でなくかつソフト区分がオプションソフトではないものを非表示にする
                        If .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = True _
                        And .PropVwSoftMasterList.Sheets(0).Cells(i, SOFT_SOFT_KBN_KAKUSHI).Value <> SOFTKBN_UNTIVIRUSSOFT Then
                            'ソフト区分がウイルス対策ソフトのもの以外を非表示にする
                            .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = False
                        End If

                    Next
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
    ''' 行ヘッダ設定処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>行ヘッダを設定する
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRowHearder(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intRowHeader As Integer = 1    '再設定する行番号

        Try

            '行ヘッダを再設定する
            With dataHBKX0901.PropVwSoftMasterList.Sheets(0)

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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除データの背景色をグレーにする
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChangeColor(dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKX0901

                

                For i = 0 To .PropVwSoftMasterList.Sheets(0).RowCount - 1
                    If .PropVwSoftMasterList.Sheets(0).GetValue(i, SOFT_JTI_FLG_KAKUSHI) = DATA_MUKO Then
                        '削除データ行はグレーに変更
                        .PropVwSoftMasterList.Sheets(0).Rows(i).BackColor = Color.Gray
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索件数の表示を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SearchResult(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intCount As Integer = 0    '件数

        Try
            With dataHBKX0901
                '表示されている件数分カウントする
                For i = 0 To .PropVwSoftMasterList.Sheets(0).RowCount - 1
                    If .PropVwSoftMasterList.Sheets(0).Rows(i).Visible = True Then
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>削除されたデータの表示、非表示を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function CheckMain(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        'スプレッド詳細設定
        If SetSpread(dataHBKX0901) = False Then
            Return False
        End If

        '検索件数の表示
        If SearchResult(dataHBKX0901) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ラジオボックス選択時データ表示非表示設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ラジオボタンが選択された場合のデータの表示非表示の設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SoftVisibleMain(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '活性非活性化処理

        With dataHBKX0901

            'スプレッド詳細設定
            If SetSpread(dataHBKX0901) = False Then
                Return False
            End If

            '検索件数の表示
            If SearchResult(dataHBKX0901) = False Then
                Return False
            End If

        End With


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' デフォルトソートボタン押下時メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果初期表示の並びに戻す
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSortmain(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        Try

            'デフォルトソートを行う
            If DefaultSort(dataHBKX0901) = False Then
                Return False
            End If

            '行ヘッダ設定処理
            If SetRowHearder(dataHBKX0901) = False Then
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
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の初期表示の並びに戻す
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DefaultSort(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '変数宣言
            Dim Si(0) As SortInfo 'ソート対象配列

            With dataHBKX0901.PropVwSoftMasterList.Sheets(0)

                'ソート対象列をソートする順番で指定
                Si(0) = New SortInfo(SOFT_SOFT_CD, True) 'ソフトマスター.ソフトCD
               
                'ソフトマスター.ソフトCDの昇順でソートする
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

    ''' <summary>
    ''' 行ヘッダ再設定メイン処理
    ''' </summary>
    ''' <param name="dataHBKX0901">[IN/OUT]ソフトマスター一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>列ヘッダがクリックされた場合に、行ヘッダの再設定を行う
    ''' <para>作成情報：2012/08/29 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetRowHeaderMain(ByRef dataHBKX0901 As DataHBKX0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '行ヘッダ設定処理
        If SetRowHearder(dataHBKX0901) = False Then
            Return False
        End If


        '終了ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

End Class
