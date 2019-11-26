Imports System.Windows.Controls
Imports Symmex.Snowflake.Common

Public Class MenuViewModel
    Inherits ItemsViewModel

    Private _Orientation As Orientation
    Public Property Orientation As Orientation
        Get
            Return _Orientation
        End Get
        Set(value As Orientation)
            _Orientation = value
            Me.OnPropertyChanged("Orientation")
        End Set
    End Property

End Class