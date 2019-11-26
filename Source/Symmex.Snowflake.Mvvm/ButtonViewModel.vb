Imports System.Windows.Media
Imports System.Windows.Input
Imports System.Windows.Controls
Imports Symmex.Snowflake.Common
Imports Symmex.Snowflake.Mvvm

Public Class ButtonViewModel
    Inherits ButtonViewModelBase

    Private _IsDefault As Boolean
    Public Property IsDefault As Boolean
        Get
            Return _IsDefault
        End Get
        Set(value As Boolean)
            _IsDefault = value
            Me.OnPropertyChanged("IsDefault")
        End Set
    End Property

    Private _IsCancel As Boolean
    Public Property IsCancel As Boolean
        Get
            Return _IsCancel
        End Get
        Set(value As Boolean)
            _IsCancel = value
            Me.OnPropertyChanged("IsCancel")
        End Set
    End Property

End Class