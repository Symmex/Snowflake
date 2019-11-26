Imports System.Data.SqlClient
Imports System.Data.Common

Public Class SqlScriptManager
    Inherits DataScriptManager

    Protected Overrides ReadOnly Property BatchSeparator As String
        Get
            Return "GO"
        End Get
    End Property

    Protected Overrides ReadOnly Property ParameterPrefix As String
        Get
            Return "@"
        End Get
    End Property

    Protected Overrides ReadOnly Property DataScriptInfoQuerystring As String
        Get
            Return My.Resources.GetDataScriptInfos
        End Get
    End Property

    Protected Overrides ReadOnly Property DataScriptInfoInsertString As String
        Get
            Return My.Resources.InsertDataScriptInfo
        End Get
    End Property

    Protected Overrides ReadOnly Property DataScriptInfoUpdateString As String
        Get
            Return My.Resources.UpdateDataScriptInfo
        End Get
    End Property

    Protected Overrides ReadOnly Property EnsureDatabaseExistsString As String
        Get
            Return My.Resources.EnsureDatabase
        End Get
    End Property

    Protected Overrides ReadOnly Property EnsureDataScriptInfoTableExistsString As String
        Get
            Return My.Resources.EnsureDataScriptInfoTable
        End Get
    End Property

    Public Sub New(ByVal connectionString As String, databaseName As String)
        MyBase.New(SqlClientFactory.Instance, connectionString, databaseName)
    End Sub

    Protected Overrides Function OpenConnection(connectionString As String, databaseName As String) As IDbConnection
        Dim builder = New SqlConnectionStringBuilder(connectionString)
        If databaseName IsNot Nothing Then
            builder.InitialCatalog = databaseName
        End If

        Dim connection = New SqlConnection(builder.ConnectionString)
        connection.Open()

        Return connection
    End Function

End Class