Imports FarPoint.Win.Spread

Public Class DataHBKZ0501
    Private ppBusyoArray As String()

    Private ppKyoku As TextBox   ' 局
    Private ppBusyo As TextBox   ' 部署
    Private ppRoom As TextBox    ' 番組／部屋
    Private ppBuilding As TextBox    ' 建物
    Private ppFloor As TextBox   ' フロア
    Private ppCount As Label        ' 件数

    Private ppSearchCount As Long   ' 検索結果件数

    Private ppVwList As FpSpread    ' 一覧

    Private ppArgs As String   ' 検索条件文字列
    Private ppMode As String   ' 選択条件
    Private ppSplitMode As String   ' 検索条件


    ''' <summary>
    ''' 検索条件文字列
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property PropArgs() As String
        Get
            Return ppArgs
        End Get
        Set(ByVal value As String)
            ppArgs = value
        End Set
    End Property

    ''' <summary>
    ''' 選択条件
    ''' </summary>
    ''' <value></value>
    ''' <remarks>0:単一行選択　1:複数行選択</remarks>
    Public Property PropMode() As String
        Get
            Return ppMode
        End Get
        Set(ByVal value As String)
            ppMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【初期検索用部署名】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBusyoArray</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropBusyoArray() As String()
        Get
            Return ppBusyoArray
        End Get
        Set(ByVal value As String())
            ppBusyoArray = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【局テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppKyoku</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropKyoku() As TextBox
        Get
            Return ppKyoku
        End Get
        Set(ByVal value As TextBox)
            ppKyoku = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【部署テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBusyo</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano</para>
    ''' <p>改訂情報：</p>
    ''' </remarks>
    Public Property PropBusyo() As TextBox
        Get
            Return ppBusyo
        End Get
        Set(ByVal value As TextBox)
            ppBusyo = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【番組／部屋テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppRoom</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropRoom() As TextBox
        Get
            Return ppRoom
        End Get
        Set(ByVal value As TextBox)
            ppRoom = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【建物テキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppBuilding</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropBuil() As TextBox
        Get
            Return ppBuilding
        End Get
        Set(ByVal value As TextBox)
            ppBuilding = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【フロアテキストボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppFloor</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropFloor() As TextBox
        Get
            Return ppFloor
        End Get
        Set(ByVal value As TextBox)
            ppFloor = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索モード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppSplitMode</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropSplitMode() As String
        Get
            Return ppSplitMode
        End Get
        Set(ByVal value As String)
            ppSplitMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【設置情報一覧スプレッド】
    ''' </summary>
    ''' <value></value>
    ''' <returns>作成情報：2012/06/14 f.nakano</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropVwList() As FpSpread
        Get
            Return ppVwList
        End Get
        Set(ByVal value As FpSpread)
            ppVwList = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppSearchCount</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropSearchCount() As Long
        Get
            Return ppSearchCount
        End Get
        Set(ByVal value As Long)
            ppSearchCount = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【件数ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCount</returns>
    ''' <remarks>
    ''' <para>作成情報：2012/06/14 f.nakano
    ''' <p>改訂情報：</p>
    ''' </para>
    ''' </remarks>
    Public Property PropCount() As Label
        Get
            Return ppCount
        End Get
        Set(ByVal value As Label)
            ppCount = value
        End Set
    End Property
End Class
