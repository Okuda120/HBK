Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 対象システム検索一覧画面Sqlクラス
''' </summary>
''' <remarks>対象システム検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/10/23 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKZ1301

    'インスタンスの生成
    Dim commonLogicHBK As New CommonLogicHBK

    '部所有機器検索結果件数取得SQL
    Private strSelectCount As String = "SELECT" & vbCrLf & _
                                       " COUNT(*) AS Cnt" & vbCrLf & _
                                       " FROM (" & vbCrLf & _
                                       " SELECT cistatuscd, class1, class2, cinm, cinmb, FreeFlg1, FreeFlg2, FreeFlg3, FreeFlg4, FreeFlg5, Class1Aimai, Class2Aimai, CINMAimai, BikoAimai, '1' as sort0, sort" & vbCrLf & _
                                       " FROM ci_info_tb" & vbCrLf & _
                                       " WHERE cistatuscd <> :CIStateHaisizumi AND cikbncd= :cikbncd" & vbCrLf & _
                                       " UNION" & vbCrLf & _
                                       " SELECT cistatuscd, class1, class2, cinm, cinmb, FreeFlg1, FreeFlg2, FreeFlg3, FreeFlg4, FreeFlg5, Class1Aimai, Class2Aimai, CINMAimai, BikoAimai, '2' as sort0, sort" & vbCrLf & _
                                       " FROM ci_info_tb" & vbCrLf & _
                                       " WHERE cistatuscd = :CIStateHaisizumi  AND cikbncd= :cikbncd" & vbCrLf & _
                                       " ) AS c" & vbCrLf & _
                                       " LEFT JOIN cistate_mtb AS csm ON csm.cistatecd = c.cistatuscd" & vbCrLf & _
                                       " WHERE '1'" & vbCrLf

    ''[SELECT]対象システム取得SQL
    Private strSelectTaisyouSystemSql As String = "SELECT" & vbCrLf & _
                                                 " 'False' AS Select," & vbCrLf & _
                                                  " class1," & vbCrLf & _
                                                  " class2," & vbCrLf & _
                                                  " cinm," & vbCrLf & _
                                                  " cistatenm," & vbCrLf & _
                                                  " cinmb" & vbCrLf & _
                                                  " FROM (" & vbCrLf & _
                                                  " SELECT cistatuscd, class1, class2, cinm, cinmb, FreeFlg1, FreeFlg2, FreeFlg3, FreeFlg4, FreeFlg5, Class1Aimai, Class2Aimai, CINMAimai, BikoAimai, '1' as sort0, sort" & vbCrLf & _
                                                  " FROM  ci_info_tb " & vbCrLf & _
                                                  " WHERE cistatuscd <> :CIStateHaisizumi AND cikbncd= :cikbncd" & vbCrLf & _
                                                  " UNION " & vbCrLf & _
                                                  " SELECT cistatuscd, class1, class2, cinm, cinmb, FreeFlg1, FreeFlg2, FreeFlg3, FreeFlg4, FreeFlg5, Class1Aimai, Class2Aimai, CINMAimai, BikoAimai, '2' as sort0, sort" & vbCrLf & _
                                                  " FROM  ci_info_tb " & vbCrLf & _
                                                  " WHERE cistatuscd = :CIStateHaisizumi  AND cikbncd= :cikbncd" & vbCrLf & _
                                                  " ) AS c" & vbCrLf & _
                                                  " LEFT JOIN cistate_mtb AS csm ON csm.CIStateCD = c.cistatuscd" & vbCrLf & _
                                                  " WHERE '1'" & vbCrLf

    ''' <summary>
    ''' 検索の件数取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ1301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>Boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' </para></remarks>
    Public Function SetSelectCountTaisyouSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'WHERE句作成
            If CreateWhereCmd(dataHBKZ1301) = False Then
                Return False
            End If

            'SELECT分の作成
            strSQL &= strSelectCount & vbCrLf
            strSQL &= dataHBKZ1301.PropStrWhere

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKZ1301) = False Then
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
    ''' 対象システム取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ1301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>Boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' </para></remarks>
    Public Function SetSelectTaisyouSystemSql(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""
        Dim strSort As String = " ORDER BY Sort0, c.Sort"

        Try

            'WHERE句作成
            If CreateWhereCmd(dataHBKZ1301) = False Then
                Return False
            End If

            'SELECT分の作成
            strSQL &= strSelectTaisyouSystemSql & vbCrLf
            strSQL &= dataHBKZ1301.PropStrWhere & vbCrLf
            strSQL &= strSort

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKZ1301) = False Then
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
    ''' <param name="dataHBKZ1301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>Boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>Where句の動的部分を作成する
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' </para></remarks>
    Public Function CreateWhereCmd(ByRef dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strWhere As String = ""                     'WHERE句
        Dim aryFreeText As String() = Nothing           'フリーテキスト

        Try
            'Where句作成
            With dataHBKZ1301

                'CIステータス
                If .PropCmbStatus.SelectedValue <> "" Then
                    strWhere &= " AND CIStatusCD = :CIStatusCD" & vbCrLf
                End If
                '分類1　　　　            ※あいまい検索
                If .PropTxtClass1.Text.Trim() <> "" Then
                    strWhere &= " AND Class1Aimai LIKE :Class1Aimai" & vbCrLf
                End If
                '分類2　　　　            ※あいまい検索
                If .PropTxtClass2.Text.Trim() <> "" Then
                    strWhere &= " AND Class2Aimai LIKE :Class2Aimai" & vbCrLf
                End If
                '名称　　　　             ※あいまい検索
                If .PropTxtCINm.Text.Trim() <> "" Then
                    strWhere &= " AND CINMaimai LIKE :CINMaimai" & vbCrLf
                End If
                'フリーテキスト           ※あいまい検索
                If .PropTxtFreeText.Text.Trim <> "" Then
                    '検索文字列の分割
                    aryFreeText = commonLogicHBK.GetSearchStringList(.PropTxtFreeText.Text, SPLIT_MODE_AND)
                    .PropAryFreeText = aryFreeText
                    '分割分だけ検索条件の設定
                    If .PropAryFreeText.Length <> 0 Then
                        strWhere &= " AND "
                        strWhere &= " ("
                        For intCnt = 0 To .PropAryFreeText.Count - 1
                            strWhere &= " BikoAimai LIKE :BikoAimai" + intCnt.ToString()
                            If intCnt <> .PropAryFreeText.Count - 1 Then
                                strWhere &= " AND "
                            End If
                        Next
                        strWhere &= ") " & vbCrLf
                    End If
                End If
                'フリーフラグ1
                If .PropCmbFreeFlg1.Text.Trim <> "" Then
                    strWhere &= " AND FreeFlg1 = :FreeFlg1" & vbCrLf
                End If
                'フリーフラグ2
                If .PropCmbFreeFlg2.Text.Trim <> "" Then
                    strWhere &= " AND FreeFlg2 = :FreeFlg2" & vbCrLf
                End If
                'フリーフラグ3
                If .PropCmbFreeFlg3.Text.Trim <> "" Then
                    strWhere &= " AND FreeFlg3 = :FreeFlg3" & vbCrLf
                End If
                'フリーフラグ4
                If .PropCmbFreeFlg4.Text.Trim <> "" Then
                    strWhere &= " AND FreeFlg4 = :FreeFlg4" & vbCrLf
                End If
                'フリーフラグ5
                If .PropCmbFreeFlg5.Text.Trim <> "" Then
                    strWhere &= " AND FreeFlg5 = :FreeFlg5" & vbCrLf
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
    ''' <param name="dataHBKZ1301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>Boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>バインド変数に型と値をセットする
    ''' <para>作成情報：2012/10/23 s.yamaguchi
    ''' </para></remarks>
    Public Function SetBind(ByRef Adapter As NpgsqlDataAdapter, _
                            ByVal dataHBKZ1301 As DataHBKZ1301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'バインド変数に型と値をセット
            With Adapter.SelectCommand

                'CI種別(システム)
                .Parameters.Add(New NpgsqlParameter("cikbncd", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Parameters("cikbncd").Value = CI_TYPE_SYSTEM

                '副問い合わせ用ステータスCD(廃止済)
                .Parameters.Add(New NpgsqlParameter("CIStateHaisizumi", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Parameters("CIStateHaisizumi").Value = CI_STATUS_SYSTEM_HAISHIZUMI

                '［検索条件］CIステータスCD
                If dataHBKZ1301.PropCmbStatus.SelectedValue <> "" Then
                    .Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CIStatusCD").Value = dataHBKZ1301.PropCmbStatus.SelectedValue
                End If
                '［検索条件］分類1
                If dataHBKZ1301.PropTxtClass1.Text <> "" Then
                    .Parameters.Add(New NpgsqlParameter("Class1Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("Class1Aimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ1301.PropTxtClass1.Text) & "%"
                End If
                '［検索条件］分類2
                If dataHBKZ1301.PropTxtClass2.Text <> "" Then
                    .Parameters.Add(New NpgsqlParameter("Class2Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("Class2Aimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ1301.PropTxtClass2.Text) & "%"
                End If
                '［検索条件］名称
                If dataHBKZ1301.PropTxtCINm.Text <> "" Then
                    .Parameters.Add(New NpgsqlParameter("CINMaimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("CINMaimai").Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ1301.PropTxtCINm.Text) & "%"
                End If
                '［検索条件］フリーテキスト
                If dataHBKZ1301.PropTxtFreeText.Text.Trim <> "" Then
                    '検索文字列をあいまい検索用に変換
                    For i As Integer = 0 To dataHBKZ1301.PropAryFreeText.Count - 1
                        dataHBKZ1301.PropAryFreeText(i) = commonLogicHBK.ChangeStringForSearch(dataHBKZ1301.PropAryFreeText(i))
                    Next
                    'バインド変数を設定
                    For i As Integer = 0 To dataHBKZ1301.PropAryFreeText.Count - 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("BikoAimai" + i.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("BikoAimai" + i.ToString).Value = "%" & dataHBKZ1301.PropAryFreeText(i) & "%"
                    Next
                End If
                '［検索条件］フリーフラグ1
                If dataHBKZ1301.PropCmbFreeFlg1.Text.Trim <> "" Then
                    .Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("FreeFlg1").Value = dataHBKZ1301.PropCmbFreeFlg1.SelectedValue
                End If
                '［検索条件］フリーフラグ2
                If dataHBKZ1301.PropCmbFreeFlg2.Text.Trim <> "" Then
                    .Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("FreeFlg2").Value = dataHBKZ1301.PropCmbFreeFlg2.SelectedValue
                End If
                '［検索条件］フリーフラグ3
                If dataHBKZ1301.PropCmbFreeFlg3.Text.Trim <> "" Then
                    .Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("FreeFlg3").Value = dataHBKZ1301.PropCmbFreeFlg3.SelectedValue
                End If
                '［検索条件］フリーフラグ4
                If dataHBKZ1301.PropCmbFreeFlg4.Text.Trim <> "" Then
                    .Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("FreeFlg4").Value = dataHBKZ1301.PropCmbFreeFlg4.SelectedValue
                End If
                '［検索条件］フリーフラグ5
                If dataHBKZ1301.PropCmbFreeFlg5.Text.Trim <> "" Then
                    .Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("FreeFlg5").Value = dataHBKZ1301.PropCmbFreeFlg5.SelectedValue
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
