Imports System.Runtime.CompilerServices

Public Module DataReaderExtensions

    <Extension()>
    Public Function ToSingle(Of T)(ByVal dataReader As IDataReader, ByVal factoryMethod As Func(Of IDataReader, T)) As T
        Using dataReader
            dataReader.Read()
            Return factoryMethod.Invoke(dataReader)
        End Using
    End Function

    <Extension()>
    Public Function ToSingleOrDefault(Of T)(ByVal dataReader As IDataReader, ByVal factoryMethod As Func(Of IDataReader, T)) As T
        Using dataReader
            If dataReader.Read() Then
                Return factoryMethod.Invoke(dataReader)
            Else
                Return Nothing
            End If
        End Using
    End Function

    <Extension()>
    Function ToList(Of T)(ByVal dataReader As IDataReader, ByVal factoryMethod As Func(Of IDataReader, T)) As List(Of T)
        Using dataReader
            Dim list As New List(Of T)()
            While dataReader.Read()
                list.Add(factoryMethod.Invoke(dataReader))
            End While
            Return list
        End Using
    End Function

End Module