Public Interface ISerializer

    Function Serialize(Of T)(item As T) As String
    Function Serialize(itemType As Type, item As Object) As String

    Function Deserialize(Of T)(rawItem As String) As T
    Function Deserialize(itemType As Type, rawItem As String) As Object

End Interface