Imports System.Data.Common
Imports System.Threading

Public Interface IDb

    ReadOnly Property ProviderFactory As DbProviderFactory
    ReadOnly Property ConnectionStringBuilder As DbConnectionStringBuilder
    Property ConnectionRetryCount As Integer
    Property CommandTimeout As Integer
    Function CreateConnection() As DbConnection
    Function OpenConnection() As DbConnection
    Function CreateCommand(ByVal text As String) As DbCommand
    Function CreateCommand(ByVal text As String, ByVal type As CommandType) As DbCommand
    Function OpenConnectionAsync() As Task(Of DbConnection)
    Function OpenConnectionAsync(cancellationToken As CancellationToken) As Task(Of DbConnection)

End Interface