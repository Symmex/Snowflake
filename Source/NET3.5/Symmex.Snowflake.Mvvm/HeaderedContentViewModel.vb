Public Class HeaderedContentViewModel
    Inherits ContentViewModel
    Implements IHeaderedViewModel

    Private _Header As Object
    Public Overridable Property Header As Object Implements IHeaderedViewModel.Header
        Get
            Return _Header
        End Get
        Set(ByVal value As Object)
            _Header = value
            Me.OnPropertyChanged("Header")
        End Set
    End Property

End Class