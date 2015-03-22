Public Class Cache
    Inherits Dictionary(Of Object, Object)

    Private _Owner As CacheScope
    Public ReadOnly Property Owner As CacheScope
        Get
            Return _Owner
        End Get
    End Property

    Public Sub New(owner As CacheScope)
        _Owner = owner
    End Sub

    Public Function GetValue(Of T)(key As Object) As T
        Return DirectCast(Me.Item(key), T)
    End Function

    Public Overloads Function TryGetValue(Of T)(key As Object, ByRef value As T) As Boolean
        Dim objectValue As Object = Nothing
        If MyBase.TryGetValue(key, objectValue) Then
            value = DirectCast(objectValue, T)
            Return True
        Else
            Return False
        End If
    End Function

End Class