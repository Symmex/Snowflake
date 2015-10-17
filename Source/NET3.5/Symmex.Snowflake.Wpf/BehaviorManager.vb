Public Class BehaviorManager

    Public Shared ReadOnly BehaviorsProperty As DependencyProperty = DependencyProperty.RegisterAttached("BehaviorsPrivate", GetType(BehaviorCollection), GetType(BehaviorManager))

    Public Shared Function GetBehaviors(d As DependencyObject) As BehaviorCollection
        Dim value = DirectCast(d.GetValue(BehaviorsProperty), BehaviorCollection)
        If value Is Nothing Then
            value = New BehaviorCollection()
            d.SetValue(BehaviorsProperty, value)
            value.Attach(d)
        End If

        Return value
    End Function

    Public Shared ReadOnly MergedBehaviorsProperty As DependencyProperty = DependencyProperty.RegisterAttached("MergedBehaviors", GetType(BehaviorCollection), GetType(BehaviorManager), New PropertyMetadata() With {.PropertyChangedCallback = New PropertyChangedCallback(AddressOf OnMergedBehaviorsChanged)})

    Public Shared Function GetMergedBehaviors(d As DependencyObject) As BehaviorCollection
        Return DirectCast(d.GetValue(MergedBehaviorsProperty), BehaviorCollection)
    End Function

    Public Shared Sub SetMergedBehaviors(d As DependencyObject, value As BehaviorCollection)
        d.SetValue(MergedBehaviorsProperty, value)
    End Sub

    Private Shared Sub OnMergedBehaviorsChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        If e.NewValue Is Nothing Then
            Exit Sub
        End If

        Dim newValue = DirectCast(e.NewValue, BehaviorCollection)
        Dim b = GetBehaviors(d)
        b.Merge(newValue)
    End Sub

End Class