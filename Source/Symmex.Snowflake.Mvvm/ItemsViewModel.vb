Imports System.Collections
Imports System.Collections.ObjectModel

Public Class ItemsViewModel
    Inherits ViewModel

    Private _Items As ObservableCollection(Of Object)
    Public ReadOnly Property Items As ObservableCollection(Of Object)
        Get
            If _Items Is Nothing Then
                _Items = New ObservableCollection(Of Object)()
            End If

            Return _Items
        End Get
    End Property

    Private _ItemsSource As IEnumerable
    Public Property ItemsSource As IEnumerable
        Get
            Return _ItemsSource
        End Get
        Set(value As IEnumerable)
            _ItemsSource = value
            Me.OnItemsSourceChanged()
        End Set
    End Property

    Protected Overridable Sub OnItemsSourceChanged()
        Me.Items.Clear()

        Dim source = Me.ItemsSource
        If source IsNot Nothing Then
            For Each item In source
                Me.Items.Add(Me.GetItemForSource(item))
            Next
        End If
    End Sub

    Protected Overridable Function GetItemForSource(source As Object) As Object
        Return source
    End Function

End Class