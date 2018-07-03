Public Class BaseSearchForm
    Private ppArgs As String   ' 引数1
    Private ppMode As String   ' 引数2

    ''' <summary>
    ''' 引数1
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
    ''' 引数2
    ''' </summary>
    ''' <value></value>
    ''' <remarks></remarks>
    Public Property PropMode() As String
        Get
            Return ppMode
        End Get
        Set(ByVal value As String)
            ppMode = value
        End Set
    End Property
End Class