Imports System.IO

Public Class TextResourceConverter
    Implements IResourceConverter

    Public ReadOnly Property Extension As String Implements IResourceConverter.Extension
        Get
            Return ".txt"
        End Get
    End Property

    Public Function Convert(s As IO.Stream) As Object Implements IResourceConverter.Convert
        Return New StreamReader(s).ReadToEnd()
    End Function

End Class