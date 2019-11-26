Imports System.Runtime.CompilerServices
Imports System.Data.Common

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

    <Extension()>
    Public Async Function ExecuteScalarAsync(Of T)(ByVal cmd As DbCommand) As Task(Of T)
        Return DirectCast(Await cmd.ExecuteScalarAsync(), T)
    End Function

End Module