Public Class PreviewMouseLeftButtonUpEventBinding
    Inherits EventBinding

    Protected Overrides Sub OnAttached()
        Dim e = DirectCast(Me.Element, UIElement)
        AddHandler e.PreviewMouseLeftButtonUp, AddressOf Me.OnPreviewMouseLeftButtonUp
    End Sub

    Private Sub OnPreviewMouseLeftButtonUp(sender As Object, e As MouseButtonEventArgs)
        e.Handled = Me.Handled
        Me.ExecuteCommand()
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New PreviewMouseLeftButtonUpEventBinding()
    End Function

End Class
