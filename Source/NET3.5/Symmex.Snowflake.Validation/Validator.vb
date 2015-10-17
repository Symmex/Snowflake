Public MustInherit Class Validator(Of T)
    Implements IValidator(Of T)

#Region "Properties"
    Private _ValidationMethods As Dictionary(Of String, Action(Of T, ValidationResult))
    Protected ReadOnly Property ValidationMethods As Dictionary(Of String, Action(Of T, ValidationResult))
        Get
            If _ValidationMethods Is Nothing Then
                _ValidationMethods = New Dictionary(Of String, Action(Of T, ValidationResult))()
            End If

            Return _ValidationMethods
        End Get
    End Property
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub
#End Region

#Region "IValidator"
    Private Function IValidator_Validate(ByVal item As Object) As ValidationResult Implements IValidator.Validate
        Return Me.Validate(DirectCast(item, T))
    End Function

    Private Sub IValidator_Validate(ByVal item As Object, ByVal result As ValidationResult) Implements IValidator.Validate
        Me.Validate(DirectCast(item, T), result)
    End Sub

    Private Function IValidator_Validate(ByVal item As Object, ByVal propertyName As String) As ValidationResult Implements IValidator.Validate
        Return Me.Validate(DirectCast(item, T), propertyName)
    End Function

    Private Sub IValidator_Validate(ByVal item As Object, ByVal propertyName As String, ByVal result As ValidationResult) Implements IValidator.Validate
        Me.Validate(DirectCast(item, T), propertyName, result)
    End Sub
#End Region

#Region "Methods"
    Public Function Validate(ByVal item As T) As ValidationResult Implements IValidator(Of T).Validate
        Dim result As New ValidationResult()
        Me.Validate(item, result)
        Return result
    End Function

    Public Sub Validate(ByVal item As T, ByVal result As ValidationResult) Implements IValidator(Of T).Validate
        For Each method In Me.ValidationMethods.Values
            method.Invoke(item, result)
        Next
    End Sub

    Public Function Validate(ByVal item As T, ByVal propertyName As String) As ValidationResult Implements IValidator(Of T).Validate
        Dim result As New ValidationResult()
        Me.Validate(item, propertyName, result)
        Return result
    End Function

    Public Sub Validate(ByVal item As T, ByVal propertyName As String, ByVal result As ValidationResult) Implements IValidator(Of T).Validate
        Dim method As Action(Of T, ValidationResult) = Nothing
        If Me.ValidationMethods.TryGetValue(propertyName, method) Then
            result.ClearErrors(propertyName)
            method.Invoke(item, result)
        End If
    End Sub
#End Region

End Class