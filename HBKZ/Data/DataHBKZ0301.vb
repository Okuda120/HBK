Imports FarPoint.Win.Spread

Public Class DataHBKZ0301

    'フォームオブジェクト
    Private ppTxtSearchGroupCD As TextBox           '検索グループＣＤ
    Private ppTxtSearchGroupName As TextBox         '検索グループ名
    Private ppLblCount As Label                     '件数ラベル
    Private ppVwList As SheetView                   '一覧シート

    'データ
    Private ppArgs As String                     '受け取った引数（検索条件）
    Private ppMode As String                     '受け取った引数（複数選択モード）
    Private ppSplitMode As String                    '受け取った引数（検索条件）
    Private ppTxtSearchStringArray As String()      '検索に用いる文字列配列
    Private ppDtGroupMasta As DataTable             '検索結果を格納するデータテーブル
    Private ppIntGroupCount As Integer              '検索結果件数

    ''' <summary>
    ''' プロパティセット【引数（検索条件）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppArgs</returns>
    ''' <remarks><para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropArgs() As String
        Get
            Return ppArgs
        End Get
        Set(ByVal value As String)
            ppArgs = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【引数（複数選択モード）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppMode</returns>
    ''' <remarks><para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropMode() As String
        Get
            Return ppMode
        End Get
        Set(ByVal value As String)
            ppMode = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【引数（検索条件）】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppMode</returns>
    ''' <remarks><para>作成情報：2012/06/01 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropSplitMode() As String
        Get
            Return ppSplitMode
        End Get
        Set(ByVal value As String)
            ppSplitMode = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【検索グループＣＤ】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtSearchGroupCD</returns>
    ''' <remarks><para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSearchGroupCD() As TextBox
        Get
            Return ppTxtSearchGroupCD
        End Get
        Set(ByVal value As TextBox)
            ppTxtSearchGroupCD = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【検索グループ名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtSearchGroupName</returns>
    ''' <remarks><para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSearchGroupName() As TextBox
        Get
            Return ppTxtSearchGroupName
        End Get
        Set(ByVal value As TextBox)
            ppTxtSearchGroupName = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【検索条件コレクション】
    ''' </summary>
    ''' <value></value>
    ''' <returns> colTxtSearchString</returns>
    ''' <remarks><para>作成情報：2012/05/31 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtSearchStringArray() As String()
        Get
            Return ppTxtSearchStringArray
        End Get
        Set(ByVal value As String())
            ppTxtSearchStringArray = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtGroupMasta</returns>
    ''' <remarks><para>作成情報：2012/06/01 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropDtGroupMasta() As DataTable
        Get
            Return ppDtGroupMasta
        End Get
        Set(ByVal value As DataTable)
            ppDtGroupMasta = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppIntGroupCount</returns>
    ''' <remarks><para>作成情報：2012/06/12 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntGroupCount() As Integer
        Get
            Return ppIntGroupCount
        End Get
        Set(ByVal value As Integer)
            ppIntGroupCount = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblCount</returns>
    ''' <remarks><para>作成情報：2012/06/15 matsuoka
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
    ''' <remarks><para>作成情報：2012/06/15 matsuoka
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

End Class
