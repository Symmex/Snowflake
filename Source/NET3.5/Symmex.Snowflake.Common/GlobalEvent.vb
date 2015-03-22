Public Class EventBroker(Of T As EventArgs)

    Private _Handlers As WeakReferenceManager(Of EventHandler(Of T))

    Public Sub New()
        _Handlers = New WeakReferenceManager(Of EventHandler(Of T))()
    End Sub

    Public Sub [AddHandler](ByVal handler As EventHandler(Of T))
        _Handlers.Add(handler)
    End Sub

    Public Sub [RemoveHandler](ByVal handler As EventHandler(Of T))
        _Handlers.Remove(handler)
    End Sub

    Public Sub [RaiseEvent](ByVal sender As Object, ByVal e As T)
        Dim handlers = _Handlers.GetAliveItems()
        For Each handler In handlers
            handler.Invoke(sender, e)
        Next
    End Sub

End Class