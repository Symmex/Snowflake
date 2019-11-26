Public Interface IDistributedCommand

    ReadOnly Property ResultType As Type
    Function Execute() As Object
    Function ExecuteAsync() As Task(Of Object)

End Interface

Public Interface IDistributedCommand(Of T)
    Inherits IDistributedCommand

    Overloads Function Execute() As T
    Overloads Function ExecuteAsync() As Task(Of T)

End Interface