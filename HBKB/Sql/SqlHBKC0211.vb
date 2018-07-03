Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 連携処理実施Sqlクラス
''' </summary>
''' <remarks>連携処理実施のSQLの作成・設定を行う
''' <para>作成情報：2012/09/12 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0211

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'インシデントSM連携指示テーブル取得（SELECT）SQL
    Private strSelectIncidentSMrenkeiSql As String = "SELECT " & vbCrLf & _
                                                    " Count(isr.IncNmb) " & vbCrLf & _
                                                    "FROM incident_sm_renkei_tb isr " & vbCrLf & _
                                                    "WHERE isr.IncNmb = :IncNmb " & vbCrLf & _
                                                    "AND isr.RenkeiFlg = :RenkeiFlg "

    'インシデントSM連携指示テーブル登録（INSERT）SQL
    Private strInsertIncidentSMrenkeisql As String = "INSERT INTO incident_sm_renkei_tb ( " & vbCrLf & _
                                                    " Seq " & vbCrLf & _
                                                    ",IncNmb " & vbCrLf & _
                                                    ",SMIncNmb " & vbCrLf & _
                                                    ",RenkeiFlg " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "VALUES ( " & vbCrLf & _
                                                    " :Seq " & vbCrLf & _
                                                    ",:IncNmb " & vbCrLf & _
                                                    ",(SELECT COALESCE(SMNmb,'') FROM incident_sm_tuti_tb WHERE IncNmb=:IncNmb) " & vbCrLf & _
                                                    ",:RenkeiFlg " & vbCrLf & _
                                                    ",:RegDT " & vbCrLf & _
                                                    ",:RegGrpCD " & vbCrLf & _
                                                    ",:RegID " & vbCrLf & _
                                                    ",:UpdateDT " & vbCrLf & _
                                                    ",:UpGrpCD " & vbCrLf & _
                                                    ",:UpdateID " & vbCrLf & _
                                                    ") "

    ''' <summary>
    ''' インシデントSM連携指示テーブル取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0211">[IN]連携処理実施データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデントSM連携指示通知テーブル取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/12 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentSMrenkeiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectIncidentSMrenkeiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("RenkeiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))    '連携状況フラグ
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("RenkeiFlg").Value = RENKEIFLG_WAIT                '連携待ち
                .Parameters("IncNmb").Value = dataHBKC0211.PropIntINCNmb
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
    ''' 新規Seq、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0211">[IN]連携処理実施データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規Seq、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewSetBusyoCDAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_INCIDENTSMRENKEI_SEQ

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
    ''' インシデントSM連携指示テーブル新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0211">[IN]連携処理実施データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデントSM連携指示テーブル新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/13 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncidentSMrenkeiSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0211 As DataHBKC0211) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try
            'インシデントSM連携指示テーブル新規登録用SQLを設定
            strSQL = strInsertIncidentSMrenkeisql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("Seq", NpgsqlTypes.NpgsqlDbType.Integer))          'seq
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
                .Add(New NpgsqlParameter("RenkeiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))    '連携状況フラグ
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))   '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))     '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("Seq").Value = dataHBKC0211.PropIntSeq
                .Parameters("IncNmb").Value = dataHBKC0211.PropIntINCNmb
                .Parameters("RenkeiFlg").Value = RENKEIFLG_WAIT
                .Parameters("RegDT").Value = dataHBKC0211.PropDtmSysDate
                .Parameters("RegGrpCD").Value = PropWorkGroupCD
                .Parameters("RegID").Value = PropUserId
                .Parameters("UpdateDT").Value = dataHBKC0211.PropDtmSysDate
                .Parameters("UpGrpCD").Value = PropWorkGroupCD
                .Parameters("UpdateID").Value = PropUserId
            End With

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Cmd)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function

End Class
