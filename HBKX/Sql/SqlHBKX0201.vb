Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Net

''' <summary>
''' ひびきユーザーマスター登録画面Sqlクラス
''' </summary>
''' <remarks>ひびきユーザーマスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/21 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0201

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    'ひびきユーザーマスター、所属マスター取得用(基本SQL)
    Private strSelectHBKUsrSZKMastarsql As String = "SELECT " & vbCrLf & _
                                                    "hm.HBKUsrID, " & vbCrLf & _
                                                    "hm.HBKUsrNM, " & vbCrLf & _
                                                    "hm.HBKUsrNmKana, " & vbCrLf & _
                                                    "hm.HBKUsrMailAdd, " & vbCrLf & _
                                                    "CASE " & vbCrLf & _
                                                        "WHEN sm.UsrGroupFlg = :UsrGroupFlgNormal " & vbCrLf & _
                                                        "THEN False " & vbCrLf & _
                                                        "WHEN sm.UsrGroupFlg = :UsrGroupFlgAdmin " & vbCrLf & _
                                                        "THEN True " & vbCrLf & _
                                                    "END AS UsrGroupFlg, " & vbCrLf & _
                                                    "CASE " & vbCrLf & _
                                                        "WHEN sm.DefaultFlg = :DefaultFlgOFF " & vbCrLf & _
                                                        "THEN False " & vbCrLf & _
                                                        "WHEN sm.DefaultFlg = :DefaultFlgON " & vbCrLf & _
                                                        "THEN True " & vbCrLf & _
                                                    "END AS DefaultFlg, " & vbCrLf & _
                                                    "CASE " & vbCrLf & _
                                                        "WHEN sm.JtiFlg = :JtiFlg " & vbCrLf & _
                                                        "THEN False " & vbCrLf & _
                                                        "WHEN sm.JtiFlg = :JtiFlgOFF " & vbCrLf & _
                                                        "THEN True " & vbCrLf & _
                                                    "END AS JtiFlg, " & vbCrLf & _
                                                    "0 AS NewData, " & vbCrLf & _
                                                    "sm.JtiFlg AS JtiFlgKAKUSHI, " & vbCrLf & _
                                                    "CASE " & vbCrLf & _
                                                        "WHEN sm.JtiFlg = :JtiFlg " & vbCrLf & _
                                                        "THEN 0 " & vbCrLf & _
                                                        "WHEN sm.JtiFlg = :JtiFlgOFF " & vbCrLf & _
                                                        "THEN 2 " & vbCrLf & _
                                                    "END AS Sort, " & vbCrLf & _
                                                    ":TextChangeFlg AS TextChangeFlg, " & vbCrLf & _
                                                    ":CheckChangeFlg AS CheckChangeFlg " & vbCrLf & _
                                                    "From HBKUSR_MTB AS hm " & vbCrLf & _
                                                    "LEFT OUTER JOIN SZK_MTB AS sm " & vbCrLf & _
                                                    "ON hm.HBKUsrID = sm.HBKUsrID " & vbCrLf & _
                                                    "WHERE sm.GroupCD = :GroupCD " & vbCrLf & _
                                                    "ORDER BY hm.HBKUsrID ASC "



    'グループマスター取得用
    Private strSelectGroupMastersql As String = "SELECT " & vbCrLf & _
                                                "gm.GroupCD, " & vbCrLf & _
                                                "gm.GroupNM " & vbCrLf & _
                                                "FROM GRP_MTB as gm " & vbCrLf & _
                                                "WHERE gm.JtiFlg = :JtiFlgON " & vbCrLf & _
                                                "ORDER BY gm.GroupCD "

    '所属マスター/デフォルトフラグ有効数取得用
    Private strSelectSZKMasterDefaultsql As String = "SELECT Count(*) " & vbCrLf & _
                                                     "FROM SZK_MTB AS sm " & vbCrLf & _
                                                     "WHERE sm.HBKUsrID = :HBKUsrID " & vbCrLf & _
                                                     "AND sm.GroupCD <> :GroupCD " & vbCrLf & _
                                                     "AND sm.DefaultFlg = :DefaultFlg "

    'ひびきユーザーマスター新規登録用
    Private strInsertHBKUsrMastersql As String = "INSERT INTO HBKUSR_MTB ( " & vbCrLf & _
                                                     "HBKUsrID, " & vbCrLf & _
                                                     "HBKUsrNM, " & vbCrLf & _
                                                     "HBKUsrNmKana, " & vbCrLf & _
                                                     "HBKUsrMailAdd, " & vbCrLf & _
                                                     "HBKUsrNMAimai, " & vbCrLf & _
                                                     "HBKUsrAimai, " & vbCrLf & _
                                                     "Sort, " & vbCrLf & _
                                                     "JtiFlg, " & vbCrLf & _
                                                     "RegDT, " & vbCrLf & _
                                                     "RegGrpCD, " & vbCrLf & _
                                                     "RegID, " & vbCrLf & _
                                                     "UpdateDT, " & vbCrLf & _
                                                     "UpGrpCD, " & vbCrLf & _
                                                     "UpdateID " & vbCrLf & _
                                                 ") " & vbCrLf & _
                                                 "VALUES ( " & vbCrLf &
                                                     ":HBKUsrID, " & vbCrLf & _
                                                     ":HBKUsrNM, " & vbCrLf & _
                                                     ":HBKUsrNmKana, " & vbCrLf & _
                                                     ":HBKUsrMailAdd, " & vbCrLf & _
                                                     ":HBKUsrNMAimai, " & vbCrLf & _
                                                     ":HBKUsrAimai, " & vbCrLf & _
                                                     ":Sort, " & vbCrLf & _
                                                     ":JtiFlg, " & vbCrLf & _
                                                     ":RegDT, " & vbCrLf & _
                                                     ":RegGrpCD, " & vbCrLf & _
                                                     ":RegID, " & vbCrLf & _
                                                     ":UpdateDT, " & vbCrLf & _
                                                     ":UpGrpCD, " & vbCrLf & _
                                                     ":UpdateID " & vbCrLf & _
                                                 ") "

    '所属マスター新規登録用
    Private strInsertSZKMastersql As String = "INSERT INTO SZK_MTB ( " & vbCrLf & _
                                                     "HBKUsrID, " & vbCrLf & _
                                                     "GroupCD, " & vbCrLf & _
                                                     "UsrGroupFlg, " & vbCrLf & _
                                                     "DefaultFlg, " & vbCrLf & _
                                                     "Sort, " & vbCrLf & _
                                                     "JtiFlg, " & vbCrLf & _
                                                     "RegDT, " & vbCrLf & _
                                                     "RegGrpCD, " & vbCrLf & _
                                                     "RegID, " & vbCrLf & _
                                                     "UpdateDT, " & vbCrLf & _
                                                     "UpGrpCD, " & vbCrLf & _
                                                     "UpdateID " & vbCrLf & _
                                                 ") " & vbCrLf & _
                                                 "VALUES ( " & vbCrLf &
                                                     ":HBKUsrID, " & vbCrLf & _
                                                     ":GroupCD, " & vbCrLf & _
                                                     ":UsrGroupFlg, " & vbCrLf & _
                                                     ":DefaultFlg, " & vbCrLf & _
                                                     ":Sort, " & vbCrLf & _
                                                     ":JtiFlg, " & vbCrLf & _
                                                     ":RegDT, " & vbCrLf & _
                                                     ":RegGrpCD, " & vbCrLf & _
                                                     ":RegID, " & vbCrLf & _
                                                     ":UpdateDT, " & vbCrLf & _
                                                     ":UpGrpCD, " & vbCrLf & _
                                                     ":UpdateID " & vbCrLf & _
                                                 ") "

    'ひびきユーザーマスター更新用
    Private strUpdateHBKUsrMastersql As String = "UPDATE HBKUSR_MTB SET " & vbCrLf & _
                                                 "HBKUsrNM = :HBKUsrNM, " & vbCrLf & _
                                                 "HBKUsrNmKana = :HBKUsrNmKana, " & vbCrLf & _
                                                 "HBKUsrMailAdd = :HBKUsrMailAdd, " & vbCrLf & _
                                                 "HBKUsrNMAimai = :HBKUsrNMAimai, " & vbCrLf & _
                                                 "HBKUsrAimai = :HBKUsrAimai, " & vbCrLf & _
                                                 "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                 "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                 "UpdateID = :UpdateID " & vbCrLf & _
                                                 "WHERE HBKUsrID = :HBKUsrID"


    '所属マスター更新用
    Private strUpdateSZKMastersql As String = "UPDATE SZK_MTB SET " & vbCrLf & _
                                              "UsrGroupFlg = :UsrGroupFlg, " & vbCrLf & _
                                              "DefaultFlg = :DefaultFlg, " & vbCrLf & _
                                              "JtiFlg = :JtiFlg, " & vbCrLf & _
                                              "UpdateDT = :UpdateDT, " & vbCrLf & _
                                              "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                              "UpdateID = :UpdateID " & vbCrLf & _
                                              "WHERE HBKUsrID = :HBKUsrID " & vbCrLf & _
                                              "AND GroupCD = :GroupCD "

    '所属マスターデフォルト更新用
    Private strUpdateSZKMasterDefaultsql As String = "UPDATE SZK_MTB SET " & vbCrLf & _
                                                     "DefaultFlg = :DefaultFlgOFF, " & vbCrLf & _
                                                     "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                     "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                     "UpdateID = :UpdateID " & vbCrLf & _
                                                     "WHERE HBKUsrID = :HBKUsrID " & vbCrLf & _
                                                     "AND GroupCD <> :GroupCD " & vbCrLf & _
                                                     "AND DefaultFlg = :DefaultFlgON"

    'ひびきユーザーマスター登録有無取得用
    Private strSelectHBKUsrMastersql As String = "SELECT count (*) " & vbCrLf & _
                                                  "FROM HBKUSR_MTB AS hm " & vbCrLf & _
                                                  "WHERE hm.HBKUsrID = :HBKUsrID "


    '所属マスター有効データ件数取得用
    Private strSelectSZKMasterYUKOsql As String = "SELECT count (*) " & vbCrLf & _
                                                  "FROM SZK_MTB AS sm " & vbCrLf & _
                                                  "WHERE sm.HBKUsrID = :HBKUsrID " & vbCrLf & _
                                                  "AND sm.JtiFlg = :JtiFlg "

    'ひびきユーザーマスター論理削除用
    Private strUpdateHBKUsrMasterDeletesql As String = "UPDATE HBKUSR_MTB SET " & vbCrLf & _
                                                       "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                       "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                       "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                       "UpdateID = :UpdateID " & vbCrLf & _
                                                       "WHERE HBKUsrID = :HBKUsrID"

    'ひびきユーザーマスター無効データ取得用
    Private strSelectHBKUsrMasterMUKOsql As String = "SELECT count (*) " & vbCrLf & _
                                                  "FROM HBKUSR_MTB AS hm " & vbCrLf & _
                                                  "WHERE hm.HBKUsrID = :HBKUsrID " & vbCrLf & _
                                                  "AND hm.JtiFlg = :JtiFlg "

    'ひびきユーザーマスター論理削除解除用
    Private strUpdateHBKUsrMasterDeleteKaijyosql As String = "UPDATE HBKUSR_MTB SET " & vbCrLf & _
                                                       "JtiFlg = :JtiFlg, " & vbCrLf & _
                                                       "UpdateDT = :UpdateDT, " & vbCrLf & _
                                                       "UpGrpCD = :UpGrpCD, " & vbCrLf & _
                                                       "UpdateID = :UpdateID " & vbCrLf & _
                                                       "WHERE HBKUsrID = :HBKUsrID"
    'システム日付取得用
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

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
    ''' ひびきユーザーマスター、所属マスター取得用SQLの作成・設定処理(削除データ含む)
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター、所属マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectHBKUsrSZKMasterDeleteDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0201 As DataHBKX0201) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = strSelectHBKUsrSZKMastarsql


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'グループコード
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("GroupCD").Value = dataHBKX0201.PropStrGroupCD
            '削除フラグ(有効データ)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlg").Value = DATA_YUKO
            '削除フラグ(無効データ)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlgOFF", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlgOFF").Value = DATA_MUKO
            'ユーザーグループ権限(一般)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrGroupFlgNormal", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("UsrGroupFlgNormal").Value = USR_GROUP_ADMIN_NORMAL
            'ユーザーグループ権限(グループ管理者)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("UsrGroupFlgAdmin", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("UsrGroupFlgAdmin").Value = USR_GROUP_ADMIN_ADMIN
            'デフォルト無効
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DefaultFlgOFF", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("DefaultFlgOFF").Value = DEFAULT_OFF
            'デフォルト有効
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DefaultFlgON", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("DefaultFlgON").Value = DEFAULT_ON
            'テキスト更新無効
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("TextChangeFlg", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("TextChangeFlg").Value = LogicHBKX0201.TEXT_CHANGE_FLG_OFF
            'チェックボックス更新無効
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CheckChangeFlg", NpgsqlTypes.NpgsqlDbType.Integer))
            Adapter.SelectCommand.Parameters("CheckChangeFlg").Value = LogicHBKX0201.CHECK_CHANGE_FLG_OFF


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
    ''' グループマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGroupMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0201 As DataHBKX0201) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = strSelectGroupMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            '削除フラグ(0:有効)
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlgON", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlgON").Value = DATA_YUKO



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
    ''' サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

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
    ''' ひびきユーザーマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertHBKUsrMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'ひびきユーザーマスター新規登録用SQLを設定
            strSQL = strInsertHBKUsrMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                '氏名
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNM").Value = .PropStrHBKUsrNM
                '氏名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNmKana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNmKana").Value = .PropStrHBKUsrNmKana
                'メールアドレス
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrMailAdd").Value = .PropStrHBKUsrMailAdd
                '氏名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNM) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNmKana)
                'ひびきユーザー(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrID) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNM) + commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNmKana) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrMailAdd)
                '表示順(空白)
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = DBNull.Value
                '削除フラグ(0:有効固定)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_YUKO
                '登録日時
                Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("RegDT").Value = .PropDtmSysDate
                '登録者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD
                '登録者ID
                Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegID").Value = PropUserId
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' 所属マスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertSZKMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            '所属マスター新規登録用SQLを設定
            strSQL = strInsertSZKMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                'グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("GroupCD").Value = .PropStrGroupCD
                'ユーザーグループ権限
                Cmd.Parameters.Add(New NpgsqlParameter("UsrGroupFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UsrGroupFlg").Value = .PropStrUsrGroupFlg
                'デフォルトフラグ
                Cmd.Parameters.Add(New NpgsqlParameter("DefaultFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("DefaultFlg").Value = .PropStrDefaultFlg
                '表示順(空白)
                Cmd.Parameters.Add(New NpgsqlParameter("Sort", NpgsqlTypes.NpgsqlDbType.Integer))
                Cmd.Parameters("Sort").Value = DBNull.Value
                '削除フラグ
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = .PropStrJtiFlg
                '登録日時
                Cmd.Parameters.Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("RegDT").Value = .PropDtmSysDate
                '登録者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegGrpCD").Value = PropWorkGroupCD
                '登録者ID
                Cmd.Parameters.Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("RegID").Value = PropUserId
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' ひびきユーザーマスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateHBKUsrMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            'ひびきユーザーマスター更新用SQLを設定
            strSQL = strUpdateHBKUsrMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                '氏名
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNM").Value = .PropStrHBKUsrNM
                '氏名カナ
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNmKana", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNmKana").Value = .PropStrHBKUsrNmKana
                'メールアドレス
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrMailAdd").Value = .PropStrHBKUsrMailAdd
                '氏名(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNM) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNmKana)
                'ひびきユーザー(あいまい)
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrAimai", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrAimai").Value = commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrID) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNM) + commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrNmKana) +
                commonLogicHBK.ChangeStringForSearch(.PropStrHBKUsrMailAdd)
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' 所属マスター編集用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスター編集用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSZKMasterSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            '所属マスター更新用SQLを設定
            strSQL = strUpdateSZKMastersql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                'グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("GroupCD").Value = .PropStrGroupCD
                'ユーザーグループ権限
                Cmd.Parameters.Add(New NpgsqlParameter("UsrGroupFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UsrGroupFlg").Value = .PropStrUsrGroupFlg
                'デフォルトフラグ
                Cmd.Parameters.Add(New NpgsqlParameter("DefaultFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("DefaultFlg").Value = .PropStrDefaultFlg
                '削除フラグ
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = .PropStrJtiFlg
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' 所属マスターデフォルト更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスターデフォルト更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateSZKMasterDefaultSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            '所属マスター更新用SQLを設定
            strSQL = strUpdateSZKMasterDefaultsql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                'グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("GroupCD").Value = .PropStrGroupCD
                'デフォルトフラグ(有効)
                Cmd.Parameters.Add(New NpgsqlParameter("DefaultFlgON", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("DefaultFlgON").Value = DEFAULT_ON
                'デフォルトフラグ(無効)
                Cmd.Parameters.Add(New NpgsqlParameter("DefaultFlgOFF", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("DefaultFlgOFF").Value = DEFAULT_OFF
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' ひびきユーザーマスター登録有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター登録有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/28 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectHBKUsrMasterSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0201 As DataHBKX0201) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = strSelectHBKUsrMastersql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("HBKUsrID").Value = .PropStrHBKUsrID

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
    ''' 所属マスター/デフォルトフラグ有効数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスター/デフォルトフラグ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/22 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectDefaultFlgSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0201 As DataHBKX0201) As Boolean



        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数宣言
        Dim strSQL As String



        Try
            '**********************************
            '* SQL文設定
            '**********************************

            'エンドユーザーマスターテーブル取得用SQLを設定
            strSQL = strSelectSZKMasterDefaultsql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("HBKUsrID").Value = .PropStrInputCheckHBKUsrID
                'グループコード
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("GroupCD").Value = .PropStrGroupCD
                'デフォルトフラグ
                Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("DefaultFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Adapter.SelectCommand.Parameters("DefaultFlg").Value = DEFAULT_ON

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
    ''' 所属マスター有効データ件数取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>所属マスター有効データ件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSZKMasterYUKOSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'SQL文(SELECT)
            strSQL = strSelectSZKMasterYUKOsql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'ひびきユーザーID
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("HBKUsrID").Value = dataHBKX0201.PropStrHBKUsrID
            '削除フラグ
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlg").Value = DATA_YUKO

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
    ''' ひびきユーザーマスター論理削除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター論理削除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateHBKUsrMasterDeleteSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            '所属マスター論理削除用SQLを設定
            strSQL = strUpdateHBKUsrMasterDeletesql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                '削除フラグ(1:無効)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_MUKO
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' ひびきユーザーマスター無効データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター無効データ取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectHBKUsrMasterMUKOSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String

        Try

            'SQL文(SELECT)
            strSQL = strSelectHBKUsrMasterMUKOsql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            '**********************************
            '* バインド変数に型と値をセット
            '**********************************

            'ひびきユーザーID
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("HBKUsrID").Value = dataHBKX0201.PropStrHBKUsrID
            '削除フラグ
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("JtiFlg").Value = DATA_MUKO

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
    ''' ひびきユーザーマスター論理削除解除用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター論理削除解除用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/23 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateHBKUsrMasterDeleteKaijyoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKX0201 As DataHBKX0201) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String


        Try

            '所属マスター論理削除用SQLを設定
            strSQL = strUpdateHBKUsrMasterDeleteKaijyosql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '**********************************
            '* バインド変数に型と値をセット
            '**********************************
            With dataHBKX0201
                'ひびきユーザーID
                Cmd.Parameters.Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("HBKUsrID").Value = .PropStrHBKUsrID
                '削除フラグ(1:無効)
                Cmd.Parameters.Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("JtiFlg").Value = DATA_YUKO
                '最終更新日時
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))
                Cmd.Parameters("UpdateDT").Value = .PropDtmSysDate
                '最終更新者グループCD
                Cmd.Parameters.Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpGrpCD").Value = PropWorkGroupCD
                '最終更新者ID
                Cmd.Parameters.Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))
                Cmd.Parameters("UpdateID").Value = PropUserId
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
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0201">[IN]ひびきユーザーマスター登録画面データクラス</param>
    ''' </summary> 
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>特権ログインログテーブルへログ情報を書き込むSQLをアダプタに設定する
    ''' <para>作成情報：2012/09/11 k.imayama
    ''' </para></remarks>
    Public Function SetInsertSuperLoginLogSql(ByRef Cmd As NpgsqlCommand, ByVal Cn As NpgsqlConnection, ByVal dataHBKX0201 As DataHBKX0201) As Boolean

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
                .Parameters("SuperUsrID").Value = dataHBKX0201.PropStrSuperUsrID
                .Parameters("HBKUsrID").Value = CommonHBK.CommonDeclareHBK.PropUserId
                .Parameters("LogInOutKbn").Value = SUPER_LOGOUT
                .Parameters("ClientHostNM").Value = Dns.GetHostName()
                .Parameters("ProgramID").Value = dataHBKX0201.PropStrProgramID
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
