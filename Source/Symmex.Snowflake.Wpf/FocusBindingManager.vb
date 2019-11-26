Public Class FocusBindingManager

    Public Shared ReadOnly FocusBindingProperty As DependencyProperty = DependencyProperty.RegisterAttached("FocusBinding", GetType(FocusBinding), GetType(FocusBindingManager), New FrameworkPropertyMetadata() With {.PropertyChangedCallback = AddressOf FocusBindingChanged})
    Public Shared Function GetFocusBinding(ByVal element As DependencyObject) As FocusBinding
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        Return DirectCast(element.GetValue(FocusBindingProperty), FocusBinding)
    End Function

    Public Shared Sub SetFocusBinding(ByVal element As DependencyObject, ByVal value As FocusBinding)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        element.SetValue(FocusBindingProperty, value)
    End Sub

    Private Shared Sub FocusBindingChanged(ByVal d As DependencyObject, ByVal e As DependencyPropertyChangedEventArgs)
        Dim value = TryCast(e.NewValue, FocusBinding)
        If value IsNot Nothing Then
            value.Element = TryCast(d, FrameworkElement)
        End If
    End Sub

End Class