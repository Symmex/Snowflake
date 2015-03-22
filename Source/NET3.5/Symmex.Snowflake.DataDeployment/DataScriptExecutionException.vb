Imports System.Reflection

Public Class DataScriptExecutionException
    Inherits Exception

    Private _Script As DataScript
    Public ReadOnly Property Script As DataScript
        Get
            Return _Script
        End Get
    End Property

    Public Sub New(ByVal script As DataScript, ByVal innerException As Exception)
        MyBase.New("An error occured while executing a DataScript. See inner exception for details.", innerException)
        _Script = script
    End Sub

End Class