Imports System.Data.Common
Imports System.Threading

Public Class FluentCommand
    Inherits DbCommand

    Private _Db As IDb
    Private _InnerCommand As DbCommand

    Public Overrides Property CommandText As String
        Get
            Return _InnerCommand.CommandText
        End Get
        Set(value As String)
            _InnerCommand.CommandText = value
        End Set
    End Property

    Public Overrides Property CommandTimeout As Integer
        Get
            Return _InnerCommand.CommandTimeout
        End Get
        Set(value As Integer)
            _InnerCommand.CommandTimeout = value
        End Set
    End Property

    Public Overrides Property CommandType As CommandType
        Get
            Return _InnerCommand.CommandType
        End Get
        Set(value As CommandType)
            _InnerCommand.CommandType = value
        End Set
    End Property

    Public Overrides Property DesignTimeVisible As Boolean
        Get
            Return _InnerCommand.DesignTimeVisible
        End Get
        Set(value As Boolean)
            _InnerCommand.DesignTimeVisible = value
        End Set
    End Property

    Public Overrides Property UpdatedRowSource As UpdateRowSource
        Get
            Return _InnerCommand.UpdatedRowSource
        End Get
        Set(value As UpdateRowSource)
            _InnerCommand.UpdatedRowSource = value
        End Set
    End Property

    Protected Overrides Property DbConnection As DbConnection
        Get
            Return _InnerCommand.Connection
        End Get
        Set(value As DbConnection)
            _InnerCommand.Connection = value
        End Set
    End Property

    Protected Overrides ReadOnly Property DbParameterCollection As DbParameterCollection
        Get
            Return _InnerCommand.Parameters
        End Get
    End Property

    Protected Overrides Property DbTransaction As DbTransaction
        Get
            Return _InnerCommand.Transaction
        End Get
        Set(value As DbTransaction)
            _InnerCommand.Transaction = value
        End Set
    End Property

    Public Sub New(db As IDb, innerCommand As DbCommand)
        _Db = db
        _InnerCommand = innerCommand
    End Sub

    Public Overrides Sub Cancel()
        _InnerCommand.Cancel()
    End Sub

    Public Overrides Sub Prepare()
        _InnerCommand.Prepare()
    End Sub

    Public Overrides Function ExecuteNonQuery() As Integer
        Using conn = _Db.OpenConnection()
            _InnerCommand.Connection = conn
            Return _InnerCommand.ExecuteNonQuery()
        End Using
    End Function

    Public Overrides Function ExecuteScalar() As Object
        Using conn = _Db.OpenConnection()
            _InnerCommand.Connection = conn
            Return _InnerCommand.ExecuteScalar()
        End Using
    End Function

    Protected Overrides Function CreateDbParameter() As DbParameter
        Return _InnerCommand.CreateParameter()
    End Function

    Protected Overrides Function ExecuteDbDataReader(behavior As CommandBehavior) As DbDataReader
        Dim conn = _Db.OpenConnection()
        _InnerCommand.Connection = conn
        Return _InnerCommand.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Protected Overrides Sub Dispose(disposing As Boolean)
        MyBase.Dispose(disposing)

        If disposing Then
            _InnerCommand.Dispose()
        End If
    End Sub

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    Protected Overrides Async Function ExecuteDbDataReaderAsync(behavior As CommandBehavior, cancellationToken As CancellationToken) As Task(Of DbDataReader)
        Dim conn = Await _Db.OpenConnectionAsync(cancellationToken)
        _InnerCommand.Connection = conn
        Dim reader = Await _InnerCommand.ExecuteReaderAsync(CommandBehavior.CloseConnection)

        Return reader
    End Function

    Public Overrides Async Function ExecuteNonQueryAsync(cancellationToken As CancellationToken) As Task(Of Integer)
        Using conn = Await _Db.OpenConnectionAsync(cancellationToken)
            _InnerCommand.Connection = conn
            Return Await _InnerCommand.ExecuteNonQueryAsync()
        End Using
    End Function

    Public Overrides Async Function ExecuteScalarAsync(cancellationToken As CancellationToken) As Task(Of Object)
        Using conn = Await _Db.OpenConnectionAsync(cancellationToken)
            _InnerCommand.Connection = conn
            Return Await _InnerCommand.ExecuteScalarAsync()
        End Using
    End Function
#End If

End Class