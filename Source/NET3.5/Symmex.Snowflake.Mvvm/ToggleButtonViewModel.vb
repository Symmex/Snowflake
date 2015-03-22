Public Class ToggleButtonViewModel
    Inherits ButtonViewModelBase

    Private _IsChecked As Boolean
    Public Overridable Property IsChecked As Boolean
        Get
            Return _IsChecked
        End Get
        Set(value As Boolean)
            _IsChecked = value
            Me.OnPropertyChanged("IsChecked")
        End Set
    End Property

End Class