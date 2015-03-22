Public Class MouseLeftButtonDownEventBinding
    Inherits EventBinding

    Protected Overrides Sub OnAttached()
        Dim c = DirectCast(Me.Element, Control)
        c.AddHandler(UIElement.MouseLeftButtonDownEvent, New RoutedEventHandler(AddressOf Me.OnMouseLeftButtonDown), True)
    End Sub

    Private Sub OnMouseLeftButtonDown(sender As Object, e As RoutedEventArgs)
        e.Handled = Me.Handled
        Me.ExecuteCommand()
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New MouseLeftButtonDownEventBinding()
    End Function

End Class