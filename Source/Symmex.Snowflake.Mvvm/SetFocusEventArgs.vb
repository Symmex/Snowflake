Public Class SetFocusEventArgs
    Inherits EventArgs

    Private _PropertyName As String
    Public ReadOnly Property PropertyName As String
        Get
            Return _PropertyName
        End Get
    End Property

    Public Sub New(ByVal propertyName As String)
        _PropertyName = propertyName
    End Sub

End Class