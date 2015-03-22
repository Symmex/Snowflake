Imports System.Runtime.Serialization

<DataContract()>
Public Class ValidationError

    <DataMember()>
    Public Property PropertyName As String

    <DataMember()>
    Public Property Message As String

    <DataMember()>
    Public Property Severity As Integer

    Public Sub New(propertyName As String, message As String)
        Me.New(propertyName, message, 0)
    End Sub

    Public Sub New(propertyName As String, message As String, severity As Integer)
        Me.PropertyName = propertyName
        Me.Message = message
        Me.Severity = severity
    End Sub

End Class