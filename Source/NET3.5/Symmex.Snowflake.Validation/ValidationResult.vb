Public Class ValidationResult

    Public Property Errors As List(Of ValidationError) = New List(Of ValidationError)()

    Public ReadOnly Property IsValid As Boolean
        Get
            Return Me.Errors.Count = 0
        End Get
    End Property

    Public Sub AddError(ByVal propertyName As String, ByVal message As String)
        Me.AddError(propertyName, message, 0)
    End Sub

    Public Sub AddError(ByVal propertyName As String, ByVal message As String, severity As Integer)
        Me.Errors.Add(New ValidationError(propertyName, message, severity))
    End Sub

    Public Sub ClearErrors(ByVal propertyName As String)
        Me.Errors.RemoveAll(Function(item) item.PropertyName = propertyName)
    End Sub

    Public Overrides Function ToString() As String
        Dim allErrors = Me.Errors.Select(Function(item) item.Message)

        Return String.Join(vbNewLine, allErrors.ToArray())
    End Function

    Public Overloads Function ToString(ByVal propertyName As String) As String
        Dim errorList = Me.Errors.Where(Function(item) item.PropertyName = propertyName).Select(Function(item) item.Message)
        Return String.Join(vbNewLine, errorList.ToArray())
    End Function

    Public Sub ThrowIfInvalid()
        If Not Me.IsValid Then
            Throw New ValidationException(Me)
        End If
    End Sub

End Class