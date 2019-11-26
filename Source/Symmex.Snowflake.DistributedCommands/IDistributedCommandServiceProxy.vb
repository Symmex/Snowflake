Public Interface IDistributedCommandServiceProxy

    Function Execute(commandEnvelope As String) As String
    Function ExecuteAsync(commandEnvelope As String) As Task(Of String)

End Interface