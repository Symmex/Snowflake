Public Class AccessKeyScope

    Public Shared ReadOnly IsScopeProperty As DependencyProperty = DependencyProperty.RegisterAttached("IsScope", GetType(Boolean), GetType(AccessKeyScope), New PropertyMetadata() With {.PropertyChangedCallback = New PropertyChangedCallback(AddressOf OnIsScopeChanged)})
    Public Shared Function GetIsScope(obj As DependencyObject) As Boolean
        Return DirectCast(obj.GetValue(IsScopeProperty), Boolean)
    End Function
    Public Shared Sub SetIsScope(obj As DependencyObject, value As Boolean)
        obj.SetValue(IsScopeProperty, value)
    End Sub

    Private Shared Sub OnIsScopeChanged(obj As DependencyObject, e As DependencyPropertyChangedEventArgs)
        Dim newValue = DirectCast(e.NewValue, Boolean)

        If newValue Then
            AccessKeyManager.AddAccessKeyPressedHandler(obj, New AccessKeyPressedEventHandler(AddressOf OnAccessKeyPressed))
        Else
            AccessKeyManager.RemoveAccessKeyPressedHandler(obj, New AccessKeyPressedEventHandler(AddressOf OnAccessKeyPressed))
        End If
    End Sub

    Private Shared Sub OnAccessKeyPressed(sender As Object, e As AccessKeyPressedEventArgs)
        Dim focusedElement = Keyboard.FocusedElement

        If focusedElement Is Nothing Then
            Exit Sub
        End If

        If focusedElement Is sender Then
            Exit Sub
        End If

        Dim currentElement = DirectCast(focusedElement, DependencyObject)

        While currentElement IsNot Nothing
            Dim childCount = VisualTreeHelper.GetChildrenCount(currentElement)

            For i = 0 To childCount - 1
                If VisualTreeHelper.GetChild(currentElement, i) Is sender Then
                    Exit Sub
                End If
            Next

            If childCount > 1 Then
                e.Scope = sender
                Exit Sub
            ElseIf childCount = 1 Then
                currentElement = VisualTreeHelper.GetChild(currentElement, 0)
            Else
                e.Scope = sender
                Exit Sub
            End If
        End While
    End Sub

End Class