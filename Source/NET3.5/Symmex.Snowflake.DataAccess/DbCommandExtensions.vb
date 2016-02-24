Imports System.Runtime.CompilerServices
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Threading
#If TargetFramework >= "4.0" Then
Imports System.Threading.Tasks
#End If
Imports Symmex.Snowflake.Common

Public Module DbCommandExtensions

    <Extension()>
    Public Function AddParameter(Of T)(ByVal cmd As DbCommand, ByVal name As String, ByVal value As T) As DbCommand
        cmd.AddParameter(name, value, DbTypeLookup.GetDbType(GetType(T)))

        Return cmd
    End Function

    <Extension()>
    Public Function AddParameter(ByVal cmd As DbCommand, ByVal name As String, ByVal value As Object, ByVal type As DbType) As DbCommand
        Dim param = cmd.CreateParameter()
        param.ParameterName = name
        param.Value = If(value, DBNull.Value)
        param.DbType = type
        cmd.Parameters.Add(param)

        Return cmd
    End Function

    <Extension()>
    Public Function ExecuteScalar(Of T)(ByVal cmd As DbCommand) As T
        Return DirectCast(cmd.ExecuteScalar(), T)
    End Function

#If TargetFramework = "4.0" Then
    <Extension()>
    Public Function ExecuteNonQueryAsync(cmd As DbCommand) As Task(Of Integer)
        Return cmd.ExecuteNonQueryAsync(CancellationToken.None)
    End Function

    <Extension()>
    Public Function ExecuteNonQueryAsync(cmd As DbCommand, cancellationToken As CancellationToken) As Task(Of Integer)
        Dim sc = TryCast(cmd, SqlCommand)
        If sc Is Nothing Then
            Return Task.Factory.FromResult(cmd.ExecuteNonQuery())
        Else
            Return Task.Factory.FromAsync(AddressOf sc.BeginExecuteNonQuery, AddressOf sc.EndExecuteNonQuery, Nothing)
        End If
    End Function

    <Extension()>
    Public Function ExecuteReaderAsync(cmd As DbCommand) As Task(Of DbDataReader)
        Return cmd.ExecuteReaderAsync(CommandBehavior.CloseConnection, CancellationToken.None)
    End Function

    <Extension()>
    Public Function ExecuteReaderAsync(cmd As DbCommand, behavior As CommandBehavior) As Task(Of DbDataReader)
        Return cmd.ExecuteReaderAsync(behavior, CancellationToken.None)
    End Function

    <Extension()>
    Public Function ExecuteReaderAsync(cmd As DbCommand, behavior As CommandBehavior, cancellationToken As CancellationToken) As Task(Of DbDataReader)
        Dim tcs As New TaskCompletionSource(Of DbDataReader)()

        Dim sc = TryCast(cmd, SqlCommand)
        If sc Is Nothing Then
            Try
                Dim result = cmd.ExecuteReader(behavior)
                tcs.SetResult(result)
            Catch ex As Exception
                tcs.SetException(ex)
            End Try
        Else
            Task.Factory.FromAsync(AddressOf sc.BeginExecuteReader, AddressOf sc.EndExecuteReader, Nothing) _
                .ContinueWith(Sub(t)
                                  If t.IsFaulted Then
                                      tcs.SetException(t.Exception)
                                  End If

                                  If t.IsCanceled Then
                                      tcs.SetCanceled()
                                  End If

                                  tcs.SetResult(t.Result)
                              End Sub)
        End If

        Return tcs.Task
    End Function

    <Extension()>
    Public Function ExecuteScalarAsync(Of TResult)(cmd As DbCommand, cancellationToken As CancellationToken) As Task(Of TResult)
        Dim tcs As New TaskCompletionSource(Of TResult)()

        cmd.ExecuteReaderAsync() _
            .ContinueWith(Sub(ert)
                              If ert.IsFaulted Then
                                  tcs.SetException(ert.Exception)
                              End If

                              If ert.IsCanceled Then
                                  tcs.SetCanceled()
                              End If

                              Dim reader = ert.Result
                              reader.ReadAsync() _
                              .ContinueWith(Sub(rt)
                                                If rt.IsFaulted Then
                                                    tcs.SetException(rt.Exception)
                                                End If

                                                If rt.IsCanceled Then
                                                    tcs.SetCanceled()
                                                End If

                                                Try
                                                    Dim result = DirectCast(reader.GetValue(0), TResult)
                                                    tcs.SetResult(result)
                                                Catch ex As Exception
                                                    tcs.SetException(ex)
                                                End Try
                                            End Sub)
                          End Sub)

        Return tcs.Task
    End Function
#End If

#If TargetFramework > "4.0" Then
    <Extension()>
    Public Function ExecuteScalarAsync(Of TResult)(cmd As DbCommand, cancellationToken As CancellationToken) As Task(Of TResult)
        Dim tcs As New TaskCompletionSource(Of TResult)()

        cmd.ExecuteScalarAsync(cancellationToken) _
            .ContinueWith(Sub(t)
                              If t.IsFaulted Then
                                  tcs.SetException(t.Exception)
                              End If

                              If t.IsCanceled Then
                                  tcs.SetCanceled()
                              End If

                              tcs.SetResult(DirectCast(t.Result, TResult))
                          End Sub)

        Return tcs.Task
    End Function
#End If

#If TargetFramework >= "4.0" Then
    <Extension()>
    Public Function ExecuteScalarAsync(Of TResult)(cmd As DbCommand) As Task(Of TResult)
        Return cmd.ExecuteScalarAsync(Of TResult)(CancellationToken.None)
    End Function
#End If

End Module