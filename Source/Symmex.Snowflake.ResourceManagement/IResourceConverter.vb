Imports System.IO

Public Interface IResourceConverter

    ReadOnly Property Extension As String
    Function Convert(s As Stream) As Object

End Interface