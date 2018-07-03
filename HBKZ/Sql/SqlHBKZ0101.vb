Imports Common
Imports System.Text
Imports Npgsql
Imports CommonHBK

''' <summary>
''' ひびきユーザー検索画面Sqlクラス
''' </summary>
''' <remarks>ひびきユーザー検索画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/04 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKZ0101

    Dim commonLogicHBK As New CommonLogicHBK


    'ひびきユーザー検索SQL
    'Private strSelectHbkUsrSearch As String = "SELECT" & _
    '                                          " m01.HBKUsrID " & _
    '                                          " ,m03.GroupNM " & _
    '                                          " ,m01.HBKUsrNM " & _
    '                                          " ,m02.GroupCD " & _
    '                                          "FROM " & _
    '                                          "  ( HBKUSR_MTB AS m01 LEFT OUTER JOIN SZK_MTB As m02 ON(m01.HBKUsrID = m02.HBKUsrID) ) " & _
    '                                          "                      LEFT OUTER JOIN GRP_MTB As m03 ON(m02.GroupCD = m03.GroupCD) " & _
    '                                          "WHERE" & _
    '                                          " COALESCE(m01.JtiFlg, '0') <> '1' " & _
    '                                          " AND COALESCE(m02.JtiFlg, '0') <> '1' " & _
    '                                          " AND COALESCE(m03.JtiFlg, '0') <> '1' "
    Private strSelectHbkUsrSearch As String = "SELECT" & _
                                          " m01.HBKUsrID " & _
                                          " ,m03.GroupNM " & _
                                          " ,m01.HBKUsrNM " & _
                                          " ,m02.GroupCD " & _
                                          " ,CASE WHEN m02.JtiFlg = '" & JTIFLG_ON & "' THEN '" & DELDATA_DISPLAY_NM & "' ELSE '' END AS JtiFlg " & _
                                          " ,CASE WHEN m02.JtiFlg = '" & JTIFLG_ON & "' THEN 1 ELSE 0 END AS JtiFlg_Sort " & _
                                          "FROM " & _
                                          "  ( HBKUSR_MTB AS m01 LEFT OUTER JOIN SZK_MTB As m02 ON(m01.HBKUsrID = m02.HBKUsrID) ) " & _
                                          "                      LEFT OUTER JOIN GRP_MTB As m03 ON(m02.GroupCD = m03.GroupCD) " & _
                                          " WHERE 1 = 1"

    'ひびきユーザー検索結果件数取得SQL
    'Private strSelectCountHbkUsr As String = "SELECT" & _
    '                                         " COUNT(*) " & _
    '                                         "FROM " & _
    '                                         "  ( HBKUSR_MTB AS m01 LEFT OUTER JOIN SZK_MTB As m02 ON(m01.HBKUsrID = m02.HBKUsrID) ) " & _
    '                                         "                      LEFT OUTER JOIN GRP_MTB As m03 ON(m02.GroupCD = m03.GroupCD) " & _
    '                                         "WHERE" & _
    '                                         " COALESCE(m01.JtiFlg, '0') <> '1' " & _
    '                                         " AND COALESCE(m02.JtiFlg, '0') <> '1' " & _
    '                                         " AND COALESCE(m03.JtiFlg, '0') <> '1' "
    Private strSelectCountHbkUsr As String = "SELECT" & _
                                         " COUNT(*) " & _
                                         "FROM " & _
                                         "  ( HBKUSR_MTB AS m01 LEFT OUTER JOIN SZK_MTB As m02 ON(m01.HBKUsrID = m02.HBKUsrID) ) " & _
                                         "                      LEFT OUTER JOIN GRP_MTB As m03 ON(m02.GroupCD = m03.GroupCD) " & _
                                         " WHERE 1 = 1"

    '初期検索時対象カラム
    'Dim strSqlSearchColumns = "COALESCE(m01.HBKUsrAimai , '') || COALESCE(m01.HBKUsrNMAimai , '') || COALESCE(m01.HBKUsrNMKana , '') || COALESCE(m03.GroupCD , '') || COALESCE(m03.GroupNM , '')"

    ''' <summary>
    ''' 初期表示用検索結果件数の取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、引数で渡された値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectCountInitHBKUserSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim loopCount As Integer = dataHBKZ0101.PropTxtSearchStringArray.Length - 1
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(loopCount) {}

        Try

            For loopIndex As Integer = 0 To loopCount Step 1
                'SQLに渡すデータの設定
                param(loopIndex) = New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar)
                param(loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchStringArray(loopIndex)) & "%"
            Next

            'SQL文(SELECT)
            strSQL = strSelectCountHbkUsr
            'Where句作成
            If loopCount >= 0 Then

                strWhere = " AND ( m03.GroupNMAimai || m01.HBKUsrAimai LIKE :param0"
                '二件目以降
                For loopIndex As Integer = 1 To loopCount Step 1

                    If dataHBKZ0101.PropSplitMode = CommonHBK.SPLIT_MODE_OR Then
                        'OR検索
                        strWhere &= " OR "
                    ElseIf dataHBKZ0101.PropSplitMode = CommonHBK.SPLIT_MODE_AND Then
                        'AND検索
                        strWhere &= " AND "
                    End If
                    strWhere &= " m03.GroupNMAimai || m01.HBKUsrAimai LIKE :param" & loopIndex.ToString

                Next
                strWhere &= " ) "

            End If

            'データアダプタに、SQLを設定
            strSQL &= strWhere
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.AddRange(param)

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
    ''' 初期表示用検索のSQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、引数で渡された値をもとに検索を行うSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectInitHBKUserSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim loopCount As Integer = dataHBKZ0101.PropTxtSearchStringArray.Length - 1
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(loopCount) {}

        Try

            For loopIndex As Integer = 0 To loopCount Step 1
                'SQLに渡すデータの設定
                param(loopIndex) = New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar)
                param(loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchStringArray(loopIndex)) & "%"
            Next

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrSearch
            'Where句作成
            If loopCount >= 0 Then

                strWhere = " AND ( m03.GroupNMAimai || m01.HBKUsrAimai LIKE :param0"
                '二件目以降
                For loopIndex As Integer = 1 To loopCount Step 1

                    If dataHBKZ0101.PropSplitMode = CommonHBK.SPLIT_MODE_OR Then
                        'OR検索
                        strWhere &= " OR "
                    ElseIf dataHBKZ0101.PropSplitMode = CommonHBK.SPLIT_MODE_AND Then
                        'AND検索
                        strWhere &= " AND "
                    End If
                    strWhere &= " m03.GroupNMAimai || m01.HBKUsrAimai LIKE :param" & loopIndex.ToString

                Next
                strWhere &= " ) "

            End If

            'データアダプタに、SQLを設定
            'strSQL &= strWhere & " ORDER BY m01.Sort ASC , m02.Sort ASC "
            strSQL &= strWhere & " ORDER BY JtiFlg_Sort ASC , m03.Sort ASC , m01.HBKUsrNmKana ASC "
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.AddRange(param)

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
    ''' 検索の件数取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectCountHBKUserSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(3) {}

        Try

            'SQLに渡すデータの設定
            param(0) = New NpgsqlParameter("setUserId", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(1) = New NpgsqlParameter("setUserName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(2) = New NpgsqlParameter("setGroupCd", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(3) = New NpgsqlParameter("setGroupName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(0).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchUserID.Text) & "%"
            param(1).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchUserName.Text) & "%"
            param(2).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchGroupCD.Text) & "%"
            param(3).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchGroupName.Text) & "%"

            'SQL文(SELECT)
            strSQL = strSelectCountHbkUsr
            'Where句作成
            If dataHBKZ0101.PropTxtSearchUserID.Text <> System.String.Empty Then
                'ユーザーIDが入力されている
                strWhere &= " AND m01.HBKUsrAimai LIKE :setUserId "
            End If
            If dataHBKZ0101.PropTxtSearchUserName.Text <> System.String.Empty Then
                'ユーザー名が入力されている
                strWhere &= " AND m01.HBKUsrNM LIKE :setUserName "
            End If
            If dataHBKZ0101.PropTxtSearchGroupCD.Text <> System.String.Empty Then
                'グループCDが入力されている
                strWhere &= " AND m02.GroupCD LIKE :setGroupCd "
            End If
            If dataHBKZ0101.PropTxtSearchGroupName.Text <> System.String.Empty Then
                'グループ名が入力されている
                strWhere &= " AND m03.GroupNMAimai LIKE :setGroupName "
            End If

            'データアダプタに、SQLを設定
            strSQL &= strWhere
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.AddRange(param)

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
    ''' <param name="dataHBKZ0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、フォームから渡される値をもとに検索を行うSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectHBKUserSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(3) {}

        Try

            'SQLに渡すデータの設定
            param(0) = New NpgsqlParameter("setUserId", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(1) = New NpgsqlParameter("setUserName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(2) = New NpgsqlParameter("setGroupCd", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(3) = New NpgsqlParameter("setGroupName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(0).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchUserID.Text) & "%"
            param(1).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchUserName.Text) & "%"
            param(2).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchGroupCD.Text) & "%"
            param(3).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0101.PropTxtSearchGroupName.Text) & "%"

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrSearch
            'Where句作成
            If dataHBKZ0101.PropTxtSearchUserID.Text <> System.String.Empty Then
                'ユーザーIDが入力されている
                strWhere &= " AND m01.HBKUsrAimai LIKE :setUserId "
            End If
            If dataHBKZ0101.PropTxtSearchUserName.Text <> System.String.Empty Then
                'ユーザー名が入力されている
                strWhere &= " AND m01.HBKUsrNM LIKE :setUserName "
            End If
            If dataHBKZ0101.PropTxtSearchGroupCD.Text <> System.String.Empty Then
                'グループCDが入力されている
                strWhere &= " AND m02.GroupCD LIKE :setGroupCd "
            End If
            If dataHBKZ0101.PropTxtSearchGroupName.Text <> System.String.Empty Then
                'グループ名が入力されている
                strWhere &= " AND m03.GroupNMAimai LIKE :setGroupName "
            End If

            'データアダプタに、SQLを設定
            'strSQL &= strWhere & " ORDER BY m01.Sort ASC , m02.Sort ASC "
            strSQL &= strWhere & " ORDER BY JtiFlg_Sort ASC , m03.Sort ASC , m01.HBKUsrNmKana ASC "
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.AddRange(param)

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
    ''' 初期表示用検索のSQLの設定_作業履歴用
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0101">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスターから、PropDataTableで渡された値をもとに検索を行うSQL
    ''' <para>作成情報：2012/09/04 r.hoshino
    ''' </para></remarks>
    Public Function SetSelectInitHBKUserSearch_initMode1Sql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0101 As DataHBKZ0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim loopCount As Integer = dataHBKZ0101.PropDataTable.Rows.Count - 1
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(loopCount) {}

        Try

            For loopIndex As Integer = 0 To loopCount Step 1
                'SQLに渡すデータの設定
                param(loopIndex) = New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar)
                param(loopIndex).Value = dataHBKZ0101.PropDataTable.Rows(loopIndex).Item("グループID") & "|" & _
                    dataHBKZ0101.PropDataTable.Rows(loopIndex).Item("ユーザーID") & "|" & _
                dataHBKZ0101.PropDataTable.Rows(loopIndex).Item("グループ名") & "|" & _
                dataHBKZ0101.PropDataTable.Rows(loopIndex).Item("ユーザー氏名")
            Next

            'SQL文(SELECT)
            strSQL = strSelectHbkUsrSearch
            'Where句作成
            If loopCount >= 0 Then

                strWhere = " AND (( m03.GroupCd ||'|'|| m01.HBKUsrAimai ||'|'|| m03.GroupNM ||'|'|| m01.HBKUsrNM= :param0)"
                '二件目以降
                For loopIndex As Integer = 1 To loopCount Step 1
                    strWhere &= " OR "
                    strWhere &= "( m03.GroupCd ||'|'|| m01.HBKUsrID ||'|'|| m03.GroupNM ||'|'|| m01.HBKUsrNM = :param" & loopIndex.ToString & ")"
                Next
                strWhere &= " ) "
            End If

            'データアダプタに、SQLを設定
            strSQL &= strWhere
            '& " ORDER BY JtiFlg_Sort ASC , m03.Sort ASC , m01.HBKUsrNmKana ASC "
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.AddRange(param)

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
