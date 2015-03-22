Imports System.Data.Common

Public Interface IDb

    ReadOnly Property ProviderFactory As DbProviderFactory
    ReadOnly Property ConnectionStringBuilder As DbConnectionStringBuilder
    Property ConnectionRetryCount As Integer
    Function CreateConnection() As IDbConnection
    Function OpenConnection() As IDbConnection
    Function CreateCommand(ByVal text As String) As IDbCommand
    Function CreateCommand(ByVal text As String, ByVal type As CommandType) As IDbCommand

End Interface