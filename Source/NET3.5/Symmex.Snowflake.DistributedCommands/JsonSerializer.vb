Imports System.IO
Imports System.Text
Imports System.Runtime.Serialization.Json

Public Class JsonSerializer
    Inherits Serializer

    Public Overrides Function Deserialize(itemType As Type, rawItem As String) As Object
        Dim buffer = Encoding.UTF8.GetBytes(rawItem)
        Using ms As New MemoryStream(buffer)
            Dim s As New DataContractJsonSerializer(itemType)
            Return s.ReadObject(ms)
        End Using
    End Function

    Public Overrides Function Serialize(itemType As Type, item As Object) As String
        Dim ms As New MemoryStream()
        Dim s As New DataContractJsonSerializer(itemType)
        s.WriteObject(ms, item)

        Return Encoding.UTF8.GetString(ms.ToArray())
    End Function

End Class