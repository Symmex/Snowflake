#If TargetFramework >= 4.0 Then
Imports System.Threading.Tasks
#End If
Imports Symmex.Snowflake.Validation

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

#If TargetFramework >= 4.0 Then
    Private _AsyncValidationMethods As Dictionary(Of String, Func(Of T, ValidationResult, Task))
    Protected ReadOnly Property AsyncValidationMethods As Dictionary(Of String, Func(Of T, ValidationResult, Task))
        Get
            If _AsyncValidationMethods Is Nothing Then
                _AsyncValidationMethods = New Dictionary(Of String, Func(Of T, ValidationResult, Task))()
            End If

            Return _AsyncValidationMethods
        End Get
    End Property
#End If
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

#If TargetFramework >= 4.0 Then
    Public Function ValidateAsync(item As T) As Task(Of ValidationResult) Implements IValidator(Of T).ValidateAsync
        Dim result As New ValidationResult()

        Dim resultTask = Me.ValidateAsync(item, result) _
            .ContinueWith(Function(t) result)

        Return resultTask
    End Function

    Public Function ValidateAsync(item As T, result As ValidationResult) As Task Implements IValidator(Of T).ValidateAsync
        Dim allTasks = (From method In Me.AsyncValidationMethods.Values
                        Select method.Invoke(item, result)).ToArray()

        Return Task.Factory _
            .ContinueWhenAll(allTasks, Sub(tasks)
                                       End Sub)
    End Function

    Public Function ValidateAsync(item As T, propertyName As String) As Task(Of ValidationResult) Implements IValidator(Of T).ValidateAsync
        Dim result As New ValidationResult()

        Dim resultTask = Me.ValidateAsync(item, propertyName, result) _
            .ContinueWith(Function(t) result)

        Return resultTask
    End Function

    Public Function ValidateAsync(item As T, propertyName As String, result As ValidationResult) As Task Implements IValidator(Of T).ValidateAsync
        Dim method As Func(Of T, ValidationResult, Task) = Nothing
        If Me.AsyncValidationMethods.TryGetValue(propertyName, method) Then
            result.ClearErrors(propertyName)
            Return method.Invoke(item, result)
        End If

        Dim tcs As New TaskCompletionSource(Of Boolean)()
        tcs.SetResult(True)
        Return tcs.Task
    End Function

    Private Function IValidator_ValidateAsync(item As Object) As Task(Of ValidationResult) Implements IValidator.ValidateAsync
        Return Me.ValidateAsync(DirectCast(item, T))
    End Function

    Private Function IValidator_ValidateAsync(item As Object, result As ValidationResult) As Task Implements IValidator.ValidateAsync
        Return Me.ValidateAsync(DirectCast(item, T), result)
    End Function

    Private Function IValidator_ValidateAsync(item As Object, propertyName As String) As Task(Of ValidationResult) Implements IValidator.ValidateAsync
        Return Me.ValidateAsync(DirectCast(item, T), propertyName)
    End Function

    Private Function IValidator_ValidateAsync(item As Object, propertyName As String, result As ValidationResult) As Task Implements IValidator.ValidateAsync
        Return Me.ValidateAsync(DirectCast(item, T), propertyName, result)
    End Function
#End If

#End Region

End Class