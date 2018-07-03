Imports CommonHBK
Public Class DataHBKA0301
    ' フォームオブジェクト
    Private ppCmbClassCD As ComboBox
    Private ppTxtNumberCD As TextBox
    Private ppGrpLoginUser As GroupControlEx                'ログイン：ログイン情報グループボックス

    ' 検索用
    Private ppStrLoginUserGrp As String
    Private ppStrLoginUserId As String
    Private ppStrClassCD As String
    Private ppIntMngNum As Integer

    ' データテーブル
    Private ppDtResultCount As DataTable


    ''' <summary>
    ''' プロパティセット【フォームオブジェクト】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbClassCD</returns>
    ''' <remarks><para>作成情報：2017/08/25 e.okuda
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
    ''' <returns>ppTxtNumberCD</returns>
    ''' <remarks><para>作成情報：2017/08/25 e.okuda
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
    ''' プロパティセット【ログイン：ログイン情報グループボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppGrpLoginUser</returns>
    ''' <remarks><para>作成情報：2017/08/28 e.okuda
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
    ''' プロパティセット【検索条件：クラスコード】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrClassCD</returns>
    ''' <remarks><para>作成情報：2017/08/25 e.okuda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropStrClassCD() As String
        Get
            Return ppStrClassCD
        End Get
        Set(ByVal value As String)
            ppStrClassCD = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【検索条件：管理番号】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrNumberCD</returns>
    ''' <remarks><para>作成情報：2017/08/25 e.okuda
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropIntMngNum() As Integer
        Get
            Return ppIntMngNum
        End Get
        Set(ByVal value As Integer)
            ppIntMngNum = value
        End Set
    End Property

    ''' <summary>
    ''' プロパティセット【データテーブル:検索件数】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppDtResultCount</returns>
    ''' <remarks><para>作成情報：2018/08/26 e.okuda
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
    ''' プロパティセット【クイックアクセス検索前提条件：ログインユーザ所属グループ】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserGrp</returns>
    ''' <remarks><para>作成情報：2017/08/28 e.okuda
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
    ''' プロパティセット【クイックアクセス検索前提条件：ログインユーザID】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppStrLoginUserId</returns>
    ''' <remarks><para>作成情報：2017/08/28 e.okuda
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


End Class
