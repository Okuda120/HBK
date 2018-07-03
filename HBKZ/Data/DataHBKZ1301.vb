Imports FarPoint.Win.Spread

''' <summary>
''' 対象システム検索一覧画面Dataクラス
''' </summary>
''' <remarks>対象システム検索一覧画面で使用するプロパティのセットを行う
''' <para>作成情報：2012/10/23 s.yamaguchi
''' <p>改訂情報:</p>
''' </para></remarks>
Public Class DataHBKZ1301

    'フォームオブジェクト
    Private ppCmbStatus As ComboBox                 'CIステータスコンボボックス
    Private ppTxtClass1 As TextBox                  '分類1テキストボックス
    Private ppTxtClass2 As TextBox                  '分類2テキストボックス
    Private ppTxtCINm As TextBox                    '名称テキストボックス
    Private ppTxtFreeText As TextBox                'フリーテキストテキストボックス
    Private ppCmbFreeFlg1 As ComboBox               'フリーフラグ1
    Private ppCmbFreeFlg2 As ComboBox               'フリーフラグ2
    Private ppCmbFreeFlg3 As ComboBox               'フリーフラグ3
    Private ppCmbFreeFlg4 As ComboBox               'フリーフラグ4
    Private ppCmbFreeFlg5 As ComboBox               'フリーフラグ5
    Private ppLblCount As Label                     '件数ラベル
    Private ppVwList As FpSpread                    '検索結果一覧スプレッド

    'データ
    Private ppAryFreeText As String()               '検索条件：フリーテキスト
    Private ppDtTaisyouSystem As DataTable          '検索結果用データテーブル
    Private ppIntTaisyouSystemCount As Integer      '検索結果件数
    Private ppDtCIStatus As DataTable               'CIステータスマスタ用データテーブル

    '入力チェック
    Private ppIntCheckIndex As Integer()            'チェック行番号配列

    'SQL
    Private ppStrWhere As String                    '検索用SQLのWhere句

    ''' <summary>
    ''' プロパティセット【CIステータスコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbStatus</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbStatus() As ComboBox
        Get
            Return ppCmbStatus
        End Get
        Set(ByVal value As ComboBox)
            ppCmbStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類1テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtClass1</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass1() As TextBox
        Get
            Return ppTxtClass1
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass1 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【分類2テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtClass2</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtClass2() As TextBox
        Get
            Return ppTxtClass2
        End Get
        Set(ByVal value As TextBox)
            ppTxtClass2 = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【名称テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtCINm</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtCINm() As TextBox
        Get
            Return ppTxtCINm
        End Get
        Set(ByVal value As TextBox)
            ppTxtCINm = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フリーテキストテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtFreeText</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【フリーフラグ1】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbFreeFlg1</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【フリーフラグ2】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbFreeFlg2</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【フリーフラグ3】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbFreeFlg3</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【フリーフラグ4】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbFreeFlg4</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【フリーフラグ5】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppCmbFreeFlg5</returns>
    ''' <remarks><para>作成情報：2012/10/25 s.yamaguchi
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
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
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
    ''' プロパティセット【検索結果一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwList</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropVwList() As FpSpread
        Get
            Return ppVwList
        End Get
        Set(ByVal value As FpSpread)
            ppVwList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：フリーテキスト】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppAryFreeText</returns>
    ''' <remarks><para>作成情報：2012/10/26 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropAryFreeText() As String()
        Get
            Return ppAryFreeText
        End Get
        Set(ByVal value As String())
            ppAryFreeText = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果用データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtTaisyouSystem</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtTaisyouSystem() As DataTable
        Get
            Return ppDtTaisyouSystem
        End Get
        Set(ByVal value As DataTable)
            ppDtTaisyouSystem = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppIntTaisyouSystemCount</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntTaisyouSystemCount() As Integer
        Get
            Return ppIntTaisyouSystemCount
        End Get
        Set(ByVal value As Integer)
            ppIntTaisyouSystemCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【CIステータスマスタ用データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppDtCIStatus</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtCIStatus() As DataTable
        Get
            Return ppDtCIStatus
        End Get
        Set(ByVal value As DataTable)
            ppDtCIStatus = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【チェック行番号配列】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntCheckIndex</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
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
    ''' プロパティセット【検索用SQLのWhere句】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppStrWhere</returns>
    ''' <remarks><para>作成情報：2012/10/23 s.yamaguchi
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
