Imports System.Runtime.CompilerServices
Imports System.Data.Common
#If TargetFramework >= "4.0" Then
Imports System.Threading
Imports System.Threading.Tasks
#End If
Imports Symmex.Snowflake.Common

Public Module DbConnectionExtensions

#If TargetFramework = "4.0" Then
    <Extension()>
    Public Function OpenAsync(dbc As DbConnection) As Task
        Return dbc.OpenAsync(CancellationToken.None)
    End Function

    <Extension()>
    Public Function OpenAsync(dbc As DbConnection, cancellationToken As CancellationToken) As Task
        Dim tcs As New TaskCompletionSource(Of Object)()

        Try
            dbc.Open()
            tcs.SetResult(Nothing)
        Catch ex As Exception
            tcs.SetException(ex)
        End Try

        Return tcs.Task
    End Function
#End If

End Module