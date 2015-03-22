Imports System.Windows.Controls.Primitives

Public Class SelectAllOnFocusBehavior
    Inherits Behavior

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New SelectAllOnFocusBehavior()
    End Function

    Protected Overrides Sub OnAttached()
        Dim fe = DirectCast(Me.Element, FrameworkElement)

        If TypeOf fe Is TextBoxBase Then
            AddHandler fe.GotFocus, AddressOf Me.OnTextBoxBaseGotFocus
        ElseIf TypeOf fe Is PasswordBox Then
            AddHandler fe.GotFocus, AddressOf Me.OnPasswordBoxGotFocus
        ElseIf TypeOf fe Is ComboBox Then
            AddHandler fe.GotFocus, AddressOf Me.OnComboBoxGotFocus
        End If
    End Sub

    Private Sub OnTextBoxBaseGotFocus(sender As Object, e As RoutedEventArgs)
        DirectCast(sender, TextBoxBase).SelectAll()
    End Sub

    Private Sub OnPasswordBoxGotFocus(sender As Object, e As RoutedEventArgs)
        DirectCast(sender, PasswordBox).SelectAll()
    End Sub

    Private Sub OnComboBoxGotFocus(sender As Object, e As RoutedEventArgs)
        Dim cb = DirectCast(sender, ComboBox)
        If cb.IsEditable Then
            Dim tb = TryCast(cb.FindName("PART_EditableTextBox"), TextBox)
            If tb IsNot Nothing Then
                tb.SelectAll()
            End If
        End If
    End Sub

End Class