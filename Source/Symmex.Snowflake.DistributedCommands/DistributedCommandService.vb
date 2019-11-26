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

    Public Async Function ExecuteAsync(commandEnvelope As String) As Task(Of String) Implements IDistributedCommandService.ExecuteAsync
        Dim cmd = Me.GetCommand(commandEnvelope)
        Dim result = Await cmd.ExecuteAsync()
        Dim resultEnvelope = Me.CreateResultEnvelope(cmd, result)

        Return resultEnvelope
    End Function

End Class