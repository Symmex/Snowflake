#If TargetFramework >= 4.0 Then
Imports System.Threading.Tasks
#End If
Imports Symmex.Snowflake.Common

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

#If TargetFramework >= 4.0 Then
    Public Overridable Function ExecuteAsync() As Task(Of T) Implements IDistributedCommand(Of T).ExecuteAsync
        Return Task.Factory.FromResult(Of T)(Nothing)
    End Function

    Private Function IDistributedCommand_ExecuteAsync() As Task(Of Object) Implements IDistributedCommand.ExecuteAsync
        Return Me.ExecuteAsync() _
            .ContinueWith(Function(ct) DirectCast(ct.Result, Object))
    End Function
#End If

End Class