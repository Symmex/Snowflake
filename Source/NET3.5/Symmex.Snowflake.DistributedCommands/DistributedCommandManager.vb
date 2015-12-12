Imports Symmex.Snowflake.Common

Public NotInheritable Class DistributedCommandManager

    Private Shared ReadOnly Property DefaultProxy As IDistributedCommandServiceProxy
        Get
            Dim value As IDistributedCommandServiceProxy = Nothing
            ObjectManager.TryResolve(Of IDistributedCommandServiceProxy)(value)
            Return value
        End Get
    End Property

    Private Sub New()
    End Sub

    Public Shared Function Execute(cmd As IDistributedCommand) As Object
        Return Execute(cmd, DefaultProxy)
    End Function

    Public Shared Function Execute(cmd As IDistributedCommand, proxy As IDistributedCommandServiceProxy) As Object
        Dim result As Object

        'If we have no proxy, execute it locally
        If proxy Is Nothing Then
            result = cmd.Execute()
        Else
            Dim commandEnvelope = EncloseCommand(cmd)
            Dim resultEnvelope = proxy.Execute(commandEnvelope)
            result = OpenResult(resultEnvelope)
        End If

        Return result
    End Function

    Public Shared Function Execute(Of T)(cmd As IDistributedCommand(Of T)) As T
        Return DirectCast(Execute(DirectCast(cmd, IDistributedCommand)), T)
    End Function

    Public Shared Function Execute(Of T)(cmd As IDistributedCommand(Of T), proxy As IDistributedCommandServiceProxy) As T
        Return DirectCast(Execute(DirectCast(cmd, IDistributedCommand), proxy), T)
    End Function

    Private Shared Function EncloseCommand(cmd As IDistributedCommand) As String
        Dim currentSerializer = SerializerManager.Current
        Dim commandEnvelope As New Envelope()
        commandEnvelope.ItemType = cmd.GetType().AssemblyQualifiedName
        commandEnvelope.Item = currentSerializer.Serialize(cmd.GetType(), cmd)
        Return currentSerializer.Serialize(commandEnvelope)
    End Function

    Private Shared Function OpenResult(resultEnvelope As String) As Object
        Dim currentSerializer = SerializerManager.Current
        Dim commandEnvelope = currentSerializer.Deserialize(Of Envelope)(resultEnvelope)
        Dim resultType = Type.GetType(commandEnvelope.ItemType)
        Return currentSerializer.Deserialize(resultType, commandEnvelope.Item)
    End Function

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Public Shared Async Function ExecuteAsync(Of T)(cmd As IDistributedCommand(Of T)) As Task(Of T)
        Dim result = Await ExecuteAsync(DirectCast(cmd, IDistributedCommand))
        Return DirectCast(result, T)
    End Function

    Public Shared Async Function ExecuteAsync(cmd As IDistributedCommand) As Task(Of Object)
        Dim result As Object
        Dim currentProxy = DefaultProxy

        If currentProxy Is Nothing Then
            result = Await cmd.ExecuteAsync()
        Else
            Dim commandEnvelope = EncloseCommand(cmd)
            Dim resultEnvelope = Await currentProxy.ExecuteAsync(commandEnvelope)
            result = OpenResult(resultEnvelope)
        End If

        Return result
    End Function
#End If

End Class