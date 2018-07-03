Imports Npgsql
Imports Common
Imports CommonHBK
Imports System.Text

''' <summary>
''' レンタル及び部所有機器の期限切れ検索一覧画面Sqlクラス
''' </summary>
''' <remarks>レンタル及び部所有機器の期限切れ検索一覧画面のSQLの作成・設定を行う
''' <para>作成情報：2012/07/05 kawate
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class SqlHBKB0801

    'インスタンス作成
    Private commonLogicHBK As New CommonLogicHBK

    '定数宣言
    Private Const TITLE_USERID = "期限切れお知らせ"         'ユーザーID選択時のインシデント共通情報.タイトル

    'SQL文宣言

    '【共通】[SELECT]メイン表示用データ件数取得：SELECT句
    Private strSelectMainDataCntSql As String = _
        "SELECT COUNT(1)" & vbCrLf

    '【共通】[SELECT]メイン表示用データ取得：SELECT句
    Private strSelectMainDataSql As String = _
        "SELECT" & vbCrLf & _
        "  t3.CINmb" & vbCrLf & _
        " ,t3.Select" & vbCrLf & _
        " ,t3.EndUsrBusyoNM" & vbCrLf & _
        " ,t3.UsrID" & vbCrLf & _
        " ,t3.EndUsrNM" & vbCrLf & _
        " ,t3.TargetKiki" & vbCrLf & _
        " ,t3.UsrBusyoNM" & vbCrLf & _
        " ,t3.LastInfoDT" & vbCrLf & _
        " ,MAX(t3.PerCINmbCnt) OVER(PARTITION BY t3.UsrID) AS DataCnt" & vbCrLf & _
        " ,t3.SCKikiType" & vbCrLf & _
        " ,t3.LimitDateFrom" & vbCrLf & _
        " ,t3.LimitDateTo" & vbCrLf & _
        " ,t3.ShareExists" & vbCrLf & _
        " ,t3.KindCD AS KikiKindCD" & vbCrLf & _
        " ,t3.Num AS KikiNum" & vbCrLf & _
        " ,t3.EndUsrCompany" & vbCrLf & _
        " ,t3.EndUsrNMkana" & vbCrLf & _
        " ,t3.EndUsrTel" & vbCrLf & _
        " ,t3.EndUsrMailAdd" & vbCrLf & _
        " ,t3.TypeKbn" & vbCrLf & _
        "FROM (" & vbCrLf & _
        "   SELECT" & vbCrLf & _
        "    ct.CINmb" & vbCrLf & _
        "   ,'False' AS Select" & vbCrLf & _
        "   ,em.EndUsrBusyoNM" & vbCrLf & _
        "   ,t.UsrID" & vbCrLf & _
        "   ,em.EndUsrNM" & vbCrLf & _
        "   ,km.KindNM || ct.Num AS TargetKiki" & vbCrLf & _
        "   ,t.UsrBusyoNM" & vbCrLf & _
        "   ,CASE WHEN COALESCE(t.LastInfoDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(t.LastInfoDT, 'YYYYMMDD'),'YYYY/MM/DD') END AS LastInfoDT" & vbCrLf & _
        "   ,ROW_NUMBER() OVER(PARTITION BY t.UsrID) AS PerCINmbCnt" & vbCrLf & _
        "   ,em.Sort AS EndUsrSort" & vbCrLf & _
        "   ,km.Sort AS KindSort" & vbCrLf & _
        "   ,ct.KindCD" & vbCrLf & vbCrLf & _
        "   ,ct.Num" & vbCrLf & vbCrLf & _
        "   ,em.EndUsrCompany" & vbCrLf & vbCrLf & _
        "   ,em.EndUsrNMkana" & vbCrLf & vbCrLf & _
        "   ,em.EndUsrTel" & vbCrLf & vbCrLf & _
        "   ,em.EndUsrMailAdd" & vbCrLf & vbCrLf

    '【サポセン】[SELECT]メイン表示用データ取得：SELECT句
    Private strSelectMainDataSqlForSap As String = _
        "   ,sm.SCKikiType" & vbCrLf & _
        "   ,CASE WHEN COALESCE(t.RentalStDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(t.RentalStDT, 'YYYYMMDD'),'YYYY/MM/DD') END AS LimitDateFrom" & vbCrLf & _
        "   ,CASE WHEN COALESCE(t.RentalEdDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(t.RentalEdDT, 'YYYYMMDD'),'YYYY/MM/DD') END AS LimitDateTo" & vbCrLf & _
        "   ,CASE WHEN (SELECT COUNT(1) FROM SHARE_TB st WHERE t.CINmb = st.CINmb) > 0 THEN '" & SHARE_ARI & "' ELSE '" & SHARE_NASHI & "' END AS ShareExists" & vbCrLf & _
        "   ,t.TypeKbn" & vbCrLf

    '【部所有機器】[SELECT]メイン表示用データ取得：SELECT句
    Private strSelectMainDataSqlForBuy As String = _
        "   ,'' AS SCKikiType" & vbCrLf & _
        "   ,CASE WHEN COALESCE(t.ConnectDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(t.ConnectDT, 'YYYYMMDD'),'YYYY/MM/DD') END AS LimitDateFrom" & vbCrLf & _
        "   ,CASE WHEN COALESCE(t.ExpirationDT,'') = '' THEN '' ELSE TO_CHAR(TO_DATE(t.ExpirationDT, 'YYYYMMDD'),'YYYY/MM/DD') END AS LimitDateTo" & vbCrLf & _
        "   ,'' AS ShareExists" & vbCrLf & _
        "   ,'' AS TypeKbn" & vbCrLf


    '【共通】[SELECT]メイン表示用データ取得：FROM句
    Private strFromMainDataSql As String = _
        "   FROM CI_INFO_TB ct" & vbCrLf & _
        "   LEFT JOIN KIND_MTB km ON ct.KindCD = km.KindCD" & vbCrLf

    '【サポセン】[SELECT]メイン表示用データ取得：FROM句
    Private strFromMainDataSqlForSap As String = _
        "   JOIN CI_SAP_TB t ON ct.CINmb = t.CINmb" & vbCrLf & _
        "   LEFT JOIN ENDUSR_MTB em ON t.UsrID = em.EndUsrID" & vbCrLf & _
        "   LEFT JOIN SAP_KIKI_TYPE_MTB sm ON t.TypeKbn = sm.SCKikiCD" & vbCrLf

    '【部所有機器】[SELECT]メイン表示用データ取得：FROM句
    Private strFromMainDataSqlForBuy As String = _
        "   JOIN CI_BUY_TB t ON ct.CINmb = t.CINmb" & vbCrLf & _
        "   LEFT JOIN ENDUSR_MTB em ON t.UsrID = em.EndUsrID" & vbCrLf


    '【共通】[SELECT]メイン表示用データ取得：ORDER BY句
    Private strOrderByMainDataSql As String = _
        ") t3" & vbCrLf & _
        "ORDER BY " & vbCrLf & _
        "t3.EndUsrBusyoNM, t3.EndUsrSort, t3.KindSort, t3.Num, t3.LimitDateFrom, t3.LimitDateTo" & vbCrLf


    '【共通】未お知らせのデータの検索条件　※月末日計算方法：（対象月 ＋ 1ヶ月） － 1日
    Private strNotInfoCond As String = _
        "    TO_TIMESTAMP(COALESCE(t.LastInfoDT,''),'YYYYMMDD')" & vbCrLf & _
        "       NOT BETWEEN TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf & _
        "           AND TO_TIMESTAMP(TO_CHAR(Now() + '1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf


    '【共通】[INSERT]インシデント共通情報新規登録
    Private strInsertIncInfoSql As String = _
        "INSERT INTO INCIDENT_INFO_TB (" & vbCrLf & _
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
        ",KigenCondUsrID " & vbCrLf & _
        ",RegDT " & vbCrLf & _
        ",RegGrpCD " & vbCrLf & _
        ",RegID " & vbCrLf & _
        ",UpdateDT " & vbCrLf & _
        ",UpGrpCD " & vbCrLf & _
        ",UpdateID " & vbCrLf & _
        ") VALUES (" & vbCrLf & _
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
        ",:Kengen " & vbCrLf & _
        ",:RentalKiki " & vbCrLf & _
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
        ",:TitleAimai " & vbCrLf & _
        ",:UkeNaiyoAimai " & vbCrLf & _
        ",:BikoAimai " & vbCrLf & _
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
        ",:KigenCondCIKbnCD " & vbCrLf & _
        ",:KigenCondTypeKbn " & vbCrLf & _
        ",:KigenCondKigen " & vbCrLf & _
        ",:KigenCondUsrID " & vbCrLf & _
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
                                                   "    ,:TantoGrpNM" & vbCrLf & _
                                                   "    ,:IncTantoID" & vbCrLf & _
                                                   "    ,:IncTantoNM" & vbCrLf & _
                                                   "    ,:RegDT " & vbCrLf & _
                                                   "    ,:RegGrpCD " & vbCrLf & _
                                                   "    ,:RegID " & vbCrLf & _
                                                   "    ,:UpdateDT " & vbCrLf & _
                                                   "    ,:UpGrpCD " & vbCrLf & _
                                                   "    ,:UpdateID " & vbCrLf & _
                                                   ") "


    '【共通】[INSERT]インシデント対応関係新規登録
    Private strInsertIncKankeiSql As String = _
        "INSERT INTO INCIDENT_KANKEI_TB ( " & vbCrLf & _
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
        ") VALUES (" & vbCrLf & _
        " :IncNmb " & vbCrLf & _
        ",:RelationKbn " & vbCrLf & _
        ",:RelationID " & vbCrLf & _
        ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_KANKEI_TB WHERE IncNmb=:IncNmb) " & vbCrLf & _
        ",:RegDT " & vbCrLf & _
        ",:RegGrpCD " & vbCrLf & _
        ",:RegID " & vbCrLf & _
        ",:UpdateDT " & vbCrLf & _
        ",:UpGrpCD " & vbCrLf & _
        ",:UpdateID " & vbCrLf & _
        ") "

    '【共通】[INSERT]インシデント機器情報新規登録
    '[mod] 2016/11/09 e.okamura 設置番組/部屋文字数変更対応 START
    ''[MOD]2013/03/12 t.fukuo 機器情報登録不具合対応 START
    ''Private strInsertIncKikiSql As String = _
    ''    "INSERT INTO INCIDENT_KIKI_TB ( " & vbCrLf & _
    ''    " IncNmb " & vbCrLf & _
    ''    ",KindCD " & vbCrLf & _
    ''    ",Num " & vbCrLf & _
    ''    ",KikiInf " & vbCrLf & _
    ''    ",EntryNmb " & vbCrLf & _
    ''    ",RegDT " & vbCrLf & _
    ''    ",RegGrpCD " & vbCrLf & _
    ''    ",RegID " & vbCrLf & _
    ''    ",UpdateDT " & vbCrLf & _
    ''    ",UpGrpCD " & vbCrLf & _
    ''    ",UpdateID " & vbCrLf & _
    ''    ") VALUES (" & vbCrLf & _
    ''    " :IncNmb " & vbCrLf & _
    ''    ",:KindCD " & vbCrLf & _
    ''    ",:Num " & vbCrLf & _
    ''    ",CASE :CIKbnCD" & vbCrLf & _
    ''    " WHEN '" & CI_TYPE_SUPORT & "' THEN" & vbCrLf & _
    ''    " (SELECT" & vbCrLf & _
    ''    "     COALESCE(cst.SetBuil,'') " & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetBusyoNM,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetRoom,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT km.KikiStateNM FROM KIKISTATE_MTB km WHERE cst.KikiUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_KIKI_RIYOKEITAI & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || CASE COALESCE(cst.KikiState,'') WHEN '' THEN '" & KIKISTATE_NO_INPUT & "' ELSE '" & KIKISTATE_INPUT & "' END" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.ImageNmb,'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT '" & IPUSECD_STATIC_WORD & "' || SUBSTR(km.KikiStateNM,1,1) FROM KIKISTATE_MTB km WHERE cst.IPUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || CASE (SELECT COUNT(1) FROM OPTSOFT_TB ot WHERE cst.CINmb = ot.CINmb) WHEN 0 THEN '" & OPTSOFT_NO_INPUT & "' ELSE '" & OPTSOFT_NO_INPUT & "' END" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(ct.CINM,'')" & vbCrLf & _
    ''    "  FROM CI_INFO_TB ct" & vbCrLf & _
    ''    "  JOIN CI_SAP_TB cst ON ct.CINmb = cst.CINmb" & vbCrLf & _
    ''    "  WHERE ct.CINmb = :CINmb" & vbCrLf & _
    ''    " ) " & vbCrLf & _
    ''    " WHEN '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
    ''    " (SELECT" & vbCrLf & _
    ''    "     COALESCE(cst.SetBuil,'') " & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(cst.SetFloor,'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetBusyoNM,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(SUBSTR(cst.SetRoom,1," & KIKIINF_INITIAL_LENGTH & "),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE((SELECT '" & IPUSECD_STATIC_WORD & "' || SUBSTR(km.KikiStateNM,1,1) FROM KIKISTATE_MTB km WHERE cst.IPUseCD = km.KikiStateCD AND km.KikiStateKbn = '" & KIKISTATEKBN_IP_WARIATE & "' AND km.JtiFlg = '0'),'')" & vbCrLf & _
    ''    "  || '" & KIKIINF_SPLIT_SIMBOL & "' || COALESCE(ct.CINM,'')" & vbCrLf & _
    ''    "  FROM CI_INFO_TB ct" & vbCrLf & _
    ''    "  JOIN CI_BUY_TB cst ON ct.CINmb = cst.CINmb" & vbCrLf & _
    ''    "  WHERE ct.CINmb = :CINmb" & vbCrLf & _
    ''    " ) " & vbCrLf & _
    ''    " ELSE '' END" & vbCrLf & _
    ''    ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_KIKI_TB WHERE IncNmb=:IncNmb) " & vbCrLf & _
    ''    ",:RegDT " & vbCrLf & _
    ''    ",:RegGrpCD " & vbCrLf & _
    ''    ",:RegID " & vbCrLf & _
    ''    ",:UpdateDT " & vbCrLf & _
    ''    ",:UpGrpCD " & vbCrLf & _
    ''    ",:UpdateID " & vbCrLf & _
    ''    ") "
    'Private strInsertIncKikiSql As String = _
    '    "INSERT INTO INCIDENT_KIKI_TB ( " & vbCrLf & _
    '    " IncNmb " & vbCrLf & _
    '    ",KindCD " & vbCrLf & _
    '    ",Num " & vbCrLf & _
    '    ",KikiInf " & vbCrLf & _
    '    ",EntryNmb " & vbCrLf & _
    '    ",RegDT " & vbCrLf & _
    '    ",RegGrpCD " & vbCrLf & _
    '    ",RegID " & vbCrLf & _
    '    ",UpdateDT " & vbCrLf & _
    '    ",UpGrpCD " & vbCrLf & _
    '    ",UpdateID " & vbCrLf & _
    '    ") VALUES (" & vbCrLf & _
    '    " :IncNmb " & vbCrLf & _
    '    ",:KindCD " & vbCrLf & _
    '    ",:Num " & vbCrLf & _
    '    ",CASE :CIKbnCD" & vbCrLf & _
    '    " WHEN '" & CI_TYPE_SUPORT & "' THEN" & vbCrLf & _
    '    " (SELECT" & vbCrLf & _
    '    "   COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(SubString(t1.SetBusyoNM ,1,10),'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(SubString(t1.SetRoom,1,10),'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(m1.cistatenm,'')  ||'/'||" & vbCrLf & _
    '    "   CASE COALESCE(t1.kikistate,'') WHEN '' Then :kikistate1 ELSE :kikistate2 END ||'/'|| " & vbCrLf & _
    '    "   COALESCE(t1.imageNmb,'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'||" & vbCrLf & _
    '    "   CASE (Select Count(*) From optsoft_tb t4 Where t1.cinmb=t4.cinmb) WHEN 0 THEN 'OP無' ELSE 'OP有' END ||'/'|| " & vbCrLf & _
    '    "   COALESCE(t0.CINM,'') " & vbCrLf & _
    '    "  FROM ci_info_tb t0 " & vbCrLf & _
    '    "  INNER JOIN ci_sap_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
    '    "  LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
    '    "  LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
    '    "  WHERE t0.CINmb=:CINmb" & vbCrLf & _
    '    " ) " & vbCrLf & _
    '    " WHEN '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
    '    " (SELECT" & vbCrLf & _
    '    "   COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(SUBSTRING(t1.SetBusyoNM ,1,10),'') ||'/'||" & vbCrLf & _
    '    "   COALESCE(SUBSTRING(t1.SetRoom,1,10),'')  ||'/'||" & vbCrLf & _
    '    "   COALESCE(m1.cistatenm,'') ||'/'||" & vbCrLf & _
    '    "   COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'|| " & vbCrLf & _
    '    "   COALESCE(t0.CINM,'') " & vbCrLf & _
    '    "  FROM ci_info_tb t0 " & vbCrLf & _
    '    "  INNER JOIN ci_buy_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
    '    "  LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
    '    "  LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
    '    "  WHERE t0.CINmb=:CINmb" & vbCrLf & _
    '    " ) " & vbCrLf & _
    '    " ELSE '' END" & vbCrLf & _
    '    ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_KIKI_TB WHERE IncNmb=:IncNmb) " & vbCrLf & _
    '    ",:RegDT " & vbCrLf & _
    '    ",:RegGrpCD " & vbCrLf & _
    '    ",:RegID " & vbCrLf & _
    '    ",:UpdateDT " & vbCrLf & _
    '    ",:UpGrpCD " & vbCrLf & _
    '    ",:UpdateID " & vbCrLf & _
    '    ") "
    ''[MOD]2013/03/12 t.fukuo 機器情報登録不具合対応 END
    Private strInsertIncKikiSql As String = _
        "INSERT INTO INCIDENT_KIKI_TB ( " & vbCrLf & _
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
        ") VALUES (" & vbCrLf & _
        " :IncNmb " & vbCrLf & _
        ",:KindCD " & vbCrLf & _
        ",:Num " & vbCrLf & _
        ",CASE :CIKbnCD" & vbCrLf & _
        " WHEN '" & CI_TYPE_SUPORT & "' THEN" & vbCrLf & _
        " (SELECT" & vbCrLf & _
        "   COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
        "   COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
        "   COALESCE(SubString(t1.SetBusyoNM ,1,10),'')  ||'/'||" & vbCrLf & _
        "   COALESCE(SubString(t1.SetRoom,1,20),'')  ||'/'||" & vbCrLf & _
        "   COALESCE(m1.cistatenm,'')  ||'/'||" & vbCrLf & _
        "   CASE COALESCE(t1.kikistate,'') WHEN '' Then :kikistate1 ELSE :kikistate2 END ||'/'|| " & vbCrLf & _
        "   COALESCE(t1.imageNmb,'')  ||'/'||" & vbCrLf & _
        "   COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'||" & vbCrLf & _
        "   CASE (Select Count(*) From optsoft_tb t4 Where t1.cinmb=t4.cinmb) WHEN 0 THEN 'OP無' ELSE 'OP有' END ||'/'|| " & vbCrLf & _
        "   COALESCE(t0.CINM,'') " & vbCrLf & _
        "  FROM ci_info_tb t0 " & vbCrLf & _
        "  INNER JOIN ci_sap_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
        "  LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
        "  LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
        "  WHERE t0.CINmb=:CINmb" & vbCrLf & _
        " ) " & vbCrLf & _
        " WHEN '" & CI_TYPE_KIKI & "' THEN" & vbCrLf & _
        " (SELECT" & vbCrLf & _
        "   COALESCE(t1.SetBuil,'')  ||'/'||" & vbCrLf & _
        "   COALESCE(t1.SetFloor,'')  ||'/'||" & vbCrLf & _
        "   COALESCE(SUBSTRING(t1.SetBusyoNM ,1,10),'') ||'/'||" & vbCrLf & _
        "   COALESCE(SUBSTRING(t1.SetRoom,1,20),'')  ||'/'||" & vbCrLf & _
        "   COALESCE(m1.cistatenm,'') ||'/'||" & vbCrLf & _
        "   COALESCE('IP'||substring(m2.kikistatenm,1,1),'') ||'/'|| " & vbCrLf & _
        "   COALESCE(t0.CINM,'') " & vbCrLf & _
        "  FROM ci_info_tb t0 " & vbCrLf & _
        "  INNER JOIN ci_buy_tb t1 ON t0.cinmb=t1.cinmb " & vbCrLf & _
        "  LEFT JOIN cistate_mtb m1 ON t0.cistatuscd = m1.cistatecd " & vbCrLf & _
        "  LEFT JOIN kikistate_mtb m2 ON t1.IPuseCD = m2.kikistateCD  " & vbCrLf & _
        "  WHERE t0.CINmb=:CINmb" & vbCrLf & _
        " ) " & vbCrLf & _
        " ELSE '' END" & vbCrLf & _
        ",(SELECT COALESCE(MAX(EntryNmb),0)+1 FROM INCIDENT_KIKI_TB WHERE IncNmb=:IncNmb) " & vbCrLf & _
        ",:RegDT " & vbCrLf & _
        ",:RegGrpCD " & vbCrLf & _
        ",:RegID " & vbCrLf & _
        ",:UpdateDT " & vbCrLf & _
        ",:UpGrpCD " & vbCrLf & _
        ",:UpdateID " & vbCrLf & _
        ") "
    '[mod] 2016/11/09 e.okamura 設置番組/部屋文字数変更対応 END

    '【共通】[INSERT]インシデント共通情報ログ新規登録
    Private strInsertIncInfoLSql As String = _
        "INSERT INTO  INCIDENT_INFO_LTB (" & vbCrLf & _
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
        ",Biko2 " & vbCrLf & _
        ",Biko3 " & vbCrLf & _
        ",Biko4 " & vbCrLf & _
        ",Biko5 " & vbCrLf & _
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
        ",KigenCondUsrID " & vbCrLf & _
        ",RegDT " & vbCrLf & _
        ",RegGrpCD " & vbCrLf & _
        ",RegID " & vbCrLf & _
        ",UpdateDT " & vbCrLf & _
        ",UpGrpCD " & vbCrLf & _
        ",UpdateID " & vbCrLf & _
        ")" & vbCrLf & _
        "SELECT" & vbCrLf & _
        " IncNmb " & vbCrLf & _
        ",:LogNo " & vbCrLf & _
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
        ",Biko2 " & vbCrLf & _
        ",Biko3 " & vbCrLf & _
        ",Biko4 " & vbCrLf & _
        ",Biko5 " & vbCrLf & _
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
        ",KigenCondUsrID " & vbCrLf & _
        ",RegDT " & vbCrLf & _
        ",RegGrpCD " & vbCrLf & _
        ",RegID " & vbCrLf & _
        ",UpdateDT " & vbCrLf & _
        ",UpGrpCD " & vbCrLf & _
        ",UpdateID " & vbCrLf & _
        "FROM INCIDENT_INFO_TB " & vbCrLf & _
        "WHERE IncNmb = :IncNmb "

    '【共通】[INSERT]インシデント対応関係ログ新規登録
    Private strInsertIncKankeiLogSql As String = _
        "INSERT INTO INCIDENT_KANKEI_LTB ( " & vbCrLf & _
        " IncNmb " & vbCrLf & _
        ",LogNo " & vbCrLf & _
        ",RelationKbn " & vbCrLf & _
        ",RelationID " & vbCrLf & _
        ",RegDT " & vbCrLf & _
        ",RegGrpCD " & vbCrLf & _
        ",RegID " & vbCrLf & _
        ",UpdateDT " & vbCrLf & _
        ",UpGrpCD " & vbCrLf & _
        ",UpdateID " & vbCrLf & _
        ")" & vbCrLf & _
        "SELECT" & vbCrLf & _
        " IncNmb " & vbCrLf & _
        ",:LogNo " & vbCrLf & _
        ",RelationKbn " & vbCrLf & _
        ",RelationID " & vbCrLf & _
        ",RegDT " & vbCrLf & _
        ",RegGrpCD " & vbCrLf & _
        ",RegID " & vbCrLf & _
        ",UpdateDT " & vbCrLf & _
        ",UpGrpCD " & vbCrLf & _
        ",UpdateID " & vbCrLf & _
        "FROM INCIDENT_KANKEI_TB " & vbCrLf & _
        "WHERE IncNmb = :IncNmb "

    '【共通】[INSERT]インシデント対応機器ログ新規登録
    Private strInsertIncKikiLogSql As String = _
        "INSERT INTO INCIDENT_KIKI_LTB ( " & vbCrLf & _
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
        ")" & vbCrLf & _
        "SELECT" & vbCrLf & _
        " IncNmb " & vbCrLf & _
        ",:LogNo " & vbCrLf & _
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
        "FROM INCIDENT_KIKI_TB " & vbCrLf & _
        "WHERE IncNmb = :IncNmb " & vbCrLf & _
        "AND KindCD = :KindCD " & vbCrLf & _
        "AND Num = :Num "

    ''' <summary>
    ''' 【サポセン】メイン表示データ件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ（CI共通情報／サポセン機器情報）件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMainDataCntSqlForSap(ByRef Adapter As NpgsqlDataAdapter, _
              ByVal Cn As NpgsqlConnection, _
              ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectMainDataCntSql

            'FROM句セット
            strSQL &= strFromMainDataSql & strFromMainDataSqlForSap

            'WHERE句セット
            If SetWhereCmdForSap(Adapter, dataHBKB0801) = False Then
                Return False
            End If
            strSQL &= dataHBKB0801.PropStrWhereCmd


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値を設定
            If SetBind(Adapter, dataHBKB0801) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【部所有機器】メイン表示データ件数取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ（CI共通情報／部所有機器情報）件数取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMainDataCntSqlForBuy(ByRef Adapter As NpgsqlDataAdapter, _
              ByVal Cn As NpgsqlConnection, _
              ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectMainDataCntSql

            'FROM句セット
            strSQL &= strFromMainDataSql & strFromMainDataSqlForBuy

            'WHERE句セット
            If SetWhereCmdForBuy(Adapter, dataHBKB0801) = False Then
                Return False
            End If
            strSQL &= dataHBKB0801.PropStrWhereCmd

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型と値を設定
            If SetBind(Adapter, dataHBKB0801) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン】メイン表示データ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ（CI共通情報／サポセン機器情報）取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMainDataSqlForSap(ByRef Adapter As NpgsqlDataAdapter, _
           ByVal Cn As NpgsqlConnection, _
           ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectMainDataSql & strSelectMainDataSqlForSap

            'FROM句セット
            strSQL &= strFromMainDataSql & strFromMainDataSqlForSap

            'WHERE句セット
            strSQL &= dataHBKB0801.PropStrWhereCmd

            'ORDER BY句セット
            strSQL &= strOrderByMainDataSql


            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値を設定
            If SetBind(Adapter, dataHBKB0801) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【部所有機器】メイン表示データ取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ（CI共通情報／部所有機器情報）取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectMainDataSqlForBuy(ByRef Adapter As NpgsqlDataAdapter, _
           ByVal Cn As NpgsqlConnection, _
           ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""

        Try
            'SQL文(SELECT)
            strSQL = strSelectMainDataSql & strSelectMainDataSqlForBuy
            strSQL &= strFromMainDataSql & strFromMainDataSqlForBuy

            'WHERE句セット
            strSQL &= dataHBKB0801.PropStrWhereCmd

            'ORDER BY句セット
            strSQL &= strOrderByMainDataSql

            'データアダプタに、SQLのSELECT文を設定
            Adapter.SelectCommand = New NpgsqlCommand(strSQL, Cn)

            'バインド変数に型と値を設定
            If SetBind(Adapter, dataHBKB0801) = False Then
                Return False
            End If

            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【サポセン】メイン表示データ取得用WHERE句の作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ取得用のSQLのWHERE句を作成し、データクラスにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetWhereCmdForSap(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim sbWhere As New StringBuilder

        Try
            With dataHBKB0801

                'CI種別
                sbWhere.Append("WHERE ct.CIKbnCD = :CIKbnCD" & vbCrLf)

                '機器利用形態：一時利用（貸出）
                sbWhere.Append("AND t.KikiUseCD = '" & KIKI_RIYOKEITAI_ICHIJI_RIYO & "'" & vbCrLf)

                'タイプ
                If .PropCmbType.SelectedValue <> "" Then
                    sbWhere.Append("AND t.TypeKbn = :TypeKbn" & vbCrLf)
                End If

                '選択されたラジオボタンにより条件設定
                If .PropRdoLimit.Checked Then

                    '期限条件選択時
                    sbWhere.Append("AND COALESCE(t.RentalEdDT,'') <> ''" & vbCrLf)
                    Select Case .PropCmbLimit.SelectedValue

                        Case LIMIT_THISMONTH_ONLY           '今月期限今月未お知らせ分

                            '実行月の１日～末日に期限切れとなり、実行前月の１日～末日に未お知らせのデータを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                       AND TO_TIMESTAMP(TO_CHAR(Now() + '1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_THISMONTH_ALL            '今月期限全部

                            '実行月の１日～末日に期限切れとなるデータを検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                          AND TO_TIMESTAMP(TO_CHAR(Now() + '1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                        Case LIMIT_LASTMONTH_ONLY           '前月期限今月未お知らせ分

                            '実行月前月の１日～末日に期限切れとなった、未お知らせのデータを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                       AND TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_LASTMONTH_ALL            '前月期限全部

                            '実行月前月の１日～末日に期限切れとなったデータを検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                          AND TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                        Case LIMIT_BEF_LASTMONTH_ONLY       '前々月以前今月未お知らせ分

                            '実行月前々月の末日以前に期限切れとなった、未お知らせのデータを検索データを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') <= TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_BEF_LASTMONTH_ALL        '前々月以前期限全部

                            '実行月前々月の末日以前に期限切れとなったデータを全て検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.RentalEdDT,'YYYYMMDD') <= TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                    End Select


                ElseIf .PropRdoUsrID.Checked Then

                    'ユーザーID条件選択時
                    If Trim(.PropTxtUsrID.Text) <> "" Then
                        sbWhere.Append("AND t.UsrID = :UsrID" & vbCrLf)
                    End If


                End If

                'データクラスに作成したWHERE句をセット
                .PropStrWhereCmd = sbWhere.ToString()

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【部所有機器】メイン表示データ取得用WHERE句の作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ取得用のSQLのWHERE句を作成し、データクラスにセットする
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetWhereCmdForBuy(ByVal Adapter As NpgsqlDataAdapter, _
                                      ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim sbWhere As New StringBuilder

        Try
            With dataHBKB0801

                'CI種別
                sbWhere.Append("WHERE ct.CIKbnCD = :CIKbnCD" & vbCrLf)

                'CIステータス：利用中
                sbWhere.Append("AND ct.CIStatusCD = '" & CI_STATUS_KIKI_RIYOUCHU & "'" & vbCrLf)

                '選択されたラジオボタンにより条件設定
                If .PropRdoLimit.Checked Then

                    '期限条件選択時
                    sbWhere.Append("AND COALESCE(t.ExpirationDT,'') <> ''" & vbCrLf)
                    Select Case .PropCmbLimit.SelectedValue

                        Case LIMIT_THISMONTH_ONLY           '今月期限今月未お知らせ分

                            '実行月の１日～末日に期限切れとなり、実行月の１日～末日に未お知らせのデータを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                       AND TO_TIMESTAMP(TO_CHAR(Now() + '1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_THISMONTH_ALL            '今月期限全部

                            '実行月の１日～末日に期限切れとなるデータを検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                          AND TO_TIMESTAMP(TO_CHAR(Now() + '1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                        Case LIMIT_LASTMONTH_ONLY           '前月期限今月未お知らせ分

                            '実行月前月の１日～末日に期限切れとなった、未お知らせのデータを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                       AND TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_LASTMONTH_ALL            '前月期限全部

                            '実行月前月の１日～末日に期限切れとなったデータを検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') BETWEEN TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 00:00'),'YYYY/MM/DD HH24:MI')" & vbCrLf)
                            sbWhere.Append("                                          AND TO_TIMESTAMP(TO_CHAR(Now(),'YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                        Case LIMIT_BEF_LASTMONTH_ONLY       '前々月以前今月未お知らせ分

                            '実行月前々月の末日以前に期限切れとなった、未お知らせのデータを検索データを検索
                            sbWhere.Append("AND (" & vbCrLf)
                            sbWhere.Append(" TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') <= TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)
                            sbWhere.Append(" AND" & strNotInfoCond)
                            sbWhere.Append(" )" & vbCrLf)


                        Case LIMIT_BEF_LASTMONTH_ALL        '前々月以前期限全部

                            '実行月前々月の末日以前に期限切れとなったデータを全て検索
                            sbWhere.Append("AND TO_TIMESTAMP(t.ExpirationDT,'YYYYMMDD') <= TO_TIMESTAMP(TO_CHAR(Now() + '-1 months','YYYY/MM' || '/01 23:59'),'YYYY/MM/DD HH24:MI') + '-1 days'" & vbCrLf)


                    End Select


                ElseIf .PropRdoUsrID.Checked Then

                    'ユーザーID条件選択時
                    If Trim(.PropTxtUsrID.Text) <> "" Then
                        sbWhere.Append("AND t.UsrID = :UsrID" & vbCrLf)
                    End If

                End If


                'データクラスに作成したWHERE句をセット
                .PropStrWhereCmd = sbWhere.ToString()

            End With


            '終了ログ出力
            CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "END", Nothing, Nothing)

            '正常終了
            Return True

        Catch ex As Exception
            'ログ出力
            CommonLogic.WriteLog(Common.LogLevel.ERROR_Lv, ex.Message, ex, Adapter.SelectCommand)
            '例外処理
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】メイン表示データ取得用SQLのバインド変数設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN]NpgsqlDataAdapterクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>メイン表示データ取得用のSQLのバインド変数の型と値を設定する
    ''' <para>作成情報：2012/07/19 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetBind(ByVal Adapter As NpgsqlDataAdapter, _
                            ByRef dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        Try
            'バインド変数の型と値を設定
            With Adapter.SelectCommand

                'CI種別
                .Parameters.Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))
                .Parameters("CIKbnCD").Value = dataHBKB0801.PropCmbCIKbn.SelectedValue

                'タイプ
                If dataHBKB0801.PropCmbType.SelectedValue <> "" Then
                    .Parameters.Add(New NpgsqlParameter("TypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))
                    .Parameters("TypeKbn").Value = dataHBKB0801.PropCmbType.SelectedValue
                End If

                '選択されたラジオボタンにより条件設定
                If dataHBKB0801.PropRdoUsrID.Checked Then

                    'ユーザーID
                    If Trim(dataHBKB0801.PropTxtUsrID.Text) <> "" Then
                        .Parameters.Add(New NpgsqlParameter("UsrID", NpgsqlTypes.NpgsqlDbType.Varchar))
                        .Parameters("UsrID").Value = commonLogicHBK.ChangeStringForSearch(dataHBKB0801.PropTxtUsrID.Text)
                    End If

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
            puErrMsg = ex.Message
            Return False
        End Try

    End Function

    ''' <summary>
    ''' 【共通】新規インシデント番号、サーバー日付取得用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Adapter">[IN/OUT]NpgSqlDataAdapterクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>新規インシデント番号、サーバー日付取得用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/03 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetSelectNewIncNmbAndSysDateSql(ByRef Adapter As NpgsqlDataAdapter, _
                                                    ByVal Cn As NpgsqlConnection, _
                                                    ByVal dataHBKB0801 As DataHBKB0801) As Boolean

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
    ''' 【共通】インシデント共通情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント共通情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoSql(ByRef Cmd As NpgsqlCommand, _
                                       ByVal Cn As NpgsqlConnection, _
                                       ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncInfoSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
                .Add(New NpgsqlParameter("ProcessKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                   'プロセス区分
                .Add(New NpgsqlParameter("UkeKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '受付手段CD
                .Add(New NpgsqlParameter("IncKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     'インシデント種別CD
                .Add(New NpgsqlParameter("ProcessStateCD", NpgsqlTypes.NpgsqlDbType.Varchar))               'プロセスステータスCD
                .Add(New NpgsqlParameter("HasseiDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '発生日時
                .Add(New NpgsqlParameter("KaitoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                    '回答日時
                .Add(New NpgsqlParameter("KanryoDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '完了日時
                .Add(New NpgsqlParameter("Priority", NpgsqlTypes.NpgsqlDbType.Varchar))                     '重要度
                .Add(New NpgsqlParameter("Errlevel", NpgsqlTypes.NpgsqlDbType.Varchar))                     '障害レベル
                .Add(New NpgsqlParameter("Title", NpgsqlTypes.NpgsqlDbType.Varchar))                        'タイトル
                .Add(New NpgsqlParameter("UkeNaiyo", NpgsqlTypes.NpgsqlDbType.Varchar))                     '受付内容
                .Add(New NpgsqlParameter("TaioKekka", NpgsqlTypes.NpgsqlDbType.Varchar))                    '対応結果
                .Add(New NpgsqlParameter("SystemNmb", NpgsqlTypes.NpgsqlDbType.Integer))                    '対象システム番号
                .Add(New NpgsqlParameter("OutSideToolNmb", NpgsqlTypes.NpgsqlDbType.Varchar))               '外部ツール番号
                .Add(New NpgsqlParameter("EventID", NpgsqlTypes.NpgsqlDbType.Varchar))                      'イベントID
                .Add(New NpgsqlParameter("Source", NpgsqlTypes.NpgsqlDbType.Varchar))                       'ソース
                .Add(New NpgsqlParameter("OPCEventID", NpgsqlTypes.NpgsqlDbType.Varchar))                   'OPCイベントID
                .Add(New NpgsqlParameter("EventClass", NpgsqlTypes.NpgsqlDbType.Varchar))                   'イベントクラス
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '担当グループCD
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))                   'インシデント担当者ID
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'インシデント担当者氏名
                .Add(New NpgsqlParameter("DomainCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     'ドメインCD
                .Add(New NpgsqlParameter("PartnerCompany", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手会社名
                .Add(New NpgsqlParameter("PartnerID", NpgsqlTypes.NpgsqlDbType.Varchar))                    '相手ID
                .Add(New NpgsqlParameter("PartnerNM", NpgsqlTypes.NpgsqlDbType.Varchar))                    '相手氏名
                .Add(New NpgsqlParameter("PartnerKana", NpgsqlTypes.NpgsqlDbType.Varchar))                  '相手シメイ
                .Add(New NpgsqlParameter("PartnerKyokuNM", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手局
                .Add(New NpgsqlParameter("UsrBusyoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   '相手部署
                .Add(New NpgsqlParameter("PartnerTel", NpgsqlTypes.NpgsqlDbType.Varchar))                   '相手電話番号
                .Add(New NpgsqlParameter("PartnerMailAdd", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手メールアドレス
                .Add(New NpgsqlParameter("PartnerContact", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手連絡先
                .Add(New NpgsqlParameter("PartnerBase", NpgsqlTypes.NpgsqlDbType.Varchar))                  '相手拠点
                .Add(New NpgsqlParameter("PartnerRoom", NpgsqlTypes.NpgsqlDbType.Varchar))                  '相手番組/部屋
                .Add(New NpgsqlParameter("ShijisyoFlg", NpgsqlTypes.NpgsqlDbType.Varchar))                  '指示書フラグ
                .Add(New NpgsqlParameter("Kengen", NpgsqlTypes.NpgsqlDbType.Varchar))                       '権限
                .Add(New NpgsqlParameter("RentalKiki", NpgsqlTypes.NpgsqlDbType.Varchar))                   '借用物
                .Add(New NpgsqlParameter("BIko1", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト１
                .Add(New NpgsqlParameter("Biko2", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト２
                .Add(New NpgsqlParameter("Biko3", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト３
                .Add(New NpgsqlParameter("Biko4", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト４
                .Add(New NpgsqlParameter("Biko5", NpgsqlTypes.NpgsqlDbType.Varchar))                        'フリーテキスト５
                .Add(New NpgsqlParameter("FreeFlg1", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ１
                .Add(New NpgsqlParameter("FreeFlg2", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ２
                .Add(New NpgsqlParameter("FreeFlg3", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ３
                .Add(New NpgsqlParameter("FreeFlg4", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ４
                .Add(New NpgsqlParameter("FreeFlg5", NpgsqlTypes.NpgsqlDbType.Varchar))                     'フリーフラグ５
                .Add(New NpgsqlParameter("TitleAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                   'タイトル（あいまい）
                .Add(New NpgsqlParameter("UkeNaiyoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                '受付内容（あいまい）
                .Add(New NpgsqlParameter("BikoAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                    'フリーテキスト（あいまい）
                .Add(New NpgsqlParameter("TaioKekkaAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               '対応結果(あいまい)
                .Add(New NpgsqlParameter("EventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                 'イベントID(あいまい)
                .Add(New NpgsqlParameter("SourceAimai", NpgsqlTypes.NpgsqlDbType.Varchar))                  'ソース(あいまい)
                .Add(New NpgsqlParameter("OPCEventIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))              'OPCイベントID(あいまい)
                .Add(New NpgsqlParameter("EventClassAimai", NpgsqlTypes.NpgsqlDbType.Varchar))              'イベントクラス(あいまい)
                .Add(New NpgsqlParameter("IncTantIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'インシデント担当者ID(あいまい)
                .Add(New NpgsqlParameter("IncTantNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               'インシデント担当者氏名(あいまい)
                .Add(New NpgsqlParameter("PartnerIDAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手ID(あいまい)
                .Add(New NpgsqlParameter("PartnerNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))               '相手氏名(あいまい)
                .Add(New NpgsqlParameter("UsrBusyoNMAimai", NpgsqlTypes.NpgsqlDbType.Varchar))              '相手部署(あいまい)
                .Add(New NpgsqlParameter("KigenCondCIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))             '期限切れ条件CI種別
                .Add(New NpgsqlParameter("KigenCondTypeKbn", NpgsqlTypes.NpgsqlDbType.Varchar))             '期限切れ条件タイプ
                .Add(New NpgsqlParameter("KigenCondKigen", NpgsqlTypes.NpgsqlDbType.Varchar))               '期限切れ条件期限
                .Add(New NpgsqlParameter("KigenCondUsrID", NpgsqlTypes.NpgsqlDbType.Varchar))               '期限切れ条件ユーザーID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With

            'バインド変数に値をセット
            With Cmd
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
                .Parameters("ProcessKbn").Value = PROCESS_TYPE_INCIDENT                                     'プロセス区分：インシデント
                .Parameters("UkeKbnCD").Value = ""                                                          '受付手段CD：空白
                .Parameters("IncKbnCD").Value = ""                                                          'インシデント種別CD：空白
                .Parameters("ProcessStateCD").Value = PROCESS_STATUS_INCIDENT_KEIZOKU                       'プロセスステータスCD：継続
                .Parameters("HasseiDT").Value = dataHBKB0801.PropDtmSysDate                                 '発生日時：サーバー日時
                .Parameters("KaitoDT").Value = DBNull.Value                                                 '回答日時：NULL
                .Parameters("KanryoDT").Value = DBNull.Value                                                '完了日時：NULL
                .Parameters("Priority").Value = ""                                                          '重要度：空白
                .Parameters("Errlevel").Value = ""                                                          '障害レベル：空白
                'タイトル
                If dataHBKB0801.PropBlnKigenChecked_Search Then
                    '期限を選択している場合、
                    '期限切れ検索一覧で選択しているコンボボックスの表示名
                    .Parameters("Title").Value = dataHBKB0801.PropStrKigenText_Search
                Else
                    'ユーザーIDを選択している場合
                    '期限切れお知らせ
                    .Parameters("Title").Value = TITLE_USERID
                End If
                .Parameters("UkeNaiyo").Value = ""                                                          '受付内容：空白
                .Parameters("TaioKekka").Value = ""                                                         '対応結果：空白
                .Parameters("SystemNmb").Value = 0                                                          '対象システム番号：0（空白）
                .Parameters("OutSideToolNmb").Value = ""                                                    '外部ツール番号：空白
                .Parameters("EventID").Value = ""                                                           'イベントID：空白
                .Parameters("Source").Value = ""                                                            'ソース：空白
                .Parameters("OPCEventID").Value = ""                                                        'OPCイベントID：空白
                .Parameters("EventClass").Value = ""                                                        'イベントクラス：空白
                .Parameters("TantoGrpCD").Value = PropWorkGroupCD                                           '担当グループCD：作業中グループCD
                .Parameters("IncTantoID").Value = PropUserId                                                'インシデント担当者ID：ログインユーザーID
                .Parameters("IncTantoNM").Value = PropUserName                                              'インシデント担当者氏名：ログインユーザー名
                .Parameters("DomainCD").Value = ""                                                          'ドメインCD：空白
                .Parameters("PartnerCompany").Value = dataHBKB0801.PropRowReg.Item("EndUsrCompany")         '相手会社名：所属会社
                '相手ID：ユーザーID
                If IsDBNull(dataHBKB0801.PropRowReg.Item("UsrID")) Then
                    .Parameters("PartnerID").Value = ""
                Else
                    .Parameters("PartnerID").Value = dataHBKB0801.PropRowReg.Item("UsrID")
                End If
                '相手氏名：ユーザー氏名
                If IsDBNull(dataHBKB0801.PropRowReg.Item("EndUsrNM")) Then
                    .Parameters("PartnerNM").Value = ""
                Else
                    .Parameters("PartnerNM").Value = dataHBKB0801.PropRowReg.Item("EndUsrNM")
                End If
                .Parameters("PartnerKana").Value = dataHBKB0801.PropRowReg.Item("EndUsrNMkana")             '相手シメイ：ユーザー氏名カナ
                .Parameters("PartnerKyokuNM").Value = ""                                                    '相手局：空白
                '相手部署：部署名
                If IsDBNull(dataHBKB0801.PropRowReg.Item("EndUsrBusyoNM")) Then
                    .Parameters("UsrBusyoNM").Value = ""
                Else
                    .Parameters("UsrBusyoNM").Value = dataHBKB0801.PropRowReg.Item("EndUsrBusyoNM")
                End If
                .Parameters("PartnerTel").Value = dataHBKB0801.PropRowReg.Item("EndUsrTel")                 '相手電話番号：電話番号
                .Parameters("PartnerMailAdd").Value = dataHBKB0801.PropRowReg.Item("EndUsrMailAdd")         '相手メールアドレス：メールアドレス
                .Parameters("PartnerContact").Value = ""                                                    '相手連絡先：空白
                .Parameters("PartnerBase").Value = ""                                                       '相手拠点：空白
                .Parameters("PartnerRoom").Value = ""                                                       '相手番組/部屋：空白
                .Parameters("ShijisyoFlg").Value = SHIJISYO_FLG_OFF                                         '指示書フラグ：指示書なし
                .Parameters("Kengen").Value = ""                                                            '権限：空白
                .Parameters("RentalKiki").Value = ""                                                        '借用物：空白
                .Parameters("BIko1").Value = ""                                                             'フリーテキスト１：空白
                .Parameters("Biko2").Value = ""                                                             'フリーテキスト２：空白
                .Parameters("BIko3").Value = ""                                                             'フリーテキスト３：空白
                .Parameters("Biko4").Value = ""                                                             'フリーテキスト４：空白
                .Parameters("Biko5").Value = ""                                                             'フリーテキスト５：空白
                .Parameters("FreeFlg1").Value = FREE_FLG_OFF                                                'フリーフラグ１：OFF
                .Parameters("FreeFlg2").Value = FREE_FLG_OFF                                                'フリーフラグ２：OFF
                .Parameters("FreeFlg3").Value = FREE_FLG_OFF                                                'フリーフラグ３：OFF
                .Parameters("FreeFlg4").Value = FREE_FLG_OFF                                                'フリーフラグ４：OFF
                .Parameters("FreeFlg5").Value = FREE_FLG_OFF                                                'フリーフラグ５：OFF
                .Parameters("TitleAimai").Value = ""                                                        'タイトル（あいまい）：空白
                .Parameters("UkeNaiyoAimai").Value = ""                                                     '受付内容（あいまい）：空白
                .Parameters("BikoAimai").Value = ""                                                         'フリーテキスト（あいまい）：空白                                                                     'フリーテキスト（あいまい）
                .Parameters("TaioKekkaAimai").Value = ""                                                    '対応結果(あいまい)：空白
                .Parameters("EventIDAimai").Value = ""                                                      'イベントID(あいまい)：空白
                .Parameters("SourceAimai").Value = ""                                                       'ソース(あいまい)：空白：空白
                .Parameters("OPCEventIDAimai").Value = ""                                                   'OPCイベントID(あいまい)：空白
                .Parameters("EventClassAimai").Value = ""                                                   'イベントクラス(あいまい)：空白
                .Parameters("IncTantIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.Parameters("IncTantoID").Value)    'インシデント担当者ID(あいまい)
                .Parameters("IncTantNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.Parameters("IncTantoNM").Value)    'インシデント担当者氏名(あいまい)
                .Parameters("PartnerIDAimai").Value = commonLogicHBK.ChangeStringForSearch(.Parameters("PartnerID").Value)     '相手ID(あいまい)
                .Parameters("PartnerNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.Parameters("PartnerNM").Value)     '相手氏名(あいまい)
                .Parameters("UsrBusyoNMAimai").Value = commonLogicHBK.ChangeStringForSearch(.Parameters("UsrBusyoNM").Value)   '相手部署(あいまい)
                .Parameters("KigenCondCIKbnCD").Value = dataHBKB0801.PropStrCIKbnCD_Search                  '期限切れ条件CI種別：CI種別CD
                .Parameters("KigenCondTypeKbn").Value = dataHBKB0801.PropRowReg.Item("TypeKbn")             '期限切れ条件タイプ
                If dataHBKB0801.PropBlnKigenChecked_Search Then
                    '検索時、期限条件選択時
                    .Parameters("KigenCondKigen").Value = dataHBKB0801.PropStrKigenCD_Search                '期限切れ条件期限：検索時の期限CD
                    .Parameters("KigenCondUsrID").Value = KIGENCOND_USERID_OFF                              '期限切れ条件ユーザーID：未選択
                Else
                    '検索時、ユーザーID条件選択時
                    .Parameters("KigenCondKigen").Value = ""                                                '期限切れ条件期限：空白
                    .Parameters("KigenCondUsrID").Value = KIGENCOND_USERID_ON                               '期限切れ条件ユーザーID：選択
                End If
                .Parameters("RegDT").Value = dataHBKB0801.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0801.PropDtmSysDate                                 '最終更新日時
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
    ''' 【共通】インシデント担当履歴情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント担当履歴情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/09/10 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncTantoRirekiSql(ByRef Cmd As NpgsqlCommand, _
                                               ByVal Cn As NpgsqlConnection, _
                                               ByVal dataHBKB0801 As DataHBKB0801) As Boolean

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
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
                .Add(New NpgsqlParameter("TantoGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                   '担当グループCD
                .Add(New NpgsqlParameter("TantoGrpNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   '担当グループ名
                .Add(New NpgsqlParameter("IncTantoID", NpgsqlTypes.NpgsqlDbType.Varchar))                   'インシデント担当者ID
                .Add(New NpgsqlParameter("IncTantoNM", NpgsqlTypes.NpgsqlDbType.Varchar))                   'インシデント担当者氏名
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
                .Parameters("TantoGrpCD").Value = PropWorkGroupCD                                           '担当グループCD
                .Parameters("TantoGrpNM").Value = PropWorkGroupName                                         '担当グループ名
                .Parameters("IncTantoID").Value = PropUserId                                                'インシデント担当者ID
                .Parameters("IncTantoNM").Value = PropUserName                                              'インシデント担当者氏名
                .Parameters("RegDT").Value = dataHBKB0801.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0801.PropDtmSysDate                                 '最終更新日時
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
    ''' 【共通】インシデント対応関係新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント対応関係新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKankeiSql(ByRef Cmd As NpgsqlCommand, _
                                          ByVal Cn As NpgsqlConnection, _
                                          ByVal dataHBKB0801 As DataHBKB0801) As Boolean

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
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
                .Add(New NpgsqlParameter("RelationKbn", NpgsqlTypes.NpgsqlDbType.Varchar))                  '関係区分
                .Add(New NpgsqlParameter("RelationID", NpgsqlTypes.NpgsqlDbType.Varchar))                   '関係ID
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
                .Parameters("RelationKbn").Value = KBN_GROUP                                                '関係区分：グループ
                .Parameters("RelationID").Value = PropWorkGroupCD                                           '関係ID：作業中グループID
                .Parameters("RegDT").Value = dataHBKB0801.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0801.PropDtmSysDate                                 '最終更新日時
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
    ''' 【共通】インシデント機器情報新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント機器情報新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiSql(ByRef Cmd As NpgsqlCommand, _
                                        ByVal Cn As NpgsqlConnection, _
                                        ByVal dataHBKB0801 As DataHBKB0801) As Boolean

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
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))                       '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))                          '番号
                .Add(New NpgsqlParameter("CIKbnCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      'CI種別CD
                '[ADD]2013/03/12 t.fukuo 機器情報登録不具合対応 START
                .Add(New NpgsqlParameter("kikistate1", NpgsqlTypes.NpgsqlDbType.Varchar))                   '機器ステータス
                .Add(New NpgsqlParameter("kikistate2", NpgsqlTypes.NpgsqlDbType.Varchar))                   '機器ステータス
                '[ADD]2013/03/12 t.fukuo 機器情報登録不具合対応 END
                .Add(New NpgsqlParameter("CINmb", NpgsqlTypes.NpgsqlDbType.Integer))                        'CI番号
                .Add(New NpgsqlParameter("RegDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                      '登録日時
                .Add(New NpgsqlParameter("RegGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                     '登録者グループCD
                .Add(New NpgsqlParameter("RegID", NpgsqlTypes.NpgsqlDbType.Varchar))                        '登録者ID
                .Add(New NpgsqlParameter("UpdateDT", NpgsqlTypes.NpgsqlDbType.Timestamp))                   '更新日時
                .Add(New NpgsqlParameter("UpGrpCD", NpgsqlTypes.NpgsqlDbType.Varchar))                      '最終更新者グループCD
                .Add(New NpgsqlParameter("UpdateID", NpgsqlTypes.NpgsqlDbType.Varchar))                     '最終更新者ID
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
                .Parameters("KindCD").Value = dataHBKB0801.PropRowReg.Item("KikiKindCD")                    '種別CD：対象機器の種別CD
                .Parameters("Num").Value = dataHBKB0801.PropRowReg.Item("KikiNum")                          '番号：対象機器の番号
                .Parameters("CIKbnCD").Value = dataHBKB0801.PropStrCIKbnCD_Search                           'CI種別CD
                '[ADD]2013/03/12 t.fukuo 機器情報登録不具合対応 START
                .Parameters("kikistate1").Value = KIKISTATE_NO_INPUT                                        '機器状態なし
                .Parameters("kikistate2").Value = KIKISTATE_INPUT                                           '機器状態あり
                '[ADD]2013/03/12 t.fukuo 機器情報登録不具合対応 END
                .Parameters("CINmb").Value = dataHBKB0801.PropRowReg.Item("CINmb")                          'CI番号：対象機器のCI番号
                .Parameters("RegDT").Value = dataHBKB0801.PropDtmSysDate                                    '登録日時
                .Parameters("RegGrpCD").Value = PropWorkGroupCD                                             '登録者グループCD
                .Parameters("RegID").Value = PropUserId                                                     '登録者ID
                .Parameters("UpdateDT").Value = dataHBKB0801.PropDtmSysDate                                 '最終更新日時
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
    ''' 【共通】インシデント共通情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント共通情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncInfoLogSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0801 As DataHBKB0801) As Boolean

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
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                        'ログ番号
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("LogNo").Value = dataHBKB0801.PropIntLogNo                                      'ログ番号
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
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
    ''' 【共通】インシデント対応関係ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント対応関係ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKankeiLogSql(ByRef Cmd As NpgsqlCommand, _
                                             ByVal Cn As NpgsqlConnection, _
                                             ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKankeiLogSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                        'ログ番号
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("LogNo").Value = dataHBKB0801.PropIntLogNo                                      'ログ番号
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
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
    ''' 【共通】インシデント機器情報ログ新規登録用SQLの作成・設定処理
    ''' </summary>
    ''' <param name="Cmd">[IN/OUT]NpgSqlCommandクラス</param>
    ''' <param name="Cn">[IN]NpgSqlConnectionクラス</param>
    ''' <param name="dataHBKB0801">[IN]レンタル及び部所有機器の期限切れ検索一覧画面データクラス</param>
    ''' <returns>boolean エラーコード  true 正常終了  false	異常終了 </returns>
    ''' <remarks>インシデント機器情報ログ新規登録用のSQLを作成し、アダプタにセットする
    ''' <para>作成情報：2012/08/06 t.fukuo
    ''' <p>改訂情報：</p>
    ''' </para></remarks>
    Public Function SetInsertIncKikiLogSql(ByRef Cmd As NpgsqlCommand, _
                                           ByVal Cn As NpgsqlConnection, _
                                           ByVal dataHBKB0801 As DataHBKB0801) As Boolean

        '開始ログ出力
        CommonLogic.WriteLog(Common.LogLevel.TRACE_Lv, "START", Nothing, Nothing)

        '変数の宣言
        Dim strSQL As String = ""               'SQL文

        Try

            'SQL文(INSERT)
            strSQL = strInsertIncKikiLogSql

            'データアダプタに、SQLのINSERT文を設定
            Cmd = New NpgsqlCommand(strSQL, Cn)


            'バインド変数に型をセット
            With Cmd.Parameters
                .Add(New NpgsqlParameter("LogNo", NpgsqlTypes.NpgsqlDbType.Integer))                        'ログ番号
                .Add(New NpgsqlParameter("IncNmb", NpgsqlTypes.NpgsqlDbType.Integer))                       'インシデント番号
                .Add(New NpgsqlParameter("KindCD", NpgsqlTypes.NpgsqlDbType.Varchar))                       '種別CD
                .Add(New NpgsqlParameter("Num", NpgsqlTypes.NpgsqlDbType.Varchar))                          '番号
            End With
            'バインド変数に値をセット

            With Cmd
                .Parameters("LogNo").Value = dataHBKB0801.PropIntLogNo                                      'ログ番号
                .Parameters("IncNmb").Value = dataHBKB0801.PropIntIncNmb                                    'インシデント番号
                .Parameters("KindCD").Value = dataHBKB0801.PropRowReg.Item("KikiKindCD")                    '種別CD：対象機器の種別CD
                .Parameters("Num").Value = dataHBKB0801.PropRowReg.Item("KikiNum")                          '番号：対象機器の番号
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
