Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.IO
Imports System.Windows.Forms
Imports FarPoint.Win.Spread

''' <summary>
''' 変更登録画面ロジッククラス
''' </summary>
''' <remarks>変更登録画面のロジックを定義したクラス
''' <para>作成情報：2012/08/13 r.hoshino
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKE0201

    'インスタンス作成
    Private sqlHBKE0201 As New SqlHBKE0201
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================
    '対応関係者情報一覧列番号
    Public Const COL_RELATION_KBN As Integer = 0                '区分
    Public Const COL_RELATION_ID As Integer = 1                 'ID
    Public Const COL_RELATION_GROUPNM As Integer = 2            'グループ名
    Public Const COL_RELATION_USERNM As Integer = 3             'ユーザー名
    Public Const COL_RELATION_ENTRYNMB As Integer = 4           '隠し：登録順
    Public Const COL_RELATION_REGDT As Integer = 5              '隠し：登録日時
    Public Const COL_RELATION_REGGP As Integer = 6              '隠し：登録GP
    Public Const COL_RELATION_REGID As Integer = 7              '隠し：登録ID
    'プロセスリンク一覧列番号
    Public Const COL_processLINK_KBN_NMR As Integer = 0         '区分
    Public Const COL_processLINK_NO As Integer = 1              '番号
    Public Const COL_processLINK_KBN As Integer = 2             '隠し：区分コード
    Public Const COL_processLINK_ENTRYDT As Integer = 3         '隠し：登録順
    Public Const COL_processLINK_REGDT As Integer = 4           '隠し：登録日時
    Public Const COL_processLINK_REGGP As Integer = 5           '隠し：登録GP
    Public Const COL_processLINK_REGID As Integer = 6           '隠し：登録ID
    '関連ファイル一覧列番号
    Public Const COL_FILE_NAIYO As Integer = 0                  '説明
    Public Const COL_FILE_REGDT As Integer = 1                  '登録日時
    Public Const COL_FILE_MNGNMB As Integer = 2                 '隠し：番号
    Public Const COL_FILE_PATH As Integer = 3                   '隠し：ファイルパス
    Public Const COL_FILE_ENTRYNMB As Integer = 4               '隠し：登録順

    '会議情報一覧列番号
    Public Const COL_MEETING_NO As Integer = 0                  '番号
    Public Const COL_MEETING_JIBI As Integer = 1                '実施日
    Public Const COL_MEETING_NIN As Integer = 2                 '承認
    Public Const COL_MEETING_TITLE As Integer = 3               'タイトル
    Public Const COL_MEETING_NINCD As Integer = 4               '隠し：承認コード
    Public Const COL_MEETING_REGDT As Integer = 5               '隠し：登録日時
    Public Const COL_MEETING_REGGP As Integer = 6               '隠し：登録GP
    Public Const COL_MEETING_REGID As Integer = 7               '隠し：登録ID
    'CYSPR情報一覧列番号
    Public Const COL_CYSPR_NO As Integer = 0                    '番号
    Public Const COL_CYSPR_BEF As Integer = 1                   '隠し：番号
    Public Const COL_CYSPR_ENTRYNMB As Integer = 2              '隠し：登録順
    Public Const COL_CYSPR_REGDT As Integer = 3                 '隠し：登録日時
    Public Const COL_CYSPR_REGGP As Integer = 4                 '隠し：登録GP
    Public Const COL_CYSPR_REGID As Integer = 5                 '隠し：登録ID
    Public Const COL_CYSPR_UPDDT As Integer = 6                 '隠し：登録日時
    Public Const COL_CYSPR_UPDGP As Integer = 7                 '隠し：登録GP
    Public Const COL_CYSPR_UPDID As Integer = 8                 '隠し：登録ID
    'タブテーブル
    Public Const TAB_KHN As Integer = 0                         '基本情報
    Public Const TAB_MEETING As Integer = 1                     '会議情報
    Public Const TAB_FREE As Integer = 2                        'フリー入力情報

    Private Const OUTPUT_LOG_TITLE As String = "Chg"            'ログ出力用

    ''' <summary>
    ''' 【共通】システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKE0201) = False Then
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
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            'トランザクション系のコントロールをリストに追加
            With dataHBKE0201
                '基本情報タブ
                aryCtlList.Add(.PropBtnKaisiDT_HM)
                aryCtlList.Add(.PropBtnKanryoDT_HM)
                aryCtlList.Add(.PropBtnTantoMY)
                aryCtlList.Add(.PropBtnTantoSearch)
                aryCtlList.Add(.PropBtnhenkouMY)
                aryCtlList.Add(.PropBtnhenkouSearch)
                aryCtlList.Add(.PropBtnsyoninMY)
                aryCtlList.Add(.PropBtnsyoninSearch)
                aryCtlList.Add(.PropBtnAddRow_File)
                aryCtlList.Add(.PropBtnRemoveRow_File)
                aryCtlList.Add(.PropBtnOpenFile)
                aryCtlList.Add(.PropBtnSaveFile)
                '会議情報タブ
                aryCtlList.Add(.PropBtnAddRow_meeting)
                aryCtlList.Add(.PropBtnRemoveRow_meeting)
                '共通
                aryCtlList.Add(.PropGrpLoginUser)        'ログイン／ロックグループ
                aryCtlList.Add(.PropBtnAddRow_Grp)
                aryCtlList.Add(.PropBtnAddRow_Usr)
                aryCtlList.Add(.PropBtnRemoveRow_Kankei)
                aryCtlList.Add(.PropBtnAddRow_plink)
                aryCtlList.Add(.PropBtnRemoveRow_plink)
                aryCtlList.Add(.PropBtnAddRow_CYSPR)
                aryCtlList.Add(.PropBtnRemoveRow_CYSPR)
                aryCtlList.Add(.PropBtnReg)
                aryCtlList.Add(.PropBtnMail)
                aryCtlList.Add(.PropBtnRelease)

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
    ''' 【新規登録モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormNewModeMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKE0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKE0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKE0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【編集モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormEditModeMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKE0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKE0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKE0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKE0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【参照モード】画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで画面の初期表示設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormRefModeMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '1スプレッド表示用データテーブル作成
        If CreateDataTableForVw(dataHBKE0201) = False Then
            Return False
        End If

        '2フォームコントロール設定
        If InitFormControl(dataHBKE0201) = False Then
            Return False
        End If

        '3初期表示用データ取得
        If GetInitData(dataHBKE0201) = False Then
            Return False
        End If

        '4初期表示用データセット
        If SetInitDataToControl(dataHBKE0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【リリース登録ボタン】プロセスリンク再取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクデータの再取得を行う。
    ''' <para>作成情報：2012/08/20 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshPLinkMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'プロセスリンク情報データ取得(PropDtResultMtg)
            If GetPLinkRef(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If


            With dataHBKE0201
                'データテーブルを取得
                .PropDtprocessLink = DirectCast(.PropVwprocessLinkInfo.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtprocessLink.Clone
                Dim dtDel As DataTable = .PropDtprocessLink.Clone
                If .PropDtprocessLink IsNot Nothing AndAlso .PropDtprocessLink.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtprocessLink.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtprocessLink.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtprocessLink.Rows(i).Item("processkbnnmr"), _
                                               .PropDtprocessLink.Rows(i).Item("mngnmb"), _
                                               .PropDtprocessLink.Rows(i).Item("processkbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtprocessLink.Rows(i).Item("mngnmb", DataRowVersion.Original), _
                                               .PropDtprocessLink.Rows(i).Item("processkbn", DataRowVersion.Original))

                        End Select
                    Next
                End If

                'プロセスリンクスプレッド再取得データを設定
                .PropDtprocessLink = .PropDtResultMtg.Copy
                .PropDtprocessLink.AcceptChanges()
                .PropVwprocessLinkInfo.DataSource = .PropDtprocessLink


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtprocessLink.Rows.Add(dtAdd.Rows(i).Item("processkbnnmr"), _
                                                  dtAdd.Rows(i).Item("mngnmb"), _
                                                  dtAdd.Rows(i).Item("processkbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtprocessLink.Rows.Count - 1
                            Select Case .PropDtprocessLink.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtprocessLink.Rows(j).Item("mngnmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtprocessLink.Rows(j).Item("processkbn", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtprocessLink.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtprocessLink.Rows(j).Item("mngnmb").ToString.Equals(dtDel.Rows(i).Item("mngnmb").ToString) AndAlso _
                                        .PropDtprocessLink.Rows(j).Item("processkbn").ToString.Equals(dtDel.Rows(i).Item("processkbn").ToString) Then
                                        .PropDtprocessLink.Rows(j).Delete()
                                    End If
                            End Select
                        Next
                    Next
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
    ''' 【プロセスリンク】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="intResult">[IN/OUT]関係者チェック情報</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strKbn">[IN]プロセス区分</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function PlinkKankeiCheckMain(ByRef intResult As Integer, ByVal intNmb As Integer, strKbn As String) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            'k-2ユーザーチェック処理
            If ChkKankeiU(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                Return False
            End If

            '関係者なら次のチェックは不要
            If intResult <> KANKEI_CHECK_EDIT Then
                'k-3所属グループチェック処理
                If ChkKankeiSZK(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                    Return False
                End If

                '関係者でないなら次のチェックは不要
                If intResult <> KANKEI_CHECK_NONE Then
                    'k-1グループチェック処理
                    If ChkKankeiG(Adapter, Cn, intNmb, strKbn, intResult) = False Then
                        Return False
                    End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' k【共通】対応関係者チェックメイン処理　[.PropintChkKankei = 0:参照不可,1:参照のみ関係者,2:編集できる関係者]
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者をチェックし、処理モードの切替を行う。
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function KankeiCheckMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ
        Try
            'コネクションを開く
            Cn.Open()

            With dataHBKE0201
                'k-2ユーザーチェック処理
                If ChkKankeiU(Adapter, Cn, .PropIntChgNmb, PROCESS_TYPE_CHANGE, .PropIntChkKankei) = False Then
                    Return False
                End If

                '関係者なら次のチェックは不要
                If .PropIntChkKankei <> KANKEI_CHECK_EDIT Then
                    'k-3所属グループチェック処理
                    If ChkKankeiSZK(Adapter, Cn, .PropIntChgNmb, PROCESS_TYPE_CHANGE, .PropIntChkKankei) = False Then
                        Return False
                    End If

                    '関係者でないなら次のチェックは不要
                    If .PropIntChkKankei <> KANKEI_CHECK_NONE Then
                        'k-1グループチェック処理
                        If ChkKankeiG(Adapter, Cn, .PropIntChgNmb, PROCESS_TYPE_CHANGE, .PropIntChkKankei) = False Then
                            Return False
                        End If
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
        Finally
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' k-3.【共通】対応関連者所属チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/08/28 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiSZK(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKE0201.GetChkKankeiSZKData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者所属グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_REF
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
        Finally
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' k-1.【共通】対応関連者グループチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiG(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKE0201.GetChkKankeiGData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者グループチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' k-2.【共通】対応関連者ユーザーチェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="IntNmb">[IN]管理番号</param>
    ''' <param name="StrKbn">[IN]プロセス区分</param>
    ''' <param name="IntResult">[IN/OUT]結果戻り値</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>チェックデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function ChkKankeiU(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal IntNmb As Integer, _
                                  ByVal StrKbn As String, _
                                  ByRef IntResult As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKE0201.GetChkKankeiUData(Adapter, Cn, IntNmb, StrKbn) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関連者ユーザーチェック", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '判定結果をデータクラスにセット
            If dtmst.Rows.Count > 0 Then
                If CLng(dtmst.Rows(0).Item(0)) > 0 Then
                    IntResult = KANKEI_CHECK_EDIT
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
        Finally
            dtmst.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 1.スプレッド表示用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTableForVw(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim DtKankei As New DataTable             'スプレッド表示用：対応関係者情報データ
        Dim DtprocessLink As New DataTable        'スプレッド表示用：プロセスリンク管理番号データ
        Dim DtFileInfo As New DataTable           'スプレッド表示用：関連ファイルデータ
        Dim DtMeeting As New DataTable            'スプレッド表示用：会議情報ファイルデータ
        Dim DtCyspr As New DataTable              'スプレッド表示用：CYSPRデータ
        Try

            '対応関係者情報データ
            With DtKankei
                .Columns.Add("RelationKbn", Type.GetType("System.String"))         '区分
                .Columns.Add("RelationID", Type.GetType("System.String"))          'ID
                .Columns.Add("GroupNM", Type.GetType("System.String"))             'グループ名
                .Columns.Add("HBKUsrNM", Type.GetType("System.String"))            'ユーザー名

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'プロセスリンクデータ
            With DtprocessLink
                .Columns.Add("ProcessKbnNMR", Type.GetType("System.String"))       'プロセス区分（略名称）
                .Columns.Add("MngNmb", Type.GetType("System.String"))              '番号
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))          'プロセス区分_隠し

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '関連ファイルデータ
            With DtFileInfo
                .Columns.Add("FileNaiyo", Type.GetType("System.String"))             '説明
                .Columns.Add("RegDt", Type.GetType("System.String"))                 '登録日時
                .Columns.Add("FileMngNmb", Type.GetType("System.String"))            'ファイル番号_隠し
                .Columns.Add("FilePath", Type.GetType("System.String"))              'ファイルパス_隠し

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'CYSPRデータ
            With DtCyspr
                .Columns.Add("cysprnmb", Type.GetType("System.String"))              '番号

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With

            '会議ファイルデータ
            With DtMeeting
                .Columns.Add("MeetingNmb", Type.GetType("System.String"))           '会議番号
                .Columns.Add("JisiDT", Type.GetType("System.String"))               '実施日
                .Columns.Add("ResultKbnNM", Type.GetType("System.String"))          '承認コード
                .Columns.Add("Title", Type.GetType("System.String"))                'タイトル
                .Columns.Add("ResultKbn", Type.GetType("System.String"))            '承認_隠し

                .Columns.Add("EntryNmb", Type.GetType("System.Int32"))
                .Columns.Add("RegDT", Type.GetType("System.DateTime"))
                .Columns.Add("RegGrpCD", Type.GetType("System.String"))
                .Columns.Add("RegID", Type.GetType("System.String"))
                'テーブルの変更を確定
                .AcceptChanges()
            End With


            'データクラスに作成テーブルを格納
            With dataHBKE0201
                .PropDtKankei = DtKankei
                .PropDtprocessLink = DtprocessLink
                .PropDtFileInfo = DtFileInfo
                .PropDtMeeting = DtMeeting
                .PropDtCyspr = DtCyspr
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
            DtKankei.Dispose()
            DtprocessLink.Dispose()
            DtFileInfo.Dispose()
            DtCyspr.Dispose()
            DtMeeting.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 2.フォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitFormControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '2-1スプレッド設定
            If SetVwControl(dataHBKE0201) = False Then
                Return False
            End If

            '2-2処理モードに応じたフォームコントロール設定
            If SetFormControlPerProcMode(dataHBKE0201) = False Then
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
    ''' 2-1.スプレッド初期設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>各スプレッド（一覧）を初期設定する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                '関係者情報一覧
                With .PropVwKankei.Sheets(0)
                    .ColumnCount = COL_RELATION_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_RELATION_KBN).DataField = "RelationKbn"                    '関係区分
                    .Columns(COL_RELATION_ID).DataField = "RelationID"                      '関係ID
                    .Columns(COL_RELATION_GROUPNM).DataField = "GroupNM"                    'グループ名
                    .Columns(COL_RELATION_USERNM).DataField = "HBKUsrNM"                    'ユーザー名
                    '隠し列非表示
                    .Columns(COL_RELATION_ENTRYNMB).Visible = False
                    .Columns(COL_RELATION_REGDT).Visible = False
                    .Columns(COL_RELATION_REGGP).Visible = False
                    .Columns(COL_RELATION_REGID).Visible = False
                End With

                'プロセスリンク一覧
                With .PropVwprocessLinkInfo.Sheets(0)
                    .ColumnCount = COL_processLINK_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_processLINK_KBN_NMR).DataField = "LinkMotoProcesskbnNM"      '区分
                    .Columns(COL_processLINK_NO).DataField = "LinkMotoNmb"                  '番号
                    .Columns(COL_processLINK_KBN).DataField = "LinkMotoProcesskbn"
                    .Columns(COL_processLINK_ENTRYDT).DataField = "EntryDT"
                    '隠し列非表示
                    .Columns(COL_processLINK_KBN).Visible = False
                    .Columns(COL_processLINK_ENTRYDT).Visible = False
                    .Columns(COL_processLINK_REGDT).Visible = False
                    .Columns(COL_processLINK_REGGP).Visible = False
                    .Columns(COL_processLINK_REGID).Visible = False
                End With

                '関連ファイル
                With .PropVwFileInfo.Sheets(0)
                    .ColumnCount = COL_FILE_ENTRYNMB + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_FILE_NAIYO).DataField = "FileNaiyo"        '説明
                    .Columns(COL_FILE_REGDT).DataField = "RegDt"            '登録日時
                    .Columns(COL_FILE_MNGNMB).DataField = "FileMngNmb"      'ファイル番号　※隠し列
                    .Columns(COL_FILE_PATH).DataField = "FilePath"          'ファイルパス　※隠し列
                    '隠し列非表示
                    .Columns(COL_FILE_MNGNMB).Visible = False
                    .Columns(COL_FILE_PATH).Visible = False
                    .Columns(COL_FILE_ENTRYNMB).Visible = False
                End With

                'Cyspr
                With .PropVwCYSPR.Sheets(0)
                    .ColumnCount = COL_CYSPR_UPDID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_CYSPR_NO).DataField = "cysprnmb"        '番号
                    '隠し列非表示
                    .Columns(COL_CYSPR_BEF).Visible = False
                    .Columns(COL_CYSPR_ENTRYNMB).Visible = False
                    .Columns(COL_CYSPR_REGDT).Visible = False
                    .Columns(COL_CYSPR_REGGP).Visible = False
                    .Columns(COL_CYSPR_REGID).Visible = False
                    .Columns(COL_CYSPR_UPDDT).Visible = False
                    .Columns(COL_CYSPR_UPDGP).Visible = False
                    .Columns(COL_CYSPR_UPDID).Visible = False
                End With

                '会議情報
                With .PropVwMeeting.Sheets(0)
                    .ColumnCount = COL_MEETING_REGID + 1
                    .DataAutoCellTypes = False
                    .DataAutoSizeColumns = False
                    .DataAutoHeadings = False

                    .Columns(COL_MEETING_NO).DataField = "MeetingNmb"           '会議番号
                    .Columns(COL_MEETING_JIBI).DataField = "JisiDT"             '実施日
                    .Columns(COL_MEETING_NIN).DataField = "ResultKbnNM"         '承認
                    .Columns(COL_MEETING_TITLE).DataField = "Title"             'タイトル
                    .Columns(COL_MEETING_NINCD).DataField = "ResultKbn"         '承認CD　※隠し列
                    '隠し列非表示
                    .Columns(COL_MEETING_NINCD).Visible = False
                    .Columns(COL_MEETING_REGDT).Visible = False
                    .Columns(COL_MEETING_REGGP).Visible = False
                    .Columns(COL_MEETING_REGID).Visible = False
                End With

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
    ''' 2-2.処理モード毎のフォームコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフォームコントロールの設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFormControlPerProcMode(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '2-2-1ログイン／ロックコントロール設定
            If SetLoginAndLockControl(dataHBKE0201) = False Then
                Return False
            End If

            '2-2-2フッタ設定
            If SetFooterControl(dataHBKE0201) = False Then
                Return False
            End If

            '2-2-3タブページ設定
            If SetTabControl(dataHBKE0201) = False Then
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
    ''' 2-2-1.ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '2-2-1-1新規登録モード用設定
                    If SetLoginAndLockControlForNew(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '2-2-1-2編集モード用設定
                    If SetLoginAndLockControlForEdit(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '2-2-1-3参照モード用設定
                    If SetLoginAndLockControlForRef(dataHBKE0201) = False Then
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
    ''' 2-2-1-1.【新規登録モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン非表示
                .PropBtnUnlockVisible = False

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
    ''' 2-2-1-2.【編集モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '解除ボタン表示
                .PropBtnUnlockVisible = True

                '解除ボタン非活性
                .PropBtnUnlockEnabled = False

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
    ''' 2-2-1-3.【参照モード】ログイン／ロックコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLoginAndLockControlForRef(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201.PropGrpLoginUser

                'グループコンボボックス非活性
                .cmbGroup.Enabled = False

                '変更ボタン非活性
                .btnChange.Enabled = False

                'ロック情報表示
                .PropLockInfoVisible = True

                '関係者か？
                If dataHBKE0201.PropIntChkKankei <> KANKEI_CHECK_NONE Then
                    '解除ボタン表示
                    .PropBtnUnlockVisible = True

                    'ロックされているか？同じグループか？
                    If dataHBKE0201.PropBlnBeLockedFlg = True AndAlso dataHBKE0201.PropDtLock.Rows.Count > 0 AndAlso _
                       dataHBKE0201.PropDtLock.Rows(0).Item("EdiGrpCD").ToString.Equals(PropWorkGroupCD) Then
                        '解除ボタン活性
                        .PropBtnUnlockEnabled = True
                    Else
                        '解除ボタン非活性
                        .PropBtnUnlockEnabled = False
                    End If

                Else
                    '解除ボタン非表示
                    .PropBtnUnlockVisible = False
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
    ''' 2-2-2.フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '2-2-2-1新規登録モード用設定
                    If SetFooterControlForNew(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '2-2-2-2編集モード用設定
                    If SetFooterControlForEdit(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then      '参照モード

                    '2-2-2-3参照モード用設定
                    If SetFooterControlForRef(dataHBKE0201) = False Then
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
    ''' 2-2-2-1.【新規登録モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                .PropBtnAddRow_Usr.Enabled = True           '対応関係者U
                .PropBtnAddRow_Grp.Enabled = True           '対応関係者G
                .PropBtnRemoveRow_Kankei.Enabled = True     '対応関係者ー
                .PropBtnAddRow_plink.Enabled = True         'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = True      'プロセスリンクー
                .PropBtnAddRow_CYSPR.Enabled = True          'CYSPR＋
                .PropBtnRemoveRow_CYSPR.Enabled = True       'CYSPRー

                .PropBtnReg.Enabled = True                  '登録
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnRelease.Enabled = False             'リリース登録

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 2-2-2-2.【編集モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                .PropBtnAddRow_Usr.Enabled = True           '対応関係者U
                .PropBtnAddRow_Grp.Enabled = True           '対応関係者G
                .PropBtnRemoveRow_Kankei.Enabled = True     '対応関係者ー
                .PropBtnAddRow_plink.Enabled = True         'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = True      'プロセスリンクー
                .PropBtnAddRow_CYSPR.Enabled = True          'CYSPR＋
                .PropBtnRemoveRow_CYSPR.Enabled = True       'CYSPRー

                .PropBtnReg.Enabled = True                  '登録
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnRelease.Enabled = True              'リリース登録

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 2-2-2-3.【参照モード】フッタコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでフッタコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetFooterControlForRef(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201
                .PropBtnAddRow_Usr.Enabled = False          '対応関係者U
                .PropBtnAddRow_Grp.Enabled = False          '対応関係者G
                .PropBtnRemoveRow_Kankei.Enabled = False    '対応関係者ー
                .PropBtnAddRow_plink.Enabled = False        'プロセスリンク＋
                .PropBtnRemoveRow_plink.Enabled = False     'プロセスリンクー
                .PropBtnAddRow_CYSPR.Enabled = False        'CYSPR＋
                .PropBtnRemoveRow_CYSPR.Enabled = False     'CYSPRー

                .PropBtnReg.Enabled = False                 '登録
                .PropBtnMail.Enabled = True                 'メール作成
                .PropBtnRelease.Enabled = False               'リリース登録

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    .PropBtnBack.Text = "閉じる"
                Else
                    '.PropBtnBack.Text = "戻る"
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
    ''' 2-2-3.タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてタブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '2-2-3-1基本情報タブ設定
            If SetTabControlKhn(dataHBKE0201) = False Then
                Return False
            End If

            '2-2-3-2会議情報タブ設定
            If SetTabControlMeeting(dataHBKE0201) = False Then
                Return False
            End If

            '2-2-3-3フリー入力情報タブ設定
            If SetTabControlFree(dataHBKE0201) = False Then
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
    ''' 2-2-3-1.【共通】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhn(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード
                    '2-2-3-1-1
                    If SetTabControlKhnForNew(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード
                    '2-2-3-1-2
                    If SetTabControlKhnForEdit(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード
                    '2-2-3-1-3
                    If SetTabControlKhnForRef(dataHBKE0201) = False Then
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
    ''' '2-2-3-1-1.【新規登録モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/01 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN

                .PropBtnAddRow_File.Enabled = True          '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = True       '関連ファイルー
                .PropBtnOpenFile.Enabled = False            '関連ファイル開
                .PropBtnSaveFile.Enabled = False            '関連ファイルダ

                .PropBtnTantoMY.Enabled = True             '担当私
                .PropBtnTantoSearch.Enabled = True         '担当検索
                .PropBtnhenkouMY.Enabled = True            '変更承認者私
                .PropBtnhenkouSearch.Enabled = True        '変更承認者検索
                .PropBtnsyoninMY.Enabled = True            '承認記録者私
                .PropBtnsyoninSearch.Enabled = True        '承認記録者検索
                .PropBtnKaisiDT_HM.Enabled = True          '開始日時
                .PropBtnKanryoDT_HM.Enabled = True         '完了日時

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
    ''' '2-2-3-1-2.【編集モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/01 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN

                .PropBtnAddRow_File.Enabled = True          '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = True       '関連ファイルー
                .PropBtnOpenFile.Enabled = True             '関連ファイル開
                .PropBtnSaveFile.Enabled = True             '関連ファイルダ

                .PropBtnTantoMY.Enabled = True             '担当私
                .PropBtnTantoSearch.Enabled = True         '担当検索
                .PropBtnhenkouMY.Enabled = True            '変更承認者私
                .PropBtnhenkouSearch.Enabled = True        '変更承認者検索
                .PropBtnsyoninMY.Enabled = True            '承認記録者私
                .PropBtnsyoninSearch.Enabled = True        '承認記録者検索
                .PropBtnKaisiDT_HM.Enabled = True          '開始日時
                .PropBtnKanryoDT_HM.Enabled = True         '完了日時
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
    ''' '2-2-3-1-3.【参照モード】基本情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードで基本情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlKhnForRef(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '基本情報タブをアクティブタブに設定
                '.PropTbInput.SelectedIndex = TAB_KHN
                .PropBtnAddRow_File.Enabled = False         '関連ファイル＋
                .PropBtnRemoveRow_File.Enabled = False      '関連ファイルー
                .PropBtnOpenFile.Enabled = True             '関連ファイル開
                .PropBtnSaveFile.Enabled = True             '関連ファイルダ

                .PropBtnTantoMY.Enabled = False             '担当私
                .PropBtnTantoSearch.Enabled = False         '担当検索
                .PropBtnhenkouMY.Enabled = False            '変更承認者私
                .PropBtnhenkouSearch.Enabled = False        '変更承認者検索
                .PropBtnsyoninMY.Enabled = False            '承認記録者私
                .PropBtnsyoninSearch.Enabled = False        '承認記録者検索
                .PropBtnKaisiDT_HM.Enabled = False          '開始日時
                .PropBtnKanryoDT_HM.Enabled = False         '完了日時
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
    ''' 2-2-3-2. 【共通】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeeting(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '2-2-3-2-1
                    If SetTabControlMeetingForNew(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '2-2-3-2-2
                    If SetTabControlMeetingForEdit(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '※新規登録モードと同じ
                    If SetTabControlMeetingForNew(dataHBKE0201) = False Then
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
    ''' '2-2-3-2-1.【新規登録モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '会議情報タブ内すべてのボタンを非活性とする
                .PropBtnAddRow_meeting.Enabled = False
                .PropBtnRemoveRow_meeting.Enabled = False

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
    ''' '2-2-3-2-2.【編集モード】会議情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlMeetingForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '会議情報タブ内すべてのボタンを活性とする
                .PropBtnAddRow_meeting.Enabled = True
                .PropBtnRemoveRow_meeting.Enabled = True

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
    ''' 2-2-3-3.【共通】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFree(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201


                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '※編集モードと同じ
                    If SetTabControlFreeForEdit(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '2-2-3-3-2
                    If SetTabControlFreeForEdit(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '※編集モードと同じ
                    If SetTabControlFreeForEdit(dataHBKE0201) = False Then
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
    ''' 2-2-3-3-2.【編集／新規登録／参照モード】フリー入力情報タブコントロール設定
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力情報タブコントロールの初期設定を行う
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetTabControlFreeForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.ReadOnly = False
                .PropTxtBIko2.ReadOnly = False
                .PropTxtBIko3.ReadOnly = False
                .PropTxtBIko4.ReadOnly = False
                .PropTxtBIko5.ReadOnly = False

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Enabled = True
                .PropChkFreeFlg2.Enabled = True
                .PropChkFreeFlg3.Enabled = True
                .PropChkFreeFlg4.Enabled = True
                .PropChkFreeFlg5.Enabled = True

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
    ''' 3.初期表示用データ取得処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>初期表示するデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '3-1マスタデータ取得
            If GetMastaData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            '3-2メイン表示データ取得
            If GetMainData(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-1.マスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '3-1-1プロセスステータスマスタ取得
            If GetprocessStateMst(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            '3-1-2担当グループマスタ取得
            If GetTantoGpMst(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            '3-1-3対象システム取得
            If GetsystemMst(Adapter, Cn, dataHBKE0201) = False Then
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
    ''' 3-1-1.【共通】ステータスマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報：ステータスを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetprocessStateMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.GetCmbProcessStateMstData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ステータスマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & E0201_E001, TBNM_PROCESSSTATE_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKE0201.PropDtprocessStatusMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-1-2.【共通】担当グループマスタ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>グループマスタを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoGpMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.GetSTantoMastaData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & E0201_E001, TBNM_GRP_MTB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKE0201.PropDtTantGrpMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-1-3.【共通】対象システム取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CI共通情報：分類１、分類２、名称を取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetsystemMst(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.GetsystemMastaData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            'データが取得できなかった場合、エラー
            If dtmst.Rows.Count = 0 Then
                puErrMsg = String.Format(HBK_E001 & E0201_E001, TBNM_CI_INFO_TB)
                Return False
            End If

            '取得データをデータクラスにセット
            dataHBKE0201.PropDtSystemMasta = dtmst


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
            dtmst.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 3-2.初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainData(ByVal Adapter As NpgsqlDataAdapter, _
                                 ByVal Cn As NpgsqlConnection, _
                                 ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '3-2-1取得しない

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '3-2-2編集モード用データ取得
                    If GetMainDataForEdit(Adapter, Cn, dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '※編集モードと同じ
                    If GetMainDataForEdit(Adapter, Cn, dataHBKE0201) = False Then
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
    ''' 3-2-2.【共通】初期表示用メインデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集／参照モードで初期表示用のメインデータを取得する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainDataForEdit(ByVal Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '3-2-2-1共通情報データ取得
            If GetMainInfo(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '担当履歴情報データ取得
            If GetTantoRireki(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '3-2-2-2対応関係者データ取得
            If GetKankei(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '3-2-2-3プロセスデータ取得
            If GetPLink(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '3-2-2-4関連ファイルデータ取得
            If GetFile(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '3-2-2-5Cysprデータ取得
            If GetCyspr(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            '3-2-2-6会議情報データ取得
            If GetMeeting(Adapter, Cn, DataHBKE0201) = False Then
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
    ''' 3-2-2-1.【共通】共通情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMainInfo(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectMainInfoSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtMainInfo = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-2-2-2.【共通】対応関係者情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectKankeiSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtKankei = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-2-2-3【共通】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPLink(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectPLinkSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtprocessLink = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-2-2-4【共通】関連ファイル情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetFile(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectFileSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtFileInfo = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-2-2-5【共通】CYSPRデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR情報データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetCyspr(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectCysprSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CYSPR情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtCyspr = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 3-2-2-6【共通】会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeeting(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectMeetingSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtMeeting = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function



    ''' <summary> 
    ''' 4.初期データ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォームコントロールに初期表示データを設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInitDataToControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '4-1ログイン／ロックデータ設定
            If SetDataToLoginAndLock(dataHBKE0201) = False Then
                Return False
            End If

            '4-2タブコントロールデータ設定
            If SetDataToTabControl(dataHBKE0201) = False Then
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
    ''' 4-1.ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLock(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '4-1-1新規登録モード用設定
                    If SetDataToLoginAndLockForNew(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '4-1-2編集モード用設定
                    If SetDataToLoginAndLockForEdit(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then        '参照モード

                    '4-1-3参照モード用設定
                    If SetDataToLoginAndLockForRef(dataHBKE0201) = False Then
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
    ''' 4-1-1.【新規登録モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201.PropGrpLoginUser

                'ロック開始日時
                .PropLockDate = Nothing

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
    ''' 4-1-2.【編集モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKE0201.PropDtLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing  '編集開始日時
                    If dataHBKE0201.PropDtLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKE0201.PropDtLock.Rows(0).Item("EdiTime")
                        dataHBKE0201.PropStrEdiTime = dtmLockTime
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 4-1-3.【参照モード】ログイン／ロックデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>参照モードでログイン／ロックデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToLoginAndLockForRef(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201.PropGrpLoginUser

                'ロック情報が取得できた場合
                If dataHBKE0201.PropDtLock IsNot Nothing AndAlso dataHBKE0201.PropDtLock.Rows.Count > 0 Then
                    'ロック開始日時
                    Dim dtmLockTime As DateTime = Nothing
                    If dataHBKE0201.PropDtLock.Rows(0).Item("EdiTime").ToString() <> "" Then
                        dtmLockTime = dataHBKE0201.PropDtLock.Rows(0).Item("EdiTime")
                    End If
                    .PropLockDate = dtmLockTime
                Else
                    'ロック開始日時
                    .PropLockDate = Nothing
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
    ''' 4-2.タブコントロールデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>タブコントロールデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabControl(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '4-2-1基本情報タブデータ設定
            If SetDataToTabKhn(dataHBKE0201) = False Then
                Return False
            End If

            '4-2-2会議情報タブデータ設定
            If SetDataToTabMeeting(dataHBKE0201) = False Then
                Return False
            End If

            '4-2-3フリー入力タブデータ設定
            If SetDataToTabFree(dataHBKE0201) = False Then
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
    ''' 4-2-1.【共通】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhn(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '4-2-1-1新規登録モード用設定
                    If SetDataToTabKhnForNew(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '4-2-1-2編集モード用設定
                    If SetDataToTabKhnForEdit(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_REF Then    '参照モード

                    '（編集モードと同じ）
                    If SetDataToTabKhnForEdit(dataHBKE0201) = False Then
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
    ''' 4-2-1-1.【新規登録モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '4-2-1-1-1コンボボックス作成
            If CreateCmb(dataHBKE0201) = False Then
                Return False
            End If

            'システム日付取得
            If GetSysdate(dataHBKE0201) = False Then
                Return False
            End If

            With dataHBKE0201

                '基本情報
                .PropTxtNmb.Text = ""
                .PropLblRegInfo_out.Text = ""
                .PropLblUpdateInfo_out.Text = ""

                .PropCmbprocessStateCD.SelectedValue = ""       'ステータス
                .PropDtpKaisiDT.txtDate.Text = "" '.PropDtmSysDate.ToShortDateString
                .PropTxtKaisiDT_HM.PropTxtTime.Text = "" 'String.Format("{0:00}:{1:00}", .PropDtmSysDate.Hour, .PropDtmSysDate.Minute)
                .PropDtpKanryoDT.txtDate.Text = ""
                .PropTxtKanryoDT_HM.PropTxtTime.Text = ""

                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    '問題画面の情報を挿入する
                    '対象システム
                    .PropCmbSystemNmb.PropCmbColumns.SelectedValue = .PropIntTSystemNmb.ToString()       '基本情報タブ：対象システム
                Else
                    .PropCmbSystemNmb.PropCmbColumns.Text = ""
                End If

                .PropCmbTantoGrpCD.SelectedValue = "" ' PropWorkGroupCD
                .PropTxtTantoID.Text = "" ' PropUserId
                .PropTxtTantoNM.Text = "" 'PropUserName
                .PropTxthenkouID.Text = ""
                .PropTxthenkouNM.Text = ""
                .PropTxtsyoninID.Text = ""
                .PropTxtsyoninNM.Text = ""

                '対応内容
                .PropTxtTitle.Text = ""
                .PropTxtNaiyo.Text = ""
                .PropTxtTaisyo.Text = ""

                '作業担当履歴
                .PropTxtTantoHistory.Text = ""
                .PropTxtGrpHistory.Text = ""

                '対応関係者スプレッド
                dataHBKE0201.PropVwKankei.DataSource = dataHBKE0201.PropDtKankei
                'プロセスリンクスプレッド
                '呼び出し元に応じて変更
                If .PropIntOwner = SCR_CALLMOTO_REG Then
                    '問題画面の情報を挿入する
                    '問題登録画面のプロセスリンク情報を挿入する
                    Dim drProcessLink As DataRow
                    drProcessLink = .PropDtprocessLink.NewRow()
                    drProcessLink(COL_processLINK_KBN_NMR) = PROCESS_TYPE_QUESTION_NAME_R
                    drProcessLink(COL_processLINK_NO) = .PropIntPrbNmb
                    drProcessLink(COL_processLINK_KBN) = PROCESS_TYPE_QUESTION
                    'DataTableに保存
                    .PropDtprocessLink.Rows.Add(drProcessLink)
                    For i As Integer = 0 To .PropVwProcessLinkInfo_Save.Sheets(0).Rows.Count - 1 Step 1
                        drProcessLink = .PropDtprocessLink.NewRow()
                        drProcessLink(COL_processLINK_KBN_NMR) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_processLINK_KBN_NMR)
                        drProcessLink(COL_processLINK_NO) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_processLINK_NO)
                        drProcessLink(COL_processLINK_KBN) = .PropVwProcessLinkInfo_Save.Sheets(0).GetText(i, COL_processLINK_KBN)
                        'DataTableに保存
                        .PropDtprocessLink.Rows.Add(drProcessLink)
                    Next
                    .PropVwprocessLinkInfo.DataSource = .PropDtprocessLink 'プロセスリンク情報：プロセスリンク情報スプレッド
                Else
                    dataHBKE0201.PropVwprocessLinkInfo.DataSource = dataHBKE0201.PropDtprocessLink
                End If

                '関連ファイル情報スプレッド
                dataHBKE0201.PropVwFileInfo.DataSource = dataHBKE0201.PropDtFileInfo
                'CYSPRスプレッド
                dataHBKE0201.PropVwCYSPR.DataSource = dataHBKE0201.PropDtCyspr

                'メール関連
                '.PropTxtkigencondcikbncd = ""
                '.PropTxtkigencondkigen = ""
                '.PropTxtkigencondtypekbn = ""
                '.PropTxtKigenCondUsrID = ""
                .PropTxtRegGp = ""
                .PropTxtRegUsr = ""
                .PropTxtRegDT = ""
                .PropTxtUpdateGp = ""
                .PropTxtUpdateUsr = ""
                .PropTxtUpdateDT = ""


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
    ''' 4-2-1-2.【編集／参照モード】基本情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードで基本情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabKhnForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '4-2-1-1-1コンボボックス作成
            If CreateCmb(dataHBKE0201) = False Then
                Return False
            End If

            With dataHBKE0201
                '基本情報  
                .PropTxtNmb.Text = .PropIntChgNmb.ToString()
                'グループ名、ユーザ名、登録日時
                .PropLblRegInfo_out.Text = .PropDtMainInfo.Rows(0).Item("LblRegInfo")
                'グループ名、ユーザ名、更新日時
                .PropLblUpdateInfo_out.Text = .PropDtMainInfo.Rows(0).Item("LblUpdateInfo")

                'Excelメール用その２
                .PropTxtRegGp = .PropDtMainInfo.Rows(0).Item("mail_RegGp")
                .PropTxtRegUsr = .PropDtMainInfo.Rows(0).Item("mail_RegUsr")
                .PropTxtRegDT = .PropDtMainInfo.Rows(0).Item("mail_RegDT")
                .PropTxtUpdateGp = .PropDtMainInfo.Rows(0).Item("mail_UpdateGp")
                .PropTxtUpdateUsr = .PropDtMainInfo.Rows(0).Item("mail_UpdateUsr")
                .PropTxtUpdateDT = .PropDtMainInfo.Rows(0).Item("mail_UpdateDT")

                .PropCmbprocessStateCD.SelectedValue = .PropDtMainInfo.Rows(0).Item("ProcessStateCD").ToString

                If .PropDtMainInfo.Rows(0).Item("kaisidt").ToString.Equals("") Then
                    .PropDtpKaisiDT.txtDate.Text = ""
                    .PropTxtKaisiDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpKaisiDT.txtDate.Text = DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kaisidt")).ToShortDateString
                    .PropTxtKaisiDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kaisidt")).Hour, DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kaisidt")).Minute)
                End If

                If .PropDtMainInfo.Rows(0).Item("kanryodt").ToString.Equals("") Then
                    .PropDtpKanryoDT.txtDate.Text = ""
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = ""
                Else
                    .PropDtpKanryoDT.txtDate.Text = DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kanryodt")).ToShortDateString
                    .PropTxtKanryoDT_HM.PropTxtTime.Text = String.Format("{0:00}:{1:00}", DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kanryodt")).Hour, DateTime.Parse(.PropDtMainInfo.Rows(0).Item("kanryodt")).Minute)
                End If

                .PropCmbSystemNmb.PropCmbColumns.SelectedValue = .PropDtMainInfo.Rows(0).Item("SystemNmb").ToString()

                '担当者
                .PropCmbTantoGrpCD.SelectedValue = .PropDtMainInfo.Rows(0).Item("tantogrpcd").ToString
                .PropTxtTantoID.Text = .PropDtMainInfo.Rows(0).Item("chgtantoid").ToString
                .PropTxtTantoNM.Text = .PropDtMainInfo.Rows(0).Item("chgtantonm").ToString
                .PropTxthenkouID.Text = .PropDtMainInfo.Rows(0).Item("approverid").ToString
                .PropTxthenkouNM.Text = .PropDtMainInfo.Rows(0).Item("approvernm").ToString
                .PropTxtsyoninID.Text = .PropDtMainInfo.Rows(0).Item("recorderid").ToString
                .PropTxtsyoninNM.Text = .PropDtMainInfo.Rows(0).Item("recordernm").ToString
                '対応内容
                .PropTxtTitle.Text = .PropDtMainInfo.Rows(0).Item("Title").ToString
                .PropTxtNaiyo.Text = .PropDtMainInfo.Rows(0).Item("Naiyo").ToString
                .PropTxtTaisyo.Text = .PropDtMainInfo.Rows(0).Item("Taisyo").ToString

                ''担当履歴
                '.PropTxtGrpHistory.Text = .PropDtMainInfo.Rows(0).Item("GroupRireki").ToString
                '.PropTxtTantoHistory.Text = .PropDtMainInfo.Rows(0).Item("TantoRireki").ToString


                .PropTxtRegGp = .PropDtMainInfo.Rows(0).Item("mail_RegGp").ToString
                .PropTxtRegUsr = .PropDtMainInfo.Rows(0).Item("mail_RegUsr").ToString
                .PropTxtRegDT = .PropDtMainInfo.Rows(0).Item("mail_RegDT").ToString
                .PropTxtUpdateGp = .PropDtMainInfo.Rows(0).Item("mail_UpdateGp").ToString
                .PropTxtUpdateUsr = .PropDtMainInfo.Rows(0).Item("mail_UpdateUsr").ToString
                .PropTxtUpdateDT = .PropDtMainInfo.Rows(0).Item("mail_UpdateDT").ToString

                '担当履歴 
                If CreateTantoRireki(dataHBKE0201) = False Then
                    Return False
                End If

                '対応関係者スプレッド
                .PropVwKankei.DataSource = .PropDtKankei

                'ユーザ名の背景色を濃灰色にする
                With .PropVwKankei.Sheets(0)
                    For i As Integer = 0 To dataHBKE0201.PropDtKankei.Rows.Count - 1
                        If .GetText(i, COL_RELATION_USERNM) = "" Then
                            .Cells(i, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY
                        End If
                        'グループ名の背景色を濃灰色にする
                        If .GetText(i, COL_RELATION_GROUPNM) = "" Then
                            .Cells(i, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY
                        End If
                    Next
                End With

                'プロセスリンクスプレッド
                .PropVwprocessLinkInfo.DataSource = .PropDtprocessLink

                '関連ファイル情報スプレッド
                .PropVwFileInfo.DataSource = .PropDtFileInfo

                'データが無い場合、ボタン制御を行う
                With .PropVwFileInfo.Sheets(0)
                    If .RowCount > 0 Then
                        dataHBKE0201.PropBtnOpenFile.Enabled = True
                        dataHBKE0201.PropBtnSaveFile.Enabled = True
                    Else
                        dataHBKE0201.PropBtnOpenFile.Enabled = False
                        dataHBKE0201.PropBtnSaveFile.Enabled = False
                    End If
                End With

                'Cysprスプレッド
                .PropVwCYSPR.DataSource = .PropDtCyspr

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
    ''' 4-2-1-1-1.【ComboBox共通】コンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateCmb(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201


                'ステータスコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtprocessStatusMasta, .PropCmbprocessStateCD, True, "", "") = False Then
                    Return False
                End If

                '対象システムコンボボックス作成
                .PropCmbSystemNmb.PropIntStartCol = 2
                If commonLogic.SetCmbBoxEx(.PropDtSystemMasta, .PropCmbSystemNmb, "cinmb", "txt", True, 0, "") = False Then
                    Return False
                End If

                '担当グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtTantGrpMasta, .PropCmbTantoGrpCD, True, "", "") = False Then
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
    ''' 4-2-2.【共通】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じて会議情報タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeeting(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '4-2-2-1新規登録モード用設定
                    If SetDataToTabMeetingForNew(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード

                    '4-2-2-2編集モード用設定
                    If SetDataToTabMeetingForEdit(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '※編集とおなじ
                    If SetDataToTabMeetingForEdit(dataHBKE0201) = False Then
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
    ''' 4-2-2-1.【新規登録モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201



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
    ''' 4-2-2-2.【編集／参照モード】会議情報タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabMeetingForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201
                dataHBKE0201.PropVwMeeting.DataSource = dataHBKE0201.PropDtMeeting
                ''会議情報スプレッド
                'If dataHBKE0201.PropDtMeeting.Rows.Count > 0 Then

                'End If

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
    ''' 4-2-3.【共通】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>処理モードに応じてフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFree(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                If .PropStrProcMode = PROCMODE_NEW Then             '新規登録モード

                    '4-2-3-1新規登録モード用設定
                    If SetDataToTabFreeForNew(dataHBKE0201) = False Then
                        Return False
                    End If


                ElseIf .PropStrProcMode = PROCMODE_EDIT Then        '編集モード


                    '4-2-3-2編集モード用設定
                    If SetDataToTabFreeForEdit(dataHBKE0201) = False Then
                        Return False
                    End If

                ElseIf .PropStrProcMode = PROCMODE_REF Then         '参照モード

                    '※編集と同じ
                    If SetDataToTabFreeForEdit(dataHBKE0201) = False Then
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
    ''' 4-2-3-1.【新規登録モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規登録モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForNew(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = ""
                .PropTxtBIko2.Text = ""
                .PropTxtBIko3.Text = ""
                .PropTxtBIko4.Text = ""
                .PropTxtBIko5.Text = ""

                'フリーフラグ１～５チェックボックス
                .PropChkFreeFlg1.Checked = False
                .PropChkFreeFlg2.Checked = False
                .PropChkFreeFlg3.Checked = False
                .PropChkFreeFlg4.Checked = False
                .PropChkFreeFlg5.Checked = False

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
    ''' 4-2-3-2.【編集／参照モード】フリー入力タブデータ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>編集モードでフリー入力タブデータを初期設定する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetDataToTabFreeForEdit(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKE0201

                'フリーテキスト１～５テキストボックス
                .PropTxtBIko1.Text = .PropDtMainInfo.Rows(0).Item("BIko1")
                .PropTxtBIko2.Text = .PropDtMainInfo.Rows(0).Item("BIko2")
                .PropTxtBIko3.Text = .PropDtMainInfo.Rows(0).Item("BIko3")
                .PropTxtBIko4.Text = .PropDtMainInfo.Rows(0).Item("BIko4")
                .PropTxtBIko5.Text = .PropDtMainInfo.Rows(0).Item("BIko5")

                'フリーフラグ１～５チェックボックス
                If .PropDtMainInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_ON Then
                    .PropChkFreeFlg1.Checked = True
                ElseIf .PropDtMainInfo.Rows(0).Item("FreeFlg1") = FREE_FLG_OFF Then
                    .PropChkFreeFlg1.Checked = False
                End If
                If .PropDtMainInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_ON Then
                    .PropChkFreeFlg2.Checked = True
                ElseIf .PropDtMainInfo.Rows(0).Item("FreeFlg2") = FREE_FLG_OFF Then
                    .PropChkFreeFlg2.Checked = False
                End If
                If .PropDtMainInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_ON Then
                    .PropChkFreeFlg3.Checked = True
                ElseIf .PropDtMainInfo.Rows(0).Item("FreeFlg3") = FREE_FLG_OFF Then
                    .PropChkFreeFlg3.Checked = False
                End If
                If .PropDtMainInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_ON Then
                    .PropChkFreeFlg4.Checked = True
                ElseIf .PropDtMainInfo.Rows(0).Item("FreeFlg4") = FREE_FLG_OFF Then
                    .PropChkFreeFlg4.Checked = False
                End If
                If .PropDtMainInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_ON Then
                    .PropChkFreeFlg5.Checked = True
                ElseIf .PropDtMainInfo.Rows(0).Item("FreeFlg5") = FREE_FLG_OFF Then
                    .PropChkFreeFlg5.Checked = False
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
    ''' 【ComboBox共通】コンボボックスリサイズメイン処理
    ''' </summary>
    ''' <param name="sender">[IN/OUT]コンボボックス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>コンボボックスサイズ変換処理
    ''' <para>作成情報：2012/08/08 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ComboBoxResizeMain(ByRef sender As Object) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コンボボックスサイズ変換処理
        If commonLogicHBK.ComboBoxResize(sender) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True


    End Function




    ''' <summary>
    ''' 【担当ID入力時】ユーザーマスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定したひびきユーザーのマスタデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetTantoDataMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetTantoData(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【変更承認者ID入力時】エンドユーザマスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定したエンドユーザーのマスタデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetHenkouDataMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetPartnerData(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【承認記録者ID入力時】ユーザーマスタデータ取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>指定したひびきユーザーのマスタデータを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetSyoninDataMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            'コネクションを開く
            Cn.Open()

            'マスタデータ取得
            If GetTantoData(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【ID入力時】ユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/08/16 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKE0201.GetTantoInfoData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ユーザーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKE0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【ID入力時】エンドユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>マスタデータを取得する
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPartnerData(ByVal Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try


            '取得用SQLの作成・設定
            If sqlHBKE0201.GetPartnerInfoData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "エンドユーザーマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)

            '取得データをデータクラスにセット
            dataHBKE0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function






    ''' <summary>
    ''' L [初期画面時]ロックメイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>データのロック状況をチェックし、状況に応じてロックまたは処理モードの切替を行う
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function LockMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック設定
        If SetLockWhenLoad(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' C [画面]クローズ時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenCloseMain(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ロック解除処理
        If UnlockData(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' J [解除]ボタンクリック時ロック解除メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータをログインユーザでロックし、フォームのロックを解除する
    ''' <para>作成情報：2012/07/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function UnlockWhenClickBtnUnlockMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'モード変更
        dataHBKE0201.PropStrProcMode = PROCMODE_EDIT

        'ロックフラグOFF
        dataHBKE0201.PropBlnBeLockedFlg = False

        'J-1ロック処理
        If SetLockWhenUnlock(dataHBKE0201) = False Then
            Return False
        End If

        'フォームコントロールのロックを解除する
        If SetFormControlPerProcMode(dataHBKE0201) = False Then
            Return False
        End If

        'ログイン／ロックデータ設定
        If SetDataToLoginAndLock(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' D【DB更新時】ロック解除チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckBeUnlockedMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'D-1ロック解除チェック
        If CheckUnlock(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function


    ''' <summary>
    ''' L-1.フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenLoad(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeLocked As Boolean = False                   'ロックフラグ  

        Try

            With dataHBKE0201

                'ロック解除チェック
                If CheckDataBeLocked(.PropIntChgNmb, blnBeLocked, .PropStrBeLockedMsg, .PropDtLock) = False Then
                    Return False
                End If

                'ロックされている（別のユーザが編集中）場合、 ロックフラグをON
                If blnBeLocked = True Then

                    dataHBKE0201.PropBlnBeLockedFlg = True

                Else

                    'ロックされていない場合、問題共通情報をロックする
                    If SetLock(dataHBKE0201) = False Then
                        Return False
                    End If

                    'ロックフラグをOFF
                    dataHBKE0201.PropBlnBeLockedFlg = False

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
    ''' L-2.フォームロード時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報テーブルをロックする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLock(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnDoUnlock As Boolean = False                   'ロック解除実行フラグ

        Try

            With dataHBKE0201

                '問題共通情報ロックテーブルデータがある場合、ロック解除実行フラグON
                If .PropDtLock.Rows.Count > 0 Then
                    blnDoUnlock = True
                End If

                '問題共通情報ロック
                If LockInfo(.PropIntChgNmb, .PropDtLock, blnDoUnlock) = False Then
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
    ''' L-1-1.ロック状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="blnBeLocked">[IN/OUT]ロックフラグ（ロック時：True）</param>
    ''' <param name="strBeLockedMsg">ロック時メッセージ</param>
    ''' <param name="dtLock">共通情報ロックテーブル</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報がロックされているかチェックする。
    ''' また、ロックされている場合はエラーメッセージも返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeLocked(ByVal intNmb As Integer, _
                                         ByRef blnBeLocked As Boolean, _
                                         ByRef strBeLockedMsg As String, _
                                         ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '問題共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロックチェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間  

        Try
            'ロックフラグ、共通情報ロックデータ数初期化
            blnBeLocked = False

            '共通情報ロックテーブル取得
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '編集者IDを取得
                strEdiID = dtResult.Rows(0).Item("EdiID")

                '編集者IDがログインユーザIDと異なるかチェック
                'If strEdiID <> PropUserId Then

                '編集者IDがログインユーザIDと異なる場合、サーバーの編集開始日時を取得
                strEdiTime = dtResult.Rows(0).Item("EdiTime").ToString()

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    '現在日時と編集開始日時の差を取得し、その差がロック解除時間を下回る場合はロックされている
                    tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                    tsUnlock = TimeSpan.Parse(PropUnlockTime)
                    If tsDiff < tsUnlock Then

                        'ロックフラグON
                        blnBeLocked = True

                    End If

                End If

                'End If

                'ロックフラグがONの場合、ロック画面表示メッセージセット
                If blnBeLocked = True Then
                    'ロック画面表示メッセージセット
                    strBeLockedMsg = String.Format(HBK_I001, dtResult.Rows(0).Item("EdiGroupNM"), dtResult.Rows(0).Item("EdiUsrNM"))
                End If

            End If

            '取得データを戻り値セット
            dtLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function

    ''' <summary>
    '''  L-1-1-1.共通情報ロック情報取得処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報ロックテーブルおよびサーバー日付を返す
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function GetLockTb(ByVal intNmb As Integer, _
                                 ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'DB接続用変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        'SQL変数宣言--------------------------------------

        Try
            'データ格納用テーブル初期化
            dtLock = New DataTable

            'コネクションを開く
            Cn.Open()

            '共通情報ロックテーブル、サーバー日付取得
            If sqlHBKE0201.SelectLock(Adapter, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
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
            dtLock.Dispose()
            Adapter.Dispose()
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' L-2-1.ロック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックテーブルデータ格納テーブル</param>
    ''' <param name="blnDoUnlock">[IN]解除実行フラグ（True：解除してからロックする）※省略可</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>管理番号をキーに共通情報ロックテーブルのデータをINSERTする
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function LockInfo(ByVal intNmb As Integer, _
                                ByRef dtLock As DataTable, _
                                Optional ByVal blnDoUnlock As Boolean = False) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try

            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'ロック解除実行フラグがONの場合、共通情報ロックテーブルデータを削除
            If blnDoUnlock = True Then
                If DeleteLock(Cn, intNmb) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            End If

            '共通情報ロックテーブル登録
            If InsertLock(Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'データ格納用テーブル初期化
            dtLock = New DataTable

            '共通情報ロックテーブル取得
            If sqlHBKE0201.SelectLock(Adapter, Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル取得", Nothing, Adapter.SelectCommand)

            'SQLを実行し、結果を取得
            Adapter.Fill(dtLock)

            'コミット
            Tsx.Commit()

            '2行取得できた場合（ロック情報がある場合）、ロック情報にサーバー日付を不可
            If dtLock.Rows.Count > 1 Then
                'ロック情報にサーバー日付を設定
                dtLock.Rows(1).Item("SysTime") = dtLock.Rows(0).Item("SysTime")
                '1行目のデータを削除
                dtLock.Rows(0).Delete()
                '変更をコミット
                dtLock.AcceptChanges()
            End If

            'ログ出力
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
            Adapter.Dispose()
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()

        End Try

    End Function


    ''' <summary>
    ''' J-1.【編集モード】解除ボタンクリック時ロック設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/07/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLockWhenUnlock(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'J-1-1共通情報テーブルロック解除
            If UnlockInfo(dataHBKE0201.PropIntChgNmb) = False Then
                Return False
            End If

            'L-2-1共通情報テーブルロック
            If LockInfo(dataHBKE0201.PropIntChgNmb, dataHBKE0201.PropDtLock, False) = False Then
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
    ''' J-1-1.ロック解除処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報のロックを解除する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function UnlockInfo(ByVal intNmb As Integer) As Boolean

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

            '共通情報ロックテーブル削除処理
            If DeleteLock(Cn, intNmb) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'コミット
            Tsx.Commit()

            'ログ出力
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
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' C-1.ロック解除処理(画面クローズ,更新処理完了時)
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]インシデント登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックを解除する
    ''' <para>作成情報：2012/07/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UnlockData(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'J-1-1共通情報ロック解除（DELETE）
            If UnlockInfo(dataHBKE0201.PropIntChgNmb) = False Then
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
    ''' D-1.【DB更新時】ロック解除チェック処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>表示中のデータのロックが解除されていないかチェックする
    ''' <para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckUnlock(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim blnBeUnocked As Boolean = False       'ロックフラグ

        Try
            'D-1-1ロック解除チェック
            If CheckDataBeUnlocked(dataHBKE0201.PropIntChgNmb, dataHBKE0201.PropStrEdiTime, _
                                                  blnBeUnocked, dataHBKE0201.PropDtLock) = False Then
                Return False
            End If

            'ロック解除されている（別のユーザが編集中）場合、ロックフラグON
            If blnBeUnocked = True Then
                dataHBKE0201.PropBlnBeLockedFlg = True
            Else
                dataHBKE0201.PropBlnBeLockedFlg = False
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
    ''' D-1-1.ロック解除状況チェック処理
    ''' </summary>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <param name="strEdiTime_Bef">[IN]既に設定済の編集開始日時</param>
    ''' <param name="blnBeUnocked">[IN/OUT]ロック解除フラグ（True：ロック解除されている）</param>
    ''' <param name="dtLock">[IN/OUT]共通情報ロックデータ</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された管理番号の共通情報のロック解除状況をチェックする。
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function CheckDataBeUnlocked(ByVal intNmb As Integer, _
                                           ByVal strEdiTime_Bef As String, _
                                           ByRef blnBeUnocked As Boolean, _
                                           ByRef dtLock As DataTable) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル項目格納用変数宣言
        Dim dtResult As New DataTable           'SELECT結果格納テーブル

        'ロック解除チェック用変数宣言
        Dim strEdiID As String = ""             '編集者ID
        Dim strEdiTime As String = ""           '編集開始日時
        Dim strSysTime As String                '現在日時（サーバー）
        Dim tsDiff As TimeSpan = Nothing        '編集開始日時と現在日時の差
        Dim tsUnlock As TimeSpan = Nothing      'ロック解除時間    

        '定数宣言
        Const DATE_FORMAT As String = "yyyy/MM/dd HH:mm:ss" '日付型フォーマット形式

        Try
            'ロック解除フラグ初期化
            blnBeUnocked = False

            '********************************
            '* 共通情報ロックテーブル取得
            '********************************
            If GetLockTb(intNmb, dtResult) = False Then
                Return False
            End If

            '********************************
            '* ロック解除チェック
            '********************************

            '現在日時を取得
            strSysTime = dtResult.Rows(0).Item("SysTime").ToString()

            '共通情報ロックデータが取得できた場合、チェックを行う
            If dtResult.Rows.Count > 0 AndAlso dtResult.Rows(0).Item("EdiID") <> "" Then

                '設定済の編集開始日時を取得
                strEdiTime = strEdiTime_Bef

                '編集開始日時がセットされている場合、現在日時と編集開始日時の差異がシステム管理マスタ.ロック解除時間以内かチェック
                If strEdiTime <> "" Then

                    'ロック時の編集開始日時と、現在ロックテーブルに登録されている編集開始日時が異なる場合、ロック解除されている
                    If Format(DateTime.Parse(strEdiTime), DATE_FORMAT) <> Format(DateTime.Parse(dtResult.Rows(0).Item("EdiTime")), DATE_FORMAT) Then
                        'ロック解除フラグON
                        blnBeUnocked = True
                    Else
                        '現在日時と編集開始日時の差を取得し、その差がロック解除時間を上回る場合はロック解除されている
                        tsDiff = New TimeSpan(DateTime.Parse(strSysTime).Subtract(DateTime.Parse(strEdiTime)).Ticks)
                        tsUnlock = TimeSpan.Parse(PropUnlockTime)
                        If tsDiff >= tsUnlock Then
                            'ロック解除フラグON
                            blnBeUnocked = True
                        End If
                    End If

                End If

            Else
                '共通情報ロックデータが取得できなかった場合

                'ロック解除フラグON
                blnBeUnocked = True

            End If

            '取得データを戻り値にセット
            dtLock = dtResult

            'ログ出力
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
            dtResult.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 共通情報ロックテーブル削除処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>管理番号をキーに共通情報ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteLock(ByVal Cn As NpgsqlConnection, _
                                  ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '共通情報ロックテーブル削除処理
        Dim Cmd As New NpgsqlCommand          'SQLコマンド

        Try

            'DeleteLockSql
            If sqlHBKE0201.DeleteLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル削除", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
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
    ''' 共通情報ロックテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intNmb">[IN]管理番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>共通情報ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/17 s.yamaguchi
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Private Function InsertLock(ByVal Cn As NpgsqlConnection, _
                                   ByVal intNmb As Integer) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '共通情報ロック用変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報ロックテーブル登録
            If sqlHBKE0201.InsertLockSql(Cmd, Cn, intNmb) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, OUTPUT_LOG_TITLE & "共通情報ロックテーブル登録", Nothing, Cmd)

            'SQL実行
            Cmd.ExecuteNonQuery()

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Cmd)

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
    ''' 関係者情報グループ追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したグループデータを設定する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetGroupToVwRelationMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'グループデータ設定処理
        If SetGroupToVwRelation(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報グループ設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたグループを設定する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetGroupToVwRelation(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKE0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'グループが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwKankei.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("グループCD") = _
                                .PropVwKankei.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwKankei.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwKankei.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_GROUP      '区分：グループ
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("グループCD")                                       'ID
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                                .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名

                            'ユーザ名の背景色を濃灰色にする
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwKankei, _
                                                      0, .PropVwKankei.Sheets(0).RowCount, 0, _
                                                      1, .PropVwKankei.Sheets(0).ColumnCount) = False Then
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
    ''' 関係者情報ユーザー追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索画面から取得したユーザーデータを設定する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function SetUserToVwRelationMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ユーザーデータ設定処理
        If SetUserToVwRelation(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報ユーザー設定処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧にサブ検索で選択されたユーザーを設定する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetUserToVwRelation(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ


        Try
            With dataHBKE0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        'ユーザーが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwKankei.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("ユーザーID") = _
                                .PropVwKankei.Sheets(0).Cells(j, COL_RELATION_ID).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwKankei.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwKankei.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_KBN).Value = KBN_USER       '区分：ユーザー
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_ID).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザーID")                                       'ID
                            '.PropVwRelation.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).Value = _
                            '    .PropDtResultSub.Rows(i).Item("グループ名")                                       'グループ名
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_USERNM).Value = _
                                .PropDtResultSub.Rows(i).Item("ユーザー氏名")                                     'ユーザー名

                            'グループ名の背景色を濃灰色にする
                            .PropVwKankei.Sheets(0).Cells(intNewRowNo, COL_RELATION_GROUPNM).BackColor = PropCellBackColorDARKGRAY

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwKankei, _
                                                      0, .PropVwKankei.Sheets(0).RowCount, 0, _
                                                      1, .PropVwKankei.Sheets(0).ColumnCount) = False Then
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
    ''' 関係者情報行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowKankeiMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowKankei(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関係者情報選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関係者情報の選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowKankei(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号
        Dim blnAddFlg As Boolean = True
        Try
            With dataHBKE0201.PropVwKankei.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        '初期化
                        blnAddFlg = True

                        '★削除対象がログイン時のグループだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_GROUP Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                'ログインユーザのIDがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_USER) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropUserId) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = E0201_E012
                                    Return False
                                End If
                            End If
                        End If

                        '★削除対象がログイン時のユーザーだった場合
                        If .GetText(i, COL_RELATION_KBN) = KBN_USER Then
                            If .GetText(i, COL_RELATION_ID).Equals(PropUserId) Then
                                'ログインユーザのグループがあるかチェック
                                For j As Integer = 0 To .Rows.Count - 1
                                    If .GetText(j, COL_RELATION_KBN).Equals(KBN_GROUP) AndAlso _
                                        .GetText(j, COL_RELATION_ID).Equals(PropWorkGroupCD) Then
                                        blnAddFlg = False
                                    End If
                                Next
                                'ない場合
                                If blnAddFlg = True Then
                                    'エラーメッセージ設定
                                    puErrMsg = E0201_E011
                                    Return False
                                End If
                            End If
                        End If

                        .Rows(i).Remove()
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
    ''' プロセスリンク行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowpLinkMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowplink(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' プロセスリンク空行追加処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクに空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowplink(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKE0201

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwprocessLinkInfo.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("mngnmb") = _
                                .PropVwprocessLinkInfo.Sheets(0).Cells(j, COL_processLINK_NO).Value AndAlso _
                                .PropDtResultSub.Rows(i).Item("ProcessKbn") = _
                                .PropVwprocessLinkInfo.Sheets(0).Cells(j, COL_processLINK_KBN).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwprocessLinkInfo.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwprocessLinkInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定

                            '取得した区分を略名で表示
                            Dim setKbn As String = ""
                            Select Case .PropDtResultSub.Rows(i).Item("processnm")
                                Case PROCESS_TYPE_INCIDENT_NAME
                                    setKbn = PROCESS_TYPE_INCIDENT_NAME_R
                                Case PROCESS_TYPE_QUESTION_NAME
                                    setKbn = PROCESS_TYPE_QUESTION_NAME_R
                                Case PROCESS_TYPE_CHANGE_NAME
                                    setKbn = PROCESS_TYPE_CHANGE_NAME_R
                                Case PROCESS_TYPE_RELEASE_NAME
                                    setKbn = PROCESS_TYPE_RELEASE_NAME_R
                            End Select

                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_KBN_NMR).Value = _
                               setKbn                                                                                   '区分(略名）
                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_NO).Value = _
                                .PropDtResultSub.Rows(i).Item("mngnmb")                                                 '番号
                            .PropVwprocessLinkInfo.Sheets(0).Cells(intNewRowNo, COL_processLINK_KBN).Value = _
                                .PropDtResultSub.Rows(i).Item("processkbn")                                             '区分CD


                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwprocessLinkInfo, _
                                                      0, .PropVwprocessLinkInfo.Sheets(0).RowCount, 0, _
                                                      1, .PropVwprocessLinkInfo.Sheets(0).ColumnCount) = False Then
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
    ''' プロセスリンク行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク一覧の選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowpLinkMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowplink(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' プロセスリンク選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンクの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowplink(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKE0201.PropVwprocessLinkInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
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
    ''' 関連ファイル行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowFileinfoMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowFileinfo(dataHBKE0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKE0201.PropVwFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKE0201.PropBtnOpenFile.Enabled = True
                dataHBKE0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKE0201.PropBtnOpenFile.Enabled = False
                dataHBKE0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関連ファイル空行追加処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルに空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowFileinfo(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKE0201



                '追加フラグ初期化
                blnAddFlg = True

                'pathと説明が既に設定済でない場合のみ追加
                For j As Integer = 0 To .PropVwFileInfo.Sheets(0).RowCount - 1

                    '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                    If .PropTxtFilePath = .PropVwFileInfo.Sheets(0).GetText(j, COL_FILE_PATH) AndAlso _
                       .PropTxtFileNaiyo = .PropVwFileInfo.Sheets(0).GetText(j, COL_FILE_NAIYO) Then
                        blnAddFlg = False
                        Exit For
                    End If

                Next

                '追加フラグがONの場合のみ追加処理を行う
                If blnAddFlg = True Then

                    '追加行番号取得
                    intNewRowNo = .PropVwFileInfo.Sheets(0).Rows.Count

                    '新規行追加
                    .PropVwFileInfo.Sheets(0).Rows.Add(intNewRowNo, 1)

                    'サブ検索画面での選択値を設定
                    .PropVwFileInfo.Sheets(0).Cells(intNewRowNo, COL_FILE_NAIYO).Value = .PropTxtFileNaiyo         '説明
                    .PropVwFileInfo.Sheets(0).Cells(intNewRowNo, COL_FILE_PATH).Value = .PropTxtFilePath           'パス
                End If



                '最終追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwFileInfo, _
                                                  0, .PropVwFileInfo.Sheets(0).RowCount, 0, _
                                                  1, .PropVwFileInfo.Sheets(0).ColumnCount) = False Then
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
    ''' 関連ファイル行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル一覧の選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowFileInfoMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowFileinfo(dataHBKE0201) = False Then
            Return False
        End If

        'データが無い場合、ボタン制御を行う
        With dataHBKE0201.PropVwFileInfo.Sheets(0)
            If .RowCount > 0 Then
                dataHBKE0201.PropBtnOpenFile.Enabled = True
                dataHBKE0201.PropBtnSaveFile.Enabled = True
            Else
                dataHBKE0201.PropBtnOpenFile.Enabled = False
                dataHBKE0201.PropBtnSaveFile.Enabled = False
            End If
        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 関連ファイル選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイルの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowFileinfo(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKE0201.PropVwFileInfo.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
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
    ''' CYSPR：行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRに空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowCYSPRMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowCYSPR(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' CYSPR：空行追加処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRに空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowCYSPR(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号

        Try
            With DataHBKE0201

                '追加行番号取得
                intNewRowNo = .PropVwCYSPR.Sheets(0).Rows.Count

                '新規行追加
                .PropVwCYSPR.Sheets(0).Rows.Add(intNewRowNo, 1)

                '最終追加行にフォーカスをセット
                If commonLogicHBK.SetFocusOnVwRow(.PropVwCYSPR, _
                                                  0, .PropVwCYSPR.Sheets(0).RowCount, 0, _
                                                  1, .PropVwCYSPR.Sheets(0).ColumnCount) = False Then
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
    ''' CYSPR：行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRの選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowCYSPRMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowCYSPR(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' CYSPR：選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPRの選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowCYSPR(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKE0201.PropVwCYSPR.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
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
    ''' 会議情報：行追加時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧に空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function AddRowMeetingMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '空行追加処理
        If AddRowMeeting(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 会議情報：空行追加処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報に空行を1行追加する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function AddRowMeeting(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '変数宣言
        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With DataHBKE0201

                '会議結果情報を取得する
                If GetMeetingResultData(DataHBKE0201) = False Then
                    Return False
                End If

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、会議情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        '追加フラグ初期化
                        blnAddFlg = True

                        '番号が既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("MeetingNmb").ToString.Equals(.PropVwMeeting.Sheets(0).GetText(j, COL_MEETING_NO)) Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwMeeting.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwMeeting.Sheets(0).Rows.Add(intNewRowNo, 1)

                            'サブ検索画面での選択値を設定
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NO).Value = _
                                .PropDtResultSub.Rows(i).Item("MeetingNmb")                                 '番号
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_JIBI).Value = _
                                .PropDtResultSub.Rows(i).Item("jisiDT")                                     '実施日
                            .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_TITLE).Value = _
                                .PropDtResultSub.Rows(i).Item("Title")                                      'タイトル

                            Dim dr() As DataRow = .PropDtResultMtg.Select("MeetingNmb='" & .PropDtResultSub.Rows(i).Item("MeetingNmb") & "'")
                            If dr.Count > 0 Then
                                '設定済みがアリ
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NIN).Value = _
                                    dr(0).Item("ResultKbnNM") '.PropDtResultSub.Rows(i).Item("ResultKbnNM")                           　'承認　
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NINCD).Value = _
                                    dr(0).Item("ResultKbn") '.PropDtResultSub.Rows(i).Item("ResultKbn")                                '承認コード
                            Else
                                '新規紐付け
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NIN).Value = ""                                '承認　
                                .PropVwMeeting.Sheets(0).Cells(intNewRowNo, COL_MEETING_NINCD).Value = "0"                             '承認コード
                            End If


                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwMeeting, _
                                                      0, .PropVwMeeting.Sheets(0).RowCount, 0, _
                                                      1, .PropVwMeeting.Sheets(0).ColumnCount) = False Then
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
    ''' 会議情報：行削除時メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報一覧の選択行を削除する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RemoveRowMeetingMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '選択行削除処理
        If RemoveRowMeeting(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 会議情報：選択行削除処理
    ''' </summary>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報の選択行を削除（Remove）する
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function RemoveRowMeeting(ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRowFrom As Integer   '選択開始行番号
        Dim intSelectedRowTo As Integer     '選択終了行番号

        Try
            With DataHBKE0201.PropVwMeeting.Sheets(0)

                '選択開始行、終了行取得
                If .Models.Selection.AnchorRow < .Models.Selection.LeadRow Then
                    intSelectedRowFrom = .Models.Selection.AnchorRow
                    intSelectedRowTo = .Models.Selection.LeadRow
                Else
                    intSelectedRowTo = .Models.Selection.AnchorRow
                    intSelectedRowFrom = .Models.Selection.LeadRow
                End If

                '一覧に行があり、かつ選択行番号が最大行数未満の場合削除処理を行う
                If .RowCount > 0 AndAlso intSelectedRowFrom < .RowCount AndAlso intSelectedRowTo < .RowCount Then

                    '開始行から終了行まで選択行を削除する（逆回し）
                    For i As Integer = intSelectedRowTo To intSelectedRowFrom Step -1
                        .Rows(i).Remove()
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
    ''' 会議情報：会議情報データ取得処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議番号をキーに会議結果情報を取得する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResultData(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            '会議情報データ取得
            If GetMeetingResult(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 会議情報：会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報データを取得する
    ''' <para>作成情報：2012/08/14 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetMeetingResult(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectMeetingSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtResultMtg = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function




    ''' <summary>
    ''' 【登録ボタン】コントロール入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/18 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputValueMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'コントロール入力チェック
        If CheckInputValue(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【登録ボタン】コントロール入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>登録項目の入力チェックを行う
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputValue(ByRef dataHBKE0201 As DataHBKE0201) As Boolean
        Dim blnStateKanryo As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201


                'ステータスの確認
                With .PropCmbprocessStateCD
                    '完了の場合
                    If .SelectedValue = PROCESS_STATUS_CHANGE_KANRYOU Then
                        '完了フラグ
                        blnStateKanryo = True
                    End If
                End With

                '?:.ステータスの入力チェック(必須)
                With .PropCmbprocessStateCD
                    '未入力の場合、エラー
                    If .SelectedValue = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E003
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With


                '1:.開始日時の入力チェック（必須）
                With .PropDtpKaisiDT
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .txtDate.Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E004
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        'エラーを返す
                        Return False
                    End If
                End With

                '開始日の時分入力チェック
                If .PropDtpKaisiDT.txtDate.Text.Trim() <> "" And .PropTxtKaisiDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = E0201_E019
                    'タブを基本情報タブに設定
                    dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtKaisiDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '開始日の日付入力チェック
                If .PropDtpKaisiDT.txtDate.Text.Trim() = "" And .PropTxtKaisiDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = E0201_E018
                    'タブを基本情報タブに設定
                    dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpKaisiDT.Focus()
                    'エラーを返す
                    Return False
                End If

                '完了日の時分入力チェック
                If .PropDtpKanryoDT.txtDate.Text.Trim() <> "" And .PropTxtKanryoDT_HM.PropTxtTime.Text = "" Then
                    'エラーメッセージ設定
                    puErrMsg = E0201_E021
                    'タブを基本情報タブに設定
                    dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropTxtKanryoDT_HM.Focus()
                    'エラーを返す
                    Return False
                End If

                '完了日の日付入力チェック
                If .PropDtpKanryoDT.txtDate.Text.Trim() = "" And .PropTxtKanryoDT_HM.PropTxtTime.Text <> "" Then
                    'エラーメッセージ設定
                    puErrMsg = E0201_E020
                    'タブを基本情報タブに設定
                    dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                    'フォーカス設定
                    .PropDtpKaisiDT.Focus()
                    'エラーを返す
                    Return False
                End If


                '3:.タイトルの入力チェック(必須)
                With .PropTxtTitle
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E006
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '4:.内容の入力チェック(必須)
                With .PropTxtNaiyo
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E007
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                '5:.対象システムの入力チェック(必須)
                With .PropCmbSystemNmb
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .PropTxtDisplay.Text = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E005
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()

                        'エラーを返す
                        Return False
                    End If
                End With
                '6:.担当グループの入力チェック(必須)
                With .PropCmbTantoGrpCD
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E008
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '7:.担当IDの入力チェック(必須)
                With .PropTxtTantoID
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E009
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With
                '8:.担当氏名の入力チェック(必須)
                With .PropTxtTantoNM
                    '未入力の場合、エラー
                    If blnStateKanryo AndAlso .Text.Trim() = "" Then
                        'エラーメッセージ設定
                        puErrMsg = E0201_E010
                        'タブを基本情報タブに設定
                        dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                        'フォーカス設定
                        .Focus()
                        .SelectAll()
                        'エラーを返す
                        Return False
                    End If
                End With

                'CYSPR情報重複チェック

                With .PropVwCYSPR.Sheets(0)
                    Dim dt As DataTable = .DataSource
                    '削除情報などはコミットしておく
                    dt.AcceptChanges()

                    '1行以上ある場合、チェックを行う
                    If dt.Rows.Count > 0 Then

                        '一覧の行数分繰り返し
                        For i As Integer = 0 To dt.Rows.Count - 1
                            Dim ct As Integer = 0
                            'ブランク以外のデータで
                            If dt.Rows(i).Item(0).ToString <> "" Then
                                For j As Integer = 0 To dt.Rows.Count - 1
                                    If dt.Rows(i).Item(0).Equals(dt.Rows(j).Item(0)) Then
                                        ct += 1
                                    End If
                                Next
                                '?:.重複チェック
                                If ct > 1 Then
                                    'エラーメッセージ設定
                                    puErrMsg = E0201_E015
                                    'タブを基本情報タブに設定
                                    dataHBKE0201.PropTbInput.SelectedIndex = TAB_KHN
                                    'フォーカス設定
                                    If commonLogicHBK.SetFocusOnVwRow(dataHBKE0201.PropVwCYSPR, _
                                                                      0, i, COL_CYSPR_NO, 1, .ColumnCount) = False Then
                                        Return False
                                    End If
                                    'エラーを返す
                                    Return False
                                End If
                            End If
                        Next i

                    End If

                End With

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
    ''' 【DB更新中断時】メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の内容をログに出力し、フォームコントロールを再設定する
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function OutputUnlockLogMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '入力内容ログ出力
        If OutputUnlockLog(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 【DB更新中断時】ログ出力処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>他のユーザによりロックが解除された場合に編集中の入力内容をログに出力する
    ''' <para>作成情報：2012/08/22 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function OutputUnlockLog(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '定数宣言
        Const SEP_HF_SPC As String = " "      '半角スペース
        'Const SEP_HF_POD As String = "."      '半角ピリオド
        'Const SEP_HF_CRN As String = ":"      '半角コロン

        ''変数宣言
        Dim strPlmList As New List(Of String)       'フォーマット埋込用パラメータ配列
        Dim strLogFilePath As String = ""           'ログファイルパス
        Dim strLogFileName As String                'ログファイル名
        Dim strOutputDir As String                  'ログ出力フォルダ

        Dim strText_cyspr As String = ""            'CYSPRパラメータ文
        Dim strText_Meeting As String = ""          '会議情報パラメータ文
        Dim strText_Relation As String = ""         '関係者情報パラメータ文
        Dim strText_PLink As String = ""            'プロセスリンクパラメータ文
        Dim strText_File As String = ""             '関連ファイルパラメータ文

        Dim sw As StreamWriter = Nothing            'ファイル書込用クラス

        Try
            With dataHBKE0201

                '書込用テキスト作成

                '【インシデント基本情報】------------------------------------
                strPlmList.Add(.PropTxtNmb.Text)                                '0:番号

                '【基本情報】--------------------------------------
                strPlmList.Add(.PropCmbprocessStateCD.Text)                     '1:ステータス
                strPlmList.Add(.PropDtpKaisiDT.txtDate.Text)                    '2:開始日時
                strPlmList.Add(.PropDtpKanryoDT.txtDate.Text)                   '3:完了日時
                strPlmList.Add(.PropCmbSystemNmb.txtDisplay.Text)               '4:対象システム
                strPlmList.Add(.PropCmbTantoGrpCD.Text)                         '5:担当グループ
                strPlmList.Add(.PropTxtTantoID.Text)                            '6:担当ID
                strPlmList.Add(.PropTxtTantoNM.Text)                            '7:担当氏名
                strPlmList.Add(.PropTxtTitle.Text)                              '8:タイトル
                strPlmList.Add(.PropTxtNaiyo.Text)                              '9:内容
                strPlmList.Add(.PropTxtTaisyo.Text)                             '10:対処
                strPlmList.Add(.PropTxthenkouID.Text)                           '11:変更承認者ID
                strPlmList.Add(.PropTxthenkouNM.Text)                           '12:変更承認者氏名
                strPlmList.Add(.PropTxtsyoninID.Text)                           '13:承認記録者ID
                strPlmList.Add(.PropTxtsyoninNM.Text)                           '14:承認記録者氏名

                '15:【会議情報】--------------------------------------
                If .PropVwMeeting.Sheets(0).RowCount > 0 Then
                    With .PropVwMeeting.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「番号」
                            '「実施日
                            '「タイトル」
                            '「承認」
                            strText_Meeting &= (i + 1).ToString() & ":" & .GetText(i, COL_MEETING_NO)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_JIBI)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_TITLE)
                            strText_Meeting &= SEP_HF_SPC & .GetText(i, COL_MEETING_NIN)
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Meeting &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Meeting)

                '【フリー入力情報】--------------------------------
                strPlmList.Add(.PropTxtBIko1.Text)            '16:フリーテキスト１
                strPlmList.Add(.PropTxtBIko2.Text)            '17:フリーテキスト２
                strPlmList.Add(.PropTxtBIko3.Text)            '18:フリーテキスト３
                strPlmList.Add(.PropTxtBIko4.Text)            '19:フリーテキスト４
                strPlmList.Add(.PropTxtBIko5.Text)            '20:フリーテキスト５

                '21～25:フリーフラグ１～５
                If .PropChkFreeFlg1.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg2.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg3.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg4.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If
                If .PropChkFreeFlg5.Checked = True Then
                    strPlmList.Add(FREE_FLG_ON_NM)
                Else
                    strPlmList.Add(FREE_FLG_OFF_NM)
                End If

                '26:【対応関係者情報】--------------------------------
                If .PropVwKankei.Sheets(0).RowCount > 0 Then
                    With .PropVwKankei.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「ID」
                            '「グループ名」
                            '「ユーザー名」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_KBN), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_ID), "")
                            Dim strNM As String = ""
                            If strKbn = KBN_GROUP Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_GROUPNM), "")
                            ElseIf strKbn = KBN_USER Then
                                strNM = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_RELATION_USERNM), "")
                            End If
                            strText_Relation &= (i + 1).ToString() & "." & strKbn & " " & strID & " " & strNM
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_Relation &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_Relation)

                '27:【プロセスリンク情報】--------------------------------
                If .PropVwprocessLinkInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwprocessLinkInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「区分」
                            '「番号」
                            Dim strKbn As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_processLINK_KBN_NMR), "")
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_processLINK_NO), "")
                            strText_PLink &= (i + 1).ToString() & "." & strKbn & " " & strID
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_PLink &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_PLink)

                '28:【CYSPR情報】--------------------------------
                If .PropVwCYSPR.Sheets(0).RowCount > 0 Then
                    With .PropVwCYSPR.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「番号」
                            Dim strID As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_CYSPR_NO), "")
                            strText_cyspr &= (i + 1).ToString() & "." & strID
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_cyspr &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_cyspr)

                '29:【関連ファイル情報】--------------------------------
                If .PropVwFileInfo.Sheets(0).RowCount > 0 Then
                    With .PropVwFileInfo.Sheets(0)
                        '一覧行数分繰り返し、パラメータ文を作成
                        For i As Integer = 0 To .RowCount - 1
                            '以下の書式で出力
                            '「説明」
                            '「登録日時」
                            Dim strNaiyo As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_FILE_NAIYO), "")
                            Dim strRegdt As String = commonLogicHBK.ChangeNothingToStr(.Cells(i, COL_FILE_REGDT), "")
                            strText_File &= (i + 1).ToString() & "." & strNaiyo & " " & strRegdt
                            '最終行以外は改行コード追加
                            If i < .RowCount - 1 Then
                                strText_File &= vbCrLf
                            End If
                        Next
                    End With
                End If
                strPlmList.Add(strText_File)


                'ログ出力フォルダ設定
                strOutputDir = Path.Combine(Application.StartupPath, OUTPUT_DIR_UNLOCKEDLOG)

                'システム日付を取得
                If GetSysdate(dataHBKE0201) = False Then
                    Return False
                End If

                'ログファイル名設定
                strLogFileName = Format(.PropDtmSysDate, "yyyyMMddHHmmss") & ".log"
                'strLogFileName = Format(DateTime.Parse(.PropDtLock.Rows(0).Item("SysTime")), "yyyyMMddHHmmss") & ".log"

                'ファイル出力を実行し、出力ファイルパスを取得
                If commonLogicHBK.OutputLogFromTextFormat(strPlmList, strLogFileName, _
                                                          FORMAT_DIR_UNLOCKEDLOG, _
                                                          FILE_UNLOCKLOG_CHANGE, _
                                                          strOutputDir, _
                                                          strLogFilePath) = False Then
                    Return False
                End If

                'データクラスにメッセージをセット
                dataHBKE0201.PropStrBeUnlockedMsg = String.Format(E0201_W001, strLogFilePath)

                'システムエラー時は以下を設定
                If puErrMsg.StartsWith(HBK_E001) Then
                    dataHBKE0201.PropStrBeUnlockedMsg = String.Format(E0201_E014, strLogFilePath)
                End If

                'ログファイルパスをプロパティにセット(出力メッセージのメッセージボックススタイル判定用)
                dataHBKE0201.PropStrLogFilePath = strLogFilePath

            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            If sw IsNot Nothing Then
                sw.Close()
            End If
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            If sw IsNot Nothing Then
                sw.Dispose()
            End If
        End Try

    End Function

    ''' <summary>
    ''' システム日付取得
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システム日付を取得する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysdate(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'B-2-1システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKE0201) = False Then
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
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' A【新規登録モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnNewModeMain(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'A-1登録前対応関係者処理
        If GetDtSysKankei(dataHBKE0201) = False Then
            Return False
        End If

        'A-2新規登録処理
        If InsertNewData(dataHBKE0201) = False Then
            Return False
        End If


        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' A-1.【共通】登録前対応関係者処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係者情報を確認する
    ''' <para>作成情報：2012/08/30 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDtSysKankei(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try
            'コネクションを開く
            Cn.Open()

            'A-1-1対象システム関係者データ取得
            If GetSysKankei(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If


            'A-1-2対象システム変更チェック
            If CheckSysNmb(Adapter, Cn, dataHBKE0201) = False Then
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
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-1-1.【共通】対象システム関係者データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムのCI番号から関係データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetSysKankei(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKE0201.GetChkKankeiSysData(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システム関係取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            '取得データをデータクラスにセット
            dataHBKE0201.PropDtResultSub = dtmst


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
            dtmst.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-1-2.【共通】対象システム変更チェック
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対象システムが変更されたかチェックする
    ''' <para>作成情報：2012/10/11 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckSysNmb(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtmst As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKE0201.GetChkSysNmbData(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対象システムの変更有無情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtmst)


            If dtmst IsNot Nothing AndAlso dtmst.Rows.Count > 0 Then
                If dtmst.Rows(0).Item(0).ToString.Equals(dataHBKE0201.PropCmbSystemNmb.PropCmbColumns.SelectedValue.ToString) Then
                    dataHBKE0201.PropBlnCheckSystemNmb = False
                Else
                    '更新前と対象システムが違う場合True
                    dataHBKE0201.PropBlnCheckSystemNmb = True
                End If
            Else
                dataHBKE0201.PropBlnCheckSystemNmb = False
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
            dtmst.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' A-2.【新規登録モード】データ新規登録処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertNewData(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

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

            'A-2-1新規番号、システム日付取得（SELECT）
            If SelectNewNmbAndSysDate(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            'A-2-3共通情報新規登録（INSERT）
            If InsertMainInfo(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-4対応関係者情報新規登録（INSERT）
            If InsertKankei(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-5プロセスリンク新規登録（INSERT）
            If InsertPlink(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-6関連ファイル情報新規登録（INSERT）
            If InsertFile(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-7CYSPR情報新規登録（INSERT）
            If InsertCyspr(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-1新規ログNo取得
            If GetNewLogNo(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-2共通ログテーブル登録
            If InserMainInfoL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-3対応者情報ログテーブル登録
            If InsertKankeiL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-4プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            'A-2-8-6関連ファイルログテーブル登録
            If InsertFileL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-7CYSPRログテーブル登録
            If InsertCYSPRL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
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
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-2-1.【新規登録／編集モード】新規番号取得処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番した番号を取得（SELECT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectNewNmbAndSysDate(ByVal Cn As NpgsqlConnection, _
                                              ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dtResult As New DataTable

        Try
            '新規番号取得（SELECT）用SQLを作成
            If sqlHBKE0201.SetSelectNewNmbAndSysDateSql(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規番号、システム日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtResult)

            'データが取得できた場合、データクラスに取得データをセット
            If dtResult.Rows.Count > 0 Then
                dataHBKE0201.PropIntChgNmb = dtResult.Rows(0).Item("chgnmb")      '新規番号
                dataHBKE0201.PropDtmSysDate = dtResult.Rows(0).Item("SysDate")  'サーバー日付
            Else
                '取得できなかったときはエラー
                'puErrMsg = E0201_E013
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
            dtResult.Dispose()
            Adapter.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' A-2-3.【新規登録／編集モード】共通情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を共通情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMainInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報新規登録（INSERT）用SQLを作成
            If sqlHBKE0201.SetInsertMainInfoSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通情報新規登録", Nothing, Cmd)

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
    ''' A-2-4.【新規登録／編集モード】関係者情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関係者情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKankei(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim blnAddFlg As Boolean = True
        Dim DtVwKankei As New DataTable       'スプレッドデータ一時保存用

        Try

            With dataHBKE0201
                'スプレッドのデータソースを取得
                DtVwKankei = .PropVwKankei.DataSource
                DtVwKankei.AcceptChanges()

                '★新規登録時のみ
                If .PropStrProcMode = PROCMODE_NEW Then
                    'ログインユーザのグループがあるかチェック
                    For i As Integer = 0 To DtVwKankei.Rows.Count - 1
                        If DtVwKankei.Rows(i).Item("RelationID").Equals(PropWorkGroupCD) Then
                            blnAddFlg = False
                        End If
                    Next
                    'ない場合追加
                    If blnAddFlg = True Then
                        Dim row As DataRow = DtVwKankei.NewRow
                        row.Item("RelationKbn") = KBN_GROUP
                        row.Item("RelationID") = PropWorkGroupCD
                        DtVwKankei.Rows.Add(row)
                    End If
                End If

                '★新規登録時、または対象システムに変更があった場合
                If .PropStrProcMode = PROCMODE_NEW Or .PropBlnCheckSystemNmb = True Then
                    '取得した関係テーブルがあればチェックする
                    If .PropDtResultSub IsNot Nothing Then
                        For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                            '追加フラグ初期化
                            blnAddFlg = True

                            '関係テーブルのグループがあるかチェック
                            If .PropDtResultSub.Rows(i).Item("relationkbn").Equals(KBN_GROUP) Then
                                For j As Integer = 0 To DtVwKankei.Rows.Count - 1
                                    If DtVwKankei.Rows(j).Item("relationkbn") = KBN_GROUP Then
                                        If DtVwKankei.Rows(j).Item("RelationID").Equals(.PropDtResultSub.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwKankei.NewRow
                                    row.Item("RelationKbn") = KBN_GROUP
                                    row.Item("RelationID") = .PropDtResultSub.Rows(i).Item("RelationID")
                                    DtVwKankei.Rows.Add(row)
                                End If

                            ElseIf .PropDtResultSub.Rows(i).Item("relationkbn").Equals(KBN_USER) Then
                                '関係テーブルのユーザがあるかチェック
                                For j As Integer = 0 To DtVwKankei.Rows.Count - 1
                                    If DtVwKankei.Rows(j).Item("relationkbn") = KBN_USER Then
                                        If DtVwKankei.Rows(j).Item("RelationID").Equals(.PropDtResultSub.Rows(i).Item("RelationID")) Then
                                            blnAddFlg = False
                                            Exit For
                                        End If
                                    End If
                                Next

                                'ない場合追加
                                If blnAddFlg = True Then
                                    Dim row As DataRow = DtVwKankei.NewRow
                                    row.Item("RelationKbn") = KBN_USER
                                    row.Item("RelationID") = .PropDtResultSub.Rows(i).Item("RelationID")
                                    DtVwKankei.Rows.Add(row)
                                End If
                            End If
                        Next

                    End If
                End If

                '修正した関係者のテーブルにて
                For i As Integer = 0 To DtVwKankei.Rows.Count - 1

                    '登録行作成
                    Dim row As DataRow = DtVwKankei.Rows(i)
                    'row.Item("RelationKbn") = DtVwKankei.Rows(i).Item(0)        'G,U(KBN_GROUP,KBN_USER)
                    'row.Item("RelationID") = DtVwKankei.Rows(i).Item(1)         '3ケタ,7ケタ

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '関係者情報新規登録（INSERT）用SQLを作成
                    If sqlHBKE0201.SetInsertKankeiSql(Cmd, Cn, dataHBKE0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係者情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-2-5.【新規登録／編集モード】プロセスリンク登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をプロセスリンク情報テーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPlink(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim row As DataRow
        Dim cnt As Integer
        Try
            With dataHBKE0201

                'データテーブルを取得
                .PropDtprocessLink = DirectCast(.PropVwprocessLinkInfo.Sheets(0).DataSource, DataTable)

                If .PropDtprocessLink IsNot Nothing Then

                    If .PropDtprocessLink.Rows.Count > 0 Then

                        'データ数分繰り返し、登録処理を行う 
                        For i As Integer = 0 To .PropDtprocessLink.Rows.Count - 1

                            row = .PropDtprocessLink.Rows(i)

                            .PropRowReg = row

                            'データの追加／削除状況に応じて新規登録／削除処理を行う
                            If row.RowState = DataRowState.Added Then           '追加時

                                '登録順カウンタ
                                cnt += 1

                                '新規登録
                                If sqlHBKE0201.InsertPLinkMoto(Cmd, Cn, dataHBKE0201, cnt) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報新規登録", Nothing, Cmd)


                            ElseIf row.RowState = DataRowState.Deleted Then     '削除時

                                '削除
                                If sqlHBKE0201.DeletePLinkMoto(Cmd, Cn, dataHBKE0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報削除", Nothing, Cmd)

                                '削除
                                If sqlHBKE0201.DeletePLinkSaki(Cmd, Cn, dataHBKE0201) = False Then
                                    Return False
                                End If

                                'SQL実行
                                Cmd.ExecuteNonQuery()

                                'ログ出力
                                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(先)情報削除", Nothing, Cmd)

                            End If


                            '行の変更をコミット
                            'row.AcceptChanges()

                        Next

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
        Finally
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-2-6.【新規登録／編集モード】関連ファイル新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFile(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            With dataHBKE0201

                '最新のファイル情報データテーブルを取得
                .PropDtFileInfo = DirectCast(.PropVwFileInfo.Sheets(0).DataSource, DataTable)

                If .PropDtFileInfo IsNot Nothing Then

                    '関連ファイルアップロード／登録
                    Dim aryStrNewDirPath As New ArrayList
                    If commonLogicHBK.UploadAndRegFile(Adapter, Cn, _
                                                    .PropIntChgNmb, _
                                                    .PropDtFileInfo, _
                                                    .PropDtmSysDate, _
                                                    UPLOAD_FILE_CHANGE, _
                                                    aryStrNewDirPath) = False Then
                        Return False
                    End If

                End If

            End With

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関係ファイル登録", Nothing, Cmd)


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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-2-7.【新規登録／編集モード】CYSPR新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容を関連ファイルテーブルに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCyspr(ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKE0201
                'データテーブルを取得
                '入力チェックでコミットしているので注意
                .PropDtCyspr = DirectCast(.PropVwCYSPR.Sheets(0).DataSource, DataTable)

                For i As Integer = 0 To .PropDtCyspr.Rows.Count - 1

                    'ブランクは除外する
                    If .PropDtCyspr.Rows(i).Item(0).ToString <> "" Then
                        '登録行作成
                        Dim row As DataRow = .PropDtCyspr.Rows(i)

                        '作成した行をデータクラスにセット
                        .PropRowReg = row

                        'CYSPR情報新規登録（INSERT）用SQLを作成
                        If sqlHBKE0201.SetInsertCysprSql(Cmd, Cn, dataHBKE0201) = False Then
                            Return False
                        End If

                        'ログ出力
                        commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CYSPR情報新規登録", Nothing, Cmd)

                        'SQL実行
                        Cmd.ExecuteNonQuery()
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
        Finally
            Adapter.Dispose()
            Cmd.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' A-2-8-1.【共通】新規ログNo取得
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewLogNo(ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Adapter As New NpgsqlDataAdapter
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKE0201.SetSelectNewLogNoSql(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKE0201.PropIntLogNo = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = E0201_E013
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
            dLogNo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' A-2-8-2.【共通】共通情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>共通情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMainInfoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertMainInfoLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-3.【共通】対応関係情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>対応関係情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertKankeiL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertKankeiLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-4.【共通】プロセスリンク情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertPLinkmotoL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertPLinkmotoLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク(元)情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-6.【共通】関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>関連ファイル情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertFileLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "関連ファイル情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-7.【共通】CYSPR情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>CYSPR情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertCYSPRL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertCYSPRLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CYSPR情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-8.【共通】新規ログNo（会議用）取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapter</param>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>新規に採番したログNoを取得する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetNewMeetingLogNo(ByVal Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dLogNo As New DataTable         'ログNo格納用テーブル

        Try

            'SQLを作成
            If sqlHBKE0201.SetSelectNewMeetingLogNoSql(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "新規ログNo（会議用）取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dLogNo)

            'データが取得できた場合、データクラスにログNoをセット
            If dLogNo.Rows.Count > 0 Then
                dataHBKE0201.PropIntLogNoSub = dLogNo.Rows(0).Item("LogNo")
            Else
                '取得できなかった場合はエラー
                puErrMsg = E0201_E013
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
    ''' A-2-8-9.【共通】会議情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InserMeetingL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertMeetingLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-10【共通】会議結果情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResultL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertMtgResultLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-11【共通】会議出席者情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInsertMtgAttendL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertMtgAttendLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議出席者情報ログ新規登録", Nothing, Cmd)

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
    ''' A-2-8-12【共通】会議関連ファイル情報ログテーブル登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報ログテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetInsertMtgFileL(ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            'SQLを作成
            If sqlHBKE0201.SetInsertMtgFileLSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議関連ファイル情報ログ新規登録", Nothing, Cmd)

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
    ''' B【編集モード】データ登録メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をデータベースに反映する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RegistDataOnEditModeMain(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'A-1登録前対応関係者処理
        If GetDtSysKankei(dataHBKE0201) = False Then
            Return False
        End If

        'B-2更新処理
        If UpdateData(dataHBKE0201) = False Then
            Return False
        End If

        'C-1ロック解除処理
        If UnlockData(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' B-2.【編集モード】データ更新処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容をDBに新規登録（INSERT）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateData(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter()    'アダプタ
        Dim Tsx As NpgsqlTransaction = Nothing    'トランザクション

        Try
            'コネクションを開く
            Cn.Open()

            'トランザクションレベルを設定し、トランザクションを開始する
            Tsx = Cn.BeginTransaction(IsolationLevel.Serializable)

            'B-2-1システム日付取得（SELECT）
            If SelectSysDate(Adapter, Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'B-2-3共通情報更新（UPDATE）
            If UpdateMainInfo(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            '担当履歴情報チェック
            If InsertTantoRireki(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'B-2-4対応関係者情報 削除（DELETE）
            If Deletekankei(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            'A-2-4対応関係者情報新規登録（INSERT）
            If InsertKankei(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-5プロセスリンク新規登録（DELETE/INSERT）
            If InsertPlink(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-6関連ファイル情報登録（DELETE/INSERT）
            If InsertFile(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'B-2-7CYSPR情報 削除（DELETE）
            If DeleteCyspr(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            'A-2-7CYSPR情報登録
            If InsertCyspr(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'B-2-8会議結果情報 削除（DELETE）
            If DeleteMtgResult(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If
            'B-2-9会議結果情報新規登録（INSERT）
            If InsertMtgResult(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-1新規ログNo取得
            If GetNewLogNo(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-2共通ログテーブル登録
            If InserMainInfoL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-3対応者情報ログテーブル登録
            If InsertKankeiL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-4プロセスリンク(元)ログテーブル登録
            If InsertPLinkmotoL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-6関連ファイルログテーブル登録
            If InsertFileL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If

            'A-2-8-7CYSPRログテーブル登録
            If InsertCYSPRL(Cn, dataHBKE0201) = False Then
                'ロールバック
                If Tsx IsNot Nothing Then
                    Tsx.Rollback()
                End If
                Return False
            End If


            For i As Integer = 0 To dataHBKE0201.PropVwMeeting.Sheets(0).Rows.Count - 1
                '会議番号
                dataHBKE0201.PropIntMeetingNmb = dataHBKE0201.PropVwMeeting.Sheets(0).GetText(i, COL_MEETING_NO)

                'A-2-8-8新規ログNo(会議用)取得
                If GetNewMeetingLogNo(Adapter, Cn, dataHBKE0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'A-2-8-9会議情報ログテーブル登録
                If InserMeetingL(Cn, dataHBKE0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'A-2-8-10会議結果ログテーブル登録
                If InsertMtgResultL(Cn, dataHBKE0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'A-2-8-11会議出席者ログテーブル登録
                If SetInsertMtgAttendL(Cn, dataHBKE0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If

                'A-2-8-12会議関連ファイルログテーブル登録
                If SetInsertMtgFileL(Cn, dataHBKE0201) = False Then
                    'ロールバック
                    If Tsx IsNot Nothing Then
                        Tsx.Rollback()
                    End If
                    Return False
                End If
            Next

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
            If Tsx IsNot Nothing Then
                Tsx.Dispose()
            End If
            'コネクションが閉じられていない場合は閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Adapter.Dispose()
            Cn.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' B-2-1.【編集モード】サーバー日付取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>更新用のサーバー日付を取得する
    ''' <para>作成情報：2012/08/27 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SelectSysDate(ByRef Adapter As NpgsqlDataAdapter, _
                                   ByVal Cn As NpgsqlConnection, _
                                   ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSysDate As New DataTable         '履歴番号格納用テーブル

        Try
            '*************************************
            '* サーバー日付取得
            '*************************************

            'SQLを作成
            If sqlHBKE0201.SetSelectSysDateSql(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "サーバー日付取得", Nothing, Adapter.SelectCommand)

            'SQL実行
            Adapter.Fill(dtSysDate)

            'データが取得できた場合、データクラスに履歴番号をセット
            If dtSysDate.Rows.Count > 0 Then
                dataHBKE0201.PropDtmSysDate = dtSysDate.Rows(0).Item("SysDate")
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
    ''' B-2-3.【編集モード】共通情報 更新処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で共通情報テーブルを更新（UPDATE）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function UpdateMainInfo(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '共通情報更新（UPDATE）用SQLを作成
            If sqlHBKE0201.SetUpdateMainInfoSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "共通情報更新", Nothing, Cmd)

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
    ''' B-2-4.【編集モード】対応関連者情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で対応関係者情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function Deletekankei(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try
            '共通情報更新（UPDATE）用SQLを作成
            If sqlHBKE0201.SetDeletekankeiSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "対応関係者情報物理削除", Nothing, Cmd)

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
    ''' B-2-7.【編集モード】CYSPR情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容でCYSPR情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteCyspr(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try



            'CYSPR情報更新（Update）用SQLを作成
            If sqlHBKE0201.SetDeleteCysprSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "CYSPR情報物理削除", Nothing, Cmd)

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
    ''' B-2-8.【編集モード】会議結果情報　削除処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを削除（delete）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function DeleteMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            '会議結果情報更新（Update）用SQLを作成
            If sqlHBKE0201.SetDeleteMtgResultSql(Cmd, Cn, dataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報物理削除", Nothing, Cmd)

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
    ''' B-2-9.【編集モード】会議情報　登録処理  
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>入力内容で会議結果情報テーブルを更新（Update）する
    ''' <para>作成情報：2012/08/19 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertMtgResult(ByVal Cn As NpgsqlConnection, _
                                  ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド

        Try

            With dataHBKE0201
                '会議情報一覧の行数分繰り返し、更新処理を行う
                For i As Integer = 0 To .PropVwMeeting.Sheets(0).RowCount - 1

                    '登録行作成
                    Dim row As DataRow = .PropDtMeeting.NewRow
                    row.Item("MeetingNmb") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_NO)
                    row.Item("ResultKbn") = .PropVwMeeting.Sheets(0).GetValue(i, COL_MEETING_NINCD)

                    '作成した行をデータクラスにセット
                    .PropRowReg = row

                    '会議結果情報追加（insert）用SQLを作成
                    If sqlHBKE0201.SetInsertMtgResultSql(Cmd, Cn, dataHBKE0201) = False Then
                        Return False
                    End If

                    'ログ出力
                    commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議結果情報新規登録", Nothing, Cmd)

                    'SQL実行
                    Cmd.ExecuteNonQuery()

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
        Finally
            Cmd.Dispose()
        End Try

    End Function



    ''' <summary>
    ''' 【編集／参照／作業履歴モード】担当履歴情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴情報データを取得する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetTantoRireki(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtINCInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectTantoRirekiSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtINCInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtTantoRireki = dtINCInfo


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
            dtINCInfo.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【編集／参照モード】担当履歴作成処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]問題登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作業履歴データを作成する
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTantoRireki(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '初期化
            Dim strTantoRirekiSplit As String = "←"
            dataHBKE0201.PropTxtGrpHistory.Text = ""
            dataHBKE0201.PropTxtTantoHistory.Text = ""

            '担当履歴
            With dataHBKE0201.PropDtTantoRireki
                If .Rows.Count > 0 Then
                    For i As Integer = 0 To .Rows.Count - 1
                        If i = 0 Then
                            dataHBKE0201.PropTxtGrpHistory.Text &= .Rows(i).Item("tantogrpnm")
                            dataHBKE0201.PropTxtTantoHistory.Text &= .Rows(i).Item("chgtantonm")
                        Else
                            'ＧＰ
                            If Not .Rows(i - 1).Item("tantogrpnm").Equals(.Rows(i).Item("tantogrpnm")) Then
                                dataHBKE0201.PropTxtGrpHistory.Text &= strTantoRirekiSplit & .Rows(i).Item("tantogrpnm")
                            End If
                            'ＩＤ
                            If Not .Rows(i - 1).Item("chgtantonm").Equals(.Rows(i).Item("chgtantonm")) Then
                                dataHBKE0201.PropTxtTantoHistory.Text &= strTantoRirekiSplit & .Rows(i).Item("chgtantonm")
                            End If
                        End If
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
    ''' 【新規／編集モード】担当履歴情報新規登録処理
    ''' </summary>
    ''' <param name="Cn">[IN]NpgsqlConnection</param>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>担当履歴判定チェックをする
    ''' <para>作成情報：2012/09/10 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InsertTantoRireki(ByVal Cn As NpgsqlConnection, ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cmd As New NpgsqlCommand            'SQLコマンド
        Dim bln_chk_flg As Boolean = False

        Try
            '担当履歴、担当グループチェック処理
            'PropDtTantoRirekiは履歴を降順にしているのでROWは0を設定する

            '最終更新GPを取得 (tantorirekinmb Max)
            With dataHBKE0201.PropTxtGrpHistory

                If dataHBKE0201.PropDtTantoRireki IsNot Nothing AndAlso dataHBKE0201.PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If dataHBKE0201.PropDtTantoRireki.Rows(0).Item("tantogrpnm").ToString.Equals(dataHBKE0201.PropCmbTantoGrpCD.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If dataHBKE0201.PropCmbTantoGrpCD.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If

            End With


            '最終更新IDを取得
            With dataHBKE0201.PropTxtTantoHistory

                If dataHBKE0201.PropDtTantoRireki IsNot Nothing AndAlso dataHBKE0201.PropDtTantoRireki.Rows.Count > 0 Then
                    '現更新者と同じかチェック
                    If dataHBKE0201.PropDtTantoRireki.Rows(0).Item("chgtantonm").ToString.Equals(dataHBKE0201.PropTxtTantoNM.Text) = False Then
                        bln_chk_flg = True
                    End If
                Else
                    'まだ登録がない
                    If dataHBKE0201.PropTxtTantoNM.Text.Length > 0 Then
                        bln_chk_flg = True
                    End If
                End If

            End With

            '変更があった場合は登録する。
            If bln_chk_flg = True Then
                '担当履歴報新規登録（INSERT）用SQLを作成
                If sqlHBKE0201.SetInsertTantoRirekiSql(Cmd, Cn, dataHBKE0201) = False Then
                    Return False
                End If

                'ログ出力
                commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "担当履歴情報 新規登録", Nothing, Cmd)

                'SQL実行
                Cmd.ExecuteNonQuery()

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
            Cmd.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【共通】開くボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileOpenMain(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKE0201) = False Then
            Return False
        End If

        'ファイル表示処理
        If FileLoad(dataHBKE0201) = False Then
            Return False
        End If

        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        Return True

    End Function


    ''' <summary>
    ''' 【共通】ダウンロードボタン押下時の処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function FileDownLoadMain(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'ファイルパス取得処理
        If GetOpenFilePath(dataHBKE0201) = False Then
            Return False
        End If

        'ファイルダウンロード処理
        If FileDownLoad(dataHBKE0201) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' ファイルパス取得処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>選択中の会議ファイルパスを習得する
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetOpenFilePath(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKE0201

                '選択行のファイルパスを取得し、データクラスにセット
                .PropStrSelectedFilePath = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_PATH).Value

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
        End Try

    End Function

    ''' <summary>
    ''' ファイルを開く処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルを開く
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileLoad(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名

        Try

            With dataHBKE0201

                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKE0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_MNGNMB).Value

                '一時フォルダパス設定
                Dim strOutputDir As String = Path.Combine(Application.StartupPath, OUTPUT_DIR_TEMP)
                'ダウンロードファイル名設定
                Dim strDLFileName As String = Path.GetFileNameWithoutExtension(strFilePath) & _
                                "_" & Now().ToString("yyyyMMddmmss") & Path.GetExtension(strFilePath)

                'ダウンロードファイルパス設定
                Dim strDLFilePath As String = Path.Combine(strOutputDir, strDLFileName)


                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                    'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If


                'ファイルをネットワークドライブより一時フォルダにコピー
                Directory.CreateDirectory(strOutputDir)
                Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(Path.Combine(strDriveName, strFilePath), strDLFilePath)


                'ファイル存在チェック
                If System.IO.File.Exists(strDLFilePath) Then

                    Dim fas As System.IO.FileAttributes = System.IO.File.GetAttributes(strDLFilePath)
                    ' ファイル属性に読み取り専用を追加
                    fas = fas Or System.IO.FileAttributes.ReadOnly
                    ' ファイル属性を設定
                    System.IO.File.SetAttributes(strDLFilePath, fas)
                    'プロセススタート
                    System.Diagnostics.Process.Start(strDLFilePath)

                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & E0201_E022
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & E0201_E022
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    '''ファイルダウンロード処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function FileDownLoad(ByVal dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFilePath As String
        Dim intFileMngNmb As Integer
        Dim sfd As New SaveFileDialog()

        Dim strCmd As String = ""                                   'コマンド文字列
        Dim strDriveName As String = ""                             '使用論理ドライブ名
        Dim strDLFilePath As String = ""                            'ダウンロードファイルパス

        Try
            With dataHBKE0201

                '選択行のファイルパスを取得
                strFilePath = dataHBKE0201.PropStrSelectedFilePath

                'ファイルダウンロード処理
                sfd.FileName = Path.GetFileName(strFilePath)
                sfd.InitialDirectory = ""
                sfd.Filter = "すべてのファイル(*.*)|*.*"
                sfd.FilterIndex = 1
                sfd.Title = "保存先を指定してください"


                '選択行のファイルパス、ファイル管理番号取得
                strFilePath = dataHBKE0201.PropStrSelectedFilePath
                intFileMngNmb = .PropVwFileInfo.Sheets(0).Cells(.PropIntSelectedRow, COL_FILE_MNGNMB).Value

                'アップロード状況に応じて処理分岐
                If intFileMngNmb > 0 Then

                    '既にアップロード済みのファイルの場合（ファイル管理番号が振られている場合）、ネットワークドライブより開く

                    'PCの論理ドライブ名をすべて取得する
                    Dim strDrives As String() = System.IO.Directory.GetLogicalDrives()
                    '利用可能な論理ドライブ名を取得する
                    For Each strDrive As String In DRIVES
                        If strDrives.Contains(strDrive) = False Then
                            strDriveName = strDrive.Substring(0, 2)
                            Exit For
                        End If
                    Next

                    'NetUse設定
                    If commonLogicHBK.NetUseConect(strDriveName) = False Then
                        Return False
                    End If

                End If

                'ダウンロードファイルパス取得
                strDLFilePath = Path.Combine(strDriveName, strFilePath)

                'ファイルの存在チェック
                If System.IO.File.Exists(strDLFilePath) = False Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

                'ファイルダイアログ表示
                If sfd.ShowDialog() = DialogResult.OK Then
                    'ファイルのコピー
                    Microsoft.VisualBasic.FileIO.FileSystem.CopyFile(strDLFilePath, sfd.FileName, True)
                End If

            End With


            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As System.IO.DirectoryNotFoundException
            'ディレクトリが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & E0201_E022
            Return False
        Catch ex As System.IO.FileNotFoundException
            'ファイルが見つからなかった場合
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & E0201_E022
            Return False
        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '接続した論理ドライブの削除
            commonLogicHBK.NetUseConectDel(strDriveName)
        End Try

    End Function

    ''' <summary>
    ''' 【会議一覧表示後】会議情報再取得メイン処理
    ''' </summary>
    ''' <param name="dataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議結果情報データの再取得を行う。
    ''' <para>作成情報：2012/09/12 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function RefreshMeetingMain(ByRef dataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter      'アダプタ

        Try

            '会議結果情報データ取得(PropDtResultMtg)
            If GetMeetingResult(Adapter, Cn, dataHBKE0201) = False Then
                Return False
            End If

            With dataHBKE0201
                'データテーブルを取得
                .PropDtMeeting = DirectCast(.PropVwMeeting.Sheets(0).DataSource, DataTable)

                '退避用データテーブル作成
                Dim dtAdd As DataTable = .PropDtMeeting.Clone
                Dim dtDel As DataTable = .PropDtMeeting.Clone
                If .PropDtMeeting IsNot Nothing AndAlso .PropDtMeeting.Rows.Count > 0 Then
                    '追加された情報で未登録のものを取得 
                    For i As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                        'Addされたデータのみ取得
                        Select Case .PropDtMeeting.Rows(i).RowState
                            Case DataRowState.Added '画面で追加されたデータ
                                dtAdd.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb"), _
                                               .PropDtMeeting.Rows(i).Item("JisiDT"), _
                                               .PropDtMeeting.Rows(i).Item("Title"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbnNM"), _
                                               .PropDtMeeting.Rows(i).Item("ResultKbn"))

                            Case DataRowState.Deleted '画面で削除されたデータ
                                dtDel.Rows.Add(.PropDtMeeting.Rows(i).Item("MeetingNmb", DataRowVersion.Original))

                        End Select
                    Next
                End If

                '会議一覧スプレッド再取得データを設定
                .PropDtMeeting = .PropDtResultMtg.Copy
                .PropDtMeeting.AcceptChanges()
                .PropVwMeeting.DataSource = .PropDtMeeting


                '画面上で追加且つＤＢ未更新のデータを反映
                If dtAdd.Rows.Count > 0 Then
                    For i As Integer = 0 To dtAdd.Rows.Count - 1
                        .PropDtMeeting.Rows.Add(dtAdd.Rows(i).Item("MeetingNmb"), _
                                                  dtAdd.Rows(i).Item("JisiDT"), _
                                                  dtAdd.Rows(i).Item("Title"), _
                                                  dtAdd.Rows(i).Item("ResultKbnNM"), _
                                                  dtAdd.Rows(i).Item("ResultKbn"))
                    Next
                End If

                '画面上で削除且つＤＢ未更新のデータを反映
                If dtDel.Rows.Count > 0 Then
                    For i As Integer = 0 To dtDel.Rows.Count - 1
                        For j As Integer = 0 To .PropDtMeeting.Rows.Count - 1
                            Select Case .PropDtMeeting.Rows(j).RowState
                                Case DataRowState.Deleted
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb", DataRowVersion.Original).ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                                Case Else
                                    If .PropDtMeeting.Rows(j).Item("MeetingNmb").ToString.Equals(dtDel.Rows(i).Item("MeetingNmb").ToString) Then
                                        .PropDtMeeting.Rows(j).Delete()
                                    End If
                            End Select
                        Next
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
        Finally
            Adapter.Dispose()
            'コネクションが閉じられていない場合、コネクションを閉じる
            If Cn IsNot Nothing Then
                Cn.Close()
            End If
            Cn.Dispose()
        End Try

    End Function


    ''' <summary>
    ''' 【共通】プロセスリンク情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="DataHBKE0201">[IN/OUT]変更登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>プロセスリンク情報データを取得する
    ''' <para>作成情報：2012/08/21 r.hoshino
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetPLinkRef(ByVal Adapter As NpgsqlDataAdapter, _
                               ByVal Cn As NpgsqlConnection, _
                               ByRef DataHBKE0201 As DataHBKE0201) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtInfo As New DataTable

        Try
            '取得用SQLの作成・設定
            If sqlHBKE0201.SetSelectPLinkSql(Adapter, Cn, DataHBKE0201) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセスリンク情報データ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtInfo)

            '取得データをデータクラスにセット
            DataHBKE0201.PropDtResultMtg = dtInfo


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
            dtInfo.Dispose()
        End Try

    End Function



End Class
