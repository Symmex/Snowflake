#If TargetFramework >= 4.0 Then
Imports System.Threading.Tasks
#End If

Public Interface IDistributedCommandServiceProxy

    Function Execute(commandEnvelope As String) As String

#If TargetFramework >= 4.0 Then
    Function ExecuteAsync(commandEnvelope As String) As Task(Of String)
#End If

End Interface