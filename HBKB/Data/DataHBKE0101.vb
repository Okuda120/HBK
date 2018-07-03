Imports Common
Imports CommonHBK
Imports FarPoint.Win.Spread

''' <summary>
''' 変更検索一覧Dataクラス
''' </summary>
''' <remarks>変更検索一覧で使用するデータのプロパティセットを行う
''' <para>作成情報：2012/08/21 k.imayama
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKE0101

    'フォームオブジェクト
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス

    '検索条件(フォームオブジェクト)
    Private ppTxtNum As TextBox                             '基本情報：番号
    Private ppLstStatus As ListBox                          '基本情報：ステータス
    Private ppLstTargetSystem As ListBox                    '基本情報：対象システム
    Private ppTxtTitle As TextBox                           '基本情報：タイトル
    Private ppTxtNaiyo As TextBox                           '基本情報：内容
    Private ppTxtTaiosyo As TextBox                         '基本情報：対処
    Private ppTxtCyspr As TextBox                           '基本情報：Cyspr
    Private ppDtpkaisidtFrom As DateTimePickerEx           '基本情報：開始日(From)
    Private ppDtpkaisidtTo As DateTimePickerEx             '基本情報：開始日(To)
    Private ppDtpKanryoDTFrom As DateTimePickerEx           '基本情報：完了日(From)
    Private ppDtpKanryoDTTo As DateTimePickerEx             '基本情報：完了日(To)
    Private ppDtpTorokuDTFrom As DateTimePickerEx           '基本情報：登録日(From)
    Private ppDtpTorokuDTTo As DateTimePickerEx             '基本情報：登録日(To)
    Private ppDtpUpdateDTFrom As DateTimePickerEx           '基本情報：最終更新日時(日付From)
    Private ppTxtExUpdateTimeFrom As TextBoxEx_IoTime       '基本情報：最終更新日時(時刻From)
    Private ppDtpUpdateDTTo As DateTimePickerEx             '基本情報：最終更新日時(日付To)
    Private ppTxtExUpdateTimeTo As TextBoxEx_IoTime         '基本情報：最終更新日時(時刻To)
    Private ppTxtFreeText As TextBox                        '基本情報：フリーテキスト
    Private ppCmbFreeFlg1 As ComboBox                       '基本情報：フリーフラグ1
    Private ppCmbFreeFlg2 As ComboBox                       '基本情報：フリーフラグ2
    Private ppCmbFreeFlg3 As ComboBox                       '基本情報：フリーフラグ3
    Private ppCmbFreeFlg4 As ComboBox                       '基本情報：フリーフラグ4
    Private ppCmbFreeFlg5 As ComboBox                       '基本情報：フリーフラグ5
    Private ppCmbTantoGrp As ComboBox                       '担当者情報情報：担当者グループ
    Private ppTxtTantoID As TextBox                         '担当者情報情報：担当者ID
    Private ppTxtTantoNM As TextBox                         '担当者情報情報：担当者氏名
    Private ppCmbProccesLinkKind As ComboBox                'プロセスリンク情報：種別
    Private ppTxtProcessLinkNum As TextBox                  'プロセスリンク情報：番号

    '検索結果
    Private ppLblResultCounter As Label                     '検索結果：件数
    Private ppVwChangeList As FpSpread                      '検索結果：結果一覧表示用スプレッド

    'フッター
    Private ppBtnMakeExcel As Button                        'フッター：「Excel出力」ボタン

    'データ
    '検索条件
    Private ppStrLoginUserGrp As String                     '検索前提条件：ログインユーザ所属グループ
    Private ppStrLoginUserId As String                      '検索前提条件：ログインユーザID
    Private ppStrChgNmb As String                           '[Excel出力]基本情報：番号
    Private ppStrStatus As String                           '[Excel出力]基本情報：ステータス
    Private ppStrTargetSystem As String                     '[Excel出力]基本情報：対象システム
    Private ppStrTitle As String                            '[Excel出力]基本情報：タイトル
    Private ppStrNaiyo As String                            '[Excel出力]基本情報：内容
    Private ppStrTaisyo As String                           '[Excel出力]基本情報：対処
    Private ppStrCyspr As String                            '[Excel出力]基本情報：Cyspr
    Private ppStrkaisidtFrom As String                     '[Excel出力]基本情報：開始日(From)
    Private ppStrkaisidtTo As String                       '[Excel出力]基本情報：開始日(To)
    Private ppStrKanryoDTFrom As String                     '[Excel出力]基本情報：完了日(From)
    Private ppStrKanryoDTTo As String                       '[Excel出力]基本情報：完了日(To)
    Private ppStrTorokuDTFrom As String                     '[Excel出力]基本情報：登録日(From)
    Private ppStrTorokuDTTo As String                       '[Excel出力]基本情報：登録日(To)
    Private ppStrUpdateDTFrom As String                     '[Excel出力]基本情報：最終更新日時(日付From)
    Private ppStrExUpdateTimeFrom As String                 '[Excel出力]基本情報：最終更新日時(時刻From)
    Private ppStrUpdateDTTo As String                       '[Excel出力]基本情報：最終更新日時(日付To)
    Private ppStrExUpdateTimeTo As String                   '[Excel出力]基本情報：最終更新日時(時刻To)
    Private ppStrFreeText As String                         '[Excel出力]基本情報：フリーテキスト
    Private ppStrFreeFlg1 As String                         '[Excel出力]基本情報：フリーフラグ1
    Private ppStrFreeFlg2 As String                         '[Excel出力]基本情報：フリーフラグ2
    Private ppStrFreeFlg3 As String                         '[Excel出力]基本情報：フリーフラグ3
    Private ppStrFreeFlg4 As String                         '[Excel出力]基本情報：フリーフラグ4
    Private ppStrFreeFlg5 As String                         '[Excel出力]基本情報：フリーフラグ5
    Private ppStrTantoGrp As String                         '[Excel出力]担当者情報情報：担当者グループ
    Private ppStrTantoID As String                          '[Excel出力]担当者情報情報：担当者ID
    Private ppStrTantoNM As String                          '[Excel出力]担当者情報情報：担当者氏名
    Private ppStrProccesLinkKind As String                  '[Excel出力]プロセスリンク情報：種別
    Private ppStrProcessLinkNum As String                   '[Excel出力]プロセスリンク情報：番号
    Private ppStrProcessLinkNumAry As String                '[Excel出力]プロセスリンク情報：番号（カンマ区切り文字列）
    'データテーブル
    Private ppDtProcessState As DataTable                   'データテーブル:プロセスステータスマスター
    Private ppDtTargetSystem As DataTable                   'データテーブル:対象システム
    Private ppDtGrp As DataTable                            'データテーブル:グループマスター
    Private ppDtResultCount As DataTable                    'データテーブル:検索件数
    Private ppDtChangeInfo As DataTable                     'データテーブル:変更検索結果
    Private ppDtSubHibikiUser As DataTable                  'データテーブル:[検索子画面]ひびきユーザ検索結果
    Private ppDtSubProcess As DataTable                     'データテーブル:[検索子画面]プロセス検索結果
    Private ppDtResultSub As DataTable                      'サブ検索戻り値：担当ID

    '判定用フラグ
    Private ppBlnIndicationFlg As Boolean                   '検索結果表示判定用フラグ
    Private ppBlnChgNumInputFlg As Boolean                  '変更番号Null判定フラグ

    '*******************************************************
    '検索条件
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstStatus</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNaiyo() As TextBox
        Get
            Return ppTxtNaiyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：対処】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTaiosyo() As TextBox
        Get
            Return ppTxtTaiosyo
        End Get
        Set(ByVal value As TextBox)
            ppTxtTaiosyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：Cyspr】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCyspr() As TextBox
        Get
            Return ppTxtCyspr
        End Get
        Set(ByVal value As TextBox)
            ppTxtCyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：開始日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpkaisidtFrom() As DateTimePickerEx
        Get
            Return ppDtpkaisidtFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpkaisidtFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：開始日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpkaisidtTo() As DateTimePickerEx
        Get
            Return ppDtpkaisidtTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpkaisidtTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpkanryoDTFrom() As DateTimePickerEx
        Get
            Return ppDtpKanryoDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：完了日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpkanryoDTTo() As DateTimePickerEx
        Get
            Return ppDtpKanryoDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpKanryoDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：登録日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpTorokuDTFrom() As DateTimePickerEx
        Get
            Return ppDtpTorokuDTFrom
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpTorokuDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：登録日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpTorokuDTTo() As DateTimePickerEx
        Get
            Return ppDtpTorokuDTTo
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpTorokuDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【基本情報：最終更新日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpUpdateDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：最終更新日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExUpdateTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：最終更新日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpUpdateDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：最終更新日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtExUpdateTimeTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtFreeText</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【基本情報：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【担当者情報情報：担当者グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoID() As TextBox
        Get
            Return ppTxtTantoID
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【担当者情報情報：担当者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppTxtIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtTantoNM() As TextBox
        Get
            Return ppTxtTantoNM
        End Get
        Set(ByVal value As TextBox)
            ppTxtTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【プロセスリンク情報：種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbProccesLinkKind</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

    ''' <summary>
    ''' プロパティセット【検索結果：件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblResultCounter</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwChangeList() As FpSpread
        Get
            Return ppVwChangeList
        End Get
        Set(ByVal value As FpSpread)
            ppVwChangeList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フッター：「Excel出力」ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnMakeExcel</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntNum</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrChgNmb() As String
        Get
            Return ppStrChgNmb
        End Get
        Set(ByVal value As String)
            ppStrChgNmb = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：ステータス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStatus</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：対象システム】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTargetSystem</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：タイトル】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：受付内容】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUkeNaiyo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrNaiyo() As String
        Get
            Return ppStrNaiyo
        End Get
        Set(ByVal value As String)
            ppStrNaiyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：対処】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTaisyo() As String
        Get
            Return ppStrTaisyo
        End Get
        Set(ByVal value As String)
            ppStrTaisyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：Cyspr】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTaioKekka</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCyspr() As String
        Get
            Return ppStrCyspr
        End Get
        Set(ByVal value As String)
            ppStrCyspr = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：開始日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrkaisidtFrom() As String
        Get
            Return ppStrkaisidtFrom
        End Get
        Set(ByVal value As String)
            ppStrkaisidtFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：開始日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrkaisidtTo() As String
        Get
            Return ppStrkaisidtTo
        End Get
        Set(ByVal value As String)
            ppStrkaisidtTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：完了日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDTFrom() As String
        Get
            Return ppStrKanryoDTFrom
        End Get
        Set(ByVal value As String)
            ppStrKanryoDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：完了日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrKanryoDTTo() As String
        Get
            Return ppStrKanryoDTTo
        End Get
        Set(ByVal value As String)
            ppStrKanryoDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：登録日(From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKanryoDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTorokuDTFrom() As String
        Get
            Return ppStrTorokuDTFrom
        End Get
        Set(ByVal value As String)
            ppStrTorokuDTFrom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：登録日(To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrHasseiDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTorokuDTTo() As String
        Get
            Return ppStrTorokuDTTo
        End Get
        Set(ByVal value As String)
            ppStrTorokuDTTo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]基本情報：最終更新日時(日付From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：最終更新日時(時刻From)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExUpdateTimeFrom</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：最終更新日時(日付To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：最終更新日時(時刻To)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrExUpdateTimeTo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeText</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]基本情報：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【[Excel出力]担当者情報情報：担当者グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrTantoGrp</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoID() As String
        Get
            Return ppStrTantoID
        End Get
        Set(ByVal value As String)
            ppStrTantoID = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]担当者情報情報：担当者氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrIncTantoNM</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrTantoNM() As String
        Get
            Return ppStrTantoNM
        End Get
        Set(ByVal value As String)
            ppStrTantoNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【[Excel出力]プロセスリンク情報：種別】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrProccesLinkKind</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【データテーブル:プロセスステータスマスター】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtProcessState</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【データテーブル:検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultCount</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【データテーブル:変更検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtIncidentInfo</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtChangeInfo() As DataTable
        Get
            Return ppDtChangeInfo
        End Get
        Set(ByVal value As DataTable)
            ppDtChangeInfo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:[検索子画面]ひびきユーザ検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubHibikiUser</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【データテーブル:[検索子画面]プロセス検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtSubProcess</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

    ''' <summary>
    ''' プロパティセット【サブ検索戻り値：検索データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultSub</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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

    '*******************************************************
    '判定フラグ
    '*******************************************************

    ''' <summary>
    ''' プロパティセット【検索結果表示判定用フラグ True:非表示 False:表示】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIndicationFlg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
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
    ''' プロパティセット【変更番号入力判定フラグ True:未入力 False:入力】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnIncNumInputFlg</returns>
    ''' <remarks><para>作成情報：2012/08/21 k.imayama
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnChgNumInputFlg() As Boolean
        Get
            Return ppBlnChgNumInputFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnChgNumInputFlg = value
        End Set
    End Property

End Class
