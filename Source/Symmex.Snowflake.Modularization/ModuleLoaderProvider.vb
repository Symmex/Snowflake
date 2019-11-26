Imports System.IO
Imports System.Reflection

Public Class ModuleLoaderProvider
    Implements IModuleLoaderProvider

    Private Function GetModuleDirectory() As String
        Return Path.GetDirectoryName(New Uri(Me.GetType().Assembly.CodeBase).LocalPath)
    End Function

    Private Function GetFullPath(fileName As String) As String
        Return Path.Combine(Me.GetModuleDirectory(), fileName)
    End Function

    Public Function LoadFile(fileName As String) As System.IO.Stream Implements IModuleLoaderProvider.LoadFile
        Dim fullPath = Me.GetFullPath(fileName)
        Return File.OpenRead(fullPath)
    End Function

    Public Function LoadAssembly(fileName As String) As System.Reflection.Assembly Implements IModuleLoaderProvider.LoadAssembly
        Dim fullPath = Me.GetFullPath(fileName)
        Return Assembly.LoadFrom(fullPath)
    End Function

End Class