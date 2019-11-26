Imports System.Runtime.Serialization
Imports Symmex.Snowflake.DistributedCommands

Public MustInherit Class DistributedCommand(Of T)
    Implements IDistributedCommand(Of T)

    Public ReadOnly Property ResultType As Type Implements IDistributedCommand.ResultType
        Get
            Return GetType(T)
        End Get
    End Property

    Public Sub New()
    End Sub

    Public Function Execute() As T Implements IDistributedCommand(Of T).Execute
        Return Nothing
    End Function

    Private Function IDistributedCommand_Execute() As Object Implements IDistributedCommand.Execute
        Return Me.Execute()
    End Function

    Public Overridable Function ExecuteAsync() As Task(Of T) Implements IDistributedCommand(Of T).ExecuteAsync
        Return Task.FromResult(Of T)(Nothing)
    End Function

    Private Async Function IDistributedCommand_ExecuteAsync() As Task(Of Object) Implements IDistributedCommand.ExecuteAsync
        Return Await Me.ExecuteAsync()
    End Function

End Class