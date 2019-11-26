Imports System.Runtime.CompilerServices

Public Module DataObjectExtensions

    <Extension()>
    Public Function IsChanged(items As IEnumerable(Of ISelfTrackingObject)) As Boolean
        Return items.Any(Function(child) child.IsChanged)
    End Function

    <Extension()>
    Public Function IsSaveRequired(items As IEnumerable(Of ISelfTrackingObject)) As Boolean
        Return items.Any(Function(child) child.IsSaveRequired)
    End Function

    <Extension()>
    Public Sub ResetState(items As IEnumerable(Of ISelfTrackingObject))
        For Each item In items
            item.ResetState()
        Next
    End Sub

End Module