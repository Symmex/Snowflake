Imports System.ServiceModel

<ServiceContract()>
Public Interface IDistributedCommandService

    <OperationContract(Name:="Execute")>
    Function Execute(commandEnvelope As String) As String

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    <OperationContract(Name:="ExecuteAsync")>
    Function ExecuteAsync(commandEnvelope As String) As Task(Of String)
#End If

End Interface