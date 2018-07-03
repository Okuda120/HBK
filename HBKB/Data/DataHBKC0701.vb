Imports FarPoint.Win.Spread

''' <summary>
''' セット選択画面Dataクラス
''' </summary>
''' <remarks>セット選択画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/09/19 t.fukuo
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKC0701

    '前画面からのパラメータ
    Private ppStrWorkCD As String                   'パラメータ：作業コード
    Private ppStrWorkNM As String                   'パラメータ：作業名

    'フォームオブジェクト
    Private ppCmbKind As ComboBox                   '種別コンボボックス
    Private ppTxtNum As TextBox                     '番号テキストボックス

    Private ppLblCount As Label                     '件数ラベル
    Private ppVwList As SheetView                   '一覧シート
    Private ppBtnSelect As Button                   '決定ボタン

    'データ
    Private ppDtKiki As DataTable                   '検索結果を格納するテーブル
    Private ppIntKikiCount As Integer               '検索結果件数（全件数）
    Private ppIntSetCnt As Integer                  'セットまたは個別機器件数
    Private ppDtKindMasta As DataTable              'コンボボックス用：種別マスタデータ
    Private ppDtReturn As DataTable                 '選択データを格納するテーブル

    'SQL
    Private ppStrWhere1 As String                    '検索用SQLのWhere句1
    Private ppStrWhere2 As String                    '検索用SQLのWhere句2

    'その他ファンクション用パラメータ
    Private ppKeyCode As System.Windows.Forms.Keys  '押下キーコード
    Private ppIntCheckIndex As Integer              'チェック行番号
    Private ppBlnSelected As Boolean                '選択フラグ


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
    ''' プロパティセット【パラメータ：作業名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWorkNM</returns>
    ''' <remarks><para>作成情報：2012/07/30 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWorkNM() As String
        Get
            Return ppStrWorkNM
        End Get
        Set(ByVal value As String)
            ppStrWorkNM = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【種別コンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbKind</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' プロパティセット【決定ボタン】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBtnSelect</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBtnSelect() As Button
        Get
            Return ppBtnSelect
        End Get
        Set(ByVal value As Button)
            ppBtnSelect = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果を格納するデータテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKiki</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' プロパティセット【検索結果件数（全件数）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntKikiCount</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' プロパティセット【検索結果件数（セットまたは個別機器件数）】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntSetCnt</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntSetCnt() As Integer
        Get
            Return ppIntSetCnt
        End Get
        Set(ByVal value As Integer)
            ppIntSetCnt = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【コンボボックス用：種別マスタデータ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtKindMasta</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
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
    ''' プロパティセット【戻り値用：選択データ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtReturn</returns>
    ''' <remarks><para>作成情報：2012/09/24 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtReturn() As DataTable
        Get
            Return ppDtReturn
        End Get
        Set(ByVal value As DataTable)
            ppDtReturn = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索用SQLのWhere句1】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWhere1</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWhere1() As String
        Get
            Return ppStrWhere1
        End Get
        Set(ByVal value As String)
            ppStrWhere1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索用SQLのWhere句2】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrWhere2</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrWhere2() As String
        Get
            Return ppStrWhere2
        End Get
        Set(ByVal value As String)
            ppStrWhere2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【押下キーコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppKeyCode</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropKeyCode() As System.Windows.Forms.Keys
        Get
            Return ppKeyCode
        End Get
        Set(ByVal value As System.Windows.Forms.Keys)
            ppKeyCode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCheckIndex</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntCheckIndex() As Integer
        Get
            Return ppIntCheckIndex
        End Get
        Set(ByVal value As Integer)
            ppIntCheckIndex = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック行番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBlnSelected</returns>
    ''' <remarks><para>作成情報：2012/09/19 t.fukuo
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropBlnSelected() As Boolean
        Get
            Return ppBlnSelected
        End Get
        Set(ByVal value As Boolean)
            ppBlnSelected = value
        End Set
    End Property

End Class
