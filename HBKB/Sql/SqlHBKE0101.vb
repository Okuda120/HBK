Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' 変更検索一覧画面Sqlクラス
''' </summary>
''' <remarks>変更検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報</p>
''' </para></remarks>
Public Class SqlHBKE0101

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const SEARCH_MODE_COUNT As String = "0" 'カウント
    Private Const SEARCH_MODE_SEARCH As String = "1" '検索

    '[SELECT]グループマスター取得SQL
    Private strSelectGrp As String = "SELECT " & vbCrLf & _
                                     " gm.GroupCD " & vbCrLf & _
                                     ",gm.GroupNM " & vbCrLf & _
                                     " FROM grp_mtb AS gm " & vbCrLf & _
                                     " ORDER BY gm.Sort "

    '[SELECT]プロセスステータスマスター取得SQL
    Private strSelectProcessState As String = "SELECT " & vbCrLf & _
                                              " psm.ProcessStateCD " & vbCrLf & _
                                              ",psm.ProcessStateNM " & vbCrLf & _
                                              ",psm.Defaultselectflg " & vbCrLf & _
                                              " FROM processstate_mtb AS psm " & vbCrLf & _
                                              " WHERE psm.ProcessKbn = :ProcessKbn " & vbCrLf & _
                                              " ORDER BY psm.Sort "

    '[SELECT]対象システム取得SQL
    Private strSelectTargetSystem As String = "SELECT " & vbCrLf & _
                                              " cit.CINmb " & vbCrLf & _
                                              ",cit.CINM || ' ' || cit.Class1 || ' ' || cit.Class2 AS SystemNM " & vbCrLf & _
                                              " FROM ( " & vbCrLf & _
                                              " SELECT cinmb,kindcd,class1,class2,cinm,'1' as sort0,sort " & vbCrLf & _
                                              " FROM  ci_info_tb " & vbCrLf & _
                                              " WHERE cistatuscd <> :CIStatusCd AND cikbncd= :CIKbnCD " & vbCrLf & _
                                              " UNION " & vbCrLf & _
                                              " SELECT cinmb,kindcd,class1,class2,cinm,'2' as sort0,sort " & vbCrLf & _
                                              " FROM  ci_info_tb " & vbCrLf & _
                                              " WHERE cistatuscd = :CIStatusCd  AND cikbncd= :CIKbnCD " & vbCrLf & _
                                              " ) AS cit " & vbCrLf & _
                                              " ORDER BY Sort0,Sort "

    '[SELECT]変更検索件数
    Private strSelectCountSql As String = "SELECT " & vbCrLf & _
                                          " COUNT(cit.ChgNmb) " & vbCrLf & _
                                          " FROM change_info_tb AS cit " & vbCrLf & _
                                          " LEFT OUTER JOIN processstate_mtb pm ON cit.ProcessStateCD = pm.ProcessStateCD " & vbCrLf & _
                                          " LEFT OUTER JOIN ci_info_tb AS ci ON cit.SystemNmb = ci.CINmb " & vbCrLf & _
                                          " LEFT OUTER JOIN grp_mtb AS gm ON cit.TantoGrpCD = gm.GroupCD "

    '[SELECT]変更検索結果
    Private strSelectIncidentInfoSql As String = "SELECT " & vbCrLf & _
                                                 " cit.ChgNmb " & vbCrLf & _
                                                 ",pm.ProcessStateNM " & vbCrLf & _
                                                 ",cit.kaisidt " & vbCrLf & _
                                                 ",cit.Title " & vbCrLf & _
                                                 ",it.CINM " & vbCrLf & _
                                                 ",gm.GroupNM " & vbCrLf & _
                                                 ",cit.ChgTantoNM " & vbCrLf & _
                                                 ",cit.ProcessStateCD " & vbCrLf & _
                                                 ",cit.ChgTantoID " & vbCrLf & _
                                                 ",cit.TantoGrpCD " & vbCrLf & _
                                                 " FROM change_info_tb AS cit " & vbCrLf & _
                                                 " LEFT OUTER JOIN processstate_mtb AS pm ON cit.ProcessStateCD = pm.ProcessStateCD " & vbCrLf & _
                                                 " LEFT OUTER JOIN ci_info_tb AS it ON cit.SystemNmb = it.CINmb " & vbCrLf & _
                                                 " LEFT OUTER JOIN grp_mtb AS gm ON cit.TantoGrpCD = gm.GroupCD "

    '[SELECT]プロセス区分取得SQL
    Private strSelectProccesLinkSql As String = "SELECT COALESCE(HBKF0011(:ProccesLinkKind,:ProcessLinkNum,'" & PROCESS_TYPE_CHANGE & "'),'0')"

    '[SELECT]ユーザマスタ取得用SQL
    Private strSelectTantoMstSql As String = "SELECT " & vbCrLf & _
                                             " hbkusrnm " & vbCrLf & _
                                             ",hbkusrnmkana " & vbCrLf & _
                                             ",ts.groupcd " & vbCrLf & _
                                             " FROM hbkusr_mtb tu " & vbCrLf & _
                                             " LEFT JOIN szk_mtb ts ON tu.hbkusrid = ts.hbkusrid " & vbCrLf & _
                                             " INNER JOIN grp_mtb tg ON tg.groupcd = ts.groupcd " & vbCrLf & _
                                             " WHERE tu.hbkusrid = :hbkusrid "

    ''' <summary>
    ''' プロセスリンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrProccesLinkKind">プロセス区分種別</param>
    ''' <param name="StrProcessLinkNum">プロセス区分番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetProccesLinkSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrProccesLinkKind As String, ByVal StrProcessLinkNum As String) As Boolean


        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim intNum As Integer
        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectProccesLinkSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProccesLinkKind", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessLinkNum", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("ProccesLinkKind").Value = StrProccesLinkKind
            If StrProcessLinkNum <> "" Then
                If Integer.TryParse(StrProcessLinkNum, intNum) = True Then
                    '数値型の場合
                    Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = Integer.Parse(StrProcessLinkNum)
                Else
                    '数値型ではない場合
                    Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = 0
                End If
            Else
                'プロセスリンク情報の番号が未入力の場合
                Adapter.SelectCommand.Parameters("ProcessLinkNum").Value = DBNull.Value
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' グループマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGrpSql(ByRef Adapter As NpgsqlDataAdapter, _
                                    ByVal Cn As NpgsqlConnection, _
                                    ByVal dataHBKE0101 As dataHBKE0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectGrp

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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' プロセスステータスマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスステータスマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessStateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectProcessState

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'プロセス区分(インシデント=001)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("ProcessKbn").Value = PROCESS_TYPE_CHANGE

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 対象システム取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対象システム取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectTargetSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectTargetSystem

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'CI種別CD(システム=001)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM
            'CIステータスCD(廃棄済=103)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCd", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIStatusCd").Value = CI_STATUS_SYSTEM_HAISHIZUMI

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 変更検索件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>変更検索件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKE0101 As DataHBKE0101) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateChangeInfoSql(Adapter, Cn, dataHBKE0101, strSql, SEARCH_MODE_COUNT) = False Then
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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 変更検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>変更検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectChangeInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKE0101 As DataHBKE0101) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectIncidentInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateChangeInfoSql(Adapter, Cn, dataHBKE0101, strSql, SEARCH_MODE_SEARCH) = False Then
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
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN]変更検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="strSearchMode">[IN]Sql判別モード</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateChangeInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKE0101 As DataHBKE0101, _
                                           ByVal strSql As String, _
                                           ByVal strSearchMode As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件
        Dim aryTitle() As String = Nothing              'タイトル
        Dim aryNaiyo() As String = Nothing              '内容
        Dim aryTaisyo() As String = Nothing             '対処
        Dim aryFreeText() As String = Nothing           'フリーテキスト
        Dim aryCysprNmb() As String = Nothing           'CysprNmb番号

        Try

            With dataHBKE0101

                strSelect = strSql

                '前提条件
                strSearch &= " WHERE " & vbCrLf & _
                            " ( EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG WHERE " & vbCrLf & _
                            " CKTG.RelationKbn = '" & KBN_GROUP & "' AND CKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                            " AND CKTG.ChgNmb = CIT.ChgNmb) " & vbCrLf & _
                            " OR EXISTS (SELECT DISTINCT CKTG.ChgNmb FROM Change_kankei_tb CKTG " & vbCrLf & _
                            " WHERE CKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                            " CKTG.RelationID = '" & .PropStrLoginUserId & "' AND CKTG.ChgNmb = CIT.ChgNmb) ) " & vbCrLf

                '変更番号(完全一致)
                If .PropBlnChgNumInputFlg = False Then
                    strSearch &= " AND cit.ChgNmb = :ChgNmb" & vbCrLf
                End If

                'ステータス(完全一致)
                If .PropStrStatus <> Nothing Then
                    strSearch &= " AND cit.ProcessStateCD IN (" & .PropStrStatus & ")" & vbCrLf
                End If

                '対象システム(完全一致)
                If .PropStrTargetSystem <> Nothing Then
                    strSearch &= " AND cit.SystemNmb IN (" & .PropStrTargetSystem & ")" & vbCrLf
                End If

                'タイトル(あいまい検索)
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列の分割
                    aryTitle = commonLogicHBK.GetSearchStringList(.PropStrTitle, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            strSearch &= " cit.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If

                '内容(あいまい検索)
                If .PropStrNaiyo.Trim <> "" Then
                    '検索文字列の分割
                    aryNaiyo = commonLogicHBK.GetSearchStringList(.PropStrNaiyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            strSearch &= " cit.NaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If

                '対処(あいまい検索)
                If .PropStrTaisyo.Trim <> "" Then
                    '検索文字列の分割
                    aryTaisyo = commonLogicHBK.GetSearchStringList(.PropStrTaisyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTaisyo.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTaisyo.Count - 1
                            strSearch &= " cit.TaisyoAimai LIKE :TaisyoAimai" + intCnt.ToString()
                            If intCnt <> aryTaisyo.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If

                'CYSPR
                If .PropStrCyspr.Trim <> "" Then
                    '検索文字列の分割
                    aryCysprNmb = commonLogicHBK.GetSearchStringList(.PropStrCyspr, SPLIT_MODE_OR)
                    '分割分だけ検索条件の設定
                    If aryCysprNmb.Length <> 0 Then
                        strSearch &= " AND cit.ChgNmb IN (SELECT cct.ChgNmb FROM Change_cyspr_tb cct WHERE " & vbCrLf
                        For intCnt = 0 To aryCysprNmb.Count - 1
                            strSearch &= " cct.CysprNmbAimai LIKE :CysprNmbAimai" + intCnt.ToString()
                            If intCnt <> aryCysprNmb.Count - 1 Then
                                strSearch &= " OR "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If

                '開始日(From)
                If .PropStrkaisidtFrom.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.kaisidt,'YYYY/MM/DD') >= :kaisidtFrom" & vbCrLf
                End If

                '開始日(To)
                If .PropStrkaisidtTo.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.kaisidt,'YYYY/MM/DD') <= :kaisidtTo" & vbCrLf
                End If

                '完了日(From)
                If .PropStrKanryoDTFrom.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.KanryoDT,'YYYY/MM/DD') >= :KanryoDTFrom" & vbCrLf
                End If

                '完了日(To)
                If .PropStrKanryoDTTo.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.KanryoDT,'YYYY/MM/DD') <= :KanryoDTTo" & vbCrLf
                End If

                '登録日(From)
                If .PropStrTorokuDTFrom.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.RegDT,'YYYY/MM/DD') >= :TorokuDTFrom" & vbCrLf
                End If

                '登録日(To)
                If .PropStrTorokuDTTo.Trim <> "" Then
                    strSearch &= " AND"
                    strSearch &= " TO_CHAR(cit.RegDT,'YYYY/MM/DD') <= :TorokuDTTo" & vbCrLf
                End If

                '[Mod]2014/11/19 e.okamura 問題要望114 Start
                ''最終更新日時(From)
                'If .PropStrUpdateDTFrom.Trim <> "" Then
                '    strSearch &= " AND"
                '    strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD HH24:MI') >= :UpdateDTFrom" & vbCrLf
                'End If
                '
                ''最終更新日時(To)
                'If .PropStrUpdateDTTo.Trim <> "" Then
                '    strSearch &= " AND"
                '    strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD HH24:MI') <= :UpdateDTTo" & vbCrLf
                'End If

                '最終更新日時(FROM)
                If .PropStrUpdateDTFrom.Trim <> "" Then
                    If .PropTxtExUpdateTimeFrom.PropTxtTime.Text.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD HH24:MI') >= :UpdateDTFrom" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD') >= :UpdateDTFrom" & vbCrLf
                    End If
                End If

                '最終更新日時(TO)
                If .PropStrUpdateDTTo.Trim <> "" Then
                    If .PropTxtExUpdateTimeTo.PropTxtTime.Text.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD HH24:MI') <= :UpdateDTTo" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD') <= :UpdateDTTo" & vbCrLf
                    End If
                End If
                '[Mod]2014/11/19 e.okamura 問題要望114 End

                'フリーテキスト検索(あいまい検索)
                If .PropStrFreeText.Trim <> "" Then
                    '検索文字列の分割
                    aryFreeText = commonLogicHBK.GetSearchStringList(.PropStrFreeText, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryFreeText.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryFreeText.Count - 1
                            strSearch &= " cit.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> aryFreeText.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If

                'フリーフラグ1(完全一致)
                If .PropStrFreeFlg1.Trim <> "" Then
                    strSearch &= " AND cit.FreeFlg1 = :FreeFlg1" & vbCrLf
                End If

                'フリーフラグ2(完全一致)
                If .PropStrFreeFlg2.Trim <> "" Then
                    strSearch &= " AND cit.FreeFlg2 = :FreeFlg2" & vbCrLf
                End If

                'フリーフラグ3(完全一致)
                If .PropStrFreeFlg3.Trim <> "" Then
                    strSearch &= " AND cit.FreeFlg3 = :FreeFlg3" & vbCrLf
                End If

                'フリーフラグ4(完全一致)
                If .PropStrFreeFlg4.Trim <> "" Then
                    strSearch &= " AND cit.FreeFlg4 = :FreeFlg4" & vbCrLf
                End If

                'フリーフラグ5(完全一致)
                If .PropStrFreeFlg5.Trim <> "" Then
                    strSearch &= " AND cit.FreeFlg5 = :FreeFlg5" & vbCrLf
                End If

                '担当者グループ
                If .PropStrTantoGrp.Trim <> "" Then
                    strSearch &= " AND cit.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                '変更担当者ID(あいまい)
                If .PropStrTantoID.Trim <> "" Then
                    strSearch &= " AND cit.ChgTantIDAimai LIKE :ChgTantIDAimai " & vbCrLf
                End If

                '変更担当者氏名(あいまい)
                If .PropStrTantoNM.Trim <> "" Then
                    strSearch &= " AND cit.ChgTantNMAimai LIKE :ChgTantNMAimai " & vbCrLf
                End If

                'プロセスリンク
                If .PropStrProcessLinkNumAry <> "" Then
                    strSearch &= " AND cit.ChgNmb IN ( " & .PropStrProcessLinkNumAry & " )" & vbCrLf
                End If

                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
                '検索時の処理
                If strSearchMode = SEARCH_MODE_SEARCH Then
                    'ORDER BY句を指定
                    strSearch &= " ORDER BY cit.ChgNmb Desc "
                End If

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                '変更番号(完全一致)
                If .PropBlnChgNumInputFlg = False Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ChgNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("ChgNmb").Value = .PropStrChgNmb.Trim
                End If

                'タイトル(あいまい検索)
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTitle.Count - 1
                        aryTitle(i) = commonLogicHBK.ChangeStringForSearch(aryTitle(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTitle.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TitleAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TitleAimai" + i.ToString).Value = "%" & aryTitle(i) & "%"
                    Next
                End If

                '内容(あいまい検索)
                If .PropStrNaiyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryNaiyo.Count - 1
                        aryNaiyo(i) = commonLogicHBK.ChangeStringForSearch(aryNaiyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryNaiyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("NaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("NaiyoAimai" + i.ToString).Value = "%" & aryNaiyo(i) & "%"
                    Next
                End If

                '対処(あいまい検索)
                If .PropStrTaisyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTaisyo.Count - 1
                        aryTaisyo(i) = commonLogicHBK.ChangeStringForSearch(aryTaisyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTaisyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TaisyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TaisyoAimai" + i.ToString).Value = "%" & aryTaisyo(i) & "%"
                    Next
                End If

                'CYSPR
                If .PropStrCyspr.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryCysprNmb.Count - 1
                        aryCysprNmb(i) = commonLogicHBK.ChangeStringForSearch(aryCysprNmb(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryCysprNmb.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CysprNmbAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("CysprNmbAimai" + i.ToString).Value = "%" + aryCysprNmb(i) + "%"
                    Next
                End If

                '開始日(From)
                If .PropStrkaisidtFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kaisidtFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kaisidtFrom").Value = .PropStrkaisidtFrom
                End If

                '開始日(To)
                If .PropStrkaisidtTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kaisidtTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kaisidtTo").Value = .PropStrkaisidtTo
                End If

                '完了日(From)
                If .PropStrKanryoDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KanryoDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KanryoDTFrom").Value = .PropStrKanryoDTFrom
                End If

                '完了日(To)
                If .PropStrKanryoDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KanryoDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KanryoDTTo").Value = .PropStrKanryoDTTo
                End If

                '登録日(From)
                If .PropStrTorokuDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TorokuDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TorokuDTFrom").Value = .PropStrTorokuDTFrom
                End If

                '登録日(To)
                If .PropStrTorokuDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TorokuDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TorokuDTTo").Value = .PropStrTorokuDTTo
                End If

                '最終更新日時(From)
                If .PropStrUpdateDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTFrom").Value = .PropStrUpdateDTFrom
                End If

                '最終更新日時(To)
                If .PropStrUpdateDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTTo").Value = .PropStrUpdateDTTo
                End If

                'フリーテキスト検索(あいまい検索)
                If .PropStrFreeText.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryFreeText.Count - 1
                        aryFreeText(i) = commonLogicHBK.ChangeStringForSearch(aryFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" & aryFreeText(i) & "%"
                    Next
                End If

                'フリーフラグ1
                If .PropStrFreeFlg1.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If

                'フリーフラグ2
                If .PropStrFreeFlg2.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If

                'フリーフラグ3
                If .PropStrFreeFlg3.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If

                'フリーフラグ4
                If .PropStrFreeFlg4.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If

                'フリーフラグ5
                If .PropStrFreeFlg5.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
                End If

                '担当者グループ
                If .PropStrTantoGrp.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropStrTantoGrp
                End If

                '変更担当者ID
                If .PropStrTantoID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ChgTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ChgTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrTantoID.Trim)
                End If

                '変更担当者氏名 
                If .PropStrTantoNM <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ChgTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ChgTantNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrTantoNM.Trim) & "%"
                End If

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' マスタデータ取得：担当
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0101">[IN/OUT]変更検索一覧画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定ユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetIncTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKE0101 As DataHBKE0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectTantoMstSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("hbkusrid", NpgsqlTypes.NpgsqlDbType.Varchar))     '担当ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("hbkusrid").Value = dataHBKE0101.PropTxtTantoID.Text            '担当ID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
