Imports System.Xml.Serialization

Public Class DataScript

    <XmlAttribute()>
    Public Property Id As Guid

    <XmlAttribute()>
    Public Property ExecuteInTransaction As Boolean = True

    Public Property Text As String
    Public Property Hash As String

End Class