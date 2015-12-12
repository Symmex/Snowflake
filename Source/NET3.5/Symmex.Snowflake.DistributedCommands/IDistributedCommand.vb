Public Interface IDistributedCommand

    ReadOnly Property ResultType As Type
    Function Execute() As Object

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Function ExecuteAsync() As Task(Of Object)
#End If

End Interface

Public Interface IDistributedCommand(Of T)
    Inherits IDistributedCommand

    Overloads Function Execute() As T

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Overloads Function ExecuteAsync() As Task(Of T)
#End If

End Interface