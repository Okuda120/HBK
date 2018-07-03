Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 導入画面Sqlクラス
''' </summary>
''' <remarks>導入画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/14 h.sasaki
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0901

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK


    'SQL文宣言

    'システム日付取得（SELECT）SQL
    Private strSelectSysDateSql As String = "SELECT Now() AS SysDate "

    '種別採番マスター取得（SELECT）SQL
    Private strSelectKindSaibanMtbSql As String = "SELECT " & vbCrLf & _
                                                  " MinNmb " & vbCrLf & _
                                                  ",MaxNmb " & vbCrLf & _
                                                  ",CurentNmb " & vbCrLf & _
                                                  ",LoopFlg " & vbCrLf & _
                                                  "FROM KIND_SAIBAN_MTB " & vbCrLf & _
                                                  "WHERE KindCD = :KindCD " & vbCrLf & _
                                                  "  AND JtiFlg <> '1' "

    '種別採番マスター取得（UPDATE）SQL
    Private strUpdateKindSaibanMtbSql As String = "UPDATE KIND_SAIBAN_MTB SET " & vbCrLf & _
                                                  " CurentNmb = :CurentNmb " & vbCrLf & _
                                                  ",UpdateDT  = :UpdateDT " & vbCrLf & _
                                                  ",UpGrpCD   = :UpGrpCD " & vbCrLf & _
                                                  ",UpdateID  = :UpdateID " & vbCrLf & _
                                                  "WHERE KindCD =:KindCD "

    '新規ログNo取得（SELECT）SQL
    Private strSelectNewLogNoSql As String = "SELECT " & vbCrLf & _
                                             " COALESCE(MAX(LogNo),0)+1 AS LogNo " & vbCrLf & _
                                             "FROM INTRODUCT_LTB " & vbCrLf & _
                                             "WHERE IntroductNmb = :IntroductNmb "

    '新規履歴番号取得（SELECT）SQL
    Private strSelectNewRirekiNoSql As String = "SELECT " & vbCrLf & _
                                                " COALESCE(MAX(ct.RirekiNo),0)+1 AS RirekiNo " & vbCrLf & _
                                                "FROM CI_INFO_RTB ct " & vbCrLf & _
                                                "WHERE ct.CINmb = :CINmb "

    '導入取得（SELECT）SQL
    Private strSelectIntroductSql As String = "SELECT " & vbCrLf & _
                                              " it.IntroductNmb " & vbCrLf & _
                                              ",it.KindCD " & vbCrLf & _
                                              ",it.KikiNmbFrom " & vbCrLf & _
                                              ",it.KikiNmbTo " & vbCrLf & _
                                              ",it.Class1 " & vbCrLf & _
                                              ",it.Class2 " & vbCrLf & _
                                              ",it.CINM " & vbCrLf & _
                                              ",it.Kataban " & vbCrLf & _
                                              ",it.Fuzokuhin " & vbCrLf & _
                                              ",it.SetNmb " & vbCrLf & _
                                              ",it.TypeKbn " & vbCrLf & _
                                              ",it.IntroductBiko " & vbCrLf & _
                                              ",it.SCHokanKbn " & vbCrLf & _
                                              ",it.IntroductDelKbn " & vbCrLf & _
                                              ",it.IntroductKbn " & vbCrLf & _
                                              ",it.HosyoUmu " & vbCrLf & _
                                              ",it.HosyoPlace " & vbCrLf & _
                                              ",CASE WHEN it.HosyoDelDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(it.HosyoDelDT, 'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END As HosyoDelDT" & vbCrLf & _
                                              ",it.LeaseNmb " & vbCrLf & _
                                              ",it.LeaseCompany " & vbCrLf & _
                                              ",CASE WHEN it.LeaseUpDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(it.LeaseUpDT, 'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END As LeaseUpDT" & vbCrLf & _
                                              ",it.MakerHosyoTerm " & vbCrLf & _
                                              ",it.EOS " & vbCrLf & _
                                              ",CASE WHEN it.DelScheduleDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(it.DelScheduleDT, 'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END As DelScheduleDT" & vbCrLf & _
                                              ",CASE WHEN it.IntroductStDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(it.IntroductStDT, 'YYYYMMDD'),'YYYY/MM/DD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END As IntroductStDT" & vbCrLf & _
                                              "FROM INTRODUCT_TB it " & vbCrLf & _
                                              "WHERE it.IntroductNmb = :IntroductNmb "

    '導入新規登録（INSERT）SQL
    Private strInsertIntroductSql As String = "INSERT INTO INTRODUCT_TB ( " & vbCrLf & _
                                              " IntroductNmb " & vbCrLf & _
                                              ",KindCD " & vbCrLf & _
                                              ",KikiNmbFrom " & vbCrLf & _
                                              ",KikiNmbTo " & vbCrLf & _
                                              ",Class1 " & vbCrLf & _
                                              ",Class2 " & vbCrLf & _
                                              ",CINM " & vbCrLf & _
                                              ",Kataban " & vbCrLf & _
                                              ",Fuzokuhin " & vbCrLf & _
                                              ",SetNmb " & vbCrLf & _
                                              ",TypeKbn " & vbCrLf & _
                                              ",IntroductBiko " & vbCrLf & _
                                              ",SCHokanKbn " & vbCrLf & _
                                              ",IntroductDelKbn " & vbCrLf & _
                                              ",IntroductKbn " & vbCrLf & _
                                              ",HosyoUmu " & vbCrLf & _
                                              ",HosyoPlace " & vbCrLf & _
                                              ",HosyoDelDT " & vbCrLf & _
                                              ",LeaseNmb " & vbCrLf & _
                                              ",LeaseCompany " & vbCrLf & _
                                              ",LeaseUpDT " & vbCrLf & _
                                              ",MakerHosyoTerm " & vbCrLf & _
                                              ",EOS " & vbCrLf & _
                                              ",DelScheduleDT " & vbCrLf & _
                                              ",IntroductStDT " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ") " & vbCrLf & _
                                              "VALUES ( " & vbCrLf & _
                                              " :IntroductNmb " & vbCrLf & _
                                              ",:KindCD " & vbCrLf & _
                                              ",TRIM(TO_CHAR(:KikiNmbFrom, '00000')) " & vbCrLf & _
                                              ",TRIM(TO_CHAR(:KikiNmbTo, '00000')) " & vbCrLf & _
                                              ",:Class1 " & vbCrLf & _
                                              ",:Class2 " & vbCrLf & _
                                              ",:CINM " & vbCrLf & _
                                              ",:Kataban " & vbCrLf & _
                                              ",:Fuzokuhin " & vbCrLf & _
                                              ",:SetNmb " & vbCrLf & _
                                              ",:TypeKbn " & vbCrLf & _
                                              ",:IntroductBiko " & vbCrLf & _
                                              ",:SCHokanKbn " & vbCrLf & _
                                              ",:IntroductDelKbn " & vbCrLf & _
                                              ",:IntroductKbn " & vbCrLf & _
                                              ",:HosyoUmu " & vbCrLf & _
                                              ",:HosyoPlace " & vbCrLf & _
                                              ",CASE WHEN :HosyoDelDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:HosyoDelDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END " & vbCrLf & _
                                              ",:LeaseNmb " & vbCrLf & _
                                              ",:LeaseCompany " & vbCrLf & _
                                              ",CASE WHEN :LeaseUpDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:LeaseUpDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END " & vbCrLf & _
                                              ",:MakerHosyoTerm " & vbCrLf & _
                                              ",:EOS " & vbCrLf & _
                                              ",CASE WHEN :DelScheduleDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:DelScheduleDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END " & vbCrLf & _
                                              ",CASE WHEN :IntroductStDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:IntroductStDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              ") "

    '導入更新（UPDATE）SQL
    Private strUpdateIntroductSql As String = "UPDATE INTRODUCT_TB SET " & vbCrLf & _
                                              " IntroductBiko  = :IntroductBiko " & vbCrLf & _
                                              ",IntroductKbn = :IntroductKbn " & vbCrLf & _
                                              ",HosyoUmu       = :HosyoUmu " & vbCrLf & _
                                              ",HosyoPlace     = :HosyoPlace " & vbCrLf & _
                                              ",HosyoDelDT     = " & vbCrLf & _
                                              " ( CASE WHEN :HosyoDelDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:HosyoDelDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END ) " & vbCrLf & _
                                              ",LeaseNmb = :LeaseNmb " & vbCrLf & _
                                              ",LeaseCompany = :LeaseCompany " & vbCrLf & _
                                              ",LeaseUpDT = " & vbCrLf & _
                                              " ( CASE WHEN :LeaseUpDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:LeaseUpDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END ) " & vbCrLf & _
                                              ",MakerHosyoTerm = :MakerHosyoTerm " & vbCrLf & _
                                              ",EOS            = :EOS " & vbCrLf & _
                                              ",DelScheduleDT = " & vbCrLf & _
                                              " ( CASE WHEN :DelScheduleDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:DelScheduleDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END ) " & vbCrLf & _
                                              ",IntroductStDT  = " & vbCrLf & _
                                              " ( CASE WHEN IntroductStDT <> '' " & vbCrLf & _
                                              " THEN TO_CHAR(TO_DATE(:IntroductStDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                              " ELSE '' " & vbCrLf & _
                                              " END ) " & vbCrLf & _
                                              ",UpdateDT       = :UpdateDT " & vbCrLf & _
                                              ",UpGrpCD        = :UpGrpCD " & vbCrLf & _
                                              ",UpdateID       = :UpdateID " & vbCrLf & _
                                              "WHERE IntroductNmb = :IntroductNmb "

    '導入ログ新規登録（INSERT）SQL
    Private strInsertIntroductLSql As String = "INSERT INTO INTRODUCT_LTB ( " & vbCrLf & _
                                               " IntroductNmb " & vbCrLf & _
                                               ",LogNo " & vbCrLf & _
                                               ",KindCD " & vbCrLf & _
                                               ",KikiNmbFrom " & vbCrLf & _
                                               ",KikiNmbTo " & vbCrLf & _
                                               ",Class1 " & vbCrLf & _
                                               ",Class2 " & vbCrLf & _
                                               ",CINM " & vbCrLf & _
                                               ",Kataban " & vbCrLf & _
                                               ",Fuzokuhin " & vbCrLf & _
                                               ",SetNmb " & vbCrLf & _
                                               ",TypeKbn " & vbCrLf & _
                                               ",IntroductBiko " & vbCrLf & _
                                               ",SCHokanKbn " & vbCrLf & _
                                               ",IntroductDelKbn " & vbCrLf & _
                                               ",IntroductKbn " & vbCrLf & _
                                               ",HosyoUmu " & vbCrLf & _
                                               ",HosyoPlace " & vbCrLf & _
                                               ",HosyoDelDT " & vbCrLf & _
                                               ",LeaseNmb " & vbCrLf & _
                                               ",LeaseCompany " & vbCrLf & _
                                               ",LeaseUpDT " & vbCrLf & _
                                               ",MakerHosyoTerm " & vbCrLf & _
                                               ",EOS " & vbCrLf & _
                                               ",DelScheduleDT " & vbCrLf & _
                                               ",IntroductStDT " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ") " & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " it.IntroductNmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",it.KindCD " & vbCrLf & _
                                               ",it.KikiNmbFrom " & vbCrLf & _
                                               ",it.KikiNmbTo " & vbCrLf & _
                                               ",it.Class1 " & vbCrLf & _
                                               ",it.Class2 " & vbCrLf & _
                                               ",it.CINM " & vbCrLf & _
                                               ",it.Kataban " & vbCrLf & _
                                               ",it.Fuzokuhin " & vbCrLf & _
                                               ",it.SetNmb " & vbCrLf & _
                                               ",it.TypeKbn " & vbCrLf & _
                                               ",it.IntroductBiko " & vbCrLf & _
                                               ",it.SCHokanKbn " & vbCrLf & _
                                               ",it.IntroductDelKbn " & vbCrLf & _
                                               ",it.IntroductKbn " & vbCrLf & _
                                               ",it.HosyoUmu " & vbCrLf & _
                                               ",it.HosyoPlace " & vbCrLf & _
                                               ",it.HosyoDelDT " & vbCrLf & _
                                               ",it.LeaseNmb " & vbCrLf & _
                                               ",it.LeaseCompany " & vbCrLf & _
                                               ",it.LeaseUpDT " & vbCrLf & _
                                               ",it.MakerHosyoTerm " & vbCrLf & _
                                               ",it.EOS " & vbCrLf & _
                                               ",it.DelScheduleDT " & vbCrLf & _
                                               ",it.IntroductStDT " & vbCrLf & _
                                               ",it.RegDT " & vbCrLf & _
                                               ",it.RegGrpCD " & vbCrLf & _
                                               ",it.RegID " & vbCrLf & _
                                               ",it.UpdateDT " & vbCrLf & _
                                               ",it.UpGrpCD " & vbCrLf & _
                                               ",it.UpdateID " & vbCrLf & _
                                               "FROM INTRODUCT_TB it " & vbCrLf & _
                                               "WHERE it.IntroductNmb = :IntroductNmb "

    'CI共通情報新規登録（INSERT）SQL
    Private strInsertCIInfoSql As String = "INSERT INTO CI_INFO_TB ( " & vbCrLf & _
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
                                           ",Biko1 " & vbCrLf & _
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
                                           "VALUES ( " & vbCrLf & _
                                           " :CINmb " & vbCrLf & _
                                           ",:CIKbnCD " & vbCrLf & _
                                           ",:KindCD " & vbCrLf & _
                                           ",TRIM(TO_CHAR(:Num, '00000')) " & vbCrLf & _
                                           ",CASE WHEN (SELECT km.SetupFlg FROM KIND_MTB km WHERE km.KindCD = :KindCD) = '1' " & vbCrLf & _
                                           " THEN :CIStatusCD1 " & vbCrLf & _
                                           " ELSE :CIStatusCD2 " & vbCrLf & _
                                           " END " & vbCrLf & _
                                           ",:Class1 " & vbCrLf & _
                                           ",:Class2 " & vbCrLf & _
                                           ",:CINM " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",(SELECT COALESCE(MAX(ct.Sort),0)+1 FROM CI_INFO_TB ct WHERE ct.CIKbnCD=:CIKbnCD AND ct.KindCD=:KindCD) " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",:FreeFlg1 " & vbCrLf & _
                                           ",:FreeFlg2 " & vbCrLf & _
                                           ",:FreeFlg3 " & vbCrLf & _
                                           ",:FreeFlg4 " & vbCrLf & _
                                           ",:FreeFlg5 " & vbCrLf & _
                                           ",:Class1Aimai " & vbCrLf & _
                                           ",:Class2Aimai " & vbCrLf & _
                                           ",:CINMAimai " & vbCrLf & _
                                           ",:FreeWordAimai " & vbCrLf & _
                                           ",'' " & vbCrLf & _
                                           ",:RegDT " & vbCrLf & _
                                           ",:RegGrpCD " & vbCrLf & _
                                           ",:RegID " & vbCrLf & _
                                           ",:UpdateDT " & vbCrLf & _
                                           ",:UpGrpCD " & vbCrLf & _
                                           ",:UpdateID " & vbCrLf & _
                                           ") "

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


    'CIサポセン機器新規登録（INSERT）SQL
    Private strInsertCISapSql As String = "INSERT INTO CI_SAP_TB ( " & vbCrLf & _
                                          " CINmb " & vbCrLf & _
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
                                          "VALUES ( " & vbCrLf & _
                                          " :CINmb " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",:Kataban " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",:Fuzokuhin " & vbCrLf & _
                                          ",:TypeKbn " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",:IntroductNmb " & vbCrLf & _
                                          ",CASE WHEN :LeaseUpDT <> '' " & vbCrLf & _
                                          " THEN TO_CHAR(TO_DATE(:LeaseUpDT, 'YYYY/MM/DD'),'YYYYMMDD') " & vbCrLf & _
                                          " ELSE '' " & vbCrLf & _
                                          " END " & vbCrLf & _
                                          ",:SCHokanKbn " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",'' " & vbCrLf & _
                                          ",:RegDT " & vbCrLf & _
                                          ",:RegGrpCD " & vbCrLf & _
                                          ",:RegID " & vbCrLf & _
                                          ",:UpdateDT " & vbCrLf & _
                                          ",:UpGrpCD " & vbCrLf & _
                                          ",:UpdateID " & vbCrLf & _
                                          ") "

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
                                           ",LastInfoDt " & vbCrLf & _
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
                                           " cs.CINmb " & vbCrLf & _
                                           ",:RirekiNo " & vbCrLf & _
                                           ",cs.MemorySize " & vbCrLf & _
                                           ",cs.Kataban " & vbCrLf & _
                                           ",cs.Serial " & vbCrLf & _
                                           ",cs.MacAddress1 " & vbCrLf & _
                                           ",cs.MacAddress2 " & vbCrLf & _
                                           ",cs.Fuzokuhin " & vbCrLf & _
                                           ",cs.TypeKbn " & vbCrLf & _
                                           ",cs.SCKikiFixNmb " & vbCrLf & _
                                           ",cs.KikiState " & vbCrLf & _
                                           ",cs.ImageNmb " & vbCrLf & _
                                           ",cs.IntroductNmb " & vbCrLf & _
                                           ",cs.LeaseUpDT " & vbCrLf & _
                                           ",cs.SCHokanKbn " & vbCrLf & _
                                           ",cs.LastInfoDt " & vbCrLf & _
                                           ",cs.ManageKyokuNM " & vbCrLf & _
                                           ",cs.ManageBusyoNM " & vbCrLf & _
                                           ",cs.WorkFromNmb " & vbCrLf & _
                                           ",cs.KikiUseCD " & vbCrLf & _
                                           ",cs.IPUseCD " & vbCrLf & _
                                           ",cs.FixedIP " & vbCrLf & _
                                           ",cs.UsrID " & vbCrLf & _
                                           ",cs.UsrNM " & vbCrLf & _
                                           ",cs.UsrCompany " & vbCrLf & _
                                           ",cs.UsrKyokuNM " & vbCrLf & _
                                           ",cs.UsrBusyoNM " & vbCrLf & _
                                           ",cs.UsrTel " & vbCrLf & _
                                           ",cs.UsrMailAdd " & vbCrLf & _
                                           ",cs.UsrContact " & vbCrLf & _
                                           ",cs.UsrRoom " & vbCrLf & _
                                           ",cs.RentalStDT " & vbCrLf & _
                                           ",cs.RentalEdDT " & vbCrLf & _
                                           ",cs.SetKyokuNM " & vbCrLf & _
                                           ",cs.SetBusyoNM " & vbCrLf & _
                                           ",cs.SetRoom " & vbCrLf & _
                                           ",cs.SetBuil " & vbCrLf & _
                                           ",cs.SetFloor " & vbCrLf & _
                                           ",cs.SetDeskNo " & vbCrLf & _
                                           ",cs.SetLANLength " & vbCrLf & _
                                           ",cs.SetLANNum " & vbCrLf & _
                                           ",cs.SetSocket " & vbCrLf & _
                                           ",cs.SerialAimai " & vbCrLf & _
                                           ",cs.ImageNmbAimai " & vbCrLf & _
                                           ",cs.ManageBusyoNMAimai " & vbCrLf & _
                                           ",cs.UsrIDAimai " & vbCrLf & _
                                           ",cs.SetBusyoNMAimai " & vbCrLf & _
                                           ",cs.SetRoomAimai " & vbCrLf & _
                                           ",cs.SetBuilAimai " & vbCrLf & _
                                           ",cs.SetFloorAimai " & vbCrLf & _
                                           ",cs.UpdateDT " & vbCrLf & _
                                           ",cs.UpGrpCD " & vbCrLf & _
                                           ",cs.UpdateID " & vbCrLf & _
                                           ",cs.UpdateDT " & vbCrLf & _
                                           ",cs.UpGrpCD " & vbCrLf & _
                                           ",cs.UpdateID " & vbCrLf & _
                                           "FROM CI_SAP_TB cs " & vbCrLf & _
                                           "WHERE cs.CINmb=:CINmb "

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
                                               "VALUES ( " & vbCrLf & _
                                               " :CINmb " & vbCrLf & _
                                               ",:RirekiNo " & vbCrLf & _
                                               ",:RegReason " & vbCrLf & _
                                               ",:WorkCD " & vbCrLf & _
                                               ",:WorkkbnCD " & vbCrLf & _
                                               ",:RegDT " & vbCrLf & _
                                               ",:RegGrpCD " & vbCrLf & _
                                               ",:RegID " & vbCrLf & _
                                               ",:UpdateDT " & vbCrLf & _
                                               ",:UpGrpCD " & vbCrLf & _
                                               ",:UpdateID " & vbCrLf & _
                                               ") "

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
                                               "VALUES ( " & vbCrLf & _
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

    '導入ロックテーブル取得用SQL
    Private strSelectIntroductLockSql As String = "SELECT" & vbCrLf & _
                                              "  ilt.EdiTime" & vbCrLf & _
                                              " ,ilt.EdiGrpCD" & vbCrLf & _
                                              " ,ilt.EdiID" & vbCrLf & _
                                              " ,gm.GroupNM" & vbCrLf & _
                                              " ,hm.HBKUsrNM" & vbCrLf & _
                                              " ,NULL" & vbCrLf & _
                                              "FROM INTRODUCT_LOCK_TB ilt" & vbCrLf & _
                                              "LEFT JOIN GRP_MTB gm ON ilt.EdiGrpCD=gm.GroupCD" & vbCrLf & _
                                              "LEFT JOIN HBKUSR_MTB hm ON ilt.EdiID=hm.HBKUsrID" & vbCrLf & _
                                              "WHERE IntroductNmb=:IntroductNmb"


    '導入ロック解除（DELETE）用SQL
    Private strDeleteIntroductLockSql As String = "DELETE FROM INTRODUCT_LOCK_TB " & vbCrLf & _
                                                  "WHERE IntroductNmb=:IntroductNmb "

    '導入ロックテーブル登録（INSERT）用SQL
    Private strInsertIntroductLockSql As String = "INSERT INTO INTRODUCT_LOCK_TB (" & vbCrLf & _
                                                  " IntroductNmb " & vbCrLf & _
                                                  ",EdiTime " & vbCrLf & _
                                                  ",EdiGrpCD " & vbCrLf & _
                                                  ",EdiID " & vbCrLf & _
                                                  ") " & vbCrLf & _
                                                  "VALUES ( " & vbCrLf & _
                                                  " :IntroductNmb " & vbCrLf & _
                                                  ", Now() " & vbCrLf & _
                                                  ",:EdiGrpCD " & vbCrLf & _
                                                  ",:EdiID " & vbCrLf & _
                                                  ") "

    '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 START
    'サポセン機器タイプマスタ取得
    Dim strSelectKindMastaSql As String = "SELECT " & vbCrLf & _
                                  " km.KindCD " & vbCrLf & _
                                  ",km.KindNM " & vbCrLf & _
                                  ",km.CIKbnCD " & vbCrLf & _
                                  ",km.Sort " & vbCrLf & _
                                  "FROM KIND_MTB km " & vbCrLf & _
                                  "WHERE (km.JtiFlg = '0' OR km.KindCD IN (SELECT KindCD FROM introduct_tb WHERE IntroductNmb = :IntroductNmb ))" & vbCrLf & _
                                  "AND km.CiKbnCD = :CiKbnCD " & vbCrLf & _
                                  "ORDER BY km.JtiFlg,km.Sort "

    'サポセン機器タイプマスタ取得
    Dim strSelectSapKikiTypeMastaSql As String = "SELECT " & vbCrLf & _
                                     " sm.SCKikiCD AS ID " & vbCrLf & _
                                     ",sm.SCKikiType AS Text " & vbCrLf & _
                                     "FROM SAP_KIKI_TYPE_MTB sm " & vbCrLf & _
                                     "WHERE sm.JtiFlg = '0' OR sm.SCKikiCD IN (SELECT TypeKbn FROM introduct_tb WHERE IntroductNmb = :IntroductNmb ) " & vbCrLf & _
                                    "ORDER BY sm.JtiFlg , sm.Sort " & vbCrLf
    '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 END

    ''' <summary>
    ''' 【編集／参照モード】導入取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIntroductSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIntroductSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '導入番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb               '導入番号
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
    ''' 【新規モード】種別採番マスター取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別採番マスター取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKindSaibanMtbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKindSaibanMtbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '種別CD
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KindCD").Value = dataHBKB0901.PropcmbKindNM.SelectedValue       '種別CD
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
    ''' 【新規モード】種別採番マスター更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>種別採番マスター更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateKindSaibanMtbSql(ByRef Cmd As NpgsqlCommand, _
                                              ByVal Cn As NpgsqlConnection, _
                                              ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateKindSaibanMtbSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CurentNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '最終番号
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))    '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))      '最終更新者ID
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))        '種別CD
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CurentNmb").Value = dataHBKB0901.PropIntKikiNmbTo               '最終番号
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                  '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                               '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                   '最終更新者ID
                .Parameters("KindCD").Value = dataHBKB0901.PropcmbKindNM.SelectedValue       '種別CD
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
    ''' 【新規登録モード】新規導入番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規導入番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewIntroductNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_INTRODUCT_NO

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
    ''' 【新規登録モード】新規CI番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規CI番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewCINmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
    ''' 【編集モード】サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
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
    ''' 【新規登録モード】導入新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIntroductSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIntroductSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))          '導入番号
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))                '種別CD
                .Add(New NpgsqlParameter("KikiNmbFrom", NpgsqlTypes.NpgsqlDbType.Integer))           '機器番号（FROM）
                .Add(New NpgsqlParameter("KikiNmbTo", NpgsqlTypes.NpgsqlDbType.Integer))             '機器番号（TO）
                .Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))                '分類１
                .Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))                '分類２
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))                  '名称
                .Add(New NpgsqlParameter("Kataban", NpgsqlTypes.NpgsqlDbType.Varchar))               '型番
                .Add(New NpgsqlParameter("Fuzokuhin", NpgsqlTypes.NpgsqlDbType.Varchar))             '付属品
                .Add(New NpgsqlParameter("SetNmb", NpgsqlTypes.NpgsqlDbType.Integer))                '台数
                .Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))               '導入タイプ
                .Add(New NpgsqlParameter("IntroductBiko", NpgsqlTypes.NpgsqlDbType.Varchar))         '導入備考
                .Add(New NpgsqlParameter("SCHokanKbn", NpgsqlTypes.NpgsqlDbType.Varchar))            'サービスセンター保管機
                .Add(New NpgsqlParameter("IntroductDelKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       '導入廃棄完了
                .Add(New NpgsqlParameter("IntroductKbn", NpgsqlTypes.NpgsqlDbType.Varchar))          '導入タイプ
                .Add(New NpgsqlParameter("HosyoUmu", NpgsqlTypes.NpgsqlDbType.Varchar))              '保証書有無
                .Add(New NpgsqlParameter("HosyoPlace", NpgsqlTypes.NpgsqlDbType.Varchar))            '保証書保管場所
                .Add(New NpgsqlParameter("HosyoDelDT", NpgsqlTypes.NpgsqlDbType.Varchar))            '保証書廃棄日
                .Add(New NpgsqlParameter("LeaseNmb", NpgsqlTypes.NpgsqlDbType.Varchar))              'リース番号
                .Add(New NpgsqlParameter("LeaseCompany", NpgsqlTypes.NpgsqlDbType.Varchar))          'リース会社
                .Add(New NpgsqlParameter("LeaseUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))             'リース期限日
                .Add(New NpgsqlParameter("MakerHosyoTerm", NpgsqlTypes.NpgsqlDbType.Varchar))        'メーカー無償保証期間
                .Add(New NpgsqlParameter("EOS", NpgsqlTypes.NpgsqlDbType.Varchar))                   'EOS
                .Add(New NpgsqlParameter("DelScheduleDT", NpgsqlTypes.NpgsqlDbType.Varchar))         '廃棄予定日
                .Add(New NpgsqlParameter("IntroductStDT", NpgsqlTypes.NpgsqlDbType.Varchar))         '導入開始日
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))               '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                 '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))            '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))               '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb                 '導入番号
                .Parameters("KindCD").Value = dataHBKB0901.PropcmbKindNM.SelectedValue               '種別CD
                .Parameters("KikiNmbFrom").Value = dataHBKB0901.PropIntKikiNmbFrom                   '機器番号（From）
                .Parameters("KikiNmbTo").Value = dataHBKB0901.PropIntKikiNmbTo                       '機器番号（To）
                .Parameters("Class1").Value = dataHBKB0901.ProptxtClass1.Text                        '分類１
                .Parameters("Class2").Value = dataHBKB0901.ProptxtClass2.Text                        '分類２
                .Parameters("CINM").Value = dataHBKB0901.ProptxtCINM.Text                            '名称
                .Parameters("Kataban").Value = dataHBKB0901.ProptxtKataban.Text                      '型番
                .Parameters("Fuzokuhin").Value = dataHBKB0901.ProptxtFuzokuhin.Text                  '付属品
                .Parameters("SetNmb").Value = dataHBKB0901.ProptxtSetNmb.Text                        '台数
                .Parameters("TypeKbn").Value = dataHBKB0901.PropcmbSCKikiType.SelectedValue          'タイプ
                .Parameters("IntroductBiko").Value = dataHBKB0901.ProptxtIntroductBiko.Text          '導入備考

                If dataHBKB0901.PropchkSCHokanKbn.Checked = True Then                                'サービスセンター保管機
                    .Parameters("SCHokanKbn").Value = FLG_ON
                Else
                    .Parameters("SCHokanKbn").Value = FLG_OFF
                End If

                If dataHBKB0901.PropchkIntroductDelKbn.Checked = True Then                           '導入廃棄完了
                    .Parameters("IntroductDelKbn").Value = FLG_ON
                Else
                    .Parameters("IntroductDelKbn").Value = FLG_OFF
                End If

                If dataHBKB0901.ProprdoIntroductKbn0.Checked = True Then                             '導入タイプ
                    .Parameters("IntroductKbn").Value = RADIO_ZERO
                ElseIf dataHBKB0901.ProprdoIntroductKbn1.Checked = True Then
                    .Parameters("IntroductKbn").Value = RADIO_ONE
                End If

                If dataHBKB0901.ProprdoHosyoUmu0.Checked = True Then                                 '保証書有無
                    .Parameters("HosyoUmu").Value = RADIO_ZERO
                ElseIf dataHBKB0901.ProprdoHosyoUmu1.Checked = True Then
                    .Parameters("HosyoUmu").Value = RADIO_ONE
                ElseIf dataHBKB0901.ProprdoHosyoUmu2.Checked = True Then
                    .Parameters("HosyoUmu").Value = RADIO_TWO
                End If

                .Parameters("HosyoPlace").Value = dataHBKB0901.ProptxtHosyoPlace.Text                '保証書保管場所
                .Parameters("HosyoDelDT").Value = dataHBKB0901.PropdtpHosyoDelDT.txtDate.Text        '保証書廃棄日
                .Parameters("LeaseNmb").Value = dataHBKB0901.ProptxtLeaseNmb.Text                    'リース番号
                .Parameters("LeaseCompany").Value = dataHBKB0901.ProptxtLeaseCompany.Text            'リース会社
                .Parameters("LeaseUpDT").Value = dataHBKB0901.PropdtpLeaseUpDT.txtDate.Text          'リース期限日
                .Parameters("MakerHosyoTerm").Value = dataHBKB0901.ProptxtMakerHosyoTerm.Text        'メーカー無償保証期間
                .Parameters("EOS").Value = dataHBKB0901.ProptxtEOS.Text                              'EOS
                .Parameters("DelScheduleDT").Value = dataHBKB0901.PropdtpDelScheduleDT.txtDate.Text  '廃棄予定日
                .Parameters("IntroductStDT").Value = dataHBKB0901.PropdtpIntroductStDT.txtDate.Text  '導入開始日

                .Parameters("RegDT").Value = dataHBKB0901.PropDtmSysDate                             '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                      '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                              '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                          '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                       '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                           '最終更新者ID

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
    ''' 【編集／参照モード】導入更新用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入更新用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetUpdateIntroductSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(UPDATE)
            strSQL = strUpdateIntroductSql

            'データアダプタに、SQLのUPDATE文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IntroductBiko", NpgsqlTypes.NpgsqlDbType.Varchar))        '導入備考
                .Add(New NpgsqlParameter("IntroductKbn", NpgsqlTypes.NpgsqlDbType.Varchar))         '導入タイプ
                .Add(New NpgsqlParameter("HosyoUmu", NpgsqlTypes.NpgsqlDbType.Varchar))             '保証書有無
                .Add(New NpgsqlParameter("HosyoPlace", NpgsqlTypes.NpgsqlDbType.Varchar))           '保証書保管場所
                .Add(New NpgsqlParameter("HosyoDelDT", NpgsqlTypes.NpgsqlDbType.Varchar))           '保証書廃棄日
                .Add(New NpgsqlParameter("LeaseNmb", NpgsqlTypes.NpgsqlDbType.Varchar))             'リース番号
                .Add(New NpgsqlParameter("LeaseCompany", NpgsqlTypes.NpgsqlDbType.Varchar))         'リース会社
                .Add(New NpgsqlParameter("LeaseUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))            'リース期限日
                .Add(New NpgsqlParameter("MakerHosyoTerm", NpgsqlTypes.NpgsqlDbType.Varchar))       'メーカー無償保証期間
                .Add(New NpgsqlParameter("EOS", NpgsqlTypes.NpgsqlDbType.Varchar))                  'EOS
                .Add(New NpgsqlParameter("DelScheduleDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '廃棄予定日
                .Add(New NpgsqlParameter("IntroductStDT", NpgsqlTypes.NpgsqlDbType.Varchar))        '導入開始日
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))           '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))              '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))             '最終更新者ID
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))         '導入番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IntroductBiko").Value = dataHBKB0901.ProptxtIntroductBiko.Text         '導入備考

                If dataHBKB0901.ProprdoIntroductKbn0.Checked = True Then                            '導入タイプ
                    .Parameters("IntroductKbn").Value = RADIO_ZERO
                ElseIf dataHBKB0901.ProprdoIntroductKbn1.Checked = True Then
                    .Parameters("IntroductKbn").Value = RADIO_ONE
                End If

                '保証書有無
                If dataHBKB0901.ProprdoHosyoUmu0.Checked = True Then
                    .Parameters("HosyoUmu").Value = RADIO_ZERO
                ElseIf dataHBKB0901.ProprdoHosyoUmu1.Checked = True Then
                    .Parameters("HosyoUmu").Value = RADIO_ONE
                ElseIf dataHBKB0901.ProprdoHosyoUmu2.Checked = True Then
                    .Parameters("HosyoUmu").Value = RADIO_TWO
                End If

                .Parameters("HosyoPlace").Value = dataHBKB0901.ProptxtHosyoPlace.Text               '保証書保管場所
                .Parameters("HosyoDelDT").Value = dataHBKB0901.PropdtpHosyoDelDT.txtDate.Text       '保証書廃棄日
                .Parameters("LeaseNmb").Value = dataHBKB0901.ProptxtLeaseNmb.Text                   'リース番号
                .Parameters("LeaseCompany").Value = dataHBKB0901.ProptxtLeaseCompany.Text           'リース会社
                .Parameters("LeaseUpDT").Value = dataHBKB0901.PropdtpLeaseUpDT.txtDate.Text         'リース期限日
                .Parameters("MakerHosyoTerm").Value = dataHBKB0901.ProptxtMakerHosyoTerm.Text       'メーカー無償保証期間
                .Parameters("EOS").Value = dataHBKB0901.ProptxtEOS.Text                             'EOS
                .Parameters("DelScheduleDT").Value = dataHBKB0901.PropdtpDelScheduleDT.txtDate.Text '廃棄予定日
                .Parameters("IntroductStDT").Value = dataHBKB0901.PropdtpIntroductStDT.txtDate.Text '導入開始日

                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                         '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                      '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                          '最終更新者ID
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb                '導入番号
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
    ''' 【共通】導入ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>導入ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIntroductLogSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIntroductLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))              'ログNo
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))       '導入番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKB0901.PropIntLogNo                            'ログNo
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb             '導入番号
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
    ''' 【新規登録モード】CI共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strClass1Aimai As String = ""       '分類１（あいまい）
        Dim strClass2Aimai As String = ""       '分類２（あいまい）
        Dim strCINMAimai As String = ""         '名称（あいまい）
        Dim strFreeWordAimai As String = ""     'フリーワード（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertCIInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      'CI種別CD
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))                       '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Integer))                          '番号
                .Add(New NpgsqlParameter("CIStatusCD1", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ステータスCD（301）
                .Add(New NpgsqlParameter("CIStatusCD2", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ステータスCD（304）
                .Add(New NpgsqlParameter("Class1", NpgsqlTypes.NpgsqlDbType.Varchar))                       '分類１
                .Add(New NpgsqlParameter("Class2", NpgsqlTypes.NpgsqlDbType.Varchar))                       '分類２
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))                         '名称
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ５
                .Add(New NpgsqlParameter("Class1Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))                  '分類１（あいまい）
                .Add(New NpgsqlParameter("Class2Aimai", NpgsqlTypes.NpgsqlDbType.Varchar))                  '分類２（あいまい）
                .Add(New NpgsqlParameter("CINMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                    '名称（あいまい）
                .Add(New NpgsqlParameter("FreeWordAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                'フリーワード（あいまい）
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                                      'CI番号
                .Parameters("CIKbnCD").Value = CI_TYPE_SUPORT                                               'CI種別CD
                .Parameters("KindCD").Value = dataHBKB0901.PropcmbKindNM.SelectedValue                      '種別CD

                '番号
                If dataHBKB0901.PropIntKikiNmbFrom + dataHBKB0901.PropIntiNmb - 1 > dataHBKB0901.PropIntMaxNmb Then
                    .Parameters("Num").Value = dataHBKB0901.PropIntKikiNmbFrom + dataHBKB0901.PropIntiNmb - 1 - dataHBKB0901.PropIntMaxNmb
                Else
                    .Parameters("Num").Value = dataHBKB0901.PropIntKikiNmbFrom + dataHBKB0901.PropIntiNmb - 1
                End If

                .Parameters("CIStatusCD1").Value = CI_STATUS_SUPORT_SYOKI                                   'ステータスCD（301）
                .Parameters("CIStatusCD2").Value = CI_STATUS_SUPORT_SYUKKOKA                                'ステータスCD（304）
                .Parameters("Class1").Value = dataHBKB0901.ProptxtClass1.Text                               '分類１
                .Parameters("Class2").Value = dataHBKB0901.ProptxtClass2.Text                               '分類２
                .Parameters("CINM").Value = dataHBKB0901.ProptxtCINM.Text                                   '名称

                'フリーフラグ１～５
                .Parameters("FreeFlg1").Value = FREE_FLG_OFF
                .Parameters("FreeFlg2").Value = FREE_FLG_OFF
                .Parameters("FreeFlg3").Value = FREE_FLG_OFF
                .Parameters("FreeFlg4").Value = FREE_FLG_OFF
                .Parameters("FreeFlg5").Value = FREE_FLG_OFF

                'あいまい検索文字列設定
                '※フリーワード（あいまい）　：分類１～２、名称、説明を連結
                '※フリーテキスト（あいまい）：フリーテキスト１～５を連結
                strClass1Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0901.ProptxtClass1.Text)
                strClass2Aimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0901.ProptxtClass2.Text)
                strCINMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKB0901.ProptxtCINM.Text)
                strFreeWordAimai = strClass1Aimai & strClass2Aimai & strCINMAimai
                .Parameters("Class1Aimai").Value = strClass1Aimai                                           '分類１（あいまい）
                .Parameters("Class2Aimai").Value = strClass2Aimai                                           '分類２（あいまい）
                .Parameters("CINMAimai").Value = strCINMAimai                                               '名称（あいまい）
                .Parameters("FreeWordAimai").Value = strFreeWordAimai                                       'フリーワード（あいまい）

                .Parameters("RegDT").Value = dataHBKB0901.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                                 '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                              '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                  '最終更新者ID

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
    ''' 【新規登録モード】CIサポセン機器新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIサポセン機器新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISapSql(ByRef Cmd As NpgsqlCommand, _
                                      ByVal Cn As NpgsqlConnection, _
                                      ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertCISapSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("Kataban", NpgsqlTypes.NpgsqlDbType.Varchar))          '型番
                .Add(New NpgsqlParameter("Fuzokuhin", NpgsqlTypes.NpgsqlDbType.Varchar))        '付属品
                .Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))          'タイプ
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '導入番号
                .Add(New NpgsqlParameter("LeaseUpDT", NpgsqlTypes.NpgsqlDbType.Varchar))        'リース期限日（機器）
                .Add(New NpgsqlParameter("SCHokanKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'サービスセンター保管機
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                          'CI番号
                .Parameters("Kataban").Value = dataHBKB0901.ProptxtKataban.Text                 '型番
                .Parameters("Fuzokuhin").Value = dataHBKB0901.ProptxtFuzokuhin.Text             '付属品
                .Parameters("TypeKbn").Value = dataHBKB0901.PropcmbSCKikiType.SelectedValue     'タイプ
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb            '導入番号
                .Parameters("LeaseUpDT").Value = dataHBKB0901.PropdtpLeaseUpDT.txtDate.Text     'リース期限日（機器）

                If dataHBKB0901.PropchkSCHokanKbn.Checked = True Then                           'サービスセンター保管機
                    .Parameters("SCHokanKbn").Value = FLG_ON
                Else
                    .Parameters("SCHokanKbn").Value = FLG_OFF
                End If

                .Parameters("RegDT").Value = dataHBKB0901.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                     '最終更新日時
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
    ''' 【共通】新規ログNo取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規ログNo取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/17 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewLogNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                         ByVal Cn As NpgsqlConnection, _
                                         ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewLogNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '導入番号
            End With
            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb            '導入番号
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
    ''' 【共通】新規履歴番号取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規履歴番号取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewRirekiNoSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectNewRirekiNoSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                                  'CI番号
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
    ''' 【共通】CI共通情報履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CI共通情報履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCIInfoRSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
                .Parameters("RirekiNo").Value = dataHBKB0901.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>CIシステム履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCISapRSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))                 '履歴番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                    'CI番号
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("RirekiNo").Value = dataHBKB0901.PropIntRirekiNo                            '履歴番号
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                                  'CI番号
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
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>登録理由履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertRegReasonRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0901.PropIntRirekiNo                    '履歴番号
                .Parameters("RegReason").Value = dataHBKB0901.PropStrRegReason                  '登録理由
                .Parameters("WorkCD").Value = WORK_CD_INTRODUCT                                 '作業CD
                .Parameters("WorkKbnCD").Value = WORK_KBN_CD_COMPLETE                           '作業区分CD
                .Parameters("RegDT").Value = dataHBKB0901.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                     '最終更新日時
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
    ''' 【共通】原因リンク履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>原因リンク履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertCauseLinkRSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0901 As DataHBKB0901) As Boolean

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
                .Parameters("CINmb").Value = dataHBKB0901.PropIntCINmb                          'CI番号
                .Parameters("RirekiNo").Value = dataHBKB0901.PropIntRirekiNo                    '履歴番号
                .Parameters("MngNmb").Value = dataHBKB0901.PropRowReg.Item("MngNmb")            '管理番号
                .Parameters("ProcessKbn").Value = dataHBKB0901.PropRowReg.Item("ProcessKbn")    'プロセス区分
                .Parameters("RegDT").Value = dataHBKB0901.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0901.PropDtmSysDate                     '最終更新日時
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
    ''' 導入ロックテーブル、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>指定された導入番号の導入ロックテーブル取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function SelectIntroductLockSql(ByRef Adapter As NpgsqlDataAdapter, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal intIntroductNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strSelectIntroductLockSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '導入番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IntroductNmb").Value = intIntroductNmb                   '導入番号
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
    ''' 導入ロックテーブル削除処理用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>導入番号をキーに導入ロックテーブルのデータを物理削除（DELETE）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function DeleteIntroductLockSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal intIntroductNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strDeleteIntroductLockSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))     '導入番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IntroductNmb").Value = intIntroductNmb                             '導入番号
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
    ''' CI共通情報ロックテーブル登録処理用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="intIntroductNmb">[IN]導入番号</param>
    ''' <returns>boolean 終了状況    True:正常  False:異常</returns>
    ''' <remarks>導入ロックテーブルにデータを新規登録（INSERT）する
    ''' <para>作成情報：2012/07/18 h.sasaki
    ''' <p>改定情報：</p>
    ''' </para></remarks> 
    Public Function InsertIntroductLockSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal intIntroductNmb As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIntroductLockSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '導入番号
                .Add(New NpgsqlParameter("EdiGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '編集者グループコード
                .Add(New NpgsqlParameter("EdiID", NpgsqlTypes.NpgsqlDbType.Varchar))             '編集者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IntroductNmb").Value = intIntroductNmb                              '導入番号
                .Parameters("EdiGrpCD").Value = PropWorkGroupCD                                  '編集者グループコード
                .Parameters("EdiID").Value = PropUserId                                          '編集者ID
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

    '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 START

    ''' <summary>
    ''' 種別マスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器タイプコンボボックス用データを取得する
    ''' <para>作成情報：2013/03/29
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKindMastaDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectKindMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '導入番号
                .Add(New NpgsqlParameter("CiKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))          'CI種別コード
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb            '導入番号
                .Parameters("CiKbnCD").Value = CI_TYPE_SUPORT            'CI種別コード
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
    ''' サポセン機器タイプマスター取得
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0901">[IN]導入画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>サポセン機器タイプコンボボックス用データを取得する
    ''' <para>作成情報：2013/03/29
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSapKikiTypeMastaDataSql(ByRef Adapter As NpgsqlDataAdapter, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0901 As DataHBKB0901) As Boolean

        '開始ログ出力
        commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectSapKikiTypeMastaSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IntroductNmb", NpgsqlTypes.NpgsqlDbType.Integer))      '導入番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IntroductNmb").Value = dataHBKB0901.PropIntIntroductNmb            '導入番号
            End With

            '終了ログ出力
            commonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            commonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = HBK_E001 & ex.Message
            Return False
        End Try

    End Function
    '[add] 2013/03/29 y.ikushima マスタデータ削除フラグ対応 END

End Class
