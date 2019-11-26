Imports System.Windows.Input

Public Class DelegateCommand
    Implements ICommand

    Public Event CanExecuteChanged(ByVal sender As Object, ByVal e As System.EventArgs) Implements ICommand.CanExecuteChanged

    Private _ExecuteMethod As Action(Of Object)
    Private _CanExecuteMethod As Func(Of Object, Boolean)

    Public Sub New(ByVal executeMethod As Action)
        Me.New(Sub(parameter As Object) executeMethod.Invoke(), Function(parameter As Object) True)
    End Sub

    Public Sub New(ByVal executeMethod As Action, ByVal canExecuteMethod As Func(Of Object, Boolean))
        Me.New(Sub(parameter As Object) executeMethod.Invoke(), canExecuteMethod)
    End Sub

    Public Sub New(ByVal executeMethod As Action(Of Object))
        Me.New(executeMethod, Function(parameter As Object) True)
    End Sub

    Public Sub New(ByVal executeMethod As Action(Of Object), ByVal canExecuteMethod As Func(Of Object, Boolean))
        _ExecuteMethod = executeMethod
        _CanExecuteMethod = canExecuteMethod
    End Sub

    Public Function CanExecute(ByVal parameter As Object) As Boolean Implements System.Windows.Input.ICommand.CanExecute
        Return _CanExecuteMethod.Invoke(parameter)
    End Function

    Public Sub Execute(ByVal parameter As Object) Implements System.Windows.Input.ICommand.Execute
        _ExecuteMethod.Invoke(parameter)
    End Sub

    Public Sub OnCanExecuteChanged()
        RaiseEvent CanExecuteChanged(Me, EventArgs.Empty)
    End Sub

End Class