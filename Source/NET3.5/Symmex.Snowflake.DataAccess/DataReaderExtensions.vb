#If TargetFramework >= 4.0 Then
Imports System.Threading.Tasks
#End If
Imports System.Runtime.CompilerServices
Imports System.Data.Common
Imports Symmex.Snowflake.Common

Public Module DataReaderExtensions

    <Extension()>
    Public Function ToSingle(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As T
        Using dataReader
            dataReader.Read()
            Return factoryMethod.Invoke(dataReader)
        End Using
    End Function

    <Extension()>
    Public Function ToSingleOrDefault(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As T
        Using dataReader
            If dataReader.Read() Then
                Return factoryMethod.Invoke(dataReader)
            Else
                Return Nothing
            End If
        End Using
    End Function

    <Extension()>
    Function ToList(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As List(Of T)
        Using dataReader
            Dim list As New List(Of T)()
            While dataReader.Read()
                list.Add(factoryMethod.Invoke(dataReader))
            End While
            Return list
        End Using
    End Function



#If TargetFramework = 4.0 Then
    <Extension()>
    Public Function ReadAsync(reader As DbDataReader) As Task(Of Boolean)
        Return Task.Factory.FromResult(reader.Read())
    End Function
#End If

#If TargetFramework >= 4.0 Then
    <Extension()>
    Public Function ToSingleAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of T)
        If dataReader Is Nothing Then
            Throw New ArgumentNullException("dataReader")
        End If

        If factoryMethod Is Nothing Then
            Throw New ArgumentNullException("factoryMethod")
        End If

        Return dataReader.ReadAsync() _
                .ContinueWith(Function(ct)
                                  Try
                                      Return factoryMethod.Invoke(dataReader)
                                  Finally
                                      dataReader.Dispose()
                                  End Try
                              End Function)
    End Function

    <Extension()>
    Public Function ToSingleOrDefaultAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of T)
        If dataReader Is Nothing Then
            Throw New ArgumentNullException("dataReader")
        End If

        If factoryMethod Is Nothing Then
            Throw New ArgumentNullException("factoryMethod")
        End If

        Return dataReader.ReadAsync() _
                .ContinueWith(Function(ct)
                                  If ct.Result Then
                                      Try
                                          Return factoryMethod.Invoke(dataReader)
                                      Finally
                                          dataReader.Dispose()
                                      End Try
                                  Else
                                      dataReader.Dispose()
                                      Return Nothing
                                  End If
                              End Function)
    End Function

    <Extension()>
    Public Function ToListAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of List(Of T))
        If dataReader Is Nothing Then
            Throw New ArgumentNullException("dataReader")
        End If

        If factoryMethod Is Nothing Then
            Throw New ArgumentNullException("factoryMethod")
        End If

        Dim completionSource As New TaskCompletionSource(Of List(Of T))()
        Dim list As New List(Of T)()

        ReadIntoListAsync(dataReader, factoryMethod, list, completionSource)

        Return completionSource.Task
    End Function

    Private Sub ReadIntoListAsync(Of T)(dataReader As DbDataReader, factoryMethod As Func(Of DbDataReader, T), list As List(Of T), completionSource As TaskCompletionSource(Of List(Of T)))
        dataReader.ReadAsync() _
            .ContinueWith(Sub(ct)
                              If ct.IsFaulted Then
                                  completionSource.SetException(ct.Exception)
                                  dataReader.Dispose()
                                  Exit Sub
                              End If

                              If ct.Result Then
                                  Try
                                      list.Add(factoryMethod.Invoke(dataReader))
                                      ReadIntoListAsync(dataReader, factoryMethod, list, completionSource)
                                  Catch ex As Exception
                                      completionSource.SetException(ex)
                                      dataReader.Dispose()
                                      Exit Sub
                                  End Try
                              Else
                                  completionSource.SetResult(list)
                                  dataReader.Dispose()
                              End If
                          End Sub)
    End Sub
#End If

End Module