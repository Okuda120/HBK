Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 一括廃棄画面Sqlクラス
''' </summary>
''' <remarks>一括廃棄画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/04 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB1105

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '*************************
    '* SQL文宣言
    '*************************

    '種別コンボボックスデータ取得（SELECT）SQL（003（サポセン機器）固定）
    Private strSelectSyubetsuCmbSql As String = "SELECT " & vbCrLf & _
                                                " KindCD " & vbCrLf & _
                                                ",KindNM " & vbCrLf & _
                                                "FROM KIND_MTB " & vbCrLf & _
                                                "WHERE JtiFlg = '0' " & vbCrLf & _
                                                "AND CIKbnCD = :CIKbnCD " & vbCrLf & _
                                                "ORDER BY Sort"

    'CIステータスコンボボックスデータ取得（SELECT）SQL
    Private StrSelectStatusCmbSql As String = "SELECT " & vbCrLf & _
                                                " CM.CIStateCD " & vbCrLf & _
                                                ",CM.CIStateNM " & vbCrLf & _
                                                "FROM CIState_MTB CM " & vbCrLf & _
                                                "WHERE CM.JtiFlg = '0' " & vbCrLf & _
                                                "AND CM.CIKbnCD = :CIKbnCD " & vbCrLf & _
                                                "AND CM.CIStateCD IN(:StrStateCD1, :StrStateCD2) " & vbCrLf & _
                                                "ORDER BY CM.Sort"

    '種別コード＋番号ステータスデータ取得（SELECT）SQL
    Private StrSelectSyuBetsuNumStatusSql As String = "SELECT" & vbCrLf & _
                                                                                    "CIT.CIStatusCD,CSM.CIStateNM" & vbCrLf & _
                                                                                " FROM CI_INFO_TB CIT " & vbCrLf & _
                                                                                " LEFT OUTER JOIN cistate_mtb CSM ON CIT.CIStatusCD = CSM.CIStateCD " & vbCrLf & _
                                                                                " WHERE CIT.KindCD || CIT.Num = :StrSyubetsuNum"

    '種別コード＋番号ロックデータ取得（SELECT）SQL
    Private strSelectSyuBetsuNumLockSql As String = "SELECT " & vbCrLf & _
                                                    " EdiTime " & vbCrLf & _
                                                    "FROM CI_LOCK_TB " & vbCrLf & _
                                                    "WHERE KindCD ||Num = :StrSyubetsuNum"

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    'CI共通情報更新（UPDATE）SQL
    Private strUpdateCIInfoSql As String = "UPDATE CI_INFO_TB SET " & vbCrLf & _
                                            "UpdateDT = :UpdateDT " & vbCrLf & _
                                            ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                            ",UpdateID = :UpdateID "

    'CIサポセン機器更新（UPDATE）SQL
    Private strUpdateCISapSql As String = "UPDATE CI_SAP_TB SET " & vbCrLf & _
                                            "WorkFromNmb = '' " & vbCrLf & _
                                            ",UpdateDT = :UpdateDT " & vbCrLf & _
                                            ",UpGrpCD = :UpGrpCD " & vbCrLf & _
                                            ",UpdateID = :UpdateID "

    '新規履歴番号取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo " & vbCrLf & _
                                                "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                "WHERE KindCD ||Num = :StrSyubetsuNum"

    'CI共通情報履歴新規登録（INSERT）SQL
    Private strInsertCIInfoRSql As String = "INSERT INTO CI_INFO_RTB ( " & vbCrLf & _
                                            " CINmb " & vbCrLf & _
                                            ",RirekiNo " & vbCrLf & _
                                            ",CIKbnCD " & vbCrLf & _
                                            ",KindCD " & vbCrLf & _
                                            ",Num " & vbCrLf & _
                                            ",CIStatusCD " & vbCrLf & _
                                            ",Class1 " & vbCrLf & _
                                            ",Class2 " & vbCrLf & _
                                            ",CINM " & vbCrLf & _
                                            ",CIOwnerCD " & vbCrLf & _
                                            ",Sort " & vbCrLf & _
                                            ",CINaiyo " & vbCrLf & _
                                            ",BIko1 " & vbCrLf & _
                                            ",Biko2 " & vbCrLf & _
                                            ",Biko3 " & vbCrLf & _
                                            ",Biko4 " & vbCrLf & _
                                            ",Biko5 " & vbCrLf & _
                                            ",FreeFlg1 " & vbCrLf & _
                                            ",FreeFlg2 " & vbCrLf & _
                                            ",FreeFlg3 " & vbCrLf & _
                                            ",FreeFlg4 " & vbCrLf & _
                                            ",FreeFlg5 " & vbCrLf & _
                                            ",Class1Aimai " & vbCrLf & _
                                            ",Class2Aimai " & vbCrLf & _
                                            ",CINMAimai " & vbCrLf & _
                                            ",FreeWordAimai " & vbCrLf & _
                                            ",BikoAimai " & vbCrLf & _
                                            ",RegDT " & vbCrLf & _
                                            ",RegGrpCD " & vbCrLf & _
                                            ",RegID " & vbCrLf & _
                                            ",UpdateDT " & vbCrLf & _
                                            ",UpGrpCD " & vbCrLf & _
                                            ",UpdateID " & vbCrLf & _
                                            ") " & vbCrLf & _
                                            "SELECT " & vbCrLf & _
                                            " CIT.CINmb " & vbCrLf & _
                                            ",HBKF0002(CINmb,1) AS RirekiNo " & vbCrLf & _
                                            ",CIT.CIKbnCD " & vbCrLf & _
                                            ",CIT.KindCD " & vbCrLf & _
                                            ",CIT.Num " & vbCrLf & _
                                            ",CIT.CIStatusCD " & vbCrLf & _
                                            ",CIT.Class1 " & vbCrLf & _
                                            ",CIT.Class2 " & vbCrLf & _
                                            ",CIT.CINM " & vbCrLf & _
                                            ",CIT.CIOwnerCD " & vbCrLf & _
                                            ",CIT.Sort " & vbCrLf & _
                                            ",CIT.CINaiyo " & vbCrLf & _
                                            ",CIT.BIko1 " & vbCrLf & _
                                            ",CIT.Biko2 " & vbCrLf & _
                                            ",CIT.Biko3 " & vbCrLf & _
                                            ",CIT.Biko4 " & vbCrLf & _
                                            ",CIT.Biko5 " & vbCrLf & _
                                            ",CIT.FreeFlg1 " & vbCrLf & _
                                            ",CIT.FreeFlg2 " & vbCrLf & _
                                            ",CIT.FreeFlg3 " & vbCrLf & _
                                            ",CIT.FreeFlg4 " & vbCrLf & _
                                            ",CIT.FreeFlg5 " & vbCrLf & _
                                            ",CIT.Class1Aimai " & vbCrLf & _
                                            ",CIT.Class2Aimai " & vbCrLf & _
                                            ",CIT.CINMAimai " & vbCrLf & _
                                            ",CIT.FreeWordAimai " & vbCrLf & _
                                            ",CIT.BikoAimai " & vbCrLf & _
                                            ",CIT.RegDT " & vbCrLf & _
                                            ",CIT.RegGrpCD " & vbCrLf & _
                                            ",CIT.RegID " & vbCrLf & _
                                            ",CIT.UpdateDT " & vbCrLf & _
                                            ",CIT.UpGrpCD " & vbCrLf & _
                                            ",CIT.UpdateID " & vbCrLf & _
                                            "FROM CI_INFO_TB CIT " & vbCrLf & _
                                            "WHERE CIT.CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum) "

    'CIサポセン機器履歴新規登録（INSERT）SQL
    Private strInsertCISapRSql As String = "INSERT INTO CI_SAP_RTB ( " & vbCrLf & _
                                           " CINmb " & vbCrLf & _
                                           ",RirekiNo " & vbCrLf & _
                                           ",MemorySize " & vbCrLf & _
                                           ",Kataban " & vbCrLf & _
                                           ",Serial " & vbCrLf & _
                                           ",MacAddress1 " & vbCrLf & _
                                           ",MacAddress2 " & vbCrLf & _
                                           ",Fuzokuhin " & vbCrLf & _
                                           ",TypeKbn " & vbCrLf & _
                                           ",SCKikiFixNmb " & vbCrLf & _
                                           ",KikiState " & vbCrLf & _
                                           ",ImageNmb " & vbCrLf & _
                                           ",IntroductNmb " & vbCrLf & _
                                           ",LeaseUpDT " & vbCrLf & _
                                           ",SCHokanKbn " & vbCrLf & _
                                           ",LastInfoDT " & vbCrLf & _
                                           ",ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb " & vbCrLf & _
                                           ",KikiUseCD " & vbCrLf & _
                                           ",IPUseCD " & vbCrLf & _
                                           ",FixedIP " & vbCrLf & _
                                           ",UsrID " & vbCrLf & _
                                           ",UsrNM " & vbCrLf & _
                                           ",UsrCompany " & vbCrLf & _
                                           ",UsrKyokuNM " & vbCrLf & _
                                           ",UsrBusyoNM " & vbCrLf & _
                                           ",UsrTel " & vbCrLf & _
                                           ",UsrMailAdd " & vbCrLf & _
                                           ",UsrContact " & vbCrLf & _
                                           ",UsrRoom " & vbCrLf & _
                                           ",RentalStDT " & vbCrLf & _
                                           ",RentalEdDT " & vbCrLf & _
                                           ",SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM " & vbCrLf & _
                                           ",SetRoom " & vbCrLf & _
                                           ",SetBuil " & vbCrLf & _
                                           ",SetFloor " & vbCrLf & _
                                           ",SetDeskNo " & vbCrLf & _
                                           ",SetLANLength " & vbCrLf & _
                                           ",SetLANNum " & vbCrLf & _
                                           ",SetSocket " & vbCrLf & _
                                           ",SerialAimai " & vbCrLf & _
                                           ",ImageNmbAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai " & vbCrLf & _
                                           ",SetBuilAimai " & vbCrLf & _
                                           ",SetFloorAimai " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") " & vbCrLf & _
                                           "SELECT " & vbCrLf & _
                                            " CST.CINmb " & vbCrLf & _
                                            ",HBKF0002(CINmb,0) AS RirekiNo " & vbCrLf & _
                                            ",CST.MemorySize " & vbCrLf & _
                                            ",CST.Kataban " & vbCrLf & _
                                            ",CST.Serial " & vbCrLf & _
                                            ",CST.MacAddress1 " & vbCrLf & _
                                            ",CST.MacAddress2 " & vbCrLf & _
                                            ",CST.Fuzokuhin " & vbCrLf & _
                                            ",CST.TypeKbn " & vbCrLf & _
                                            ",CST.SCKikiFixNmb " & vbCrLf & _
                                            ",CST.KikiState " & vbCrLf & _
                                            ",CST.ImageNmb " & vbCrLf & _
                                            ",CST.IntroductNmb " & vbCrLf & _
                                            ",CST.LeaseUpDT " & vbCrLf & _
                                            ",CST.SCHokanKbn " & vbCrLf & _
                                            ",CST.LastInfoDT " & vbCrLf & _
                                            ",CST.ManageKyokuNM " & vbCrLf & _
                                            ",CST.ManageBusyoNM " & vbCrLf & _
                                            ",CST.WorkFromNmb " & vbCrLf & _
                                            ",CST.KikiUseCD " & vbCrLf & _
                                            ",CST.IPUseCD " & vbCrLf & _
                                            ",CST.FixedIP " & vbCrLf & _
                                            ",CST.UsrID " & vbCrLf & _
                                            ",CST.UsrNM " & vbCrLf & _
                                            ",CST.UsrCompany " & vbCrLf & _
                                            ",CST.UsrKyokuNM " & vbCrLf & _
                                            ",CST.UsrBusyoNM " & vbCrLf & _
                                            ",CST.UsrTel " & vbCrLf & _
                                            ",CST.UsrMailAdd " & vbCrLf & _
                                            ",CST.UsrContact " & vbCrLf & _
                                            ",CST.UsrRoom " & vbCrLf & _
                                            ",CST.RentalStDT " & vbCrLf & _
                                            ",CST.RentalEdDT " & vbCrLf & _
                                            ",CST.SetKyokuNM " & vbCrLf & _
                                            ",CST.SetBusyoNM " & vbCrLf & _
                                            ",CST.SetRoom " & vbCrLf & _
                                            ",CST.SetBuil " & vbCrLf & _
                                            ",CST.SetFloor " & vbCrLf & _
                                            ",CST.SetDeskNo " & vbCrLf & _
                                            ",CST.SetLANLength " & vbCrLf & _
                                            ",CST.SetLANNum " & vbCrLf & _
                                            ",CST.SetSocket " & vbCrLf & _
                                            ",CST.SerialAimai " & vbCrLf & _
                                            ",CST.ImageNmbAimai " & vbCrLf & _
                                            ",CST.ManageBusyoNMAimai " & vbCrLf & _
                                            ",CST.UsrIDAimai " & vbCrLf & _
                                            ",CST.SetBusyoNMAimai " & vbCrLf & _
                                            ",CST.SetRoomAimai " & vbCrLf & _
                                            ",CST.SetBuilAimai " & vbCrLf & _
                                            ",CST.SetFloorAimai " & vbCrLf & _
                                            ",CST.RegDT " & vbCrLf & _
                                            ",CST.RegGrpCD " & vbCrLf & _
                                            ",CST.RegID " & vbCrLf & _
                                            ",CST.UpdateDT " & vbCrLf & _
                                            ",CST.UpGrpCD " & vbCrLf & _
                                            ",CST.UpdateID " & vbCrLf & _
                                            "FROM CI_SAP_TB CST " & vbCrLf & _
                                            "WHERE CST.CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum) "

    '登録理由履歴新規登録（INSERT）SQL
    Private strInsertRegReasonRSql As String = "INSERT INTO REGREASON_RTB ( " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",RirekiNo " & vbCrLf & _
                                                ",RegReason " & vbCrLf & _
                                                ",WorkCD " & vbCrLf & _
                                                ",WorkKbnCD " & vbCrLf & _
                                                ",RegDT " & vbCrLf & _
                                                ",RegGrpCD " & vbCrLf & _
                                                ",RegID " & vbCrLf & _
                                                ",UpdateDT " & vbCrLf & _
                                                ",UpGrpCD " & vbCrLf & _
                                                ",UpdateID " & vbCrLf & _
                                                ") " & vbCrLf & _
                                                "SELECT " & vbCrLf & _
                                                " CINmb " & vbCrLf & _
                                                ",HBKF0002(CINmb,0) AS RirekiNo " & vbCrLf & _
                                                ",:RegReason " & vbCrLf & _
                                                ",'" & WORK_CD_PACKAGE & "'" & vbCrLf & _
                                                ",'" & WORK_KBN_CD_COMPLETE & "'" & vbCrLf & _
                                                ",:RegDT " & vbCrLf & _
                                                ",:RegGrpCD " & vbCrLf & _
                                                ",:RegID " & vbCrLf & _
                                                ",:UpdateDT " & vbCrLf & _
                                                ",:UpGrpCD " & vbCrLf & _
                                                ",:UpdateID " & vbCrLf & _
                                                "FROM CI_INFO_TB WHERE CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum) "

    '原因リンク履歴新規登録（INSERT）SQL
    Private strInsertCauseLinkRSql As String = "INSERT INTO CAUSELINK_RTB ( " & vbCrLf & _
                                               " CINmb " & vbCrLf & _
                                               ",RirekiNo " & vbCrLf & _
                                               ",ProcessKbn " & vbCrLf & _
                                               ",MngNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " CINmb " & vbCrLf & _
                                               ",HBKF0002(CINmb,0) AS RirekiNo " & vbCrLf & _
                                               ",:ProcessKbn " & vbCrLf & _
                                               ",:MngNmb " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT" & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               "FROM CI_INFO_TB WHERE CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum) "

    ''' <summary>
    ''' 種別コンボボックスデータ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>スプレッドの種別コンボボックスデータ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSyuBetsueCmb(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(SELECT)
            strSQL = StrSelectSyubetsuCmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = dataHBKB1105.PropStrCIKbnCD

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
    ''' ステータスコンボボックスデータ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>スプレッドのタイプコンボボックスデータ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectStatusCmb(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(SELECT)
            strSQL = StrSelectStatusCmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("CIKbnCD").Value = dataHBKB1105.PropStrCIKbnCD
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("StrStateCD1", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("StrStateCD1").Value = CI_STATUS_SUPORT_HAIKIZUMI
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("StrStateCD2", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("StrStateCD2").Value = CI_STATUS_SUPORT_REUSE

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
    ''' 種別コード＋番号ステータスチェック用データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別コード＋番号ステータスチェック用データ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSyuBetsuNumStatus(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(SELECT)
            strSQL = StrSelectSyuBetsuNumStatusSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum

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
    ''' 種別コード＋番号ロックチェック用データ取得処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN/OUT]一括廃棄画面Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別コード＋番号ロックチェック用データ取得のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSyuBetsuNumLock(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(SELECT)
            strSQL = strSelectSyuBetsuNumLockSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値をセット
            Adapter.SelectCommand.Parameters.Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))
            Adapter.SelectCommand.Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum

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
    ''' 【編集／履歴モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

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
    ''' 【編集／参照／履歴モード】CI共通情報更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCIInfoSql

            If dataHBKB1105.PropStrCIStatusCD <> "" Then
                strSQL &= ",CIStatusCD = :CIStatusCD " & vbCrLf
            End If
            strSQL &= "WHERE CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum) "
            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                If dataHBKB1105.PropStrCIStatusCD <> "" Then
                    .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))       'ステータスCD
                End If
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKB1105.PropStrCIStatusCD <> "" Then
                    .Parameters("CIStatusCD").Value = dataHBKB1105.PropStrCIStatusCD        'ステータスCD
                End If
                .Parameters("UpdateDT").Value = dataHBKB1105.PropDtmSysDate             '最終更新日時'ステータスCD
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                              '最終更新者ID
                .Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum   '種別CD＋番号
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
    ''' 【編集／参照／履歴モード】CIシステム更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateCISapSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateCISapSql

            If dataHBKB1105.PropStrKikiState <> "" Then
                strSQL &= ",KikiState = :KikiState " & vbCrLf
            End If

            strSQL &= "WHERE CINmb IN (SELECT CINmb FROM CI_INFO_TB WHERE KindCD ||Num = :StrSyubetsuNum)  "

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                If dataHBKB1105.PropStrKikiState <> "" Then
                    .Add(New NpgsqlParameter("KikiState", NpgsqlTypes.NpgsqlDbType.Varchar))        '機器状態
                End If
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
            End With

            'バインド変数に値をセット
            With Cmd
                If dataHBKB1105.PropStrKikiState <> "" Then
                    .Parameters("KikiState").Value = dataHBKB1105.PropStrKikiState          '機器状態
                End If
                .Parameters("UpdateDT").Value = dataHBKB1105.PropDtmSysDate             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                              '最終更新者ID
                .Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum   '種別CD＋番号
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
    ''' 【共通】CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum       '種別CD＋番号
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
    ''' 【共通】CIサポセン機器履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISapRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCISapRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum       '種別CD＋番号
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
    ''' 【共通】登録理由履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="DataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertRegReasonRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録理由
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("RegReason").Value = dataHBKB1105.PropStrRegReason              '登録理由
                .Parameters("StrSyubetsuNum").Value = dataHBKB1105.PropStrSyubetsuNum       '種別CD＋番号
                .Parameters("RegDT").Value = dataHBKB1105.PropDtmSysDate                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB1105.PropDtmSysDate                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                  '最終更新者ID
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
    ''' 【共通】原因リンク履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="DataHBKB1105">[IN]一括廃棄画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/04 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal DataHBKB1105 As DataHBKB1105) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCauseLinkRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '管理番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("StrSyubetsuNum", NpgsqlTypes.NpgsqlDbType.Varchar))   '種別CD＋番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("MngNmb").Value = DataHBKB1105.PropRowReg.Item("MngNmb")            '管理番号
                .Parameters("ProcessKbn").Value = DataHBKB1105.PropRowReg.Item("ProcessKbn")    'プロセス区分
                .Parameters("StrSyubetsuNum").Value = DataHBKB1105.PropStrSyubetsuNum           '種別CD＋番号
                .Parameters("RegDT").Value = DataHBKB1105.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = DataHBKB1105.PropDtmSysDate                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                      '最終更新者ID
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
