Imports System.Runtime.Serialization

Public MustInherit Class DistributedCommand(Of T)
    Implements IDistributedCommand(Of T)

    Public ReadOnly Property ResultType As Type Implements IDistributedCommand.ResultType
        Get
            Return GetType(T)
        End Get
    End Property

    Public Sub New()
    End Sub

    Public MustOverride Function Execute() As T Implements IDistributedCommand(Of T).Execute

    Private Function ICommand_Execute() As Object Implements IDistributedCommand.Execute
        Try
            Return Me.Execute()
        Catch ex As Exception
            Me.OnError(ex)
            Throw ex
        End Try
    End Function

    Public Overridable Sub BeforeExecute() Implements IDistributedCommand.BeforeExecute
    End Sub

    Public Overridable Sub AfterExecute() Implements IDistributedCommand.AfterExecute
    End Sub

    Protected Overridable Sub OnError(ex As Exception)
    End Sub

End Class