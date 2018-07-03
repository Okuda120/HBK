Imports Npgsql
Imports Common
Imports CommonHBK

''' <summary>
''' 問題検索一覧Excel出力Sqlクラス
''' </summary>
''' <remarks>問題検索一覧Excel出力のSQLの作成・設定を行う
''' <para>作成情報：2012/08/13 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKD0102

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '[SELECT]問題検索結果
    Private strSelectProblemInfoSql As String = " SELECT " & vbCrLf & _
                                                                        " PIT.PrbNmb, " & vbCrLf & _
                                                                        " PSM.ProcessStateNM, " & vbCrLf & _
                                                                        " TO_CHAR(PIT.KaisiDT,'YYYY/MM/DD HH24:MI') AS KaisiDT, " & vbCrLf & _
                                                                        " TO_CHAR(PIT.KanryoDT,'YYYY/MM/DD HH24:MI') AS KanryoDT, " & vbCrLf & _
                                                                        " PCM.PrbCaseNM, " & vbCrLf & _
                                                                        " PIT.Title, " & vbCrLf & _
                                                                        " PIT.Naiyo, " & vbCrLf & _
                                                                        " PIT.Taisyo, " & vbCrLf & _
                                                                        " PIT.ApproverID, " & vbCrLf & _
                                                                        " PIT.ApproverNM, " & vbCrLf & _
                                                                        " PIT.RecorderID, " & vbCrLf & _
                                                                        " PIT.RecorderNM, " & vbCrLf & _
                                                                        " CIT.CINM, " & vbCrLf & _
                                                                        " PCT.CysprNmb, " & vbCrLf & _
                                                                        " HBKF0003(PIT.TantoGrpCD), " & vbCrLf & _
                                                                        " PIT.PrbTantoID, " & vbCrLf & _
                                                                        " PIT.PrbTantoNM, " & vbCrLf & _
                                                                        " TO_CHAR(PIT.RegDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                                                        " HBKF0003(PIT.RegGrpCD), " & vbCrLf & _
                                                                        " HBKF0004(PIT.RegID), " & vbCrLf & _
                                                                        " TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                                                        " HBKF0003(PIT.UpGrpCD), " & vbCrLf & _
                                                                        " HBKF0004(PIT.UpdateID), " & vbCrLf & _
                                                                        " WSM.WorkStateNM, " & vbCrLf & _
                                                                        " PWRT.WorkNaiyo, " & vbCrLf & _
                                                                        " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                                                        " CIT2.CINM, " & vbCrLf & _
                                                                        " PWTT.SagyouInfo " & vbCrLf & _
                                                                    " FROM " & vbCrLf & _
                                                                        " problem_info_tb PIT " & vbCrLf & _
                                                                    " LEFT OUTER JOIN " & vbCrLf & _
                                                                        "(SELECT * FROM problem_wk_rireki_tb WHERE (PrbNmb,WorkRirekiNmb) IN " & vbCrLf & _
                                                                            "(SELECT PWRT.PrbNmb,MIN(PWRT.WorkRirekiNmb) FROM problem_wk_rireki_tb PWRT WHERE (PWRT.PrbNmb,PWRT.WorkSceDT) IN " & vbCrLf & _
                                                                                " (SELECT PWRT2.PrbNmb,MIN(PWRT2.WorkSceDT) FROM problem_wk_rireki_tb PWRT2 WHERE PWRT2.WorkEdDT IS NULL GROUP BY PrbNmb) AND PWRT.WorkEdDT IS NULL GROUP BY PrbNmb)) PWRT ON PIT.PrbNmb = PWRT.PrbNmb " & vbCrLf & _
                                                                    " LEFT OUTER JOIN (SELECT PrbNmb, WorkRirekiNmb, STRING_AGG(WorkTantoGrpNM || '　' || WorkTantoID || '　' || WorkTantoNM,'／') AS SagyouInfo " & vbCrLf & _
                                                                        " FROM problem_wk_tanto_tb GROUP BY PrbNmb, WorkRirekiNmb) PWTT ON PWRT.PrbNmb = PWTT.PrbNmb AND PWRT.WorkRirekiNmb = PWTT.WorkRirekiNmb " & vbCrLf & _
                                                                    " LEFT OUTER JOIN ci_info_tb CIT ON CIT.CINmb = PIT.SystemNmb " & vbCrLf & _
                                                                    " LEFT OUTER JOIN ci_info_tb CIT2 ON CIT2.CINmb = PWRT.SystemNmb " & vbCrLf & _
                                                                    " LEFT OUTER JOIN (SELECT PrbNmb , STRING_AGG(CysprNmb, '／') AS CysprNmb FROM problem_cyspr_tb GROUP BY PrbNmb) PCT ON PIT.PrbNmb = PCT.PrbNmb " & vbCrLf & _
                                                                    " LEFT OUTER JOIN processstate_mtb PSM ON PIT.ProcessStateCD = PSM.ProcessStateCD AND PSM.ProcessKbn = '" & PROCESS_TYPE_QUESTION & "' " & vbCrLf & _
                                                                    " LEFT OUTER JOIN problem_case_mtb PCM ON PIT.PrbCaseCD = PCM.PrbCaseCD " & vbCrLf & _
                                                                    " LEFT OUTER JOIN workstate_mtb WSM ON PWRT.WorkStateCD = WSM.WorkStateCD " & vbCrLf


    ''' <summary>
    ''' 問題検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKD0102">[IN/OUT]問題検索一覧Excel出力Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/13 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProblemInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKD0102 As DataHBKD0102) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectProblemInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If SetSqlWhereStatementl(dataHBKD0102, Adapter, Cn, strSql) = False Then
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
    ''' 問題情報取得用SQLのWHERE句作成・設定処理
    ''' </summary>
    ''' <param name="dataHBKD0102">[IN/OUT]問題検索一覧Excel出力Dataクラス</param>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="strSQL">[IN/OUT]WHERE句をセットするSQL文</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>問題情報取得用SQLのWHERE句作成、アダプタにセットする
    ''' <para>作成情報：2012/08/13 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSqlWhereStatementl(ByVal dataHBKD0102 As DataHBKD0102, _
                                                                ByRef Adapter As NpgsqlDataAdapter, _
                                                                ByVal Cn As NpgsqlConnection, _
                                                                ByRef strSQL As String
                                                                ) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        '変数宣言
        Dim strFreeText() As String = Nothing           'フリーテキスト検索用配列
        Dim aryTitle() As String = Nothing              'タイトル
        Dim aryNaiyo() As String = Nothing              '内容
        Dim aryTaiSyo() As String = Nothing             '対処
        Dim aryCysprNmb() As String = Nothing           'CysprNmb番号

        Try
            With dataHBKD0102

                strSQL &= " WHERE " & vbCrLf & _
                                    " ( EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG WHERE " & vbCrLf & _
                                    " PKTG.RelationKbn = '" & KBN_GROUP & "' AND PKTG.RelationID  IN(" & .PropStrLoginUserGrp & ") " & vbCrLf & _
                                    " AND PKTG.PrbNmb = PIT.PrbNmb) " & vbCrLf & _
                                    " OR EXISTS (SELECT DISTINCT PKTG.PrbNmb FROM problem_kankei_tb PKTG " & vbCrLf & _
                                    " WHERE PKTG.RelationKbn = '" & KBN_USER & "' AND " & vbCrLf & _
                                    " PKTG.RelationID = '" & .PropStrLoginUserId & "' AND PKTG.PrbNmb = PIT.PrbNmb) ) " & vbCrLf

                '番号
                If .PropStrPrbNmb <> "" Then
                    strSQL &= " AND PIT.PrbNmb = :PrbNmb " & vbCrLf
                End If

                'ステータス（リストボックスで選択されている項目分ループし、カンマ区切りの文字列を生成
                If .PropStrProcessState <> "" Then
                    strSQL &= "AND PIT.ProcessStateCD IN (" & .PropStrProcessState & ") " & vbCrLf
                End If

                '対象システム（リストボックスで選択されている項目分ループし、カンマ区切りの文字列を生成
                If .PropStrTargetSys <> "" Then
                    strSQL &= " AND PIT.SystemNmb IN  (" & .PropStrTargetSys & ") " & vbCrLf
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
                            strSQL &= " PIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
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
                            strSQL &= " PIT.NaiyoAimai LIKE :NaiyoAimai" + intCnt.ToString()
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
                            strSQL &= " PIT.TaisyoAimai LIKE :TaisyoAimai" + intCnt.ToString()
                            If intCnt <> aryTaiSyo.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '開始日(FROM)
                If .PropStrStartDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.KaisiDT,'YYYY/MM/DD') >= :KaisiDTFrom " & vbCrLf
                End If

                '開始日(TO)
                If .PropStrStartDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.KaisiDT,'YYYY/MM/DD') <= :KaisiDTTo " & vbCrLf
                End If

                '完了日(FROM)
                If .PropStrKanryoDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.KanryoDT,'YYYY/MM/DD') >= :KanryoDTFrom " & vbCrLf
                End If

                '完了日(TO)
                If .PropStrKanryoDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.KanryoDT,'YYYY/MM/DD') <= :KanryoDTTo " & vbCrLf
                End If

                '登録日(FROM)
                If .PropStrRegDTFrom.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.RegDT,'YYYY/MM/DD') >= :RegDTFrom " & vbCrLf
                End If

                '登録日(TO)
                If .PropStrRegDTTo.Trim <> "" Then
                    strSQL &= " AND TO_CHAR(PIT.RegDT,'YYYY/MM/DD') <= :RegDTTo " & vbCrLf
                End If

                '[Mod]2014/11/19 e.okamura 問題要望114 Start
                ''最終更新日時(FROM)
                'If .PropStrLastRegDTFrom.Trim <> "" Then
                '    strSQL &= " AND TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= TO_CHAR(TO_TIMESTAMP(:LastRegDTFrom,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                'End If
                '
                ''最終更新日時(TO)
                'If .PropStrLastRegDTTo.Trim <> "" Then
                '    strSQL &= " AND TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= TO_CHAR(TO_TIMESTAMP(:LastRegDTTo,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                'End If

                '最終更新日時(FROM)
                If .PropStrLastRegDTFrom.Trim <> "" Then
                    If .PropStrLastRegTimeFrom.Trim <> "" Then
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= :LastRegDTFrom" & vbCrLf
                    Else
                        '時間表記なし
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD') >= :LastRegDTFrom" & vbCrLf
                    End If
                End If

                '最終更新日時(TO)
                If .PropStrLastRegDTTo.Trim <> "" Then
                    If .PropStrLastRegTimeTo.Trim <> "" Then
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= :LastRegDTTo" & vbCrLf
                    Else
                        '時間表記なし
                        strSQL &= " AND"
                        strSQL &= " TO_CHAR(PIT.UpdateDT,'YYYY/MM/DD') <= :LastRegDTTo" & vbCrLf
                    End If
                End If
                '[Mod]2014/11/19 e.okamura 問題要望114 End

                '発生原因
                If .PropStrPrbCase <> "" Then
                    strSQL &= " AND PIT.PrbCaseCD = :PrbCaseCD " & vbCrLf
                End If

                'CYSPR
                If .PropStrCysprNmb.Trim <> "" Then
                    '検索文字列の分割
                    aryCysprNmb = commonLogicHBK.GetSearchStringList(.PropStrCysprNmb, SPLIT_MODE_OR)
                    '分割分だけ検索条件の設定
                    If aryCysprNmb.Length <> 0 Then
                        strSQL &= " AND PIT.PrbNmb IN (SELECT CYT.PrbNmb FROM problem_cyspr_tb CYT WHERE " & vbCrLf
                        For intCnt = 0 To aryCysprNmb.Count - 1
                            strSQL &= " CYT.CysprNmbAimai = :CysprNmbAimai" + intCnt.ToString()
                            If intCnt <> aryCysprNmb.Count - 1 Then
                                strSQL &= " OR "
                            End If
                        Next
                        strSQL &= ") " & vbCrLf
                    End If
                End If

                '担当者情報
                If .PropStrTantoRdoCheck = D0101_RDO_CHOKUSETSU Then
                    '直接選択時

                    '担当者グループ
                    If .PropStrTantoGrpCD <> "" Then
                        strSQL &= " AND PIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                    End If
                    '担当者ID
                    If .PropStrTantoID.Trim <> "" Then
                        strSQL &= " AND PIT.PrbTantIDAimai = :TantIDAimai " & vbCrLf
                    End If
                    '担当者氏名 
                    If .PropStrTantoNM.Trim <> "" Then
                        strSQL &= " AND PIT.PrbTantNMAimai LIKE :TantNMAimai " & vbCrLf
                    End If

                ElseIf .PropStrTantoRdoCheck = D0101_RDO_KANYO Then
                    '関与選択時

                    '担当者グループ、担当者ID、担当者氏名が入力されているかチェック
                    If .PropStrTantoGrpCD <> "" Or .PropStrTantoID.Trim <> "" Or .PropStrTantoNM.Trim <> "" Then
                        strSQL &= " AND EXISTS (SELECT DISTINCT PWTT.PrbNmb " & vbCrLf
                        strSQL &= " FROM problem_wk_tanto_tb PWTT " & vbCrLf
                        strSQL &= " LEFT OUTER JOIN hbkusr_mtb HUM ON HUM.HBKUsrID = PWTT.WorkTantoID " & vbCrLf
                        strSQL &= " WHERE " & vbCrLf

                        '担当者グループ
                        If .PropStrTantoGrpCD <> "" Then
                            strSQL &= " PWTT.WorkTantoGrpCD = :TantoGrpCD " & vbCrLf
                        End If
                        '担当者ID
                        If .PropStrTantoID.Trim <> "" Then
                            If .PropStrTantoGrpCD <> "" Then
                                strSQL &= " AND " & vbCrLf
                            End If
                            strSQL &= " PWTT.WorkTantoID = :TantIDAimai " & vbCrLf
                        End If
                        '担当者氏名 
                        If .PropStrTantoNM.Trim <> "" Then
                            If .PropStrTantoGrpCD <> "" Or .PropStrTantoID.Trim <> "" Then
                                strSQL &= " AND " & vbCrLf
                            End If
                            strSQL &= " HUM.HBKUsrNMAimai LIKE :TantNMAimai " & vbCrLf
                        End If

                        strSQL &= " AND PWTT.PrbNmb = PIT.PrbNmb ) " & vbCrLf
                    End If
                End If

                '作業予定日時(From)、(To)か対象システムに入力があった場合
                If .PropStrWorkSceDTFrom.Trim <> "" Or .PropStrWorkSceDTTo.Trim <> "" Or .PropStrSystemNmb.Trim <> "0" Then

                    strSQL &= " AND EXISTS (SELECT DISTINCT PWRT.PrbNmb " & vbCrLf
                    strSQL &= " FROM problem_wk_rireki_tb PWRT " & vbCrLf
                    strSQL &= " WHERE " & vbCrLf

                    '[Mod]2014/11/19 e.okamura 問題要望114 Start
                    ''作業予定日時(From)
                    'If .PropStrWorkSceDTFrom.Trim <> "" Then
                    '    strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD HH24:MI') >= TO_CHAR(TO_TIMESTAMP(:WorkSceDTFrom,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                    'End If
                    ''作業予定日時(To)
                    'If .PropStrWorkSceDTTo.Trim <> "" Then
                    '    If .PropStrWorkSceDTFrom.Trim <> "" Then
                    '        strSQL &= " AND " & vbCrLf
                    '    End If
                    '    strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD HH24:MI') <= TO_CHAR(TO_TIMESTAMP(:WorkSceDTTo,'YYYY/MM/DD HH24:MI'),'YYYY/MM/DD HH24:MI') " & vbCrLf
                    'End If

                    '作業予定日時(From)
                    If .PropStrWorkSceDTFrom.Trim <> "" Then
                        If .PropStrWorkSceTimeFrom.Trim <> "" Then
                            strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD HH24:MI') >= :WorkSceDTFrom " & vbCrLf
                        Else
                            '時間表記なし
                            strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD') >= :WorkSceDTFrom " & vbCrLf
                        End If
                    End If
                    '作業予定日時(To)
                    If .PropStrWorkSceDTTo.Trim <> "" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Then
                            strSQL &= " AND " & vbCr
                        End If
                        If .PropStrWorkSceTimeTo.Trim <> "" Then
                            strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD HH24:MI') <= :WorkSceDTTo " & vbCrLf
                        Else
                            '時間表記なし
                            strSQL &= " TO_CHAR(PWRT.WorkSceDT,'YYYY/MM/DD') <= :WorkSceDTTo " & vbCrLf
                        End If
                    End If
                    '[Mod]2014/11/19 e.okamura 問題要望114 End

                    '対象システム
                    If .PropStrSystemNmb <> "0" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Or _
                           .PropStrWorkSceDTTo.Trim <> "" Then
                            strSQL &= " AND " & vbCrLf
                        End If
                        strSQL &= " PWRT.SystemNmb = :SystemNmb " & vbCrLf
                    End If
                    strSQL &= " AND PWRT.PrbNmb = PIT.PrbNmb ) " & vbCrLf
                End If

                'プロセスリンク
                If .PropStrKindCD <> "" Then
                    strSQL &= " AND PIT.PrbNmb IN ( " & .PropStrKindCD & " )" & vbCrLf
                End If

                'フリーテキスト検索(あいまい検索)
                If .PropStrBiko.Trim <> "" Then
                    ' 検索文字列の分割
                    strFreeText = CommonHBK.CommonLogicHBK.GetSearchStringList(.PropStrBiko, SPLIT_MODE_AND)

                    If strFreeText.Length <> 0 Then
                        strSQL &= " AND "
                        strSQL &= " ("
                        For intCnt = 0 To strFreeText.Count - 1
                            strSQL &= " PIT.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> strFreeText.Count - 1 Then
                                strSQL &= " AND "
                            End If
                        Next
                        strSQL &= ") "
                    End If
                End If

                'フリーフラグ1
                If .PropStrFreeFlg1 <> "" Then
                    strSQL &= " AND PIT.FreeFlg1 = :FreeFlg1 " & vbCrLf
                End If

                'フリーフラグ2
                If .PropStrFreeFlg2 <> "" Then
                    strSQL &= " AND PIT.FreeFlg2 = :FreeFlg2 " & vbCrLf
                End If

                'フリーフラグ3
                If .PropStrFreeFlg3 <> "" Then
                    strSQL &= " AND PIT.FreeFlg3 = :FreeFlg3 " & vbCrLf
                End If

                'フリーフラグ4
                If .PropStrFreeFlg4 <> "" Then
                    strSQL &= " AND PIT.FreeFlg4 = :FreeFlg4 " & vbCrLf
                End If

                'フリーフラグ5
                If .PropStrFreeFlg5 <> "" Then
                    strSQL &= " AND PIT.FreeFlg5 = :FreeFlg5 " & vbCrLf
                End If

                'ORDER BY句セット
                strSQL &= " ORDER BY PIT.PrbNmb " & vbCrLf


                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

                'バインド変数に型と値をセット
                '番号
                If .PropStrPrbNmb <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PrbNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("PrbNmb").Value = .PropStrPrbNmb.Trim
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
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KaisiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KaisiDTFrom").Value = .PropStrStartDTFrom.Trim
                End If

                '開始日(TO)
                If .PropStrStartDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KaisiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KaisiDTTo").Value = .PropStrStartDTTo.Trim
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

                '発生原因
                If .PropStrPrbCase <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PrbCaseCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("PrbCaseCD").Value = .PropStrPrbCase
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
                        Adapter.SelectCommand.Parameters("CysprNmbAimai" + i.ToString).Value = aryCysprNmb(i)
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

                '作業予定日時(From)
                If .PropStrWorkSceDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTFrom").Value = .PropStrWorkSceDTFrom.Trim
                End If

                '作業予定日時(To)
                If .PropStrWorkSceDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTTo").Value = .PropStrWorkSceDTTo.Trim
                End If

                '対象システム
                If .PropStrSystemNmb <> "0" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("SystemNmb").Value = Integer.Parse(.PropStrSystemNmb)
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
