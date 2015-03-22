Imports System.Runtime.CompilerServices

Public Module ListExtensions

    <Extension()>
    Function FindIndex(Of T)(items As IList(Of T), predicate As Predicate(Of T)) As Integer
        For index = 0 To items.Count - 1
            Dim item = items(index)
            If predicate.Invoke(item) Then
                Return index
            End If
        Next

        Return -1
    End Function

    <Extension()>
    Sub RemoveAll(Of T)(items As IList(Of T), predicate As Predicate(Of T))
        Dim itemsToRemove = (From item In items Where predicate(item)).ToList()
        For Each item In itemsToRemove
            items.Remove(item)
        Next
    End Sub

End Module