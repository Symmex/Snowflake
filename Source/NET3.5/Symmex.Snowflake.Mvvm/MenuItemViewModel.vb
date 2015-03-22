Imports System.Windows.Media
Imports System.Windows.Input
Imports Symmex.Snowflake.Common

Public Class MenuItemViewModel
    Inherits HeaderedItemsViewModel
    Implements ICommandViewModel

    Public Property IsSeparator As Boolean

    Private _Image As ImageSource
    Public Property Image As ImageSource
        Get
            Return _Image
        End Get
        Set(value As ImageSource)
            _Image = value
            Me.OnPropertyChanged("Image")
        End Set
    End Property

    Private _Command As ICommand
    Public Property Command As ICommand Implements ICommandViewModel.Command
        Get
            Return _Command
        End Get
        Set(value As ICommand)
            _Command = value
            Me.OnPropertyChanged("Command")
        End Set
    End Property

    Private _CommandParameter As Object
    Public Property CommandParameter As Object Implements ICommandViewModel.CommandParameter
        Get
            Return _CommandParameter
        End Get
        Set(value As Object)
            _CommandParameter = value
            Me.OnPropertyChanged("CommandParameter")
        End Set
    End Property

End Class