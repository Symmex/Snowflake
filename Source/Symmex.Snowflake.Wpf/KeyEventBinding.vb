Public Class KeyEventBinding
    Inherits RoutedEventBinding

    Public Shared ReadOnly KeyProperty As DependencyProperty = DependencyProperty.Register("Key", GetType(Key?), GetType(KeyEventBinding))
    Public Property Key As Key?
        Get
            Return DirectCast(Me.GetValue(KeyProperty), Key?)
        End Get
        Set(value As Key?)
            Me.SetValue(KeyProperty, value)
        End Set
    End Property

    Public Shared ReadOnly ModifiersProperty As DependencyProperty = DependencyProperty.Register("Modifiers", GetType(ModifierKeys?), GetType(KeyEventBinding))
    Public Property Modifiers As ModifierKeys?
        Get
            Return DirectCast(Me.GetValue(ModifiersProperty), ModifierKeys?)
        End Get
        Set(value As ModifierKeys?)
            Me.SetValue(ModifiersProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnEventRaised(sender As Object, e As EventArgs)
        Dim args = DirectCast(e, KeyEventArgs)

        If Me.Key IsNot Nothing AndAlso args.Key <> Me.Key.Value Then
            Exit Sub
        End If

        If Me.Modifiers IsNot Nothing AndAlso Not (Keyboard.Modifiers And Me.Modifiers.Value) = Me.Modifiers.Value Then
            Exit Sub
        End If

        MyBase.OnEventRaised(sender, e)
    End Sub

End Class