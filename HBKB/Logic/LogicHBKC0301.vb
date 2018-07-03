Imports Common
Imports CommonHBK
Imports Npgsql
Imports FarPoint.Win.Spread

''' <summary>
''' 会議検索一覧画面ロジッククラス
''' </summary>
''' <remarks>会議検索一覧画面のロジックを定義したクラス
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改定情報：</p>
''' </para></remarks>
Public Class LogicHBKC0301

    'インスタンス生成
    Private sqlHBKC0301 As New SqlHBKC0301
    Private commonLogic As New CommonLogic
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    '会議一覧列番号
    Public Const COL_SEARCHLIST_SEL As Integer = 0              '選択
    Public Const COL_SEARCHLIST_NUM As Integer = 1              '会議番号
    Public Const COL_SEARCHLIST_YOTEI As Integer = 2            '実施予定日
    Public Const COL_SEARCHLIST_JISI As Integer = 3             '実施日
    Public Const COL_SEARCHLIST_TITLE As Integer = 4            'タイトル
    Public Const COL_SEARCHLIST_HOSTGRP As Integer = 5          '主催者グループ
    Public Const COL_SEARCHLIST_HOSTNM As Integer = 6           '主催者名
    'Public Const COL_SEARCHLIST_RESULT As Integer = 7           '結果区分（非表示）
    'Public Const COL_SEARCHLIST_RESULTNM As Integer = 8         '結果区分名称（非表示）
    Public Const COL_SEARCHLIST_SORTNO As Integer = 7           'ソートNo（非表示）

    Private Const RETURN_TEXT_FROM_MENU As String = "閉じる"    '戻る／閉じるボタンテキスト：メニューから遷移時

    ''' <summary>
    ''' 画面初期表示メイン処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索一覧画面呼出時に初期データをセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function InitFormMain(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '初期データ取得処理
        If GetInitData(dataHBKC0301) = False Then
            Return False
        End If

        'プロセスコンボボックス作成処理
        If InitProcessCmb(dataHBKC0301) = False Then
            Return False
        End If

        '主催者グループコンボボックス作成処理
        If InitGroupCmb(dataHBKC0301) = False Then
            Return False
        End If

        '検索項目初期化処理
        If InitSearchControl(dataHBKC0301) = False Then
            Return False
        End If

        '遷移元に応じてコントロールの設定を行う
        If SetControlPerFrom(dataHBKC0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 画面初期表示データ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetInitData(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            'グループマスタデータ取得（コンボボックス用）
            If GetGroupMaster(Adapter, Cn, dataHBKC0301) = False Then
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
    ''' グループマスタデータ取得（コンボボックス用）
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetGroupMaster(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtGroup As New DataTable

        Try

            'SQLの作成・設定
            If sqlHBKC0301.SetSelectGroupMasterSql(Adapter, Cn, dataHBKC0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "グループマスタ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtGroup)

            '取得データをデータクラスにセット
            dataHBKC0301.PropDtGroup = dtGroup

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
            dtGroup.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 【検索用】プロセスコンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKC0301"></param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>コンボボックスの初期化を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Private Function InitProcessCmb(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'プロセスコンボボックスを初期化する
            Dim list As New List(Of DictionaryEntry)

            list.Add(New DictionaryEntry(String.Empty, String.Empty))
            list.Add(New DictionaryEntry(PROCESS_TYPE_INCIDENT, PROCESS_TYPE_INCIDENT_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_QUESTION, PROCESS_TYPE_QUESTION_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_CHANGE, PROCESS_TYPE_CHANGE_NAME))
            list.Add(New DictionaryEntry(PROCESS_TYPE_RELEASE, PROCESS_TYPE_RELEASE_NAME))

            dataHBKC0301.PropCmbProcessKbn.DataSource = list

            dataHBKC0301.PropCmbProcessKbn.DisplayMember = "Value"
            dataHBKC0301.PropCmbProcessKbn.ValueMember = "Key"

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    '''【検索用】主催者グループコンボボックス作成処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のコンボボックスを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitGroupCmb(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0301

                'グループコンボボックス作成
                If commonLogic.SetCmbBox(.PropDtGroup, .PropCmbHostGrpCD, True, "", "") = False Then
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
    ''' 検索条件初期化処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索条件入力フォームに入力された内容を初期化する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function InitSearchControl(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            '各検索用のコントロールを初期化する
            With dataHBKC0301

                .PropTxtMeetingNmb.Text = ""                    '会議番号
                .PropCmbProcessKbn.SelectedValue = ""           'プロセス
                .PropTxtProcessNmb.Text = ""                    '管理番号
                .PropDtpYoteiDTFrom.txtDate.Text = ""           '実施予定日(FROM)
                .PropDtpYoteiDTTo.txtDate.Text = ""             '実施予定日(TO)
                .PropDtpJisiDTFrom.txtDate.Text = ""            '実施日(FROM)
                .PropDtpJisiDTTo.txtDate.Text = ""              '実施日(To)
                .PropTxtTitle.Text = ""                         'タイトル
                .PropCmbHostGrpCD.SelectedValue = ""            '主催者グループ
                .PropTxtHostID.Text = ""                        '主催者ID
                .PropTxtHostNM.Text = ""                        '主催者氏名

                ''遷移元に応じてコントロールの設定を行う
                'If SetControlPerFrom(dataHBKC0301) = False Then
                '    Return False
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
    ''' コントロール初期設定
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>遷移元に応じてコントロールの設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetControlPerFrom(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0301

                '遷移元によりコントロール設定
                Select Case .PropBlnTranFlg

                    Case SELECT_MODE_MENU       'メニューより遷移

                        'メニューより遷移時の設定
                        If SetControlForFromMenu(dataHBKC0301) = False Then
                            Return False
                        End If

                    Case SELECT_MODE_NOTMENU    'メニュー以外より遷移

                        'メニュー以外より遷移時の設定
                        If SetControlForFromNotMenu(dataHBKC0301) = False Then
                            Return False
                        End If

                End Select

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
    ''' 【メニューから遷移時】コントロール初期設定
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニューから遷移時のコントロールの設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetControlForFromMenu(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0301

                'ボタン非表示
                .PropBtnAllcheck.Visible = False    '全選択
                .PropBtnAllrelease.Visible = False  '全解除
                .PropBtnSelect.Visible = False      '選択

                '非表示に合わせ、表示ボタンの位置を左に移動
                .PropBtnSort.Location = .PropBtnAllcheck.Location       'デフォルトソートボタンを全選択ボタンの位置に
                .PropBtnDetails.Location = .PropBtnReg.Location         '詳細確認ボタンを登録ボタンの位置に
                .PropBtnReg.Location = .PropBtnSelect.Location          '登録ボタンを選択ボタンの位置に


                'スプレッドの選択列非活性
                .PropVwMeetingList.Sheets(0).Columns(COL_SEARCHLIST_SEL).Visible = False
          

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
    ''' 【メニュー以外から遷移時】コントロール初期設定
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>メニューから遷移時のコントロールの設定を行う
    ''' <para>作成情報：2012/08/13 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SetControlForFromNotMenu(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0301

                'ボタン非表示
                .PropBtnClear.Visible = False   'クリア

                'ボタンテキスト変更
                .PropBtnReturn.Text = RETURN_TEXT_FROM_MENU     '「戻る」→「閉じる」

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
    ''' 会議検索結果表示処理メイン
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索を行い結果を表示する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：2017/08/17 e.okuda</p>
    ''' </para></remarks>
    Public Function SearchDataMain(ByRef dataHBKC0301 As dataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '件数取得処理
        If GetResultCount(dataHBKC0301) = False Then
            Return False
        End If

        '件数判定(判定を行い表示しない場合処理を抜ける)
        If dataHBKC0301.PropResultCount.Rows(0).Item(0) > PropSearchMsgCount Then

            '件数が20件以上で表示しない(NO)を選択した場合処理を抜ける
            If MsgBox(String.Format(C0301_W001, PropSearchMsgCount), MsgBoxStyle.Exclamation + MsgBoxStyle.YesNo, TITLE_WARNING) = MsgBoxResult.No Then
                '終了ログ出力
                commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
                '正常終了
                Return True
            End If

        ElseIf dataHBKC0301.PropResultCount.Rows(0).Item(0) = 0 Then
            ' 2017/08/17 e.okuda 削除対象が0件でない条件付加
            If dataHBKC0301.PropVwMeetingList.Sheets(0).Rows.Count > 0 Then
                dataHBKC0301.PropVwMeetingList.Sheets(0).RemoveRows(0, dataHBKC0301.PropVwMeetingList.Sheets(0).Rows.Count)
            End If
            '0件の場合処理を抜ける
            dataHBKC0301.PropLblItemCount.Text = dataHBKC0301.PropResultCount.Rows(0).Item(0) & "件"
            'メッセージ変数にエラーメッセージを格納
            'puErrMsg = C0301_I001
            'メッセージ変数に空白をセット
            puErrMsg = ""
            Return False
        End If

        'スプレッド用データテーブル作成処理
        If CreateDataTable(dataHBKC0301) = False Then
            Return False
        End If

        '検索結果取得処理
        If GetSearchData(dataHBKC0301) = False Then
            Return False
        End If

        'スプレッド出力データ設定処理
        If SetVwData(dataHBKC0301) = False Then
            Return False
        End If

        '件数表示処理
        If SetResultCount(dataHBKC0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果件数データ取得
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果件数を取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetResultCount(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        '変数宣言
        Dim dtResultCount As New DataTable '検索件数

        Try

            'コネクションを開く
            Cn.Open()

            'SQLの作成・設定
            If sqlHBKC0301.SetResultCountSql(Adapter, Cn, dataHBKC0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "検索結果件数", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtResultCount)

            '取得データをデータクラスにセット
            dataHBKC0301.PropResultCount = dtResultCount

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
            'リソースの解放
            Adapter.Dispose()
            Cn.Dispose()
            dtResultCount.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' スプレッド用データテーブル作成処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>スプレッドに出力するためのデータテーブルの作成を行う
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function CreateDataTable(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtMeeting As New DataTable '会議検索結果用データテーブル

        Try

            '会議検索結果用データテーブル作成
            With dtMeeting

                .Columns.Add("Select", Type.GetType("System.Boolean"))          '選択
                .Columns.Add("MeetingNmb", Type.GetType("System.Int32"))        '会議番号
                .Columns.Add("YoteiDT", Type.GetType("System.String"))          '実施予定日
                .Columns.Add("JisiDT", Type.GetType("System.String"))           '実施日
                .Columns.Add("Title", Type.GetType("System.String"))            'タイトル
                .Columns.Add("GroupNM", Type.GetType("System.String"))          '主催者グループ
                .Columns.Add("HostNM", Type.GetType("System.String"))           '主催者
                .Columns.Add("ResultKbn", Type.GetType("System.String"))        '結果区分
                .Columns.Add("ResultKbnNM", Type.GetType("System.String"))      '結果区分名称
                .Columns.Add("SortNo", Type.GetType("System.Int32"))            'ソートNo

                'テーブルの変更を確定
                .AcceptChanges()
            End With

            'データクラスをデータテーブルにセット
            dataHBKC0301.PropDtMeeting = dtMeeting

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
            dtMeeting.Dispose()
        End Try

    End Function

    ''' <summary>
    ''' 検索結果取得処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetSearchData(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try
            'コネクションを開く
            Cn.Open()

            '会議情報取得（スプレッド用）
            If GetMeetingTable(Adapter, Cn, dataHBKC0301) = False Then
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
    ''' スプレッド用会議情報データ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]データアダプター</param>
    ''' <param name="Cn">[IN]コネクション</param>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>会議検索一覧画面の初期表示に必要なデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function GetMeetingTable(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'SQLの作成・設定
            If sqlHBKC0301.SetSelectMeetingTableSql(Adapter, Cn, dataHBKC0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "会議情報取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dataHBKC0301.PropDtMeeting)

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
        End Try

    End Function

    ''' <summary>
    '''スプレッドの出力データ設定処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>テーブルから取得した内容をスプレッドに設定する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetVwData(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            With dataHBKC0301

                '検索結果
                With .PropVwMeetingList.Sheets(0)

                    .DataSource = Nothing
                    .DataSource = dataHBKC0301.PropDtMeeting
                    .Columns(COL_SEARCHLIST_SEL).DataField = "Select"           '選択
                    .Columns(COL_SEARCHLIST_NUM).DataField = "MeetingNmb"       '会議番号
                    .Columns(COL_SEARCHLIST_YOTEI).DataField = "YoteiDT"        '実施予定日
                    .Columns(COL_SEARCHLIST_JISI).DataField = "JisiDT"          '実施日
                    .Columns(COL_SEARCHLIST_TITLE).DataField = "Title"          'タイトル
                    .Columns(COL_SEARCHLIST_HOSTGRP).DataField = "GroupNM"      '主催者グループ
                    .Columns(COL_SEARCHLIST_HOSTNM).DataField = "HostNM"        '主催者
                    '.Columns(COL_SEARCHLIST_RESULT).DataField = "ResultKbn"     '結果区分
                    '.Columns(COL_SEARCHLIST_RESULTNM).DataField = "ResultKbnNM" '結果区分名称
                    .Columns(COL_SEARCHLIST_SORTNO).DataField = "SortNo"        'ソートNo

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
    ''' 件数表示処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果の件数を表示する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Private Function SetResultCount(ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0301

                '件数判定
                If .PropResultCount.Rows(0).Item(0) = 0 Then
                    MsgBox(C0301_I001, MsgBoxStyle.Information, TITLE_INFO)
                End If

                '件数をセット
                .PropLblItemCount.Text = .PropResultCount.Rows(0).Item(0) & "件"

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
    ''' デフォルトソート処理メイン
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>デフォルトソート処理メイン
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SortDefaultMain(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索結果ソート処理
        If SortSearchData(dataHBKC0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常終了
        Return True

    End Function

    ''' <summary>
    ''' 検索結果ソート処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>検索結果を検索時のソート順に並べ替える
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function SortSearchData(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            With dataHBKC0301.PropVwMeetingList.Sheets(0)

                'ソート列(ソートNo)の昇順にソートする
                .SortRows(COL_SEARCHLIST_SORTNO, True, False)

                'ソートインジケーターの初期化
                For i = 0 To .Columns.Count - 1
                    .Columns(i).ResetSortIndicator()
                Next

            End With

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
        End Try

    End Function

    ''' <summary>
    ''' 主催者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>作成者IDEnter時の処理
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function CreateIDEnterMain(ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim Cn As New NpgsqlConnection(DbString)  'サーバーとクライアントをつなげる
        Dim Adapter As New NpgsqlDataAdapter

        Try

            'コネクションを開く
            Cn.Open()

            'テーブル取得
            If GetEndUsrMasta(Adapter, Cn, dataHBKC0301) = False Then
                Return False
            End If

            '主催者ID設定
            If SetNewCrateData(dataHBKC0301) = False Then
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
    ''' 【共通】ひびきユーザーマスタ取得処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ファイル管理テーブルからファイルパスを取得し、ファイルをダウンロードする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetEndUsrMasta(ByVal Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim dtHbkUser As New DataTable

        Try

            '取得用SQLの作成・設定
            If sqlHBKC0301.GetHbnUsrMastaData(Adapter, Cn, dataHBKC0301) = False Then
                Return False
            End If

            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.DEBUG_Lv, "ひびきユーザーマスタデータ取得", Nothing, Adapter.SelectCommand)

            'データを取得
            Adapter.Fill(dtHbkUser)

            '取得データをデータクラスにセット
            dataHBKC0301.PropDtResultSub = dtHbkUser

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
            dtHbkUser.Dispose()
        End Try
    End Function

    ''' <summary>
    ''' 主催者IDテキストボックスEnter時の処理
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面Dataクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ＩＤテキストボックスにエンドユーザーマスタから取得した値を入力する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : 2012/09/19 k.ueda</p>
    ''' </para></remarks>
    Public Function SetNewCrateData(ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        With dataHBKC0301

            '選択データがある場合のみ値をセットする
            If .PropDtResultSub IsNot Nothing AndAlso .PropDtResultSub.Rows.Count > 0 Then

                '選択されたひびきユーザー情報を主催者情報にセットする
                .PropTxtHostNM.Text = .PropDtResultSub.Rows(0).Item("HbkUsrNM")     'ユーザー氏名
                '検索したユーザーのグループが1件の場合のみグループを設定する
                If .PropDtResultSub.Rows.Count = 1 Then
                    .PropCmbHostGrpCD.SelectedValue = .PropDtResultSub.Rows(0).Item("GroupCD")   '主催者グループ
                Else
                    .PropCmbHostGrpCD.SelectedValue = ""                                '主催者グループ
                End If
            Else

                '取得データがない場合（ENTERキーにて検索した場合）クリア
                .PropTxtHostNM.Text = ""                                            'ユーザー氏名
                .PropCmbHostGrpCD.SelectedValue = ""                                '主催者グループ
            End If

        End With

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

    ''' <summary>
    ''' 検索条件フォームオブジェクト初期化処理メイン
    ''' </summary>
    ''' <param name="dataHBKC0301">[IN/OUT]インシデント検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>フォーム上のリストボックスを作成する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function ClearSearchFormMain(ByRef dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '検索条件フォームオブジェクト初期化処理
        If InitSearchControl(dataHBKC0301) = False Then
            Return False
        End If

        '終了ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

        '正常処理終了
        Return True

    End Function

End Class
