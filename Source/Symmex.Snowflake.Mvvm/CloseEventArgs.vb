Public Class CloseEventArgs
    Inherits EventArgs

    Private _Result As Boolean?
    Public ReadOnly Property Result As Boolean?
        Get
            Return _Result
        End Get
    End Property

    Public Sub New(ByVal result As Boolean?)
        _Result = result
    End Sub

End Class