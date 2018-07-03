Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' 部所有機器検索一覧画面Sqlクラス
''' </summary>
''' <remarks>部所有機器検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/20 s.yamaguchi
''' <p>改訂情報:2012/07/11</p>
''' </para></remarks>
Public Class SqlHBKB1201

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数
    Private Const SEARCH_MODE_SEARCH As Integer = 0         '検索結果
    Private Const SEARCH_MODE_COUNT As Integer = 1          '検索件数

    '[SELECT]CIステータスマスタ取得SQL
    Private strSelectStatusMastaSql As String = "SELECT" & vbCrLf & _
                                                " cm.CIStateCD AS ID," & vbCrLf & _
                                                " cm.CIStateNM AS Text" & vbCrLf & _
                                                " FROM CISTATE_MTB cm" & vbCrLf & _
                                                " WHERE cm.JtiFlg = '0'" & vbCrLf & _
                                                " AND cm.CIKbnCD = :CIKbnCD" & vbCrLf & _
                                                " ORDER BY cm.Sort "

    '[SELECT]CI共通情報取得SQL
    Private strSelectCIInfoTableSql As String = "SELECT" & vbCrLf & _
                                                " km.KindNM," & vbCrLf & _
                                                " cit.Num," & vbCrLf & _
                                                " cbt.Aliau," & vbCrLf & _
                                                " cit.Class2," & vbCrLf & _
                                                " cit.CINM," & vbCrLf & _
                                                " csm.CIStateNM," & vbCrLf & _
                                                " (CASE WHEN cbt.ExpirationDT <> '' THEN TO_CHAR(TO_DATE(cbt.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                                "       ELSE ''" & vbCrLf & _
                                                "  END) AS ExpirationDT," & vbCrLf & _
                                                " (CASE WHEN cbt.NumInfoKbn = '" & NUMINFO_KBN_UNFIN & "' THEN '" & NUMINFO_NM_UNFIN & _
                                                "'      WHEN cbt.NumInfoKbn = '" & NUMINFO_KBN_FIN & "' THEN '" & NUMINFO_NM_FIN & _
                                                "'      ELSE ''" & _
                                                "  END) AS NumInfoKbn," & vbCrLf & _
                                                " (CASE WHEN cbt.SealSendkbn = '" & SEALSEND_KBN_UNFIN & "' THEN '" & SEALSEND_NM_UNFIN & _
                                                "'      WHEN cbt.SealSendkbn = '" & SEALSEND_KBN_FIN & "' THEN '" & SEALSEND_NM_FIN & _
                                                "'      ELSE ''" & _
                                                "  END) AS SealSendkbn," & vbCrLf & _
                                                " (CASE WHEN cbt.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_UNFIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_UNFIN & _
                                                "'      WHEN cbt.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_FIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_FIN & _
                                                "'      ELSE ''" & _
                                                "  END) AS AntiVirusSofCheckKbn," & vbCrLf & _
                                                " (CASE WHEN cbt.AntiVirusSofCheckDT <> '' THEN TO_CHAR(TO_DATE(cbt.AntiVirusSofCheckDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                                "       ELSE ''" & vbCrLf & _
                                                "  END) AS AntiVirusSofCheckDT," & vbCrLf & _
                                                " cbt.UsrBusyoNM," & vbCrLf & _
                                                " cbt.UsrID," & vbCrLf & _
                                                " cbt.UsrNM," & vbCrLf & _
                                                " cbt.ManageKyokuNM," & vbCrLf & _
                                                " cbt.ManageBusyoNM," & vbCrLf & _
                                                " cbt.SetBusyoNM," & vbCrLf & _
                                                " cit.Sort," & vbCrLf & _
                                                " cit.CINmb" & vbCrLf & _
                                                " FROM CI_INFO_TB AS cit" & vbCrLf & _
                                                " LEFT OUTER JOIN KIND_MTB km ON cit.KindCD = km.KindCD" & vbCrLf & _
                                                " LEFT OUTER JOIN CISTATE_MTB csm ON cit.CIStatusCD = csm.CIStateCD" & vbCrLf & _
                                                " LEFT OUTER JOIN CI_BUY_TB cbt ON cit.CINmb = cbt.CINmb"

    '[SELECT]部所有機器検索件数
    Private strSelectCountSql As String = "SELECT" & vbCrLf & _
                                          " COUNT(*)" & vbCrLf & _
                                          " FROM CI_INFO_TB AS cit" & vbCrLf & _
                                          " LEFT OUTER JOIN KIND_MTB km ON cit.KindCD = km.KindCD" & vbCrLf & _
                                          " LEFT OUTER JOIN CISTATE_MTB csm ON cit.CIStatusCD = csm.CIStateCD" & vbCrLf & _
                                          " LEFT OUTER JOIN CI_BUY_TB cbt ON cit.CINmb = cbt.CINmb" & vbCrLf


    ''' <summary>
    ''' CIステータスマスタ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1201">[IN]部所有機器検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIステータスマスタ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/06/20 s.yaamguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIStatusMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB1201 As DataHBKB1201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            '**********************************
            '* SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSQL = strSelectStatusMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            'CI種別CD(部所有機器=004)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_KIKI

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
    ''' CI共通情報テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1201">[IN]部所有機器検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報テーブル取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/06/21 s.yamaguchi
    ''' <p>改訂情報：2012/07/05 s.yamaguchi</p>
    ''' </para></remarks>
    Public Function SetSelectCIInfoTableSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB1201 As DataHBKB1201) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCIInfoTableSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKB1201, strSql, SEARCH_MODE_SEARCH) = False Then
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
    ''' 検索結果件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1201">[IN]部所有機器検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB1201 As DataHBKB1201) As Boolean

        '検索結果件数取得用SQLを設定
        Dim strSql As String = strSelectCountSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKB1201, strSql, SEARCH_MODE_COUNT) = False Then
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
    ''' <param name="dataHBKB1201">[IN]部所有機器検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="intSearchFlg">[IN]Sql判別フラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/05 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKB1201 As DataHBKB1201, _
                                     ByVal strSql As String,
                                     ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        'SELECT句とは別のWhere生成用変数
        Dim strSearchCondition As String = ""

        Try

            '**********************************
            'SQL文設定
            '**********************************

            '変数の宣言
            'SQL文(SELECT)
            Dim sbSql As New StringBuilder(strSql)
            Dim aryStrFreetext As String() = Nothing 'フリーテキスト検索用配列

            '**********************************
            'SQL文の生成(コントロールの中身を判定しながら条件追加)
            '**********************************

            With dataHBKB1201

                'CI番号
                sbSql.Append(" WHERE cit.CIKbnCD = :CIKbnCD")

                '番号(共通:テーブル定義はCHAR型)(完全一致)
                If .PropStrNumber.Trim <> "" Then
                    sbSql.Append(" AND cit.Num = LPAD(:Num, 5, '0')")
                End If
                'ステータス(共通:IDを条件CHAR型)(完全一致)
                If .PropStrStatus <> "" Then
                    sbSql.Append(" AND cit.CIStatusCD = :CIStatusCD")
                End If
                'ユーザID(部所有:VARCHAR)(完全一致)
                If .PropStrUserId.Trim <> "" Then
                    sbSql.Append(" AND cbt.UsrIDAimai = :UsrIDAimai")
                End If
                'ユーザ所属部署(あいまい)
                If .PropStrSyozokuBusyo.Trim <> "" Then
                    sbSql.Append(" AND cbt.UsrBusyoNMAimai LIKE :UsrBusyoNMAimai")
                End If
                '管理部署(あいまい)
                If .PropStrKanriBusyo.Trim <> "" Then
                    sbSql.Append(" AND cbt.ManageBusyoNMAimai LIKE :ManageBusyoNMAimai")
                End If
                '設置部署(あいまい)
                If .PropStrSettiBusyo.Trim <> "" Then
                    sbSql.Append(" AND cbt.SetBusyoNMAimai LIKE :SetBusyoNMAimai")
                End If
                'フリーテキスト(AND検索とOR検索がある)(あいまい)
                If .PropStrFreeText.Trim <> "" Then
                    'AND検索用に文字列を分割して取得
                    aryStrFreetext = commonLogicHBK.GetSearchStringList(.PropStrFreeText, SPLIT_MODE_AND)
                    'フリーテキスト検索条件作成
                    If CreateSqlFreeText(aryStrFreetext, sbSql) = False Then
                        Return False
                    End If
                End If
                'フリーフラグ1(CHAR型)(完全一致)
                If .PropStrFreeFlg1 <> "" Then
                    sbSql.Append(" AND cit.FreeFlg1 = :FreeFlg1")
                End If
                'フリーフラグ2(完全一致)
                If .PropStrFreeFlg2 <> "" Then
                    sbSql.Append(" AND cit.FreeFlg2 = :FreeFlg2")
                End If
                'フリーフラグ3(完全一致)
                If .PropStrFreeFlg3 <> "" Then
                    sbSql.Append(" AND cit.FreeFlg3 = :FreeFlg3")
                End If
                'フリーフラグ4(完全一致)
                If .PropStrFreeFlg4 <> "" Then
                    sbSql.Append(" AND cit.FreeFlg4 = :FreeFlg4")
                End If
                'フリーフラグ5(完全一致)
                If .PropStrFreeFlg5 <> "" Then
                    sbSql.Append(" AND cit.FreeFlg5 = :FreeFlg5")
                End If
                '検索時の処理
                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    '************************************************
                    'ORDER BY句を指定
                    'sbSql.Append(" ORDER BY cit.Sort")
                    ''ORDER BY句を指定
                    sbSql.Append(" ORDER BY cit.Num")
                    '************************************************
                End If

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString, Cn)

                '***************************************
                'バインド変数のセット
                '***************************************

                'CI種別CD(部所有機器=004)
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("CIKbnCD").Value = CI_TYPE_KIKI
                '番号(共通:テーブル定義はInteger型)(完全一致)
                If .PropStrNumber.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Num").Value = .PropStrNumber
                End If
                'ステータス(共通:IDを条件CHAR型)(完全一致)
                If .PropStrStatus <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIStatusCD").Value = .PropStrStatus
                End If
                'ユーザID(部所有:VARCHAR)(完全一致)
                If .PropStrUserId.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrUserId)
                End If
                'ユーザ所属部署(あいまい)
                If .PropStrSyozokuBusyo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrBusyoNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrSyozokuBusyo) & "%"
                End If
                '管理部署(あいまい)
                If .PropStrKanriBusyo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("ManageBusyoNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrKanriBusyo) & "%"
                End If
                '設置部署(あいまい)
                If .PropStrSettiBusyo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SetBusyoNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrSettiBusyo) & "%"
                End If
                'フリーテキスト(AND検索)(あいまい)
                If .PropStrFreeText.Trim <> "" Then
                    'AND条件の数だけバインド変数をセット
                    For index As Integer = 0 To aryStrFreetext.Length - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeText" & index, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeText" & index).Value = "%" & commonLogicHBK.ChangeStringForSearch(aryStrFreetext(index).ToString) & "%"
                    Next
                End If
                'フリーフラグ1(CHAR型)(完全一致)
                If .PropStrFreeFlg1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If
                'フリーフラグ2(完全一致)
                If .PropStrFreeFlg2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If
                'フリーフラグ3(完全一致)
                If .PropStrFreeFlg3 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If
                'フリーフラグ4(完全一致)
                If .PropStrFreeFlg4 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If
                'フリーフラグ5(完全一致)
                If .PropStrFreeFlg5 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
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
    ''' フリーテキスト検索条件の作成処理
    ''' </summary>
    ''' <param name="aryStrFreetext">[IN]AND検索条件対象データ配列</param>
    ''' <param name="sbSql">[IN/OUT]CI共通情報テーブル取得用SQL文字列</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>フリーテキストがスペース区切りで入力された際のAND条件のSQLを作成する
    ''' <para>作成情報：2012/06/25 s.yamguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateSqlFreeText(ByVal aryStrFreetext As String(), _
                                       ByRef sbSql As StringBuilder) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim commonLogicHBK As New CommonLogicHBK

        Try

            'AND条件の数だけ条件文の生成
            For index As Integer = 0 To aryStrFreetext.Length - 1 Step 1
                '初回判定
                If index = 0 Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If
                '条件式の追加
                sbSql.Append(" cit.BikoAimai ")
                sbSql.Append(" LIKE ").Append(":FreeText" & index)
            Next
            If (aryStrFreetext.Length > 0) Then
                sbSql.Append(" ) ")
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '処理成功
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, Nothing, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class