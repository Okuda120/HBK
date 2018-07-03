Imports System.Text
Imports Npgsql
Imports Common
Imports CommonHBK

''' <summary>
''' グループ検索画面Sqlクラス
''' </summary>
''' <remarks>グループ検索画面のSQLの作成・設定を行う
''' <para>作成情報：2012/06/04 matsuoka
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKZ0301

    Private commonLogicHBK As New CommonLogicHBK

    'グループ検索SQL
    'Private strSelectGroupSearch As String = "SELECT" & _
    '                                         " GroupCD , " & _
    '                                         " GroupNM " & _
    '                                         "FROM " & _
    '                                         " GRP_MTB " & _
    '                                         "WHERE" & _
    '                                         " JtiFlg <> '1' "
    Private strSelectGroupSearch As String = "SELECT" & _
                                         " GroupCD , " & _
                                         " GroupNM " & _
                                         " ,CASE WHEN JtiFlg = '" & JTIFLG_ON & "' THEN '" & DELDATA_DISPLAY_NM & "' ELSE '' END " & _
                                         " FROM " & _
                                         " GRP_MTB " & _
                                         "WHERE" & _
                                         " 1 = 1 "

    'グループ検索結果件数取得SQL
    'Private strSelectCountGroup As String = "SELECT" & _
    '                                        " COUNT(GroupCD) " & _
    '                                        "FROM " & _
    '                                        " GRP_MTB " & _
    '                                        "WHERE" & _
    '                                        " JtiFlg <> '1' "
    Private strSelectCountGroup As String = "SELECT" & _
                                        " COUNT(GroupCD) " & _
                                        " FROM " & _
                                        " GRP_MTB " & _
                                        "WHERE " & _
                                        " 1 = 1 "


    ''' <summary>
    ''' 初期表示用検索結果件数の取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスターから、引数で渡された値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectCountInitGroupSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim loopCount As Integer = dataHBKZ0301.PropTxtSearchStringArray.Length - 1
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(loopCount) {}

        Try

            For loopIndex As Integer = 0 To loopCount Step 1
                'SQLに渡すデータの設定
                param(loopIndex) = New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar)
                param(loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchStringArray(loopIndex)) & "%"
            Next

            'SQL文(SELECT)
            strSQL = strSelectCountGroup
            'Where句作成
            If loopCount >= 0 Then

                strWhere = " AND ( GroupNMAimai LIKE :param0"
                '二件目以降
                For loopIndex As Integer = 1 To loopCount Step 1

                    If dataHBKZ0301.PropSplitMode = CommonHBK.SPLIT_MODE_OR Then
                        'OR検索
                        strWhere &= " OR "
                    ElseIf dataHBKZ0301.PropSplitMode = CommonHBK.SPLIT_MODE_AND Then
                        'AND検索
                        strWhere &= " AND "
                    End If
                    strWhere &= " GroupNMAimai LIKE :param" & loopIndex.ToString

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
    ''' <param name="dataHBKZ0301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスターから、引数で渡された値をもとに検索を行うSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectInitGroupSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim loopCount As Integer = dataHBKZ0301.PropTxtSearchStringArray.Length - 1
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(loopCount) {}

        Try

            For loopIndex As Integer = 0 To loopCount Step 1
                'SQLに渡すデータの設定
                param(loopIndex) = New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar)
                param(loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchStringArray(loopIndex)) & "%"
            Next

            'SQL文(SELECT)
            strSQL = strSelectGroupSearch
            'Where句作成
            If loopCount >= 0 Then

                strWhere = " AND ( GroupNMAimai LIKE :param0"
                '二件目以降
                For loopIndex As Integer = 1 To loopCount Step 1

                    If dataHBKZ0301.PropSplitMode = CommonHBK.SPLIT_MODE_OR Then
                        'OR検索
                        strWhere &= " OR "
                    ElseIf dataHBKZ0301.PropSplitMode = CommonHBK.SPLIT_MODE_AND Then
                        'AND検索
                        strWhere &= " AND "
                    End If
                    strWhere &= " GroupNMAimai LIKE :param" & loopIndex.ToString

                Next
                strWhere &= " ) "

            End If

            'データアダプタに、SQLを設定
            'strSQL &= strWhere & "ORDER BY Sort ASC "
            strSQL &= strWhere & "ORDER BY JtiFlg ASC , Sort ASC "
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
    ''' <param name="dataHBKZ0301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスターから、フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectCountGroupSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(1) {}

        Try

            'SQLに渡すデータの設定
            param(0) = New NpgsqlParameter("setGroupCd", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(1) = New NpgsqlParameter("setGroupName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(0).Value = commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchGroupCD.Text)
            param(1).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchGroupName.Text) & "%"

            'SQL文(SELECT)
            strSQL = strSelectCountGroup
            'Where句作成
            If dataHBKZ0301.PropTxtSearchGroupCD.Text <> System.String.Empty Then
                'グループCDが入力されている
                strWhere &= " AND GroupCD = :setGroupCd "
            End If
            If dataHBKZ0301.PropTxtSearchGroupName.Text <> System.String.Empty Then
                'グループ名が入力されている
                strWhere &= " AND GroupNMAimai LIKE :setGroupName "
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
    ''' <param name="dataHBKZ0301">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスターから、フォームから渡される値をもとに検索を行うSQL
    ''' <para>作成情報：2012/06/14 matsuoka
    ''' </para></remarks>
    Public Function SetSelectGroupSearchSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0301 As DataHBKZ0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String
        Dim strWhere As String = ""
        Dim param As Npgsql.NpgsqlParameter() = New Npgsql.NpgsqlParameter(1) {}

        Try

            'SQLに渡すデータの設定
            param(0) = New NpgsqlParameter("setGroupCd", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(1) = New NpgsqlParameter("setGroupName", NpgsqlTypes.NpgsqlDbType.Varchar)
            param(0).Value = commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchGroupCD.Text)
            param(1).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0301.PropTxtSearchGroupName.Text) & "%"

            'SQL文(SELECT)
            strSQL = strSelectGroupSearch
            'Where句作成
            If dataHBKZ0301.PropTxtSearchGroupCD.Text <> System.String.Empty Then
                'グループCDが入力されている
                strWhere &= " AND GroupCD = :setGroupCd "
            End If
            If dataHBKZ0301.PropTxtSearchGroupName.Text <> System.String.Empty Then
                'グループ名が入力されている
                strWhere &= " AND GroupNMAimai LIKE :setGroupName "
            End If

            'データアダプタに、SQLを設定
            'strSQL &= strWhere & " ORDER BY Sort ASC "
            strSQL &= strWhere & "ORDER BY JtiFlg ASC , Sort ASC "
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
