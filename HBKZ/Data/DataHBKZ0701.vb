Imports FarPoint.Win.Spread

''' <summary>
''' 機器検索一覧画面Dataクラス
''' </summary>
''' <remarks>機器検索一覧画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/07/06 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKZ0701

    '前画面からのパラメータ
    Private ppStrMode As String                     'パラメータ：選択モード
    Private ppStrCIKbnCD As String                  'パラメータ：CI種別コード
    Private ppStrCIStatusCD As String               'パラメータ：CIステータスコード
    Private ppStrWorkCD As String                   'パラメータ：作業コード

    'フォームオブジェクト
    Private ppCmbKind As ComboBox                   '種別コンボボックス
    Private ppTxtNum As TextBox                     '番号テキストボックス
    Private ppCmbCIStatus As ComboBox               'CIステータスコンボボックス
    Private ppTxtCINM As TextBox                    '名称（機種）テキストボックス
    Private ppLblCount As Label                     '件数ラベル
    Private ppVwList As SheetView                   '一覧シート
    Private ppBtnAllCheck As Button                 '全選択ボタン
    Private ppBtnAllUnCheck As Button               '全解除ボタン

    'データ
    Private ppStrAryCIKbnCD As String()             'CI種別コード配列
    Private ppStrAryCIStatusCD As String()          'CIステータスコード配列
    Private ppDtKiki As DataTable                   '検索結果を格納するデータテーブル
    Private ppIntKikiCount As Integer               '検索結果件数
    Private ppDtKindMasta As DataTable              'コンボボックス用：種別マスタデータ
    Private ppDtCIStatusMasta As DataTable          'コンボボックス用：CIステータスマスタデータ

    '入力チェック
    Private ppIntCheckIndex As Integer()            'チェック行番号配列

    'SQL
    Private ppStrSearchKikiKbn As String            '検索機器区分（1：サポセン、2：部所有、3：サポセン／部所有）
    Private ppStrWhere As String                    '検索用SQLのWhere句


    ''' <summary>
    ''' プロパティセット【パラメータ：選択モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrMode</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrMode() As String
        Get
            Return ppStrMode
        End Get
        Set(ByVal value As String)
            ppStrMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【パラメータ：CI種別コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIKbnCD() As String
        Get
            Return ppStrCIKbnCD
        End Get
        Set(ByVal value As String)
            ppStrCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【パラメータ：CIステータスコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrCIStatusCD() As String
        Get
            Return ppStrCIStatusCD
        End Get
        Set(ByVal value As String)
            ppStrCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【パラメータ：作業コード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkCD() As String
        Get
            Return ppStrWorkCD
        End Get
        Set(ByVal value As String)
            ppStrWorkCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbKind</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbKind() As ComboBox
        Get
            Return ppCmbKind
        End Get
        Set(ByVal value As ComboBox)
            ppCmbKind = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番号テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtNum</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
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
    ''' プロパティセット【CIステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbCIStatus</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbCIStatus() As ComboBox
        Get
            Return ppCmbCIStatus
        End Get
        Set(ByVal value As ComboBox)
            ppCmbCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名称（機種）テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtCINM</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINM() As TextBox
        Get
            Return ppTxtCINM
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
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
    ''' プロパティセット【一覧シート】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppVwList</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwList() As SheetView
        Get
            Return ppVwList
        End Get
        Set(ByVal value As SheetView)
            ppVwList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全選択ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAllCheck</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllCheck() As Button
        Get
            Return ppBtnAllCheck
        End Get
        Set(ByVal value As Button)
            ppBtnAllCheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【全解除ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnAllUnCheck</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnAllUnCheck() As Button
        Get
            Return ppBtnAllUnCheck
        End Get
        Set(ByVal value As Button)
            ppBtnAllUnCheck = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CI種別コード配列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrAryCIKbnCD</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrAryCIKbnCD() As String()
        Get
            Return ppStrAryCIKbnCD
        End Get
        Set(ByVal value As String())
            ppStrAryCIKbnCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIステータスコード配列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrAryCIStatusCD</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrAryCIStatusCD() As String()
        Get
            Return ppStrAryCIStatusCD
        End Get
        Set(ByVal value As String())
            ppStrAryCIStatusCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKiki</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKiki() As DataTable
        Get
            Return ppDtKiki
        End Get
        Set(ByVal value As DataTable)
            ppDtKiki = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntKikiCount</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntKikiCount() As Integer
        Get
            Return ppIntKikiCount
        End Get
        Set(ByVal value As Integer)
            ppIntKikiCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtKindMasta() As DataTable
        Get
            Return ppDtKindMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtKindMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：CIステータスマスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtCIStatusMasta</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatusMasta() As DataTable
        Get
            Return ppDtCIStatusMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatusMasta = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック行番号配列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCheckIndex</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCheckIndex() As Integer()
        Get
            Return ppIntCheckIndex
        End Get
        Set(ByVal value As Integer())
            ppIntCheckIndex = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索機器区分（1：サポセン、2：部所有、3：サポセン／部所有）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrSearchKikiKbn</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrSearchKikiKbn() As String
        Get
            Return ppStrSearchKikiKbn
        End Get
        Set(ByVal value As String)
            ppStrSearchKikiKbn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索用SQLのWhere句】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWhere</returns>
    ''' <remarks><para>作成情報：2012/07/06 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWhere() As String
        Get
            Return ppStrWhere
        End Get
        Set(ByVal value As String)
            ppStrWhere = value
        End Set
    End Property

End Class
