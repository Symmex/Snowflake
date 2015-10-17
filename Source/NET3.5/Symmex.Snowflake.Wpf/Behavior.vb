Public MustInherit Class Behavior
    Inherits FrameworkElement

    Private _Element As DependencyObject
    Public ReadOnly Property Element As DependencyObject
        Get
            Return _Element
        End Get
    End Property

    Protected Sub New()
    End Sub

    Friend Sub Attach(element As DependencyObject)
        If _Element IsNot Nothing Then
            Throw New InvalidOperationException("The behavior is already attached.")
        End If

        _Element = element

        Me.OnAttached()
    End Sub

    Protected Overridable Sub OnAttached()
    End Sub

End Class