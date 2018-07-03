Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' インシデント検索一覧Excel出力Sqlクラス
''' </summary>
''' <remarks>インシデント検索一覧Excel出力のSQLの作成・設定を行う
''' <para>作成情報：2012/08/03 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0102

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '[SELECT]インシデント検索結果
    'Private strSelectIncidentInfoSql As String = " SELECT " & vbCrLf & _
    '                                             " IIT.IncNmb, " & vbCrLf & _
    '                                             " UWM.UketsukeWayNM, " & vbCrLf & _
    '                                             " IKM.IncKindNM," & vbCrLf & _
    '                                             " PSM.ProcessStateNM, " & vbCrLf & _
    '                                             " TO_CHAR(IIT.HasseiDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " TO_CHAR(IIT.KaitoDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " TO_CHAR(IIT.KanryoDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " IIT.Priority, " & vbCrLf & _
    '                                             " IIT.Errlevel, " & vbCrLf & _
    '                                             " IIT.Title, " & vbCrLf & _
    '                                             " IIT.UkeNaiyo, " & vbCrLf & _
    '                                             " IIT.TaioKekka, " & vbCrLf & _
    '                                             " CIT.CINM, " & vbCrLf & _
    '                                             " IIT.OutSideToolNmb, " & vbCrLf & _
    '                                             " HBKF0003(IIT.TantoGrpCD), " & vbCrLf & _
    '                                             " IIT.IncTantoID, " & vbCrLf & _
    '                                             " IIT.IncTantoNM, " & vbCrLf & _
    '                                             " DMM.DomainNM, " & vbCrLf & _
    '                                             " IIT.PartnerCompany, " & vbCrLf & _
    '                                             " IIT.PartnerID, " & vbCrLf & _
    '                                             " IIT.PartnerNM, " & vbCrLf & _
    '                                             " IIT.PartnerKana, " & vbCrLf & _
    '                                             " IIT.PartnerKyokuNM, " & vbCrLf & _
    '                                             " IIT.UsrBusyoNM, " & vbCrLf & _
    '                                             " IIT.PartnerTel, " & vbCrLf & _
    '                                             " IIT.PartnerMailAdd, " & vbCrLf & _
    '                                             " IIT.PartnerContact, " & vbCrLf & _
    '                                             " IIT.PartnerBase, " & vbCrLf & _
    '                                             " IIT.PartnerRoom, " & vbCrLf & _
    '                                             " IIT.ShijisyoFlg, " & vbCrLf & _
    '                                             " TO_CHAR(IIT.RegDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " KKM.KeikaKindNM, " & vbCrLf & _
    '                                             " IWRT.WorkNaiyo, " & vbCrLf & _
    '                                             " TO_CHAR(IWRT.WorkSceDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " TO_CHAR(IWRT.WorkStDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " TO_CHAR(IWRT.WorkEdDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
    '                                             " CIT2.CINM, " & vbCrLf & _
    '                                             " IWTT.SagyouInfo, " & vbCrLf & _
    '                                             " IKT.KikiInf " & vbCrLf & _
    '                                             " FROM incident_info_tb IIT " & vbCrLf & _
    '                                             " LEFT OUTER JOIN " & vbCrLf & _
    '                                             " (SELECT * " & vbCrLf & _
    '                                             " FROM incident_wk_rireki_tb WHERE (IncNmb,WorkRirekiNmb) IN " & vbCrLf & _
    '                                             " (SELECT IWRT.IncNmb, " & vbCrLf & _
    '                                             " MIN(IWRT.WorkRirekiNmb) " & vbCrLf & _
    '                                             " FROM incident_wk_rireki_tb IWRT WHERE (IWRT.IncNmb,IWRT.WorkSceDT) IN (SELECT IWRT2.IncNmb,MIN(IWRT2.WorkSceDT) AS " & vbCrLf & _
    '                                             " WorkSceDT FROM incident_wk_rireki_tb IWRT2 GROUP BY IWRT2.IncNmb )GROUP BY IWRT.IncNmb)) IWRT ON IWRT.IncNmb = IIT.IncNmb " & vbCrLf & _
    '                                             " LEFT OUTER JOIN (SELECT IWTT.IncNmb, IWTT.WorkRirekiNmb,STRING_AGG(IWTT.WorkTantoGrpNM || '　' || " & vbCrLf & _
    '                                             " IWTT.WorkTantoID || '　' || IWTT.WorkTantoNM, '／') AS SagyouInfo " & vbCrLf & _
    '                                             " FROM incident_wk_tanto_tb IWTT GROUP BY IWTT.IncNmb, IWTT.WorkRirekiNmb) IWTT " & vbCrLf & _
    '                                             " ON IWRT.IncNmb = IWTT.IncNmb AND IWRT.WorkRirekiNmb = IWTT.WorkRirekiNmb " & vbCrLf & _
    '                                             " LEFT OUTER JOIN (SELECT STRING_AGG(KKM2.KindNM || IKT.Num || IKT.KikiInf, '／') AS KikiInf ,IKT.IncNmb " & vbCrLf & _
    '                                             " FROM incident_kiki_tb IKT LEFT OUTER JOIN kind_mtb KKM2 ON KKM2.KindCD = IKT.KindCD " & vbCrLf & _
    '                                             " GROUP BY IKT.IncNmb ) IKT ON  IKT.IncNmb = IIT.IncNmb " & vbCrLf & _
    '                                             "  LEFT OUTER JOIN (SELECT IncNmb,HBKF0010(IncNmb, '" & PROCESS_TYPE_INCIDENT & "') AS SortDT FROM incident_info_tb) SortDT ON IIT.IncNmb = SortDT.IncNmb " & vbCrLf & _
    '                                             " LEFT OUTER JOIN ci_info_tb CIT ON CIT.CINmb = IIT.SystemNmb AND CIT.CIKbnCD = '" & CI_TYPE_SYSTEM & "' " & vbCrLf & _
    '                                             " LEFT OUTER JOIN ci_info_tb CIT2 ON CIT2.CINmb = IWRT.SystemNmb AND CIT2.CIKbnCD = '" & CI_TYPE_SYSTEM & "' " & vbCrLf & _
    '                                             " LEFT OUTER JOIN uketsukeway_mtb UWM ON UWM.UketsukeWayCD = IIT.UkeKbnCD " & vbCrLf & _
    '                                             " LEFT OUTER JOIN incident_kind_mtb IKM ON IKM.IncKindCD = IIT.IncKbnCD " & vbCrLf & _
    '                                             " LEFT OUTER JOIN processstate_mtb PSM ON PSM.ProcessStateCD = IIT.ProcessStateCD " & vbCrLf & _
    '                                             " LEFT OUTER JOIN domain_mtb DMM ON DMM.DomainCD = IIT.DomainCD " & vbCrLf & _
    '                                             " LEFT OUTER JOIN keika_kind_mtb KKM ON KKM.KeikaKindCD = IWRT.KeikaKbnCD " & vbCrLf

    Private strSelectIncidentInfoSql As String = " SELECT " & vbCrLf & _
                                             " IIT.IncNmb, " & vbCrLf & _
                                             " UWM.UketsukeWayNM, " & vbCrLf & _
                                             " IKM.IncKindNM," & vbCrLf & _
                                             " PSM.ProcessStateNM, " & vbCrLf & _
                                             " TO_CHAR(IIT.HasseiDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " TO_CHAR(IIT.KaitoDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " TO_CHAR(IIT.KanryoDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " IIT.Priority, " & vbCrLf & _
                                             " IIT.Errlevel, " & vbCrLf & _
                                             " IIT.Title, " & vbCrLf & _
                                             " IIT.UkeNaiyo, " & vbCrLf & _
                                             " IIT.TaioKekka, " & vbCrLf & _
                                             " CIT.CINM, " & vbCrLf & _
                                             " IIT.OutSideToolNmb, " & vbCrLf & _
                                             " HBKF0003(IIT.TantoGrpCD), " & vbCrLf & _
                                             " IIT.IncTantoID, " & vbCrLf & _
                                             " IIT.IncTantoNM, " & vbCrLf & _
                                             " DMM.DomainNM, " & vbCrLf & _
                                             " IIT.PartnerCompany, " & vbCrLf & _
                                             " IIT.PartnerID, " & vbCrLf & _
                                             " IIT.PartnerNM, " & vbCrLf & _
                                             " IIT.PartnerKana, " & vbCrLf & _
                                             " IIT.PartnerKyokuNM, " & vbCrLf & _
                                             " IIT.UsrBusyoNM, " & vbCrLf & _
                                             " IIT.PartnerTel, " & vbCrLf & _
                                             " IIT.PartnerMailAdd, " & vbCrLf & _
                                             " IIT.PartnerContact, " & vbCrLf & _
                                             " IIT.PartnerBase, " & vbCrLf & _
                                             " IIT.PartnerRoom, " & vbCrLf & _
                                             " IIT.ShijisyoFlg, " & vbCrLf & _
                                             " TO_CHAR(IIT.RegDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " KKM.KeikaKindNM, " & vbCrLf & _
                                             " IWRT.WorkNaiyo, " & vbCrLf & _
                                             " TO_CHAR(IWRT.WorkSceDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " TO_CHAR(IWRT.WorkStDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " TO_CHAR(IWRT.WorkEdDT,'YYYY/MM/DD HH24:MI'), " & vbCrLf & _
                                             " CIT2.CINM, " & vbCrLf & _
                                             " ( SELECT STRING_AGG(IWTT.WorkTantoGrpNM || '　' ||  IWTT.WorkTantoID || '　' || IWTT.WorkTantoNM, '／') AS SagyouInfo " & vbCrLf & _
                                             " FROM incident_wk_tanto_tb IWTT " & vbCrLf & _
                                             " WHERE IWRT.IncNmb = IWTT.IncNmb AND IWRT.WorkRirekiNmb = IWTT.WorkRirekiNmb " & vbCrLf & _
                                             " GROUP BY IWTT.IncNmb, IWTT.WorkRirekiNmb) AS SagyouInfo , " & vbCrLf & _
                                             " (SELECT STRING_AGG(KKM2.KindNM || IKT.Num || IKT.KikiInf, '／') AS KikiInf " & vbCrLf & _
                                             " FROM incident_kiki_tb IKT " & vbCrLf & _
                                             " LEFT OUTER JOIN kind_mtb KKM2 ON KKM2.KindCD = IKT.KindCD " & vbCrLf & _
                                             " WHERE IKT.IncNmb = IIT.IncNmb GROUP BY IKT.IncNmb ) AS KikiInf " & vbCrLf & _
                                             " FROM incident_info_tb IIT " & vbCrLf & _
                                             " LEFT OUTER JOIN " & vbCrLf & _
                                             " (SELECT * " & vbCrLf & _
                                             " FROM incident_wk_rireki_tb WHERE (IncNmb,WorkRirekiNmb) IN " & vbCrLf & _
                                             " (SELECT IWRT.IncNmb, " & vbCrLf & _
                                             " MIN(IWRT.WorkRirekiNmb) " & vbCrLf & _
                                             " FROM incident_wk_rireki_tb IWRT WHERE (IWRT.IncNmb,IWRT.WorkSceDT) IN (SELECT IWRT2.IncNmb,MIN(IWRT2.WorkSceDT) AS " & vbCrLf & _
                                             " WorkSceDT FROM incident_wk_rireki_tb IWRT2 GROUP BY IWRT2.IncNmb )GROUP BY IWRT.IncNmb)) IWRT ON IWRT.IncNmb = IIT.IncNmb " & vbCrLf & _
                                             "  LEFT OUTER JOIN (SELECT IncNmb,HBKF0010(IncNmb, '" & PROCESS_TYPE_INCIDENT & "') AS SortDT FROM incident_info_tb) SortDT ON IIT.IncNmb = SortDT.IncNmb " & vbCrLf & _
                                             " LEFT OUTER JOIN ci_info_tb CIT ON CIT.CINmb = IIT.SystemNmb AND CIT.CIKbnCD = '" & CI_TYPE_SYSTEM & "' " & vbCrLf & _
                                             " LEFT OUTER JOIN ci_info_tb CIT2 ON CIT2.CINmb = IWRT.SystemNmb AND CIT2.CIKbnCD = '" & CI_TYPE_SYSTEM & "' " & vbCrLf & _
                                             " LEFT OUTER JOIN uketsukeway_mtb UWM ON UWM.UketsukeWayCD = IIT.UkeKbnCD " & vbCrLf & _
                                             " LEFT OUTER JOIN incident_kind_mtb IKM ON IKM.IncKindCD = IIT.IncKbnCD " & vbCrLf & _
                                             " LEFT OUTER JOIN processstate_mtb PSM ON PSM.ProcessStateCD = IIT.ProcessStateCD " & vbCrLf & _
                                             " LEFT OUTER JOIN domain_mtb DMM ON DMM.DomainCD = IIT.DomainCD " & vbCrLf & _
                                             " LEFT OUTER JOIN keika_kind_mtb KKM ON KKM.KeikaKindCD = IWRT.KeikaKbnCD " & vbCrLf


    '[SELECT]インシデント機器情報取得SQL(暫定版)SELECT ki.IncNmb FROM incident_kiki_tb ki WHERE ki.KindCD = :KindCD AND ki.Num = :Num Group by ki.IncNmb
    Private strSelectIncidentKikiSql As String = "SELECT" & vbCrLf & _
                                                 " ikit.IncNmb" & vbCrLf & _
                                                 " FROM incident_kiki_tb AS ikit" & vbCrLf

    ''' <summary>
    ''' インシデント検索結果取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0102">[IN]インシデント検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント検索結果取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0102 As DataHBKC0102) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectIncidentInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索条件作成
            If CreateIncidentInfoSql(Adapter, Cn, dataHBKC0102, strSql) = False Then
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
    ''' <param name="dataHBKC0102">[IN]インシデント検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateIncidentInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0102 As DataHBKC0102, _
                                           ByVal strSql As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSelect As String = ""                    'SELECT文
        Dim strSearch As String = ""                    '検索条件
        Dim strKikiSearch As String = ""                'インシデント機器情報取得SQl
        Dim aryOutsideToolNum() As String = Nothing     '外部ツール番号
        Dim aryTitle() As String = Nothing              'タイトル
        Dim aryUkeNaiyo() As String = Nothing           '受付内容
        Dim aryTaioKekka() As String = Nothing          '対応結果
        Dim arySagyou() As String = Nothing          '作業内容
        Dim aryFreeText() As String = Nothing           'フリーテキスト

        Try

            With dataHBKC0102

                strSelect = strSql

                '***************************************************************************************************************
                'サンプル
                'WHERE it.IncNmb in (SELECT ki.IncNmb FROM incident_kiki_tb ki WHERE ki.KindCD = :KindCD AND ki.Num = :Num Group by ki.IncNmb)
                '***************************************************************************************************************

                '前提条件
                strSearch &= " WHERE IIT.IncNmb IN (" & vbCrLf & _
                             "                      SELECT ikant.IncNmb FROM incident_kankei_tb AS ikant" & vbCrLf & _
                             "                      WHERE (ikant.RelationKbn = '" & KBN_GROUP & "' AND ikant.RelationID IN(" & .PropStrLoginUserGrp & "))" & vbCrLf & _
                             "                      OR (ikant.RelationKbn = '" & KBN_USER & "' AND ikant.RelationID = '" & .PropStrLoginUserId & "')" & vbCrLf & _
                             "                      GROUP BY ikant.IncNmb" & vbCrLf & _
                             "                     )"

                'インシデント番号(完全一致)
                If .PropBlnIncNumInputFlg = False Then
                    strSearch &= " AND IIT.IncNmb = :IncNmb" & vbCrLf
                End If
                '[ADD] 2012/10/24 s.yamaguchi START
                'インシデント基本情報：受付手段(完全一致)
                If .PropStrUketsukeWay.Trim <> "" Then
                    strSearch &= " AND IIT.UkeKbnCD = :UkeKbnCD" & vbCrLf
                End If
                '[ADD] 2012/10/24 s.yamaguchi END
                'インシデント基本情報：インシデント種別(完全一致)
                If .PropStrIncidentKind.Trim <> "" Then
                    strSearch &= " AND IIT.IncKbnCD = :IncKbnCD" & vbCrLf
                End If
                'インシデント基本情報：ドメイン(完全一致)
                If .PropStrDomain.Trim <> "" Then
                    strSearch &= " AND IIT.DomainCD = :DomainCD" & vbCrLf
                End If
                'インシデント基本情報：外部ツール番号(完全一致)
                If .PropStrOutsideToolNum.Trim <> "" Then
                    '検索文字列の分割
                    aryOutsideToolNum = commonLogicHBK.GetSearchStringList(.PropStrOutsideToolNum, SPLIT_MODE_OR)
                    '分割分だけ検索条件の設定
                    If aryOutsideToolNum.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryOutsideToolNum.Count - 1
                            strSearch &= " IIT.OutSideToolNmb = :OutSideToolNmb" + intCnt.ToString()
                            If intCnt <> aryOutsideToolNum.Count - 1 Then
                                strSearch &= " OR "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：ステータス(完全一致)
                If .PropStrStatus <> Nothing Then
                    strSearch &= " AND IIT.ProcessStateCD IN (" & .PropStrStatus & ")" & vbCrLf
                End If
                'インシデント基本情報：対象システム(完全一致)
                If .PropStrTargetSystem <> Nothing Then
                    strSearch &= " AND cit.CINmb IN (" & .PropStrTargetSystem & ")" & vbCrLf
                End If
                'インシデント基本情報：タイトル(あいまい検索)
                If .PropStrTitle.Trim <> "" Then
                    '検索文字列の分割
                    aryTitle = commonLogicHBK.GetSearchStringList(.PropStrTitle, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTitle.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTitle.Count - 1
                            strSearch &= " IIT.TitleAimai LIKE :TitleAimai" + intCnt.ToString()
                            If intCnt <> aryTitle.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrUkeNaiyo.Trim <> "" Then
                    '検索文字列の分割
                    aryUkeNaiyo = commonLogicHBK.GetSearchStringList(.PropStrUkeNaiyo, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryUkeNaiyo.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryUkeNaiyo.Count - 1
                            strSearch &= " IIT.UkeNaiyoAimai LIKE :UkeNaiyoAimai" + intCnt.ToString()
                            If intCnt <> aryUkeNaiyo.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：対応結果(あいまい検索)
                If .PropStrTaioKekka.Trim <> "" Then
                    '検索文字列の分割
                    aryTaioKekka = commonLogicHBK.GetSearchStringList(.PropStrTaioKekka, SPLIT_MODE_AND)
                    '分割分だけ検索条件の設定
                    If aryTaioKekka.Length <> 0 Then
                        strSearch &= " AND "
                        strSearch &= " ("
                        For intCnt = 0 To aryTaioKekka.Count - 1
                            strSearch &= " IIT.TaioKekkaAimai LIKE :TaioKekkaAimai" + intCnt.ToString()
                            If intCnt <> aryTaioKekka.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：発生日(From)
                If .PropStrHasseiDTFrom.Trim <> "" Then
                    strSearch &= " AND"
                    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                    'strSearch &= " IIT.HasseiDT >= TO_DATE(:HasseiDTFrom,'YYYY/MM/DD') " & vbCrLf
                    strSearch &= " TO_CHAR(IIT.HasseiDT,'YYYY/MM/DD') >= :HasseiDTFrom" & vbCrLf
                    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                End If
                'インシデント基本情報：発生日(To)
                If .PropStrHasseiDTTo.Trim <> "" Then
                    strSearch &= " AND"
                    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                    'strSearch &= " IIT.HasseiDT <= TO_DATE(:HasseiDTTo,'YYYY/MM/DD') " & vbCrLf
                    strSearch &= " TO_CHAR(IIT.HasseiDT,'YYYY/MM/DD') <= :HasseiDTTo" & vbCrLf
                    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                End If
                '[Mod]2014/11/19 e.okamura 問題要望114 Start
                ''インシデント基本情報：最終更新日時(From)
                'If .PropStrUpdateDTFrom.Trim <> "" Then
                '    strSearch &= " AND"
                '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                '    'strSearch &= " IIT.UpdateDT >= TO_DATE(:UpdateDTFrom,'YYYY/MM/DD HH24:MI') " & vbCrLf
                '    strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= :UpdateDTFrom " & vbCrLf
                '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                'End If
                ''インシデント基本情報：最終更新日時(To)
                'If .PropStrUpdateDTTo.Trim <> "" Then
                '    strSearch &= " AND"
                '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                '    'strSearch &= " IIT.UpdateDT <= TO_DATE(:UpdateDTTo,'YYYY/MM/DD HH24:MI') " & vbCrLf
                '    strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= :UpdateDTTo " & vbCrLf
                '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                'End If

                'インシデント基本情報：最終更新日時(From)
                If .PropStrUpdateDTFrom.Trim <> "" Then
                    If .PropStrExUpdateTimeFrom.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD HH24:MI') >= :UpdateDTFrom" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD') >= :UpdateDTFrom" & vbCrLf
                    End If
                End If
                'インシデント基本情報：最終更新日時(To)
                If .PropStrUpdateDTTo.Trim <> "" Then
                    If .PropStrExUpdateTimeTo.Trim <> "" Then
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD HH24:MI') <= :UpdateDTTo" & vbCrLf
                    Else
                        '時間表記なし
                        strSearch &= " AND"
                        strSearch &= " TO_CHAR(IIT.UpdateDT,'YYYY/MM/DD') <= :UpdateDTTo" & vbCrLf
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
                            strSearch &= " IIT.BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> aryFreeText.Count - 1 Then
                                strSearch &= " AND "
                            End If
                        Next
                        strSearch &= ") " & vbCrLf
                    End If
                End If
                'インシデント基本情報：フリーフラグ1(完全一致)
                If .PropStrFreeFlg1.Trim <> "" Then
                    strSearch &= " AND IIT.FreeFlg1 = :FreeFlg1" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ2(完全一致)
                If .PropStrFreeFlg2.Trim <> "" Then
                    strSearch &= " AND IIT.FreeFlg2 = :FreeFlg2" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ3(完全一致)
                If .PropStrFreeFlg3.Trim <> "" Then
                    strSearch &= " AND IIT.FreeFlg3 = :FreeFlg3" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ4(完全一致)
                If .PropStrFreeFlg4.Trim <> "" Then
                    strSearch &= " AND IIT.FreeFlg4 = :FreeFlg4" & vbCrLf
                End If
                'インシデント基本情報：フリーフラグ5(完全一致)
                If .PropStrFreeFlg5.Trim <> "" Then
                    strSearch &= " AND IIT.FreeFlg5 = :FreeFlg5" & vbCrLf
                End If
                '相手情報：相手ID(完全一致)
                If .PropStrPartnerID.Trim <> "" Then
                    strSearch &= " AND IIT.PartnerIDAimai = :PartnerIDAimai" & vbCrLf
                End If
                '相手情報：相手氏名(あいまい)
                If .PropStrPartnerNM.Trim <> "" Then
                    strSearch &= " AND IIT.PartnerNMAimai LIKE :PartnerNMAimai" & vbCrLf
                End If
                '相手情報：相手部署(あいまい)
                If .PropStrUsrBusyoNM.Trim <> "" Then
                    strSearch &= " AND IIT.UsrBusyoNMAimai LIKE :UsrBusyoNMAimai" & vbCrLf
                End If
                'イベント情報：イベントID(あいまい)
                If .PropStrEventID.Trim <> "" Then
                    strSearch &= " AND IIT.EventIDAimai LIKE :EventIDAimai" & vbCrLf
                End If
                'イベント情報：OPCイベントID(あいまい)
                If .PropStrOPCEventID.Trim <> "" Then
                    strSearch &= " AND IIT.OPCEventIDAimai LIKE :OPCEventIDAimai" & vbCrLf
                End If
                'イベント情報：ソース(あいまい)
                If .PropStrSource.Trim <> "" Then
                    strSearch &= " AND IIT.SourceAimai LIKE :SourceAimai" & vbCrLf
                End If
                'イベント情報：イベントクラス(あいまい)
                If .PropStrEventClass.Trim <> "" Then
                    strSearch &= " AND IIT.EventClassAimai LIKE :EventClassAimai" & vbCrLf
                End If
                '*******************************************************************************
                '担当者グループ,担当者ID,担当者氏名



                '担当者情報
                If .PropStrTantoRdoCheck = C0102_RDO_CHOKUSETSU Then
                    '直接選択時

                    '担当者グループ
                    If .PropStrTantoGrp.Trim <> "" Then
                        strSearch &= " AND IIT.TantoGrpCD = :TantoGrpCD " & vbCrLf
                    End If
                    '担当者ID(あいまい)
                    If .PropStrIncTantoID.Trim <> "" Then
                        strSearch &= " AND IIT.IncTantIDAimai = :IncTantoID " & vbCrLf
                    End If
                    '担当者氏名 インシデント担当者氏名(あいまい)

                    If .PropStrIncTantoNM.Trim <> "" Then
                        strSearch &= " AND IIT.IncTantNMAimai LIKE :TantNMAimai " & vbCrLf
                    End If

                ElseIf .PropStrTantoRdoCheck = C0102_RDO_KANYO Then
                    '関与選択時

                    '担当者グループ、担当者ID、担当者氏名が入力されているかチェック
                    If .PropStrTantoGrp.Trim <> "" Or .PropStrIncTantoID.Trim <> "" Or .PropStrIncTantoNM.Trim <> "" Then

                        strSearch &= " AND EXISTS (SELECT DISTINCT iwrt.IncNmb FROM incident_wk_rireki_tb AS iwrt " & vbCrLf
                        strSearch &= " LEFT OUTER JOIN incident_wk_tanto_tb AS iwtt ON iwrt.IncNmb = iwtt.IncNmb AND iwrt.WorkRirekiNmb = iwtt.WorkRirekiNmb" & vbCrLf
                        strSearch &= " LEFT OUTER JOIN hbkusr_mtb AS hm2 ON hm2.HBKUsrID = iwtt.WorkTantoID " & vbCrLf
                        strSearch &= " WHERE" & vbCrLf

                        '担当者グループ
                        If .PropStrTantoGrp.Trim <> "" Then
                            strSearch &= " iwtt.WorkTantoGrpCD = :TantoGrpCD" & vbCrLf
                        End If
                        '担当者ID
                        If .PropStrIncTantoID.Trim <> "" Then
                            If .PropStrTantoGrp.Trim <> "" Then
                                strSearch &= " AND" & vbCr
                            End If
                            strSearch &= " iwtt.WorkTantoID = :IncTantoID" & vbCrLf
                        End If
                        '担当者氏名 
                        If .PropStrIncTantoNM.Trim <> "" Then
                            If .PropStrTantoGrp.Trim <> "" Or .PropStrIncTantoID.Trim <> "" Then
                                strSearch &= " AND" & vbCr
                            End If
                            strSearch &= " hm2.HBKUsrNMAimai LIKE :TantNMAimai" & vbCrLf
                        End If

                        strSearch &= " AND IIT.IncNmb = iwrt.IncNmb ) " & vbCrLf
                    End If
                End If

                '*****************************************************************************************
                '作業予定日時(From)、(To)か作業内容に入力があった場合
                If .PropStrWorkSceDTFrom.Trim <> "" Or .PropStrWorkSceDTTo.Trim <> "" Or .PropStrWorkNaiyo <> "" Then

                    strSearch &= " AND EXISTS (SELECT DISTINCT iwrt.IncNmb " & vbCrLf
                    strSearch &= " FROM incident_wk_rireki_tb iwrt " & vbCrLf
                    strSearch &= " WHERE " & vbCrLf

                    '[Mod]2014/11/19 e.okamura 問題要望114 Start
                    ''作業予定日時(From)
                    'If .PropStrWorkSceDTFrom.Trim <> "" Then
                    '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                    '    'strSearch &= " iwrt.WorkSceDT >= TO_DATE(:WorkSceDTFrom,'YYYY/MM/DD HH24:MI') " & vbCrLf
                    '    strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') >= :WorkSceDTFrom " & vbCrLf
                    '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                    '
                    'End If
                    ''作業予定日時(To)
                    'If .PropStrWorkSceDTTo.Trim <> "" Then
                    '    If .PropStrWorkSceDTFrom.Trim <> "" Then
                    '        strSearch &= " AND " & vbCr
                    '    End If
                    '    '時間表記なし
                    '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする START
                    '    'strSearch &= " iwrt.WorkSceDT <= TO_DATE(:WorkSceDTTo,'YYYY/MM/DD HH24:MI') " & vbCrLf
                    '    strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') <= :WorkSceDTTo " & vbCrLf
                    '    '[mod] 2013/03/14 y.ikushima インシデントExcel出力、検索条件を画面を同一にする END
                    '
                    'End If

                    '作業予定日時(From)
                    If .PropStrWorkSceDTFrom.Trim <> "" Then
                        If .PropStrExWorkSceTimeFrom.Trim <> "" Then
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') >= :WorkSceDTFrom " & vbCrLf
                        Else
                            '時間表記なし
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD') >= :WorkSceDTFrom " & vbCrLf
                        End If
                    End If
                    '作業予定日時(To)
                    If .PropStrWorkSceDTTo.Trim <> "" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Then
                            strSearch &= " AND " & vbCr
                        End If
                        If .PropStrExWorkSceTimeTo.Trim <> "" Then
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD HH24:MI') <= :WorkSceDTTo " & vbCrLf
                        Else
                            '時間表記なし
                            strSearch &= " TO_CHAR(iwrt.WorkSceDT,'YYYY/MM/DD') <= :WorkSceDTTo " & vbCrLf
                        End If
                    End If
                    '[Mod]2014/11/19 e.okamura 問題要望114 End

                    '作業内容
                    If .PropStrWorkNaiyo <> "" Then
                        If .PropStrWorkSceDTFrom.Trim <> "" Or _
                           .PropStrWorkSceDTTo.Trim <> "" Then
                            strSearch &= " OR " & vbCrLf
                        End If
                        '検索文字列の分割
                        arySagyou = commonLogicHBK.GetSearchStringList(.PropStrWorkNaiyo, SPLIT_MODE_AND)
                        '分割分だけ検索条件の設定
                        If arySagyou.Length <> 0 Then
                            strSearch &= " ("
                            For intCnt = 0 To arySagyou.Count - 1
                                strSearch &= " iwrt.WorkNaiyoAimai LIKE :WorkNaiyoAimai" + intCnt.ToString()
                                If intCnt <> arySagyou.Count - 1 Then
                                    strSearch &= " AND "
                                End If
                            Next
                            strSearch &= ") " & vbCrLf
                        End If
                    End If
                    strSearch &= "AND IIT.IncNmb = iwrt.IncNmb ) " & vbCrLf
                End If
                '*****************************************************************************************

                'プロセスリンク
                If .PropStrProccesLinkKind <> "" Then
                    strSearch &= " AND IIT.IncNmb IN ( " & .PropStrProccesLinkKind & ")" & vbCrLf
                End If


                'インシデント機器情報
                If .PropStrKikiKind <> "" And .PropStrKikiNum <> "" Then
                    '機器種別及び機器番号入力時
                    strSearch &= " AND IIT.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.KindCD = :KindCD AND ikit.Num = :Num Group by ikit.IncNmb)" & vbCrLf
                ElseIf .PropStrKikiKind <> "" Then
                    '機器種別のみ入力時
                    strSearch &= " AND IIT.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.KindCD = :KindCD Group by ikit.IncNmb )" & vbCrLf
                ElseIf .PropStrKikiNum <> "" Then
                    '機器番号のみ入力時
                    strSearch &= " AND IIT.IncNmb IN ("
                    strSearch &= strSelectIncidentKikiSql
                    strSearch &= " WHERE ikit.Num = :Num Group by ikit.IncNmb )" & vbCrLf
                End If

                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-
                'ORDER BY句を指定
                'strSearch &= " ORDER BY SortDT.SortDT, IIT.IncNmb"

                '【ADD】 2012/08/15 r.hoshino START
                'strSearch &= "ORDER BY COALESCE(IIT.hasseidt,to_date('0000/00/00 00:00','YYYY/MM/DD HH24:MI:SS')) DESC"
                strSearch &= "ORDER BY IIT.hasseidt DESC"
                strSearch &= ",IIT.IncNmb"
                '【ADD】 2012/08/15 r.hoshino END

                '検索条件セット
                strSelect &= strSearch

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(strSelect, Cn)
                '+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-+-

                'インシデント番号(完全一致)
                If .PropBlnIncNumInputFlg = False Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))
                    Adapter.SelectCommand.Parameters("IncNmb").Value = .PropIntNum
                End If
                '[ADD] 2012/10/24 s.yamaguchi START
                'インシデント基本情報：受付手段(完全一致)
                If .PropStrUketsukeWay.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UkeKbnCD").Value = .PropStrUketsukeWay
                End If
                '[ADD] 2012/10/24 s.yamaguchi END
                'インシデント基本情報：インシデント種別(完全一致)
                If .PropStrIncidentKind.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IncKbnCD").Value = .PropStrIncidentKind
                End If
                'インシデント基本情報：ドメイン(完全一致)
                If .PropStrDomain.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("DomainCD").Value = .PropStrDomain
                End If
                'インシデント基本情報：外部ツール番号(完全一致)
                If .PropStrOutsideToolNum.Trim <> "" Then
                    'バインド変数を設定
                    For i As Integer = 0 To aryOutsideToolNum.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("OutSideToolNmb" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("OutSideToolNmb" + i.ToString).Value = aryOutsideToolNum(i)
                    Next
                End If
                'インシデント基本情報：タイトル(あいまい検索)
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
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrUkeNaiyo.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryUkeNaiyo.Count - 1
                        aryUkeNaiyo(i) = commonLogicHBK.ChangeStringForSearch(aryUkeNaiyo(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryUkeNaiyo.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UkeNaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("UkeNaiyoAimai" + i.ToString).Value = "%" & aryUkeNaiyo(i) & "%"
                    Next
                End If
                'インシデント基本情報：受付内容(あいまい検索)
                If .PropStrTaioKekka.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To aryTaioKekka.Count - 1
                        aryTaioKekka(i) = commonLogicHBK.ChangeStringForSearch(aryTaioKekka(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To aryTaioKekka.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TaioKekkaAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("TaioKekkaAimai" + i.ToString).Value = "%" & aryTaioKekka(i) & "%"
                    Next
                End If
                '作業日(FROM)
                If .PropStrHasseiDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HasseiDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HasseiDTFrom").Value = .PropStrHasseiDTFrom
                End If
                '作業日(To)
                If .PropStrHasseiDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HasseiDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("HasseiDTTo").Value = .PropStrHasseiDTTo
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
                'インシデント基本情報：フリーフラグ1
                If .PropStrFreeFlg1.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = .PropStrFreeFlg1
                End If
                'インシデント基本情報：フリーフラグ2
                If .PropStrFreeFlg2.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = .PropStrFreeFlg2
                End If
                'インシデント基本情報：フリーフラグ3
                If .PropStrFreeFlg3.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = .PropStrFreeFlg3
                End If
                'インシデント基本情報：フリーフラグ4
                If .PropStrFreeFlg4.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = .PropStrFreeFlg4
                End If
                'インシデント基本情報：フリーフラグ5
                If .PropStrFreeFlg5.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = .PropStrFreeFlg5
                End If
                '相手情報：相手ID(完全一致)
                If .PropStrPartnerID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("PartnerIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrPartnerID)
                End If
                '相手情報：相手氏名(あいまい)
                If .PropStrPartnerNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("PartnerNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrPartnerNM) & "%"
                End If
                '相手情報：相手部署(あいまい)
                If .PropStrUsrBusyoNM.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrBusyoNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrUsrBusyoNM) & "%"
                End If
                'イベント情報：イベントID(あいまい)
                If .PropStrEventID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EventIDAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrEventID) & "%"
                End If
                'イベント情報：OPCイベントID(あいまい)
                If .PropStrOPCEventID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("OPCEventIDAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrOPCEventID) & "%"
                End If
                'イベント情報：ソース(あいまい)
                If .PropStrSource.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("SourceAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrSource) & "%"
                End If
                'イベント情報：イベントクラス(あいまい)
                If .PropStrEventClass.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EventClassAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrEventClass) & "%"
                End If
                '担当者グループ
                If .PropStrTantoGrp.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantoGrpCD").Value = .PropStrTantoGrp
                End If
                '担当者ID
                If .PropStrIncTantoID.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("IncTantoID").Value = commonLogicHBK.ChangeStringForSearch(.PropStrIncTantoID.Trim)
                End If
                '担当者氏名 
                If .PropStrIncTantoNM <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("TantNMAimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(.PropStrIncTantoNM.Trim) & "%"
                End If
                'インシデント機器情報
                If .PropStrKikiKind <> "" Or .PropStrKikiNum <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KindCD").Value = .PropStrKikiKind
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Num").Value = .PropStrKikiNum
                End If
                '作業予定日時(From)
                If .PropStrWorkSceDTFrom.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTFrom").Value = .PropStrWorkSceDTFrom
                End If
                '作業予定日時(To)
                If .PropStrWorkSceDTTo.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkSceDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("WorkSceDTTo").Value = .PropStrWorkSceDTTo
                End If
                '作業内容
                If .PropStrWorkNaiyo <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To arySagyou.Count - 1
                        arySagyou(i) = commonLogicHBK.ChangeStringForSearch(arySagyou(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To arySagyou.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("WorkNaiyoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("WorkNaiyoAimai" + i.ToString).Value = "%" & arySagyou(i) & "%"
                    Next
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
