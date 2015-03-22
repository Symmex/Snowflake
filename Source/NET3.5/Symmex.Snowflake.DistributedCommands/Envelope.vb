Imports System.Runtime.Serialization

<Serializable(), DataContract()>
Public Class Envelope

    <DataMember()>
    Public Property ItemType As String

    <DataMember()>
    Public Property Item As String

End Class