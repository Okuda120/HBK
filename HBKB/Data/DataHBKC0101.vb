Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' インシデント索一覧Dataクラス
''' </summary>
''' <remarks>インシデント検索一覧で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/07/24 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0101

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス

    '検索条件(フォームオブジェクト)
    Private ppTxtNum As TextBox                             'インシデント基本情報：番号
    Private ppCmbUketsukeWay As ComboBox                    'インシデント基本情報：受付手段
    Private ppCmbIncidentKind As ComboBox                   'インシデント基本情報：インシデント種別
    Private ppCmbDomain As ComboBox                         'インシデント基本情報：ドメイン
    Private ppTxtOutsideToolNum As TextBox                  'インシデント基本情報：外部ツール番号
    Private ppLstStatus As ListBox                          'インシデント基本情報：ステータス
    Private ppLstTargetSystem As ListBox                    'インシデント基本情報：対象システム
    Private ppTxtTitle As TextBox                           'インシデント基本情報：タイトル
    Private ppTxtUkeNaiyo As TextBox                        'インシデント基本情報：受付内容
    Private ppTxtTaioKekka As TextBox                       'インシデント基本情報：対応結果
    Private ppDtpHasseiDTFrom As DateTimePickerEx           'インシデント基本情報：発生日(From)
    Private ppDtpHasseiDTTo As DateTimePickerEx             'インシデント基本情報：発生日(To)
    Private ppDtpUpdateDTFrom As DateTimePickerEx           'インシデント基本情報：最終更新日時(日付From)
    Private ppTxtExUpdateTimeFrom As TextBoxEx_IoTime       'インシデント基本情報：最終更新日時(時刻From)
    Private ppDtpUpdateDTTo As DateTimePickerEx             'インシデント基本情報：最終更新日時(日付To)
    Private ppTxtExUpdateTimeTo As TextBoxEx_IoTime         'インシデント基本情報：最終更新日時(時刻To)
    Private ppTxtFreeText As TextBox                        'インシデント基本情報：フリーテキスト
    Private ppCmbFreeFlg1 As ComboBox                       'インシデント基本情報：フリーフラグ1
    Private ppCmbFreeFlg2 As ComboBox                       'インシデント基本情報：フリーフラグ2
    Private ppCmbFreeFlg3 As ComboBox                       'インシデント基本情報：フリーフラグ3
    Private ppCmbFreeFlg4 As ComboBox                       'インシデント基本情報：フリーフラグ4
    Private ppCmbFreeFlg5 As ComboBox                       'インシデント基本情報：フリーフラグ5
    Private ppTxtPartnerID As TextBox                       '相手情報：相手ID
    Private ppTxtPartnerNM As TextBox                       '相手情報：相手氏名
    Private ppTxtUsrBusyoNM As TextBox                      '相手情報：相手部署
    Private ppTxtEventID As TextBox                         'イベント情報：イベントID
    Private ppTxtOPCEventID As TextBox                      'イベント情報：OPCイベントID
    Private ppTxtSource As TextBox                          'イベント情報：ソース
    Private ppTxtEventClass As TextBox                      'イベント情報：イベントクラス
    Private ppRdoChokusetsu As RadioButton                  '担当者情報情報：直接
    Private ppRdoKanyo As RadioButton                       '担当者情報情報：間接
    Private ppCmbTantoGrp As ComboBox                       '担当者情報情報：担当者グループ
    Private ppTxtIncTantoID As TextBox                      '担当者情報情報：担当者ID
    Private ppTxtIncTantoNM As TextBox                      '担当者情報情報：担当者氏名
    Private ppDtpWorkSceDTFrom As DateTimePickerEx          '作業情報：作業予定日時(日付From)
    Private ppTxtExWorkSceTimeFrom As TextBoxEx_IoTime      '作業情報：作業予定日時(時刻From)
    Private ppDtpWorkSceDTTo As DateTimePickerEx            '作業情報：作業予定日時(日付To)
    Private ppTxtExWorkSceTimeTo As TextBoxEx_IoTime        '作業情報：作業予定日時(時刻To)
    Private ppTxtWorkNaiyo As TextBox                       '作業情報：作業内容
    Private ppCmbKikiKind As ComboBox                       '機器情報：機器種別
    Private ppTxtKikiNum As TextBox                         '機器情報：番号
    Private ppCmbProccesLinkKind As ComboBox                'プロセスリンク情報：種別
    Private ppTxtProcessLinkNum As TextBox                  'プロセスリンク情報：番号

    '検索結果
    Private ppLblResultCounter As Label                     '検索結果：件数
    Private ppVwIncidentList As FpSpread                    '検索結果：結果一覧表示用スプレッド

    'フッター
    Private ppBtnMakeExcel As Button                        'フッター：「Excel出力」ボタン

    'データ
    '検索条件
    Private ppStrLoginUserGrp As String                     '検索前提条件：ログインユーザ所属グループ
    Private ppStrLoginUserId As String                      '検索前提条件：ログインユーザID
    Private ppIntNum As Integer                             '[Excel出力]インシデント基本情報：番号
    Private ppStrUketsukeWay As String                      '[Excel出力]インシデント基本情報：受付手段
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
    Private ppStrProcessLinkNumAry As String                '[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）

    'データテーブル
    Private ppDtUketsukeWay As DataTable                    'データテーブル:受付得手段マスター
    Private ppDtIncidentKind As DataTable                   'データテーブル:インシデント種別マスター
    Private ppDtDomain As DataTable                         'データテーブル:ドメインマスター
    Private ppDtProcessState As DataTable                   'データテーブル:プロセスステータスマスター
    Private ppDtTargetSystem As DataTable                   'データテーブル:対象システム
    Private ppDtGrp As DataTable                            'データテーブル:グループマスター
    Private ppDtKind As DataTable                           'データテーブル:種別マスター
    Private ppDtResultCount As DataTable                    'データテーブル:検索件数
    Private ppDtIncidentInfo As DataTable                   'データテーブル:インシデント検索結果
    Private ppDtSubEndUser As DataTable                     'データテーブル:[検索子画面]エンドユーザ検索結果
    Private ppDtSubHibikiUser As DataTable                  'データテーブル:[検索子画面]ひびきユーザ検索結果
    Private ppDtSubKiki As DataTable                        'データテーブル:[検索子画面]機器検索結果
    Private ppDtSubProcess As DataTable                     'データテーブル:[検索子画面]プロセス検索結果
    Private ppDtResultSub As DataTable                      'サブ検索戻り値：相手先ID、担当ID

    '判定用フラグ
    Private ppBlnIndicationFlg As Boolean                   '検索結果表示判定用フラグ
    Private ppBlnIncNumInputFlg As Boolean                  'インシデント番号Null判定フラグ


    '*******************************************************
    '検索条件
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropGrpLoginUser() As GroupControlEx
        Get
            Return ppGrpLoginUser
        End Get
        Set(ByVal value As GroupControlEx)
            ppGrpLoginUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNum() As TextBox
        Get
            Return ppTxtNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：受付手段】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbUketsukeWay</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbUketsukeWay() As ComboBox
        Get
            Return ppCmbUketsukeWay
        End Get
        Set(ByVal value As ComboBox)
            ppCmbUketsukeWay = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：インシデント種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbIncidentKind</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbIncidentKind() As ComboBox
        Get
            Return ppCmbIncidentKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbIncidentKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：ドメイン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbDomain</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbDomain() As ComboBox
        Get
            Return ppCmbDomain
        End Get
        Set(ByVal value As ComboBox)
            ppCmbDomain = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：外部ツール番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtOutsideToolNum</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtOutsideToolNum() As TextBox
        Get
            Return ppTxtOutsideToolNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtOutsideToolNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstStatus</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstStatus() As ListBox
        Get
            Return ppLstStatus
        End Get
        Set(ByVal value As ListBox)
            ppLstStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstTargetSystem() As ListBox
        Get
            Return ppLstTargetSystem
        End Get
        Set(ByVal value As ListBox)
            ppLstTargetSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTitle() As TextBox
        Get
            Return ppTxtTitle
        End Get
        Set(ByVal value As TextBox)
            ppTxtTitle = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：受付内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUkeNaiyo() As TextBox
        Get
            Return ppTxtUkeNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtUkeNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：対応結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTaioKekka() As TextBox
        Get
            Return ppTxtTaioKekka
        End Get
        Set(ByVal value As TextBox)
            ppTxtTaioKekka = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：発生日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpHasseiDTFrom() As DateTimePickerEx
        Get
            Return ppDtpHasseiDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpHasseiDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：発生日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpHasseiDTTo() As DateTimePickerEx
        Get
            Return ppDtpHasseiDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpHasseiDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：最終更新日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpUpdateDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpUpdateDTFrom() As DateTimePickerEx
        Get
            Return ppDtpUpdateDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpUpdateDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：最終更新日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExUpdateTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtExUpdateTimeFrom() As TextBoxEx_IoTime
        Get
            Return ppTxtExUpdateTimeFrom
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtExUpdateTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：最終更新日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpUpdateDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpUpdateDTTo() As DateTimePickerEx
        Get
            Return ppDtpUpdateDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpUpdateDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：最終更新日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExUpdateTimeTo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtExUpdateTimeTo() As TextBoxEx_IoTime
        Get
            Return ppTxtExUpdateTimeTo
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtExUpdateTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeText() As TextBox
        Get
            Return ppTxtFreeText
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg1() As ComboBox
        Get
            Return ppCmbFreeFlg1
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg2() As ComboBox
        Get
            Return ppCmbFreeFlg2
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg3() As ComboBox
        Get
            Return ppCmbFreeFlg3
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg3 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg4() As ComboBox
        Get
            Return ppCmbFreeFlg4
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg4 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント基本情報：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlg5() As ComboBox
        Get
            Return ppCmbFreeFlg5
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlg5 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手情報：相手ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerID</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerID() As TextBox
        Get
            Return ppTxtPartnerID
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手情報：相手氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtPartnerNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtPartnerNM() As TextBox
        Get
            Return ppTxtPartnerNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtPartnerNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【相手情報：相手部署】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUsrBusyoNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtUsrBusyoNM() As TextBox
        Get
            Return ppTxtUsrBusyoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtUsrBusyoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベント情報：イベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEventID</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEventID() As TextBox
        Get
            Return ppTxtEventID
        End Get
        Set(ByVal value As TextBox)
            ppTxtEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベント情報：OPCイベントID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtOPCEventID</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtOPCEventID() As TextBox
        Get
            Return ppTxtOPCEventID
        End Get
        Set(ByVal value As TextBox)
            ppTxtOPCEventID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベント情報：ソース】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtSource</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSource() As TextBox
        Get
            Return ppTxtSource
        End Get
        Set(ByVal value As TextBox)
            ppTxtSource = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【イベント情報：イベントクラス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtEventClass</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEventClass() As TextBox
        Get
            Return ppTxtEventClass
        End Get
        Set(ByVal value As TextBox)
            ppTxtEventClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：直接】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRdoChokusetsu</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
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
    ''' プロパティセット【担当者情報情報：担当者グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbTantoGrp() As ComboBox
        Get
            Return ppCmbTantoGrp
        End Get
        Set(ByVal value As ComboBox)
            ppCmbTantoGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：担当者ID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoID</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncTantoID() As TextBox
        Get
            Return ppTxtIncTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：担当者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtIncTantoNM() As TextBox
        Get
            Return ppTxtIncTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtIncTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業情報：作業予定日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpWorkSceDTFrom</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpWorkSceDTFrom() As DateTimePickerEx
        Get
            Return ppDtpWorkSceDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpWorkSceDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業情報：作業予定日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExWorkSceTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtExWorkSceTimeFrom() As TextBoxEx_IoTime
        Get
            Return ppTxtExWorkSceTimeFrom
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtExWorkSceTimeFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業情報：作業予定日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpWorkSceDTTo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpWorkSceDTTo() As DateTimePickerEx
        Get
            Return ppDtpWorkSceDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpWorkSceDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業情報：作業予定日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExWorkSceTimeTo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtExWorkSceTimeTo() As TextBoxEx_IoTime
        Get
            Return ppTxtExWorkSceTimeTo
        End Get
        Set(ByVal value As TextBoxEx_IoTime)
            ppTxtExWorkSceTimeTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【作業情報：作業内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtWorkNaiyo</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtWorkNaiyo() As TextBox
        Get
            Return ppTxtWorkNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtWorkNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器情報：機器種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbKikiKind</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKikiKind() As ComboBox
        Get
            Return ppCmbKikiKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKikiKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【機器情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtKikiNum</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtKikiNum() As TextBox
        Get
            Return ppTxtKikiNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtKikiNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク情報：種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbProccesLinkKind</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbProccesLinkKind() As ComboBox
        Get
            Return ppCmbProccesLinkKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbProccesLinkKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtProcessLinkNum</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtProcessLinkNum() As TextBox
        Get
            Return ppTxtProcessLinkNum
        End Get
        Set(ByVal value As TextBox)
            ppTxtProcessLinkNum = value
        End Set
    End Property

    '*******************************************************
    '検索結果
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【検索結果：件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblResultCounter</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblResultCounter() As Label
        Get
            Return ppLblResultCounter
        End Get
        Set(ByVal value As Label)
            ppLblResultCounter = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果：結果一覧表示用スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwIncidentList</returns>
    ''' <remarks><para>作成情報：2012/07/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwIncidentList() As FpSpread
        Get
            Return ppVwIncidentList
        End Get
        Set(ByVal value As FpSpread)
            ppVwIncidentList = value
        End Set
    End Property

    '*******************************************************
    'フッター
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【フッター：「Excel出力」ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMakeExcel</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnMakeExcel() As Button
        Get
            Return ppBtnMakeExcel
        End Get
        Set(ByVal value As Button)
            ppBtnMakeExcel = value
        End Set
    End Property

    '*******************************************************
    'Execl出力用検索条件
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【検索前提条件：ログインユーザ所属グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserGrp</returns>
    ''' <remarks><para>作成情報：2012/07/31 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/31 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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

    ''' <summary>
    ''' プロパティセット【[Excel出力]インシデント基本情報：インシデント種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncidentKind</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
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
    ''' プロパティセット【[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProcessLinkNumAry</returns>
    ''' <remarks><para>作成情報：2012/09/18 m.ibuki
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrProcessLinkNumAry() As String
        Get
            Return ppStrProcessLinkNumAry
        End Get
        Set(ByVal value As String)
            ppStrProcessLinkNumAry = value
        End Set
    End Property

    '*******************************************************
    'データテーブル
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【データテーブル:受付得手段マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtUketsukeWay</returns>
    ''' <remarks><para>作成情報：2012/10/24 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtUketsukeWay() As DataTable
        Get
            Return ppDtUketsukeWay
        End Get
        Set(ByVal value As DataTable)
            ppDtUketsukeWay = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:インシデント種別マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIncidentKind</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIncidentKind() As DataTable
        Get
            Return ppDtIncidentKind
        End Get
        Set(ByVal value As DataTable)
            ppDtIncidentKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:ドメインマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtDomain</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtDomain() As DataTable
        Get
            Return ppDtDomain
        End Get
        Set(ByVal value As DataTable)
            ppDtDomain = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:プロセスステータスマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProcessState</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtProcessState() As DataTable
        Get
            Return ppDtProcessState
        End Get
        Set(ByVal value As DataTable)
            ppDtProcessState = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTargetSystem() As DataTable
        Get
            Return ppDtTargetSystem
        End Get
        Set(ByVal value As DataTable)
            ppDtTargetSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:グループマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGrp</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGrp() As DataTable
        Get
            Return ppDtGrp
        End Get
        Set(ByVal value As DataTable)
            ppDtGrp = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:種別マスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKind</returns>
    ''' <remarks><para>作成情報：2012/07/25 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKind() As DataTable
        Get
            Return ppDtKind
        End Get
        Set(ByVal value As DataTable)
            ppDtKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultCount</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultCount() As DataTable
        Get
            Return ppDtResultCount
        End Get
        Set(ByVal value As DataTable)
            ppDtResultCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:インシデント検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIncidentInfo</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtIncidentInfo() As DataTable
        Get
            Return ppDtIncidentInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtIncidentInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:[検索子画面]エンドユーザ検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubEndUser</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubEndUser() As DataTable
        Get
            Return ppDtSubEndUser
        End Get
        Set(ByVal value As DataTable)
            ppDtSubEndUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:[検索子画面]ひびきユーザ検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubHibikiUser</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubHibikiUser() As DataTable
        Get
            Return ppDtSubHibikiUser
        End Get
        Set(ByVal value As DataTable)
            ppDtSubHibikiUser = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:[検索子画面]機器検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubKiki</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubKiki() As DataTable
        Get
            Return ppDtSubKiki
        End Get
        Set(ByVal value As DataTable)
            ppDtSubKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:[検索子画面]プロセス検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubProcess</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtSubProcess() As DataTable
        Get
            Return ppDtSubProcess
        End Get
        Set(ByVal value As DataTable)
            ppDtSubProcess = value
        End Set
    End Property

    '*******************************************************
    '判定フラグ
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【検索結果表示判定用フラグ True:非表示 False:表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIndicationFlg</returns>
    ''' <remarks><para>作成情報：2012/07/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnIndicationFlg() As Boolean
        Get
            Return ppBlnIndicationFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnIndicationFlg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【インシデント番号入力判定フラグ True:未入力 False:入力】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIncNumInputFlg</returns>
    ''' <remarks><para>作成情報：2012/07/30 s.yamaguchi
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
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/13 r.hoshino
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtResultSub() As DataTable
        Get
            Return ppDtResultSub
        End Get
        Set(ByVal value As DataTable)
            ppDtResultSub = value
        End Set
    End Property

End Class
