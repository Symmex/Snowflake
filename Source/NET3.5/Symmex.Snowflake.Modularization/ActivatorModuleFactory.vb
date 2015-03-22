Public Class ActivatorModuleFactory
    Implements IModuleFactory

    Public Function CreateModule(ByVal moduleType As Type) As IModule Implements IModuleFactory.CreateModule
        Return DirectCast(Activator.CreateInstance(moduleType), IModule)
    End Function

End Class