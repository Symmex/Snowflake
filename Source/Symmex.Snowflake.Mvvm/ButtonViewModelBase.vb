Imports System.Windows.Media
Imports System.Windows.Input

Public Class ButtonViewModelBase
    Inherits ContentViewModel
    Implements ICommandViewModel

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