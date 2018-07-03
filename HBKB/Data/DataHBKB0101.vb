Imports FarPoint.Win.Spread
Imports Common

Public Class DataHBKB0101

    'フォームオブジェクト
    Private ppCmbGroupCD As ComboBox            'グループ名コンボボックス
    Private ppLstCiClassCD As ListBox           'CI種別リストボックス
    Private ppCmbClassCD As ComboBox            '種別コンボボックス
    Private ppTxtNumberCD As TextBox            '番号テキストボックス
    Private ppCmbStatusCD As ComboBox           'ステータスコンボボックス
    Private ppCmbCiOwnerCD As ComboBox          'CIオーナーコンボボックス
    Private ppTxtCategory1CD As TextBox         '分類１テキストボックス
    Private ppTxtCategory2CD As TextBox         '分類２テキストボックス
    Private ppTxtNameCD As TextBox              '名称テキストボックス
    Private ppTxtFreeWordCD As TextBox          'フリーワードテキストボックス
    Private ppDtpStartDT As DateTimePickerEx    '最終更新日(FROM)DTPボックス
    Private ppDtpEndDT As DateTimePickerEx      '最終更新日(TO)DTPボックス
    Private ppTxtFreeTextCD As TextBox          'フリーテキストテキストボックス
    Private ppCmbFreeFlag1CD As ComboBox        'フリーフラグ1コンボボックス
    Private ppCmbFreeFlag2CD As ComboBox        'フリーフラグ2コンボボックス
    Private ppCmbFreeFlag3CD As ComboBox        'フリーフラグ3コンボボックス
    Private ppCmbFreeFlag4CD As ComboBox        'フリーフラグ4コンボボックス
    Private ppCmbFreeFlag5CD As ComboBox        'フリーフラグ5コンボボックス
    Private ppTxtDocCD As TextBox               '文書配付先テキストボックス
    Private ppVwDoc As FpSpread                 '文書一覧スプレッド
    Private ppVwOther As FpSpread               'その他一覧スプレッド
    Private ppLblCount As Label                 '件数ラベル
    Private ppBtnNewReg As Button               '新規登録ボタン
    Private ppBtnUpPack As Button               '一括登録ボタン
    Private ppBtnOutput As Button               'EXCEL出力ボタン

    'パラメータ変数宣言(遷移元情報)
    Private ppStrPlmCIKbnCD As String           'CI種別コード

    'パラメータ変数宣言(値取得)
    Private ppDtCiClass As DataTable            'CI種別CDとCI種別名を取得
    Private ppDtKindAll As DataTable            '種別CDと種別名を取得（全データ）
    Private ppDtKind As DataTable               '種別CDと種別名を取得（CI種別ごとのデータ）
    Private ppDtStatusAll As DataTable          'CIステータスCDとステータス名を取得（全データ）
    Private ppDtStatus As DataTable             'CIステータスCDとステータス名を取得（CI種別ごとのデータ）
    Private ppDtCiOwner As DataTable            'グループCDとグループ名を取得

    '検索用
    Private ppIntResultCnt As Integer
    Private ppCount As String

    '検索ボタンクリック時検索条件
    Private ppStrGroupCD_Search As String       'グループCD
    Private ppStrCiKbnCD_Search As String       'CI種別CD
    Private ppStrKindCD_Search As String        '種別CD
    Private ppStrNum_Search As String           '番号
    Private ppStrStatusCD_Search As String      'ステータスCD
    Private ppStrCiOwnerCD_Search As String     'CIオーナーCD
    Private ppStrClass1_Search As String        '分類１
    Private ppStrClass2_Search As String        '分類２
    Private ppStrCINM_Search As String          '名称
    Private ppStrFreeWordAimai_Search As String 'フリーワード
    Private ppStrUpdateDTFrom_Search As String  '最終更新日(FROM)
    Private ppStrUpdateDTTo_Search As String    '最終更新日(TO)
    Private ppStrBikoAimai_Search As String     'フリーテキスト
    Private ppStrFreeFlg1_Search As String      'フリーフラグ1
    Private ppStrFreeFlg2_Search As String      'フリーフラグ2
    Private ppStrFreeFlg3_Search As String      'フリーフラグ3
    Private ppStrFreeFlg4_Search As String      'フリーフラグ4
    Private ppStrFreeFlg5_Search As String      'フリーフラグ5
    Private ppStrShareteamNM_Search As String   '文書配付先

    'その他ファンクション用パラメータ
    Private ppIntSelectedRow As Integer         '選択行番号

    'その他フラグ
    Private ppBlnEnabledFlg As Boolean          '出力ボタン活性／非活性判定用フラグ

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbClassCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbGroupCD() As ComboBox
        Get
            Return ppCmbGroupCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbGroupCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLstCiClassCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLstCiClassCD() As ListBox
        Get
            Return ppLstCiClassCD
        End Get
        Set(ByVal value As ListBox)
            ppLstCiClassCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbClassCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbClassCD() As ComboBox
        Get
            Return ppCmbClassCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbClassCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtNumberCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNumberCD() As TextBox
        Get
            Return ppTxtNumberCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtNumberCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbStatusCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbStatusCD() As ComboBox
        Get
            Return ppCmbStatusCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbCiOwnerCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCiOwnerCD() As ComboBox
        Get
            Return ppCmbCiOwnerCD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbCiOwnerCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtCategory1CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCategory1CD() As TextBox
        Get
            Return ppTxtCategory1CD
        End Get
        Set(ByVal value As TextBox)
            ppTxtCategory1CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtCategory2CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCategory2CD() As TextBox
        Get
            Return ppTxtCategory2CD
        End Get
        Set(ByVal value As TextBox)
            ppTxtCategory2CD = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtNameCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtNameCD() As TextBox
        Get
            Return ppTxtNameCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtNameCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFreeWordCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeWordCD() As TextBox
        Get
            Return ppTxtFreeWordCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeWordCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpStartDT</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpStartDT() As DateTimePickerEx
        Get
            Return ppDtpStartDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpStartDT = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtpEndDT</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtpEndDT() As DateTimePickerEx
        Get
            Return ppDtpEndDT
        End Get
        Set(ByVal value As DateTimePickerEx)
            ppDtpEndDT = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFreeTextCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtFreeTextCD() As TextBox
        Get
            Return ppTxtFreeTextCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtFreeTextCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlag1CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlag1CD() As ComboBox
        Get
            Return ppCmbFreeFlag1CD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlag1CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlag2CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlag2CD() As ComboBox
        Get
            Return ppCmbFreeFlag2CD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlag2CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlag3CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlag3CD() As ComboBox
        Get
            Return ppCmbFreeFlag3CD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlag3CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlag4CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlag4CD() As ComboBox
        Get
            Return ppCmbFreeFlag4CD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlag4CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbFreeFlag5CD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbFreeFlag5CD() As ComboBox
        Get
            Return ppCmbFreeFlag5CD
        End Get
        Set(ByVal value As ComboBox)
            ppCmbFreeFlag5CD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtDocCD</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtDocCD() As TextBox
        Get
            Return ppTxtDocCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtDocCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwDoc</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwDoc() As FpSpread
        Get
            Return ppVwDoc
        End Get
        Set(ByVal value As FpSpread)
            ppVwDoc = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwOther</returns>
    ''' <remarks><para>作成情報：2012/06/19 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwOther() As FpSpread
        Get
            Return ppVwOther
        End Get
        Set(ByVal value As FpSpread)
            ppVwOther = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト：件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblCount() As Label
        Get
            Return ppLblCount
        End Get
        Set(ByVal value As Label)
            ppLblCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト：新規登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnNewReg</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnNewReg() As Button
        Get
            Return ppBtnNewReg
        End Get
        Set(ByVal value As Button)
            ppBtnNewReg = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト：一括登録ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnUpPack</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnUpPack() As Button
        Get
            Return ppBtnUpPack
        End Get
        Set(ByVal value As Button)
            ppBtnUpPack = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フォームオブジェクト：EXCEL出力ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppBtnOutput</returns>
    ''' <remarks><para>作成情報：2012/07/23 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnOutput() As Button
        Get
            Return ppBtnOutput
        End Get
        Set(ByVal value As Button)
            ppBtnOutput = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【遷移元情報：CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrPlmCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrPlmCIKbnCD() As String
        Get
            Return ppStrPlmCIKbnCD
        End Get
        Set(ByVal value As String)
            ppStrPlmCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【値取得】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCiClass</returns>
    ''' <remarks><para>作成情報：2012/06/01 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCiClass() As DataTable
        Get
            Return ppDtCiClass
        End Get
        Set(ByVal value As DataTable)
            ppDtCiClass = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別マスタデータ（全CI種別）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtKindAll</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindAll() As DataTable
        Get
            Return ppDtKindAll
        End Get
        Set(ByVal value As DataTable)
            ppDtKindAll = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別マスタデータ（CI種別ごと）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtKind</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
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
    ''' プロパティセット【CIステータスマスタデータ（全CI種別）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtStatusAll</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtStatusAll() As DataTable
        Get
            Return ppDtStatusAll
        End Get
        Set(ByVal value As DataTable)
            ppDtStatusAll = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIステータスマスタデータ（CI種別ごと）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtStatus</returns>
    ''' <remarks><para>作成情報：2012/07/22 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtStatus() As DataTable
        Get
            Return ppDtStatus
        End Get
        Set(ByVal value As DataTable)
            ppDtStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【値取得】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCiOwner</returns>
    ''' <remarks><para>作成情報：2012/06/01 kuga
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCiOwner() As DataTable
        Get
            Return ppDtCiOwner
        End Get
        Set(ByVal value As DataTable)
            ppDtCiOwner = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntResultCnt</returns>
    ''' <remarks><para>作成情報：2012/06/04 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropIntResultCnt() As Integer
        Get
            Return ppIntResultCnt
        End Get
        Set(ByVal value As Integer)
            ppIntResultCnt = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【SQLカウント判断】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCount</returns>
    ''' <remarks><para>作成情報：2012/06/04 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropCount() As String
        Get
            Return ppCount
        End Get
        Set(ByVal value As String)
            ppCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：グループCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrGroupCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrGroupCD_Search() As String
        Get
            Return ppStrGroupCD_Search
        End Get
        Set(ByVal value As String)
            ppStrGroupCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：CI種別CD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCiKbnCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCiKbnCD_Search() As String
        Get
            Return ppStrCiKbnCD_Search
        End Get
        Set(ByVal value As String)
            ppStrCiKbnCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：種別CD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrKindCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrKindCD_Search() As String
        Get
            Return ppStrKindCD_Search
        End Get
        Set(ByVal value As String)
            ppStrKindCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNum_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrNum_Search() As String
        Get
            Return ppStrNum_Search
        End Get
        Set(ByVal value As String)
            ppStrNum_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：ステータスCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrStatusCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrStatusCD_Search() As String
        Get
            Return ppStrStatusCD_Search
        End Get
        Set(ByVal value As String)
            ppStrStatusCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：CIオーナーCD】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCiOwnerCD_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCiOwnerCD_Search() As String
        Get
            Return ppStrCiOwnerCD_Search
        End Get
        Set(ByVal value As String)
            ppStrCiOwnerCD_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：分類１】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrClass1_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrClass1_Search() As String
        Get
            Return ppStrClass1_Search
        End Get
        Set(ByVal value As String)
            ppStrClass1_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：分類２】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrClass2_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrClass2_Search() As String
        Get
            Return ppStrClass2_Search
        End Get
        Set(ByVal value As String)
            ppStrClass2_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：名称】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCINM_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrCINM_Search() As String
        Get
            Return ppStrCINM_Search
        End Get
        Set(ByVal value As String)
            ppStrCINM_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーワード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeWordAimai_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeWordAimai_Search() As String
        Get
            Return ppStrFreeWordAimai_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeWordAimai_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：最終更新日(FROM)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTFrom_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrUpdateDTFrom_Search() As String
        Get
            Return ppStrUpdateDTFrom_Search
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTFrom_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：最終更新日(TO)】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrUpdateDTTo_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrUpdateDTTo_Search() As String
        Get
            Return ppStrUpdateDTTo_Search
        End Get
        Set(ByVal value As String)
            ppStrUpdateDTTo_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrBikoAimai_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrBikoAimai_Search() As String
        Get
            Return ppStrBikoAimai_Search
        End Get
        Set(ByVal value As String)
            ppStrBikoAimai_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg1_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg1_Search() As String
        Get
            Return ppStrFreeFlg1_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg1_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg2_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg2_Search() As String
        Get
            Return ppStrFreeFlg2_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg2_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg3_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg3_Search() As String
        Get
            Return ppStrFreeFlg3_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg3_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg4_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg4_Search() As String
        Get
            Return ppStrFreeFlg4_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg4_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrFreeFlg5_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrFreeFlg5_Search() As String
        Get
            Return ppStrFreeFlg5_Search
        End Get
        Set(ByVal value As String)
            ppStrFreeFlg5_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索時条件：文書配付先】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrShareteamNM_Search</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropStrShareteamNM_Search() As String
        Get
            Return ppStrShareteamNM_Search
        End Get
        Set(ByVal value As String)
            ppStrShareteamNM_Search = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【その他ファンクション用パラメータ：選択行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSelectedRow</returns>
    ''' <remarks><para>作成情報：2012/07/18 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropIntSelectedRow() As Integer
        Get
            Return ppIntSelectedRow
        End Get
        Set(ByVal value As Integer)
            ppIntSelectedRow = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【出力ボタン活性／非活性判定用フラグ ※True:活性 False:非活性】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnEnabledFlg</returns>
    ''' <remarks><para>作成情報：2012/09/05 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnEnabledFlg() As Boolean
        Get
            Return ppBlnEnabledFlg
        End Get
        Set(ByVal value As Boolean)
            ppBlnEnabledFlg = value
        End Set
    End Property

End Class
