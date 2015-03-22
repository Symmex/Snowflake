Public Class PreviewKeyDownEventBinding
    Inherits EventBinding

    Public Shared ReadOnly KeyProperty As DependencyProperty = DependencyProperty.Register("Key", GetType(Key?), GetType(PreviewKeyDownEventBinding))
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
        AddHandler c.PreviewKeyDown, AddressOf Me.OnPreviewKeyDown
    End Sub

    Private Sub OnPreviewKeyDown(sender As Object, e As RoutedEventArgs)
        Dim args = DirectCast(e, KeyEventArgs)

        If Me.Key Is Nothing OrElse args.Key = Me.Key Then
            e.Handled = Me.Handled
            Me.ExecuteCommand()
        End If
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New KeyDownEventBinding()
    End Function

End Class