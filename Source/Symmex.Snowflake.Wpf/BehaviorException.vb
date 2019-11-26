Public Class BehaviorException
    Inherits Exception

    Private _Behavior As Behavior
    Public ReadOnly Property Behavior As Behavior
        Get
            Return _Behavior
        End Get
    End Property

    Public Sub New(b As Behavior, message As String)
        MyBase.New(message)
        _Behavior = b
    End Sub

End Class