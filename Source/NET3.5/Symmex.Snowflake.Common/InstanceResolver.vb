Public Class InstanceResolver(Of T)
    Inherits Resolver(Of T)

    Private _Instance As T

    Public Sub New(ByVal instance As T)
        _Instance = instance
    End Sub

    Public Overrides Function Resolve() As T
        Return _Instance
    End Function

End Class