Public Class ObjectManagerPath

    Private _Elements As Object()
    Public ReadOnly Property Elements As Object()
        Get
            Return _Elements
        End Get
    End Property

    Public Sub New(ParamArray path As Object())
        _Elements = path
    End Sub

End Class