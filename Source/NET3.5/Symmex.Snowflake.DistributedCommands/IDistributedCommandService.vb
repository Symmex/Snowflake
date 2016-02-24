Imports System.ServiceModel

<ServiceContract()>
Public Interface IDistributedCommandService

    <OperationContract(Name:="Execute")>
    Function Execute(commandEnvelope As String) As String

#If TargetFramework >= "4.0" Then
    <OperationContract(Name:="BeginExecute", AsyncPattern:=True)>
    Function BeginExecute(commandEnvelope As String, ByVal callback As AsyncCallback, ByVal asyncState As Object) As IAsyncResult
    Function EndExecute(ByVal result As IAsyncResult) As String
#End If

End Interface