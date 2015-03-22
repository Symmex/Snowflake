Imports System.Runtime.CompilerServices

Public Module DbConnectionExtensions

    <Extension()>
    Public Sub Open(conn As IDbConnection, retryCount As Integer)
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