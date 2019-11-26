Public Class MouseButtonEventBinding
    Inherits RoutedEventBinding

    Public Shared ReadOnly ChangedButtonProperty As DependencyProperty = DependencyProperty.Register("ChangedButton", GetType(MouseButton?), GetType(MouseDoubleClickEventBinding))
    Public Property ChangedButton As MouseButton?
        Get
            Return DirectCast(Me.GetValue(ChangedButtonProperty), MouseButton?)
        End Get
        Set(value As MouseButton?)
            Me.SetValue(ChangedButtonProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnEventRaised(sender As Object, e As EventArgs)
        Dim args = DirectCast(e, MouseButtonEventArgs)

        If Me.ChangedButton IsNot Nothing AndAlso args.ChangedButton <> Me.ChangedButton.Value Then
            Exit Sub
        End If

        MyBase.OnEventRaised(sender, e)
    End Sub

End Class