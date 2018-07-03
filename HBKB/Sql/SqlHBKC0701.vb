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
Public Class SqlHBKC0701

    Dim commonLogicHBK As New CommonLogicHBK

    'セット機器検索SQL：共通部分（FROM～WHERE句）
    'Private strSelectSetKiki_Common As String = _
    '    "FROM (" & vbCrLf & _
    '    " SELECT DISTINCT" & vbCrLf & _
    '    "   skt.EndUsrID" & vbCrLf & _
    '    "  ,cit.CINmb" & vbCrLf & _
    '    "  ,cit.KindCD" & vbCrLf & _
    '    "  ,cit.Num" & vbCrLf & _
    '    "  ,cit.CIStatusCD" & vbCrLf & _
    '    "  ,cit.SetKikiID" & vbCrLf & _
    '    "  ,cit.Class2" & vbCrLf & _
    '    "  ,cit.CINM" & vbCrLf & _
    '    "  ,cit.CIKbnCD" & vbCrLf & _
    '    " FROM CI_INFO_TB cit" & vbCrLf & _
    '    " LEFT JOIN SET_KIKI_MNG_TB skt ON cit.SetKikiID = skt.SetKikiID" & vbCrLf & _
    '    " LEFT JOIN CI_INFO_TB cit2 ON skt.SetKikiID = cit2.SetKikiID" & vbCrLf & _
    '    " WHERE cit.SetKikiID IS NOT NULL" & vbCrLf & _
    '    "   AND cit2.CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
    '    "   AND cit2.CIStatusCD IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    '    " {0}" & vbCrLf & _
    '    " UNION ALL" & vbCrLf & _
    '    " SELECT" & vbCrLf & _
    '    "   ''" & vbCrLf & _
    '    "  ,cit.CINmb" & vbCrLf & _
    '    "  ,cit.KindCD" & vbCrLf & _
    '    "  ,cit.Num" & vbCrLf & _
    '    "  ,cit.CIStatusCD" & vbCrLf & _
    '    "  ,cit.SetKikiID" & vbCrLf & _
    '    "  ,cit.Class2" & vbCrLf & _
    '    "  ,cit.CINM" & vbCrLf & _
    '    "  ,cit.CIKbnCD" & vbCrLf & _
    '    " FROM CI_INFO_TB cit" & vbCrLf & _
    '    " WHERE cit.SetKikiID IS NULL" & vbCrLf & _
    '    "   AND cit.CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
    '    "   AND cit.CIStatusCD IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    '    " {1}" & vbCrLf & _
    '    ") t" & vbCrLf & _
    '    "LEFT JOIN KIND_MTB km ON t.KindCD = km.KindCD" & vbCrLf & _
    '    "LEFT JOIN CISTATE_MTB cm ON t.CIStatusCD = cm.CIStateCD AND t.CIKbnCD = cm.CIKbnCD" & vbCrLf & _
    '    "LEFT JOIN ENDUSR_MTB em ON t.EndUsrID = em.EndUsrID" & vbCrLf & _
    '    "WHERE NOT EXISTS (" & vbCrLf & _
    '    "  SELECT *" & vbCrLf & _
    '    "  FROM CI_INFO_TB cit3" & vbCrLf & _
    '    "  WHERE cit3.SetKikiID = t.SetKikiID" & vbCrLf & _
    '    "    AND cit3.CIStatusCD NOT IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    '    ")"
    '[add] 2012/10/29 検索結果重複対応（AND cit.CINmb = skt.CINmbを追加）
    Private strSelectSetKiki_Common As String = _
    "FROM (" & vbCrLf & _
    " SELECT DISTINCT" & vbCrLf & _
    "   skt.EndUsrID" & vbCrLf & _
    "  ,cit.CINmb" & vbCrLf & _
    "  ,cit.KindCD" & vbCrLf & _
    "  ,cit.Num" & vbCrLf & _
    "  ,cit.CIStatusCD" & vbCrLf & _
    "  ,cit.SetKikiID" & vbCrLf & _
    "  ,cit.Class2" & vbCrLf & _
    "  ,cit.CINM" & vbCrLf & _
    "  ,cit.CIKbnCD" & vbCrLf & _
    " FROM CI_INFO_TB cit" & vbCrLf & _
    " LEFT JOIN SET_KIKI_MNG_TB skt ON cit.SetKikiID = skt.SetKikiID" & vbCrLf & _
    " LEFT JOIN CI_INFO_TB cit2 ON skt.SetKikiID = cit2.SetKikiID" & vbCrLf & _
    " WHERE cit.SetKikiID IS NOT NULL" & vbCrLf & _
    "   AND cit2.CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
    "   AND cit2.CIStatusCD IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    "   AND cit.CINmb = skt.CINmb" & vbCrLf & _
    " {0}" & vbCrLf & _
    " UNION ALL" & vbCrLf & _
    " SELECT" & vbCrLf & _
    "   ''" & vbCrLf & _
    "  ,cit.CINmb" & vbCrLf & _
    "  ,cit.KindCD" & vbCrLf & _
    "  ,cit.Num" & vbCrLf & _
    "  ,cit.CIStatusCD" & vbCrLf & _
    "  ,cit.SetKikiID" & vbCrLf & _
    "  ,cit.Class2" & vbCrLf & _
    "  ,cit.CINM" & vbCrLf & _
    "  ,cit.CIKbnCD" & vbCrLf & _
    " FROM CI_INFO_TB cit" & vbCrLf & _
    " WHERE cit.SetKikiID IS NULL" & vbCrLf & _
    "   AND cit.CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
    "   AND cit.CIStatusCD IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    " {1}" & vbCrLf & _
    ") t" & vbCrLf & _
    "LEFT JOIN KIND_MTB km ON t.KindCD = km.KindCD" & vbCrLf & _
    "LEFT JOIN CISTATE_MTB cm ON t.CIStatusCD = cm.CIStateCD AND t.CIKbnCD = cm.CIKbnCD" & vbCrLf & _
    "LEFT JOIN ENDUSR_MTB em ON t.EndUsrID = em.EndUsrID" & vbCrLf & _
    "WHERE NOT EXISTS (" & vbCrLf & _
    "  SELECT *" & vbCrLf & _
    "  FROM CI_INFO_TB cit3" & vbCrLf & _
    "  WHERE cit3.SetKikiID = t.SetKikiID" & vbCrLf & _
    "    AND cit3.CIStatusCD NOT IN ('" & CI_STATUS_SUPORT_KADOUCHU & "','" & CI_STATUS_SUPORT_TSUIKASETTEIMACHI & "')" & vbCrLf & _
    ")"

    'セット機器取得SQL：SELECT句
    Private strSelectSetKiki As String = _
        "SELECT" & vbCrLf & _
        "  'False' AS Select " & vbCrLf & _
        " ,km.KindNM || t.Num AS KikiNM" & vbCrLf & _
        " ,t.EndUsrID" & vbCrLf & _
        " ,em.EndUsrNM" & vbCrLf & _
        " ,cm.CIStateNM" & vbCrLf & _
        " ,t.CINmb" & vbCrLf & _
        " ,t.KindCD" & vbCrLf & _
        " ,km.KindNM" & vbCrLf & _
        " ,t.Num" & vbCrLf & _
        " ,t.CIStatusCD" & vbCrLf & _
        " ,t.SetKikiID" & vbCrLf & _
        " ,t.Class2" & vbCrLf & _
        " ,t.CINM" & vbCrLf & _
        " ,'' AS WorkCD" & vbCrLf & _
        " ,'' AS WorkNM" & vbCrLf & _
        strSelectSetKiki_Common & vbCrLf & _
        "ORDER BY t.SetKikiID, t.KindCD, t.Num"

    'セット機器件数取得SQL：SELECT句
    Private strSelectCountSetKiki As String = _
        "SELECT DISTINCT COUNT(*) AS Cnt" & vbCrLf & _
        strSelectSetKiki_Common
                                          


    ''' <summary>
    ''' 検索の件数取得SQLの設定
    ''' </summary> 
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0701">[IN/OUT]セット機器選択画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>フォームから渡される値を基にセット機器の検索を行った結果の件数を取得するSQLを作成しアダプタにセットする
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectCountSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            With dataHBKC0701

                'WHERE句作成
                If CreateWhereCmd(dataHBKC0701) = False Then
                    Return False
                End If

                'SQL文作成
                strSQL &= String.Format(strSelectCountSetKiki, .PropStrWhere1, .PropStrWhere2)

            End With

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKC0701) = False Then
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
    ''' </summary> 
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0701">[IN]セット機器選択画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器情報から、フォームから渡される値をもとに検索を行うSQLを作成しアダプタにセットする
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' </para></remarks>
    Public Function SetSelectSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            With dataHBKC0701

                 'SQL文作成
                strSQL = String.Format(strSelectSetKiki, .PropStrWhere1, .PropStrWhere2)

            End With

            'データアダプタに、SQLを設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            If SetBind(Adapter, dataHBKC0701) = False Then
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
    ''' WHERE句作成
    ''' <param name="dataHBKC0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>WHERE句の動的部分を作成する
    ''' <para>作成情報：2012/09/24 t.fukuo
    ''' </para></remarks>
    Public Function CreateWhereCmd(ByRef dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strWhere1 As String = ""    '1つ目のWHERE句（セット機器あり）
        Dim strWhere2 As String = ""    '2つ目のWHERE句（セット機器なし）

        Try
            'WHERE句作成
            With DataHBKC0701

                '種別コード
                If .PropCmbKind.SelectedValue <> "" Then
                    strWhere1 &= " AND cit2.KindCD = :KindCD " & vbCrLf
                    strWhere2 &= " AND cit.KindCD = :KindCD " & vbCrLf
                End If

                '番号
                If .PropTxtNum.Text.Trim() <> "" Then
                    strWhere1 &= " AND cit2.Num = :Num " & vbCrLf
                    strWhere2 &= " AND cit.Num = :Num " & vbCrLf
                End If


                'データクラスにセット
                .PropStrWhere1 = strWhere1
                .PropStrWhere2 = strWhere2

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
    ''' <param name="dataHBKC0701">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>バインド変数に型と値をセットする
    ''' <para>作成情報：2012/07/06 t.fukuo
    ''' </para></remarks>
    Public Function SetBind(ByRef Adapter As NpgsqlDataAdapter, _
                            ByVal dataHBKC0701 As DataHBKC0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try

            'バインド変数に型と値をセット
            With Adapter.SelectCommand

                '種別コード
                If DataHBKC0701.PropCmbKind.SelectedValue <> "" Then
                    .Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("KindCD").Value = DataHBKC0701.PropCmbKind.SelectedValue
                End If

                '番号
                If DataHBKC0701.PropTxtNum.Text.Trim() <> "" Then
                    .Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("Num").Value = DataHBKC0701.PropTxtNum.Text.Trim().PadLeft(5, "0"c)
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
