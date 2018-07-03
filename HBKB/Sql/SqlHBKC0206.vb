Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' インシデント登録（チェックリスト出力）Sqlクラス
''' </summary>
''' <remarks>インシデント登録（チェックリスト出力）のSQLの作成・設定を行う
''' <para>作成情報：2012/07/30 s.tsuruta
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0206

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'CIサポセン機器履歴情報取得（SELECT）SQL
    Private strSelectCISapRSql As String = "SELECT " & vbCrLf & _
                                          " cr.UsrID" & vbCrLf & _
                                          ",cr.UsrNM" & vbCrLf & _
                                          ",cr.UsrCompany" & vbCrLf & _
                                          ",cr.UsrBusyoNM" & vbCrLf & _
                                          ",cr.UsrMailAdd" & vbCrLf & _
                                          ",cr.UsrContact" & vbCrLf & _
                                          ",cr.UsrRoom" & vbCrLf & _
                                          ",cr.FixedIP" & vbCrLf & _
                                          ",cr.Serial" & vbCrLf & _
                                          ",cr.SetKyokuNM" & vbCrLf & _
                                          ",cr.SetBusyoNM" & vbCrLf & _
                                          ",cr.SetRoom" & vbCrLf & _
                                          ",cr.SetBuil" & vbCrLf & _
                                          ",cr.SetFloor" & vbCrLf & _
                                          ",cr.SetDeskNo" & vbCrLf & _
                                          "FROM CI_SAP_RTB cr " & vbCrLf & _
                                          "WHERE cr.CINmb = :CINmb " & vbCrLf & _
                                          "and cr.RirekiNo = :RirekiNo "

    'セット機器履歴情報取得（SELECT）SQL
    '[MOD] 2015/08/24 y.naganuma セット機器情報不具合対応 START
    'Private strSelectSetKikiRSql As String = "SELECT " & vbCrLf & _
    '                                        " ( SELECT km.KindNM" & vbCrLf & _
    '                                        "   FROM KIND_MTB km" & vbCrLf & _
    '                                        "   WHERE CIKbnCD = '" & CI_TYPE_SUPORT & "'" & vbCrLf & _
    '                                        "   AND ct.KindCD = km.KindCD" & vbCrLf & _
    '                                        " ) || ct.Num " & vbCrLf & _
    '                                        " AS SetKikiNo" & vbCrLf & _
    '                                        "FROM CI_INFO_RTB ct " & vbCrLf & _
    '                                        "WHERE (ct.CINmb,ct.RirekiNo) IN (SELECT SetCINmb,SetRirekiNo FROM SETKIKI_RTB WHERE SetKikiID IN " & vbCrLf & _
    '                                        "(SELECT SetKikiID FROM SETKIKI_RTB WHERE CINmb = :CINmb AND RirekiNo = :RirekiNo)) "
    Private strSelectSetKikiRSql As String = "SELECT " & vbCrLf & _
                                            "  t.kindnm || t.num as setkikino" & vbCrLf & _
                                            " FROM" & vbCrLf & _
                                            "  (SELECT" & vbCrLf & _
                                            "   (SELECT km.kindnm FROM kind_mtb km WHERE cikbncd = '" & CI_TYPE_SUPORT & "'AND km.kindcd = ct.kindcd) AS kindnm" & vbCrLf & _
                                            "    ,ct.num AS num" & vbCrLf & _
                                            "    ,ct.cinmb" & vbCrLf & _
                                            "    ,(SELECT km.sort FROM kind_mtb km WHERE cikbncd = '" & CI_TYPE_SUPORT & "'AND km.kindcd = ct.kindcd) AS sort" & vbCrLf & _
                                            "    FROM CI_INFO_RTB ct" & vbCrLf & _
                                            "    LEFT OUTER JOIN SETKIKI_RTB skt on ct.cinmb = skt.setcinmb AND ct.rirekino = skt.setrirekino" & vbCrLf & _
                                            "    WHERE (ct.cinmb,ct.rirekino) IN (SELECT setcinmb, setrirekino FROM setkiki_rtb WHERE cinmb = skt.cinmb and rirekino = skt.rirekino)" & vbCrLf & _
                                            "    AND skt.cinmb = :CINmb" & vbCrLf & _
                                            "    AND skt.rirekino = :RirekiNo" & vbCrLf & _
                                            "    AND skt.setkikiid = (SELECT crt.setkikiid FROM ci_info_rtb crt WHERE crt.cinmb = :CINmb AND crt.rirekino = :RirekiNo)" & vbCrLf & _
                                            "  ) t" & vbCrLf & _
                                            "WHERE t.cinmb <> :CINmb" & vbCrLf & _
                                            "GROUP BY t.kindnm, t.num, t.sort " & vbCrLf & _
                                            "ORDER BY t.sort, t.num"
    '[MOD] 2015/08/24 y.naganuma セット機器情報不具合対応 END

    'オプションソフト履歴取得（SELECT）SQL
    Private strSelectOptionSoftRSql As String = "SELECT " & vbCrLf & _
                                                " sm.SoftNM" & vbCrLf & _
                                                "FROM optsoft_rtb ort " & vbCrLf & _
                                                "LEFT OUTER JOIN soft_mtb sm ON sm.SoftCD = ort.SoftCD AND sm.SoftKbn = '" & SOFTKBN_OPTIONSOFT & "'" & vbCrLf & _
                                                "WHERE ort.CINmb = :CINmb " & vbCrLf & _
                                                "and ort.RirekiNo = :RirekiNo "



    ''' <summary>
    ''' CIサポセン機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0206">[IN]インシデント登録（チェックリスト出力）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCISupportSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCISapRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号


            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0206.PropIntCINmb                                 'CI番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0206.PropIntRirekiNo                           '履歴番号


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
    ''' セット機器履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0206">[IN]インシデント登録（チェックリスト出力）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>セット機器履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSetKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSetKikiRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("SetKikiNo", NpgsqlTypes.NpgsqlDbType.Varchar))        'セット機器番号

            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0206.PropIntCINmb                                     'CI番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0206.PropIntRirekiNo                               '履歴番号
            Adapter.SelectCommand.Parameters("SetKikiNo").Value = dataHBKC0206.PropStrKindCD & dataHBKC0206.PropStrKikiNmb  'セット機器番号

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
    '''オプションソフト履歴取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0206">[IN]インシデント登録（チェックリスト出力）データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>オプションソフト履歴取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/30 s.tsuruta
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectOptionSoftSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0206 As DataHBKC0206) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectOptionSoftRSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))        'CI番号
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '履歴番号

            Adapter.SelectCommand.Parameters("CINmb").Value = dataHBKC0206.PropIntCINmb                                 'CI番号
            Adapter.SelectCommand.Parameters("RirekiNo").Value = dataHBKC0206.PropIntRirekiNo                           '履歴番号

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
