Imports Npgsql
Imports Common
Imports System.Text
Imports CommonHBK
Public Class SqlHBKZ0201

    'エンドユーザー初期表示SQL
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 START
    'Dim strSqlLoad As String = _
    '    "SELECT FALSE 選択,A.EndUsrID,A.EndUsrCompany,A.EndUsrBusyoNM,A.EndUsrNM,A.EndUsrMailAdd, " & _
    '    "A.EndUsrTel,A.EndUsrContact FROM ENDUSR_MTB A " & _
    '    "WHERE NOT EXISTS(SELECT '1' FROM ENDUSR_MTB AS B WHERE " & _
    '    "(B.JtiFlg = '1' OR (B.UsrKbn = '0' AND B.EndUsrRetireDT <= TO_CHAR(now(),'YYYYMMDD') ) " & _
    '    "OR (B.UsrKbn = '1' AND B.EndUsrActCtrlFlg = '0')) AND B.EndUsrID = A.EndUsrID) "
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 END

    'エンドユーザー初期表示結果件数取得SQL
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 START
    'Dim strSqlLoadCount As String = _
    '    "SELECT COUNT(*) COUNT " & _
    '    "FROM ENDUSR_MTB A WHERE NOT EXISTS(SELECT '1' " & _
    '    "FROM ENDUSR_MTB AS B WHERE " & _
    '    "(B.JtiFlg = '1' OR (B.UsrKbn = '0' AND B.EndUsrRetireDT <= TO_CHAR(now(),'YYYYMMDD') ) " & _
    '    "OR (B.UsrKbn = '1' AND B.EndUsrActCtrlFlg = '0')) AND B.EndUsrID = A.EndUsrID) "

    Dim strSqlLoadCount As String = _
    "SELECT COUNT(*) COUNT " & _
    "FROM ENDUSR_MTB A WHERE 1 = 1 "
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 END

    'エンドユーザー検索SQL
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 START
    'Dim strSqlSearch As String = _
    '    "SELECT FALSE 選択,A.EndUsrID,A.EndUsrCompany,A.EndUsrBusyoNM,A.EndUsrNM, " & _
    '    "A.EndUsrMailAdd,A.EndUsrTel,A.EndUsrContact FROM ENDUSR_MTB A " & _
    '    "WHERE NOT EXISTS(SELECT '1' FROM ENDUSR_MTB AS B WHERE (B.JtiFlg = '1' OR " & _
    '    "(B.UsrKbn = '0' AND B.EndUsrRetireDT <= TO_CHAR(now(),'YYYYMMDD') ) OR " & _
    '    "(B.UsrKbn = '1' AND B.EndUsrActCtrlFlg = '0')) AND B.EndUsrID = A.EndUsrID) "

    Dim strSqlSearch As String = _
    "SELECT FALSE AS Check,A.EndUsrID,A.EndUsrCompany,A.EndUsrBusyoNM,A.EndUsrNM,A.EndUsrMailAdd,A.StateNaiyo, " & _
    "A.EndUsrTel,A.EndUsrMailAdd AS Conect,CASE WHEN A.StateNaiyo LIKE '%削除%' THEN 1 ELSE 0 END AS JtiFlg_Sort,A.EndUsrNMkana FROM ENDUSR_MTB A " & _
    "WHERE 1 = 1 "
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 END

    'エンドユーザー検索結果件数取得SQL
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 START
    'Dim strSqlSearchCOUNT As String = _
    '"SELECT COUNT(*) COUNT  " & _
    '" FROM ENDUSR_MTB A WHERE NOT EXISTS(SELECT '1' FROM ENDUSR_MTB AS B WHERE " & _
    '"(B.JtiFlg = '1' OR (B.UsrKbn = '0' AND B.EndUsrRetireDT <= TO_CHAR(now(),'YYYYMMDD') ) OR " & _
    '"(B.UsrKbn = '1' AND B.EndUsrActCtrlFlg = '0')) AND B.EndUsrID = A.EndUsrID) "

    Dim strSqlSearchCOUNT As String = _
