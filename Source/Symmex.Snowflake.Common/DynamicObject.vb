Imports System.Runtime.Serialization

<DataContract()>
Public Class DynamicObject
    Inherits ObservableObject
    Implements IDynamicObject

#Region "Properties"
    Private _Values As Dictionary(Of String, Object)
    Public ReadOnly Property Values As Dictionary(Of String, Object)
        Get
            If _Values Is Nothing Then
                _Values = New Dictionary(Of String, Object)()
            End If

            Return _Values
        End Get
    End Property
#End Region

#Region "Methods"
    Public Function GetValue(Of T)(dp As DynamicProperty(Of T)) As T
        If dp Is Nothing Then
            Throw New ArgumentNullException("dp")
        End If

        Dim value = Me.GetValueCore(dp.Name, dp.DefaultValueResolver)
        If value Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(value, T)
        End If
    End Function

    Public Function GetValue(dp As IDynamicProperty) As Object Implements IDynamicObject.GetValue
        If dp Is Nothing Then
            Throw New ArgumentNullException("dp")
        End If

        Return Me.GetValueCore(dp.Name, dp.DefaultValueResolver)
    End Function

    Public Function GetValue(Of T)(propertyName As String) As T
        Dim value = Me.GetValueCore(propertyName, Nothing)
        If value Is Nothing Then
            Return Nothing
        Else
            Return DirectCast(value, T)
        End If
    End Function

    Public Function GetValue(propertyName As String) As Object
        Return Me.GetValueCore(propertyName, Nothing)
    End Function

    Private Function GetValueCore(propertyName As String, defaultValueResolver As IResolver) As Object
        If propertyName Is Nothing Then
            Throw New ArgumentNullException("propertyName")
        End If

        Dim value As Object = Nothing
        If Not Me.Values.TryGetValue(propertyName, value) AndAlso defaultValueResolver IsNot Nothing Then
            value = defaultValueResolver.Resolve()
            Me.Values(propertyName) = value
        End If

        Return value
    End Function

    Public Sub SetValue(Of T)(dp As DynamicProperty(Of T), value As T)
        If dp Is Nothing Then
            Throw New ArgumentException("dp")
        End If

        Me.SetValueCore(dp.Name, value, dp.DefaultValueResolver)
    End Sub

    Public Sub SetValue(dp As IDynamicProperty, value As Object) Implements IDynamicObject.SetValue
        If dp Is Nothing Then
            Throw New ArgumentException("dp")
        End If

        Me.SetValueCore(dp.Name, value, dp.DefaultValueResolver)
    End Sub

    Public Sub SetValue(propertyName As String, value As Object)
        Me.SetValueCore(propertyName, value, Nothing)
    End Sub

    Private Sub SetValueCore(propertyName As String, value As Object, defaultValueResolver As IResolver)
        If propertyName Is Nothing Then
            Throw New ArgumentException("propertyName")
        End If

        Dim originalValue = Me.GetValueCore(propertyName, defaultValueResolver)

        If Not Object.Equals(value, originalValue) Then
            Me.Values(propertyName) = value
            Me.OnPropertyChanged(propertyName)
        End If
    End Sub

    Public Sub CopyValues(other As DynamicObject)
        Me.CopyValues(other.Values)
    End Sub

    Public Sub CopyValues(source As IDictionary(Of String, Object))
        If source Is Nothing Then
            Throw New ArgumentNullException("source")
        End If

        Dim props = DynamicPropertyManager.GetProperties(Me.GetType())
        For Each prop In props
            Dim value As Object = Nothing
            If source.TryGetValue(prop.Name, value) Then
                Me.SetValue(prop, value)
            End If
        Next
    End Sub
#End Region

End Class