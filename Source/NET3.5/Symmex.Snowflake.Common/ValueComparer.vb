Public Class ValueComparer(Of TSource)
    Implements IComparer(Of TSource)

    Private _ValueSelector As Func(Of TSource, IComparable)

    Public Sub New(valueSelector As Func(Of TSource, IComparable))
        _ValueSelector = valueSelector
    End Sub

    Public Function Compare(x As TSource, y As TSource) As Integer Implements IComparer(Of TSource).Compare
        Dim value1 = _ValueSelector.Invoke(x)
        Dim value2 = _ValueSelector.Invoke(y)
        Return value1.CompareTo(value2)
    End Function

End Class