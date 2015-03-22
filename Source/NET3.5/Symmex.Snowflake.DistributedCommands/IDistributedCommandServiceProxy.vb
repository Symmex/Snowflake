Public Interface IDistributedCommandServiceProxy

    Function Execute(commandEnvelope As String) As String

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Function ExecuteAsync(commandEnvelope As String) As Task(Of String)
#End If

End Interface