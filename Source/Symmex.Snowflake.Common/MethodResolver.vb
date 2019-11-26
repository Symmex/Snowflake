Public Class MethodResolver(Of T)
    Inherits Resolver(Of T)

    Private _Method As Func(Of T)

    Public Sub New(ByVal method As Func(Of T))
        _Method = method
    End Sub

    Public Overrides Function Resolve() As T
        Return _Method.Invoke()
    End Function

End Class