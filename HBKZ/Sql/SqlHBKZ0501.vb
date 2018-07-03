Imports Npgsql
Imports System.Text
Imports Common
Imports CommonHBK

Public Class SqlHBKZ0501
    Private commonLogicHBK As New CommonLogicHBK
    'Private strSelectSetPosSql As String = "SELECT" & _
    '                                       "   FALSE CHK" & _
    '                                       "  ,SetKyokuNM" & _
    '                                       "  ,SetBusyoNM" & _
    '                                       "  ,SetRoom" & _
    '                                       "  ,SetBuil" & _
    '                                       "  ,SetFloor" & _
    '                                       "  ,SetBusyoCd" & _
    '                                       " FROM" & _
    '                                       "   SETPOS_MTB" & _
    '                                       " WHERE" & _
    '                                       "   COALESCE(JtiFlg,'0') <> '1'"

    'Private strSelectCountSetPosSql As String = " SELECT" & _
    '                                            "   COUNT(*) COUNT" & _
    '                                            " FROM" & _
    '                                            "   SETPOS_MTB" & _
    '                                            " WHERE" & _
    '                                            "   COALESCE(JtiFlg,'0') <> '1'"

    Private strSelectSetPosSql As String = "SELECT" & _
                                       "   FALSE CHK" & _
                                       "  ,SetKyokuNM" & _
                                       "  ,SetBusyoNM" & _
                                       "  ,SetRoom" & _
                                       "  ,SetBuil" & _
                                       "  ,SetFloor" & _
                                       "  ,SetBusyoCd" & _
                                         " ,CASE WHEN JtiFlg = '" & JTIFLG_ON & "' THEN '" & DELDATA_DISPLAY_NM & "' ELSE '' END " & _
                                       " FROM" & _
                                       "   SETPOS_MTB" & _
                                       " WHERE " & _
                                       " 1 = 1 "

    Private strSelectCountSetPosSql As String = " SELECT" & _
                                                " COUNT(*) COUNT" & _
                                                " FROM" & _
                                                "   SETPOS_MTB" & _
                                                " WHERE " & _
                                                " 1 = 1 "

    ''' <summary>
    ''' 初期表示用設置情報一覧取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">NpgsqlDataAdapter型オブジェクト</param>
    ''' <param name="Cn">NpgsqlConnection型オブジェクト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>設置情報一覧用のSQLを作成し、データアダプタにセットする
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetInitSelectSetPosSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0501 As DataHBKZ0501) As Boolean

        ' 開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Try
            ' パラメータの作成
            Dim param As New List(Of NpgsqlParameter)
            For i As Integer = 0 To dataHBKZ0501.PropBusyoArray.Length - 1
                ' パラメータの作成
                param.Add(New NpgsqlParameter("SetBusyoNM" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
                param(i).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBusyoArray(i)) & "%"
            Next

            ' SQL文作成
            Dim sb As New StringBuilder
            sb.Append(strSelectSetPosSql)

            For i As Integer = 0 To param.Count - 1
                If i = 0 Then
                    sb.Append("   AND (")
                Else
                    Select Case dataHBKZ0501.PropSplitMode
                        Case CommonHBK.CommonDeclareHBK.SPLIT_MODE_AND
                            sb.Append("   AND")
                        Case CommonHBK.CommonDeclareHBK.SPLIT_MODE_OR
                            sb.Append("   OR")
                    End Select
                End If
                sb.Append("   SetBusyoNMaimai").Append(" LIKE :").Append(param(i).ParameterName)
            Next

            If param.Count <> 0 Then
                sb.Append("   )")
            End If

            sb.Append(" ORDER BY")
            sb.Append(" JtiFlg, SetKyokuNM ,SetKyokuNM,SetBusyoNM,SetRoom,SetBuil, SetFloor")
            'sb.Append("  Sort")

            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray())

            ' 終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 設置情報一覧取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">NpgsqlDataAdapter型オブジェクト</param>
    ''' <param name="Cn">NpgsqlConnection型オブジェクト</param>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>設置情報一覧用のSQLを作成し、データアダプタにセットする
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectSetPosSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0501 As DataHBKZ0501) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'パラメータの作成
            Dim param As New List(Of NpgsqlParameter)
            Dim index As Integer = 0

            ' 局
            If Not dataHBKZ0501.PropKyoku.Text.Trim = String.Empty Then
                param.Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropKyoku.Text.Trim) & "%"
                index += 1
            End If

            ' 部署
            If Not dataHBKZ0501.PropBusyo.Text.Trim = String.Empty Then
                param.Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBusyo.Text.Trim) & "%"
                index += 1
            End If

            ' 番組／部屋
            If Not dataHBKZ0501.PropRoom.Text.Trim = String.Empty Then
                param.Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropRoom.Text.Trim) & "%"
                index += 1
            End If

            ' 建物
            If Not dataHBKZ0501.PropBuil.Text.Trim = String.Empty Then
                param.Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBuil.Text.Trim) & "%"
                index += 1
            End If

            ' フロア
            If Not dataHBKZ0501.PropFloor.Text.Trim = String.Empty Then
                param.Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropFloor.Text.Trim) & "%"
                index += 1
            End If

            'SQL文作成
            Dim sb As New StringBuilder
            sb.Append(strSelectSetPosSql)

            For i As Integer = 0 To param.Count - 1
                Select Case param(i).ParameterName
                    Case "SetKyokuNM"
                        sb.Append("   AND SetKyokuNMaimai LIKE :SetKyokuNM")
                    Case "SetBusyoNM"
                        sb.Append("   AND SetBusyoNMaimai LIKE :SetBusyoNM")
                    Case "SetRoom"
                        sb.Append("   AND SetRoomaimai LIKE :SetRoom")
                    Case "SetBuil"
                        sb.Append("   AND SetBuilaimai LIKE :SetBuil")
                    Case "SetFloor"
                        sb.Append("   AND SetFlooraimai LIKE :SetFloor")
                End Select
            Next
            sb.Append(" ORDER BY")
            sb.Append(" JtiFlg, SetKyokuNM ,SetKyokuNM,SetBusyoNM,SetRoom,SetBuil, SetFloor")
            'sb.Append("  Sort")

            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray)

            ' 終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 初期表示用設置情報件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKZ0501">[IN]DataHBKZ0501クラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>設置情報件数用のSQLを作成し、データアダプタにセットする
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetInitSelectSetPosCountSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0501 As DataHBKZ0501) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)
        Dim cmd As New NpgsqlCommand

        Try

            ' パラメータの作成
            Dim param As New List(Of NpgsqlParameter)

            For i As Integer = 0 To dataHBKZ0501.PropBusyoArray.Length - 1
                ' パラメータの作成
                param.Add(New NpgsqlParameter("SetBusyoNM" & i, NpgsqlTypes.NpgsqlDbType.Varchar))
                param(i).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBusyoArray(i)) & "%"
            Next

            ' SQL文作成
            Dim sb As New StringBuilder()
            sb.Append(strSelectCountSetPosSql)

            For i As Integer = 0 To param.Count - 1
                If i = 0 Then
                    sb.Append("   AND (")
                Else
                    Select Case dataHBKZ0501.PropSplitMode
                        Case CommonHBK.CommonDeclareHBK.SPLIT_MODE_AND
                            sb.Append("   AND")
                        Case CommonHBK.CommonDeclareHBK.SPLIT_MODE_OR
                            sb.Append("   OR")
                    End Select
                End If
                sb.Append("   SetBusyoNMaimai").Append(" LIKE :").Append(param(i).ParameterName)
            Next

            If param.Count <> 0 Then
                sb.Append("   )")
            End If

            Adapter.SelectCommand = cmd

            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray())

            ' 終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True
        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function

    ''' <summary>
    ''' 設置情報件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">NpgsqlDataAdapter型オブジェクト</param>
    ''' <param name="Cn">NpgsqlConnection型オブジェクト</param>
    ''' <param name="dataHBKZ0501">DataHBKZ0501型オブジェクト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了</returns>
    ''' <remarks>設置情報件数用のSQLを作成し、データアダプタにセットする
    ''' <para>作成情報：2012/06/11 nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Function SetSelectSetPosCountSql(ByRef Adapter As NpgsqlDataAdapter, ByVal Cn As NpgsqlConnection, ByVal dataHBKZ0501 As DataHBKZ0501) As Boolean
        ' 開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'パラメータの作成
            Dim param As New List(Of NpgsqlParameter)
            Dim index As Integer = 0

            ' 局
            If Not dataHBKZ0501.PropKyoku.Text.Trim() = String.Empty Then
                ' パラメータを作成する
                param.Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropKyoku.Text.Trim()) & "%"
                index += 1
            End If

            ' 部署
            If Not dataHBKZ0501.PropBusyo.Text.Trim() = String.Empty Then
                ' パラメータを作成する
                param.Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBusyo.Text.Trim()) & "%"
                index += 1
            End If

            ' 番組／部屋
            If Not dataHBKZ0501.PropRoom.Text.Trim() = String.Empty Then
                ' パラメータを作成する
                param.Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropRoom.Text.Trim) & "%"
                index += 1
            End If

            ' 建物
            If Not dataHBKZ0501.PropBuil.Text.Trim = String.Empty Then
                ' パラメータを作成する
                param.Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropBuil.Text.Trim) & "%"
                index += 1
            End If

            ' フロア
            If Not dataHBKZ0501.PropFloor.Text.Trim = String.Empty Then
                ' パラメータを作成する
                param.Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))
                param(index).Value = "%" & commonLogicHBK.ChangeStringForSearch(dataHBKZ0501.PropFloor.Text.Trim) & "%"
            End If

            ' SQL文作成
            Dim sb As New StringBuilder()
            sb.Append(strSelectCountSetPosSql)

            For i As Integer = 0 To param.Count - 1
                Select Case param(i).ParameterName
                    Case "SetKyokuNM"
                        sb.Append("   AND SetKyokuNMaimai LIKE :SetKyokuNM")
                    Case "SetBusyoNM"
                        sb.Append("   AND SetBusyoNMaimai LIKE :SetBusyoNM")
                    Case "SetRoom"
                        sb.Append("   AND SetRoomaimai LIKE :SetRoom")
                    Case "SetBuil"
                        sb.Append("   AND SetBuilaimai LIKE :SetBuil")
                    Case "SetFloor"
                        sb.Append("   AND SetFlooraimai LIKE :SetFloor")
                End Select
            Next

            ' SQL文設定
            Adapter.SelectCommand = New NpgsqlCommand(sb.ToString(), Cn)

            ' パラメータ設定
            Adapter.SelectCommand.Parameters.AddRange(param.ToArray)

            ' 終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            Return True

        Catch ex As Exception
            Common.CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try
    End Function
End Class
