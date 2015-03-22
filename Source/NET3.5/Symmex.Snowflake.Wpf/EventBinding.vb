Public MustInherit Class EventBinding
    Inherits CommandBehavior

    Public Shared ReadOnly HandledProperty As DependencyProperty = DependencyProperty.Register("Handled", GetType(Boolean), GetType(EventBinding))
    Public Property Handled As Boolean
        Get
            Return DirectCast(Me.GetValue(HandledProperty), Boolean)
        End Get
        Set(value As Boolean)
            Me.SetValue(HandledProperty, value)
        End Set
    End Property

    Protected Sub New()
    End Sub

End Class