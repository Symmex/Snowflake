Public Interface IValidator

    Function Validate(ByVal item As Object) As ValidationResult
    Sub Validate(ByVal item As Object, ByVal result As ValidationResult)
    Function Validate(ByVal item As Object, ByVal propertyName As String) As ValidationResult
    Sub Validate(ByVal item As Object, ByVal propertyName As String, ByVal result As ValidationResult)

End Interface