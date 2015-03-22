Imports System.Runtime.CompilerServices

Public Module DbCommandExtensions

    <Extension()>
    Public Function AddParameter(Of T)(ByVal cmd As IDbCommand, ByVal name As String, ByVal value As T) As IDbCommand
        cmd.AddParameter(name, value, DbTypeLookup.GetDbType(GetType(T)))

        Return cmd
    End Function

    <Extension()>
    Public Function AddParameter(ByVal cmd As IDbCommand, ByVal name As String, ByVal value As Object, ByVal type As DbType) As IDbCommand
        Dim param = cmd.CreateParameter()
        param.ParameterName = name
        param.Value = If(value, DBNull.Value)
        param.DbType = type
        cmd.Parameters.Add(param)

        Return cmd
    End Function

    <Extension()>
    Public Function ExecuteScalar(Of T)(ByVal cmd As IDbCommand) As T
        Return DirectCast(cmd.ExecuteScalar(), T)
    End Function

End Module