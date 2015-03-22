Public Class BehaviorManager

    Public Shared ReadOnly BehaviorsProperty As DependencyProperty = DependencyProperty.RegisterAttached("BehaviorsInternal", GetType(BehaviorCollection), GetType(BehaviorManager), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnBehaviorsChanged)))
    Public Shared Function GetBehaviors(d As DependencyObject) As BehaviorCollection
        Dim value = DirectCast(d.GetValue(BehaviorsProperty), BehaviorCollection)
        If value Is Nothing Then
            value = New BehaviorCollection()
            d.SetValue(BehaviorsProperty, value)
        End If

        Return value
    End Function

    Private Shared Sub OnBehaviorsChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim newValue = DirectCast(e.NewValue, BehaviorCollection)
        newValue.Attach(d)
    End Sub

    Public Shared ReadOnly BehaviorsSourceProperty As DependencyProperty = DependencyProperty.RegisterAttached("BehaviorsSource", GetType(BehaviorCollection), GetType(BehaviorManager), New PropertyMetadata(New PropertyChangedCallback(AddressOf OnBehaviorsSourceChanged)))

    Public Shared Function GetBehaviorsSource(d As DependencyObject) As BehaviorCollection
        Return DirectCast(d.GetValue(BehaviorsSourceProperty), BehaviorCollection)
    End Function

    Public Shared Sub SetBehaviorsSource(d As DependencyObject, value As BehaviorCollection)
        d.SetValue(BehaviorsSourceProperty, value)
    End Sub

    Private Shared Sub OnBehaviorsSourceChanged(d As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim source = DirectCast(e.NewValue, BehaviorCollection)

        If source IsNot Nothing Then
            Dim behaviors = GetBehaviors(d)

            For Each b In source
                Dim clone = DirectCast(b.Clone(), Behavior)
                behaviors.Add(clone)
            Next
        End If
    End Sub

End Class