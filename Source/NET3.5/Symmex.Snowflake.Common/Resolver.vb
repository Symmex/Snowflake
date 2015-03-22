Public MustInherit Class Resolver(Of T)
    Implements IResolver(Of T)

    Protected Sub New()
    End Sub

    Private Function IResolver_Resolve() As Object Implements IResolver.Resolve
        Return Me.Resolve()
    End Function

    Public MustOverride Overloads Function Resolve() As T Implements IResolver(Of T).Resolve

End Class