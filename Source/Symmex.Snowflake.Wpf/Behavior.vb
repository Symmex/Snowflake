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
            Throw New BehaviorException(Me, String.Format("The behavior '{0}' is already attached to the element {1}.", Me, _Element))
        End If

        _Element = element

        Me.OnAttached()
    End Sub

    Protected Overridable Sub OnAttached()
    End Sub

End Class