Public Interface IResolver

    Function Resolve() As Object

End Interface

Public Interface IResolver(Of T)
    Inherits IResolver

    Overloads Function Resolve() As T

End Interface