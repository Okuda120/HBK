Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' メールテンプレートマスター登録画面Sqlクラス
''' </summary>
''' <remarks>メールテンプレートマスター登録画面のSQLの作成・設定を行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKX0701

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    'SQL文宣言
    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    'メールテンプレートマスター取得（SELECT）SQL
    Private strSelectMailTemplateSql As String = "SELECT " & vbCrLf & _
                                                " mt.TemplateNmb " & vbCrLf & _
                                                ",mt.TemplateNM " & vbCrLf & _
                                                ",mt.ProcessKbn " & vbCrLf & _
                                                ",mt.GroupCD " & vbCrLf & _
                                                ",mt.MailFrom " & vbCrLf & _
                                                ",mt.MailTo " & vbCrLf & _
                                                ",mt.CC " & vbCrLf & _
                                                ",mt.Bcc " & vbCrLf & _
                                                ",mt.PriorityKbn " & vbCrLf & _
                                                ",mt.Title " & vbCrLf & _
                                                ",mt.Text " & vbCrLf & _
                                                ",mt.KigenCondCIKbnCD " & vbCrLf & _
                                                ",mt.KigenCondTypeKbn " & vbCrLf & _
                                                ",mt.KigenCondKbn " & vbCrLf & _
                                                ",mt.KigenCondKigen " & vbCrLf & _
                                                ",mt.Sort " & vbCrLf & _
                                                ",mt.JtiFlg " & vbCrLf & _
                                                "FROM MAIL_TEMPLATE_MTB mt " & vbCrLf & _
                                                "WHERE mt.TemplateNmb = :TemplateNmb "

    'メールテンプレートマスター新規登録（INSERT）SQL
    Private strInsertMailTemplateSql As String = "INSERT INTO MAIL_TEMPLATE_MTB ( " & vbCrLf & _
                                                " TemplateNmb " & vbCrLf & _
                                                ",TemplateNM " & vbCrLf & _
                                                ",ProcessKbn " & vbCrLf & _
                                                ",GroupCD " & vbCrLf & _
                                                ",MailFrom " & vbCrLf & _
                                                ",MailTo " & vbCrLf & _
                                                ",CC " & vbCrLf & _
                                                ",Bcc " & vbCrLf & _
                                                ",PriorityKbn " & vbCrLf & _
                                                ",Title " & vbCrLf & _
                                                ",Text " & vbCrLf & _
                                                ",KigenCondCIKbnCD " & vbCrLf & _
                                                ",KigenCondTypeKbn " & vbCrLf & _
                                                ",KigenCondKbn " & vbCrLf & _
                                                ",KigenCondKigen " & vbCrLf & _
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
                                                " :TemplateNmb " & vbCrLf & _
                                                ",:TemplateNM " & vbCrLf & _
                                                ",:ProcessKbn " & vbCrLf & _
                                                ",:GroupCD " & vbCrLf & _
                                                ",:MailFrom " & vbCrLf & _
                                                ",:MailTo " & vbCrLf & _
                                                ",:CC " & vbCrLf & _
                                                ",:Bcc " & vbCrLf & _
                                                ",:PriorityKbn " & vbCrLf & _
                                                ",:Title " & vbCrLf & _
                                                ",:Text " & vbCrLf & _
                                                ",:KigenCondCIKbnCD " & vbCrLf & _
                                                ",:KigenCondTypeKbn " & vbCrLf & _
                                                ",:KigenCondKbn " & vbCrLf & _
                                                ",:KigenCondKigen " & vbCrLf & _
                                                ",:Sort " & vbCrLf & _
                                                ",:JtiFlg " & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                ") "

    'メールテンプレートマスター更新（UPDATE）SQL
    Private strUpdateMailTemplateSql As String = "UPDATE MAIL_TEMPLATE_MTB SET " & vbCrLf & _
                                                " TemplateNM = :TemplateNM " & vbCrLf & _
                                                ",ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                ",MailFrom = :MailFrom " & vbCrLf & _
                                                ",MailTo = :MailTo " & vbCrLf & _
                                                ",CC = :CC " & vbCrLf & _
                                                ",Bcc = :Bcc " & vbCrLf & _
                                                ",PriorityKbn = :PriorityKbn " & vbCrLf & _
                                                ",Title = :Title " & vbCrLf & _
                                                ",Text = :Text " & vbCrLf & _
                                                ",KigenCondCIKbnCD = :KigenCondCIKbnCD " & vbCrLf & _
                                                ",KigenCondTypeKbn = :KigenCondTypeKbn " & vbCrLf & _
                                                ",KigenCondKbn = :KigenCondKbn " & vbCrLf & _
                                                ",KigenCondKigen = :KigenCondKigen " & vbCrLf & _
                                                ",UpdateDT = :UpdateDT " & vbCrLf & _
                                                ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                                ",UpdateID = :UpdateID " & vbCrLf & _
                                                "WHERE TemplateNmb = :TemplateNmb "

    'メールテンプレートマスター更新（UPDATE）SQL
    Private strDeleteMailTemplateSql As String = "UPDATE MAIL_TEMPLATE_MTB SET " & vbCrLf & _
                                                " JtiFlg = :JtiFlg " & vbCrLf & _
                                                ",UpdateDT = :UpdateDT " & vbCrLf & _
                                                ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                                ",UpdateID = :UpdateID " & vbCrLf & _
                                                "WHERE TemplateNmb = :TemplateNmb "

    'ひびきユーザーマスター取得（SELECT）SQL
    Private strSelectHBKUsrSql As String = "SELECT " & vbCrLf & _
                                                " hm.HBKUsrMailAdd " & vbCrLf & _
                                                "FROM hbkusr_mtb hm " & vbCrLf & _
                                                "WHERE hm.HBKUsrID = :HBKUsrID "

    '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
    Dim strSelectSapKikiTypeMastaSql As String = "SELECT " & vbCrLf & _
                                     " sm.SCKikiCD AS ID " & vbCrLf & _
                                     ",sm.SCKikiType AS Text " & vbCrLf & _
                                     "FROM SAP_KIKI_TYPE_MTB sm " & vbCrLf & _
                                     "WHERE sm.JtiFlg = '0' OR sm.SCKikiCD IN (SELECT KigenCondTypeKbn FROM mail_template_mtb WHERE TemplateNmb = :TemplateNmb ) " & vbCrLf & _
                                     "ORDER BY sm.JtiFlg , sm.Sort " & vbCrLf
    '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

    ''' <summary>
    ''' 【新規登録モード】新規テンプレート番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規テンプレート番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewTemplateNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                        ByVal Cn As NpgsqlConnection, _
                                                        ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_TEMPLATE_NO

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
    ''' 【編集モード】メールテンプレートマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMailTemplateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectMailTemplateSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("TemplateNmb").Value = dataHBKX0701.PropIntTemplateNmb
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
    ''' 【新規登録モード】メールテンプレートマスター新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertMailTemplateSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(INSERT)
            strSQL = strInsertMailTemplateSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))          'テンプレート番号
                .Add(New NpgsqlParameter("TemplateNM", NpgsqlTypes.NpgsqlDbType.Varchar))           'テンプレート名
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))              'グループCD
                .Add(New NpgsqlParameter("MailFrom", NpgsqlTypes.NpgsqlDbType.Varchar))             '差出人
                .Add(New NpgsqlParameter("MailTo", NpgsqlTypes.NpgsqlDbType.Varchar))               '宛先
                .Add(New NpgsqlParameter("CC", NpgsqlTypes.NpgsqlDbType.Varchar))                   'CC
                .Add(New NpgsqlParameter("Bcc", NpgsqlTypes.NpgsqlDbType.Varchar))                  'Bcc
                .Add(New NpgsqlParameter("PriorityKbn", NpgsqlTypes.NpgsqlDbType.Varchar))          '重要度
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                'タイトル
                .Add(New NpgsqlParameter("Text", NpgsqlTypes.NpgsqlDbType.Varchar))                 '本文
                .Add(New NpgsqlParameter("KigenCondCIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件CI種別
                .Add(New NpgsqlParameter("KigenCondTypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件タイプ
                .Add(New NpgsqlParameter("KigenCondKbn", NpgsqlTypes.NpgsqlDbType.Varchar))         '期限切れ条件区分
                .Add(New NpgsqlParameter("KigenCondKigen", NpgsqlTypes.NpgsqlDbType.Varchar))       '期限切れ条件期限
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
                .Parameters("TemplateNmb").Value = dataHBKX0701.PropIntTemplateNmb                  'テンプレート番号
                .Parameters("ProcessKbn").Value = dataHBKX0701.PropcmbProcessKbn.SelectedValue      'プロセス区分
                .Parameters("TemplateNM").Value = dataHBKX0701.ProptxtTemplateNM.Text               'テンプレート名
                .Parameters("GroupCD").Value = dataHBKX0701.PropGrpLoginUser.cmbGroup.SelectedValue 'グループCD
                .Parameters("MailFrom").Value = dataHBKX0701.ProptxtMailFrom.Text                   '差出人
                .Parameters("MailTo").Value = dataHBKX0701.ProptxtMailTo.Text                       '宛先
                .Parameters("CC").Value = dataHBKX0701.ProptxtCC.Text                               'CC
                .Parameters("Bcc").Value = dataHBKX0701.ProptxtBcc.Text                             'Bcc
                .Parameters("PriorityKbn").Value = dataHBKX0701.PropcmbPriorityKbn.SelectedValue    '重要度
                .Parameters("Title").Value = dataHBKX0701.ProptxtTitle.Text                         'タイトル
                .Parameters("Text").Value = dataHBKX0701.ProptxtText.Text                           '本文

                'インシデントが選択された場合
                If dataHBKX0701.PropcmbProcessKbn.SelectedValue = PROCESS_TYPE_INCIDENT Then

                    .Parameters("KigenCondCIKbnCD").Value = dataHBKX0701.PropcmbKigenCondCIKbnCD.SelectedValue  '期限切れ条件CI種別
                    .Parameters("KigenCondtypeKbn").Value = dataHBKX0701.PropcmbKigenCondTypeKbn.SelectedValue  '期限切れ条件タイプ

                    If dataHBKX0701.ProprdoKigenCondKbn.Checked = True Then                                     '期限切れ条件区分
                        .Parameters("KigenCondKbn").Value = KIGEN_KBN_OFF
                    ElseIf dataHBKX0701.ProprdoKigenCondUsrID.Checked = True Then
                        .Parameters("KigenCondKbn").Value = KIGEN_KBN_ON
                    Else
                        .Parameters("KigenCondKbn").Value = ""
                    End If

                    '[Mod] 2012/09/25 m.ibuki 期限条件変更START
                    If dataHBKX0701.ProprdoKigenCondKbn.Checked = True Then
                        .Parameters("KigenCondKigen").Value = dataHBKX0701.PropcmbKigenCondKigen.SelectedValue      '期限切れ条件期限
                    Else
                        .Parameters("KigenCondKigen").Value = ""
                    End If
                    '[Mod] 2012/09/25 m.ibuki 期限条件変更END
                Else
                    .Parameters("KigenCondCIKbnCD").Value = DBNull.Value
                    .Parameters("KigenCondtypeKbn").Value = DBNull.Value
                    .Parameters("KigenCondKbn").Value = DBNull.Value
                    .Parameters("KigenCondKigen").Value = DBNull.Value
                End If

                .Parameters("Sort").Value = DBNull.Value                                            '表示順
                .Parameters("JtiFlg").Value = DELETE_MODE_YUKO                                      '削除フラグ
                .Parameters("RegDT").Value = dataHBKX0701.PropDtmSysDate                            '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                     '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                             '登録者ID
                .Parameters("UpdateDT").Value = dataHBKX0701.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
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
    ''' 【編集モード】メールテンプレートマスター更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateMailtemplateSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(UPDATE)
            strSQL = strUpdateMailTemplateSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))          'テンプレート番号
                .Add(New NpgsqlParameter("TemplateNM", NpgsqlTypes.NpgsqlDbType.Varchar))           'テンプレート名
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))           'プロセス区分
                .Add(New NpgsqlParameter("MailFrom", NpgsqlTypes.NpgsqlDbType.Varchar))             '差出人
                .Add(New NpgsqlParameter("MailTo", NpgsqlTypes.NpgsqlDbType.Varchar))               '宛先
                .Add(New NpgsqlParameter("CC", NpgsqlTypes.NpgsqlDbType.Varchar))                   'CC
                .Add(New NpgsqlParameter("Bcc", NpgsqlTypes.NpgsqlDbType.Varchar))                  'Bcc
                .Add(New NpgsqlParameter("PriorityKbn", NpgsqlTypes.NpgsqlDbType.Varchar))          '重要度
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                'タイトル
                .Add(New NpgsqlParameter("Text", NpgsqlTypes.NpgsqlDbType.Varchar))                 '本文
                .Add(New NpgsqlParameter("KigenCondCIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件CI種別
                .Add(New NpgsqlParameter("KigenCondTypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))     '期限切れ条件タイプ
                .Add(New NpgsqlParameter("KigenCondKbn", NpgsqlTypes.NpgsqlDbType.Varchar))         '期限切れ条件区分
                .Add(New NpgsqlParameter("KigenCondKigen", NpgsqlTypes.NpgsqlDbType.Varchar))       '期限切れ条件期限
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '導入番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("TemplateNmb").Value = dataHBKX0701.PropIntTemplateNmb                  'テンプレート番号
                .Parameters("ProcessKbn").Value = dataHBKX0701.PropcmbProcessKbn.SelectedValue      'プロセス区分
                .Parameters("TemplateNM").Value = dataHBKX0701.ProptxtTemplateNM.Text               'テンプレート名
                .Parameters("MailFrom").Value = dataHBKX0701.ProptxtMailFrom.Text                   '差出人
                .Parameters("MailTo").Value = dataHBKX0701.ProptxtMailTo.Text                       '宛先
                .Parameters("CC").Value = dataHBKX0701.ProptxtCC.Text                               'CC
                .Parameters("Bcc").Value = dataHBKX0701.ProptxtBcc.Text                             'Bcc
                .Parameters("PriorityKbn").Value = dataHBKX0701.PropcmbPriorityKbn.SelectedValue    '重要度
                .Parameters("Title").Value = dataHBKX0701.ProptxtTitle.Text                         'タイトル
                .Parameters("Text").Value = dataHBKX0701.ProptxtText.Text                           '本文

                'インシデントが選択された場合
                If dataHBKX0701.PropcmbProcessKbn.SelectedValue = PROCESS_TYPE_INCIDENT Then

                    .Parameters("KigenCondCIKbnCD").Value = dataHBKX0701.PropcmbKigenCondCIKbnCD.SelectedValue  '期限切れ条件CI種別
                    .Parameters("KigenCondtypeKbn").Value = dataHBKX0701.PropcmbKigenCondTypeKbn.SelectedValue  '期限切れ条件タイプ

                    If dataHBKX0701.ProprdoKigenCondKbn.Checked = True Then                                     '期限切れ条件区分
                        If dataHBKX0701.PropcmbKigenCondCIKbnCD.SelectedValue <> "" Then
                            .Parameters("KigenCondKbn").Value = KIGEN_KBN_OFF
                        Else
                            .Parameters("KigenCondKbn").Value = ""
                        End If
                    ElseIf dataHBKX0701.ProprdoKigenCondUsrID.Checked = True Then
                        .Parameters("KigenCondKbn").Value = KIGEN_KBN_ON
                    Else
                        .Parameters("KigenCondKbn").Value = ""
                    End If

                    '[Mod] 2012/09/25 m.ibuki 期限条件変更START
                    If dataHBKX0701.ProprdoKigenCondKbn.Checked = True Then
                        .Parameters("KigenCondKigen").Value = dataHBKX0701.PropcmbKigenCondKigen.SelectedValue      '期限切れ条件期限
                    Else
                        .Parameters("KigenCondKigen").Value = ""
                    End If
                    '[Mod] 2012/09/25 m.ibuki 期限条件変更END

                Else
                    .Parameters("KigenCondCIKbnCD").Value = DBNull.Value
                    .Parameters("KigenCondtypeKbn").Value = DBNull.Value
                    .Parameters("KigenCondKbn").Value = DBNull.Value
                    .Parameters("KigenCondKigen").Value = DBNull.Value
                End If

                .Parameters("UpdateDT").Value = dataHBKX0701.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
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
    ''' 【編集モード（削除・削除解除モード）】メールテンプレートマスター更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メールテンプレートマスター更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetDeleteMailTemplateSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try
            'SQL文(UPDATE)
            strSQL = strDeleteMailTemplateSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("JtiFlg", NpgsqlTypes.NpgsqlDbType.Varchar))               '削除フラグ
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))          'テンプレート番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("JtiFlg").Value = dataHBKX0701.PropStrJtiFlg                            '削除フラグ
                .Parameters("UpdateDT").Value = dataHBKX0701.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("TemplateNmb").Value = dataHBKX0701.PropIntTemplateNmb                  'テンプレート番号
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
    ''' ひびきユーザーマスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ひびきユーザーマスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectHBKUsrSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectHBKUsrSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HBKUsrID").Value = dataHBKX0701.PropDtResultSub.Rows(0).Item(0)
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

    '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 START
    ''' <summary>
    ''' サポセン機器タイプマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKX0701">[IN]メールテンプレートマスター登録画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器タイプコンボボックス用データを取得する
    ''' <para>作成情報：2013/03/19
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSapKikiTypeMastaDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKX0701 As DataHBKX0701) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectSapKikiTypeMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("TemplateNmb", NpgsqlTypes.NpgsqlDbType.Integer))          'テンプレート番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("TemplateNmb").Value = dataHBKX0701.PropIntTemplateNmb                  'テンプレート番号
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
    '[mod] 2013/03/19 y.ikushima マスタデータ削除フラグ対応 END

End Class
