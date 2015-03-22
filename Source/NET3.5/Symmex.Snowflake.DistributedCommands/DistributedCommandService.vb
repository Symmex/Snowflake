Public Class DistributedCommandService
    Implements IDistributedCommandService

    Public Function Execute(commandEnvelope As String) As String Implements IDistributedCommandService.Execute
        Dim currentSerializer = SerializerManager.Current
        Dim cmdEnv = currentSerializer.Deserialize(Of Envelope)(commandEnvelope)
        Dim cmdType = Type.GetType(cmdEnv.ItemType)
        Dim cmd = DirectCast(currentSerializer.Deserialize(cmdType, cmdEnv.Item), IDistributedCommand)

        cmd.BeforeExecute()
        Dim result = cmd.Execute()
        cmd.AfterExecute()

        Dim resultEnvelope As New Envelope()
        resultEnvelope.ItemType = cmd.ResultType.AssemblyQualifiedName
        resultEnvelope.Item = currentSerializer.Serialize(cmd.ResultType, result)

        Return currentSerializer.Serialize(resultEnvelope)
    End Function

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Public Function ExecuteAsync(commandEnvelope As String) As Task(Of String) Implements IDistributedCommandService.ExecuteAsync
        Return Task.Run(Function()
                            Return Me.Execute(commandEnvelope)
                        End Function)
    End Function
#End If

End Class