<Serializable()>
Public Class SortedCollection(Of TKey, TValue)
    Implements IEnumerable(Of TValue)

    Private _Items As SortedList(Of TKey, TValue)
    Protected ReadOnly Property Items As SortedList(Of TKey, TValue)
        Get
            If _Items Is Nothing Then
                _Items = New SortedList(Of TKey, TValue)()
            End If

            Return _Items
        End Get
    End Property

    Default Public Property Item(key As TKey) As TValue
        Get
            Return Me.Items.Item(key)
        End Get
        Set(value As TValue)
            Me.Items.Item(key) = value
        End Set
    End Property

    Public Overridable Sub Add(key As TKey, value As TValue)
        Me.Items.Add(key, value)
    End Sub

    Public Overridable Sub Remove(key As TKey)
        Me.Items.Remove(key)
    End Sub

    Public Function ContainsKey(key As TKey) As Boolean
        Return Me.Items.ContainsKey(key)
    End Function

    Public Function ContainsItem(value As TValue) As Boolean
        Return Me.Items.ContainsValue(value)
    End Function

    Public Overridable Sub Clear()
        Me.Items.Clear()
    End Sub

    Public Function GetEnumerator() As IEnumerator(Of TValue) Implements IEnumerable(Of TValue).GetEnumerator
        Return Me.Items.Values.GetEnumerator()
    End Function

    Private Function IEnumerableGetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumerator()
    End Function

End Class