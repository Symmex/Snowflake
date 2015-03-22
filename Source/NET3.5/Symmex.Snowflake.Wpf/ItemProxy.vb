Public Class ItemProxy
    Inherits Freezable

    Public Shared ReadOnly ItemProperty As DependencyProperty = DependencyProperty.Register("Item", GetType(Object), GetType(ItemProxy))
    Public Property Item As Object
        Get
            Return Me.GetValue(ItemProxy.ItemProperty)
        End Get
        Set(ByVal value As Object)
            Me.SetValue(ItemProxy.ItemProperty, value)
        End Set
    End Property

    Protected Overrides Function CreateInstanceCore() As Freezable
        Throw New NotImplementedException()
    End Function

End Class