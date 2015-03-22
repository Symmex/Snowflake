Public Class DataScriptExecutedEventArgs
    Inherits EventArgs

    Private _Script As DataScript
    Public ReadOnly Property Script As DataScript
        Get
            Return _Script
        End Get
    End Property

    Public Property Cancel As Boolean

    Private _Exception As DataScriptExecutionException
    Public ReadOnly Property Exception As DataScriptExecutionException
        Get
            Return _Exception
        End Get
    End Property

    Public Sub New(script As DataScript)
        Me.New(script, Nothing)
    End Sub

    Public Sub New(script As DataScript, ex As DataScriptExecutionException)
        _Script = script
        _Exception = ex
    End Sub

End Class