Imports Common
Imports CommonHBK

''' <summary>
''' インシデント検索一覧Excel出力Dataクラス
''' </summary>
''' <remarks>インシデント検索一覧Excel出力で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/03 y.ikushima
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0102

    'データ
    Private ppStrOutPutFilePath As String           '出力先ファイルパス
    Private ppStrOutPutFileName As String           '出力ファイル名
    '検索条件
    Private ppStrLoginUserGrp As String                     '検索前提条件：ログインユーザ所属グループ
    Private ppStrLoginUserId As String                      '検索前提条件：ログインユーザID
    Private ppIntNum As Integer                             '[Excel出力]インシデント基本情報：番号
    '[ADD] 2012/10/24 s.yamaguchi START
    Private ppStrUketsukeWay As String                      '[Excel出力]インシデント基本情報：受付手段
    '[ADD] 2012/10/24 s.yamaguchi END
    Private ppStrIncidentKind As String                     '[Excel出力]インシデント基本情報：インシデント種別
    Private ppStrDomain As String                           '[Excel出力]インシデント基本情報：ドメイン
    Private ppStrOutsideToolNum As String                   '[Excel出力]インシデント基本情報：外部ツール番号
    Private ppStrStatus As String                           '[Excel出力]インシデント基本情報：ステータス
    Private ppStrTargetSystem As String                     '[Excel出力]インシデント基本情報：対象システム
    Private ppStrTitle As String                            '[Excel出力]インシデント基本情報：タイトル
    Private ppStrUkeNaiyo As String                         '[Excel出力]インシデント基本情報：受付内容
    Private ppStrTaioKekka As String                        '[Excel出力]インシデント基本情報：対応結果
    Private ppStrHasseiDTFrom As String                     '[Excel出力]インシデント基本情報：発生日(From)
    Private ppStrHasseiDTTo As String                       '[Excel出力]インシデント基本情報：発生日(To)
    Private ppStrUpdateDTFrom As String                     '[Excel出力]インシデント基本情報：最終更新日時(日付From)
    Private ppStrExUpdateTimeFrom As String                 '[Excel出力]インシデント基本情報：最終更新日時(時刻From)
    Private ppStrUpdateDTTo As String                       '[Excel出力]インシデント基本情報：最終更新日時(日付To)
    Private ppStrExUpdateTimeTo As String                   '[Excel出力]インシデント基本情報：最終更新日時(時刻To)
    Private ppStrFreeText As String                         '[Excel出力]インシデント基本情報：フリーテキスト
    Private ppStrFreeFlg1 As String                         '[Excel出力]インシデント基本情報：フリーフラグ1
    Private ppStrFreeFlg2 As String                         '[Excel出力]インシデント基本情報：フリーフラグ2
    Private ppStrFreeFlg3 As String                         '[Excel出力]インシデント基本情報：フリーフラグ3
    Private ppStrFreeFlg4 As String                         '[Excel出力]インシデント基本情報：フリーフラグ4
    Private ppStrFreeFlg5 As String                         '[Excel出力]インシデント基本情報：フリーフラグ5
    Private ppStrPartnerID As String                        '[Excel出力]相手情報：相手ID
    Private ppStrPartnerNM As String                        '[Excel出力]相手情報：相手氏名
    Private ppStrUsrBusyoNM As String                       '[Excel出力]相手情報：相手部署
    Private ppStrEventID As String                          '[Excel出力]イベント情報：イベントID
    Private ppStrOPCEventID As String                       '[Excel出力]イベント情報：OPCイベントID
    Private ppStrSource As String                           '[Excel出力]イベント情報：ソース
    Private ppStrEventClass As String                       '[Excel出力]イベント情報：イベントクラス
    Private ppBlnChokusetsu As Boolean                      '[Excel出力]担当者情報情報：直接
    Private ppBlnKanyo As Boolean                           '[Excel出力]担当者情報情報：間接
    Private ppStrTantoGrp As String                         '[Excel出力]担当者情報情報：担当者グループ
    Private ppStrIncTantoID As String                       '[Excel出力]担当者情報情報：担当者ID
    Private ppStrIncTantoNM As String                       '[Excel出力]担当者情報情報：担当者氏名
    Private ppStrWorkSceDTFrom As String                    '[Excel出力]作業情報：作業予定日時(日付From)
    Private ppStrExWorkSceTimeFrom As String                '[Excel出力]作業情報：作業予定日時(時刻From)
    Private ppStrWorkSceDTTo As String                      '[Excel出力]作業情報：作業予定日時(日付To)
    Private ppStrExWorkSceTimeTo As String                  '[Excel出力]作業情報：作業予定日時(時刻To)
    Private ppStrWorkNaiyo As String                        '[Excel出力]作業情報：作業内容
    Private ppStrKikiKind As String                         '[Excel出力]機器情報：機器種別
    Private ppStrKikiNum As String                          '[Excel出力]機器情報：番号
    Private ppStrProccesLinkKind As String                  '[Excel出力]プロセスリンク情報：種別
    Private ppStrProcessLinkNum As String                   '[Excel出力]プロセスリンク情報：番号
    Private ppStrTantoRdoCheck As String                    '[Excel出力]担当者ラジオボタンフラグ

    'データテーブル
    Private ppDtResult As DataTable                    'データテーブル:検索結果

    Private ppBlnIncNumInputFlg As Boolean                  'インシデント番号Null判定フラグ
    Private ppRdoChokusetsu As RadioButton                  '担当者情報情報：直接
    Private ppRdoKanyo As RadioButton                       '担当者情報情報：間接

    '*******************************************************
    'Execl出力用検索条件
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【検索前提条件：ログインユーザ所属グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserGrp</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserGrp() As String
        Get
            Return ppStrLoginUserGrp
        End Get
        Set(ByVal value As String)
            ppStrLoginUserGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索前提条件：ログインユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserId</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrLoginUserId() As String
        Get
            Return ppStrLoginUserId
        End Get
        Set(ByVal value As String)
            ppStrLoginUserId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntNum</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntNum() As Integer
        Get
            Return ppIntNum
        End Get
        Set(ByVal value As Integer)
            ppIntNum = value
        End Set
    End Property

    '[ADD] 2012/10/24 s.yamaguchi START
    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：受付手段】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUketsukeWay</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUketsukeWay() As String
        Get
            Return ppStrUketsukeWay
        End Get
        Set(ByVal value As String)
            ppStrUketsukeWay = value
        End Set
    End Property
    '[ADD] 2012/10/24 s.yamaguchi END

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：インシデント種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncidentKind</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncidentKind() As String
        Get
            Return ppStrIncidentKind
        End Get
        Set(ByVal value As String)
            ppStrIncidentKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：ドメイン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrDomain</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrDomain() As String
        Get
            Return ppStrDomain
        End Get
        Set(ByVal value As String)
            ppStrDomain = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：外部ツール番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutsideToolNum</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutsideToolNum() As String
        Get
            Return ppStrOutsideToolNum
        End Get
        Set(ByVal value As String)
            ppStrOutsideToolNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStatus</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrStatus() As String
        Get
            Return ppStrStatus
        End Get
        Set(ByVal value As String)
            ppStrStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTargetSystem() As String
        Get
            Return ppStrTargetSystem
        End Get
        Set(ByVal value As String)
            ppStrTargetSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTitle() As String
        Get
            Return ppStrTitle
        End Get
        Set(ByVal value As String)
            ppStrTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：受付内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUkeNaiyo() As String
        Get
            Return ppStrUkeNaiyo
        End Get
        Set(ByVal value As String)
            ppStrUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：対応結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTaioKekka() As String
        Get
            Return ppStrTaioKekka
        End Get
        Set(ByVal value As String)
            ppStrTaioKekka = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：発生日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHasseiDTFrom() As String
        Get
            Return ppStrHasseiDTFrom
        End Get
        Set(ByVal value As String)
            ppStrHasseiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：発生日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrHasseiDTTo() As String
        Get
            Return ppStrHasseiDTTo
        End Get
        Set(ByVal value As String)
            ppStrHasseiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：最終更新日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateDTFrom() As String
        Get
            Return ppStrUpdateDTFrom
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：最終更新日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExUpdateTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrExUpdateTimeFrom() As String
        Get
            Return ppStrExUpdateTimeFrom
        End Get
        Set(ByVal value As String)
            ppStrExUpdateTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：最終更新日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUpdateDTTo() As String
        Get
            Return ppStrUpdateDTTo
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：最終更新日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExUpdateTimeTo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrExUpdateTimeTo() As String
        Get
            Return ppStrExUpdateTimeTo
        End Get
        Set(ByVal value As String)
            ppStrExUpdateTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeText</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeText() As String
        Get
            Return ppStrFreeText
        End Get
        Set(ByVal value As String)
            ppStrFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg1() As String
        Get
            Return ppStrFreeFlg1
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg2() As String
        Get
            Return ppStrFreeFlg2
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg3() As String
        Get
            Return ppStrFreeFlg3
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg4() As String
        Get
            Return ppStrFreeFlg4
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrFreeFlg5() As String
        Get
            Return ppStrFreeFlg5
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]相手情報：相手ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerID</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerID() As String
        Get
            Return ppStrPartnerID
        End Get
        Set(ByVal value As String)
            ppStrPartnerID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]相手情報：相手氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPartnerNM() As String
        Get
            Return ppStrPartnerNM
        End Get
        Set(ByVal value As String)
            ppStrPartnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]相手情報：相手部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrUsrBusyoNM() As String
        Get
            Return ppStrUsrBusyoNM
        End Get
        Set(ByVal value As String)
            ppStrUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]イベント情報：イベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEventID</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEventID() As String
        Get
            Return ppStrEventID
        End Get
        Set(ByVal value As String)
            ppStrEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]イベント情報：OPCイベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOPCEventID() As String
        Get
            Return ppStrOPCEventID
        End Get
        Set(ByVal value As String)
            ppStrOPCEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]イベント情報：ソース】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSource</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSource() As String
        Get
            Return ppStrSource
        End Get
        Set(ByVal value As String)
            ppStrSource = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]イベント情報：イベントクラス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrEventClass</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrEventClass() As String
        Get
            Return ppStrEventClass
        End Get
        Set(ByVal value As String)
            ppStrEventClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：直接】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnChokusetsu</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnChokusetsu() As Boolean
        Get
            Return ppBlnChokusetsu
        End Get
        Set(ByVal value As Boolean)
            ppBlnChokusetsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：間接】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnKanyo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnKanyo() As Boolean
        Get
            Return ppBlnKanyo
        End Get
        Set(ByVal value As Boolean)
            ppBlnKanyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：担当者グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoGrp() As String
        Get
            Return ppStrTantoGrp
        End Get
        Set(ByVal value As String)
            ppStrTantoGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：担当者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncTantoID</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncTantoID() As String
        Get
            Return ppStrIncTantoID
        End Get
        Set(ByVal value As String)
            ppStrIncTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：担当者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrIncTantoNM() As String
        Get
            Return ppStrIncTantoNM
        End Get
        Set(ByVal value As String)
            ppStrIncTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]作業情報：作業予定日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceDTFrom() As String
        Get
            Return ppStrWorkSceDTFrom
        End Get
        Set(ByVal value As String)
            ppStrWorkSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]作業情報：作業予定日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExWorkSceTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrExWorkSceTimeFrom() As String
        Get
            Return ppStrExWorkSceTimeFrom
        End Get
        Set(ByVal value As String)
            ppStrExWorkSceTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]作業情報：作業予定日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkSceDTTo() As String
        Get
            Return ppStrWorkSceDTTo
        End Get
        Set(ByVal value As String)
            ppStrWorkSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]作業情報：作業予定日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExWorkSceTimeTo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrExWorkSceTimeTo() As String
        Get
            Return ppStrExWorkSceTimeTo
        End Get
        Set(ByVal value As String)
            ppStrExWorkSceTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]作業情報：作業内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkNaiyo() As String
        Get
            Return ppStrWorkNaiyo
        End Get
        Set(ByVal value As String)
            ppStrWorkNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]機器情報：機器種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiKind</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKikiKind() As String
        Get
            Return ppStrKikiKind
        End Get
        Set(ByVal value As String)
            ppStrKikiKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]機器情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKikiNum</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKikiNum() As String
        Get
            Return ppStrKikiNum
        End Get
        Set(ByVal value As String)
            ppStrKikiNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]プロセスリンク情報：種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProccesLinkKind</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProccesLinkKind() As String
        Get
            Return ppStrProccesLinkKind
        End Get
        Set(ByVal value As String)
            ppStrProccesLinkKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]プロセスリンク情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessLinkNum</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessLinkNum() As String
        Get
            Return ppStrProcessLinkNum
        End Get
        Set(ByVal value As String)
            ppStrProcessLinkNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者ラジオボタンチェックフラグ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoRdoCheck</returns>
    ''' <remarks><para>作成情報：2012/08/05 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoRdoCheck() As String
        Get
            Return ppStrTantoRdoCheck
        End Get
        Set(ByVal value As String)
            ppStrTantoRdoCheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResult</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResult() As DataTable
        Get
            Return ppDtResult
        End Get
        Set(ByVal value As DataTable)
            ppDtResult = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント番号入力判定フラグ True:未入力 False:入力】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIncNumInputFlg</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnIncNumInputFlg() As Boolean
        Get
            Return ppBlnIncNumInputFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnIncNumInputFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：直接】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoChokusetsu</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoChokusetsu() As RadioButton
        Get
            Return ppRdoChokusetsu
        End Get
        Set(ByVal value As RadioButton)
            ppRdoChokusetsu = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：間接】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoKanyo</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropRdoKanyo() As RadioButton
        Get
            Return ppRdoKanyo
        End Get
        Set(ByVal value As RadioButton)
            ppRdoKanyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力先ファイルパス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFilePath</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFilePath() As String
        Get
            Return ppStrOutPutFilePath
        End Get
        Set(ByVal value As String)
            ppStrOutPutFilePath = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力ファイル名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrOutPutFileName</returns>
    ''' <remarks><para>作成情報：2012/08/03 y.ikushima
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrOutPutFileName() As String
        Get
            Return ppStrOutPutFileName
        End Get
        Set(ByVal value As String)
            ppStrOutPutFileName = value
        End Set
    End Property

End Class
