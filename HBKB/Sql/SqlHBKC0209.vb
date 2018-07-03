Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' インシデント登録（メール作成）Sqlクラス
''' </summary>
''' <remarks>インシデント登録（メール作成）のSQLの作成・設定を行う
''' <para>作成情報：2012/08/08 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0209

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'CI番号取得SQL
    Private strSelectCINmbSql As String = " SELECT " & vbCrLf & _
                                                        " CINmb " & vbCrLf & _
                                                    " FROM " & vbCrLf & _
                                                        " ci_info_tb " & vbCrLf

    'レンタル機器情報取得SQL
    Private strRentalKikiSql As String = " SELECT " & vbCrLf & _
                                                            " CIT.Class2, " & vbCrLf & _
                                                            " CIT.CINM, " & vbCrLf & _
                                                            " CST.Fuzokuhin, " & vbCrLf & _
                                                            " CASE WHEN CST.RentalStDT = '' THEN '' " & vbCrLf & _
                                                            " ELSE TO_CHAR(TO_DATE(CST.RentalStDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                            " END AS RentalStDT, " & vbCrLf & _
                                                            " CASE WHEN CST.RentalEdDT = '' THEN '' " & vbCrLf & _
                                                            " ELSE TO_CHAR(TO_DATE(CST.RentalEdDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                            " END AS RentalEdDT, " & vbCrLf & _
                                                            " CST.SetBusyoNM, " & vbCrLf & _
                                                            " ST.UsrNM " & vbCrLf & _
                                                        " FROM " & vbCrLf & _
                                                            " ci_info_tb CIT " & vbCrLf & _
                                                        " LEFT OUTER JOIN ci_sap_tb CST ON CIT.CINmb = CST.CINmb " & vbCrLf & _
                                                        " LEFT OUTER JOIN (SELECT CINmb,STRING_AGG(UsrNM,'／') AS UsrNM FROM share_tb GROUP BY CINmb) ST ON CIT.CINmb = ST.CINmb " & vbCrLf & _
                                                        " WHERE CIT.CINmb = :CINmb " & vbCrLf

    '部所有機器情報取得SQL
    Private strByuKikiSql As String = " SELECT " & vbCrLf & _
                                                        " CBT.Aliau, " & vbCrLf & _
                                                        " HBKF0008(CBT.DNSRegCD,'" & KIKISTATEKBN_DNS_REG & "'), " & vbCrLf & _
                                                        " HBKF0008(CBT.IPUseCD,'" & KIKISTATEKBN_IP_WARIATE & "'), " & vbCrLf & _
                                                        " CBT.FixedIP, " & vbCrLf & _
                                                        " CASE WHEN CBT.ZooKbn = '" & ZOO_KBN_UNFIN & "' THEN '" & ZOO_NM_UNFIN & "' " & vbCrLf & _
                                                                " WHEN CBT.ZooKbn = '" & ZOO_KBN_FIN & "' THEN '" & ZOO_NM_FIN & "' " & vbCrLf & _
                                                        " ELSE '' END, " & vbCrLf & _
                                                        " CIT.Class2, " & vbCrLf & _
                                                        " CIT.CINM, " & vbCrLf & _
                                                        " CIT.Class1, " & vbCrLf & _
                                                        " SM.SoftNM, " & vbCrLf & _
                                                        " CBT.NIC1, " & vbCrLf & _
                                                        " CBT.MacAddress1, " & vbCrLf & _
                                                        " CBT.NIC2, " & vbCrLf & _
                                                        " CBT.MacAddress2, " & vbCrLf & _
                                                        " SM2.SoftNM, " & vbCrLf & _
                                                        " CBT.AntiVirusSofCheckDT, " & vbCrLf & _
                                                        " CBT.SetBuil, " & vbCrLf & _
                                                        " CBT.SetFloor, " & vbCrLf & _
                                                        " CBT.SetRoom, " & vbCrLf & _
                                                        " CBT.UsrID, " & vbCrLf & _
                                                        " CBT.UsrNM, " & vbCrLf & _
                                                        " CASE WHEN CBT.ConnectDT = '' THEN '' " & vbCrLf & _
                                                        " ELSE TO_CHAR(TO_DATE(CBT.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                        " END AS ConnectDT, " & vbCrLf & _
                                                        " CASE WHEN CBT.ExpirationDT = '' THEN '' " & vbCrLf & _
                                                        " ELSE TO_CHAR(TO_DATE(CBT.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                        " END AS ExpirationDT, " & vbCrLf & _
                                                        " CBT.ConectReason, " & vbCrLf & _
                                                        " CBT.BusyoKikiBiko " & vbCrLf & _
                                                    " FROM ci_info_tb CIT " & vbCrLf & _
                                                    " LEFT OUTER JOIN ci_buy_tb CBT ON CIT.CINmb = CBT.CINmb " & vbCrLf & _
                                                    " LEFT OUTER JOIN soft_mtb SM ON CBT.OSNM = SM.SoftNM AND SM.SoftKbn = '" & SOFTKBN_OS & "' " & vbCrLf & _
                                                    " LEFT OUTER JOIN soft_mtb SM2 ON CBT.AntiVirusSoftNM = SM2.SoftNM AND SM2.SoftKbn = '" & SOFTKBN_UNTIVIRUSSOFT & "' " & vbCrLf & _
                                                    " WHERE CIT.CINmb = :CINmb " & vbCrLf

    '部所有機器情報取得(相手情報）
    'Private strSelectAiteSql As String = " SELECT " & vbCrLf & _
    '                                                    " KM.KindNM || CIT.Num, " & vbCrLf & _
    '                                                    " CASE WHEN CBT.ConnectDT = '' THEN '' " & vbCrLf & _
    '                                                    " ELSE TO_CHAR(TO_DATE(CBT.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
    '                                                    " END AS ConnectDT, " & vbCrLf & _
    '                                                    " CASE WHEN CBT.ExpirationDT = '' THEN '' " & vbCrLf & _
    '                                                    " ELSE TO_CHAR(TO_DATE(CBT.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
    '                                                    " END AS ExpirationDT, " & vbCrLf & _
    '                                                    " CIT.Class2, " & vbCrLf & _
    '                                                    " CIT.CINM " & vbCrLf & _
    '                                                " FROM ci_info_tb CIT " & vbCrLf & _
    '                                                " LEFT OUTER JOIN ci_buy_tb CBT ON CIT.CINmb = CBT.CINmb " & vbCrLf & _
    '                                                " LEFT OUTER JOIN kind_mtb KM ON CIT.KindCD = KM.KindCD " & vbCrLf & _
    '                                                " WHERE CBT.DeletDT != '' AND CBT.UsrID = :UsrID " & vbCrLf & _
    '                                                " ORDER BY CBT.CINmb "
    Private strSelectAiteSql As String = " SELECT " & vbCrLf & _
                                                    " KM.KindNM || CIT.Num, " & vbCrLf & _
                                                    " CASE WHEN CBT.ConnectDT = '' THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(TO_DATE(CBT.ConnectDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                    " END AS ConnectDT, " & vbCrLf & _
                                                    " CASE WHEN CBT.ExpirationDT = '' THEN '' " & vbCrLf & _
                                                    " ELSE TO_CHAR(TO_DATE(CBT.ExpirationDT,'YYYYMMDD'),'YYYY/MM/DD HH24:MI') " & vbCrLf & _
                                                    " END AS ExpirationDT, " & vbCrLf & _
                                                    " CIT.Class2, " & vbCrLf & _
                                                    " CIT.CINM " & vbCrLf & _
                                                " FROM ci_info_tb CIT " & vbCrLf & _
                                                " LEFT OUTER JOIN ci_buy_tb CBT ON CIT.CINmb = CBT.CINmb " & vbCrLf & _
                                                " LEFT OUTER JOIN kind_mtb KM ON CIT.KindCD = KM.KindCD " & vbCrLf & _
                                                " WHERE CIT.cistatuscd='" & CI_STATUS_KIKI_RIYOUCHU & "' AND CBT.UsrID = :UsrID " & vbCrLf & _
                                                " ORDER BY CBT.CINmb "

    'CI番号取得SQL
    Private strSelectCIInfoSql As String = " SELECT " & vbCrLf & _
                                                        " Class1 || '-' || Class2 || '-' || CINM " & vbCrLf & _
                                                    " FROM " & vbCrLf & _
                                                        " ci_info_tb " & vbCrLf & _
                                                    " WHERE CINmb = :CINmb"


    ''' <summary>
    ''' CI番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrKindNum">[IN]種別＋番号検索条件</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI番号取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectCINmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrKindNum As String) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCINmbSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            strSql &= "WHERE KindCD || Num IN (" & StrKindNum & ")"

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

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
    ''' レンタル機器情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrCINmb">[IN]CI番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>レンタル機器情報取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectRentalKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrCINmb As String) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strRentalKikiSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = Integer.Parse(StrCINmb)

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
    ''' 部所有機器情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrCINmb">[IN]CI番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>部所有機器情報取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectByuKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrCINmb As String) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strByuKikiSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = Integer.Parse(StrCINmb)

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
    ''' 部所有機器情報取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="StrUserID">[IN]ユーザID</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>部所有機器情報取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectAiteSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal StrUserID As String) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectAiteSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("UsrID").Value = StrUserID

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
    ''' CI番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="IntSysNmb">[IN]システム番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI番号取得用SQLし、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/08 y.ikushima
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SelectCIInfoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal IntSysNmb As Integer) As Boolean

        'CI共通情報テーブル取得用SQLを設定
        Dim strSql As String = strSelectCIInfoSql

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CINmb").Value = IntSysNmb
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
End Class
