Imports System.Xml.Serialization

<XmlType("Module")> _
Public Class ModuleConfiguration

    Private _AssemblyFile As String
    <XmlAttribute()> _
    Public Property AssemblyFile() As String
        Get
            Return _AssemblyFile
        End Get
        Set(ByVal value As String)
            _AssemblyFile = value
        End Set
    End Property

    Private _ModuleType As String
    <XmlAttribute()> _
    Public Property ModuleType() As String
        Get
            Return _ModuleType
        End Get
        Set(ByVal value As String)
            _ModuleType = value
        End Set
    End Property

End Class