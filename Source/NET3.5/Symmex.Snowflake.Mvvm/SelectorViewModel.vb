Public Class SelectorViewModel
    Inherits ItemsViewModel

    Public Event SelectedItemChanged(sender As Object, e As EventArgs)

    Private _SelectedItem As Object
    Public Overridable Property SelectedItem As Object
        Get
            Return _SelectedItem
        End Get
        Set(value As Object)
            _SelectedItem = value
            Me.OnPropertyChanged("SelectedItem")
            Me.OnSelectedItemChanged()
        End Set
    End Property

    Protected Overridable Sub OnSelectedItemChanged()
        RaiseEvent SelectedItemChanged(Me, EventArgs.Empty)
    End Sub

End Class