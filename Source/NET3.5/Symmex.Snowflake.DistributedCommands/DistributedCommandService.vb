#If TargetFramework >= "4.0" Then
Imports System.Threading.Tasks
#End If

Public Class DistributedCommandService
    Implements IDistributedCommandService

    Public Function Execute(commandEnvelope As String) As String Implements IDistributedCommandService.Execute
        Dim cmd = Me.GetCommand(commandEnvelope)
        Dim result = cmd.Execute()
        Dim resultEnvelope = Me.CreateResultEnvelope(cmd, result)

        Return resultEnvelope
    End Function

    Private Function GetCommand(commandEnvelope As String) As IDistributedCommand
        Dim currentSerializer = SerializerManager.Current
        Dim cmdEnv = currentSerializer.Deserialize(Of Envelope)(commandEnvelope)
        Dim cmdType = Type.GetType(cmdEnv.ItemType)

        Return DirectCast(currentSerializer.Deserialize(cmdType, cmdEnv.Item), IDistributedCommand)
    End Function

    Private Function CreateResultEnvelope(cmd As IDistributedCommand, result As Object) As String
        Dim currentSerializer = SerializerManager.Current
        Dim resultEnvelope As New Envelope()
        resultEnvelope.ItemType = cmd.ResultType.AssemblyQualifiedName
        resultEnvelope.Item = currentSerializer.Serialize(cmd.ResultType, result)

        Return currentSerializer.Serialize(resultEnvelope)
    End Function

#If TargetFramework >= "4.0" Then
    Public Function BeginExecute(commandEnvelope As String, callback As AsyncCallback, state As Object) As IAsyncResult Implements IDistributedCommandService.BeginExecute
        Dim completionSource As New TaskCompletionSource(Of String)(state)

        Dim cmd = Me.GetCommand(commandEnvelope)
        cmd.ExecuteAsync() _
            .ContinueWith(Sub(ct)
                              Dim resultEnvelope = Me.CreateResultEnvelope(cmd, ct.Result)
                              completionSource.SetResult(resultEnvelope)
                              callback.Invoke(completionSource.Task)
                          End Sub)

        Return completionSource.Task
    End Function

    Public Function EndExecute(result As IAsyncResult) As String Implements IDistributedCommandService.EndExecute
        Return DirectCast(result, Task(Of String)).Result
    End Function
#End If

End Class