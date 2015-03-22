Public MustInherit Class Serializer
    Implements ISerializer

    Public Function Deserialize(Of T)(rawItem As String) As T Implements ISerializer.Deserialize
        Return DirectCast(Me.Deserialize(GetType(T), rawItem), T)
    End Function

    Public MustOverride Function Deserialize(itemType As Type, rawItem As String) As Object Implements ISerializer.Deserialize

    Public Function Serialize(Of T)(item As T) As String Implements ISerializer.Serialize
        Return Me.Serialize(GetType(T), item)
    End Function

    Public MustOverride Function Serialize(itemType As Type, item As Object) As String Implements ISerializer.Serialize

End Class