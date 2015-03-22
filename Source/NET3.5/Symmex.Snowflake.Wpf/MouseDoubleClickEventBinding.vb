Public Class MouseDoubleClickEventBinding
    Inherits EventBinding

    Public Shared ReadOnly ChangedButtonProperty As DependencyProperty = DependencyProperty.Register("ChangedButton", GetType(MouseButton?), GetType(MouseDoubleClickEventBinding))
    Public Property ChangedButton As MouseButton?
        Get
            Return DirectCast(Me.GetValue(ChangedButtonProperty), MouseButton?)
        End Get
        Set(value As MouseButton?)
            Me.SetValue(ChangedButtonProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnAttached()
        Dim c = DirectCast(Me.Element, Control)
        c.AddHandler(Control.MouseDoubleClickEvent, New RoutedEventHandler(AddressOf Me.OnMouseDoubleClick), True)
    End Sub

    Private Sub OnMouseDoubleClick(sender As Object, e As RoutedEventArgs)
        Dim args = DirectCast(e, MouseButtonEventArgs)
        Dim change = Me.ChangedButton

        If change Is Nothing OrElse args.ChangedButton = change.Value Then
            e.Handled = Me.Handled
            Me.ExecuteCommand()
        End If
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New MouseDoubleClickEventBinding()
    End Function

End Class