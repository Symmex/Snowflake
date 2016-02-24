#If TargetFramework >= "4.0" Then
Imports System.Threading.Tasks
#End If

Public Interface IValidator

    Function Validate(ByVal item As Object) As ValidationResult
    Sub Validate(ByVal item As Object, ByVal result As ValidationResult)
    Function Validate(ByVal item As Object, ByVal propertyName As String) As ValidationResult
    Sub Validate(ByVal item As Object, ByVal propertyName As String, ByVal result As ValidationResult)

#If TargetFramework >= "4.0" Then
    Function ValidateAsync(ByVal item As Object) As Task(Of ValidationResult)
    Function ValidateAsync(ByVal item As Object, ByVal result As ValidationResult) As Task
    Function ValidateAsync(ByVal item As Object, ByVal propertyName As String) As Task(Of ValidationResult)
    Function ValidateAsync(ByVal item As Object, ByVal propertyName As String, ByVal result As ValidationResult) As Task
#End If

End Interface

Public Interface IValidator(Of T)
    Inherits IValidator

    Overloads Function Validate(ByVal item As T) As ValidationResult
    Overloads Sub Validate(ByVal item As T, ByVal result As ValidationResult)
    Overloads Function Validate(ByVal item As T, ByVal propertyName As String) As ValidationResult
    Overloads Sub Validate(ByVal item As T, ByVal propertyName As String, ByVal result As ValidationResult)

#If TargetFramework >= "4.0" Then
    Overloads Function ValidateAsync(ByVal item As T) As Task(Of ValidationResult)
    Overloads Function ValidateAsync(ByVal item As T, ByVal result As ValidationResult) As Task
    Overloads Function ValidateAsync(ByVal item As T, ByVal propertyName As String) As Task(Of ValidationResult)
    Overloads Function ValidateAsync(ByVal item As T, ByVal propertyName As String, ByVal result As ValidationResult) As Task
#End If

End Interface