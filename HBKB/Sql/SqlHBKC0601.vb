Imports Common
Imports CommonHBK
Imports Npgsql

''' <summary>
''' 一括登録　システムクラスクラス
''' </summary>
''' <remarks>一括登録　システムのSQLの作成・設定を行う
''' <para>作成情報：2012/07/24 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKC0601

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '受付手段CD取得処理（SELECT）SQL
    Private strSelectUketsukewaySql As String = "SELECT UketsukeWayCD " & vbCrLf & _
                                                "FROM UKETSUKEWAY_MTB " & vbCrLf & _
                                                "WHERE JtiFlg = '0' " & vbCrLf & _
                                                "AND UketsukeWayNM = :UketsukeWayNM"

    'インシデント種別CD取得処理（SELECT）SQL
    Private strSelectIncidentKindSql As String = "SELECT IncKindCD " & vbCrLf & _
                                                    "FROM incident_kind_MTB " & vbCrLf & _
                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                    "AND IncKindNM = :IncKindNM"

    'プロセスステータスCD取得処理（SELECT）SQL
    Private strSelectProcessStateSql As String = "SELECT ProcessStateCD " & vbCrLf & _
                                                    "FROM PROCESSSTATE_MTB " & vbCrLf & _
                                                    "WHERE JtiFlg = '0' " & vbCrLf & _
                                                    "AND ProcessKbn = :ProcessKbn " & vbCrLf & _
                                                    "AND ProcessStateNM = :ProcessStateNM"

    'システム番号の取得処理（SELECT）SQL
    Private strSelectSystemNmbSql As String = "SELECT CINmb " & vbCrLf & _
                                                ",Txt " & vbCrLf & _
                                                "FROM ( " & vbCrLf & _
                                                "SELECT CINmb, Class1||' '||Class2||' '||CINM AS Txt " & vbCrLf & _
                                                "FROM CI_INFO_TB " & vbCrLf & _
                                                "WHERE CIKbnCD = :CIKbnCD " & vbCrLf & _
                                                ") AS C " & vbCrLf & _
                                                "where Txt = :CINM"

    'ドメインCD取得処理（SELECT）SQL
    Private strSelectDomainSql As String = "SELECT DomainCD " & vbCrLf & _
                                            "FROM DOMAIN_MTB " & vbCrLf & _
                                            "WHERE JtiFlg = '0' " & vbCrLf & _
                                            "AND DomainNM = :DomainNM"

    '経過種別CD取得処理（SELECT）SQL
    Private strSelectKeikaKindSql As String = "SELECT KeikaKindCD " & vbCrLf & _
                                                "FROM KEIKA_KIND_MTB " & vbCrLf & _
                                                "WHERE JtiFlg = '0' " & vbCrLf & _
                                                "AND KeikaKindNM = :KeikaKindNM"

    '機器種別CD取得処理（SELECT）SQL
    Private strSelectKikiKindSql As String = "SELECT KindCD " & vbCrLf & _
                                                "FROM KIND_MTB " & vbCrLf & _
                                                "WHERE JtiFlg = '0' " & vbCrLf & _
                                                "AND KindNM = :KindNM"
    '機器取得処理（SELECT）SQL
    Private strSelectKikiSql As String = "SELECT CINMB " & vbCrLf & _
                                                "FROM CI_INFO_TB " & vbCrLf & _
                                                "WHERE KindCD = :KindCD " & vbCrLf & _
                                                "AND NUM = :NUM"

    'グループCD取得処理（SELECT）SQL
    Private strSelectGroupSql As String = "SELECT COUNT(GroupCD) " & vbCrLf & _
                                            "FROM GRP_MTB " & vbCrLf & _
                                            "WHERE JtiFlg = '0' " & vbCrLf & _
                                            "AND GroupCD = :GroupCD"

    'ひびきユーザーID取得処理（SELECT）SQL
    Private strSelectUsrSql As String = "SELECT COUNT(HBKUsrID) " & vbCrLf & _
                                            "FROM HBKUSR_MTB " & vbCrLf & _
                                            "WHERE JtiFlg = '0' " & vbCrLf & _
                                            "AND HBKUsrID = :HBKUsrID"

    'インシデント共通情報新規登録（INSERT）SQL
    Private strInsertIncInfoSql As String = "INSERT INTO INCIDENT_INFO_TB (" & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",ProcessKbn " & vbCrLf & _
                                                    ",UkeKbnCD " & vbCrLf & _
                                                    ",IncKbnCD " & vbCrLf & _
                                                    ",ProcessStateCD " & vbCrLf & _
                                                    ",HasseiDT " & vbCrLf & _
                                                    ",KaitoDT " & vbCrLf & _
                                                    ",KanryoDT " & vbCrLf & _
                                                    ",Priority " & vbCrLf & _
                                                    ",Errlevel " & vbCrLf & _
                                                    ",Title " & vbCrLf & _
                                                    ",UkeNaiyo " & vbCrLf & _
                                                    ",TaioKekka " & vbCrLf & _
                                                    ",SystemNmb " & vbCrLf & _
                                                    ",OutSideToolNmb " & vbCrLf & _
                                                    ",EventID " & vbCrLf & _
                                                    ",Source " & vbCrLf & _
                                                    ",OPCEventID " & vbCrLf & _
                                                    ",EventClass " & vbCrLf & _
                                                    ",TantoGrpCD " & vbCrLf & _
                                                    ",IncTantoID " & vbCrLf & _
                                                    ",IncTantoNM " & vbCrLf & _
                                                    ",DomainCD " & vbCrLf & _
                                                    ",PartnerCompany " & vbCrLf & _
                                                    ",PartnerID " & vbCrLf & _
                                                    ",PartnerNM " & vbCrLf & _
                                                    ",PartnerKana " & vbCrLf & _
                                                    ",PartnerKyokuNM " & vbCrLf & _
                                                    ",UsrBusyoNM " & vbCrLf & _
                                                    ",PartnerTel " & vbCrLf & _
                                                    ",PartnerMailAdd " & vbCrLf & _
                                                    ",PartnerContact " & vbCrLf & _
                                                    ",PartnerBase " & vbCrLf & _
                                                    ",PartnerRoom " & vbCrLf & _
                                                    ",ShijisyoFlg " & vbCrLf & _
                                                    ",Kengen " & vbCrLf & _
                                                    ",RentalKiki " & vbCrLf & _
                                                    ",TitleAimai " & vbCrLf & _
                                                    ",UkeNaiyoAimai " & vbCrLf & _
                                                    ",BikoAimai " & vbCrLf & _
                                                    ",TaioKekkaAimai " & vbCrLf & _
                                                    ",EventIDAimai " & vbCrLf & _
                                                    ",SourceAimai " & vbCrLf & _
                                                    ",OPCEventIDAimai " & vbCrLf & _
                                                    ",EventClassAimai " & vbCrLf & _
                                                    ",IncTantIDAimai " & vbCrLf & _
                                                    ",IncTantNMAimai " & vbCrLf & _
                                                    ",PartnerIDAimai " & vbCrLf & _
                                                    ",PartnerNMAimai " & vbCrLf & _
                                                    ",UsrBusyoNMAimai " & vbCrLf & _
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
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "VALUES ( " & vbCrLf & _
                                                    " :IncNmb " & vbCrLf & _
                                                    ",:ProcessKbn " & vbCrLf & _
                                                    ",:UkeKbnCD " & vbCrLf & _
                                                    ",:IncKbnCD " & vbCrLf & _
                                                    ",:ProcessStateCD " & vbCrLf & _
                                                    ",:HasseiDT " & vbCrLf & _
                                                    ",:KaitoDT " & vbCrLf & _
                                                    ",:KanryoDT " & vbCrLf & _
                                                    ",:Priority " & vbCrLf & _
                                                    ",:Errlevel " & vbCrLf & _
                                                    ",:Title " & vbCrLf & _
                                                    ",:UkeNaiyo " & vbCrLf & _
                                                    ",:TaioKekka " & vbCrLf & _
                                                    ",:SystemNmb " & vbCrLf & _
                                                    ",:OutSideToolNmb " & vbCrLf & _
                                                    ",:EventID " & vbCrLf & _
                                                    ",:Source " & vbCrLf & _
                                                    ",:OPCEventID " & vbCrLf & _
                                                    ",:EventClass " & vbCrLf & _
                                                    ",:TantoGrpCD " & vbCrLf & _
                                                    ",:IncTantoID " & vbCrLf & _
                                                    ",:IncTantoNM " & vbCrLf & _
                                                    ",:DomainCD " & vbCrLf & _
                                                    ",:PartnerCompany " & vbCrLf & _
                                                    ",:PartnerID " & vbCrLf & _
                                                    ",:PartnerNM " & vbCrLf & _
                                                    ",:PartnerKana " & vbCrLf & _
                                                    ",:PartnerKyokuNM " & vbCrLf & _
                                                    ",:UsrBusyoNM " & vbCrLf & _
                                                    ",:PartnerTel " & vbCrLf & _
                                                    ",:PartnerMailAdd " & vbCrLf & _
                                                    ",:PartnerContact " & vbCrLf & _
                                                    ",:PartnerBase " & vbCrLf & _
                                                    ",:PartnerRoom " & vbCrLf & _
                                                    ",:ShijisyoFlg " & vbCrLf & _
                                                    ",''" & vbCrLf & _
                                                    ",'' " & vbCrLf & _
                                                    ",:TitleAimai " & vbCrLf & _
                                                    ",:UkeNaiyoAimai " & vbCrLf & _
                                                    ",''" & vbCrLf & _
                                                    ",:TaioKekkaAimai " & vbCrLf & _
                                                    ",:EventIDAimai " & vbCrLf & _
                                                    ",:SourceAimai " & vbCrLf & _
                                                    ",:OPCEventIDAimai " & vbCrLf & _
                                                    ",:EventClassAimai " & vbCrLf & _
                                                    ",:IncTantIDAimai " & vbCrLf & _
                                                    ",:IncTantNMAimai " & vbCrLf & _
                                                    ",:PartnerIDAimai " & vbCrLf & _
                                                    ",:PartnerNMAimai " & vbCrLf & _
                                                    ",:UsrBusyoNMAimai " & vbCrLf & _
                                                    ",''" & vbCrLf & _
                                                    ",'' " & vbCrLf & _
                                                    ",'' " & vbCrLf & _
                                                    ",'' " & vbCrLf & _
                                                    ",'' " & vbCrLf & _
                                                    ",'0' " & vbCrLf & _
                                                    ",'0' " & vbCrLf & _
                                                    ",'0' " & vbCrLf & _
                                                    ",'0' " & vbCrLf & _
                                                    ",'0' " & vbCrLf & _
                                                    ",:RegDT " & vbCrLf & _
                                                    ",:RegGrpCD " & vbCrLf & _
                                                    ",:RegID " & vbCrLf & _
                                                    ",:UpdateDT " & vbCrLf & _
                                                    ",:UpGrpCD " & vbCrLf & _
                                                    ",:UpdateID " & vbCrLf & _
                                                    ") "

    'インシデント担当履歴情報登録（INSERT）SQL
    Private strInsertIncTantoRirekiSql As String = "INSERT INTO INCIDENT_TANTO_RIREKI_TB (" & vbCrLf & _
                                                   "     IncNmb" & vbCrLf & _
                                                   "    ,TantoRirekiNmb" & vbCrLf & _
                                                   "    ,TantoGrpCD" & vbCrLf & _
                                                   "    ,TantoGrpNM" & vbCrLf & _
                                                   "    ,IncTantoID" & vbCrLf & _
                                                   "    ,IncTantoNM" & vbCrLf & _
                                                   "    ,RegDT " & vbCrLf & _
                                                   "    ,RegGrpCD " & vbCrLf & _
                                                   "    ,RegID " & vbCrLf & _
                                                   "    ,UpdateDT " & vbCrLf & _
                                                   "    ,UpGrpCD " & vbCrLf & _
                                                   "    ,UpdateID " & vbCrLf & _
                                                   ") " & vbCrLf & _
                                                   "VALUES ( " & vbCrLf & _
                                                   "     :IncNmb" & vbCrLf & _
                                                   "    ,(SELECT COALESCE(MAX(TantoRirekiNmb),0)+1 FROM INCIDENT_TANTO_RIREKI_TB WHERE IncNmb=:IncNmb)" & vbCrLf & _
                                                   "    ,:TantoGrpCD" & vbCrLf & _
                                                   "    ,(SELECT GroupNM FROM GRP_MTB WHERE GroupCD = :TantoGrpCD)" & vbCrLf & _
                                                   "    ,:IncTantoID" & vbCrLf & _
                                                   "    ,:IncTantoNM" & vbCrLf & _
                                                   "    ,:RegDT " & vbCrLf & _
                                                   "    ,:RegGrpCD " & vbCrLf & _
                                                   "    ,:RegID " & vbCrLf & _
                                                   "    ,:UpdateDT " & vbCrLf & _
                                                   "    ,:UpGrpCD " & vbCrLf & _
                                                   "    ,:UpdateID " & vbCrLf & _
                                                   ") "
                                                   

    'インシデント作業履歴新規登録（INSERT）SQL
    Private strInsertIncWkRirekiSql As String = "INSERT INTO INCIDENT_WK_RIREKI_TB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",WorkRirekiNmb " & vbCrLf & _
                                                    ",KeikaKbnCD " & vbCrLf & _
                                                    ",WorkNaiyo " & vbCrLf & _
                                                    ",WorkSceDT " & vbCrLf & _
                                                    ",WorkStDT " & vbCrLf & _
                                                    ",WorkEdDT " & vbCrLf & _
                                                    ",SystemNmb " & vbCrLf & _
                                                    ",WorkNaiyoAimai " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "VALUES ( " & vbCrLf & _
                                                    " :IncNmb " & vbCrLf & _
                                                    ",:WorkRirekiNmb " & vbCrLf & _
                                                    ",:KeikaKbnCD " & vbCrLf & _
                                                    ",:WorkNaiyo " & vbCrLf & _
                                                    ",:WorkSceDT " & vbCrLf & _
                                                    ",:WorkStDT " & vbCrLf & _
                                                    ",:WorkEdDT " & vbCrLf & _
                                                    ",:SystemNmb " & vbCrLf & _
                                                    ",:WorkNaiyoAimai " & vbCrLf & _
                                                    ",:RegDT " & vbCrLf & _
                                                    ",:RegGrpCD " & vbCrLf & _
                                                    ",:RegID " & vbCrLf & _
                                                    ",:UpdateDT " & vbCrLf & _
                                                    ",:UpGrpCD " & vbCrLf & _
                                                    ",:UpdateID " & vbCrLf & _
                                                    ") "

    'インシデント作業担当新規登録（INSERT）SQL
    Private strInsertIncWkTantoSql As String = "INSERT INTO INCIDENT_WK_TANTO_TB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",WorkRirekiNmb " & vbCrLf & _
                                                    ",worktantonmb " & vbCrLf & _
                                                    ",WorkTantoGrpCD " & vbCrLf & _
                                                    ",WorkTantoID " & vbCrLf & _
                                                    ",WorkTantoGrpNM " & vbCrLf & _
                                                    ",WorkTantoNM " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "VALUES ( " & vbCrLf & _
                                                    " :IncNmb " & vbCrLf & _
                                                    ",:WorkRirekiNmb " & vbCrLf & _
                                                    ",:worktantonmb " & vbCrLf & _
                                                    ",:WorkTantoGrpCD " & vbCrLf & _
                                                    ",:WorkTantoID " & vbCrLf & _
                                                    ",(SELECT GroupNM FROM GRP_MTB WHERE GroupCD =:WorkTantoGrpCD) " & vbCrLf & _
                                                    ",:WorkTantoNM " & vbCrLf & _
                                                    ",:RegDT " & vbCrLf & _
                                                    ",:RegGrpCD " & vbCrLf & _
                                                    ",:RegID " & vbCrLf & _
                                                    ",:UpdateDT " & vbCrLf & _
                                                    ",:UpGrpCD " & vbCrLf & _
                                                    ",:UpdateID " & vbCrLf & _
                                                    ") "

    'インシデント機器情報新規登録（INSERT）SQL
    Private strInsertIncKikiSql As String = "INSERT INTO INCIDENT_KIKI_TB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",KindCD " & vbCrLf & _
                                                    ",Num " & vbCrLf & _
                                                    ",KikiInf " & vbCrLf & _
                                                    ",EntryNmb " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "VALUES ( " & vbCrLf & _
                                                    " :IncNmb " & vbCrLf & _
                                                    ",:KindCD " & vbCrLf & _
                                                    ",:Num " & vbCrLf & _
                                                    ",(" & vbCrLf & _
                                                    " SELECT" & vbCrLf & _
                                                    "     COALESCE(cst.SetBuil,'') " & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetBusyoNM,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetRoom,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT km.KikiStateNM FROM KIKISTATE_MTB km WHERE cst.KikiUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_KIKI_RIYOKEITAI & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || CASE COALESCE(cst.KikiState,'') WHEN '' THEN '" & KIKISTATE_NO_INPUT & "' ELSE '" & KIKISTATE_INPUT & "' END" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.ImageNmb,'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT '" & IPUSECD_STATIC_WORD & "' || SUBSTR(km.KikiStateNM,1,1) FROM KIKISTATE_MTB km WHERE cst.IPUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || CASE (SELECT COUNT(1) FROM OPTSOFT_TB ot WHERE cst.CINmb = ot.CINmb) WHEN 0 THEN '" & OPTSOFT_NO_INPUT & "' ELSE '" & OPTSOFT_NO_INPUT & "' END" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(ct.CINM,'')" & vbCrLf & _
                                                    "  FROM CI_INFO_TB ct" & vbCrLf & _
                                                    "  JOIN CI_SAP_TB cst ON ct.CINmb = cst.CINmb" & vbCrLf & _
                                                    "  WHERE ct.CINmb = :CINmb" & vbCrLf & _
                                                    " UNION ALL" & vbCrLf & _
                                                    " SELECT" & vbCrLf & _
                                                    "     COALESCE(cst.SetBuil,'') " & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetBusyoNM,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetRoom,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT '" & IPUSECD_STATIC_WORD & "' || SUBSTR(km.KikiStateNM,1,1) FROM KIKISTATE_MTB km WHERE cst.IPUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
                                                    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(ct.CINM,'')" & vbCrLf & _
                                                    "  FROM CI_INFO_TB ct" & vbCrLf & _
                                                    "  JOIN CI_BUY_TB cst ON ct.CINmb = cst.CINmb" & vbCrLf & _
                                                    "  WHERE ct.CINmb = :CINmb" & vbCrLf & _
                                                    " )" & vbCrLf & _
                                                    ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_KIKI_TB WHERE IncNmb=:IncNmb) " & vbCrLf & _
                                                    ",:RegDT " & vbCrLf & _
                                                    ",:RegGrpCD " & vbCrLf & _
                                                    ",:RegID " & vbCrLf & _
                                                    ",:UpdateDT " & vbCrLf & _
                                                    ",:UpGrpCD " & vbCrLf & _
                                                    ",:UpdateID " & vbCrLf & _
                                                    ") "

    'インシデント共通情報ログ新規登録（INSERT）SQL
    Private strInsertIncInfoLSql As String = "INSERT INTO INCIDENT_INFO_LTB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",LogNo " & vbCrLf & _
                                                    ",ProcessKbn " & vbCrLf & _
                                                    ",UkeKbnCD " & vbCrLf & _
                                                    ",IncKbnCD " & vbCrLf & _
                                                    ",ProcessStateCD " & vbCrLf & _
                                                    ",HasseiDT " & vbCrLf & _
                                                    ",KaitoDT " & vbCrLf & _
                                                    ",KanryoDT " & vbCrLf & _
                                                    ",Priority " & vbCrLf & _
                                                    ",Errlevel " & vbCrLf & _
                                                    ",Title " & vbCrLf & _
                                                    ",UkeNaiyo " & vbCrLf & _
                                                    ",TaioKekka " & vbCrLf & _
                                                    ",SystemNmb " & vbCrLf & _
                                                    ",OutSideToolNmb " & vbCrLf & _
                                                    ",EventID " & vbCrLf & _
                                                    ",Source " & vbCrLf & _
                                                    ",OPCEventID " & vbCrLf & _
                                                    ",EventClass " & vbCrLf & _
                                                    ",TantoGrpCD " & vbCrLf & _
                                                    ",IncTantoID " & vbCrLf & _
                                                    ",IncTantoNM " & vbCrLf & _
                                                    ",DomainCD " & vbCrLf & _
                                                    ",PartnerCompany " & vbCrLf & _
                                                    ",PartnerID " & vbCrLf & _
                                                    ",PartnerNM " & vbCrLf & _
                                                    ",PartnerKana " & vbCrLf & _
                                                    ",PartnerKyokuNM " & vbCrLf & _
                                                    ",UsrBusyoNM " & vbCrLf & _
                                                    ",PartnerTel " & vbCrLf & _
                                                    ",PartnerMailAdd " & vbCrLf & _
                                                    ",PartnerContact " & vbCrLf & _
                                                    ",PartnerBase " & vbCrLf & _
                                                    ",PartnerRoom " & vbCrLf & _
                                                    ",ShijisyoFlg " & vbCrLf & _
                                                    ",Kengen " & vbCrLf & _
                                                    ",RentalKiki " & vbCrLf & _
                                                    ",BIko1 " & vbCrLf & _
                                                    ",BIko2 " & vbCrLf & _
                                                    ",BIko3 " & vbCrLf & _
                                                    ",BIko4 " & vbCrLf & _
                                                    ",BIko5 " & vbCrLf & _
                                                    ",FreeFlg1 " & vbCrLf & _
                                                    ",FreeFlg2 " & vbCrLf & _
                                                    ",FreeFlg3 " & vbCrLf & _
                                                    ",FreeFlg4 " & vbCrLf & _
                                                    ",FreeFlg5 " & vbCrLf & _
                                                    ",TitleAimai " & vbCrLf & _
                                                    ",UkeNaiyoAimai " & vbCrLf & _
                                                    ",BikoAimai " & vbCrLf & _
                                                    ",TaioKekkaAimai " & vbCrLf & _
                                                    ",EventIDAimai " & vbCrLf & _
                                                    ",SourceAimai " & vbCrLf & _
                                                    ",OPCEventIDAimai " & vbCrLf & _
                                                    ",EventClassAimai " & vbCrLf & _
                                                    ",IncTantIDAimai " & vbCrLf & _
                                                    ",IncTantNMAimai " & vbCrLf & _
                                                    ",PartnerIDAimai " & vbCrLf & _
                                                    ",PartnerNMAimai " & vbCrLf & _
                                                    ",UsrBusyoNMAimai " & vbCrLf & _
                                                    ",KigenCondCIKbnCD " & vbCrLf & _
                                                    ",KigenCondTypeKbn " & vbCrLf & _
                                                    ",KigenCondKigen " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "SELECT " & vbCrLf & _
                                                    " it.IncNmb " & vbCrLf & _
                                                    ",:LogNo " & vbCrLf & _
                                                    ",it.ProcessKbn " & vbCrLf & _
                                                    ",it.UkeKbnCD " & vbCrLf & _
                                                    ",it.IncKbnCD " & vbCrLf & _
                                                    ",it.ProcessStateCD " & vbCrLf & _
                                                    ",it.HasseiDT " & vbCrLf & _
                                                    ",it.KaitoDT " & vbCrLf & _
                                                    ",it.KanryoDT " & vbCrLf & _
                                                    ",it.Priority " & vbCrLf & _
                                                    ",it.Errlevel " & vbCrLf & _
                                                    ",it.Title " & vbCrLf & _
                                                    ",it.UkeNaiyo " & vbCrLf & _
                                                    ",it.TaioKekka " & vbCrLf & _
                                                    ",it.SystemNmb " & vbCrLf & _
                                                    ",it.OutSideToolNmb " & vbCrLf & _
                                                    ",it.EventID " & vbCrLf & _
                                                    ",it.Source " & vbCrLf & _
                                                    ",it.OPCEventID " & vbCrLf & _
                                                    ",it.EventClass " & vbCrLf & _
                                                    ",it.TantoGrpCD " & vbCrLf & _
                                                    ",it.IncTantoID " & vbCrLf & _
                                                    ",it.IncTantoNM " & vbCrLf & _
                                                    ",it.DomainCD " & vbCrLf & _
                                                    ",it.PartnerCompany " & vbCrLf & _
                                                    ",it.PartnerID " & vbCrLf & _
                                                    ",it.PartnerNM " & vbCrLf & _
                                                    ",it.PartnerKana " & vbCrLf & _
                                                    ",it.PartnerKyokuNM " & vbCrLf & _
                                                    ",it.UsrBusyoNM " & vbCrLf & _
                                                    ",it.PartnerTel " & vbCrLf & _
                                                    ",it.PartnerMailAdd " & vbCrLf & _
                                                    ",it.PartnerContact " & vbCrLf & _
                                                    ",it.PartnerBase " & vbCrLf & _
                                                    ",it.PartnerRoom " & vbCrLf & _
                                                    ",it.ShijisyoFlg " & vbCrLf & _
                                                    ",it.Kengen " & vbCrLf & _
                                                    ",it.RentalKiki " & vbCrLf & _
                                                    ",it.BIko1 " & vbCrLf & _
                                                    ",it.BIko2 " & vbCrLf & _
                                                    ",it.BIko3 " & vbCrLf & _
                                                    ",it.BIko4 " & vbCrLf & _
                                                    ",it.BIko5 " & vbCrLf & _
                                                    ",it.FreeFlg1 " & vbCrLf & _
                                                    ",it.FreeFlg2 " & vbCrLf & _
                                                    ",it.FreeFlg3 " & vbCrLf & _
                                                    ",it.FreeFlg4 " & vbCrLf & _
                                                    ",it.FreeFlg5 " & vbCrLf & _
                                                    ",it.TitleAimai " & vbCrLf & _
                                                    ",it.UkeNaiyoAimai " & vbCrLf & _
                                                    ",it.BikoAimai " & vbCrLf & _
                                                    ",it.TaioKekkaAimai " & vbCrLf & _
                                                    ",it.EventIDAimai " & vbCrLf & _
                                                    ",it.SourceAimai " & vbCrLf & _
                                                    ",it.OPCEventIDAimai " & vbCrLf & _
                                                    ",it.EventClassAimai " & vbCrLf & _
                                                    ",it.IncTantIDAimai " & vbCrLf & _
                                                    ",it.IncTantNMAimai " & vbCrLf & _
                                                    ",it.PartnerIDAimai " & vbCrLf & _
                                                    ",it.PartnerNMAimai " & vbCrLf & _
                                                    ",it.UsrBusyoNMAimai " & vbCrLf & _
                                                    ",it.KigenCondCIKbnCD " & vbCrLf & _
                                                    ",it.KigenCondTypeKbn " & vbCrLf & _
                                                    ",it.KigenCondKigen " & vbCrLf & _
                                                    ",it.RegDT " & vbCrLf & _
                                                    ",it.RegGrpCD " & vbCrLf & _
                                                    ",it.RegID " & vbCrLf & _
                                                    ",it.UpdateDT " & vbCrLf & _
                                                    ",it.UpGrpCD " & vbCrLf & _
                                                    ",it.UpdateID " & vbCrLf & _
                                                    "FROM INCIDENT_INFO_TB it " & vbCrLf & _
                                                    "WHERE it.IncNmb = :IncNmb "

    'インシデント機器情報ログ新規登録（INSERT）SQL
    Private strInsertIncKikiLSql As String = "INSERT INTO INCIDENT_KIKI_LTB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",LogNo " & vbCrLf & _
                                                    ",KindCD " & vbCrLf & _
                                                    ",Num " & vbCrLf & _
                                                    ",KikiInf " & vbCrLf & _
                                                    ",EntryNmb " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "SELECT " & vbCrLf & _
                                                    " ik.IncNmb " & vbCrLf & _
                                                    ",:LogNo " & vbCrLf & _
                                                    ",ik.KindCD " & vbCrLf & _
                                                    ",ik.Num " & vbCrLf & _
                                                    ",ik.KikiInf " & vbCrLf & _
                                                    ",ik.EntryNmb " & vbCrLf & _
                                                    ",ik.UpdateDT " & vbCrLf & _
                                                    ",ik.UpGrpCD " & vbCrLf & _
                                                    ",ik.UpdateID " & vbCrLf & _
                                                    ",ik.UpdateDT " & vbCrLf & _
                                                    ",ik.UpGrpCD " & vbCrLf & _
                                                    ",ik.UpdateID " & vbCrLf & _
                                                    "FROM INCIDENT_KIKI_TB ik " & vbCrLf & _
                                                    "WHERE ik.IncNmb = :IncNmb "

    'インシデント作業履歴ログ新規登録（INSERT）SQL
    Private strInsertIncWkRirekiLSql As String = "INSERT INTO INCIDENT_WK_RIREKI_LTB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",LogNo " & vbCrLf & _
                                                    ",WorkRirekiNmb " & vbCrLf & _
                                                    ",KeikaKbnCD " & vbCrLf & _
                                                    ",WorkNaiyo " & vbCrLf & _
                                                    ",WorkSceDT " & vbCrLf & _
                                                    ",WorkStDT " & vbCrLf & _
                                                    ",WorkEdDT " & vbCrLf & _
                                                    ",SystemNmb " & vbCrLf & _
                                                    ",WorkNaiyoAimai " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "SELECT " & vbCrLf & _
                                                    " iwr.IncNmb " & vbCrLf & _
                                                    ",:LogNo " & vbCrLf & _
                                                    ",iwr.WorkRirekiNmb " & vbCrLf & _
                                                    ",iwr.KeikaKbnCD " & vbCrLf & _
                                                    ",iwr.WorkNaiyo " & vbCrLf & _
                                                    ",iwr.WorkSceDT " & vbCrLf & _
                                                    ",iwr.WorkStDT " & vbCrLf & _
                                                    ",iwr.WorkEdDT " & vbCrLf & _
                                                    ",iwr.SystemNmb " & vbCrLf & _
                                                    ",iwr.WorkNaiyoAimai " & vbCrLf & _
                                                    ",iwr.UpdateDT " & vbCrLf & _
                                                    ",iwr.UpGrpCD " & vbCrLf & _
                                                    ",iwr.UpdateID " & vbCrLf & _
                                                    ",iwr.UpdateDT " & vbCrLf & _
                                                    ",iwr.UpGrpCD " & vbCrLf & _
                                                    ",iwr.UpdateID " & vbCrLf & _
                                                    "FROM INCIDENT_WK_RIREKI_TB iwr " & vbCrLf & _
                                                    "WHERE iwr.IncNmb = :IncNmb " & vbCrLf & _
                                                    "AND iwr.WorkRirekiNmb = :RirekiNo "

    'インシデント作業担当ログ新規登録（INSERT）SQL
    Private strInsertIncWkTantoLSql As String = "INSERT INTO INCIDENT_WK_TANTO_LTB ( " & vbCrLf & _
                                                    " IncNmb " & vbCrLf & _
                                                    ",LogNo " & vbCrLf & _
                                                    ",WorkRirekiNmb " & vbCrLf & _
                                                    ",worktantonmb " & vbCrLf & _
                                                    ",WorkTantoGrpCD " & vbCrLf & _
                                                    ",WorkTantoID " & vbCrLf & _
                                                    ",WorkTantoGrpNM " & vbCrLf & _
                                                    ",WorkTantoNM " & vbCrLf & _
                                                    ",RegDT " & vbCrLf & _
                                                    ",RegGrpCD " & vbCrLf & _
                                                    ",RegID " & vbCrLf & _
                                                    ",UpdateDT " & vbCrLf & _
                                                    ",UpGrpCD " & vbCrLf & _
                                                    ",UpdateID " & vbCrLf & _
                                                    ") " & vbCrLf & _
                                                    "SELECT " & vbCrLf & _
                                                    " iwt.IncNmb " & vbCrLf & _
                                                    ",:LogNo " & vbCrLf & _
                                                    ",iwt.WorkRirekiNmb " & vbCrLf & _
                                                    ",iwt.worktantonmb " & vbCrLf & _
                                                    ",iwt.WorkTantoGrpCD " & vbCrLf & _
                                                    ",iwt.WorkTantoID " & vbCrLf & _
                                                    ",iwt.WorkTantoGrpNM " & vbCrLf & _
                                                    ",iwt.WorkTantoNM " & vbCrLf & _
                                                    ",iwt.UpdateDT " & vbCrLf & _
                                                    ",iwt.UpGrpCD " & vbCrLf & _
                                                    ",iwt.UpdateID " & vbCrLf & _
                                                    ",iwt.UpdateDT " & vbCrLf & _
                                                    ",iwt.UpGrpCD " & vbCrLf & _
                                                    ",iwt.UpdateID " & vbCrLf & _
                                                    "FROM INCIDENT_WK_TANTO_TB iwt " & vbCrLf & _
                                                    "WHERE iwt.IncNmb = :IncNmb " & vbCrLf & _
                                                    "AND iwt.WorkRirekiNmb = :RirekiNo "

    'INC対応関係者（INSERT）SQL
    Private strInsertIncKankeiSql As String = "INSERT INTO INCIDENT_KANKEI_TB ( " & vbCrLf & _
                                              " IncNmb " & vbCrLf & _
                                              ",RelationKbn " & vbCrLf & _
                                              ",RelationID " & vbCrLf & _
                                              ",EntryNmb " & vbCrLf & _
                                              ",RegDT " & vbCrLf & _
                                              ",RegGrpCD " & vbCrLf & _
                                              ",RegID " & vbCrLf & _
                                              ",UpdateDT " & vbCrLf & _
                                              ",UpGrpCD " & vbCrLf & _
                                              ",UpdateID " & vbCrLf & _
                                              ")" & vbCrLf & _
                                              "SELECT" & vbCrLf & _
                                              " :IncNmb " & vbCrLf & _
                                              ",t.RelationKbn " & vbCrLf & _
                                              ",t.RelationID " & vbCrLf & _
                                              ",t.EntryNmb " & vbCrLf & _
                                              ",:RegDT " & vbCrLf & _
                                              ",:RegGrpCD " & vbCrLf & _
                                              ",:RegID " & vbCrLf & _
                                              ",:UpdateDT " & vbCrLf & _
                                              ",:UpGrpCD " & vbCrLf & _
                                              ",:UpdateID " & vbCrLf & _
                                              "FROM (" & vbCrLf & _
                                              "  SELECT" & vbCrLf & _
                                              "     t2.RelationKbn" & vbCrLf & _
                                              "    ,t2.RelationID" & vbCrLf & _
                                              "    ,ROW_NUMBER() OVER(PARTITION BY t2.CINmb) AS EntryNmb" & vbCrLf & _
                                              "  FROM (" & vbCrLf & _
                                              "     SELECT" & vbCrLf & _
                                              "         kt.RelationKbn" & vbCrLf & _
                                              "        ,kt.RelationID" & vbCrLf & _
                                              "        ,kt.CINmb" & vbCrLf & _
                                              "     FROM KANKEI_TB kt" & vbCrLf & _
                                              "     WHERE CINmb = :SystemNmb" & vbCrLf & _
                                              "     UNION" & vbCrLf & _
                                              "     SELECT" & vbCrLf & _
                                              "         '" & KBN_GROUP & "' AS RelationKbn" & vbCrLf & _
                                              "        ,:RelationID AS RelationID" & vbCrLf & _
                                              "        ,:SystemNmb AS CINmb" & vbCrLf & _
                                              "  ) t2" & vbCrLf & _
                                              ") t"

    'INC対応関係者ログ（insert）SQL
    Private strInsertIncKankeiLSql As String = "INSERT INTO incident_kankei_ltb ( " & vbCrLf & _
                                               " IncNmb " & vbCrLf & _
                                               ",LogNo " & vbCrLf & _
                                               ",RelationKbn " & vbCrLf & _
                                               ",RelationID " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               ")" & vbCrLf & _
                                               "SELECT " & vbCrLf & _
                                               " IncNmb " & vbCrLf & _
                                               ",:LogNo " & vbCrLf & _
                                               ",RelationKbn " & vbCrLf & _
                                               ",RelationID " & vbCrLf & _
                                               ",EntryNmb " & vbCrLf & _
                                               ",RegDT " & vbCrLf & _
                                               ",RegGrpCD " & vbCrLf & _
                                               ",RegID " & vbCrLf & _
                                               ",UpdateDT " & vbCrLf & _
                                               ",UpGrpCD " & vbCrLf & _
                                               ",UpdateID " & vbCrLf & _
                                               "FROM INCIDENT_KANKEI_TB " & vbCrLf & _
                                               "WHERE IncNmb = :IncNmb "


    ''' <summary>
    ''' 受付手段コードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>受付手段コードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectUketsukewayCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectUketsukewaySql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("UketsukeWayNM", NpgsqlTypes.NpgsqlDbType.Varchar))            '受付手段
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("UketsukeWayNM").Value = dataHBKC0601.PropAryUkeKbnCD(intIndex).ToString    '受付手段
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
    ''' インシデント種別コードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント種別コードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectIncidentKindCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectIncidentKindSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("IncKindNM", NpgsqlTypes.NpgsqlDbType.Varchar))    'インシデント種別
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("IncKindNM").Value = dataHBKC0601.PropAryIncKbnCD(intIndex).ToString    'インシデント種別
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
    ''' プロセスステータスコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>プロセスステータスコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectProcessStateCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectProcessStateSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("ProcessStateNM", NpgsqlTypes.NpgsqlDbType.Varchar))   'ステータス
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("ProcessKbn").Value = CI_TYPE_SYSTEM                                                'プロセス区分
                .Parameters("ProcessStateNM").Value = dataHBKC0601.PropAryProcessStatusCD(intIndex).ToString    'ステータス
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
    ''' システム番号のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>システム番号のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectSystemNmbSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601, _
                                            ByVal intIndex As Integer, _
                                            ByRef strCINM As String) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectSystemNmbSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("CINM", NpgsqlTypes.NpgsqlDbType.Varchar))     'システム番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))  'CI種別CD
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("CINM").Value = strCINM                                     'システム番号
                .Parameters("CIKbnCD").Value = CI_TYPE_SYSTEM
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
    ''' ドメインコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ドメインコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectDomainCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectDomainSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("DomainNM", NpgsqlTypes.NpgsqlDbType.Varchar))             'ドメイン
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("DomainNM").Value = dataHBKC0601.PropAryDomainCD(intIndex).ToString     'ドメイン
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
    ''' 経過種別コードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>経過種別コードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKeikaKindCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKeikaKindSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KeikaKindNM", NpgsqlTypes.NpgsqlDbType.Varchar))              '経過種別
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KeikaKindNM").Value = dataHBKC0601.PropAryKeikaKbnCD(intIndex).ToString    '経過種別
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
    ''' 機器種別コードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器種別コードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKikiKindCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKikiKindSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KindNM", NpgsqlTypes.NpgsqlDbType.Varchar))           '機器種別
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KindNM").Value = dataHBKC0601.PropAryKindCD(intIndex).ToString     '機器種別
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
    ''' 機器のデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]インシデント一括登録Dataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>機器のデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/16 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectKikiSql(ByRef Adapter As NpgsqlDataAdapter, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601, _
                                            ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectKikiSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))              '番号
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("KindCD").Value = dataHBKC0601.PropAryKindCD(intIndex).ToString     '種別CD
                .Parameters("Num").Value = dataHBKC0601.PropAryNum(intIndex).ToString           '番号

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
    ''' グループコードのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>グループコードのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectGroupCDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKC0601 As DataHBKC0601, _
                                             ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectGroupSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("GroupCD", NpgsqlTypes.NpgsqlDbType.Varchar))  'グループCD
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("GroupCD").Value = dataHBKC0601.PropStrGroupCD              'グループCD
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
    ''' ユーザーIDのデータ有無取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>ユーザーIDから関係者IDのデータ有無取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectUsrIDSql(ByRef Adapter As NpgsqlDataAdapter, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = strSelectUsrSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Adapter.SelectCommand.Parameters
                .Add(New NpgsqlParameter("HBKUsrID", NpgsqlTypes.NpgsqlDbType.Varchar)) 'ユーザーID
            End With

            'バインド変数に値をセット
            With Adapter.SelectCommand
                .Parameters("HBKUsrID").Value = dataHBKC0601.PropStrUsrID               'ユーザーID
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
    ''' 新規インシデント番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規インシデント番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewIncNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                   ByVal Cn As NpgsqlConnection, _
                                                   ByVal dataHBKC0601 As DataHBKC0601) As Boolean
        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try

            'SQL文(SELECT)
            strSQL = GET_NEXTVAL_INCIDENT_NO

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
    ''' インシデント共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""                   'SQL文
        Dim strTitleAimai As String = ""            'タイトル（あいまい）
        Dim strUkeNaiyoAimai As String = ""         '受付内容（あいまい）
        Dim strTaioKekkaAimai As String = ""        '対応結果（あいまい）
        Dim strEventIDAimai As String = ""          'イベントID（あいまい）
        Dim strSourceAimai As String = ""           'ソース（あいまい）
        Dim strOPCEventIDAimai As String = ""       'OPCイベントID（あいまい）
        Dim strEventClassAimai As String = ""       'イベントクラス（あいまい）
        Dim strIncTantIDAimai As String = ""        'インシデント担当者CD（あいまい）
        Dim strIncTantNMAimai As String = ""        'インシデント担当者氏名（あいまい）
        Dim strPartnerIDAimai As String = ""        '相手ID（あいまい）
        Dim strPartnerNMAimai As String = ""        '相手氏名（あいまい）
        Dim strUsrBusyoNMAimai As String = ""       '相手部署（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'インシデント番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))       'プロセス区分
                .Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '受付手段CD
                .Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))         'インシデント種別CD
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))   'プロセスステータスCD
                .Add(New NpgsqlParameter("HasseiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '発生日時
                .Add(New NpgsqlParameter("KaitoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))        '回答日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '完了日時
                .Add(New NpgsqlParameter("Priority", NpgsqlTypes.NpgsqlDbType.Varchar))         '重要度
                .Add(New NpgsqlParameter("Errlevel", NpgsqlTypes.NpgsqlDbType.Varchar))         '障害レベル
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))            'タイトル
                .Add(New NpgsqlParameter("UkeNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))         '受付内容
                .Add(New NpgsqlParameter("TaioKekka", NpgsqlTypes.NpgsqlDbType.Varchar))        '対応結果
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '対象システム番号
                .Add(New NpgsqlParameter("OutSideToolNmb", NpgsqlTypes.NpgsqlDbType.Varchar))   '外部ツール番号
                .Add(New NpgsqlParameter("EventID", NpgsqlTypes.NpgsqlDbType.Varchar))          'イベントID
                .Add(New NpgsqlParameter("Source", NpgsqlTypes.NpgsqlDbType.Varchar))           'ソース
                .Add(New NpgsqlParameter("OPCEventID", NpgsqlTypes.NpgsqlDbType.Varchar))       'OPCイベントID
                .Add(New NpgsqlParameter("EventClass", NpgsqlTypes.NpgsqlDbType.Varchar))       'イベントクラス
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当グループCD
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))       'インシデント担当者CD
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       'インシデント担当者氏名
                .Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))         'ドメインCD
                .Add(New NpgsqlParameter("PartnerCompany", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手会社名
                .Add(New NpgsqlParameter("PartnerID", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手ID
                .Add(New NpgsqlParameter("PartnerNM", NpgsqlTypes.NpgsqlDbType.Varchar))        '相手氏名
                .Add(New NpgsqlParameter("PartnerKana", NpgsqlTypes.NpgsqlDbType.Varchar))      '相手シメイ
                .Add(New NpgsqlParameter("PartnerKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手部署
                .Add(New NpgsqlParameter("PartnerTel", NpgsqlTypes.NpgsqlDbType.Varchar))       '相手電話番号
                .Add(New NpgsqlParameter("PartnerMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手メールアドレス
                .Add(New NpgsqlParameter("PartnerContact", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手連絡先
                .Add(New NpgsqlParameter("PartnerBase", NpgsqlTypes.NpgsqlDbType.Varchar))      '相手拠点
                .Add(New NpgsqlParameter("PartnerRoom", NpgsqlTypes.NpgsqlDbType.Varchar))      '相手番組/部屋
                .Add(New NpgsqlParameter("ShijisyoFlg", NpgsqlTypes.NpgsqlDbType.Varchar))      '指示書フラグ
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))       'タイトル(あいまい)
                .Add(New NpgsqlParameter("UkeNaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))    '受付内容(あいまい)
                .Add(New NpgsqlParameter("TaioKekkaAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '対応結果(あいまい)
                .Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))     'イベントID(あいまい)
                .Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))      'ソース(あいまい)
                .Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  'OPCイベントID(あいまい)
                .Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  'イベントクラス(あいまい)
                .Add(New NpgsqlParameter("IncTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'インシデント担当者ID(あいまい)
                .Add(New NpgsqlParameter("IncTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   'インシデント担当者氏名(あいまい)
                .Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手ID(あいまい)
                .Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '相手氏名(あいまい)
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))  '相手部署(あいまい)

                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                                        'インシデント番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                         'プロセス区分
                .Parameters("UkeKbnCD").Value = dataHBKC0601.PropAryUkeKbnCD(intIndex).ToString                 '受付区分
                .Parameters("IncKbnCD").Value = dataHBKC0601.PropAryIncKbnCD(intIndex).ToString                 'インシデント種別CD
                .Parameters("ProcessStateCD").Value = dataHBKC0601.PropAryProcessStatusCD(intIndex).ToString    'プロセスステータスCD
                If dataHBKC0601.PropAryHasseiDT(intIndex) = "" Then                                             '発生日時
                    .Parameters("HasseiDT").Value = DBNull.Value
                Else
                    .Parameters("HasseiDT").Value = DateTime.Parse(dataHBKC0601.PropAryHasseiDT(intIndex))
                End If
                If dataHBKC0601.PropAryKaitoDT(intIndex) = "" Then                                              '回答日時
                    .Parameters("KaitoDT").Value = DBNull.Value
                Else
                    .Parameters("KaitoDT").Value = DateTime.Parse(dataHBKC0601.PropAryKaitoDT(intIndex))
                End If
                If dataHBKC0601.PropAryKanryoDT(intIndex) = "" Then                                             '完了日時
                    .Parameters("KanryoDT").Value = DBNull.Value
                Else
                    .Parameters("KanryoDT").Value = DateTime.Parse(dataHBKC0601.PropAryKanryoDT(intIndex))
                End If
                .Parameters("Priority").Value = dataHBKC0601.PropAryPriority(intIndex).ToString                 '重要度
                .Parameters("Errlevel").Value = dataHBKC0601.PropAryErrLevel(intIndex).ToString                 '障害レベル
                .Parameters("Title").Value = dataHBKC0601.PropAryTitle(intIndex).ToString                       'タイトル
                .Parameters("UkeNaiyo").Value = dataHBKC0601.PropAryUkeNaiyo(intIndex).ToString                 '受付内容
                .Parameters("TaioKekka").Value = dataHBKC0601.PropAryTaioKekka(intIndex).ToString               '対応結果
                If dataHBKC0601.PropArySystemNmb(intIndex).ToString = "" Then
                    .Parameters("SystemNmb").Value = 0                                                          '対象システム番号
                Else
                    .Parameters("SystemNmb").Value = dataHBKC0601.PropArySystemNmb(intIndex)                    '対象システム番号
                End If
                .Parameters("OutSideToolNmb").Value = dataHBKC0601.PropAryOutSideToolNmb(intIndex).ToString     '外部ツール番号
                .Parameters("EventID").Value = dataHBKC0601.PropAryEventID(intIndex).ToString                   'イベントID
                .Parameters("Source").Value = dataHBKC0601.PropArySource(intIndex).ToString                     'ソース
                .Parameters("OPCEventID").Value = dataHBKC0601.PropAryOPCEventID(intIndex).ToString             'OPCイベントID
                .Parameters("EventClass").Value = dataHBKC0601.PropAryEventClass(intIndex).ToString             'イベントクラス
                .Parameters("TantoGrpCD").Value = dataHBKC0601.PropAryTantoGrpCD(intIndex).ToString             '担当者グループCD
                .Parameters("IncTantoID").Value = dataHBKC0601.PropAryIncTantoID(intIndex).ToString             '担当者ID
                .Parameters("IncTantoNM").Value = dataHBKC0601.PropAryIncTantoNM(intIndex).ToString             '担当者名
                .Parameters("DomainCD").Value = dataHBKC0601.PropAryDomainCD(intIndex).ToString                 'ドメインCD
                .Parameters("PartnerCompany").Value = dataHBKC0601.PropAryPartnerCompany(intIndex).ToString     '相手会社
                .Parameters("PartnerID").Value = dataHBKC0601.PropAryPartnerID(intIndex).ToString               '相手ID
                .Parameters("PartnerNM").Value = dataHBKC0601.PropAryPartnerNM(intIndex).ToString               '相手氏名
                .Parameters("PartnerKana").Value = dataHBKC0601.PropAryPartnerKana(intIndex).ToString           '相手シメイ
                .Parameters("PartnerKyokuNM").Value = dataHBKC0601.PropAryPartnerKyokuNM(intIndex).ToString     '相手局
                .Parameters("UsrBusyoNM").Value = dataHBKC0601.PropAryUsrBusyoNM(intIndex).ToString             '相手部署
                .Parameters("PartnerTel").Value = dataHBKC0601.PropAryPartnerTel(intIndex).ToString             '相手電話番号
                .Parameters("PartnerMailAdd").Value = dataHBKC0601.PropAryPartnerMailAdd(intIndex).ToString     '相手メールアドレス
                .Parameters("PartnerContact").Value = dataHBKC0601.PropAryPartnerContact(intIndex).ToString     '相手連絡先
                .Parameters("PartnerBase").Value = dataHBKC0601.PropAryPartnerBase(intIndex).ToString           '相手拠点
                .Parameters("PartnerRoom").Value = dataHBKC0601.PropAryPartnerRoom(intIndex).ToString           '相手番組/部屋
                .Parameters("ShijisyoFlg").Value = dataHBKC0601.PropAryShijisyoFlg(intIndex).ToString           '指示書フラグ
                'あいまい検索文字列設定
                strTitleAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryTitle(intIndex).ToString)
                strUkeNaiyoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryUkeNaiyo(intIndex).ToString)
                strTaioKekkaAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryTaioKekka(intIndex).ToString)
                strEventIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryEventID(intIndex).ToString)
                strSourceAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropArySource(intIndex).ToString)
                strOPCEventIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryOPCEventID(intIndex).ToString)
                strEventClassAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryEventClass(intIndex).ToString)
                strIncTantIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryIncTantoID(intIndex).ToString)
                strIncTantNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryIncTantoNM(intIndex).ToString)
                strPartnerIDAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryPartnerID(intIndex).ToString)
                strPartnerNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryPartnerNM(intIndex).ToString)
                strUsrBusyoNMAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryUsrBusyoNM(intIndex).ToString)

                .Parameters("TitleAimai").Value = strTitleAimai                 'タイトル（あいまい）
                .Parameters("UkeNaiyoAimai").Value = strUkeNaiyoAimai           '受付内容（あいまい）
                .Parameters("TaioKekkaAimai").Value = strTaioKekkaAimai         '対応結果（あいまい）
                .Parameters("EventIDAimai").Value = strEventIDAimai             'イベントID（あいまい）
                .Parameters("SourceAimai").Value = strSourceAimai               'ソース（あいまい
                .Parameters("OPCEventIDAimai").Value = strOPCEventIDAimai       'OPCイベントID（あいまい
                .Parameters("EventClassAimai").Value = strEventClassAimai       'イベントクラス（あいまい
                .Parameters("IncTantIDAimai").Value = strIncTantIDAimai         'インシデント担当者ID（あいまい
                .Parameters("IncTantNMAimai").Value = strIncTantNMAimai         'インシデント担当者氏名(あいまい
                .Parameters("PartnerIDAimai").Value = strPartnerIDAimai         '相手ID（あいまい
                .Parameters("PartnerNMAimai").Value = strPartnerNMAimai         '相手氏名（あいまい
                .Parameters("UsrBusyoNMAimai").Value = strUsrBusyoNMAimai       '相手部署（あいまい

                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate     '最終更新日時
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
    ''' インシデント作業履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント作業履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncWkRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文
        Dim strWorkNaiyoAimai As String = ""    '作業内容（あいまい）

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncWkRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'インシデント番号
                .Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業履歴番号
                .Add(New NpgsqlParameter("KeikaKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '経過種別CD
                .Add(New NpgsqlParameter("WorkNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))        '作業内容
                .Add(New NpgsqlParameter("WorkSceDT", NpgsqlTypes.NpgsqlDbType.Timestamp))      '作業予定日時
                .Add(New NpgsqlParameter("WorkStDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '作業開始日時
                .Add(New NpgsqlParameter("WorkEdDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '作業終了日時
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))        '対象システム番号
                .Add(New NpgsqlParameter("WorkNaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業内容(あいまい)
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                                'インシデント番号
                .Parameters("WorkRirekiNmb").Value = dataHBKC0601.PropIntRirekiNo                       '作業履歴番号
                .Parameters("KeikaKbnCD").Value = dataHBKC0601.PropAryKeikaKbnCD(intIndex).ToString     '経過種別CD
                .Parameters("WorkNaiyo").Value = dataHBKC0601.PropAryWorkNaiyo(intIndex).ToString       '作業内容
                If dataHBKC0601.PropAryWorkSceDT(intIndex) = "" Then                                    '作業予定日時
                    .Parameters("WorkSceDT").Value = DBNull.Value
                Else
                    .Parameters("WorkSceDT").Value = DateTime.Parse(dataHBKC0601.PropAryWorkSceDT(intIndex))
                End If
                If dataHBKC0601.PropAryWorkStDT(intIndex) = "" Then                                     '作業開始日時
                    .Parameters("WorkStDT").Value = DBNull.Value
                Else
                    .Parameters("WorkStDT").Value = DateTime.Parse(dataHBKC0601.PropAryWorkStDT(intIndex))
                End If
                If dataHBKC0601.PropAryWorkEdDT(intIndex) = "" Then                                     '作業終了日時
                    .Parameters("WorkEdDT").Value = DBNull.Value
                Else
                    .Parameters("WorkEdDT").Value = DateTime.Parse(dataHBKC0601.PropAryWorkEdDT(intIndex))
                End If
                If dataHBKC0601.PropArySystemNmb2(intIndex).ToString = "" Then
                    .Parameters("SystemNmb").Value = 0                                                  '対象システム番号
                Else
                    .Parameters("SystemNmb").Value = dataHBKC0601.PropArySystemNmb2(intIndex)           '対象システム番号
                End If

                'あいまい検索文字列設定
                strWorkNaiyoAimai = commonLogicHBK.ChangeStringForSearch(dataHBKC0601.PropAryWorkNaiyo(intIndex).ToString)
                .Parameters("WorkNaiyoAimai").Value = strWorkNaiyoAimai         '作業内容（あいまい）

                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate     '最終更新日時
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
    ''' インシデント担当履歴新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント担当履歴新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKC0601 As DataHBKC0601, _
                                               ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncTantoRirekiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'インシデント番号
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))       '担当グループCD
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))       'インシデント担当者ID
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))       'インシデント担当者氏名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                        'インシデント番号
                .Parameters("TantoGrpCD").Value = dataHBKC0601.PropAryTantoGrpCD(intIndex)      '担当グループCD
                .Parameters("IncTantoID").Value = dataHBKC0601.PropAryIncTantoID(intIndex)      'インシデント担当者ID
                .Parameters("IncTantoNM").Value = dataHBKC0601.PropAryIncTantoNM(intIndex)      'インシデント担当者氏名
                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate                     '最終更新日時
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
    ''' インシデント作業担当新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント作業担当新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncWkTantoSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601, _
                                                ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncWkTantoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'インシデント番号
                .Add(New NpgsqlParameter("WorkRirekiNmb", NpgsqlTypes.NpgsqlDbType.Integer))    '作業履歴番号
                .Add(New NpgsqlParameter("worktantonmb", NpgsqlTypes.NpgsqlDbType.Integer))     '作業当番号
                .Add(New NpgsqlParameter("WorkTantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))   '作業担当グループCD
                .Add(New NpgsqlParameter("WorkTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))      '作業担当者ID
                .Add(New NpgsqlParameter("WorkTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))      '作業担当者名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                'インシデント番号
                .Parameters("WorkRirekiNmb").Value = dataHBKC0601.PropIntRirekiNo       '作業履歴番号
                .Parameters("worktantonmb").Value = dataHBKC0601.PropIntTantoNo         '作業担当番号
                .Parameters("WorkTantoGrpCD").Value = dataHBKC0601.PropStrGroupCD       '作業担当グループCD
                .Parameters("WorkTantoID").Value = dataHBKC0601.PropStrUsrID            '作業担当者ID
                .Parameters("WorkTantoNM").Value = dataHBKC0601.PropStrUsrNM            '作業担当者名
                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate                '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                         '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                 '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate             '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                          '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                              '最終更新者ID
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
    ''' インシデント機器情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]インデックス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント機器情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKC0601 As DataHBKC0601, _
                                        ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKikiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))           'インシデント番号
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))           '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))              '番号
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))            'CI番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))         '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                        'インシデント番号
                .Parameters("KindCD").Value = dataHBKC0601.PropAryKindCD(intIndex).ToString     '種別CD
                .Parameters("Num").Value = dataHBKC0601.PropAryNum(intIndex).ToString           '番号
                .Parameters("CINmb").Value = dataHBKC0601.PropAryKikiCINmb(intIndex)            'CI番号
                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate                     '最終更新日時
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
    ''' インシデント共通情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント共通情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoLSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncInfoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))   'インシデント番号
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))    'ログNo
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                'インシデント番号
                .Parameters("LogNo").Value = dataHBKC0601.PropIntLogNo                  'ログNo
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
    ''' インシデント作業履歴ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント作業履歴ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncWkRirekiLSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncWkRirekiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '作業履歴番号
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                    'インシデント番号
                .Parameters("RirekiNo").Value = dataHBKC0601.PropIntRirekiNo                '履歴番号
                .Parameters("LogNo").Value = dataHBKC0601.PropIntLogNo                      'ログNo
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
    ''' インシデント作業担当ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント作業担当ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncWkTantoLSql(ByRef Cmd As NpgsqlCommand, _
                                                ByVal Cn As NpgsqlConnection, _
                                                ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncWkTantoLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
                .Add(New NpgsqlParameter("RirekiNo", NpgsqlTypes.NpgsqlDbType.Integer))     '作業履歴番号
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                    'インシデント番号
                .Parameters("RirekiNo").Value = dataHBKC0601.PropIntRirekiNo                '履歴番号
                .Parameters("LogNo").Value = dataHBKC0601.PropIntLogNo                      'ログNo

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
    ''' インシデント機器情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント機器情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/24 k.imayama
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiLSql(ByRef Cmd As NpgsqlCommand, _
                                            ByVal Cn As NpgsqlConnection, _
                                            ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKikiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))       'インシデント番号
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))        'ログNo
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                    'インシデント番号
                .Parameters("LogNo").Value = dataHBKC0601.PropIntLogNo                      'ログNo

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
    ''' 【新規登録】対応関係者情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <param name="intIndex">[IN]データ行番号</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/13 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertINCKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKC0601 As DataHBKC0601, _
                                          ByVal intIndex As Integer) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKankeiSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                           'INC番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                          '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                         '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                            '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                       '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                          '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                         '最終更新者ID
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))                        '対象システム（CI番号）
                .Add(New NpgsqlParameter("RelationID", NpgsqlTypes.NpgsqlDbType.Varchar))                       '関係ID
            End With
            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                                        'INC番号
                .Parameters("RegDT").Value = dataHBKC0601.PropDtmSysDate                                        '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                                 '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                         '登録者ID
                .Parameters("UpdateDT").Value = dataHBKC0601.PropDtmSysDate                                     '最終更新日時
                .Parameters("UpGrpCD").Value = PropWorkGroupCD                                                  '最終更新者グループCD
                .Parameters("UpdateID").Value = PropUserId                                                      '最終更新者ID
                If dataHBKC0601.PropArySystemNmb(intIndex).ToString <> "" Then
                    .Parameters("SystemNmb").Value = Integer.Parse(dataHBKC0601.PropArySystemNmb(intIndex))     '対象システム（CI番号）
                Else
                    .Parameters("SystemNmb").Value = 0
                End If
                .Parameters("RelationID").Value = PropWorkGroupCD                                               '関係ID            
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
    ''' 【共通】INC対応関係情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKC0601">[IN/OUT]一括登録　システムDataクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>対応関係者情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/13 m.ibuki
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKankeiLSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKC0601 As DataHBKC0601) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKankeiLSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                            'ログNo
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                           'INC番号
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("LogNo").Value = dataHBKC0601.PropIntLogNo                                          'ログNo
                .Parameters("IncNmb").Value = dataHBKC0601.PropIntIncNmb                                        'INC番号
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
