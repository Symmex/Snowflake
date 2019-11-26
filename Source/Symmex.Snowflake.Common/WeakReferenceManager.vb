Public Class WeakReferenceManager(Of T)

    Private _Items As List(Of WeakReference)
    Private ReadOnly Property Items As List(Of WeakReference)
        Get
            If _Items Is Nothing Then
                _Items = New List(Of WeakReference)()
            End If

            Return _Items
        End Get
    End Property

    Public Sub Add(ByVal item As T)
        Me.RemoveDeadItems()
        Me.Items.Add(New WeakReference(item))
    End Sub

    Public Sub Remove(ByVal item As T)
        Me.RemoveDeadItems()
        Dim index = Me.Items.FindIndex(Function(wr As WeakReference) Object.Equals(wr.Target, item))
        If index > -1 Then
            Me.Items.RemoveAt(index)
        End If
    End Sub

    Public Function GetAliveItems() As List(Of T)
        Me.RemoveDeadItems()
        Return (From item In Me.Items
                Where item.Target IsNot Nothing
                Select DirectCast(item.Target, T)).ToList()
    End Function

    Private Sub RemoveDeadItems()
        Me.Items.RemoveAll(Function(wr As WeakReference) wr.Target Is Nothing)
    End Sub

End Class