Public Class ValidationException
    Inherits Exception

    Public ReadOnly Property Result As ValidationResult

    Public Sub New(result As ValidationResult)
        MyBase.New(result.ToString())

        Me.Result = result
    End Sub

End Class