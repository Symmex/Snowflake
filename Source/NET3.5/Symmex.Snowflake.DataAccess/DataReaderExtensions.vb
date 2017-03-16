Imports System.Runtime.CompilerServices
Imports System.Data.Common

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

#If NETMajorVersion >= 4 AndAlso NETMinorVersion >= 5 Then
    <Extension()>
    Public Async Function ToSingleAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of T)
        Using dataReader
            Await dataReader.ReadAsync()
            Return factoryMethod.Invoke(dataReader)
        End Using
    End Function

    <Extension()>
    Public Async Function ToSingleOrDefaultAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of T)
        Using dataReader
            If Await dataReader.ReadAsync() Then
                Return factoryMethod.Invoke(dataReader)
            Else
                Return Nothing
            End If
        End Using
    End Function

    <Extension()>
    Public Async Function ToListAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, T)) As Task(Of List(Of T))
        Using dataReader
            Dim list As New List(Of T)()

            While Await dataReader.ReadAsync()
                list.Add(factoryMethod.Invoke(dataReader))
            End While

            Return list
        End Using
    End Function

    <Extension()>
    Public Async Function ToSingleAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, Task(Of T))) As Task(Of T)
        Using dataReader
            Await dataReader.ReadAsync()
            Return Await factoryMethod.Invoke(dataReader)
        End Using
    End Function

    <Extension()>
    Public Async Function ToSingleOrDefaultAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, Task(Of T))) As Task(Of T)
        Using dataReader
            If Await dataReader.ReadAsync() Then
                Return Await factoryMethod.Invoke(dataReader)
            Else
                Return Nothing
            End If
        End Using
    End Function

    <Extension()>
    Public Async Function ToListAsync(Of T)(ByVal dataReader As DbDataReader, ByVal factoryMethod As Func(Of DbDataReader, Task(Of T))) As Task(Of List(Of T))
        Using dataReader
            Dim list As New List(Of T)()

            While Await dataReader.ReadAsync()
                list.Add(Await factoryMethod.Invoke(dataReader))
            End While

            Return list
        End Using
    End Function
#End If

End Module