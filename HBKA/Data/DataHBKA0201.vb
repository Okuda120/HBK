Public Class DataHBKA0201

    'フォームオブジェクト
    Private ppLblUserId As Label
    Private ppLblUserName As Label
    Private ppCmbGroup As ComboBox


    ''' <summary>
    ''' プロパティセット【ユーザーIDラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUserId</returns>
    ''' <remarks><para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUserId() As Label
        Get
            Return ppLblUserId
        End Get
        Set(ByVal value As Label)
            ppLblUserId = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【ユーザー指名ラベル】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppLblUserName</returns>
    ''' <remarks><para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropLblUserName() As Label
        Get
            Return ppLblUserName
        End Get
        Set(ByVal value As Label)
            ppLblUserName = value
        End Set
    End Property
    ''' <summary>
    ''' プロパティセット【グループコンボボックス】
    ''' </summary>
    ''' <value></value>
    ''' <returns>ppCmbGroup</returns>
    ''' <remarks><para>作成情報：2012/06/15 matsuoka
    ''' <p>改訂情報:</p>
    ''' </para></remarks>
    Public Property PropCmbGroup() As ComboBox
        Get
            Return ppCmbGroup
        End Get
        Set(ByVal value As ComboBox)
            ppCmbGroup = value
        End Set
    End Property

End Class
