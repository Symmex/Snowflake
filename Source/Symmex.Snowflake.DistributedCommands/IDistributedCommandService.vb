Imports System.ServiceModel

<ServiceContract()>
Public Interface IDistributedCommandService

    <OperationContract(Name:="Execute")>
    Function Execute(commandEnvelope As String) As String

    <OperationContract(Name:="ExecuteAsync")>
    Function ExecuteAsync(commandEnvelope As String) As Task(Of String)

End Interface