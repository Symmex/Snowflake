Public Interface IModuleFactory

    Function CreateModule(ByVal moduleType As Type) As IModule

End Interface