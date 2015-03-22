Public Class KeyUpEventBinding
    Inherits EventBinding

    Public Shared ReadOnly KeyProperty As DependencyProperty = DependencyProperty.Register("Key", GetType(Key?), GetType(KeyUpEventBinding))
    Public Property Key As Key?
        Get
            Return DirectCast(Me.GetValue(KeyProperty), Key?)
        End Get
        Set(value As Key?)
            Me.SetValue(KeyProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnAttached()
        Dim c = DirectCast(Me.Element, Control)
        AddHandler c.KeyUp, AddressOf Me.OnKeyUp
    End Sub

    Private Sub OnKeyUp(sender As Object, e As RoutedEventArgs)
        Dim args = DirectCast(e, KeyEventArgs)

        If Me.Key Is Nothing OrElse args.Key = Me.Key Then
            e.Handled = Me.Handled
            Me.ExecuteCommand()
        End If
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New KeyUpEventBinding()
    End Function

End Class