Imports System.IO
Imports System.Reflection

Public Interface IModuleLoaderProvider

    Function LoadFile(fileName As String) As Stream
    Function LoadAssembly(fileName As String) As Assembly

End Interface