Imports Npgsql
Imports Common
Imports System.Text
Imports System.Text.RegularExpressions
Imports CommonHBK

''' <summary>
''' 共通検索一覧画面Sqlクラス
''' </summary>
''' <remarks>共通検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/05/31 kuga
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0101

    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const FORMAT_NUM As Integer = 5 '番号フォーマット（00000）

    'CI種別取得SQL
    Private strSqlList As String = _
            " SELECT A.CIKbnCD, A.CIKbnNM" & _
            " FROM CI_KIND_MTB AS A " & _
            " WHERE A.JtiFlg <> '1'"

    'グループ名取得SQL
    Private strSqlGroup As String = _
            " SELECT A.GroupCD, A.GroupNM" & _
            " FROM GRP_MTB AS A" & _
            " WHERE A.JtiFlg <> '1'"

    ''その他SQL
    'Private strSqlSearchDefault As String = _
    '   " SELECT  G.kindnm, A.Num, A.Class1,  A.Class2, A.CINM, " & _
    '   " F.CIStateNM, A.CINaiyo, TO_CHAR(A.UpdateDT,'yyyy/mm/dd hh24:mi') AS UpdateDT, B.HBKUsrNM, " & _
    '   " C.GroupNM,A.CINmb,G.Sort AS KindSort,A.CIKbnCD " & _
    '   " FROM CI_INFO_TB AS A " & _
    '   " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
    '   " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
    '   " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
    '   " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
    '   " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
    '   " WHERE " & _
    '   " COALESCE(B.JtiFlg,'0') <> '1' " & _
    '   " AND COALESCE(C.JtiFlg,'0') <> '1' " & _
    '   " AND COALESCE(E.JtiFlg,'0') <> '1' " & _
    '   " AND COALESCE(F.JtiFlg,'0') <> '1' " & _
    '   " AND COALESCE(G.JtiFlg,'0') <> '1' "

    'その他SQL
    Private strSqlSearchDefault As String = _
       " SELECT  G.kindnm, A.Num, A.Class1,  A.Class2, A.CINM, " & _
       " F.CIStateNM, A.CINaiyo, TO_CHAR(A.UpdateDT,'yyyy/mm/dd hh24:mi') AS UpdateDT, B.HBKUsrNM, " & _
       " C.GroupNM,A.CINmb,G.Sort AS KindSort,A.CIKbnCD " & _
       " FROM CI_INFO_TB AS A " & _
       " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
       " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
       " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
       " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
       " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
       " WHERE 1 = 1  "


    ''文書用SQL
    'Private strSqlSearchDoc As String = _
    '  " SELECT G.KindNM, A.Num, A.Class1, " & _
    '  " A.Class2, A.CINM, " & _
    '  " F.CIStateNM, A.CINaiyo, TO_CHAR(A.UpdateDT,'yyyy/mm/dd hh24:mi') AS UpdateDT, B.HBKUsrNM, " & _
    '  " C.GroupNM, D.ShareteamNM, A.CINmb, CASE COALESCE(D.FileMngNmb,0) WHEN 0 THEN 'False' ELSE 'True' END ExistsFile, " & _
    '  " G.Sort AS KindSort, H.FilePath || E'\\' || H.FileNM || H.Ext AS FilePath, A.CIKbnCD " & _
    '  " FROM CI_INFO_TB AS A " & _
    '  " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
    '  " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
    '  " LEFT OUTER JOIN CI_DOC_TB D  ON A.CINmb = D.CINmb " & _
    '  " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
    '  " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
    '  " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
    '  " LEFT OUTER JOIN FILE_MNG_TB H  ON D.FileMngNmb = H.FileMngNmb " & _
    '  " WHERE COALESCE(B.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(C.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(E.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(F.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(G.JtiFlg,'0') <> '1' "

    '文書用SQL
    Private strSqlSearchDoc As String = _
      " SELECT G.KindNM, A.Num, A.Class1, " & _
      " A.Class2, A.CINM, " & _
      " F.CIStateNM, A.CINaiyo, TO_CHAR(A.UpdateDT,'yyyy/mm/dd hh24:mi') AS UpdateDT, B.HBKUsrNM, " & _
      " C.GroupNM, D.ShareteamNM, A.CINmb, CASE COALESCE(D.FileMngNmb,0) WHEN 0 THEN 'False' ELSE 'True' END ExistsFile, " & _
      " G.Sort AS KindSort, H.FilePath || E'\\' || H.FileNM || H.Ext AS FilePath, A.CIKbnCD " & _
      " FROM CI_INFO_TB AS A " & _
      " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
      " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
      " LEFT OUTER JOIN CI_DOC_TB D  ON A.CINmb = D.CINmb " & _
      " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
      " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
      " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
      " LEFT OUTER JOIN FILE_MNG_TB H  ON D.FileMngNmb = H.FileMngNmb " & _
      " WHERE 1 = 1  "


    ''カウント用その他SQL
    'Private strSqlCountDefault As String = _
    '  " SELECT" & _
    '  " COUNT(*) COUNT" & _
    '  " FROM CI_INFO_TB AS A " & _
    '  " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
    '  " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
    '  " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
    '  " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
    '  " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
    '  " WHERE " & _
    '  " COALESCE(B.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(C.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(E.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(F.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(G.JtiFlg,'0') <> '1' "


    'カウント用その他SQL
    Private strSqlCountDefault As String = _
      " SELECT" & _
      " COUNT(*) COUNT" & _
      " FROM CI_INFO_TB AS A " & _
      " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
      " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
      " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
      " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
      " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
      " WHERE 1 = 1  "


    ''カウント文書用SQL
    'Private strSqlCountDoc As String = _
    '  " SELECT" & _
    '  "   COUNT(*) COUNT" & _
    '  " FROM CI_INFO_TB AS A " & _
    '  " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
    '  " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
    '  " LEFT OUTER JOIN CI_DOC_TB D  ON A.CINmb = D.CINmb " & _
    '  " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
    '  " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
    '  " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
    '  " LEFT OUTER JOIN FILE_MNG_TB H  ON D.FileMngNmb = H.FileMngNmb " & _
    '  " WHERE COALESCE(B.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(C.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(E.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(F.JtiFlg,'0') <> '1' " & _
    '  " AND COALESCE(G.JtiFlg,'0') <> '1' "

    'カウント文書用SQL
    Private strSqlCountDoc As String = _
      " SELECT" & _
      "   COUNT(*) COUNT" & _
      " FROM CI_INFO_TB AS A " & _
      " LEFT OUTER JOIN HBKUSR_MTB B  ON A.UpdateID = B.HBKUsrID " & _
      " LEFT OUTER JOIN GRP_MTB C  ON A.CIOwnerCD = C.GroupCD " & _
      " LEFT OUTER JOIN CI_DOC_TB D  ON A.CINmb = D.CINmb " & _
      " LEFT OUTER JOIN CI_KIND_MTB E  ON A.CIKbnCD = E.CIKbnCD " & _
      " LEFT OUTER JOIN CISTATE_MTB F  ON A.CIStatusCD = F.CIStateCD " & _
      " LEFT OUTER JOIN KIND_MTB G  ON A.KindCD = G.kindcd " & _
      " LEFT OUTER JOIN FILE_MNG_TB H  ON D.FileMngNmb = H.FileMngNmb " & _
      " WHERE 1 = 1  "


    ''' <summary>
    ''' CIオーナーコンボボックスへ値を格納
    ''' <paramref name="dataTable">[OUT]取得したデータを格納するコンボボックス</paramref>
    ''' </summary>
    ''' <returns>boolean  取得状況 　true  該当種別名取得  false  取得データなし</returns>
    ''' <remarks>グループマスターから種別名を取得する
    ''' <para>作成情報：2012/06/01 Kuga
    ''' </para></remarks>
    Public Function SelectCiOwner(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal DataHBKB0101 As DataHBKB0101) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            '変数の宣言
            Dim strSQL As String = ""
            Dim strOrder As String = ""

            'ORDER BY句設定
            strOrder = " ORDER BY A.Sort ASC "

            'SQL文作成
            strSQL &= strSqlGroup & strOrder

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

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
    ''' 検索結果一覧作成
    ''' <paramref name="table">[OUT]取得したデータを格納するspread</paramref>
    ''' </summary>
    ''' <returns>boolean  取得状況 　true  該当検索結果取得  false  取得データなし</returns>
    ''' <remarks>検索一覧を取得する
    ''' <para>作成情報：2012/06/01 abe
    ''' </para></remarks>
    Public Function SelectSearchList(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal DataHBKB0101 As DataHBKB0101) As Boolean

        With DataHBKB0101

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            Try
                ' 一覧取得SQL
                Dim sbSql As New StringBuilder
                Dim strArry() As String

                Dim strCIType As String = Trim(.PropLstCiClassCD.SelectedValue)
                If (.PropCmbClassCD.SelectedValue Is DBNull.Value) Then
                    .PropCmbClassCD.SelectedValue = ""
                End If
                Dim strType As String = Trim(.PropCmbClassCD.SelectedValue)
                Dim strNo As String = Trim(.PropTxtNumberCD.Text)
                If strNo <> "" Then
                    'サポセンまたは部所有機器の場合は番号0埋め
                    Select Case .PropStrCiKbnCD_Search
                        Case CI_TYPE_SUPORT
                            strNo = strNo.PadLeft(FORMAT_NUM, "0"c)
                        Case CI_TYPE_KIKI
                            strNo = strNo.PadLeft(FORMAT_NUM, "0"c)
                    End Select
                End If

                If (.PropCmbStatusCD.SelectedValue Is DBNull.Value) Then
                    .PropCmbStatusCD.SelectedValue = ""
                End If
                Dim strStatus As String = Trim(.PropCmbStatusCD.SelectedValue)
                If (.PropCmbCiOwnerCD.SelectedValue Is DBNull.Value) Then
                    .PropCmbCiOwnerCD.SelectedValue = ""
                End If
                Dim strCIOner As String = Trim(.PropCmbCiOwnerCD.SelectedValue)
                Dim strBunrui1 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropTxtCategory1CD.Text))
                Dim strBunrui2 As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropTxtCategory2CD.Text))
                Dim strName As String = commonLogicHBK.ChangeStringForSearch(Trim(.PropTxtNameCD.Text))
                Dim strFreeword As String = Trim(.PropTxtFreeWordCD.Text)
                Dim strUpdateFrom As String = Trim(.PropDtpStartDT.txtDate.Text)
                Dim strUpdateTo As String = Trim(.PropDtpEndDT.txtDate.Text)
                Dim strFreeText As String = Trim(.PropTxtFreeTextCD.Text)
                Dim strFreeFlg1 As String = Trim(.PropCmbFreeFlag1CD.SelectedValue)
                Dim strFreeFlg2 As String = Trim(.PropCmbFreeFlag2CD.SelectedValue)
                Dim strFreeFlg3 As String = Trim(.PropCmbFreeFlag3CD.SelectedValue)
                Dim strFreeFlg4 As String = Trim(.PropCmbFreeFlag4CD.SelectedValue)
                Dim strFreeFlg5 As String = Trim(.PropCmbFreeFlag5CD.SelectedValue)
                Dim strDocAdd As String = Trim(.PropTxtDocCD.Text)

                If (strCIType = CommonDeclareHBK.CI_TYPE_DOC) Then
                    If (DataHBKB0101.PropCount = "COUNT") Then
                        sbSql.Append(strSqlCountDoc)
                    Else
                        sbSql.Append(strSqlSearchDoc)
                    End If
                Else
                    If (DataHBKB0101.PropCount = "COUNT") Then
                        sbSql.Append(strSqlCountDefault)
                    Else
                        sbSql.Append(strSqlSearchDefault)
                    End If
                End If

                If Not (strCIType = "") Then
                    sbSql.Append(" AND A.CIKbnCD = :CIKbnCD")
                End If

                If Not (strType = "") Then
                    sbSql.Append(" AND A.KindCD = :KindCD")
                End If

                If Not (strNo = "") Then
                    sbSql.Append(" AND A.Num = :Num")
                End If

                If Not (strCIOner = "") Then
                    sbSql.Append(" AND A.CIOwnerCD = :CIOwnerCD")
                End If

                If Not (strStatus = "") Then
                    sbSql.Append(" AND A.CIStatusCD = :CIStatusCD")
                End If

                If Not (strBunrui1 = "") Then
                    sbSql.Append(" AND A.Class1Aimai like :Class1")
                End If

                If Not (strBunrui2 = "") Then
                    sbSql.Append(" AND A.Class2Aimai like :Class2")
                End If

                If Not (strName = "") Then
                    sbSql.Append(" AND A.CINMAimai like :CINM")
                End If

                'フリーワード
                If Not (strFreeword = "") Then
                    CreateSqlFreeWord(Adapter, Cn, strFreeword, sbSql)
                End If

                'フリーテキスト
                If Not (strFreeText = "") Then
                    CreateSqlFreeText(Adapter, Cn, strFreeText, sbSql)
                End If

                '最終更新日　FROM TO
                sbSql.Append(" AND TO_CHAR(A.UpdateDT,'yyyy/mm/dd') BETWEEN")

                If Not (strUpdateFrom = "") Then
                    sbSql.Append("  :UpdateDTFrom AND ")
                Else
                    sbSql.Append(" '0001/01/01' AND ")
                End If

                If Not (strUpdateTo = "") Then
                    sbSql.Append(" :UpdateDTTo ")
                Else
                    sbSql.Append(" '9999/12/31' ")
                End If

                If Not (strFreeFlg1 = "") Then
                    sbSql.Append(" AND A.FreeFlg1 = :FreeFlg1")
                End If

                If Not (strFreeFlg2 = "") Then
                    sbSql.Append(" AND A.FreeFlg2 = :FreeFlg2")
                End If

                If Not (strFreeFlg3 = "") Then
                    sbSql.Append(" AND A.FreeFlg3 = :FreeFlg3")
                End If

                If Not (strFreeFlg4 = "") Then
                    sbSql.Append(" AND A.FreeFlg4 = :FreeFlg4")
                End If

                If Not (strFreeFlg5 = "") Then
                    sbSql.Append(" AND A.FreeFlg5 = :FreeFlg5")
                End If

                '文書配布先
                If Not (strDocAdd = "") Then
                    If (strCIType = CommonDeclareHBK.CI_TYPE_DOC) Then
                        CreateSqlDocAdd(Adapter, Cn, strDocAdd, sbSql)
                    Else
                        strDocAdd = ""
                    End If
                End If

                If Not (DataHBKB0101.PropCount = "COUNT") Then
                    If (strCIType = CommonDeclareHBK.CI_TYPE_SYSTEM) Then

                        sbSql.Append(" ORDER BY A.Sort ASC ")

                    ElseIf (strCIType = CommonDeclareHBK.CI_TYPE_DOC) Then
                        sbSql.Append(" ORDER BY A.Class1 ASC,A.Class2 ASC,CINM ASC ")

                    ElseIf (strCIType = CommonDeclareHBK.CI_TYPE_SUPORT) Then
                        sbSql.Append(" ORDER BY G.Sort ASC,A.Num ASC ")

                    ElseIf (strCIType = CommonDeclareHBK.CI_TYPE_KIKI) Then
                        sbSql.Append(" ORDER BY G.Sort ASC,A.Num ASC ")

                    End If
                End If

                'データアダプタに、SQLのSELECT文を設定
                Adapter.SelectCommand = New NpgsqlCommand(sbSql.ToString(), Cn)

                'パラメータ設定
                If strCIType <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIKbnCD").Value = strCIType
                End If

                If strType <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("KindCD").Value = strType
                End If

                If strNo <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Num").Value = strNo
                End If

                If strStatus <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIStatusCD").Value = strStatus
                End If

                If strCIOner <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIOwnerCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CIOwnerCD").Value = strCIOner
                End If

                If strBunrui1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Class1").Value = "%" & strBunrui1 & "%"
                End If

                If strBunrui2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("Class2").Value = "%" & strBunrui2 & "%"
                End If

                If strName <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("CINM").Value = "%" & strName & "%"
                End If

                If strFreeword <> "" Then
                    strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strFreeword, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strArry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeWord" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeWord" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArry(loopIndex).ToString) & "%"
                    Next
                End If

                If strUpdateFrom <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTFrom", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTFrom").Value = strUpdateFrom
                End If

                If strUpdateTo <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UpdateDTTo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UpdateDTTo").Value = strUpdateTo
                End If

                If strFreeText <> "" Then
                    strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strFreeText, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strArry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeText" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("FreeText" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArry(loopIndex).ToString) & "%"
                    Next
                End If

                If strFreeFlg1 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg1").Value = strFreeFlg1
                End If

                If strFreeFlg2 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg2").Value = strFreeFlg2
                End If

                If strFreeFlg3 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg3").Value = strFreeFlg3
                End If

                If strFreeFlg4 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg4").Value = strFreeFlg4
                End If

                If strFreeFlg5 <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("FreeFlg5").Value = strFreeFlg5
                End If

                If strDocAdd <> "" Then
                    strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strDocAdd, CommonDeclareHBK.SPLIT_MODE_AND)
                    For loopIndex As Integer = 0 To strArry.Length - 1 Step 1
                        Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("ShareteamNM" & loopIndex, NpgsqlTypes.NpgsqlDbType.Varchar))
                        Adapter.SelectCommand.Parameters("ShareteamNM" & loopIndex).Value = "%" & commonLogicHBK.ChangeStringForSearch(strArry(loopIndex).ToString) & "%"
                    Next
                End If

                '終了ログ出力
                CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

                '正常値を返す
                Return True

            Catch ex As Exception
                '例外発生
                CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
                puErrMsg = HBK_E001 & ex.Message
                Return False
            End Try

        End With

    End Function

    Private Function CreateSqlFreeWord(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal strSearch As String, ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim commonLogicHBK As New CommonLogicHBK

        Dim strArry() As String

        Try
            ' 検索文字列の分割
            strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)


            For loopIndex As Integer = 0 To strArry.Length - 1 Step 1
                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If
                sbSql.Append(" A.FreeWordAimai ")
                sbSql.Append(" like ").Append(":FreeWord" & loopIndex)
            Next

            If strArry.Length > 0 Then
                sbSql.Append(" ) ")
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function


    Private Function CreateSqlFreeText(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal strSearch As String, ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim commonLogicHBK As New CommonLogicHBK

        Dim strArry() As String

        Try
            ' 検索文字列の分割
            strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)

            For loopIndex As Integer = 0 To strArry.Length - 1 Step 1
                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If
                sbSql.Append(" A.BikoAimai ")
                sbSql.Append(" like ").Append(":FreeText" & loopIndex)
            Next
            If (strArry.Length > 0) Then
                sbSql.Append(" ) ")
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function

    Private Function CreateSqlDocAdd(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal strSearch As String, ByVal sbSql As StringBuilder) As StringBuilder

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Dim strArry() As String

        Try
            ' 検索文字列の分割
            strArry = CommonHBK.CommonLogicHBK.GetSearchStringList(strSearch, CommonDeclareHBK.SPLIT_MODE_AND)

            For loopIndex As Integer = 0 To strArry.Length - 1 Step 1

                If (loopIndex = 0) Then
                    sbSql.Append(" AND ( ")
                Else
                    sbSql.Append(" AND ")
                End If

                sbSql.Append(" D.ShareteamNMAimai ")
                sbSql.Append(" like ").Append(":ShareteamNM" & loopIndex)

            Next

            If strArry.Length > 0 Then
                sbSql.Append(" ) ")
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)
            Return sbSql

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return sbSql
        End Try

    End Function

End Class

