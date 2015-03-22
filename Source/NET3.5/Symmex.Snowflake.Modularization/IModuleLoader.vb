Public Interface IModuleLoader

    Function LoadModules(fileName As String) As List(Of IModule)

End Interface