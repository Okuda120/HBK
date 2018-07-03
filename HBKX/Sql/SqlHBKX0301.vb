Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Net

''' <summary>
''' エンドユーザーマスター検索一覧画面Sqlクラス
''' </summary>
''' <remarks>エンドユーザーマスター検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/06 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0301

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数
    Private Const SEARCH_MODE_SEARCH As Integer = 0         '検索結果
    Private Const SEARCH_MODE_COUNT As Integer = 1          '検索件数

    'エンドユーザーマスター/ユーザー区分取得用SQL
    Private strSelectEndUsrMasterUsrKbnsql As String = "SELECT DISTINCT " & vbCrLf & _
                                                       "em.UsrKbn AS UsrKbn_Disp" & vbCrLf & _
                                                       ",em.UsrKbn AS UsrKbn_Hidden" & vbCrLf & _
                                                       "FROM ENDUSR_MTB AS em "


    'エンドユーザーマスター件数取得用(削除データ含まない)
    Private strSelectEndUsrMastarCountsql As String = "SELECT " & vbCrLf & _
                                                      "COUNT(*) " & vbCrLf & _
                                                      "FROM  ENDUSR_MTB AS em " & vbCrLf & _
                                                      "WHERE em.StateNaiyo NOT LIKE :StateNaiyo "

    'エンドユーザーマスター件数(削除データ含む)取得用
    Private strSelectEndUsrMastarAllCountsql As String = "SELECT " & vbCrLf & _
                                                      "COUNT(*) " & vbCrLf & _
                                                      "FROM ENDUSR_MTB AS em " & vbCrLf & _
                                                      "WHERE 1 = 1 "

   

    'エンドユーザーマスター検索結果取得用
    Private strSelectEndUsrMasterSearchsql As String = "SELECT " & vbCrLf & _
                                                       "em.EndUsrID," & vbCrLf & _
                                                       "em.EndUsrNM," & vbCrLf & _
                                                       "em.EndUsrNMkana," & vbCrLf & _
                                                       "em.EndUsrCompany," & vbCrLf & _
                                                       "em.EndUsrBusyoNM," & vbCrLf & _
                                                       "em.EndUsrTel," & vbCrLf & _
                                                       "em.EndUsrMailAdd," & vbCrLf & _
                                                       "em.UsrKbn, " & vbCrLf & _
                                                       "CASE " & vbCrLf & _
                                                          "WHEN em.RegKbn = :RegKbnTorikomi " & vbCrLf & _
                                                          "THEN '" & REG_TORIKOMI_NM & "' " & vbCrLf & _
                                                          "WHEN em.RegKbn = :RegKbnGamen " & vbCrLf & _
                                                          "THEN '" & REG_GAMEN_NM & "' " & vbCrLf & _
                                                          "ELSE '' " & vbCrLf & _
                                                       "END AS RegKbn," & vbCrLf & _
                                                       "em.StateNaiyo, " & vbCrLf & _
                                                       "em.RegKbn AS RegKbnSort " & vbCrLf & _
                                                       "FROM ENDUSR_MTB AS em " & vbCrLf & _
                                                       "WHERE 1 = 1 "

    '特権ログインログデータ書き込みSQL
    Private strInsertSuperLoginLog As String = "INSERT INTO SUPERLOGIN_LTB (" & _
                                                " SuperUsrID " & _
                                                ",HBKUsrID " & _
                                                ",LogInOutKbn " & _
                                                ",KindCD " & _
                                                ",ClientHostNM " & _
                                                ",ProgramID ) " & _
                                                " VALUES (" & _
                                                " :SuperUsrID " & _
                                                ",:HBKUsrID " & _
                                                ",:LogInOutKbn " & _
                                                ",Now() " & _
                                                ",:ClientHostNM " & _
                                                ",:ProgramID ) "

    ''' <summary>
    ''' エンドユーザーマスター/ユーザー区分取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター/ユーザー区分取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/03 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectEndUsrMasterUsrKbnSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0301 As DataHBKX0301) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスター/ユーザー区分取得用SQLを設定
            strSQL = strSelectEndUsrMasterUsrKbnsql

            'データアダプタに、SQLのSELECT文を設定

            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


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
    ''' 検索結果件数(削除含む)取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/09/11 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultAllCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKX0301 As DataHBKX0301) As Boolean

        '変数宣言
        Dim strSql As String

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索結果件数取得用SQLを設定
            strSql = strSelectEndUsrMastarAllCountsql


            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKX0301, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

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
    ''' 検索結果件数(削除データ含まない)取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>検索結果件数取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetResultCountSql(ByRef Adapter As NpgsqlDataAdapter, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKX0301 As DataHBKX0301) As Boolean

        '変数宣言
        Dim strSql As String

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            '検索結果件数取得用SQLを設定
            strSql = strSelectEndUsrMastarCountsql


            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKX0301, strSql, SEARCH_MODE_COUNT) = False Then
                Return False
            End If

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
    ''' エンドユーザーマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスターテーブル取得用のSQLを作成し、アダプタにセットするための関数を呼び出す
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectEndUsrMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKX0301 As DataHBKX0301) As Boolean

        '変数宣言
        Dim strSql As String

        Try

            '開始ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSql = strSelectEndUsrMasterSearchsql

            '検索条件作成
            If CreateSearchSql(Adapter, Cn, dataHBKX0301, strSql, SEARCH_MODE_SEARCH) = False Then
                Return False
            End If

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
    ''' SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' <param name="strSql">[IN]基本SQL文</param>
    ''' <param name="intSearchFlg">[IN]Sql判別フラグ</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>パラメータとして受け取ったSQL文をもとにSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Private Function CreateSearchSql(ByRef Adapter As NpgsqlDataAdapter, _
                                     ByVal Cn As NpgsqlConnection, _
                                     ByVal dataHBKX0301 As DataHBKX0301, _
                                     ByVal strSql As String,
                                     ByVal intSearchFlg As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strWHERE As String = ""                 'WHERE句のSQLを格納する場所

        Try

            '**********************************
            'SQL文設定
            '**********************************

            '検索項目に設定されている情報をSQL文に追記する処理

            With dataHBKX0301

                'エンドユーザーID検索

                If .PropTxtEndUsrID.Text.Trim <> "" Then

                    strWHERE &= " AND em.EndUsrID = :EndUsrID "

                End If

                'エンドユーザー氏名検索(あいまい検索)

                If .PropTxtEndUsrNM.Text.Trim <> "" Then
                   
                    strWHERE &= " AND em.EndUsrNMAimai like :EndUsrNMAimai "

                End If

                '部署名検索(あいまい検索)

                If .PropTxtBusyoNM.Text.Trim <> "" Then

                    strWHERE &= " AND em.EndUsrBusyoNMAimai like :EndUsrBusyoNMAimai "

                End If

               

                'ユーザー区分検索

                If .PropcmbUsrKbn.SelectedValue <> Nothing Then

                    strWHERE &= " AND em.UsrKbn = :UsrKbn "

                End If

                '登録方法検索

                If .PropCmbRegKbn.SelectedValue <> Nothing Then

                    strWHERE &= " AND em.RegKbn = :RegKbn "

                End If

                'ソートをかける一文を追加
                '検索結果取得時のみ実行
                If intSearchFlg = SEARCH_MODE_SEARCH Then
                    '登録区分＋エンドユーザーIDの昇順でソートを行う
                    strWHERE &= " ORDER BY em.RegKbn ASC, em.EndUsrID ASC"
                End If

                'WHERE句を結合

                strSql &= strWHERE

                'データアダプタに、SQLのSELECT文を設定

                Adapter.SelectCommand = New NpgsqlCommand(strSql, Cn)

                '**********************************
                '* バインド変数に型と値をセット
                '**********************************

                If intSearchFlg = SEARCH_MODE_SEARCH Then
                   

                    '登録方法(取込)
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegKbnTorikomi", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegKbnTorikomi").Value = REG_TORIKOMI
                    '登録方法(画面入力)
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegKbnGamen", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegKbnGamen").Value = REG_GAMEN

                    
                End If

                'エンドユーザーID
                If .PropTxtEndUsrID.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EndUsrID").Value = .PropTxtEndUsrID.Text
                End If

                'エンドユーザー氏名(あいまい)
                If .PropTxtEndUsrNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EndUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EndUsrNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtEndUsrNM.Text) + "%"
                End If

                '部署名(あいまい)
                If .PropTxtBusyoNM.Text.Trim <> "" Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("EndUsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("EndUsrBusyoNMAimai").Value = "%" + commonLogicHBK.ChangeStringForSearch(.PropTxtBusyoNM.Text) + "%"
                End If

               

                'ユーザー区分
                If .PropcmbUsrKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("UsrKbn").Value = .PropcmbUsrKbn.SelectedValue
                End If

                '登録方法
                If .PropCmbRegKbn.SelectedValue <> Nothing Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("RegKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("RegKbn").Value = .PropCmbRegKbn.SelectedValue
                End If

               

                '状態説明
                If .PropChkJtiFlg.Checked = False Then
                    Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("StateNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))
                    Adapter.SelectCommand.Parameters("StateNaiyo").Value = "%" + STATE_NAIYO_DELETE + "%"
                End If

            End With



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
    ''' 特権ログインログを書き込むSQL作成
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0301">[IN]エンドユーザーマスター検索一覧画面データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/09/11 k.imayama
    ''' </para></remarks>
    Public Function SetInsertSuperLoginLogSql(ByRef Cmd As NpgsqlCommand, ByVal Cn As NpgsqlConnection, ByVal dataHBKX0301 As DataHBKX0301) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'SQL文(INSERT)
            strSQL = strInsertSuperLoginLog

            'データアダプタに、SQLを設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("SuperUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("LogInOutKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("ClientHostNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Add(New NpgsqlParameter("ProgramID", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("SuperUsrID").Value = dataHBKX0301.PropStrSuperUsrID
                .Parameters("HBKUsrID").Value = CommonHBK.CommonDeclareHBK.PropUserId
                .Parameters("LogInOutKbn").Value = SUPER_LOGOUT
                .Parameters("ClientHostNM").Value = Dns.GetHostName()
                .Parameters("ProgramID").Value = dataHBKX0301.PropStrProgramID
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            '例外発生
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
