Public Class FluentCommand
    Implements IDbCommand

#Region "Properties"
    Private _Db As IDb
    Private _InnerCommand As IDbCommand

    Public Property CommandText As String Implements System.Data.IDbCommand.CommandText
        Get
            Return _InnerCommand.CommandText
        End Get
        Set(ByVal value As String)
            _InnerCommand.CommandText = value
        End Set
    End Property

    Public Property CommandTimeout As Integer Implements System.Data.IDbCommand.CommandTimeout
        Get
            Return _InnerCommand.CommandTimeout
        End Get
        Set(ByVal value As Integer)
            _InnerCommand.CommandTimeout = value
        End Set
    End Property

    Public Property CommandType As System.Data.CommandType Implements System.Data.IDbCommand.CommandType
        Get
            Return _InnerCommand.CommandType
        End Get
        Set(ByVal value As System.Data.CommandType)
            _InnerCommand.CommandType = value
        End Set
    End Property

    Public Property Connection As System.Data.IDbConnection Implements System.Data.IDbCommand.Connection
        Get
            Return _InnerCommand.Connection
        End Get
        Set(ByVal value As System.Data.IDbConnection)
            _InnerCommand.Connection = value
        End Set
    End Property

    Public ReadOnly Property Parameters As System.Data.IDataParameterCollection Implements System.Data.IDbCommand.Parameters
        Get
            Return _InnerCommand.Parameters
        End Get
    End Property

    Public Property Transaction As System.Data.IDbTransaction Implements System.Data.IDbCommand.Transaction
        Get
            Return _InnerCommand.Transaction
        End Get
        Set(ByVal value As System.Data.IDbTransaction)
            _InnerCommand.Transaction = value
        End Set
    End Property

    Public Property UpdatedRowSource As System.Data.UpdateRowSource Implements System.Data.IDbCommand.UpdatedRowSource
        Get
            Return _InnerCommand.UpdatedRowSource
        End Get
        Set(ByVal value As System.Data.UpdateRowSource)
            _InnerCommand.UpdatedRowSource = value
        End Set
    End Property
#End Region

#Region "Constructors"
    Public Sub New(ByVal db As IDb, ByVal innerCommand As IDbCommand)
        _Db = db
        _InnerCommand = innerCommand
    End Sub
#End Region

#Region "Methods"
    Public Sub Cancel() Implements System.Data.IDbCommand.Cancel
        _InnerCommand.Cancel()
    End Sub

    Public Function CreateParameter() As System.Data.IDbDataParameter Implements System.Data.IDbCommand.CreateParameter
        Return _InnerCommand.CreateParameter()
    End Function

    Public Function ExecuteNonQuery() As Integer Implements System.Data.IDbCommand.ExecuteNonQuery
        Using conn = _Db.OpenConnection()
            Me.Connection = conn
            Return _InnerCommand.ExecuteNonQuery()
        End Using
    End Function

    Public Function ExecuteReader() As System.Data.IDataReader Implements System.Data.IDbCommand.ExecuteReader
        Return Me.ExecuteReader(CommandBehavior.CloseConnection)
    End Function

    Private Function ExecuteReader(ByVal behavior As System.Data.CommandBehavior) As System.Data.IDataReader Implements System.Data.IDbCommand.ExecuteReader
        Dim conn = _Db.OpenConnection()
        Me.Connection = conn
        Return _InnerCommand.ExecuteReader(behavior)
    End Function

    Public Function ExecuteScalar() As Object Implements System.Data.IDbCommand.ExecuteScalar
        Using conn = _Db.OpenConnection()
            Me.Connection = conn
            Return _InnerCommand.ExecuteScalar()
        End Using
    End Function

    Public Sub Prepare() Implements System.Data.IDbCommand.Prepare
        _InnerCommand.Prepare()
    End Sub
#End Region

#Region "IDisposable Support"
    Private disposedValue As Boolean

    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me.disposedValue Then
            If disposing Then
                _InnerCommand.Dispose()
            End If
        End If

        Me.disposedValue = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class