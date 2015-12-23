Imports System.Runtime.CompilerServices
Imports System.Data.Common
Imports System.Data.SqlClient
Imports System.Threading
#If TargetFramework >= 4.0 Then
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

#If TargetFramework = 4.0 Then
    <Extension()>
    Public Function ExecuteNonQueryAsync(cmd As DbCommand) As Task(Of Integer)
        Return cmd.ExecuteNonQueryAsync(CancellationToken.None)
    End Function

    <Extension()>
    Public Function ExecuteNonQueryAsync(cmd As DbCommand, cancellationToken As CancellationToken) As Task(Of Integer)
        If TypeOf cmd Is FluentCommand Then
            Return DirectCast(cmd, FluentCommand).ExecuteNonQueryAsync(cancellationToken)
        End If

        Return Task.Factory.FromResult(cmd.ExecuteNonQuery())
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
        If TypeOf cmd Is FluentCommand Then
            Return DirectCast(cmd, FluentCommand).ExecuteReaderAsync(behavior, cancellationToken)
        End If

        Return Task.Factory.FromResult(cmd.ExecuteReader(behavior))
    End Function

    <Extension()>
    Public Function ExecuteScalarAsync(Of TResult)(cmd As DbCommand) As Task(Of TResult)
        Return cmd.ExecuteScalarAsync(Of TResult)(CancellationToken.None)
    End Function

    <Extension()>
    Public Function ExecuteScalarAsync(Of TResult)(cmd As DbCommand, cancellationToken As CancellationToken) As Task(Of TResult)
        If TypeOf cmd Is FluentCommand Then
            Return DirectCast(cmd, FluentCommand).ExecuteScalarAsync(cancellationToken) _
                .ContinueWith(Function(ct)
                                  Return DirectCast(ct.Result, TResult)
                              End Function)
        End If

        Return Task.Factory.FromResult(cmd.ExecuteScalar(Of TResult)())
    End Function
#End If

End Module