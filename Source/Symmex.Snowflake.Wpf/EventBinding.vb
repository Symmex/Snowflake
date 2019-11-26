Public MustInherit Class EventBinding
    Inherits CommandBehavior

    Protected Sub New()
    End Sub

    Protected Overridable Sub OnEventRaised(sender As Object, e As EventArgs)
        Dim parameter = If(Me.CommandParameter, e)
        Me.ExecuteCommand(parameter)
    End Sub

End Class