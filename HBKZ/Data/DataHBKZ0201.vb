Imports FarPoint.Win.Spread

Public Class DataHBKZ0201
    'パラメータ変数宣言(検索条件)
    Private ppTxtBusyoName As Object
    Private ppTxtEndUsrId As Object
    Private ppTxtEndUsrNm As Object
    Private ppTxtEndUsrMail As Object

    'パラメータ変数宣言(検索結果)
    Private ppVwList As FpSpread

    'Private変数宣言(選択結果)
    Public ppSendList As Object

    Private ppArgs As String   ' 検索条件文字列
    Private ppMode As String   ' 選択条件
    Private ppSplitMode As String   ' 検索条件

    '検索用
    Private ppDtResultTable As DataTable
    Private ppSearchCount As Long   ' 検索結果件数
    Private ppCount As String

    ''' <summary>
    ''' プロパティセット【部署名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtBusyoName</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtBusyoName()
        Get
            Return ppTxtBusyoName
        End Get
        Set(ByVal value)
            ppTxtBusyoName = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エンドユーザーID】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtEndUsrId</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrId()
        Get
            Return ppTxtEndUsrId
        End Get
        Set(ByVal value)
            ppTxtEndUsrId = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【エンドユーザー氏名】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtEndUsrNm</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrNm()
        Get
            Return ppTxtEndUsrNm
        End Get
        Set(ByVal value)
            ppTxtEndUsrNm = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【メールアドレス】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppTxtTitle</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropTxtEndUsrMail()
        Get
            Return ppTxtEndUsrMail
        End Get
        Set(ByVal value)
            ppTxtEndUsrMail = value
        End Set
    End Property


    ''' <summary>
    ''' プロパティセット【検索結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppVwList</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
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
    ''' プロパティセット【選択結果】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppSendList</returns>
    ''' <remarks><para>作成情報：2012/05/28 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropSendList()
        Get
            Return ppSendList
        End Get
        Set(ByVal value)
            ppSendList = value
        End Set
    End Property

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
    ''' 検索条件
    ''' </summary>
    ''' <value></value>
    ''' <returns></returns>
    ''' <remarks>1:AND条件　2:OR条件　0:単一条件</remarks>
    Public Property PropSplitMode() As String
        Get
            Return ppSplitMode
        End Get
        Set(ByVal value As String)
            ppSplitMode = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索結果データテーブル】
    ''' </summary>
    ''' <value></value>
    ''' <returns> ppResultTable</returns>
    ''' <remarks><para>作成情報：2012/06/04 abe
    ''' <p>改訂情報:</p>
    ''' </para></remarks>

    Public Property PropDtResultTable() As DataTable
        Get
            Return ppdtResultTable
        End Get
        Set(ByVal value As DataTable)
            ppdtResultTable = value
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

End Class
