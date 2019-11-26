Imports System.Data.Common
Imports System.Configuration
Imports Symmex.Snowflake.DataAccess
Imports System.Threading

Public Class Db
    Implements IDb

    Public Property ConnectionRetryCount As Integer Implements IDb.ConnectionRetryCount

    Private _ProviderFactory As DbProviderFactory
    Public ReadOnly Property ProviderFactory As DbProviderFactory Implements IDb.ProviderFactory
        Get
            Return _ProviderFactory
        End Get
    End Property

    Private _ConnectionStringBuilder As DbConnectionStringBuilder
    Public ReadOnly Property ConnectionStringBuilder As DbConnectionStringBuilder Implements IDb.ConnectionStringBuilder
        Get
            Return _ConnectionStringBuilder
        End Get
    End Property

    Public Property CommandTimeout As Integer = 30 Implements IDb.CommandTimeout

    Public Sub New(ByVal factory As IDbSettingsFactory)
        Me.New(factory.GetSettings())
    End Sub

    Public Sub New(ByVal settings As DbSettings)
        _ProviderFactory = DbProviderFactories.GetFactory(settings.ProviderName)
        _ConnectionStringBuilder = _ProviderFactory.CreateConnectionStringBuilder()
        _ConnectionStringBuilder.ConnectionString = settings.ConnectionString
    End Sub

    Public Function CreateConnection() As DbConnection Implements IDb.CreateConnection
        Dim conn = _ProviderFactory.CreateConnection()
        conn.ConnectionString = _ConnectionStringBuilder.ConnectionString
        Return conn
    End Function

    Public Function CreateCommand(ByVal text As String) As DbCommand Implements IDb.CreateCommand
        Return Me.CreateCommand(text, CommandType.Text)
    End Function

    Public Function CreateCommand(ByVal text As String, ByVal type As CommandType) As DbCommand Implements IDb.CreateCommand
        Dim cmd = _ProviderFactory.CreateCommand()
        cmd.CommandText = text
        cmd.CommandType = type
        cmd.CommandTimeout = Me.CommandTimeout

        Return New FluentCommand(Me, cmd)
    End Function

    Public Function OpenConnection() As DbConnection Implements IDb.OpenConnection
        Dim conn = Me.CreateConnection()
        conn.Open(Me.ConnectionRetryCount)

        Return conn
    End Function

    Public Function OpenConnectionAsync() As Task(Of DbConnection) Implements IDb.OpenConnectionAsync
        Return Me.OpenConnectionAsync(CancellationToken.None)
    End Function

    Public Async Function OpenConnectionAsync(cancellationToken As CancellationToken) As Task(Of DbConnection) Implements IDb.OpenConnectionAsync
        Dim conn = Me.CreateConnection()
        Await conn.OpenAsync(cancellationToken)

        Return conn
    End Function

End Class