Public MustInherit Class CommandBehavior
    Inherits Behavior

    Public Shared ReadOnly CommandProperty As DependencyProperty = DependencyProperty.Register("Command", GetType(ICommand), GetType(CommandBehavior))
    Public Property Command As ICommand
        Get
            Return DirectCast(Me.GetValue(CommandProperty), ICommand)
        End Get
        Set(ByVal value As ICommand)
            Me.SetValue(CommandProperty, value)
        End Set
    End Property

    Public Shared ReadOnly CommandParameterProperty As DependencyProperty = DependencyProperty.Register("CommandParameter", GetType(Object), GetType(CommandBehavior))
    Public Property CommandParameter As Object
        Get
            Return Me.GetValue(CommandParameterProperty)
        End Get
        Set(ByVal value As Object)
            Me.SetValue(CommandParameterProperty, value)
        End Set
    End Property

    Protected Sub ExecuteCommand()
        Me.ExecuteCommand(Me.CommandParameter)
    End Sub

    Protected Overridable Sub ExecuteCommand(parameter As Object)
        Dim cmd = Me.Command

        If cmd IsNot Nothing AndAlso cmd.CanExecute(parameter) Then
            cmd.Execute(parameter)
        End If
    End Sub

End Class