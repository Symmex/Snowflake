Imports System.ComponentModel

Public Class WindowClosingEventBinding
    Inherits EventBinding

    Public Shared ReadOnly CancelProperty As DependencyProperty = DependencyProperty.Register("Cancel", GetType(Boolean), GetType(WindowClosingEventBinding))
    Public Property Cancel As Boolean
        Get
            Return DirectCast(Me.GetValue(CancelProperty), Boolean)
        End Get
        Set(value As Boolean)
            Me.SetValue(CancelProperty, value)
        End Set
    End Property

    Public Sub New()
    End Sub

    Protected Overrides Sub OnAttached()
        Dim w = DirectCast(Me.Element, Window)
        AddHandler w.Closing, AddressOf Me.OnClosing
    End Sub

    Private Sub OnClosing(sender As Object, e As CancelEventArgs)
        e.Cancel = Me.Cancel
        Me.ExecuteCommand(e)
    End Sub

End Class