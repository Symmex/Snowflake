Imports System.Runtime.CompilerServices

Public Module DictionaryExtensions

    <Extension()>
    Function GetValue(Of T As TValue, TKey, TValue)(source As Dictionary(Of TKey, TValue), key As TKey) As T
        Return DirectCast(source.Item(key), T)
    End Function

    <Extension()>
    Function TryGetValue(Of T As TValue, TKey, TValue)(source As Dictionary(Of TKey, TValue), key As TKey, ByRef value As T) As Boolean
        Dim val As TValue
        If source.TryGetValue(key, val) Then
            value = DirectCast(val, T)
            Return True
        Else
            Return False
        End If
    End Function

End Module