Public Class PasswordBinding
    Inherits Behavior

    Public Shared ReadOnly PasswordProperty As DependencyProperty = DependencyProperty.Register("Password", GetType(String), GetType(PasswordBinding), New FrameworkPropertyMetadata(String.Empty, FrameworkPropertyMetadataOptions.BindsTwoWayByDefault, New PropertyChangedCallback(AddressOf OnPasswordChanged)))
    Public Property Password As String
        Get
            Return DirectCast(Me.GetValue(PasswordProperty), String)
        End Get
        Set(value As String)
            Me.SetValue(PasswordProperty, value)
        End Set
    End Property

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New PasswordBinding()
    End Function

    Protected Overrides Sub OnAttached()
        Dim pb = DirectCast(Me.Element, PasswordBox)
        AddHandler pb.PasswordChanged, AddressOf Me.OnPasswordBoxPasswordChanged
    End Sub

    Private Sub OnPasswordBoxPasswordChanged(sender As Object, e As RoutedEventArgs)
        Me.Password = DirectCast(sender, PasswordBox).Password
    End Sub

    Private Shared Sub OnPasswordChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim item = DirectCast(d, PasswordBinding)
        Dim box = DirectCast(item.Element, PasswordBox)
        If box.Password <> item.Password Then
            box.Password = item.Password
        End If
    End Sub

End Class