Imports System.Reflection
Imports System.IO
Imports System.Xml.Serialization

Public Class ModuleLoader
    Implements IModuleLoader

    Private _Provider As IModuleLoaderProvider
    Private _Factory As IModuleFactory

    Public Sub New()
        Me.New(New ModuleLoaderProvider(), New ActivatorModuleFactory())
    End Sub

    Public Sub New(provider As IModuleLoaderProvider, factory As IModuleFactory)
        _Provider = provider
        _Factory = factory
    End Sub

    Public Function LoadModules(fileName As String) As List(Of IModule) Implements IModuleLoader.LoadModules
        Dim moduleConfigurations As ModuleConfigurationCollection
        Using fs = _Provider.LoadFile(fileName)
            Dim s As New XmlSerializer(GetType(ModuleConfigurationCollection))
            moduleConfigurations = DirectCast(s.Deserialize(fs), ModuleConfigurationCollection)
        End Using

        Dim moduleAssembly As Assembly
        Dim moduleType As Type
        Dim moduleInstance As IModule
        Dim modules As New List(Of IModule)()
        For Each mc In moduleConfigurations
            moduleAssembly = _Provider.LoadAssembly(mc.AssemblyFile)
            moduleType = moduleAssembly.GetType(mc.ModuleType)
            moduleInstance = _Factory.CreateModule(moduleType)
            moduleInstance.BeginInitialize()
            modules.Add(moduleInstance)
        Next

        For Each m In modules
            m.EndInitialize()
        Next

        Return modules
    End Function

End Class