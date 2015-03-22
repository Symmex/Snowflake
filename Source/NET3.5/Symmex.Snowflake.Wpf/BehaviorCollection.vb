Public Class BehaviorCollection
    Inherits FreezableCollection(Of Behavior)

    Public Sub Attach(element As DependencyObject)
        For Each child In Me
            child.Attach(element)
        Next
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New BehaviorCollection()
    End Function

End Class