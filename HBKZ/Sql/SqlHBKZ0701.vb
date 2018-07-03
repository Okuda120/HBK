Imports Common
Imports System.Text
Imports Npgsql
Imports CommonHBK

''' <summary>
''' 機器検索一覧画面Sqlクラス
''' </summary>
''' <remarks>機器検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/06 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKZ0701

    Dim commonLogicHBK As New CommonLogicHBK

    'サポセン機器情報取得SQL
    Private strSelectSapSearch As String = "SELECT" & vbCrLf & _
                                            "  'False' AS Select " & vbCrLf & _
                                            " ,km.KindNM" & vbCrLf & _
                                            " ,cit.Num" & vbCrLf & _
                                            " ,cit.Class2" & vbCrLf & _
                                            " ,cit.CINM" & vbCrLf & _
                                            " ,cit.KindCD" & vbCrLf & _
                                            " ,ckm.Sort AS Sort1" & vbCrLf & _
                                            " ,km.Sort AS Sort2" & vbCrLf & _
                                            " ,cit.CINmb" & vbCrLf & _
                                            " ,km.KikiUseKbn" & vbCrLf & _
                                            " ,km.SetupFlg" & vbCrLf & _
                                            " ,cit.CIKbnCD" & vbCrLf & _
                                            " ,0 AS RowNmb" & vbCrLf & _
                                            " ,cit.SetKikiID" & vbCrLf & _
                                            "FROM" & vbCrLf & _
                                            " CI_INFO_TB cit" & vbCrLf & _
                                            "JOIN CI_SAP_TB cst        ON cit.CINmb = cst.CINmb" & vbCrLf & _
                                            "LEFT JOIN CI_KIND_MTB ckm ON cit.CIKbnCD = ckm.CIKbnCD" & vbCrLf & _
                                            "LEFT JOIN KIND_MTB km     ON cit.CIKbnCD = km.CIKbnCD AND cit.KindCD = km.KindCD" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            "      NOT EXISTS (SELECT 1 FROM CI_KIND_MTB ckm2 WHERE ckm2.JtiFlg = '1' AND ckm2.CIKbnCD = ckm.CIKbnCD) " & vbCrLf & _
                                            "  AND NOT EXISTS (SELECT 1 FROM KIND_MTB km2 WHERE km2.JtiFlg = '1' AND km2.CIKbnCD = km.CIKbnCD AND km2.KindCD = km.KindCD) " & vbCrLf & _
                                            "  AND cit.CIKbnCD = :CIKbnCD_Sap " & vbCrLf
    '部所有機器情報取得SQL
    Private strSelectBuySearch As String = "SELECT" & vbCrLf & _
                                            "  'False' AS Select " & vbCrLf & _
                                            " ,km.KindNM" & vbCrLf & _
                                            " ,cit.Num" & vbCrLf & _
                                            " ,cit.Class2" & vbCrLf & _
                                            " ,cit.CINM" & vbCrLf & _
                                            " ,cit.KindCD" & vbCrLf & _
                                            " ,ckm.Sort AS Sort1" & vbCrLf & _
                                            " ,km.Sort AS Sort2" & vbCrLf & _
                                            " ,cit.CINmb" & vbCrLf & _
                                            " ,km.KikiUseKbn" & vbCrLf & _
                                            " ,km.SetupFlg" & vbCrLf & _
                                            " ,cit.CIKbnCD" & vbCrLf & _
                                            " ,0 AS RowNmb" & vbCrLf & _
                                            " ,NULL AS SetKikiID" & vbCrLf & _
                                            "FROM" & vbCrLf & _
                                            " CI_INFO_TB cit" & vbCrLf & _
                                            "JOIN CI_BUY_TB cbt        ON cit.CINmb = cbt.CINmb" & vbCrLf & _
                                            "LEFT JOIN CI_KIND_MTB ckm ON cit.CIKbnCD = ckm.CIKbnCD" & vbCrLf & _
                                            "LEFT JOIN KIND_MTB km     ON cit.CIKbnCD = km.CIKbnCD AND cit.KindCD = km.KindCD" & vbCrLf & _
                                            "WHERE" & vbCrLf & _
                                            "      NOT EXISTS (SELECT 1 FROM CI_KIND_MTB ckm2 WHERE ckm2.JtiFlg = '1' AND ckm2.CIKbnCD = ckm.CIKbnCD) " & vbCrLf & _
                                            "  AND NOT EXISTS (SELECT 1 FROM KIND_MTB km2 WHERE km2.JtiFlg = '1' AND km2.CIKbnCD = km.CIKbnCD AND km2.KindCD = km.KindCD) " & vbCrLf & _
                                            "  AND cit.CIKbnCD = :CIKbnCD_Buy " & vbCrLf

    'サポセン機器検索結果件数取得SQL
    Private strSelectCountSap As String = "SELECT" & vbCrLf & _
                                          "  COUNT(*) AS Cnt" & vbCrLf & _
                                          "FROM" & vbCrLf & _
                                          " CI_INFO_TB cit" & vbCrLf & _
                                          "JOIN CI_SAP_TB cst        ON cit.CINmb = cst.CINmb" & vbCrLf & _
                                          "LEFT JOIN CI_KIND_MTB ckm ON cit.CIKbnCD = ckm.CIKbnCD" & vbCrLf & _
                                          "LEFT JOIN KIND_MTB km     ON cit.CIKbnCD = km.CIKbnCD AND cit.KindCD = km.KindCD" & vbCrLf & _
                                          "WHERE" & vbCrLf & _
                                          "      NOT EXISTS (SELECT 1 FROM CI_KIND_MTB ckm2 WHERE ckm2.JtiFlg = '1' AND ckm2.CIKbnCD = ckm.CIKbnCD) " & vbCrLf & _
                                          "  AND NOT EXISTS (SELECT 1 FROM KIND_MTB km2 WHERE km2.JtiFlg = '1' AND km2.CIKbnCD = km.CIKbnCD AND km2.KindCD = km.KindCD) " & vbCrLf & _
                                          "  AND cit.CIKbnCD = :CIKbnCD_Sap " & vbCrLf

    '部所有機器検索結果件数取得SQL
    Private strSelectCountBuy As String = "SELECT" & vbCrLf & _
                                          "  COUNT(*) AS Cnt" & vbCrLf & _
                                          "FROM" & vbCrLf & _
                                          " CI_INFO_TB cit" & vbCrLf & _
                                          "JOIN CI_BUY_TB cbt        ON cit.CINmb = cbt.CINmb" & vbCrLf & _
                                          "LEFT JOIN CI_KIND_MTB ckm ON cit.CIKbnCD = ckm.CIKbnCD" & vbCrLf & _
                                          "LEFT JOIN KIND_MTB km     ON cit.CIKbnCD = km.CIKbnCD AND cit.KindCD = km.KindCD" & vbCrLf & _
                                          "WHERE" & vbCrLf & _
                                          "      NOT EXISTS (SELECT 1 FROM CI_KIND_MTB ckm2 WHERE ckm2.JtiFlg = '1' AND ckm2.CIKbnCD = ckm.CIKbnCD) " & vbCrLf & _
                                          "  AND NOT EXISTS (SELECT 1 FROM KIND_MTB km2 WHERE km2.JtiFlg = '1' AND km2.CIKbnCD = km.CIKbnCD AND km2.KindCD = km.KindCD) " & vbCrLf & _
                                          "  AND cit.CIKbnCD = :CIKbnCD_Buy " & vbCrLf


    ''' <summary>
    ''' 検索の件数取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectCountKikiSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            With dataHBKZ0701

                'WHERE句作成
                If CreateWhereCmd(dataHBKZ0701) = False Then
                    Return False
                End If

                '検索機器区分によりSQLを作成
                If .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAP Then

                    '検索機器区分がサポセンの場合、サポセン機器情報を検索
                    strSQL &= strSelectCountSap & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf

                ElseIf .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_BUY Then

                    '検索機器区分が部所有の場合、部所有機器情報を検索
                    strSQL &= strSelectCountBuy & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf

                ElseIf .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAPBUY Then

                    '検索機器区分がサポセン／部所有の場合、サポセン機器情報、部所有機器情報を結合して検索
                    strSQL &= "SELECT SUM(t.Cnt) FROM (" & vbCrLf
                    strSQL &= strSelectCountSap & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf
                    strSQL &= "UNION ALL" & vbCrLf
                    strSQL &= strSelectCountBuy & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf
                    strSQL &= ") t" & vbCrLf

                End If

            End With

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKZ0701) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 検索のSQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器／部所有機器情報から、フォームから渡される値をもとに検索を行うSQL
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectKikiSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        'Dim strOrderBy As String = "ORDER BY Sort1, Sort2"
        Dim strOrderBy As String = "ORDER BY KindCD, Num"

        Try
            With dataHBKZ0701

                'WHERE句作成
                If CreateWhereCmd(dataHBKZ0701) = False Then
                    Return False
                End If

                '検索機器区分によりSQLを作成
                If .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAP Then

                    '検索機器区分がサポセンの場合、サポセン機器情報を検索
                    strSQL &= strSelectSapSearch & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf

                ElseIf .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_BUY Then

                    '検索機器区分が部所有の場合、部所有機器情報を検索
                    strSQL &= strSelectBuySearch & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf

                ElseIf .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAPBUY Then

                    '検索機器区分がサポセン／部所有の場合、サポセン機器情報、部所有機器情報を結合して検索
                    strSQL &= "SELECT" & vbCrLf
                    strSQL &= "  t.Select" & vbCrLf
                    strSQL &= " ,t.KindNM" & vbCrLf
                    strSQL &= " ,t.Num" & vbCrLf
                    strSQL &= " ,t.Class2" & vbCrLf
                    strSQL &= " ,t.CINM" & vbCrLf
                    strSQL &= " ,t.KindCD" & vbCrLf
                    strSQL &= " ,t.CINmb" & vbCrLf
                    strSQL &= " ,t.KikiUseKbn" & vbCrLf
                    strSQL &= " ,t.SetupFlg" & vbCrLf
                    strSQL &= " ,t.CIKbnCD" & vbCrLf
                    strSQL &= " ,t.RowNmb" & vbCrLf
                    strSQL &= " ,t.SetKikiID" & vbCrLf
                    strSQL &= "FROM (" & vbCrLf
                    strSQL &= strSelectSapSearch & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf
                    strSQL &= "UNION ALL" & vbCrLf
                    strSQL &= strSelectBuySearch & vbCrLf
                    strSQL &= .PropStrWhere & vbCrLf
                    strSQL &= ") t" & vbCrLf

                End If

            End With
           

            'SQLにORDER BY句を追加
            strSQL &= strOrderBy

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKZ0701) = False Then
                Return False
            End If


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' Where句作成
    ''' <param name="dataHBKZ0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>Where句の動的部分を作成する
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' </para></remarks>
    Public Function CreateWhereCmd(ByRef dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strWhere As String = ""

        Try
            'Where句作成
            With dataHBKZ0701

                '種別コード
                If .PropCmbKind.SelectedValue <> "" Then
                    strWhere &= " AND cit.KindCD = :KindCD " & vbCrLf
                End If
                '番号
                If .PropTxtNum.Text.Trim() <> "" Then
                    strWhere &= " AND cit.Num = :Num " & vbCrLf
                End If
                'CIステータス
                If .PropCmbCIStatus.SelectedValue <> "" Then
                    strWhere &= " AND cit.CIStatusCD = :CIStatusCD " & vbCrLf
                    '検索機器がサポセンで、かつ作業が「セットアップ」「陳腐化」の場合
                    If .PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAP AndAlso _
                       (.PropStrWorkCD = WORK_CD_SETUP Or .PropStrWorkCD = WORK_CD_OBSOLETE) Then
                        'セットアップフラグONを条件に追加
                        strWhere &= " AND km.SetupFlg = '" & SETUP_FLG_ON & "'" & vbCrLf
                    End If
                End If
                '名称（機種）　※あいまい検索
                If .PropTxtCINM.Text.Trim() <> "" Then
                    strWhere &= " AND cit.CINMaimai LIKE :CINM " & vbCrLf
                End If

                'データクラスにセット
                .PropStrWhere = strWhere

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Nothing)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' バインド変数への型と値の設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="dataHBKZ0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>バインド変数に型と値をセットする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' </para></remarks>
    Public Function SetBind(ByRef Adapter As NpgsqlDataAdapter, _
                            ByVal dataHBKZ0701 As DataHBKZ0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'バインド変数に型と値をセット
            With Adapter.SelectCommand

                '検索機器区分によりCI種別コードをセット
                If dataHBKZ0701.PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAP Then

                    '検索機器区分がサポセンの場合、サポセン機器のCI種別コードをセット
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD_Sap", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD_Sap").Value = CI_TYPE_SUPORT

                ElseIf dataHBKZ0701.PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_BUY Then

                    '検索機器区分が部所有の場合、部所有機器のCI種別コードをセット
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD_Buy", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD_Buy").Value = CI_TYPE_KIKI

                ElseIf dataHBKZ0701.PropStrSearchKikiKbn = LogicHBKZ0701.SEARCH_KIKI_SAPBUY Then

                    '検索機器区分がサポセン／部所有の場合、サポセン機器情報、部所有機器情報のCI種別コードをセット
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD_Sap", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters.Add(New NpgsqlParameter("CIKbnCD_Buy", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIKbnCD_Sap").Value = CI_TYPE_SUPORT
                    .Parameters("CIKbnCD_Buy").Value = CI_TYPE_KIKI

                End If

                '種別コード
                If dataHBKZ0701.PropCmbKind.SelectedValue <> "" Then
                    .Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("KindCD").Value = dataHBKZ0701.PropCmbKind.SelectedValue
                End If

                '番号
                If dataHBKZ0701.PropTxtNum.Text.Trim() <> "" Then
                    .Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    '.Parameters("Num").Value = dataHBKZ0701.PropTxtNum.Text
                    .Parameters("Num").Value = dataHBKZ0701.PropTxtNum.Text.Trim().PadLeft(5, "0"c)
                End If

                'CIステータス
                If dataHBKZ0701.PropCmbCIStatus.SelectedValue <> "" Then
                    .Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIStatusCD").Value = dataHBKZ0701.PropCmbCIStatus.SelectedValue
                End If

                '名称（機種）
                If dataHBKZ0701.PropTxtCINM.Text.Trim() <> "" Then
                    .Parameters.Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CINM").Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0701.PropTxtCINM.Text) & "%"
                End If

            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
