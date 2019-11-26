Public Class HeaderedItemsViewModel
    Inherits ItemsViewModel
    Implements IHeaderedViewModel

    Private _Header As Object
    Public Property Header As Object Implements IHeaderedViewModel.Header
        Get
            Return _Header
        End Get
        Set(value As Object)
            _Header = value
            Me.OnPropertyChanged("Header")
        End Set
    End Property

End Class