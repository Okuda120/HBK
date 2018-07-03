Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 一括登録　部所有機器SQLクラス
''' </summary>
''' <remarks>一括登録　部所有機器のSQLの作成・設定を行う
''' <para>作成情報：2012/07/19 k.ueda
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0204

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    '* SQL文宣言


    '番号取得処理(SELECT)SQL
    Private strSelectNumSql As String = "SELECT COUNT(*) " & vbCrLf & _
                                        "FROM CI_INFO_TB " & vbCrLf & _
                                        "WHERE Num = :Num" & vbCrLf & _
                                        "  AND CIKbnCD = :CIKbnCD" & vbCrLf


    'ステータスコード取得処理（SELECT）SQL
    Private strSelectConvertStatusSql As String = "  SELECT CIStateCD " & vbCrLf & _
                                                                    "FROM CISTATE_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND CIKbnCD = :CIKbnCD " & vbCrLf & _
                                                                        "AND CIStateNM = :CIStateNM"
    'CIオーナーCD(グループCD)取得処理(SELECT)SQL
    Private strSelectCIOwnerCDSql As String = " SELECT GroupCD " & vbCrLf & _
                                                                        "FROM GRP_MTB" & vbCrLf & _
                                                                        "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                            "AND GroupCD = :GroupCD"
    'OSソフトコード取得処理(SELECT)SQL
    Private strSelectConvertOSSoftSql As String = " SELECT SoftCD " & vbCrLf & _
                                                                    "FROM SOFT_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND SoftKbn  = :SoftKbnOS " & vbCrLf & _
                                                                        "AND SoftNM = :SoftNMOS"
    'ウイルス対策ソフトコード取得処理(SELECT)SQL
    Private strSelectConvertAntiVirusSoftSql As String = " SELECT SoftCD " & vbCrLf & _
                                                                    "FROM SOFT_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND SoftKbn  = :SoftKbnAntiVirus " & vbCrLf & _
                                                                        "AND SoftNM = :SoftNMAntivirus"
    'DNS登録(機器ステータスコード)取得処理(SELECT文)
    Private strSelectConvertDNSRegSql As String = " SELECT KikiStateCD " & vbCrLf & _
                                                                    "FROM KIKISTATE_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND KikiStateKbn  = :KikiStateKbnDNS " & vbCrLf & _
                                                                        "AND KikiStateNM = :KikiStateNMDNS"
    'IP割当種類(機器ステータスコード)取得処理(SELECT文)
    Private strSelectConvertIPUseSql As String = " SELECT KikiStateCD " & vbCrLf & _
                                                                    "FROM KIKISTATE_MTB" & vbCrLf & _
                                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                                        "AND KikiStateKbn  = :KikiStateKbnIP " & vbCrLf & _
                                                                        "AND KikiStateNM = :KikiStateNMIP"

    'CI共通情報新規登録（INSERT）SQL
    Private strInsertCIInfoSql As String = "INSERT INTO CI_INFO_TB" & vbCrLf & _
                                                            "(" & vbCrLf & _
                                                                " CINmb " & vbCrLf & _
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
                                                            "VALUES " & vbCrLf & _
                                                            "(" & vbCrLf & _
                                                               " :CINmb " & vbCrLf & _
                                                               ",:CIKbnCD " & vbCrLf & _
                                                               ",'401' " & vbCrLf & _
                                                               ",LPAD(:Num, 5, '0') " & vbCrLf & _
                                                               ",:CIStatusCD " & vbCrLf & _
                                                               ",:Class1 " & vbCrLf & _
                                                               ",:Class2 " & vbCrLf & _
                                                               ",:CINM " & vbCrLf & _
                                                               ",:CIOwnerCD " & vbCrLf & _
                                                               ",(SELECT COALESCE(MAX(ct.Sort),0)+1 FROM CI_INFO_TB ct WHERE ct.CIKbnCD=:CIKbnCD) " & vbCrLf & _
                                                               ",:CINaiyo " & vbCrLf & _
                                                               ",:BIko1 " & vbCrLf & _
                                                               ",:Biko2 " & vbCrLf & _
                                                               ",:Biko3 " & vbCrLf & _
                                                               ",:Biko4 " & vbCrLf & _
                                                               ",:Biko5 " & vbCrLf & _
                                                               ",:FreeFlg1 " & vbCrLf & _
                                                               ",:FreeFlg2 " & vbCrLf & _
                                                               ",:FreeFlg3 " & vbCrLf & _
                                                               ",:FreeFlg4 " & vbCrLf & _
                                                               ",:FreeFlg5 " & vbCrLf & _
                                                               ",:Class1Aimai " & vbCrLf & _
                                                               ",:Class2Aimai " & vbCrLf & _
                                                               ",:CINMAimai " & vbCrLf & _
                                                               ",:FreeWordAimai " & vbCrLf & _
                                                               ",:BikoAimai " & vbCrLf & _
                                                               ",:RegDT " & vbCrLf & _
                                                               ",:RegGrpCD " & vbCrLf & _
                                                               ",:RegID " & vbCrLf & _
                                                               ",:UpdateDT " & vbCrLf & _
                                                               ",:UpGrpCD " & vbCrLf & _
                                                               ",:UpdateID " & vbCrLf & _
                                                            ") "

    'CI共通情報履歴新規登録（INSERT）SQL
    Private strInsertCIInfoRSql As String = "INSERT INTO CI_INFO_RTB " & vbCrLf & _
                                                                "( " & vbCrLf & _
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
                                                                           " ct.CINmb " & vbCrLf & _
                                                                           ",:RirekiNo " & vbCrLf & _
                                                                           ",ct.CIKbnCD " & vbCrLf & _
                                                                           ",ct.KindCD " & vbCrLf & _
                                                                           ",ct.Num " & vbCrLf & _
                                                                           ",ct.CIStatusCD " & vbCrLf & _
                                                                           ",ct.Class1 " & vbCrLf & _
                                                                           ",ct.Class2 " & vbCrLf & _
                                                                           ",ct.CINM " & vbCrLf & _
                                                                           ",ct.CIOwnerCD " & vbCrLf & _
                                                                           ",ct.Sort " & vbCrLf & _
                                                                           ",ct.CINaiyo " & vbCrLf & _
                                                                           ",ct.BIko1 " & vbCrLf & _
                                                                           ",ct.Biko2 " & vbCrLf & _
                                                                           ",ct.Biko3 " & vbCrLf & _
                                                                           ",ct.Biko4 " & vbCrLf & _
                                                                           ",ct.Biko5 " & vbCrLf & _
                                                                           ",ct.FreeFlg1 " & vbCrLf & _
                                                                           ",ct.FreeFlg2 " & vbCrLf & _
                                                                           ",ct.FreeFlg3 " & vbCrLf & _
                                                                           ",ct.FreeFlg4 " & vbCrLf & _
                                                                           ",ct.FreeFlg5 " & vbCrLf & _
                                                                           ",ct.Class1Aimai " & vbCrLf & _
                                                                           ",ct.Class2Aimai " & vbCrLf & _
                                                                           ",ct.CINMAimai " & vbCrLf & _
                                                                           ",ct.FreeWordAimai " & vbCrLf & _
                                                                           ",ct.BikoAimai " & vbCrLf & _
                                                                           ",ct.UpdateDT " & vbCrLf & _
                                                                           ",ct.UpGrpCD " & vbCrLf & _
                                                                           ",ct.UpdateID " & vbCrLf & _
                                                                           ",ct.UpdateDT " & vbCrLf & _
                                                                           ",ct.UpGrpCD " & vbCrLf & _
                                                                           ",ct.UpdateID " & vbCrLf & _
                                                                       "FROM CI_INFO_TB ct " & vbCrLf & _
                                                                       "WHERE ct.CINmb=:CINmb "

    'CI部所有機器新規登録（INSERT）SQL
    Private strInsertCIBuySql As String = "INSERT INTO CI_BUY_TB ( " & vbCrLf & _
                                           " CINmb " & vbCrLf & _
                                           ",Kataban " & vbCrLf & _
                                           ",Aliau " & vbCrLf & _
                                           ",Serial " & vbCrLf & _
                                           ",MacAddress1" & vbCrLf & _
                                           ",MacAddress2 " & vbCrLf & _
                                           ",ZooKbn " & vbCrLf & _
                                           ",OSNM " & vbCrLf & _
                                           ",AntiVirusSoftNM " & vbCrLf & _
                                           ",DNSRegCD " & vbCrLf & _
                                           ",NIC1 " & vbCrLf & _
                                           ",NIC2 " & vbCrLf & _
                                           ",ConnectDT " & vbCrLf & _
                                           ",ExpirationDT " & vbCrLf & _
                                           ",DeletDT " & vbCrLf & _
                                           ",LastInfoDT " & vbCrLf & _
                                           ",ConectReason " & vbCrLf & _
                                           ",ExpirationUPDT " & vbCrLf & _
                                           ",InfoDT " & vbCrLf & _
                                           ",NumInfoKbn " & vbCrLf & _
                                           ",SealSendkbn " & vbCrLf & _
                                           ",AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",AntiVirusSofCheckDT " & vbCrLf & _
                                           ",BusyoKikiBiko " & vbCrLf & _
                                           ",ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb " & vbCrLf & _
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
                                           ",SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM " & vbCrLf & _
                                           ",SetRoom " & vbCrLf & _
                                           ",SetBuil " & vbCrLf & _
                                           ",SetFloor " & vbCrLf & _
                                           ",SerialAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai " & vbCrLf & _
                                           ",UsrBusyoNMAimai " & vbCrLf & _
                                           ",SetBusyoNMAimai " & vbCrLf & _
                                           ",SetRoomAimai " & vbCrLf & _
                                           ",SetBuilaimai" & vbCrLf & _
                                           ",SetFloorAimai " & vbCrLf & _
                                           ",RegDT " & vbCrLf & _
                                           ",RegGrpCD " & vbCrLf & _
                                           ",RegID " & vbCrLf & _
                                           ",UpdateDT " & vbCrLf & _
                                           ",UpGrpCD " & vbCrLf & _
                                           ",UpdateID " & vbCrLf & _
                                           ") " & vbCrLf & _
                                           "VALUES ( " & vbCrLf & _
                                           " :CINmb " & vbCrLf & _
                                           ",:Kataban " & vbCrLf & _
                                           ",:Aliau " & vbCrLf & _
                                           ",:Serial " & vbCrLf & _
                                           ",:MacAddress1" & vbCrLf & _
                                           ",:MacAddress2 " & vbCrLf & _
                                           ",:ZooKbn " & vbCrLf & _
                                           ",:OSNM " & vbCrLf & _
                                           ",:AntiVirusSoftNM " & vbCrLf & _
                                           ",:DNSRegCD " & vbCrLf & _
                                           ",:NIC1 " & vbCrLf & _
                                           ",:NIC2 " & vbCrLf & _
                                           ",:ConnectDT " & vbCrLf & _
                                           ",:ExpirationDT " & vbCrLf & _
                                           ",:DeletDT " & vbCrLf & _
                                           ",:LastInfoDT " & vbCrLf & _
                                           ",:ConectReason " & vbCrLf & _
                                           ",:ExpirationUPDT " & vbCrLf & _
                                           ",:InfoDT " & vbCrLf & _
                                           ",:NumInfoKbn " & vbCrLf & _
                                           ",:SealSendkbn " & vbCrLf & _
                                           ",:AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",:AntiVirusSofCheckDT " & vbCrLf & _
                                           ",:BusyoKikiBiko " & vbCrLf & _
                                           ",:ManageKyokuNM " & vbCrLf & _
                                           ",:ManageBusyoNM " & vbCrLf & _
                                           ",:WorkFromNmb " & vbCrLf & _
                                           ",:IPUseCD " & vbCrLf & _
                                           ",:FixedIP " & vbCrLf & _
                                           ",:UsrID " & vbCrLf & _
                                           ",:UsrNM " & vbCrLf & _
                                           ",:UsrCompany " & vbCrLf & _
                                           ",:UsrKyokuNM " & vbCrLf & _
                                           ",:UsrBusyoNM " & vbCrLf & _
                                           ",:UsrTel " & vbCrLf & _
                                           ",:UsrMailAdd " & vbCrLf & _
                                           ",:UsrContact " & vbCrLf & _
                                           ",:UsrRoom " & vbCrLf & _
                                           ",:SetKyokuNM " & vbCrLf & _
                                           ",:SetBusyoNM " & vbCrLf & _
                                           ",:SetRoom " & vbCrLf & _
                                           ",:SetBuil " & vbCrLf & _
                                           ",:SetFloor " & vbCrLf & _
                                           ",:SerialAimai " & vbCrLf & _
                                           ",:ManageBusyoNMAimai " & vbCrLf & _
                                           ",:UsrIDAimai " & vbCrLf & _
                                           ",:UsrBusyoNMAimai " & vbCrLf & _
                                           ",:SetBusyoNMAimai " & vbCrLf & _
                                           ",:SetRoomAimai " & vbCrLf & _
                                           ",:SetBuilAimai " & vbCrLf & _
                                           ",:SetFloorAimai " & vbCrLf & _
                                           ",:RegDT " & vbCrLf & _
                                           ",:RegGrpCD " & vbCrLf & _
                                           ",:RegID " & vbCrLf & _
                                           ",:UpdateDT " & vbCrLf & _
                                           ",:UpGrpCD " & vbCrLf & _
                                           ",:UpdateID " & vbCrLf & _
                                           ") "


    'CI部所有機器履歴テーブルinsert
    Private strInsertCIBuyRSql As String = "INSERT INTO CI_BUY_RTB ( " & vbCrLf & _
                                           " CINmb " & vbCrLf & _
                                           ",RirekiNo " & vbCrLf & _
                                           ",Kataban " & vbCrLf & _
                                           ",Aliau " & vbCrLf & _
                                           ",Serial " & vbCrLf & _
                                           ",MacAddress1" & vbCrLf & _
                                           ",MacAddress2 " & vbCrLf & _
                                           ",ZooKbn " & vbCrLf & _
                                           ",OSNM " & vbCrLf & _
                                           ",AntiVirusSoftNM " & vbCrLf & _
                                           ",DNSRegCD " & vbCrLf & _
                                           ",NIC1 " & vbCrLf & _
                                           ",NIC2 " & vbCrLf & _
                                           ",ConnectDT " & vbCrLf & _
                                           ",ExpirationDT " & vbCrLf & _
                                           ",DeletDT " & vbCrLf & _
                                           ",LastInfoDT " & vbCrLf & _
                                           ",ConectReason " & vbCrLf & _
                                           ",ExpirationUPDT " & vbCrLf & _
                                           ",InfoDT " & vbCrLf & _
                                           ",NumInfoKbn " & vbCrLf & _
                                           ",SealSendkbn " & vbCrLf & _
                                           ",AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",AntiVirusSofCheckDT " & vbCrLf & _
                                           ",BusyoKikiBiko " & vbCrLf & _
                                           ",ManageKyokuNM " & vbCrLf & _
                                           ",ManageBusyoNM " & vbCrLf & _
                                           ",WorkFromNmb " & vbCrLf & _
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
                                           ",SetKyokuNM " & vbCrLf & _
                                           ",SetBusyoNM " & vbCrLf & _
                                           ",SetRoom " & vbCrLf & _
                                           ",SetBuil " & vbCrLf & _
                                           ",SetFloor " & vbCrLf & _
                                           ",SerialAimai " & vbCrLf & _
                                           ",ManageBusyoNMAimai " & vbCrLf & _
                                           ",UsrIDAimai " & vbCrLf & _
                                           ",UsrBusyoNMAimai " & vbCrLf & _
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
                                           "  SELECT " & vbCrLf & _
                                           " ct.CINmb " & vbCrLf & _
                                           ",:RirekiNo " & vbCrLf & _
                                           ",ct.Kataban " & vbCrLf & _
                                           ",ct.Aliau " & vbCrLf & _
                                           ",ct.Serial " & vbCrLf & _
                                           ",ct.MacAddress1" & vbCrLf & _
                                           ",ct.MacAddress2 " & vbCrLf & _
                                           ",ct.ZooKbn " & vbCrLf & _
                                           ",ct.OSNM " & vbCrLf & _
                                           ",ct.AntiVirusSoftNM " & vbCrLf & _
                                           ",ct.DNSRegCD " & vbCrLf & _
                                           ",ct.NIC1 " & vbCrLf & _
                                           ",ct.NIC2 " & vbCrLf & _
                                           ",ct.ConnectDT " & vbCrLf & _
                                           ",ct.ExpirationDT " & vbCrLf & _
                                           ",ct.DeletDT " & vbCrLf & _
                                           ",ct.LastInfoDT " & vbCrLf & _
                                           ",ct.ConectReason " & vbCrLf & _
                                           ",ct.ExpirationUPDT " & vbCrLf & _
                                           ",ct.InfoDT " & vbCrLf & _
                                           ",ct.NumInfoKbn " & vbCrLf & _
                                           ",ct.SealSendkbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckKbn " & vbCrLf & _
                                           ",ct.AntiVirusSofCheckDT " & vbCrLf & _
                                           ",ct.BusyoKikiBiko " & vbCrLf & _
                                           ",ct.ManageKyokuNM " & vbCrLf & _
                                           ",ct.ManageBusyoNM " & vbCrLf & _
                                           ",ct.WorkFromNmb " & vbCrLf & _
                                           ",ct.IPUseCD " & vbCrLf & _
                                           ",ct.FixedIP " & vbCrLf & _
                                           ",ct.UsrID " & vbCrLf & _
                                           ",ct.UsrNM " & vbCrLf & _
                                           ",ct.UsrCompany " & vbCrLf & _
                                           ",ct.UsrKyokuNM " & vbCrLf & _
                                           ",ct.UsrBusyoNM " & vbCrLf & _
                                           ",ct.UsrTel " & vbCrLf & _
                                           ",ct.UsrMailAdd " & vbCrLf & _
                                           ",ct.UsrContact " & vbCrLf & _
                                           ",ct.UsrRoom " & vbCrLf & _
                                           ",ct.SetKyokuNM " & vbCrLf & _
                                           ",ct.SetBusyoNM " & vbCrLf & _
                                           ",ct.SetRoom " & vbCrLf & _
                                           ",ct.SetBuil " & vbCrLf & _
                                           ",ct.SetFloor " & vbCrLf & _
                                           ",ct.SerialAimai " & vbCrLf & _
                                           ",ct.ManageBusyoNMAimai " & vbCrLf & _
                                           ",ct.UsrIDAimai " & vbCrLf & _
                                           ",ct.UsrBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetBusyoNMAimai " & vbCrLf & _
                                           ",ct.SetRoomAimai " & vbCrLf & _
                                           ",ct.SetBuilAimai " & vbCrLf & _
                                           ",ct.SetFloorAimai " & vbCrLf & _
                                           ",ct.RegDT " & vbCrLf & _
                                           ",ct.RegGrpCD " & vbCrLf & _
                                           ",ct.RegID " & vbCrLf & _
                                           ",ct.UpdateDT " & vbCrLf & _
                                           ",ct.UpGrpCD " & vbCrLf & _
                                           ",ct.UpdateID " & vbCrLf & _
                                           "FROM CI_BUY_TB ct " & vbCrLf & _
                                           "WHERE ct.CINmb=:CINmb "

    '登録理由履歴新規登録（INSERT）SQL
    Private strInsertRegReasonRSql As String = "INSERT INTO REGREASON_RTB " & vbCrLf & _
                                                                    "( " & vbCrLf & _
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
                                                                    "VALUES " & vbCrLf & _
                                                                    "( " & vbCrLf & _
                                                                           " :CINmb " & vbCrLf & _
                                                                           ",:RirekiNo " & vbCrLf & _
                                                                           ",:RegReason " & vbCrLf & _
                                                                           ",:WorkCD " & vbCrLf & _
                                                                           ",:WorkKbnCD " & vbCrLf & _
                                                                           ",:RegDT " & vbCrLf & _
                                                                           ",:RegGrpCD " & vbCrLf & _
                                                                           ",:RegID " & vbCrLf & _
                                                                           ",:UpdateDT " & vbCrLf & _
                                                                           ",:UpGrpCD " & vbCrLf & _
                                                                           ",:UpdateID " & vbCrLf & _
                                                                    ") "

    '原因リンク履歴新規登録（INSERT）SQL
    Private strInsertCauseLinkRSql As String = "INSERT INTO CAUSELINK_RTB " & vbCrLf & _
                                                                        "( " & vbCrLf & _
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
                                                                        "VALUES " & vbCrLf & _
                                                                        "( " & vbCrLf & _
                                                                               " :CINmb " & vbCrLf & _
                                                                               ",:RirekiNo " & vbCrLf & _
                                                                               ",:ProcessKbn " & vbCrLf & _
                                                                               ",:MngNmb " & vbCrLf & _
                                                                               ",:RegDT " & vbCrLf & _
                                                                               ",:RegGrpCD " & vbCrLf & _
                                                                               ",:RegID " & vbCrLf & _
                                                                               ",:UpdateDT " & vbCrLf & _
                                                                               ",:UpGrpCD " & vbCrLf & _
                                                                               ",:UpdateID " & vbCrLf & _
                                                                        ") "


    ''' <summary>
    ''' 番号のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strNum">[IN]番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>番号のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNumSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, ByRef strNum As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNumSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))          '番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))      'CI種別コード
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("Num").Value = strNum      '番号
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                 'CI種別コード：部所有機器
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
    ''' CIステータスコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIステータスコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCountCIStateCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, _
                                             ByRef strStatus As String) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectConvertStatusSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'CI種別CD
                .Add(New NpgsqlParameter("CIStateNM", NpgsqlTypes.NpgsqlDbType.Varchar))        'ステータス名
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                                         'CI種別CD
                .Parameters("CIStateNM").Value = strStatus                  'ステータス名
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
    ''' CIオーナーCDのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strCIOwner">[IN]CIオーナーCD</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIオーナーCDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectCIOwnerCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, ByRef strCIOwner As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectCIOwnerCDSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'グループコード
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("GroupCD").Value = strCIOwner           'グループコード
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
    ''' OSソフトコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strOs">[IN/OUT]入力されたソフト名</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>OSソフトコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectOSSoftCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, ByRef strOs As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectConvertOSSoftSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("SoftKbnOS", NpgsqlTypes.NpgsqlDbType.Varchar))          'ソフト区分
                .Add(New NpgsqlParameter("softNMOS", NpgsqlTypes.NpgsqlDbType.Varchar))          'ソフト名
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("SoftKbnOS").Value = SOFTKBN_OS                                                    'ソフト区分
                .Parameters("SoftNMOS").Value = strOs                    'ソフト名
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
    ''' ウイルス対策ソフトコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strAntiVirus">[IN/OUT]入力されたウイルス対策ソフト</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ウイルス対策ソフトコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectAntiVirusSoftCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, ByRef strAntiVirus As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectConvertAntiVirusSoftSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("SoftKbnAntiVirus", NpgsqlTypes.NpgsqlDbType.Varchar))          'ソフト区分
                .Add(New NpgsqlParameter("softNMAntivirus", NpgsqlTypes.NpgsqlDbType.Varchar))           'ソフト名
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("SoftKbnAntivirus").Value = SOFTKBN_UNTIVIRUSSOFT                                         'ソフト区分
                .Parameters("SoftNMAntivirus").Value = strAntiVirus         'ソフト名
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
    ''' DNS登録のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strDNSReg">[IN/OUT]入力されたDNS登録</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>DNS登録のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectDNSRegSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByRef IntIndex As Integer, ByRef strDNSReg As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectConvertDNSRegSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KikiStateKbnDNS", NpgsqlTypes.NpgsqlDbType.Varchar))          '機器ステータス区分(DNS登録)
                .Add(New NpgsqlParameter("KikiStateNMDNS", NpgsqlTypes.NpgsqlDbType.Varchar))           '機器ステータス名(DNS登録)
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KikiStateKbnDNS").Value = KIKISTATEKBN_DNS_REG                                   '機器ステータス区分(DNS登録)
                .Parameters("KikiStateNMDNS").Value = strDNSReg         '機器ステータス名(DNS登録)
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
    ''' IP割当種類のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <param name="strIpUse">[IN/OUT]IP割当種類</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>IP割当種類のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIPUseSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0204 As DataHBKB0204, _
                                             ByVal intIndex As Integer, ByRef strIpUse As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectConvertIPUseSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KikiStateKbnIP", NpgsqlTypes.NpgsqlDbType.Varchar))          '機器ステータス区分(IP割当種類)
                .Add(New NpgsqlParameter("KikiStateNMIP", NpgsqlTypes.NpgsqlDbType.Varchar))           '機器ステータス名(IP割当種類)
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KikiStateKbnIP").Value = KIKISTATEKBN_IP_WARIATE                               '機器ステータス区分(IP割当種類)
                .Parameters("KikiStateNMIP").Value = strIpUse         '機器ステータス名(IP割当種類)
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
    ''' 新規CI番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0204 As DataHBKB0204) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_CI_NO

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
    ''' CI共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0204 As DataHBKB0204, _
                                       ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strClass1Aimai As String = ""       '分類１（あいまい）
        Dim strClass2Aimai As String = ""       '分類２（あいまい）
        Dim strCINMAimai As String = ""         '名称（あいまい）
        Dim strFreeWordAimai As String = ""     'フリーワード（あいまい）
        Dim strBikoAimai As String = ""         'フリーテキスト（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'CI種別CD
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))              '番号
                .Add(New NpgsqlParameter("CIStatusCD", NpgsqlTypes.NpgsqlDbType.Varchar))       'ステータスCD
                .Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))           '分類１
                .Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))           '分類２
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))             '名称
                .Add(New NpgsqlParameter("CIOwnerCD", NpgsqlTypes.NpgsqlDbType.Varchar))        'CIオーナーCD
                .Add(New NpgsqlParameter("CINaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))          '説明
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))            'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))         'フリーフラグ５
                .Add(New NpgsqlParameter("Class1Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '分類１（あいまい）
                .Add(New NpgsqlParameter("Class2Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))      '分類２（あいまい）
                .Add(New NpgsqlParameter("CINMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        '名称（あいまい）
                .Add(New NpgsqlParameter("FreeWordAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    'フリーワード（あいまい）
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))        'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                                  'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_KIKI                                             'CI種別CD
                .Parameters("KindCD").Value = ""                                                        '種別CD
                .Parameters("Num").Value = dataHBKB0204.PropAryNum(intIndex).ToString                   '番号
                .Parameters("CIStatusCD").Value = dataHBKB0204.PropAryStatsu(intIndex).ToString         'ステータスCD
                .Parameters("Class1").Value = dataHBKB0204.PropAryGrouping1(intIndex).ToString          '分類１
                .Parameters("Class2").Value = dataHBKB0204.PropAryGrouping2(intIndex).ToString          '分類２
                .Parameters("CINM").Value = dataHBKB0204.PropAryTitle(intIndex).ToString                '名称
                .Parameters("CIOwnerCD").Value = dataHBKB0204.PropAryCIOwnerCD(intIndex).ToString       'CIオーナーCD
                .Parameters("CINaiyo").Value = dataHBKB0204.PropAryExplanation(intIndex).ToString       '説明

                'フリーテキスト１～５
                .Parameters("BIko1").Value = dataHBKB0204.PropAryFreeText1(intIndex).ToString
                .Parameters("Biko2").Value = dataHBKB0204.PropAryFreeText2(intIndex).ToString
                .Parameters("BIko3").Value = dataHBKB0204.PropAryFreeText3(intIndex).ToString
                .Parameters("Biko4").Value = dataHBKB0204.PropAryFreeText4(intIndex).ToString
                .Parameters("Biko5").Value = dataHBKB0204.PropAryFreeText5(intIndex).ToString

                'フリーフラグ１～５
                .Parameters("FreeFlg1").Value = dataHBKB0204.PropAryFreeFlg1(intIndex).ToString
                .Parameters("FreeFlg2").Value = dataHBKB0204.PropAryFreeFlg2(intIndex).ToString
                .Parameters("FreeFlg3").Value = dataHBKB0204.PropAryFreeFlg3(intIndex).ToString
                .Parameters("FreeFlg4").Value = dataHBKB0204.PropAryFreeFlg4(intIndex).ToString
                .Parameters("FreeFlg5").Value = dataHBKB0204.PropAryFreeFlg5(intIndex).ToString

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryGrouping1(intIndex).ToString)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryGrouping2(intIndex).ToString)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryTitle(intIndex).ToString)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai & commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryExplanation(intIndex).ToString)
                strBikoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryFreeText1(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryFreeText2(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryFreeText3(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryFreeText4(intIndex).ToString) & _
                               commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryFreeText5(intIndex).ToString)
                .Parameters("Class1Aimai").Value = strClass1Aimai           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai       'フリーワード（あいまい）
                .Parameters("BikoAimai").Value = strBikoAimai               'フリーテキスト（あいまい）

                .Parameters("RegDT").Value = dataHBKB0204.PropDtmSysDate    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0204.PropDtmSysDate '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                  '最終更新者ID

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
    ''' CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0204 As DataHBKB0204) As Boolean

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
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0204.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                                  'CI番号
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
    ''' 登録理由履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0204 As DataHBKB0204) As Boolean

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
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号
                .Add(New NpgsqlParameter("RegReason", NpgsqlTypes.NpgsqlDbType.Varchar))        '登録理由
                .Add(New NpgsqlParameter("WorkCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '作業CD
                .Add(New NpgsqlParameter("WorkKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '作業区分CD
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0204.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = dataHBKB0204.PropStrRegReason                  '登録理由
                '[mod] y.ikushima 2012/08/30 y.ikushima作業CD、作業区分CD修正 START
                '.Parameters("WorkCD").Value = WORK_CD_PACKAGE                                   '作業CD
                '.Parameters("WorkKbnCD").Value = WORK_KBN_CD_COMPLETE                           '作業区分CD
                .Parameters("WorkCD").Value = Nothing                                   '作業CD
                .Parameters("WorkKbnCD").Value = Nothing                           '作業区分CD
                .Parameters("RegDT").Value = dataHBKB0204.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0204.PropDtmSysDate                     '最終更新日時
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


    ''' <summary>
    ''' 原因リンク履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN/OUT]一括登録　部所有機器Dataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0204 As DataHBKB0204) As Boolean

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
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))         '履歴番号
                .Add(New NpgsqlParameter("MngNmb", NpgsqlTypes.NpgsqlDbType.Integer))           '管理番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0204.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = CInt(dataHBKB0204.PropStrMngNmb)                  '管理番号
                .Parameters("ProcessKbn").Value = dataHBKB0204.PropStrProcessKbn                'プロセス区分
                .Parameters("RegDT").Value = dataHBKB0204.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0204.PropDtmSysDate                     '最終更新日時
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

    ''' <summary>
    ''' CI部所有機器新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">一括登録　部所有機器データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIBuySql(ByRef Cmd As NpgsqlCommand, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0204 As DataHBKB0204, _
                                         ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                       'SQL文
        Dim strSerialAimai As String = ""               '製造番号（あいまい）
        Dim strManageBusyoAimai As String = ""          '管理部署（あいまい）
        Dim strUsrIDAimai As String = ""                'ユーザーID（あいまい）
        Dim strUsrBusyoNMAimai As String = ""           'ユーザー所属部署（あいまい）
        Dim strSetBusyoNMAimai As String = ""           '設置部署（あいまい）
        Dim strSetRoomAimai As String = ""              '設置番組/部屋    （あいまい）
        Dim strSetBuilAimai As String = ""              '設置建物（あいまい）
        Dim strSetFloorAimai As String = ""             '設置フロア（あいまい）


        Try
            'SQL文(INSERT)
            strSQL = strInsertCIBuySql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)



            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                                    'CI種別CD
                .Add(New NpgsqlParameter("Kataban", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '型番
                .Add(New NpgsqlParameter("Aliau", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'エイリアス
                .Add(New NpgsqlParameter("Serial", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '製造番号
                .Add(New NpgsqlParameter("MacAddress1", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス1
                .Add(New NpgsqlParameter("MacAddress2", NpgsqlTypes.NpgsqlDbType.Varchar))                              'MACアドレス2
                .Add(New NpgsqlParameter("ZooKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'zoo参加有無
                .Add(New NpgsqlParameter("OSNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'OS名
                .Add(New NpgsqlParameter("AntiVirusSoftNM", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ウイルス対策ソフト名
                .Add(New NpgsqlParameter("DNSRegCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 'DNS登録CD
                .Add(New NpgsqlParameter("NIC1", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC1
                .Add(New NpgsqlParameter("NIC2", NpgsqlTypes.NpgsqlDbType.Varchar))                                     'NIC2
                .Add(New NpgsqlParameter("ConnectDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                '接続日
                .Add(New NpgsqlParameter("ExpirationDT", NpgsqlTypes.NpgsqlDbType.Varchar))                             '有効日
                .Add(New NpgsqlParameter("DeletDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '停止日
                .Add(New NpgsqlParameter("LastInfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                               '最終お知らせ日
                .Add(New NpgsqlParameter("ConectReason", NpgsqlTypes.NpgsqlDbType.Varchar))                             '接続理由
                .Add(New NpgsqlParameter("ExpirationUPDT", NpgsqlTypes.NpgsqlDbType.Varchar))                           '更新日
                .Add(New NpgsqlParameter("InfoDT", NpgsqlTypes.NpgsqlDbType.Varchar))                                   '通知日
                .Add(New NpgsqlParameter("NumInfoKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                               '番号通知
                .Add(New NpgsqlParameter("SealSendkbn", NpgsqlTypes.NpgsqlDbType.Varchar))                              'シール送付
                .Add(New NpgsqlParameter("AntiVirusSofCheckKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                     'ウイルス対策ソフト確認
                .Add(New NpgsqlParameter("AntiVirusSofCheckDT", NpgsqlTypes.NpgsqlDbType.Varchar))                      'ウイルス対策ソフトサーバー確認日
                .Add(New NpgsqlParameter("BusyoKikiBiko", NpgsqlTypes.NpgsqlDbType.Varchar))                            '部所有機器備考
                .Add(New NpgsqlParameter("ManageKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理局
                .Add(New NpgsqlParameter("ManageBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                            '管理部署
                .Add(New NpgsqlParameter("WorkFromNmb", NpgsqlTypes.NpgsqlDbType.Varchar))                              '作業の元
                .Add(New NpgsqlParameter("IPUseCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'IP割当種類CD
                .Add(New NpgsqlParameter("FixedIP", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '固定IP
                .Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザーID
                .Add(New NpgsqlParameter("UsrNM", NpgsqlTypes.NpgsqlDbType.Varchar))                                    'ユーザー氏名
                .Add(New NpgsqlParameter("UsrCompany ", NpgsqlTypes.NpgsqlDbType.Varchar))                              'ユーザー所属会社
                .Add(New NpgsqlParameter("UsrKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー所属部署
                .Add(New NpgsqlParameter("UsrTel", NpgsqlTypes.NpgsqlDbType.Varchar))                                   'ユーザー電話番号
                .Add(New NpgsqlParameter("UsrMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーメールアドレス
                .Add(New NpgsqlParameter("UsrContact", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザー連絡先
                .Add(New NpgsqlParameter("UsrRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  'ユーザー番組/部屋
                .Add(New NpgsqlParameter("SetKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置局
                .Add(New NpgsqlParameter("SetBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                               '設置部署
                .Add(New NpgsqlParameter("SetRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置番組/部屋
                .Add(New NpgsqlParameter("SetBuil", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '設置建物
                .Add(New NpgsqlParameter("SetFloor", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '設置フロア
                .Add(New NpgsqlParameter("SerialAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                              '製造番号（あいまい）
                .Add(New NpgsqlParameter("ManageBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                       '管理部署（あいまい）
                .Add(New NpgsqlParameter("UsrIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                               'ユーザーID（あいまい）
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          'ユーザー所属部署（あいまい）
                .Add(New NpgsqlParameter("SetBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                          '設置部署（あいまい）
                .Add(New NpgsqlParameter("SetRoomAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置番組/部屋（あいまい）
                .Add(New NpgsqlParameter("SetBuilAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                             '設置建物（あいまい）
                .Add(New NpgsqlParameter("SetFloorAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                            '設置フロア（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                                  '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                                    '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                               '最終更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                                  '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                                 '最終更新者ID

            End With
            '値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                                                  'CI種別CD
                .Parameters("Kataban").Value = dataHBKB0204.PropAryKataban(intIndex).ToString                           '型番
                .Parameters("Aliau").Value = dataHBKB0204.PropAryAliau(intIndex).ToString                               'エイリアス
                .Parameters("Serial").Value = dataHBKB0204.PropArySerial(intIndex).ToString                             '製造番号
                .Parameters("MacAddress1").Value = dataHBKB0204.PropAryMacAddress1(intIndex).ToString                   'MACアドレス1
                .Parameters("MacAddress2").Value = dataHBKB0204.PropAryMacAddress2(intIndex).ToString                   'MACアドレス2
                .Parameters("ZooKbn").Value = dataHBKB0204.PropAryZooKbn(intIndex).ToString                             'zoo参加有無
                .Parameters("OSNM").Value = dataHBKB0204.PropAryOSNM(intIndex)                                          'OS名
                .Parameters("AntiVirusSoftNM").Value = dataHBKB0204.PropAryAntiVirusSoftNM(intIndex)                    'ウイルス対策ソフト名
                .Parameters("DNSRegCD").Value = dataHBKB0204.PropAryDNSRegCD(intIndex).ToString                         'DNS登録CD
                .Parameters("NIC1").Value = dataHBKB0204.PropAryNIC1(intIndex).ToString                                 'NIC1
                .Parameters("NIC2").Value = dataHBKB0204.PropAryNIC2(intIndex).ToString                                 'NIC2
                .Parameters("ConnectDT").Value = dataHBKB0204.PropAryConnectDT(intIndex).ToString                       '接続日
                .Parameters("ExpirationDT").Value = dataHBKB0204.PropAryExpirationDT(intIndex).ToString                 '有効日
                .Parameters("DeletDT").Value = dataHBKB0204.PropAryDeletDT(intIndex).ToString                           '停止日
                .Parameters("LastInfoDT").Value = dataHBKB0204.PropAryLastInfoDT(intIndex).ToString                     '最終お知らせ日
                .Parameters("ConectReason").Value = dataHBKB0204.PropAryConnectReason(intIndex).ToString                '接続理由
                .Parameters("ExpirationUPDT").Value = dataHBKB0204.PropAryExpirationUPDT(intIndex).ToString             '更新日
                .Parameters("InfoDT").Value = dataHBKB0204.PropAryInfoDT(intIndex).ToString                             '通知日
                .Parameters("NumInfoKbn").Value = dataHBKB0204.PropAryNumInfoKbn(intIndex).ToString                     '番号通知
                .Parameters("SealSendkbn").Value = dataHBKB0204.PropArySealSendkbn(intIndex).ToString                   'シール送付
                .Parameters("AntiVirusSofCheckKbn").Value = dataHBKB0204.PropAryAntiVirusSoftCheckKbn(intIndex).ToString 'ウイルス対策ソフト確認
                .Parameters("AntiVirusSofCheckDT").Value = dataHBKB0204.PropAryAntiVirusSoftCheckDT(intIndex).ToString  'ウイルス対策ソフトサーバー確認日
                .Parameters("BusyoKikiBiko").Value = dataHBKB0204.PropAryBusyoKikiBiko(intIndex).ToString               '部所有機器備考
                .Parameters("ManageKyokuNM").Value = dataHBKB0204.PropAryManageKyokuNM(intIndex).ToString               '管理局
                .Parameters("ManageBusyoNM").Value = dataHBKB0204.PropAryManageBusyoNM(intIndex).ToString               '管理部署
                .Parameters("WorkFromNmb").Value = ""                                                                   '作業の元
                .Parameters("IPUseCD").Value = dataHBKB0204.PropAryIPUseCD(intIndex).ToString                           'IP割当種類CD
                .Parameters("FixedIP").Value = dataHBKB0204.PropAryFixedIP(intIndex).ToString                           '固定IP
                .Parameters("UsrID").Value = dataHBKB0204.PropAryUsrID(intIndex).ToString                               'ユーザーID
                .Parameters("UsrNM").Value = dataHBKB0204.PropAryUsrNM(intIndex).ToString                               'ユーザー氏名
                .Parameters("UsrCompany").Value = dataHBKB0204.PropAryUsrCompany(intIndex).ToString                     'ユーザー所属会社
                .Parameters("UsrKyokuNM").Value = dataHBKB0204.PropAryUsrKyokuNM(intIndex).ToString                     'ユーザー所属局
                .Parameters("UsrBusyoNM").Value = dataHBKB0204.PropAryUsrBusyoNM(intIndex).ToString                     'ユーザー所属部署
                .Parameters("UsrTel").Value = dataHBKB0204.PropAryUsrTel(intIndex).ToString                             'ユーザー電話番号
                .Parameters("UsrMailAdd").Value = dataHBKB0204.PropAryUsrMailAdd(intIndex).ToString                     'ユーザーメールアドレス
                .Parameters("UsrContact").Value = dataHBKB0204.PropAryUsrContact(intIndex).ToString                     'ユーザー連絡先
                .Parameters("UsrRoom").Value = dataHBKB0204.PropAryUsrRoom(intIndex).ToString                           'ユーザー番組/部屋
                .Parameters("SetKyokuNM").Value = dataHBKB0204.PropArySetKyokuNM(intIndex).ToString                     '設置局
                .Parameters("SetBusyoNM").Value = dataHBKB0204.PropArySetBusyoNM(intIndex).ToString                     '設置部署
                .Parameters("SetRoom").Value = dataHBKB0204.PropArySetRoom(intIndex).ToString                           '設置番組/部屋
                .Parameters("SetBuil").Value = dataHBKB0204.PropArySetBuil(intIndex).ToString                           '設置建物
                .Parameters("SetFloor").Value = dataHBKB0204.PropArySetFloor(intIndex).ToString                         '設置フロア

                'あいまい検索文字列設定
                strSerialAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropArySerial(intIndex).ToString)                  '製造番号（あいまい）
                strManageBusyoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryManageBusyoNM(intIndex).ToString)      '管理部署（あいまい）
                strUsrIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryUsrID(intIndex).ToString)                    'ユーザーID（あいまい）
                strUsrBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropAryUsrBusyoNM(intIndex).ToString)          'ユーザー所属部署（あいまい）
                strSetBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropArySetBusyoNM(intIndex).ToString)          '設置部署（あいまい）
                strSetRoomAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropArySetRoom(intIndex).ToString)                '設置番組/部屋    （あいまい）
                strSetBuilAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropArySetBuil(intIndex).ToString)                '設置建物（あいまい）
                strSetFloorAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0204.PropArySetFloor(intIndex).ToString)              '設置フロア（あいまい）

                .Parameters("SerialAimai").Value = strSerialAimai                                                       '製造番号（あいまい）
                .Parameters("ManageBusyoNMAimai").Value = strManageBusyoAimai                                           '管理部署（あいまい）
                .Parameters("UsrIDAimai").Value = strUsrIDAimai                                                         'ユーザーID（あいまい）
                .Parameters("UsrBusyoNMAimai").Value = strUsrBusyoNMAimai                                               'ユーザー所属部署（あいまい）
                .Parameters("SetBusyoNMAimai").Value = strSetBusyoNMAimai                                               '設置部署（あいまい）
                .Parameters("SetRoomAimai").Value = strSetRoomAimai                                                     '設置番組/部屋    （あいまい）
                .Parameters("SetBuilAimai").Value = strSetBuilAimai                                                     '設置建物（あいまい）
                .Parameters("SetFloorAimai").Value = strSetFloorAimai                                                   '設置フロア（あいまい）

                .Parameters("RegDT").Value = dataHBKB0204.PropDtmSysDate                                                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0204.PropDtmSysDate                                             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                              '最終更新者ID

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
    ''' CI部所有機器履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0204">[IN]一括登録　部所有機器データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI部所有機器歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/20 k.ueda
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIBuyRSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0204 As DataHBKB0204) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIBuyRSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            '型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            '値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0204.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0204.PropIntCINmb                                  'CI番号
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
