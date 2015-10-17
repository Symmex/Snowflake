Public Interface IDistributedCommand

    ReadOnly Property ResultType As Type
    Sub BeforeExecute()
    Function Execute() As Object
    Sub AfterExecute()

End Interface

Public Interface IDistributedCommand(Of Out T)
    Inherits IDistributedCommand

    Overloads Function Execute() As T

End Interface