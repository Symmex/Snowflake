Imports Symmex.Snowflake.Modularization
Imports System.Reflection

Public Class ModuleLoaderProvider
    Implements IModuleLoaderProvider

    Public Function LoadAssembly(fileName As String) As System.Reflection.Assembly Implements Symmex.Snowflake.Modularization.IModuleLoaderProvider.LoadAssembly
        Dim sri = Application.GetResourceStream(New Uri(fileName, UriKind.Relative))
        Dim ap As New AssemblyPart()
        Return ap.Load(sri.Stream)
    End Function

    Public Function LoadFile(fileName As String) As System.IO.Stream Implements Symmex.Snowflake.Modularization.IModuleLoaderProvider.LoadFile
        Dim rs = Application.GetResourceStream(New Uri(fileName, UriKind.Relative))
        Return rs.Stream
    End Function

End Class