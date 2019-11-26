Imports System.Windows.Threading

Public Class LoadedEventBinding
    Inherits EventBinding

    Public Sub New()
    End Sub

    Protected Overrides Sub OnAttached()
        AddHandler DirectCast(Me.Element, FrameworkElement).Loaded, AddressOf Me.OnLoaded
    End Sub

    Private Sub OnLoaded(sender As Object, e As EventArgs)
        Dispatcher.BeginInvoke(New Action(AddressOf Me.ExecuteCommand), DispatcherPriority.ContextIdle, Nothing)
    End Sub

End Class