Imports Npgsql
Imports Common
Imports CommonHBK

''' <summary>
''' 変更検索一覧Excel出力Sqlクラス
''' </summary>
''' <remarks>変更検索一覧Excel出力のSQLの作成・設定を行う
''' <para>作成情報：2012/08/24 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKE0102

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '[SELECT]変更検索結果
    Private strSelectChangeInfoSql As String = " SELECT " & vbCrLf & _
                                                " CIT.ChgNmb, " & vbCrLf & _
                                                " PSM.ProcessStateNM, " & vbCrLf & _
                                                " TO_CHAR(CIT.kaisidt,'YYYY/MM/DD HH24:MI') AS kaisidt, " & vbCrLf & _
                                                " TO_CHAR(CIT.KanryoDT,'YYYY/MM/DD HH24:MI') AS KanryoDT, " & vbCrLf & _
                                                " CIT.Title, " & vbCrLf & _
                                                " CIT.Naiyo, " & vbCrLf & _
                                                " CIT.Taisyo, " & vbCrLf & _
                                                " CIT.ApproverID, " & vbCrLf & _
                                                " CIT.ApproverNM, " & vbCrLf & _
                                                " CIT.RecorderID, " & vbCrLf & _
                                                " CIT.RecorderNM, " & vbCrLf & _
                                                " CI.CINM, " & vbCrLf & _
                                                " PCT.CysprNmb, " & vbCrLf & _
                                                " HBKF0003(CIT.TantoGrpCD), " & vbCrLf & _
                                                " CIT.ChgTantoID, " & vbCrLf & _
                                                " CIT.ChgTantoNM, " & vbCrLf & _
                                                " TO_CHAR(CIT.RegDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                                " HBKF0003(CIT.RegGrpCD), " & vbCrLf & _
                                                " HBKF0004(CIT.RegID), " & vbCrLf & _
                                                " TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                                " HBKF0003(CIT.UpGrpCD), " & vbCrLf & _
                                                " HBKF0004(CIT.UpdateID) " & vbCrLf & _
                                                " FROM " & vbCrLf & _
                                                " Change_info_tb CIT " & vbCrLf & _
                                                " LEFT OUTER JOIN ci_info_tb CI ON CI.CINmb = CIT.SystemNmb " & vbCrLf & _
                                                " LEFT OUTER JOIN (SELECT ChgNmb , STRING_AGG(CysprNmb, '／') AS CysprNmb FROM Change_cyspr_tb GROUP BY ChgNmb) PCT ON CIT.ChgNmb = PCT.ChgNmb " & vbCrLf & _
                                                " LEFT OUTER JOIN processstate_mtb PSM ON CIT.ProcessStateCD = PSM.ProcessStateCD AND PSM.ProcessKbn = '" & PROCESS_TYPE_CHANGE & "' "

    ''' <summary>
    ''' 変更検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKE0102">[IN/OUT]変更検索一覧Excel出力Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>変更検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectChangeInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKE0102 As DataHBKE0102) As Boolean

        '変更情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectChangeInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSqlWhereStatementl(dataHBKE0102, Adapter, Cn, strSql) = False Then
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
    ''' 変更情報取得用SQLのWHERE句作成・設定処理
    ''' </summary>
    ''' <param name="dataHBKE0102">[IN/OUT]変更検索一覧Excel出力Dataクラス</param>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="strSQL">[IN/OUT]WHERE句をセットするSQL文</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>変更情報取得用SQLのWHERE句作成、アダプタにセットする
    ''' <para>作成情報：2012/08/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSqlWhereStatementl(ByVal dataHBKE0102 As DataHBKE0102, _
                                        ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByRef strSQL As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strFreeText() As String = Nothing           'フリーテキスト検索用配列
        Dim aryTitle() As String = Nothing              'タイトル
        Dim aryNaiyo() As String = Nothing              '内容
        Dim aryTaiSyo() As String = Nothing             '対処
        Dim aryCysprNmb() As String = Nothing           'CysprNmb番号

        Try
            With dataHBKE0102

                strSQL &= " WHERE " & vbCrLf & _
                                    " ( EXISTS (SELECT DISTINCT PKTG.ChgNmb FROM Change_kankei_tb PKTG WHERE " & vbCrLf & _
                                    " PKTG.RelationKbn = '" & KBN_GROUP & "' AND PKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                    " AND PKTG.ChgNmb = CIT.ChgNmb) " & vbCrLf & _
                                    " OR EXISTS (SELECT DISTINCT PKTG.ChgNmb FROM Change_kankei_tb PKTG " & vbCrLf & _
                                    " WHERE PKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                    " PKTG.RelationID = '" & .PropStrLoginUserId & "' AND PKTG.ChgNmb = CIT.ChgNmb) ) " & vbCrLf

                '番号
                If .PropStrChgNmb <> "" Then
                    strSQL &= " AND CIT.ChgNmb = :ChgNmb " & vbCrLf
                End If

                'ステータス（リストボックスで選択されている項目分ループし、カンマ区切りの文字列を生成
                If .PropStrProcessState <> "" Then
                    strSQL &= "AND CIT.ProcessStateCD IN (" & .PropStrProcessState & ") " & vbCrLf
                End If

                '対象システム（リストボックスで選択されている項目分ループし、カンマ区切りの文字列を生成
                If .PropStrTargetSys <> "" Then
                    strSQL &= " AND CIT.SystemNmb IN  (" & .PropStrTargetSys & ") " & vbCrLf
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
                            strSQL &= " CIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '内容
                If .PropStrNaiyo.Trim <> "" Then
                    '検索文字列の分割
                    aryNaiyo = commonLogicHBK.GetSearchStringList(.PropStrNaiyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryNaiyo.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To aryNaiyo.Count - 1
                            strSQL &= " CIT.NaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryNaiyo.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '対処
                If .PropStrTaisyo.Trim <> "" Then
                    '検索文字列の分割
                    aryTaiSyo = commonLogicHBK.GetSearchStringList(.PropStrTaisyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTaiSyo.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To aryTaiSyo.Count - 1
                            strSQL &= " CIT.TaisyoAimai LIKE :TaisyoAimai" + intCnt.ToString()
                            If intCnt <> aryTaiSyo.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '開始日(FROM)
                If .PropStrStartDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.kaisidt,'YYYY/MM/DD') >= :kaisidtFrom " & vbCrLf
                End If

                '開始日(TO)
                If .PropStrStartDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.kaisidt,'YYYY/MM/DD') <= :kaisidtTo " & vbCrLf
                End If

                '完了日(FROM)
                If .PropStrKanryoDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.KanryoDT,'YYYY/MM/DD') >= :KanryoDTFrom " & vbCrLf
                End If

                '完了日(TO)
                If .PropStrKanryoDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.KanryoDT,'YYYY/MM/DD') <= :KanryoDTTo " & vbCrLf
                End If

                '登録日(FROM)
                If .PropStrRegDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom " & vbCrLf
                End If

                '登録日(TO)
                If .PropStrRegDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(CIT.RegDT,'YYYY/MM/DD') <= :RegDTTo " & vbCrLf
                End If

                '[Mod]2014/11/19 e.okamura 問題要望114 Start
                ''最終更新日時(FROM)
                'If .PropStrLastRegDTFrom.Trim <> "" Then
                '    strSQL &= " AND TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= TO_CHAR(TO_TIMESTAMP(:LastRegDTFrom,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                'End If

                ''最終更新日時(TO)
                'If .PropStrLastRegDTTo.Trim <> "" Then
                '    strSQL &= " AND TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= TO_CHAR(TO_TIMESTAMP(:LastRegDTTo,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                'End If

                '最終更新日時(FROM)
                If .PropStrLastRegDTFrom.Trim <> "" Then
                    If .PropStrLastRegTimeFrom.Trim <> "" Then
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= :LastRegDTFrom" & vbCrLf
                    Else
                        '時間表記なし
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD') >= :LastRegDTFrom" & vbCrLf
                    End If
                End If

                '最終更新日時(TO)
                If .PropStrLastRegDTTo.Trim <> "" Then
                    If .PropStrLastRegTimeTo.Trim <> "" Then
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= :LastRegDTTo" & vbCrLf
                    Else
                        '時間表記なし
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(CIT.UpdateDT,'YYYY/MM/DD') <= :LastRegDTTo" & vbCrLf
                    End If
                End If
                '[Mod]2014/11/19 e.okamura 問題要望114 End

                'CYSPR
                If .PropStrCysprNmb.Trim <> "" Then
                    '検索文字列の分割
                    aryCysprNmb = commonLogicHBK.GetSearchStringList(.PropStrCysprNmb, SPLIT_MODE_OR)
                    '分割分だけ検索条件の設定
                    If aryCysprNmb.Length <> 0 Then
                        strSQL &= " AND CIT.ChgNmb IN (SELECT CYT.ChgNmb FROM Change_cyspr_tb CYT WHERE " & vbCrLf
                        For intCnt = 0 To aryCysprNmb.Count - 1
                            strSQL &= " CYT.CysprNmbAimai LIKE :CysprNmbAimai" + intCnt.ToString()
                            If intCnt <> aryCysprNmb.Count - 1 Then
                                strSQL &= " OR "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '担当者グループ
                If .PropStrTantoGrpCD <> "" Then
                    strSQL &= " AND CIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                End If
                '担当者ID
                If .PropStrTantoID.Trim <> "" Then
                    strSQL &= " AND CIT.ChgTantIDAimai = :TantIDAimai " & vbCrLf
                End If
                '担当者氏名 
                If .PropStrTantoNM.Trim <> "" Then
                    strSQL &= " AND CIT.ChgTantNMAimai LIKE :TantNMAimai " & vbCrLf
                End If

                'プロセスリンク
                If .PropStrKindCD <> "" Then
                    strSQL &= " AND CIT.ChgNmb IN ( " & .PropStrKindCD & " )" & vbCrLf
                End If

                'フリーテキスト検索(あいまい検索)
                If .PropStrBiko.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrBiko, SPLIT_MODE_AND)

                    If strFreeText.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strSQL &= " CIT.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") "
                    End If
                End If

                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    strSQL &= " AND CIT.FreeFlg1 = :FreeFlg1 " & vbCrLf
                End If

                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    strSQL &= " AND CIT.FreeFlg2 = :FreeFlg2 " & vbCrLf
                End If

                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    strSQL &= " AND CIT.FreeFlg3 = :FreeFlg3 " & vbCrLf
                End If

                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    strSQL &= " AND CIT.FreeFlg4 = :FreeFlg4 " & vbCrLf
                End If

                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    strSQL &= " AND CIT.FreeFlg5 = :FreeFlg5 " & vbCrLf
                End If

                'ORDER BY句セット
                strSQL &= " ORDER BY CIT.ChgNmb Desc " & vbCrLf

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

                'バインド変数に型と値をセット
                '番号
                If .PropStrChgNmb <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ChgNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("ChgNmb").Value = .PropStrChgNmb.Trim
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

                '内容
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

                '対処
                If .PropStrTaisyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTaiSyo.Count - 1
                        aryTaiSyo(i) = commonLogicHBK.ChangeStringForSearch(aryTaiSyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTaiSyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TaisyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TaisyoAimai" + i.ToString).Value = "%" & aryTaiSyo(i) & "%"
                    Next
                End If

                '開始日(FROM)
                If .PropStrStartDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kaisidtFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kaisidtFrom").Value = .PropStrStartDTFrom.Trim
                End If

                '開始日(TO)
                If .PropStrStartDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("kaisidtTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("kaisidtTo").Value = .PropStrStartDTTo.Trim
                End If

                '完了日(FROM)
                If .PropStrKanryoDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KanryoDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KanryoDTFrom").Value = .PropStrKanryoDTFrom.Trim
                End If

                '完了日(TO)
                If .PropStrKanryoDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KanryoDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KanryoDTTo").Value = .PropStrKanryoDTTo.Trim
                End If

                '登録日(FROM)
                If .PropStrRegDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTFrom").Value = .PropStrRegDTFrom.Trim
                End If

                '登録日(TO)
                If .PropStrRegDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegDTTO", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegDTTO").Value = .PropStrRegDTTo.Trim
                End If

                '最終更新日時(FROM)
                If .PropStrLastRegDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("LastRegDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("LastRegDTFrom").Value = .PropStrLastRegDTFrom.Trim
                End If

                '最終更新日時(TO)
                If .PropStrLastRegDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("LastRegDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("LastRegDTTo").Value = .PropStrLastRegDTTo.Trim
                End If

                'CYSPR
                If .PropStrCysprNmb.Trim <> "" Then
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

                '担当者グループ
                If .PropStrTantoGrpCD <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropStrTantoGrpCD
                End If

                '担当者ID
                If .PropStrTantoID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrTantoID.Trim)
                End If

                '担当者氏名 
                If .PropStrTantoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrTantoNM.Trim) & "%"
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

End Class
