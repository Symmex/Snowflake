Imports System.Runtime.CompilerServices
Imports System.Data.Common
#If TargetFramework >= 4.0 Then
Imports System.Threading
Imports System.Threading.Tasks
#End If
Imports Symmex.Snowflake.Common

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

#If TargetFramework = 4.0 Then
    <Extension()>
    Public Function OpenAsync(conn As DbConnection, cancellationToken As CancellationToken) As Task
        conn.Open()
        Return Task.Factory.CompletedTask()
    End Function
#End If

End Module