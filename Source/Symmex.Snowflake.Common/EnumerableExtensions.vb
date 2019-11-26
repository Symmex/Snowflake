Imports System.Runtime.CompilerServices

Public Module EnumerableExtensions

    <Extension()>
    Public Function Contains(Of T)(items As IEnumerable(Of T), predicate As Predicate(Of T)) As Boolean
        Return (From item In items Where predicate(item)).Any()
    End Function

    <Extension()>
    Public Sub ForEach(Of T)(items As IEnumerable(Of T), performAction As Action(Of T))
        For Each item In items
            performAction(item)
        Next
    End Sub

    <Extension()>
    Public Function IsNullOrEmpty(Of T)(items As IEnumerable(Of T)) As Boolean
        Return items Is Nothing OrElse Not items.Any()
    End Function

End Module