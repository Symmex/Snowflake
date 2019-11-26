Public Class MethodComparer(Of T)
    Implements IComparer(Of T)

    Private _Method As Func(Of T, T, Integer)

    Public Sub New(method As Func(Of T, T, Integer))
        _Method = method
    End Sub

    Public Function Compare(x As T, y As T) As Integer Implements IComparer(Of T).Compare
        Return _Method.Invoke(x, y)
    End Function

End Class