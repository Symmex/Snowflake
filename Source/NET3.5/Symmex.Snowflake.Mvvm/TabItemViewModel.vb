Public Class TabItemViewModel
    Inherits HeaderedContentViewModel

    Private _IsSelected As Boolean
    Public Property IsSelected As Boolean
        Get
            Return _IsSelected
        End Get
        Set(ByVal value As Boolean)
            _IsSelected = value
            Me.OnPropertyChanged("IsSelected")
        End Set
    End Property

End Class