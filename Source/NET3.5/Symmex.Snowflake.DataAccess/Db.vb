Imports System.Data.Common
Imports System.Configuration

Public Class Db
    Implements IDb

    Public Property ConnectionRetryCount As Integer Implements IDb.ConnectionRetryCount

    Private _ProviderFactory As DbProviderFactory
    Public ReadOnly Property ProviderFactory As System.Data.Common.DbProviderFactory Implements IDb.ProviderFactory
        Get
            Return _ProviderFactory
        End Get
    End Property

    Private _ConnectionStringBuilder As DbConnectionStringBuilder
    Public ReadOnly Property ConnectionStringBuilder As System.Data.Common.DbConnectionStringBuilder Implements IDb.ConnectionStringBuilder
        Get
            Return _ConnectionStringBuilder
        End Get
    End Property

    Public Sub New(ByVal factory As IDbSettingsFactory)
        Me.New(factory.GetSettings())
    End Sub

    Public Sub New(ByVal settings As DbSettings)
        _ProviderFactory = DbProviderFactories.GetFactory(settings.ProviderName)
        _ConnectionStringBuilder = _ProviderFactory.CreateConnectionStringBuilder()
        _ConnectionStringBuilder.ConnectionString = settings.ConnectionString
    End Sub

    Public Function CreateConnection() As System.Data.IDbConnection Implements IDb.CreateConnection
        Dim conn = _ProviderFactory.CreateConnection()
        conn.ConnectionString = _ConnectionStringBuilder.ConnectionString
        Return conn
    End Function

    Public Function CreateCommand(ByVal text As String) As System.Data.IDbCommand Implements IDb.CreateCommand
        Return Me.CreateCommand(text, CommandType.Text)
    End Function

    Public Function CreateCommand(ByVal text As String, ByVal type As CommandType) As System.Data.IDbCommand Implements IDb.CreateCommand
        Dim cmd = _ProviderFactory.CreateCommand()
        cmd.CommandText = text
        cmd.CommandType = type

        Return New FluentCommand(Me, cmd)
    End Function

    Public Function OpenConnection() As IDbConnection Implements IDb.OpenConnection
        Dim conn = Me.CreateConnection()
        conn.Open(Me.ConnectionRetryCount)

        Return conn
    End Function

End Class