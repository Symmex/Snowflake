Imports System.Runtime.CompilerServices

Public Module CollectionExtensions

    <Extension()>
    Public Sub AddRange(Of T)(items As ICollection(Of T), itemsToAdd As IEnumerable(Of T))
        For Each item In itemsToAdd
            items.Add(item)
        Next
    End Sub

End Module