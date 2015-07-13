Public Class ModuleLoaderException
    Inherits Exception

    Public Sub New(moduleType As String, moduleAssembly As String, message As String, innerException As Exception)
        MyBase.New(String.Format("An error occured in the module {0} in assembly {1}. {2}", moduleType, moduleAssembly, message), innerException)
    End Sub

End Class