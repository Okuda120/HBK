Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

Public Class SqlHBKB1204

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '[SELECT]CI部所有機器テーブル【Excel出力用】取得SQL
    Private strSelectCIBuyTable As String = "SELECT" & vbCrLf & _
                                            " cit.CINmb," & vbCrLf & _
                                            " ckm.CIKbnNM," & vbCrLf & _
                                            " km.KindNM," & vbCrLf & _
                                            " cit.Num," & vbCrLf & _
                                            " csm.CIStateNM," & vbCrLf & _
                                            " cit.Class1," & vbCrLf & _
                                            " cit.Class2," & vbCrLf & _
                                            " cit.CINM," & vbCrLf & _
                                            " grm1.GroupNM," & vbCrLf & _
                                            " cit.CINaiyo," & vbCrLf & _
                                            " cit.BIko1," & vbCrLf & _
                                            " cit.BIko2," & vbCrLf & _
                                            " cit.BIko3," & vbCrLf & _
                                            " cit.BIko4," & vbCrLf & _
                                            " cit.BIko5," & vbCrLf & _
                                            " (CASE WHEN cit.FreeFlg1 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "'" & vbCrLf & _
                                            "       WHEN cit.FreeFlg1 = '" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS FreeFlg1," & vbCrLf & _
                                            " (CASE WHEN cit.FreeFlg2 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "'" & vbCrLf & _
                                            "       WHEN cit.FreeFlg2 = '" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS FreeFlg2," & vbCrLf & _
                                            " (CASE WHEN cit.FreeFlg3 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "'" & vbCrLf & _
                                            "       WHEN cit.FreeFlg3 = '" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS FreeFlg3," & vbCrLf & _
                                            " (CASE WHEN cit.FreeFlg4 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "'" & vbCrLf & _
                                            "       WHEN cit.FreeFlg4 = '" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS FreeFlg4," & vbCrLf & _
                                            " (CASE WHEN cit.FreeFlg5 = '" & FREE_FLG_OFF & "' THEN '" & FREE_FLG_OFF_NM & "'" & vbCrLf & _
                                            "       WHEN cit.FreeFlg5 = '" & FREE_FLG_ON & "' THEN '" & FREE_FLG_ON_NM & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS FreeFlg5," & vbCrLf & _
                                            " TO_CHAR(cit.RegDT,'YYYY/MM/DD HH24:MI:SS') AS RegDT," & vbCrLf & _
                                            " grm2.GroupNM," & vbCrLf & _
                                            " cit.RegID," & vbCrLf & _
                                            " hum1.HBKUsrNM," & vbCrLf & _
                                            " TO_CHAR(cit.UpdateDT,'YYYY/MM/DD HH24:MI:SS') AS UpdateDT," & vbCrLf & _
                                            " grm3.GroupNM," & vbCrLf & _
                                            " cit.UpdateID," & vbCrLf & _
                                            " hum2.HBKUsrNM," & vbCrLf & _
                                            " cbt.Kataban," & vbCrLf & _
                                            " cbt.Aliau," & vbCrLf & _
                                            " cbt.Serial," & vbCrLf & _
                                            " cbt.MacAddress1," & vbCrLf & _
                                            " cbt.MacAddress2," & vbCrLf & _
                                            " (CASE WHEN cbt.ZooKbn = '" & ZOO_KBN_UNFIN & "' THEN '" & ZOO_NM_UNFIN & "'" & vbCrLf & _
                                            "       WHEN cbt.ZooKbn = '" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS ZooKbn," & vbCrLf & _
                                            " OSNM," & vbCrLf & _
                                            " AntiVirusSoftNM," & vbCrLf & _
                                            " kim2.KikiStateNM," & vbCrLf & _
                                            " cbt.NIC1," & vbCrLf & _
                                            " cbt.NIC2," & vbCrLf & _
                                            " (CASE WHEN cbt.ConnectDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS ConnectDT," & vbCrLf & _
                                            " (CASE WHEN cbt.ExpirationDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS ExpirationDT," & vbCrLf & _
                                            " (CASE WHEN cbt.DeletDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.DeletDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS DeletDT," & vbCrLf & _
                                            " (CASE WHEN cbt.LastInfoDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.LastInfoDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS LastInfoDT," & vbCrLf & _
                                            " cbt.ConectReason," & vbCrLf & _
                                            " (CASE WHEN cbt.ExpirationUPDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.ExpirationUPDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS ExpirationUPDT," & vbCrLf & _
                                            " (CASE WHEN cbt.InfoDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.InfoDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS InfoDT," & vbCrLf & _
                                            " (CASE WHEN cbt.NumInfoKbn = '" & NUMINFO_KBN_UNFIN & "' THEN '" & NUMINFO_NM_UNFIN & "'" & vbCrLf & _
                                            "       WHEN cbt.NumInfoKbn = '" & NUMINFO_KBN_FIN & "' THEN '" & NUMINFO_NM_FIN & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS NumInfoKbn," & vbCrLf & _
                                            " (CASE WHEN cbt.SealSendkbn = '" & SEALSEND_KBN_UNFIN & "' THEN '" & SEALSEND_NM_UNFIN & "'" & vbCrLf & _
                                            "       WHEN cbt.SealSendkbn = '" & SEALSEND_KBN_FIN & "' THEN '" & SEALSEND_NM_FIN & "'" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "       END) AS SealSendkbn," & vbCrLf & _
                                            " (CASE WHEN cbt.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_UNFIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_UNFIN & "'" & vbCrLf & _
                                            "       WHEN cbt.AntiVirusSofCheckKbn = '" & ANTIVIRUSSOFCHECK_KBN_FIN & "' THEN '" & ANTIVIRUSSOFCHECK_NM_FIN & "'" & vbCrLf & _
                                            "       ELSE '' END) AS AntiVirusSofCheckKbn," & vbCrLf & _
                                            " (CASE WHEN cbt.AntiVirusSofCheckDT <> ''" & vbCrLf & _
                                            "            THEN TO_CHAR(TO_DATE(cbt.AntiVirusSofCheckDT,'YYYYMMDD'),'YYYY/MM/DD')" & vbCrLf & _
                                            "       ELSE ''" & vbCrLf & _
                                            "  END)  AS AntiVirusSofCheckDT," & vbCrLf & _
                                            " cbt.BusyoKikiBiko," & vbCrLf & _
                                            " cbt.ManageKyokuNM," & vbCrLf & _
                                            " cbt.ManageBusyoNM," & vbCrLf & _
                                            " cbt.WorkFromNmb," & vbCrLf & _
                                            " kim1.KikiStateNM," & vbCrLf & _
                                            " cbt.FixedIP," & vbCrLf & _
                                            " cbt.UsrID," & vbCrLf & _
                                            " cbt.UsrNM," & vbCrLf & _
                                            " cbt.UsrCompany," & vbCrLf & _
                                            " cbt.UsrKyokuNM," & vbCrLf & _
                                            " cbt.UsrBusyoNM," & vbCrLf & _
                                            " cbt.UsrTel," & vbCrLf & _
                                            " cbt.UsrMailAdd," & vbCrLf & _
                                            " cbt.UsrContact," & vbCrLf & _
                                            " cbt.UsrRoom," & vbCrLf & _
                                            " cbt.SetKyokuNM," & vbCrLf & _
                                            " cbt.SetBusyoNM," & vbCrLf & _
                                            " cbt.SetRoom," & vbCrLf & _
                                            " cbt.SetBuil," & vbCrLf & _
                                            " cbt.SetFloor" & vbCrLf & _
                                            " FROM CI_INFO_TB cit" & vbCrLf & _
                                            " LEFT OUTER JOIN CI_KIND_MTB ckm ON cit.CIKbnCD = ckm.CIKbnCD" & vbCrLf & _
                                            " LEFT OUTER JOIN KIND_MTB km ON cit.KindCD = km.KindCD" & vbCrLf & _
                                            " LEFT OUTER JOIN CISTATE_MTB csm ON csm.CIStateCD = cit.CIStatusCD" & vbCrLf & _
                                            " LEFT OUTER JOIN GRP_MTB grm1 ON cit.CIOwnerCD = grm1.GroupCD" & vbCrLf & _
                                            " LEFT OUTER JOIN GRP_MTB grm2 ON cit.RegGrpCD = grm2.GroupCD" & vbCrLf & _
                                            " LEFT OUTER JOIN GRP_MTB grm3 ON cit.UpGrpCD = grm3.GroupCD" & vbCrLf & _
                                            " LEFT OUTER JOIN HBKUSR_MTB hum1 ON cit.RegID = hum1.HBKUsrID" & vbCrLf & _
                                            " LEFT OUTER JOIN HBKUSR_MTB hum2 ON cit.UpdateID = hum2.HBKUsrID" & vbCrLf & _
                                            " LEFT OUTER JOIN CI_BUY_TB cbt ON cit.CINmb = cbt.CINmb" & vbCrLf & _
                                            " LEFT OUTER JOIN KIKISTATE_MTB kim1 ON cbt.IPUseCD = kim1.KikiStateCD" & vbCrLf & _
                                            "            AND kim1.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "'" & vbCrLf & _
                                            " LEFT OUTER JOIN KIKISTATE_MTB kim2 ON cbt.DNSRegCD = kim2.KikiStateCD" & vbCrLf & _
                                            "            AND kim2.KikiStateKbn = '" & KIKISTATEKBN_DNS_REG & "'"

    ''' <summary>
    ''' CI部所有機器テーブル【Excel出力用】取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1204">[IN]部所有機器検索(Excel出力)データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器テーブル【人事連絡用】取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/10 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIBuyTableSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB1204 As DataHBKB1204) As Boolean


        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSql As String = ""

        Try

            '**********************************
            'SQL文設定
            '**********************************
            'SQL文(SELECT)
            strSql = strSelectCIBuyTable
            Dim sbSql As New StringBuilder(strSql)
            Dim aryStrFreetext As String() = Nothing 'フリーテキスト検索用配列

            '**********************************
            'SQL文の生成(コントロールの中身を判定しながら条件追加)
            '**********************************
            With dataHBKB1204

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

                '++++++++++++++++++++++++++++++++++++++++++++++
                ''ORDER BY句を指定
                'sbSql.Append(" ORDER BY cit.Sort")

                'ORDER BY句を指定
                sbSql.Append(" ORDER BY cit.Num")
                '++++++++++++++++++++++++++++++++++++++++++++++

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
    ''' <para>作成情報：2012/07/19 s.yamaguchi
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