Imports System.Windows.Threading

Public Class BeginInvokeCommandBehavior
    Inherits CommandBehavior

    Public Shared ReadOnly DispatcherPriorityProperty As DependencyProperty = DependencyProperty.Register("DispatcherPriority", GetType(DispatcherPriority), GetType(BeginInvokeCommandBehavior), New FrameworkPropertyMetadata() With {.DefaultValue = DispatcherPriority.Normal})
    Public Property DispatcherPriority As DispatcherPriority
        Get
            Return DirectCast(Me.GetValue(DispatcherPriorityProperty), DispatcherPriority)
        End Get
        Set(value As DispatcherPriority)
            Me.SetValue(DispatcherPriorityProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnAttached()
        Dispatcher.BeginInvoke(New Action(AddressOf Me.ExecuteCommand), Me.DispatcherPriority, Nothing)
    End Sub

End Class