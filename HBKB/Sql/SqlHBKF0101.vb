Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' リリース検索一覧画面Sqlクラス
''' </summary>
''' <remarks>リリース検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/20 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKF0101

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'プロセスステータス取得SQL
    Private strSelectProcessStateSql As String = " SELECT " & vbCrLf & _
                                                                    " ProcessStateCD, " & vbCrLf & _
                                                                    " ProcessStateNM, " & vbCrLf & _
                                                                    " Defaultselectflg " & vbCrLf & _
                                                                " FROM " & vbCrLf & _
                                                                    " processstate_mtb " & vbCrLf & _
                                                                " WHERE ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                                " ORDER BY Sort "

    'グループ取得SQL
    Private strSelectGrpCDSql As String = " SELECT " & vbCrLf & _
                                                                " GroupCD, " & vbCrLf & _
                                                                " GroupNM " & vbCrLf & _
                                                            " FROM " & vbCrLf & _
                                                                " grp_mtb " & vbCrLf & _
                                                            " ORDER BY Sort "

    'プロセス区分取得SQL
    Private strSelectProccesLinkSql As String = "SELECT COALESCE(HBKF0011(:ProccesLinkKind,:ProcessLinkNum, '" & PROCESS_TYPE_RELEASE & "'),'0')"


    'リリース情報取得用SQL
    Private strSelectRelInfoSql As String = " SELECT " & vbCrLf & _
                                                                " RIT.RelNmb AS RelNmb, " & vbCrLf & _
                                                                " RIT.RelUkeNmb AS RelUkeNmb, " & vbCrLf & _
                                                                " PSM.ProcessStateNM AS ProcessStateNM, " & vbCrLf & _
                                                                " TO_CHAR(RIT.IraiDT,'YYYY/MM/DD') AS IraiDT, " & vbCrLf & _
                                                                " RIT.Title AS Title, " & vbCrLf & _
                                                                " CASE WHEN RIT.TujyoKinkyuKbn = '" & TUJYOKINKYU_KBN_NORMAL & "' THEN '" & TUJYOKINKYU_NM_NORMAL & "' " & vbCrLf & _
                                                                    " WHEN RIT.TujyoKinkyuKbn = '" & TUJYOKINKYU_KBN_EMERGENCY & "' THEN '" & TUJYOKINKYU_NM_EMERGENCY & "' " & vbCrLf & _
                                                                    " ELSE '' END AS TujyoKinkyuKbn, " & vbCrLf & _
                                                                " CASE WHEN RIT.UsrSyutiKbn = '" & USRSYUTI_KBN_UNFIN & "' THEN '" & USRSYUTI_NM_UNFIN & "' " & vbCrLf & _
                                                                    " WHEN RIT.UsrSyutiKbn = '" & USRSYUTI_KBN_FIN & "' THEN '" & USRSYUTI_NM_FIN & "' " & vbCrLf & _
                                                                    " ELSE '' END AS UsrSyutiKbn, " & vbCrLf & _
                                                                " TO_CHAR(RIT.RelSceDT,'YYYY/MM/DD HH24:MI') AS RelSceDT, " & vbCrLf & _
                                                                " TO_CHAR(RIT.RelStDT,'YYYY/MM/DD HH24:MI') AS RelStDT, " & vbCrLf & _
                                                                " TO_CHAR(RIT.RelEdDT,'YYYY/MM/DD HH24:MI') AS RelEdDT, " & vbCrLf & _
                                                                " HBKF0003(RIT.TantoGrpCD) AS TantoGrpNM, " & vbCrLf & _
                                                                " HBKF0004(RIT.RelTantoID) AS RelTantoNM, " & vbCrLf & _
                                                                " RIT.TantoGrpCD AS TantoGrpCD, " & vbCrLf & _
                                                                " RIT.RelTantoID AS RelTantoID, " & vbCrLf & _
                                                                " RIT.ProcessStateCD AS ProcessStateCD, " & vbCrLf

    'リリース情報件数取得用SQL
    Private strSelectRelCountSql As String = " SELECT " & vbCrLf & _
                                                                " COUNT(*) " & vbCrLf


    'ユーザマスタ取得用SQL
    Private strSelectTantoMstSql As String = "SELECT " & vbCrLf & _
                                                                    " hbkusrnm " & vbCrLf & _
                                                                    " , hbkusrnmkana " & vbCrLf & _
                                                                    " , ts.groupcd " & vbCrLf & _
                                                            " FROM hbkusr_mtb tu " & vbCrLf & _
                                                                " LEFT JOIN szk_mtb ts ON tu.hbkusrid=ts.hbkusrid " & vbCrLf & _
                                                                " INNER JOIN grp_mtb tg ON tg.groupcd = ts.groupcd " & vbCrLf & _
                                                            " WHERE tu.hbkusrid = :hbkusrid "

    ''' <summary>
    ''' ステータスリストボックス用データ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ステータスリストボックス用データ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessStateSql(ByVal dataHBKF0101 As DataHBKF0101, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection
                                                                ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProcessStateSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'CI種別CD
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("ProcessKbn").Value = PROCESS_TYPE_RELEASE

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
    ''' 担当者グループコンボボックス用データ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>担当者グループコンボボックス用データ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGrpCDSql(ByVal dataHBKF0101 As DataHBKF0101, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection
                                                                ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectGrpCDSql

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
    ''' リリース情報データ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース情報データ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelInfoSql(ByVal dataHBKF0101 As DataHBKF0101, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection
                                                                ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRelInfoSql

            'Where句設定
            If SetSqlWhereStatementl(dataHBKF0101, Adapter, Cn, strSQL, True) = False Then
                Return False
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
    ''' リリース情報件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース情報件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectRelCountSql(ByVal dataHBKF0101 As DataHBKF0101, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection
                                                                ) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectRelCountSql

            'Where句設定
            If SetSqlWhereStatementl(dataHBKF0101, Adapter, Cn, strSQL, False) = False Then
                Return False
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
    ''' リリース情報取得用SQLのWHERE句作成・設定処理
    ''' </summary>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="strSQL">[IN/OUT]WHERE句をセットするSQL文</param>
    ''' <param name="blnKbn">[IN]件数かデータ取得か判断する区分</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>リリース情報取得用SQLのWHERE句作成、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSqlWhereStatementl(ByVal dataHBKF0101 As DataHBKF0101, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, _
                                                                ByRef strSQL As String, _
                                                                ByRef blnKbn As Boolean
                                                                ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strFreeText() As String = Nothing           'フリーテキスト検索用配列
        Dim aryTitle() As String = Nothing              'タイトル検索用配列
        Dim aryGaiyo() As String = Nothing              '概要検索用配列

        Try
            With dataHBKF0101
                '並び順設定用(ORBER BY句順に行番号設定、デフォルトソート用)
                If blnKbn = True Then
                    strSQL &= " ROW_NUMBER() OVER(ORDER BY RIT.RelSceDT ASC NULLS FIRST , RIT.RelNmb DESC) AS Sort " & vbCrLf
                End If

                'FROM句の設定
                strSQL &= " FROM release_info_tb RIT " & vbCrLf & _
                               " LEFT OUTER JOIN processstate_mtb PSM " & vbCrLf & _
                               " ON PSM.ProcessStateCD = RIT.ProcessStateCD AND PSM.ProcessKbn = '" & PROCESS_TYPE_RELEASE & "' " & vbCrLf
                'WHERE句の設定
                strSQL &= " WHERE " & vbCrLf & _
                                    " ( EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG WHERE  " & vbCrLf & _
                                    " RKTG.RelationKbn = '" & KBN_GROUP & "' AND RKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                    " AND RKTG.RelNmb = RIT.RelNmb) " & vbCrLf & _
                                    " OR EXISTS (SELECT DISTINCT RKTG.RelNmb FROM release_kankei_tb RKTG  " & vbCrLf & _
                                    " WHERE RKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                    " RKTG.RelationID = '" & .PropStrLoginUserId & "' AND RKTG.RelNmb = RIT.RelNmb) ) " & vbCrLf

                'リリース番号
                If .PropStrRelNmb <> "" Then
                    strSQL &= " AND RIT.RelNmb = :RelNmb " & vbCrLf
                End If

                'リリース受付番号
                If .PropStrRelUkeNmb <> "" Then
                    strSQL &= " AND RIT.RelUkeNmbAimai = :RelUkeNmbAimai " & vbCrLf
                End If

                'ステータス（リストボックスで選択されている項目分ループし、カンマ区切りの文字列を生成
                If .PropStrProcessState <> "" Then
                    strSQL &= " AND RIT.ProcessStateCD IN (" & .PropStrProcessState & ") " & vbCrLf
                End If

                'タイトル
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列の分割
                    aryTitle = commonLogicHBK.GetSearchStringList(.PropStrTitle, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            strSQL &= " RIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '概要
                If .PropStrGaiyo.Trim <> "" Then
                    '検索文字列の分割
                    aryGaiyo = commonLogicHBK.GetSearchStringList(.PropStrGaiyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryGaiyo.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To aryGaiyo.Count - 1
                            strSQL &= " RIT.GaiyoAimai LIKE :GaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryGaiyo.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                'フリーテキスト検索(あいまい検索)
                If .PropStrBiko.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeText = commonLogicHBK.GetSearchStringList(.PropStrBiko, SPLIT_MODE_AND)
                    If strFreeText.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strSQL &= " RIT.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") "
                    End If
                End If

                'ユーザ周知必要有無
                If .PropStrUsrSyutiKbn <> "" Then
                    strSQL &= " AND RIT.UsrSyutiKbn = :UsrSyutiKbn " & vbCrLf
                End If

                '依頼日(FROM)
                If .PropStrIraiDTFrom <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.IraiDT,'YYYY/MM/DD') >= :IraiDTFrom " & vbCrLf
                End If

                '依頼日(TO)
                If .PropStrIraiDTTo <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.IraiDT,'YYYY/MM/DD') <= :IraiDTTo " & vbCrLf
                End If

                'リリース予定日(FROM)
                If .PropStrRelSceDTFrom <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.RelSceDT,'YYYY/MM/DD') >= :RelSceDTFrom " & vbCrLf
                End If

                'リリース予定日(TO)
                If .PropStrRelSceDTTo <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.RelSceDT,'YYYY/MM/DD') <= :RelSceDTTo " & vbCrLf
                End If

                'リリース着手日(FROM)
                If .PropStrRelStDTFrom <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.RelStDT,'YYYY/MM/DD') >= :RelStDTFrom " & vbCrLf
                End If

                'リリース着手日(TO)
                If .PropStrRelStDTTo <> "" Then
                    strSQL &= " AND TO_CHAR(RIT.RelStDT,'YYYY/MM/DD') <= :RelStDTTo " & vbCrLf
                End If

                '担当者グループ
                If .PropStrTantoGrpCD <> "" Then
                    strSQL &= " AND RIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If

                '担当者ID
                If .PropStrTantoID <> "" Then
                    strSQL &= " AND RIT.RelTantIDAimai = :RelTantID " & vbCrLf
                End If

                '担当者名
                If .PropStrTantoNM <> "" Then
                    strSQL &= " AND RIT.RelTantNMAimai LIKE :RelTantNM " & vbCrLf
                End If

                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    strSQL &= " AND RIT.FreeFlg1 = :FreeFlg1 " & vbCrLf
                End If

                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    strSQL &= " AND RIT.FreeFlg2 = :FreeFlg2 " & vbCrLf
                End If

                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    strSQL &= " AND RIT.FreeFlg3 = :FreeFlg3 " & vbCrLf
                End If

                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    strSQL &= " AND RIT.FreeFlg4 = :FreeFlg4 " & vbCrLf
                End If

                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    strSQL &= " AND RIT.FreeFlg5 = :FreeFlg5 " & vbCrLf
                End If

                'プロセスリンク
                If .PropStrProcessLinkNumAry <> "" Then
                    strSQL &= " AND RIT.RelNmb IN ( " & .PropStrProcessLinkNumAry & " )" & vbCrLf
                End If

                '並び順設定
                If blnKbn = True Then
                    strSQL &= " ORDER BY RIT.RelSceDT ASC NULLS FIRST , RIT.RelNmb DESC "
                End If

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

                'リリース番号
                If .PropStrRelNmb <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("RelNmb").Value = .PropStrRelNmb.Trim
                End If

                'リリース受付番号
                If .PropStrRelUkeNmb <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelUkeNmbAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelUkeNmbAimai").Value = .PropStrRelUkeNmb.Trim
                End If

                'タイトル
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

                '概要
                If .PropStrGaiyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryGaiyo.Count - 1
                        aryGaiyo(i) = commonLogicHBK.ChangeStringForSearch(aryGaiyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTitle.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("GaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("GaiyoAimai" + i.ToString).Value = "%" & aryGaiyo(i) & "%"
                    Next
                End If

                'フリーテキスト用のバインド変数設定
                If .PropStrBiko.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To strFreeText.Count - 1
                        strFreeText(i) = commonLogicHBK.ChangeStringForSearch(strFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To strFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" + strFreeText(i) + "%"
                    Next
                End If

                'ユーザ周知必要有無
                If .PropStrUsrSyutiKbn <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrSyutiKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrSyutiKbn").Value = .PropStrUsrSyutiKbn
                End If

                '依頼日(FROM)
                If .PropStrIraiDTFrom <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IraiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IraiDTFrom").Value = .PropStrIraiDTFrom
                End If

                '依頼日(TO)
                If .PropStrIraiDTTo <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IraiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IraiDTTo").Value = .PropStrIraiDTTo
                End If

                'リリース予定日(FROM)
                If .PropStrRelSceDTFrom <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelSceDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelSceDTFrom").Value = .PropStrRelSceDTFrom
                End If

                'リリース予定日(TO)
                If .PropStrRelSceDTTo <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelSceDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelSceDTTo").Value = .PropStrRelSceDTTo
                End If

                'リリース着手日(FROM)
                If .PropStrRelStDTFrom <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelStDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelStDTFrom").Value = .PropStrRelStDTFrom
                End If

                'リリース着手日(TO)
                If .PropStrRelStDTTo <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelStDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelStDTTo").Value = .PropStrRelStDTTo
                End If

                '担当者グループ
                If .PropStrTantoGrpCD <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropStrTantoGrpCD
                End If

                '担当者ID
                If .PropStrTantoID <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelTantID", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelTantID").Value = commonLogicHBK.ChangeStringForSearch(.PropStrTantoID)
                End If

                '担当者名
                If .PropStrTantoNM <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RelTantNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RelTantNM").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrTantoNM) & "%"
                End If

                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If

                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If

                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If

                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If

                'フリーフラグ5
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
    ''' プロセスリンク取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrProccesLinkKind">プロセス区分種別</param>
    ''' <param name="StrProcessLinkNum">プロセス区分番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスリンク取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
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
    ''' マスタデータ取得：担当
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKF0101">[IN/OUT]リリース検索一覧画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>指定ユーザー取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/20 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function GetIncTantoInfoData(ByRef Adapter As NpgsqlDataAdapter, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKF0101 As DataHBKF0101) As Boolean

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
                .Add(New NpgsqlParameter("hbkusrid", NpgsqlTypes.NpgsqlDbType.Varchar))        '担当ID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("hbkusrid").Value = dataHBKF0101.PropTxtTantoID.Text            '担当ID
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
