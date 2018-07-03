Imports Common
Imports CommonHBK
Imports Npgsql
Imports System.Net

''' <summary>
''' 一括登録　システムクラスクラス
''' </summary>
''' <remarks>一括登録　システムのSQLの作成・設定を行う
''' <para>作成情報：2012/09/07 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0501

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'エンドユーザーマスター取得処理（SELECT）SQL
    Private strSelectEndUsrSql As String = "SELECT EndUsrID,RegKbn " & vbCrLf & _
                                                "FROM ENDUSR_MTB " & vbCrLf & _
                                                "WHERE EndUsrID = :EndUsrID"

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    'エンドユーザーマスター新規登録（INSERT）SQL
    Private strInsertEndUsrSql As String = "INSERT INTO ENDUSR_MTB (" & vbCrLf & _
                                                " EndUsrID " & vbCrLf & _
                                                ",EndUsrSei " & vbCrLf & _
                                                ",EndUsrMei " & vbCrLf & _
                                                ",EndUsrNM " & vbCrLf & _
                                                ",EndUsrSeikana " & vbCrLf & _
                                                ",EndUsrMeikana " & vbCrLf & _
                                                ",EndUsrNMkana " & vbCrLf & _
                                                ",EndUsrCompany " & vbCrLf & _
                                                ",EndUsrBusyoNM " & vbCrLf & _
                                                ",EndUsrTel " & vbCrLf & _
                                                ",EndUsrMailAdd " & vbCrLf & _
                                                ",UsrKbn " & vbCrLf & _
                                                ",StateNaiyo " & vbCrLf & _
                                                ",RegKbn " & vbCrLf & _
                                                ",EndUsrNMAimai " & vbCrLf & _
                                                ",EndUsrBusyoNMAimai " & vbCrLf & _
                                                ",EndUsrAimai " & vbCrLf & _
                                                ",Sort " & vbCrLf & _
                                                ",JtiFlg " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") " & vbCrLf & _
                                                "VALUES ( " & vbCrLf & _
                                                " :EndUsrID " & vbCrLf & _
                                                ",:EndUsrSei " & vbCrLf & _
                                                ",:EndUsrMei " & vbCrLf & _
                                                ",:EndUsrNM " & vbCrLf & _
                                                ",:EndUsrSeikana " & vbCrLf & _
                                                ",:EndUsrMeikana " & vbCrLf & _
                                                ",:EndUsrNMkana " & vbCrLf & _
                                                ",:EndUsrCompany " & vbCrLf & _
                                                ",:EndUsrBusyoNM " & vbCrLf & _
                                                ",:EndUsrTel " & vbCrLf & _
                                                ",:EndUsrMailAdd " & vbCrLf & _
                                                ",:UsrKbn " & vbCrLf & _
                                                ",:StateNaiyo " & vbCrLf & _
                                                ",:RegKbn " & vbCrLf & _
                                                ",:EndUsrNMAimai " & vbCrLf & _
                                                ",:EndUsrBusyoNMAimai " & vbCrLf & _
                                                ",:EndUsrAimai " & vbCrLf & _
                                                ",:Sort " & vbCrLf & _
                                                ",:JtiFlg " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "

    'エンドユーザーマスター更新用（UPDATE）SQL
    Private strUpdateEndUsrsql As String = "UPDATE ENDUSR_MTB SET " & vbCrLf & _
                                                "EndUsrSei = :EndUsrSei, " & vbCrLf & _
                                                "EndUsrMei = :EndUsrMei, " & vbCrLf & _
                                                "EndUsrNM = :EndUsrNM, " & vbCrLf & _
                                                "EndUsrSeikana = :EndUsrSeikana, " & vbCrLf & _
                                                "EndUsrMeikana = :EndUsrMeikana, " & vbCrLf & _
                                                "EndUsrNMkana = :EndUsrNMkana, " & vbCrLf & _
                                                "EndUsrCompany = :EndUsrCompany, " & vbCrLf & _
                                                "EndUsrBusyoNM = :EndUsrBusyoNM, " & vbCrLf & _
                                                "EndUsrTel = :EndUsrTel, " & vbCrLf & _
                                                "EndUsrMailAdd = :EndUsrMailAdd, " & vbCrLf & _
                                                "UsrKbn = :UsrKbn, " & vbCrLf & _
                                                "StateNaiyo = :StateNaiyo, " & vbCrLf & _
                                                "EndUsrNMAimai = :EndUsrNMAimai, " & vbCrLf & _
                                                "EndUsrBusyoNMAimai = :EndUsrBusyoNMAimai, " & vbCrLf & _
                                                "EndUsrAimai = :EndUsrAimai, " & vbCrLf & _
                                                "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                "UpdateID = :UpdateID " & vbCrLf & _
                                                "WHERE EndUsrID = :EndUsrID"

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
    ''' エンドユーザーIDのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーIDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectEndUsrSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKX0501 As DataHBKX0501, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectEndUsrSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("EndUsrID").Value = dataHBKX0501.PropAryEndUsrID(intIndex).ToString
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
    ''' 【編集モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectSysDateSql

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
    ''' エンドユーザーマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertEndUsrSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0501 As DataHBKX0501, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertEndUsrSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))             'エンドユーザーID
                .Add(New NpgsqlParameter("EndUsrSei", NpgsqlTypes.NpgsqlDbType.Varchar))            '姓
                .Add(New NpgsqlParameter("EndUsrMei", NpgsqlTypes.NpgsqlDbType.Varchar))            '名
                .Add(New NpgsqlParameter("EndUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))             '氏名
                .Add(New NpgsqlParameter("EndUsrSeikana", NpgsqlTypes.NpgsqlDbType.Varchar))        '姓カナ
                .Add(New NpgsqlParameter("EndUsrMeikana", NpgsqlTypes.NpgsqlDbType.Varchar))        '名カナ
                .Add(New NpgsqlParameter("EndUsrNMkana", NpgsqlTypes.NpgsqlDbType.Varchar))         '氏名カナ
                .Add(New NpgsqlParameter("EndUsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))        '所属会社
                .Add(New NpgsqlParameter("EndUsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        '部署名
                .Add(New NpgsqlParameter("EndUsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))            '電話番号
                .Add(New NpgsqlParameter("EndUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))        'メールアドレス
                .Add(New NpgsqlParameter("UsrKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー区分
                .Add(New NpgsqlParameter("StateNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))           '状態説明
                .Add(New NpgsqlParameter("RegKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               '登録方法
                .Add(New NpgsqlParameter("EndUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '氏名（あいまい）
                .Add(New NpgsqlParameter("EndUsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '部署名（あいまい）
                .Add(New NpgsqlParameter("EndUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))          'エンドユーザー（あいまい）
                .Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))                 '表示順
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))               '削除フラグ
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))              '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("EndUsrID").Value = dataHBKX0501.PropAryEndUsrID(intIndex).ToString             'エンドユーザーID
                .Parameters("EndUsrSei").Value = dataHBKX0501.PropAryEndUsrSei(intIndex).ToString           '姓
                .Parameters("EndUsrMei").Value = dataHBKX0501.PropAryEndUsrMei(intIndex).ToString           '名
                .Parameters("EndUsrNM").Value = dataHBKX0501.PropAryEndUsrSei(intIndex).ToString & "　" & _
                                                dataHBKX0501.PropAryEndUsrMei(intIndex).ToString            '氏名
                .Parameters("EndUsrSeikana").Value = dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString   '姓カナ
                .Parameters("EndUsrMeikana").Value = dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString   '名カナ
                .Parameters("EndUsrNMkana").Value = dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString & "　" & _
                                                    dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString    '氏名カナ
                .Parameters("EndUsrCompany").Value = dataHBKX0501.PropAryEndUsrCompany(intIndex).ToString   '所属会社
                .Parameters("EndUsrBusyoNM").Value = dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString   '部署名
                .Parameters("EndUsrTel").Value = dataHBKX0501.PropAryEndUsrTel(intIndex).ToString           '電話番号
                .Parameters("EndUsrMailAdd").Value = dataHBKX0501.PropAryEndUsrMailAdd(intIndex).ToString   'メールアドレス
                .Parameters("UsrKbn").Value = dataHBKX0501.PropAryUsrKbn(intIndex).ToString                 'ユーザー区分
                .Parameters("StateNaiyo").Value = dataHBKX0501.PropAryStateNaiyo(intIndex).ToString         '状態説明
                .Parameters("RegKbn").Value = DATA_REG_UPLOAD                                               '登録方法（0:取込）

                'あいまい検索文字列設定
                .Parameters("EndUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSei(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMei(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString)

                .Parameters("EndUsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString)

                .Parameters("EndUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrID(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSei(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMei(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMailAdd(intIndex).ToString)

                .Parameters("Sort").Value = DBNull.Value                        '表示順
                .Parameters("JtiFlg").Value = DATA_YUKO                         '削除フラグ
                .Parameters("RegDT").Value = dataHBKX0501.PropDtmSysDate        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKX0501.PropDtmSysDate     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                      '最終更新者ID
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

    ''' <summary>
    ''' エンドユーザーマスター更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN/OUT]エンドユーザー取込画面Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>エンドユーザーマスター更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateEndUsrSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0501 As DataHBKX0501, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文

        Try
            'SQL文(Update)
            strSQL = strUpdateEndUsrsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("EndUsrSei", NpgsqlTypes.NpgsqlDbType.Varchar))            '姓
                .Add(New NpgsqlParameter("EndUsrMei", NpgsqlTypes.NpgsqlDbType.Varchar))            '名
                .Add(New NpgsqlParameter("EndUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))             '氏名
                .Add(New NpgsqlParameter("EndUsrSeikana", NpgsqlTypes.NpgsqlDbType.Varchar))        '姓カナ
                .Add(New NpgsqlParameter("EndUsrMeikana", NpgsqlTypes.NpgsqlDbType.Varchar))        '名カナ
                .Add(New NpgsqlParameter("EndUsrNMkana", NpgsqlTypes.NpgsqlDbType.Varchar))         '氏名カナ
                .Add(New NpgsqlParameter("EndUsrCompany", NpgsqlTypes.NpgsqlDbType.Varchar))        '所属会社
                .Add(New NpgsqlParameter("EndUsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))        '部署名
                .Add(New NpgsqlParameter("EndUsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))            '電話番号
                .Add(New NpgsqlParameter("EndUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))        'メールアドレス
                .Add(New NpgsqlParameter("UsrKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               'ユーザー区分
                .Add(New NpgsqlParameter("StateNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))           '状態説明
                .Add(New NpgsqlParameter("EndUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '氏名（あいまい）
                .Add(New NpgsqlParameter("EndUsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '部署名（あいまい）
                .Add(New NpgsqlParameter("EndUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))          'エンドユーザー（あいまい）
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))               '削除フラグ
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("EndUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))             'エンドユーザーID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("EndUsrSei").Value = dataHBKX0501.PropAryEndUsrSei(intIndex).ToString           '姓
                .Parameters("EndUsrMei").Value = dataHBKX0501.PropAryEndUsrMei(intIndex).ToString           '名
                .Parameters("EndUsrNM").Value = dataHBKX0501.PropAryEndUsrSei(intIndex).ToString & "　" & _
                                                dataHBKX0501.PropAryEndUsrMei(intIndex).ToString            '氏名
                .Parameters("EndUsrSeikana").Value = dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString   '姓カナ
                .Parameters("EndUsrMeikana").Value = dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString   '名カナ
                .Parameters("EndUsrNMkana").Value = dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString & "　" & _
                                                    dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString    '氏名カナ
                .Parameters("EndUsrCompany").Value = dataHBKX0501.PropAryEndUsrCompany(intIndex).ToString   '所属会社
                .Parameters("EndUsrBusyoNM").Value = dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString   '部署名
                .Parameters("EndUsrTel").Value = dataHBKX0501.PropAryEndUsrTel(intIndex).ToString           '電話番号
                .Parameters("EndUsrMailAdd").Value = dataHBKX0501.PropAryEndUsrMailAdd(intIndex).ToString   'メールアドレス
                .Parameters("UsrKbn").Value = dataHBKX0501.PropAryUsrKbn(intIndex).ToString                 'ユーザー区分
                .Parameters("StateNaiyo").Value = dataHBKX0501.PropAryStateNaiyo(intIndex).ToString         '状態説明

                'あいまい検索文字列設定
                .Parameters("EndUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSei(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMei(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString) +
                                                     commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString)

                .Parameters("EndUsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString)

                .Parameters("EndUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrID(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSei(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMei(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrSeikana(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMeikana(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrBusyoNM(intIndex).ToString) +
                                                   commonLogicHBK.ChangeStringForSearch(dataHBKX0501.PropAryEndUsrMailAdd(intIndex).ToString)

                .Parameters("JtiFlg").Value = DATA_YUKO                         '削除フラグ
                .Parameters("UpdateDT").Value = dataHBKX0501.PropDtmSysDate     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                      '最終更新者ID
                .Parameters("EndUsrID").Value = dataHBKX0501.PropAryEndUsrID(intIndex).ToString             'エンドユーザーID
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

    ''' <summary>
    ''' 特権ログインログを書き込むSQL作成
    ''' <param name="Adapter">[IN/OUT]NpgsqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0501">[IN]エンドユーザー取込画面Dataクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/09/07 k.imayama
    ''' </para></remarks>
    Public Function SetInsertSuperLoginLogSql(ByRef Cmd As NpgsqlCommand, ByVal Cn As NpgsqlConnection, ByVal dataHBKX0501 As DataHBKX0501) As Boolean

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
                .Parameters("SuperUsrID").Value = dataHBKX0501.PropStrSuperUsrID
                .Parameters("HBKUsrID").Value = CommonHBK.CommonDeclareHBK.PropUserId
                .Parameters("LogInOutKbn").Value = SUPER_LOGOUT
                .Parameters("ClientHostNM").Value = Dns.GetHostName()
                .Parameters("ProgramID").Value = dataHBKX0501.PropStrProgramID
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
