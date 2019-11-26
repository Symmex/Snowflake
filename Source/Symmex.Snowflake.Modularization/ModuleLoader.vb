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
            Try
                moduleAssembly = _Provider.LoadAssembly(mc.AssemblyFile)
                moduleType = moduleAssembly.GetType(mc.ModuleType)
                moduleInstance = _Factory.CreateModule(moduleType)
            Catch ex As Exception
                Throw New ModuleLoaderException(mc.ModuleType, mc.AssemblyFile, "Error while creating module instance.", ex)
            End Try

            Try
                moduleInstance.BeginInitialize()
            Catch ex As Exception
                Throw New ModuleLoaderException(moduleType.Name, moduleType.Assembly.GetName().Name, "Error occured in BeginInitialize.", ex)
            End Try

            modules.Add(moduleInstance)
        Next

        For Each m In modules
            Try
                m.EndInitialize()
            Catch ex As Exception
                moduleType = m.GetType()
                Throw New ModuleLoaderException(moduleType.Name, moduleType.Assembly.GetName().Name, "Error occured in EndInitialize.", ex)
            End Try
        Next

        Return modules
    End Function

End Class