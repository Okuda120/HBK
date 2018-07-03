Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Text

''' <summary>
''' 会議検索一覧画面Sqlクラス
''' </summary>
''' <remarks>会議検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/09 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0301

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数
    Private Const SEARCH_MODE_SEARCH As Integer = 0         '検索結果
    Private Const SEARCH_MODE_COUNT As Integer = 1          '検索件数

    '[SELECT]グループマスタ取得SQL
    Private strSelectGroupMastaSql As String = "SELECT " & vbCrLf & _
                                                " gm.GroupCD " & vbCrLf & _
                                                ",gm.GroupNM " & vbCrLf & _
                                                "FROM GRP_MTB AS gm " & vbCrLf & _
                                                "WHERE gm.JtiFlg = '0' " & vbCrLf & _
                                                "ORDER BY gm.Sort ASC"

    '[SELECT]ひびきユーザーマスタ取得SQL
    Private strSelectHbkUsrMastaSql As String = "SELECT " & vbCrLf & _
                                                " hm.HbkUsrID" & vbCrLf & _
                                                ",hm.HbkUsrNM" & vbCrLf & _
                                                ",sm.groupcd " & vbCrLf & _
                                                "FROM HBKUSR_MTB AS hm" & vbCrLf & _
                                                "LEFT JOIN szk_mtb sm ON hm.hbkusrid = sm.hbkusrid " & vbCrLf & _
                                                "WHERE hm.HbkUsrID = :HbkUsrID "

    '[SELECT]会議情報取得SQL
    Private strSelectMeetingTableSql As String = "SELECT " & vbCrLf & _
                                                 " FALSE AS Select " & vbCrLf & _
                                                 ",mt.MeetingNmb AS MeetingNmb " & vbCrLf & _
                                                 ",TO_CHAR(mt.YoteiStDT,'YYYY/MM/DD') AS YoteiDT " & vbCrLf & _
                                                 ",TO_CHAR(mt.JisiSTDT,'YYYY/MM/DD') AS JisiDT " & vbCrLf & _
                                                 ",mt.Title AS Title " & vbCrLf & _
                                                 ",gm.GroupNM AS GroupNM " & vbCrLf & _
                                                 ",mt.HostNM AS HostNM " & vbCrLf & _
                                                 "FROM MEETING_TB AS mt " & vbCrLf & _
                                                 "LEFT OUTER JOIN GRP_MTB AS gm ON mt.HostGrpCD = gm.GroupCD " & vbCrLf

    '[SELECT]会議検索件数
    Private strSelectCountSql As String = "SELECT " & vbCrLf & _
                                          " COUNT(mt.MeetingNmb) " & vbCrLf & _
                                          "FROM MEETING_TB AS mt " & vbCrLf & _
                                          "LEFT OUTER JOIN GRP_MTB AS gm ON mt.HostGrpCD = gm.GroupCD " & vbCrLf

    ''' <summary>
    ''' グループマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループスマスタ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGroupMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectGroupMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' ひびきユーザーマスタデータ取得
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgsqlConnectionクラス</param>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面データクラス</param>
    ''' <returns>Boolean True:正常終了 False:異常終了</returns>
    ''' <remarks>ひびきユーザーマスタデータを取得する
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報 : </p>
    ''' </para></remarks>
    Public Function GetHbnUsrMastaData(ByVal Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HbkUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))     'ひびきユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HbkUsrID").Value = dataHBKC0301.PropTxtHostID.Text             'ひびきユーザーID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常処理終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Adapter.SelectCommand)
            'メッセージ変数にエラーメッセージを格納
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 会議情報テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>会議情報テーブル取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMeetingTableSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '会議情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectMeetingTableSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKC0301, strSql, SEARCH_MODE_SEARCH) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索結果件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKC0301 As DataHBKC0301) As Boolean

        '検索結果件数取得用SQLを設定
        Dim strSql As String = strSelectCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKC0301, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0301">[IN]会議検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/09 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKC0301 As DataHBKC0301, _
                                     ByVal strSql As String,
                                     ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim intMeetingNmb As Integer = 0
        Dim intProcessNmb As Integer = 0

        Try

            '**********************************
            'SQL文設定
            '**********************************

            '変数の宣言
            'SQL文(SELECT)
            Dim sbSql As New StringBuilder(strSql)
            'SQL文(WHERE)
            Dim strWhere As New List(Of String)

            '**********************************
            'SQL文の生成(コントロールの中身を判定しながら条件追加)
            '**********************************

            With dataHBKC0301

                '会議番号(会議情報:テーブル定義はINTEGER型)(完全一致)
                If .PropTxtMeetingNmb.Text.Trim <> "" Then
                    strWhere.Add(" mt.MeetingNmb =:MeetingNmb")
                End If

                'プロセス区分(会議結果情報:IDを条件CHAR型)(完全一致)
                If .PropCmbProcessKbn.SelectedValue <> "" Then
                    strWhere.Add(" EXISTS(Select DISTINCT 1 FROM meeting_result_tb mrt WHERE mrt.MeetingNmb = mt.MeetingNmb AND mrt.ProcessKbn = :ProcessKbn)")
                End If

                'プロセス番号(会議結果情報:INTEGER型)(完全一致)
                If .PropTxtProcessNmb.Text.Trim <> "" Then
                    strWhere.Add(" EXISTS(Select DISTINCT 1 FROM meeting_result_tb mrt WHERE mrt.MeetingNmb = mt.MeetingNmb AND mrt.ProcessNmb = :ProcessNmb)")
                End If

                '実施予定日(FROM)(会議情報:TIMESTAMP型)(範囲指定)
                If .PropDtpYoteiDTFrom.txtDate.Text.Trim <> "" Then
                    strWhere.Add(" to_char(mt.YoteiSTDT,'yyyy/mm/dd') >= :YoteiDTFrom")
                End If

                '実施予定日(TO)(会議情報:TIMESTAMP型)(範囲指定)
                If .PropDtpYoteiDTTo.txtDate.Text.Trim <> "" Then
                    strWhere.Add(" to_char(mt.YoteiSTDT,'yyyy/mm/dd') <= :YoteiDTTo")
                End If

                '実施日(FROM)(会議情報:TIMESTAMP型)(範囲指定)
                If .PropDtpJisiDTFrom.txtDate.Text.Trim <> "" Then
                    strWhere.Add(" to_char(mt.JisiSTDT,'yyyy/mm/dd') >= :JisiDTFrom")
                End If

                '実施日(TO)(会議情報:TIMESTAMP型)(範囲指定)
                If .PropDtpJisiDTTo.txtDate.Text.Trim <> "" Then
                    strWhere.Add(" to_char(mt.JisiSTDT,'yyyy/mm/dd') <= :JisiDTTo")
                End If

                'タイトル(会議情報:VARCHAR型)(あいまい)
                If .PropTxtTitle.Text.Trim <> "" Then
                    strWhere.Add(" mt.TitleAimai LIKE :TitleAimai")
                End If

                '主催者グループCD(会議情報:IDを条件CHAR型)(完全一致)
                If .PropCmbHostGrpCD.SelectedValue <> "" Then
                    strWhere.Add(" mt.HostGrpCD = :HostGrpCD")
                End If

                '主催者ID(会議情報:IDを条件VARCHAR型)(完全一致)
                If .PropTxtHostID.Text.Trim <> "" Then
                    strWhere.Add(" mt.HostIDAimai = :HostIDAimai")
                End If

                '主催者氏名(会議情報:IDを条件VARCHAR型)(あいまい)
                If .PropTxtHostNM.Text.Trim <> "" Then
                    strWhere.Add(" mt.HostNMAimai LIKE :HostNMAimai")
                End If

                'WHERE句連結
                For i As Integer = 0 To strWhere.Count - 1
                    If i = 0 Then
                        sbSql.Append(" WHERE")
                    Else
                        sbSql.Append(" AND")
                    End If
                    sbSql.Append(strWhere(i))
                Next

                'ソート順指定(会議情報：実施予定開始日時(降順))(検索の場合のみ行う)
                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    ' sbSql.Append(" GROUP BY mt.MeetingNmb, mt.YoteiStDT, mt.JisiSTDT, mt.Title, gm.GroupNM, mt.HostNM")
                    sbSql.Append(" ORDER BY mt.YoteiSTDT DESC, mt.Title")
                    'デフォルトソート用のROWNUMを設定
                    sbSql.Insert(0, "SELECT Tx.*,row_number()over() AS SortNo FROM(")
                    sbSql.Append(") Tx")
                Else
                    'sbSql.Append(" GROUP BY mt.MeetingNmb")
                    'sbSql.Append(" ORDER BY mt.MeetingNmb) AS mt")
                End If

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

                '***************************************
                'バインド変数のセット
                '***************************************

                '会議番号
                If .PropTxtMeetingNmb.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("MeetingNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    If Integer.TryParse(.PropTxtMeetingNmb.Text.Trim, intMeetingNmb) = True Then
                        Adapter.SelectCommand.Parameters("MeetingNmb").Value = Integer.Parse(.PropTxtMeetingNmb.Text)
                    Else
                        Adapter.SelectCommand.Parameters("MeetingNmb").Value = 0
                    End If
                End If

                'プロセス区分
                If .PropCmbProcessKbn.SelectedValue <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ProcessKbn").Value = .PropCmbProcessKbn.SelectedValue
                End If

                'プロセス番号
                If .PropTxtProcessNmb.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    If Integer.TryParse(.PropTxtProcessNmb.Text.Trim, intProcessNmb) = True Then
                        Adapter.SelectCommand.Parameters("ProcessNmb").Value = Integer.Parse(.PropTxtProcessNmb.Text)
                    Else
                        Adapter.SelectCommand.Parameters("ProcessNmb").Value = 0
                    End If

                End If

                '実施予定日(FROM)
                If .PropDtpYoteiDTFrom.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("YoteiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("YoteiDTFrom").Value = .PropDtpYoteiDTFrom.txtDate.Text
                End If

                '実施予定日(TO)
                If .PropDtpYoteiDTTo.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("YoteiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("YoteiDTTo").Value = .PropDtpYoteiDTTo.txtDate.Text
                End If

                '実施日(FROM)
                If .PropDtpJisiDTFrom.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JisiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("JisiDTFrom").Value = .PropDtpJisiDTFrom.txtDate.Text
                End If

                '実施日(TO)
                If .PropDtpJisiDTTo.txtDate.Text <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JisiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("JisiDTTo").Value = .PropDtpJisiDTTo.txtDate.Text
                End If

                'タイトル(あいまい)
                If .PropTxtTitle.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TitleAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropTxtTitle.Text) & "%"
                End If

                '主催者グループCD
                If .PropCmbHostGrpCD.SelectedValue <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HostGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HostGrpCD").Value = .PropCmbHostGrpCD.SelectedValue
                End If

                '主催者ID
                If .PropTxtHostID.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HostIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HostIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropTxtHostID.Text)
                End If

                '主催者氏名(あいまい)
                If .PropTxtHostNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HostNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HostNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropTxtHostNM.Text) & "%"
                End If

                '【DELETE】2012/06/15 r.hoshino START
                ''結果区分
                'If intSearchFlg = SEARCH_MODE_SEARCH Then
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_NO", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：未入力
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_NO_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：未入力
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_OK", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：承認
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_OK_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：承認
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_NG", NpgsqlTypes.NpgsqlDbType.Varchar))       '結果区分：却下
                '    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Kbn_NG_NM", NpgsqlTypes.NpgsqlDbType.Varchar))    '結果区分名略称：却下

                '    Adapter.SelectCommand.Parameters("Kbn_NO").Value = SELECT_RESULTKBN_NO          '結果区分：未入力
                '    Adapter.SelectCommand.Parameters("Kbn_NO_NM").Value = SELECT_RESULTKBNNM_NO     '結果区分名略称：未入力
                '    Adapter.SelectCommand.Parameters("Kbn_OK").Value = SELECT_RESULTKBN_OK          '結果区分：承認
                '    Adapter.SelectCommand.Parameters("Kbn_OK_NM").Value = SELECT_RESULTKBNNM_OK     '結果区分名略称：承認
                '    Adapter.SelectCommand.Parameters("Kbn_NG").Value = SELECT_RESULTKBN_NG          '結果区分：却下
                '    Adapter.SelectCommand.Parameters("Kbn_NG_NM").Value = SELECT_RESULTKBNNM_NG     '結果区分名略称：却下
                'End If
                '【DELETE】2012/06/15 r.hoshino END
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

End Class