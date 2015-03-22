Public Class PreviewMouseLeftButtonDownEventBinding
    Inherits EventBinding

    Protected Overrides Sub OnAttached()
        Dim e = DirectCast(Me.Element, UIElement)
        AddHandler e.PreviewMouseLeftButtonDown, AddressOf Me.OnPreviewMouseLeftButtonDown
    End Sub

    Private Sub OnPreviewMouseLeftButtonDown(sender As Object, e As MouseButtonEventArgs)
        e.Handled = Me.Handled
        Me.ExecuteCommand()
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New PreviewMouseLeftButtonDownEventBinding()
    End Function

End Class