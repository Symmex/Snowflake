Imports System.Collections
Imports System.Collections.Specialized

Public Class CollectionFilter(Of T)
    Implements INotifyCollectionChanged, IEnumerable(Of T)

#Region "Events"
    Public Event CollectionChanged(sender As Object, e As NotifyCollectionChangedEventArgs) Implements INotifyCollectionChanged.CollectionChanged
#End Region

#Region "Properties"
    Private _Items As IEnumerable(Of T)
    Public Property Items As IEnumerable(Of T)
        Get
            Return _Items
        End Get
        Set(value As IEnumerable(Of T))
            _Items = value
            Me.FilterItems()
        End Set
    End Property

    Private _FilteredItems As List(Of T)
    Private ReadOnly Property FilteredItems As List(Of T)
        Get
            If _FilteredItems Is Nothing Then
                _FilteredItems = New List(Of T)()
            End If

            Return _FilteredItems
        End Get
    End Property

    Private _Filter As Func(Of T, Boolean)
    Public Property Filter As Func(Of T, Boolean)
        Get
            Return _Filter
        End Get
        Set(value As Func(Of T, Boolean))
            _Filter = value
            Me.FilterItems()
        End Set
    End Property
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(items As IEnumerable(Of T))
        _Items = items
        Me.FilterItems()
    End Sub
#End Region

#Region "Methods"
    Public Function GetEnumerator() As IEnumerator(Of T) Implements IEnumerable(Of T).GetEnumerator
        Return Me.FilteredItems.GetEnumerator()
    End Function

    Private Function IEnumerable_GetEnumerator() As IEnumerator Implements IEnumerable.GetEnumerator
        Return Me.GetEnumerator()
    End Function

    Private Sub OnCollectionChanged()
        RaiseEvent CollectionChanged(Me, New NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset))
    End Sub

    Public Sub FilterItems()
        Me.FilteredItems.Clear()

        If Me.Items IsNot Nothing Then
            If Me.Filter Is Nothing Then
                Me.FilteredItems.AddRange(Me.Items)
            Else
                Me.FilteredItems.AddRange(From item In _Items Where Me.Filter.Invoke(item))
            End If
        End If

        Me.OnCollectionChanged()
    End Sub
#End Region

End Class