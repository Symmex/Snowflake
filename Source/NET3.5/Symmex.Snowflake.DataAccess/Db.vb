Imports System.Data.Common
Imports Symmex.Snowflake.DataAccess
Imports System.Threading
#If TargetFramework >= "4.0" Then
Imports System.Threading.Tasks
#End If

Public Class Db
    Implements IDb

    Private _IsDisposed As Boolean

    Public Property ConnectionRetryCount As Integer Implements IDb.ConnectionRetryCount
    Public Property CommandTimeout As Integer = 30 Implements IDb.CommandTimeout

    Private _Connection As DbConnection
    Public ReadOnly Property Connection As DbConnection Implements IDb.Connection
        Get
            Return _Connection
        End Get
    End Property

    Private _Transaction As DbTransaction
    Public ReadOnly Property Transaction As DbTransaction Implements IDb.Transaction
        Get
            Return _Transaction
        End Get
    End Property

    Public Sub New(connectionString As String, providerFactory As DbProviderFactory)
        _Connection = providerFactory.CreateConnection()
        _Connection.ConnectionString = connectionString
    End Sub

    Public Function CreateCommand(ByVal text As String) As DbCommand Implements IDb.CreateCommand
        Return Me.CreateCommand(text, CommandType.Text)
    End Function

    Public Function CreateCommand(ByVal text As String, ByVal type As CommandType) As DbCommand Implements IDb.CreateCommand
        Dim cmd = _Connection.CreateCommand()
        cmd.Transaction = _Transaction
        cmd.CommandText = text
        cmd.CommandType = type
        cmd.CommandTimeout = Me.CommandTimeout

        Return cmd
    End Function

    Public Sub OpenConnection() Implements IDb.OpenConnection
        For attempt = 0 To Me.ConnectionRetryCount
            Try
                _Connection.Open()
                Exit For
            Catch ex As Exception
                If attempt = Me.ConnectionRetryCount Then
                    Throw ex
                End If
            End Try
        Next
    End Sub

    Public Sub BeginTransaction() Implements IDb.BeginTransaction
        _Transaction = Me.Connection.BeginTransaction()
    End Sub

    Public Sub CommitTransaction() Implements IDb.CommitTransaction
        _Transaction.Commit()
    End Sub

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _IsDisposed Then
            If disposing Then
                _Transaction?.Dispose()
                _Connection?.Dispose()
            End If
        End If

        _IsDisposed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
    End Sub

#If TargetFramework >= "4.0" Then
    Public Function OpenConnectionAsync() As Task Implements IDb.OpenConnectionAsync
        'TODO: Support ConnectionRetryCount work with OpenConnectionAsync
        Return _Connection.OpenAsync()
    End Function
#End If

End Class