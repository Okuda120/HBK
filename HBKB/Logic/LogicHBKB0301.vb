Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Windows.Forms

Public Class LogicHBKB0301

    Private sqlHBKB0301 As New SqlHBKB0301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    'Public定数宣言==============================================
    '原因リンク列番号
    Public Const COL_KBN_NM As Integer = 0          'プロセス区分名称
    Public Const COL_MANAGE_NM As Integer = 1   '管理番号
    Public Const COL_TITLE As Integer = 2               'タイトル
    Public Const COL_KBN_CD As Integer = 3               'プロセス区分コード
    '画面モード文言
    Public Const PACKAGE As String = "一括更新"
    Public Const HISTORY As String = "ロールバック"
    '===========================================================

    ''' <summary>
    ''' 画面初期表示時メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面の初期表示処理を行う(インシデント以外）
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKB0301) = False Then
            Return False
        End If

        '初期表示用データ取得
        If GetInitData(dataHBKB0301) = False Then
            Return False
        End If

        '画面コントロール設定
        If SetPicControl(dataHBKB0301) = False Then
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
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面表示用のデータを取得する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strParaHyoji As String = ""

        Try

            '引渡し項目がインシデントの場合、それ以外の場合で処理を分岐させる
            Select Case dataHBKB0301.PropStrRegMode
                Case REG_MODE_BLANK         '画面初期表示処理(引渡しなし）

                    strParaHyoji = ""

                Case REG_MODE_PACKAGE       '画面初期表示処理(一括）

                    strParaHyoji = PACKAGE

                Case REG_MODE_HISTORY       '画面初期表示処理(履歴）

                    strParaHyoji = HISTORY

                Case REG_MODE_INCIDENT      '画面初期表示処理（インシデント）

                    '理由データ取得、Spredデータ取得・設定
                    If GetIncidentData(dataHBKB0301) = False Then
                        Return False
                    End If
                    strParaHyoji = ""

            End Select

            'データクラスに引き渡し値セット
            dataHBKB0301.PropStrDefaultReason = strParaHyoji

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
    ''' 画面表示用データを設定する
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面表示用データを設定する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetIncidentData(ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ


        Try

            'コネクションを開く
            Cn.Open()

            '理由文字列、原因リンクデータ取得
            If GetDataTableForPic(Adapter, Cn, dataHBKB0301) = False Then
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
    ''' 画面表示用データテーブル取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>画面表示用のデータを取得する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetDataTableForPic(ByVal Adapter As NpgsqlDataAdapter, _
                                                            ByVal Cn As NpgsqlConnection, _
                                                            ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtReason As New DataTable                           '理由取得用テーブル
        Dim dtCauseLink As New DataTable                           '原因リンク取得用テーブル
        Try

            '理由テーブル取得SQLの作成・設定
            If sqlHBKB0301.SetSelectReason(Adapter, Cn, dataHBKB0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "理由データ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtReason)

            '原因リンク取得SQLの作成・設定
            If sqlHBKB0301.SetSelectCause(Adapter, Cn, dataHBKB0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "原因リンクデータ取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dtCauseLink)

            'Dataクラスに保存
            With dataHBKB0301
                .PropDtCauseLink = dtCauseLink
                If dtReason.Rows.Count = 0 Then
                    .PropStrRegReason = ""
                Else
                    .PropStrRegReason = dtReason.Rows(0).Item(0).ToString
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
            dtReason.Dispose()
            dtCauseLink.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドのデータソースとして設定するデータテーブルを作成する
    ''' <para>作成情報：2012/07/02 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtSpreSet As New DataTable

        Try

            With dtSpreSet
                .Columns.Add("ProcessKbnNm", Type.GetType("System.String"))        'プロセス区分
                .Columns.Add("MngNmb", Type.GetType("System.Int32"))           '管理番号
                .Columns.Add("Title", Type.GetType("System.String"))                    'タイトル
                .Columns.Add("ProcessKbn", Type.GetType("System.String"))        'プロセス区分コード

                'テーブルの変更を確定
                .AcceptChanges()

            End With

            'データクラスに保存
            With dataHBKB0301
                .PropDtCauseLink = dtSpreSet
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
    ''' 画面コントロール設定処理（Spread以外）
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由登録画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Spread以外の画面コントロールの設定を行う(理由)
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetPicControl(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'コントロールの設定
            With dataHBKB0301
                .PropTxtRegReason.Text = .PropStrDefaultReason

                '最終作業プロセス区分、番号が空で無い場合、最終管理番号ボタンを活性状態にする。
                If CommonDeclareHBK.PropLastProcessKbn <> "" And CommonDeclareHBK.PropLastProcessNmb <> "" Then
                    .PropBtnLastManageNmb.Enabled = True
                Else
                    .PropBtnLastManageNmb.Enabled = False
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
    ''' 原因リンク行削除メイン処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>原因リンクの行削除処理を行う
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function RemoveRowCauseLinkMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)


        '選択行削除処理
        If RemoveRowCauseLink(dataHBKB0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 最終管理番号セットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>最終管理番号を原因リンクにセットする
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function SetLastManageNmMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '最終管理番号データ設定処理
        If SetLastManageNm(dataHBKB0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    '''   原因リンク選択行削除処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>原因リンクの選択行を削除（Remove）する
    ''' <para>作成情報：2012/06/12 kawate
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Function RemoveRowCauseLink(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intSelectedRow As Integer   '選択行番号

        Try
            With dataHBKB0301.PropVwCauseLink.Sheets(0)

                '選択行取得
                intSelectedRow = .ActiveRowIndex

                '一覧に可視行がない場合は処理終了
                If .RowCount = 0 Then
                    Return True
                End If

                '選択行を削除する
                .Rows(intSelectedRow).Remove()

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
    ''' 最終管理番号セット処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>ログインユーザが最後に登録した管理番号を原因リンクにセットする
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Function SetLastManageNm(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '最終管理番号タイトル取得処理
            If GetLastManageTitle(dataHBKB0301) = False Then
                Return False
            End If

            '最終管理番号情報設定処理
            If SetLastManageDt(dataHBKB0301) = False Then
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
    ''' 原因リンクセットメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>プロセス検索結果を原因リンクにセット（行追加）する
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Public Function SetProcessToVwCauseLinkMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'データ設定処理
        If SetProcessToVwCauseLink(dataHBKB0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 原因リンクセット処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>原因リンクにサブ検索で選択されたプロセス情報を設定する
    ''' <para>作成情報：2012/06/12 kawate 
    ''' <p>改訂情報 : 2012/07/02 y.ikushima(開発引継ぎ）</p>
    ''' </para></remarks>
    Private Function SetProcessToVwCauseLink(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        '[mod] 2012/08/27 y.ikushima START
        'Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        'Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        '[mod] 2012/08/27 y.ikushima END

        Dim intNewRowNo As Integer          '新規追加行番号
        Dim blnAddFlg As Boolean = True     '追加フラグ

        Try
            With dataHBKB0301

                'サブ検索画面で1件以上選択された場合に値を設定
                If .PropDtResultSub IsNot Nothing Then

                    '選択データ数分繰り返し、関係者情報一覧に行を追加する
                    For i As Integer = 0 To .PropDtResultSub.Rows.Count - 1

                        'プロセスが既に設定済でない場合のみ追加
                        For j As Integer = 0 To .PropVwCauseLink.Sheets(0).RowCount - 1

                            '既に設定済みの場合は追加フラグをOFFにして処理を抜ける
                            If .PropDtResultSub.Rows(i).Item("MngNmb") = _
                                .PropVwCauseLink.Sheets(0).Cells(j, COL_MANAGE_NM).Value And _
                                .PropDtResultSub.Rows(i).Item("ProcessKbn") = _
                                .PropVwCauseLink.Sheets(0).Cells(j, COL_KBN_CD).Value Then
                                blnAddFlg = False
                                Exit For
                            End If

                        Next

                        '追加フラグがONの場合のみ追加処理を行う
                        If blnAddFlg = True Then

                            '追加行番号取得
                            intNewRowNo = .PropVwCauseLink.Sheets(0).Rows.Count

                            '新規行追加
                            .PropVwCauseLink.Sheets(0).Rows.Add(intNewRowNo, 1)

                            '[mod] 2012/08/27 y.ikushima START
                            ''コネクションを開く
                            'Cn.Open()
                            ''データクラスに条件をセット
                            'dataHBKB0301.PropStrProcessKbn = .PropDtResultSub.Rows(i).Item("ProcessKbn")
                            ''プロセス区分名称取得
                            'If GetProcessKbn(Adapter, Cn, dataHBKB0301) = False Then
                            '    Return False
                            'End If
                            ''コネクションを閉じる
                            'Cn.Close()
                            '[mod] 2012/08/27 y.ikushima EDN
                            'データクラスに条件をセット
                            dataHBKB0301.PropStrProcessKbn = .PropDtResultSub.Rows(i).Item("ProcessKbn")
                            'プロセス区分名称取得
                            If GetProcessKbn(dataHBKB0301) = False Then
                                Return False
                            End If


                            'サブ検索画面での選択値を設定
                            .PropVwCauseLink.Sheets(0).Cells(intNewRowNo, COL_KBN_NM).Value = _
                                dataHBKB0301.PropStrProcessKbnNm                                       'プロセス区分
                            .PropVwCauseLink.Sheets(0).Cells(intNewRowNo, COL_MANAGE_NM).Value = _
                                .PropDtResultSub.Rows(i).Item("MngNmb")                                       '管理番号
                            .PropVwCauseLink.Sheets(0).Cells(intNewRowNo, COL_TITLE).Value = _
                                .PropDtResultSub.Rows(i).Item("Title")                                       'タイトル
                            .PropVwCauseLink.Sheets(0).Cells(intNewRowNo, COL_KBN_CD).Value = _
                                .PropDtResultSub.Rows(i).Item("ProcessKbn")                                       'プロセス区分コード

                        End If

                    Next

                    '最終追加行にフォーカスをセット
                    If commonLogicHBK.SetFocusOnVwRow(.PropVwCauseLink, _
                                                      0, .PropVwCauseLink.Sheets(0).RowCount, 0, _
                                                      1, .PropVwCauseLink.Sheets(0).ColumnCount) = False Then
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
            '[mod] 2012/08/27 y.ikushima START
            ''コネクションが閉じられていない場合、コネクションを閉じる
            'If Cn IsNot Nothing Then
            '    Cn.Close()
            'End If
            '[mod] 2012/08/27 y.ikushima END
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        Finally
            '[mod] 2012/08/27 y.ikushima START
            'Adapter.Dispose()
            'Cn.Dispose()
            '[mod] 2012/08/27 y.ikushima END
        End Try

    End Function

    ''' <summary>
    ''' プロセス区分データ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>Datatableに入力されたプロセス区分コードから名称を取得する
    ''' <para>作成情報：2012/07/03 y.ikushima
    ''' <p>改訂情報 :2012/08/27 y.ikushima </p>
    ''' </para></remarks>
    Private Function GetProcessKbn(ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        '[mod] 2012/08/27 y.ikushima START
        'Dim dtProcessKbn As New DataTable
        '[mod] 2012/08/27 y.ikushima END

        Try
            '[mod] 2012/08/27 y.ikushima START
            ''プロセス区分名称取得SQLの作成・設定
            'If sqlHBKB0301.SetSelectCauseLinkSql(Adapter, Cn, dataHBKB0301) = False Then
            '    Return False
            'End If
            ''ログ出力
            'commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "プロセス区分名称取得", Nothing, Adapter.SelectCommand)
            ''データを取得
            'Adapter.Fill(dtProcessKbn)
            ''データクスにセット
            'dataHBKB0301.PropStrProcessKbnNm = dtProcessKbn.Rows(0).Item(0).ToString()
            '[mod] 2012/08/27 y.ikushima END

            'プロセス区分からプロセス区分名略称をセット
            If dataHBKB0301.PropStrProcessKbn = PROCESS_TYPE_INCIDENT Then
                dataHBKB0301.PropStrProcessKbnNm = PROCESS_TYPE_INCIDENT_NAME_R

            ElseIf dataHBKB0301.PropStrProcessKbn = PROCESS_TYPE_QUESTION Then
                dataHBKB0301.PropStrProcessKbnNm = PROCESS_TYPE_QUESTION_NAME_R
            ElseIf dataHBKB0301.PropStrProcessKbn = PROCESS_TYPE_CHANGE Then
                dataHBKB0301.PropStrProcessKbnNm = PROCESS_TYPE_CHANGE_NAME_R
            ElseIf dataHBKB0301.PropStrProcessKbn = PROCESS_TYPE_RELEASE Then
                dataHBKB0301.PropStrProcessKbnNm = PROCESS_TYPE_RELEASE_NAME_R
            Else
                dataHBKB0301.PropStrProcessKbnNm = ""
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
    ''' 最終管理番号タイトルデータ取得処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>最終管理番号、区分からタイトルを取得する
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function GetLastManageTitle(ByRef dataHBKB0301 As DataHBKB0301) As Boolean
        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dt As New DataTable
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter        'アダプタ
        Try

            'コネクションを開く
            Cn.Open()
            '最終管理番号タイトル取得SQLの作成・設定
            If sqlHBKB0301.SetSelectLastManageTitle(Adapter, Cn, dataHBKB0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "最終管理番号タイトル取得", Nothing, Adapter.SelectCommand)
            'データを取得
            Adapter.Fill(dt)

            'データクラスにセット
            dataHBKB0301.PropStrLastManageTitle = dt.Rows(0).Item(0).ToString()

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
            dt.Dispose()
            Adapter.Dispose()
            Cn.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 最終管理番号情報設定処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>最終管理番号ボタンクリック時にデータ（区分、番号、タイトル）をセットする
    ''' <para>作成情報：2012/08/07 m.ibuki
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetLastManageDt(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim IntSpreadRowCount As Integer = 0   'スプレッド表示行

        Try
            'スプレッドの最終行に1行追加して値をセットする
            IntSpreadRowCount = dataHBKB0301.PropVwCauseLink.Sheets(0).Rows.Count
            '最終表示行に1行追加する
            dataHBKB0301.PropVwCauseLink.Sheets(0).Rows.Add(IntSpreadRowCount, 1)

            '追加行に値をセットする
            '区分
            If CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_INCIDENT Then
                dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 0).Value = PROCESS_TYPE_INCIDENT_NAME_R
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_QUESTION Then
                dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 0).Value = PROCESS_TYPE_QUESTION_NAME_R
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_CHANGE Then
                dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 0).Value = PROCESS_TYPE_CHANGE_NAME_R
            ElseIf CommonDeclareHBK.PropLastProcessKbn = PROCESS_TYPE_RELEASE Then
                dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 0).Value = PROCESS_TYPE_RELEASE_NAME_R
            End If
            '番号
            dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 1).Value = Integer.Parse(CommonDeclareHBK.PropLastProcessNmb)
            'タイトル
            dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 2).Value = dataHBKB0301.PropStrLastManageTitle
            '区分CD
            dataHBKB0301.PropVwCauseLink.Sheets(0).Cells(IntSpreadRowCount, 3).Value = CommonDeclareHBK.PropLastProcessKbn

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
    ''' 画面入力チェックメイン処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェックを行う
    ''' <para>作成情報：2012/07/02 y.ikushima 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CheckInputPicMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '画面コントロール入力チェック処理
        If CheckInputPic(dataHBKB0301) = False Then
            Return False
        End If

        '戻り値設定処理
        If SetRetrunDt(dataHBKB0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 画面入力チェック処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>画面の入力チェック処理を行う
    ''' <para>作成情報：2012/07/02 y.ikushima 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CheckInputPic(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim bolInputChk As Boolean = False  'Spread入力チェック（初期値False）

        Try
            'Spreadの入力チェック
            With dataHBKB0301.PropVwCauseLink.Sheets(0)
                For i As Integer = 0 To .Rows.Count - 1 Step 1
                    For j As Integer = 0 To .Columns.Count - 1 Step 1
                        If .GetValue(i, 0 + j) <> "" Then
                            bolInputChk = True
                            Exit For
                        End If
                    Next
                    If bolInputChk = True Then
                        Exit For
                    End If
                Next
            End With

            With dataHBKB0301
                '理由入力チェック
                If (.PropTxtRegReason.Text = "") And bolInputChk = False Then
                    '理由入力、または原因リンクにデータが未入力の場合エラー
                    puErrMsg = B0301_E001
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
    ''' 戻り値設定処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>boolean  エラーコード    true  正常終了  false  異常終了</returns>
    ''' <remarks>原因リンクSpreadを戻り値のDataTableへ変換する
    ''' <para>作成情報：2012/07/03 y.ikushima 
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetRetrunDt(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '戻り値にデータをセット
            With dataHBKB0301
                .PropStrRegReason = .PropTxtRegReason.Text
                'DataTable 作成
                For i As Integer = 0 To .PropVwCauseLink.Sheets(0).Rows.Count - 1 Step 1
                    Dim row As DataRow = .PropDtCauseLink.NewRow()
                    row.Item(COL_KBN_NM) = .PropVwCauseLink.Sheets(0).GetValue(i, COL_KBN_NM)
                    row.Item(COL_MANAGE_NM) = .PropVwCauseLink.Sheets(0).GetValue(i, COL_MANAGE_NM)
                    row.Item(COL_TITLE) = .PropVwCauseLink.Sheets(0).GetValue(i, COL_TITLE)
                    row.Item(COL_KBN_CD) = .PropVwCauseLink.Sheets(0).GetValue(i, COL_KBN_CD)
                    .PropDtCauseLink.Rows.Add(row)
                Next
                .PropDtCauseLink.AcceptChanges()
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
    ''' システムエラー事前対応処理
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>システムエラー発生時に非活性にするコントロールリストを作成する
    ''' <para>作成情報：2012/07/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function DoProcForErrorMain(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'トランザクション系コントロールリスト作成
        If CreateTsxCtlList(dataHBKB0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' トランザクション系コントロールリスト作成処理（システムエラー対応）
    ''' </summary>
    ''' <param name="dataHBKB0301">[IN/OUT]変更理由画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>トランザクション系コントロールのリストを作成する
    ''' <para>作成情報：2012/07/20 y.ikushima
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateTsxCtlList(ByRef dataHBKB0301 As DataHBKB0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim aryCtlList As New ArrayList

        Try
            With dataHBKB0301

                'トランザクション系のコントロールをリストに追加
                aryCtlList.Add(.PropBtntouroku)              '登録ボタン

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

End Class
