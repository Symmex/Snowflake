Public Class ToolTipViewModel
    Inherits ContentViewModel

    Private _IsOpen As Boolean
    Public Overridable Property IsOpen As Boolean
        Get
            Return _IsOpen
        End Get
        Set(ByVal value As Boolean)
            _IsOpen = value
            Me.OnPropertyChanged("IsOpen")
        End Set
    End Property

End Class