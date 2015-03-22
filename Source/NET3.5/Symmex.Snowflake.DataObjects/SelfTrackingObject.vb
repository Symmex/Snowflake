Imports System.Runtime.Serialization
Imports Symmex.Snowflake.Common

<DataContract()>
Public MustInherit Class SelfTrackingObject
    Inherits DynamicObject
    Implements ISelfTrackingObject

#Region "Properties"
    Public Property IsTrackingChanges As Boolean Implements ISelfTrackingObject.IsTrackingChanges

    <DataMember()>
    Public Overridable Property IsNew As Boolean Implements ISelfTrackingObject.IsNew

    <DataMember()>
    Public Overridable Property IsDeleted As Boolean Implements ISelfTrackingObject.IsDeleted

    <DataMember()>
    Public Overridable Property IsChanged As Boolean Implements ISelfTrackingObject.IsChanged

    Public ReadOnly Property IsSaveRequired As Boolean Implements ISelfTrackingObject.IsSaveRequired
        Get
            Return Me.SaveAction <> DataObjects.SaveAction.None
        End Get
    End Property

    Public ReadOnly Property SaveAction As SaveAction Implements ISelfTrackingObject.SaveAction
        Get
            If Me.IsNew Then
                If Not Me.IsDeleted Then
                    Return SaveAction.Insert
                End If
            ElseIf Me.IsDeleted Then
                Return DataObjects.SaveAction.Delete
            ElseIf Me.IsChanged OrElse Me.IsChildSaveRequired Then
                Return DataObjects.SaveAction.Update
            End If

            Return DataObjects.SaveAction.None
        End Get
    End Property

    Public ReadOnly Property IsChildSaveRequired As Boolean
        Get
            For Each child In Me.Values.Values
                If TypeOf child Is ISelfTrackingObject Then
                    If DirectCast(child, ISelfTrackingObject).IsSaveRequired Then
                        Return True
                    End If
                ElseIf TypeOf child Is IEnumerable(Of ISelfTrackingObject) Then
                    If DirectCast(child, IEnumerable(Of ISelfTrackingObject)).IsSaveRequired() Then
                        Return True
                    End If
                End If
            Next

            Return False
        End Get
    End Property
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub
#End Region

#Region "Methods"
    Public Overridable Sub ResetState() Implements ISelfTrackingObject.ResetState
        For Each child In Me.Values.Values
            If TypeOf child Is ISelfTrackingObject Then
                DirectCast(child, ISelfTrackingObject).ResetState()
            ElseIf TypeOf child Is IEnumerable(Of ISelfTrackingObject) Then
                DirectCast(child, IEnumerable(Of ISelfTrackingObject)).ResetState()
            End If
        Next

        Me.IsChanged = False
        Me.IsNew = False
        Me.IsDeleted = False
    End Sub

    <OnDeserializing()>
    Public Sub OnDeserializing(ByVal context As StreamingContext)
        Me.IsTrackingChanges = False
    End Sub

    <OnDeserialized()>
    Public Sub OnDeserialized(ByVal context As StreamingContext)
        Me.IsTrackingChanges = True
    End Sub

    Public Overrides Sub OnPropertyChanged(propertyName As String)
        If Me.IsTrackingChanges Then
            Me.IsChanged = True
        End If

        MyBase.OnPropertyChanged(propertyName)
    End Sub
#End Region

End Class