"SELECT COUNT(*) COUNT  " & _
" FROM ENDUSR_MTB A WHERE 1 = 1 "
    '[mod] y.ikushima 2012/09/08 エンドユーザテーブル定義変更 END

    ''' <summary>
    ''' 初期表示用検索結果件数の取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0201">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスターから、引数で渡された値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/28 abe
    ''' </para></remarks>
    Public Function setEndUsr_Load(ByRef Adapter As NpgsqlDataAdapter, _
                                  ByVal Cn As NpgsqlConnection, _
                                  ByVal DataHBKZ0201 As DataHBKZ0201) As Boolean

        Try

            Dim table As New DataTable()
            Dim strArry As String()
            ' 一覧取得SQL
            Dim sbSql As New StringBuilder
            Dim commonLogicHBK As New CommonLogicHBK
            Dim strTxtSearch As String = Trim(DataHBKZ0201.PropArgs.ToString)
            Dim param As New List(Of NpgsqlParameter)

            CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)

            strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strTxtSearch, DataHBKZ0201.PropSplitMode)

            If (DataHBKZ0201.PropSplitMode = CommonDeclareHBK.SPLIT_MODE_ONE Or strArry.Length = 0) Then '単一検索

                ' パラメータの作成
                param.Add(New NpgsqlParameter("param" & "0", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(0).Value = "%" & commonLogicHBK.ChangeStringForSearch(strTxtSearch) & "%"

            Else

                Dim loopCount As Integer = strArry.Length - 1

                For loopIndex As Integer = 0 To loopCount Step 1

                    ' パラメータの作成
                    param.Add(New NpgsqlParameter("param" & loopIndex.ToString, NpgsqlTypes.NpgsqlDbType.Varchar))
                    param(loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(Trim(strArry(loopIndex).ToString)) & "%"

                Next

            End If

            If (DataHBKZ0201.PropCount = "COUNT") Then
                sbSql.Append(strSqlLoadCount)
            Else
                sbSql.Append(strSqlSearch)
            End If

            'あいまい検索セット
            sbSql = CreateSqlLoad(strArry.Length - 1, strTxtSearch, sbSql, DataHBKZ0201)

            If Not (DataHBKZ0201.PropCount = "COUNT") Then
                sbSql.Append(" ORDER BY JtiFlg_Sort, A.Sort ASC ")
            End If

            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray())


            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

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
    ''' 検索の件数取得SQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0201">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスターから、フォームから渡される値をもとに検索を行った結果の件数を取得するSQL
    ''' <para>作成情報：2012/06/28 abe
    ''' </para></remarks>
    Public Function setEndUsr_Search(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal DataHBKZ0201 As DataHBKZ0201) As Boolean

        Try

            Dim table As New DataTable()
            Dim param As New List(Of NpgsqlParameter)
            Dim commonLogicHBK As New CommonLogicHBK

            ' 一覧取得SQL
            Dim sbSql As New StringBuilder
            Dim strBusyo As String = CommonLogicHBK.ChangeStringForSearch(Trim(DataHBKZ0201.PropTxtBusyoName.text))
            Dim strId As String = CommonLogicHBK.ChangeStringForSearch(Trim(DataHBKZ0201.PropTxtEndUsrId.text))
            Dim strName As String = CommonLogicHBK.ChangeStringForSearch(Trim(DataHBKZ0201.PropTxtEndUsrNm.text))
            Dim strMail As String = CommonLogicHBK.ChangeStringForSearch(Trim(DataHBKZ0201.PropTxtEndUsrMail.text))

            CommonLogic.WriteLog(LogLevel.None, "START", Nothing, Nothing)


            If (DataHBKZ0201.PropCount = "COUNT") Then
                sbSql.Append(strSqlSearchCOUNT)
            Else
                sbSql.Append(strSqlSearch)
            End If

            If Not (strBusyo = String.Empty) Then
                sbSql.Append(" AND A.EndUsrBusyoNMAimai like :EndUsrBusyoNM")
                'sbSql.Append(" AND A.EndUsrBusyoNM like :EndUsrBusyoNM")

                ' パラメータの作成
                param.Add(New NpgsqlParameter("EndUsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(param.ToArray.Length - 1).Value = "%" & strBusyo & "%"

            End If

            If Not (strId = String.Empty) Then
                sbSql.Append(" AND A.EndUsrAimai like :EndUsrID")

                ' パラメータの作成
                param.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(param.ToArray.Length - 1).Value = "%" & strId & "%"

            End If

            If Not (strName = String.Empty) Then
                sbSql.Append(" AND A.EndUsrNMAimai like :EndUsrNM")
                'sbSql.Append(" AND A.EndUsrNM like :EndUsrNM")

                ' パラメータの作成
                param.Add(New NpgsqlParameter("EndUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param.ToArray.Length.ToString()
                param(param.ToArray.Length - 1).Value = "%" & strName & "%"

            End If

            If Not (strMail = String.Empty) Then
                sbSql.Append(" AND A.EndUsrMailAdd like :EndUsrMailAdd")

                ' パラメータの作成
                param.Add(New NpgsqlParameter("EndUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(param.ToArray.Length - 1).Value = "%" & strMail & "%"


            End If

            If Not (DataHBKZ0201.PropCount = "COUNT") Then
                sbSql.Append(" ORDER BY JtiFlg_Sort, A.Sort ASC ")
            End If


            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray())


            CommonLogic.WriteLog(LogLevel.None, "END", Nothing, Nothing)

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
    ''' 初期表示用検索のSQLの設定
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0201">[IN]データクラス</param>
    ''' </summary> 
    ''' <returns>StringBuilder</returns>
    ''' <remarks>初期画面表示時のあいまい検索を行うSQLを作成する
    ''' <para>作成情報：2012/06/28 abe
    ''' </para></remarks>
    Private Function CreateSqlLoad(ByVal loopCount As Integer, ByVal strSearch As String, ByVal sbSql As StringBuilder, ByVal DataHBKZ0201 As DataHBKZ0201) As StringBuilder

        Dim strArry() As String

        ' 検索文字列の分割
        strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, DataHBKZ0201.PropSplitMode)



        Dim strSqlAndOr As String = ""

        If (DataHBKZ0201.PropSplitMode = CommonDeclareHBK.SPLIT_MODE_AND) Then 'AND検索
            strSqlAndOr = " AND "
        ElseIf (DataHBKZ0201.PropSplitMode = CommonDeclareHBK.SPLIT_MODE_OR) Then 'OR検索
            strSqlAndOr = " OR "
        End If

        For loopIndex As Integer = 0 To strArry.Length - 1 Step 1

            If (loopIndex = 0) Then
                sbSql.Append(" AND ( ")
            Else
                sbSql.Append(strSqlAndOr)
            End If

            sbSql.Append(" A.EndusrAimai ")
            'sbSql.Append(" ( COALESCE(A.EndUsrID,'') || COALESCE(A.EndUsrBusyoNM,'') || COALESCE(A.EndUsrNM,'') || COALESCE(A.EndUsrMailAdd,'') ")
            sbSql.Append(" like ").Append(":param" & loopIndex.ToString)

        Next

        If strArry.Length > 0 Then
            sbSql.Append(" ) ")
        End If

        Return sbSql

    End Function

 End Class
