Imports System.Windows.Controls.Primitives

Public Class ClickEventBinding
    Inherits EventBinding

    Protected Overrides Sub OnAttached()
        Dim c = DirectCast(Me.Element, Control)
        c.AddHandler(ButtonBase.ClickEvent, New RoutedEventHandler(AddressOf Me.OnClick), True)
    End Sub

    Private Sub OnClick(sender As Object, e As RoutedEventArgs)
        Me.ExecuteCommand()
    End Sub

End Class