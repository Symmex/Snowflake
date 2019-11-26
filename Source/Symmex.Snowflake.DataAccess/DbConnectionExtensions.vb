Imports System.Runtime.CompilerServices
Imports System.Data.Common

Public Module DbConnectionExtensions

    <Extension()>
    Public Sub Open(conn As DbConnection, retryCount As Integer)
        For attempt = 0 To retryCount
            Try
                conn.Open()
                Exit For
            Catch ex As Exception
                If attempt = retryCount Then
                    Throw ex
                End If
            End Try
        Next
    End Sub

End Module