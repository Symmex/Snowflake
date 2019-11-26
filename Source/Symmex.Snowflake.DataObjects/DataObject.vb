Imports System.Runtime.Serialization
Imports System.ComponentModel
Imports Symmex.Snowflake.Common

<DataContract()>
Public MustInherit Class DataObject(Of TId)
    Inherits SelfTrackingObject
    Implements IIdentifiableObject(Of TId)

#Region "Properties"
    Public Shared ReadOnly IdProperty As New DynamicProperty(Of TId)("Id")
    <DataMember()>
    Public Property Id As TId Implements IIdentifiableObject(Of TId).Id
        Get
            Return Me.GetValue(IdProperty)
        End Get
        Set(ByVal value As TId)
            Me.SetValue(IdProperty, value)
        End Set
    End Property
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub
#End Region

#Region "Methods"
    Public Overrides Function Equals(ByVal obj As Object) As Boolean
        Dim item = TryCast(obj, DataObject(Of TId))
        Return If(item Is Nothing, False, Object.Equals(item.Id, Me.Id))
    End Function

    Public Overrides Function GetHashCode() As Integer
        Return Me.Id.GetHashCode()
    End Function
#End Region

End Class