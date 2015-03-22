Public Class MouseLeftButtonUpEventBinding
    Inherits EventBinding

    Protected Overrides Sub OnAttached()
        Dim c = DirectCast(Me.Element, Control)
        c.AddHandler(UIElement.MouseLeftButtonUpEvent, New RoutedEventHandler(AddressOf Me.OnMouseLeftButtonUp), True)
    End Sub

    Private Sub OnMouseLeftButtonUp(sender As Object, e As RoutedEventArgs)
        e.Handled = Me.Handled
        Me.ExecuteCommand()
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New MouseLeftButtonUpEventBinding()
    End Function

End Class