Public MustInherit Class Behavior
    Inherits Freezable

    Private _Element As DependencyObject
    Public ReadOnly Property Element As DependencyObject
        Get
            Return _Element
        End Get
    End Property

    Protected Sub New()
    End Sub

    Friend Sub Attach(element As DependencyObject)
        If Not element Is _Element Then
            If _Element IsNot Nothing Then
                Throw New InvalidOperationException("The behavior is already attached to another element.")
            End If

            _Element = element
            Me.OnAttached()
        End If
    End Sub

    Protected Overridable Sub OnAttached()
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return DirectCast(Activator.CreateInstance(Me.GetType()), Freezable)
    End Function

End Class