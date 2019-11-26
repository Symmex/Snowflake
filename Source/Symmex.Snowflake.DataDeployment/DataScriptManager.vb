Imports System.IO
Imports System.Text
Imports System.Xml.Serialization
Imports System.Security.Cryptography
Imports System.Data.Common

Public MustInherit Class DataScriptManager

    Public Event ScriptExecuted(sender As Object, e As DataScriptExecutedEventArgs)

    Private _IsInitialized As Boolean
    Private _IsPrepared As Boolean

    Private _ConnectionString As String
    Private _DatabaseName As String

    Protected MustOverride ReadOnly Property BatchSeparator As String
    Protected MustOverride ReadOnly Property ParameterPrefix As String
    Protected MustOverride ReadOnly Property DataScriptInfoQueryString As String
    Protected MustOverride ReadOnly Property DataScriptInfoInsertString As String
    Protected MustOverride ReadOnly Property DataScriptInfoUpdateString As String
    Protected MustOverride ReadOnly Property EnsureDatabaseExistsString As String
    Protected MustOverride ReadOnly Property EnsureDataScriptInfoTableExistsString As String

    Public Property CommandTimeout As Integer

    Private _LoadedScripts As DataScriptCollection
    Public ReadOnly Property LoadedScripts As DataScriptCollection
        Get
            If _LoadedScripts Is Nothing Then
                _LoadedScripts = New DataScriptCollection()
            End If

            Return _LoadedScripts
        End Get
    End Property

    Private _ExistingScripts As DataScriptInfoCollection
    Private ReadOnly Property ExistingScripts As DataScriptInfoCollection
        Get
            If _ExistingScripts Is Nothing Then
                _ExistingScripts = New DataScriptInfoCollection()
            End If

            Return _ExistingScripts
        End Get
    End Property

    Private _OutdatedScripts As DataScriptCollection
    Public ReadOnly Property OutdatedScripts As DataScriptCollection
        Get
            If _OutdatedScripts Is Nothing Then
                _OutdatedScripts = New DataScriptCollection()
            End If

            Return _OutdatedScripts
        End Get
    End Property

    Private _HashAlgorithm As HashAlgorithm
    Private ReadOnly Property HashAlgorithm As HashAlgorithm
        Get
            If _HashAlgorithm Is Nothing Then
                _HashAlgorithm = New SHA256Managed()
            End If

            Return _HashAlgorithm
        End Get
    End Property

    Protected Sub New(provider As DbProviderFactory, connectionString As String, databaseName As String)
        If provider Is Nothing Then
            Throw New ArgumentNullException("provider")
        End If

        If connectionString Is Nothing Then
            Throw New ArgumentNullException("connectionString")
        End If

        If databaseName Is Nothing Then
            Throw New ArgumentNullException("databaseName")
        End If

        _ConnectionString = connectionString
        _DatabaseName = databaseName

        Me.Initialize()
    End Sub

    Public Sub LoadScripts(source As String)
        If source Is Nothing Then
            Throw New ArgumentNullException("source")
        End If

        Dim scriptStrings = source.Split(New String() {"--$"}, StringSplitOptions.RemoveEmptyEntries)

        Dim serializer As New XmlSerializer(GetType(DataScript))
        For Each scriptString In scriptStrings
            Dim scriptReader As New StringReader(scriptString)
            Dim scriptHeader = scriptReader.ReadLine()
            Dim script As DataScript

            Try
                script = DirectCast(serializer.Deserialize(New StringReader(scriptHeader)), DataScript)
            Catch ex As Exception
                Dim message = String.Format("An error occured while deserializing a script header.{0}Header: {1}", Environment.NewLine, scriptHeader)
                Throw New Exception(message, ex)
            End Try

            If Me.LoadedScripts.Contains(script.Id) Then
                Dim message = String.Format("A script with Id '{0}' has already been loaded.", script.Id)
                Throw New Exception(message)
            Else
                script.Text = scriptReader.ReadToEnd().Trim()
                script.Hash = Me.ComputeHash(script.Text)
                Me.LoadedScripts.Add(script)
            End If
        Next
    End Sub

    Private Sub Initialize()
        If Not _IsInitialized Then
            Me.EnsureDatabaseExists()
            Me.EnsureDataScriptInfoTableExists()
            _IsInitialized = True
        End If
    End Sub

    Public Sub PrepareScripts()
        If Not _IsPrepared Then
            Me.OutdatedScripts.Clear()
            Me.ExistingScripts.Clear()

            Using conn = OpenConnection()
                Using cmd = conn.CreateCommand()
                    cmd.CommandText = Me.DataScriptInfoQueryString

                    Using dataReader = cmd.ExecuteReader()
                        While dataReader.Read()
                            Me.ExistingScripts.Add(Me.CreateDataScriptInfo(dataReader))
                        End While
                    End Using
                End Using
            End Using

            For Each script In Me.LoadedScripts
                If Me.ExistingScripts.Contains(script.Id) Then
                    Dim existingScript = Me.ExistingScripts(script.Id)
                    If script.Hash <> existingScript.Hash Then
                        Me.OutdatedScripts.Add(script)
                    End If
                Else
                    Me.OutdatedScripts.Add(script)
                End If
            Next

            _IsPrepared = True
        End If
    End Sub

    Public Sub ExecuteScripts()
        Me.ExecuteScripts(False)
    End Sub

    Public Sub ExecuteScripts(executeAll As Boolean)
        Dim scriptsToRun = Me.GetScriptsToRun(executeAll)

        Using conn = OpenConnection()
            For Each script In scriptsToRun
                Dim e As DataScriptExecutedEventArgs

                Try
                    Me.ExecuteScript(conn, script)
                    Me.OutdatedScripts.Remove(script)
                    e = New DataScriptExecutedEventArgs(script)
                Catch ex As Exception
                    e = New DataScriptExecutedEventArgs(script, New DataScriptExecutionException(script, ex))
                End Try

                Me.OnScriptExecuted(e)
                If e.Cancel Then
                    Exit For
                End If
            Next
        End Using
    End Sub

    Private Sub ExecuteScript(conn As IDbConnection, script As DataScript)
        Dim batches = Me.GetDataScriptBatches(script)

        Using tx = If(script.ExecuteInTransaction, conn.BeginTransaction(), Nothing)
            For Each batch In batches
                Using cmd = conn.CreateCommand()
                    cmd.CommandTimeout = Me.CommandTimeout
                    cmd.Transaction = tx
                    cmd.CommandText = batch
                    cmd.ExecuteNonQuery()
                End Using
            Next

            If tx IsNot Nothing Then
                tx.Commit()
            End If
        End Using

        'Update the script info with the new executed date and hash
        Me.UpdateScriptInfo(script, conn)
    End Sub

    Private Sub UpdateScriptInfo(script As DataScript, conn As IDbConnection)
        Dim scriptInfo = Me.CreateDataScriptInfo(script)

        If Me.ExistingScripts.Contains(script.Id) Then
            Me.UpdateDataScriptInfo(conn, scriptInfo)
        Else
            Me.InsertDataScriptInfo(conn, scriptInfo)
        End If
    End Sub

    Private Function GetScriptsToRun(executeAll As Boolean) As IEnumerable(Of DataScript)
        Me.PrepareScripts()

        Dim scriptsToRun As List(Of DataScript)
        If executeAll Then
            scriptsToRun = Me.LoadedScripts.ToList()
        Else
            scriptsToRun = Me.OutdatedScripts.ToList()
        End If

        Return scriptsToRun
    End Function

    Private Function CreateDataScriptInfo(script As DataScript) As DataScriptInfo
        Dim scriptInfo = New DataScriptInfo()
        scriptInfo.Id = script.Id
        scriptInfo.ExecutedDate = DateTimeOffset.Now
        scriptInfo.Hash = script.Hash

        Return scriptInfo
    End Function

    Private Sub EnsureDatabaseExists()
        Using conn = OpenConnection(Nothing)
            Using cmd = conn.CreateCommand()
                cmd.CommandText = String.Format(Me.EnsureDatabaseExistsString, _DatabaseName)
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Sub EnsureDataScriptInfoTableExists()
        Using conn = OpenConnection()
            Using cmd = conn.CreateCommand()
                cmd.CommandText = Me.EnsureDataScriptInfoTableExistsString
                cmd.ExecuteNonQuery()
            End Using
        End Using
    End Sub

    Private Function CreateDataScriptInfo(data As IDataRecord) As DataScriptInfo
        Dim item As New DataScriptInfo()
        item.Id = data.GetGuid(data.GetOrdinal("Id"))
        item.ExecutedDate = DirectCast(data.GetValue(data.GetOrdinal("ExecutedDate")), DateTimeOffset)
        item.Hash = data.GetString(data.GetOrdinal("Hash"))

        Return item
    End Function

    Private Function ComputeHash(text As String) As String
        Dim buffer = Encoding.UTF8.GetBytes(text)
        Dim hash = Me.HashAlgorithm.ComputeHash(buffer)
        Return Convert.ToBase64String(hash)
    End Function

    Private Function OpenConnection() As IDbConnection
        Return OpenConnection(_ConnectionString, _DatabaseName)
    End Function

    Private Function OpenConnection(databaseName As String) As IDbConnection
        Return OpenConnection(_ConnectionString, databaseName)
    End Function

    Protected MustOverride Function OpenConnection(connectionString As String, databaseName As String) As IDbConnection

    Private Sub InsertDataScriptInfo(conn As IDbConnection, script As DataScriptInfo)
        Using cmd = conn.CreateCommand()
            cmd.CommandText = Me.DataScriptInfoInsertString

            Me.AddParameter(cmd, "Id", DbType.Guid, script.Id)
            Me.AddParameter(cmd, "ExecutedDate", DbType.DateTimeOffset, script.ExecutedDate)
            Me.AddParameter(cmd, "Hash", DbType.String, script.Hash)

            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub UpdateDataScriptInfo(conn As IDbConnection, script As DataScriptInfo)
        Using cmd = conn.CreateCommand()
            cmd.CommandText = Me.DataScriptInfoUpdateString

            Me.AddParameter(cmd, "Id", DbType.Guid, script.Id)
            Me.AddParameter(cmd, "ExecutedDate", DbType.DateTimeOffset, script.ExecutedDate)
            Me.AddParameter(cmd, "Hash", DbType.String, script.Hash)

            cmd.ExecuteNonQuery()
        End Using
    End Sub

    Private Sub AddParameter(cmd As IDbCommand, parameterName As String, parameterType As DbType, value As Object)
        Dim param = cmd.CreateParameter()
        param.ParameterName = Me.ParameterPrefix + parameterName
        param.DbType = parameterType
        param.Value = value

        cmd.Parameters.Add(param)
    End Sub

    Private Function GetDataScriptBatches(script As DataScript) As String()
        Dim separator = New String() {Me.BatchSeparator}
        Return script.Text.Split(separator, StringSplitOptions.RemoveEmptyEntries)
    End Function

    Protected Overridable Sub OnScriptExecuted(e As DataScriptExecutedEventArgs)
        RaiseEvent ScriptExecuted(Me, e)
    End Sub

End Class