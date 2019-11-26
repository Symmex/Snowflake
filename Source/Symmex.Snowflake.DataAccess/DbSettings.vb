Public Class DbSettings

    Private _ProviderName As String
    Public ReadOnly Property ProviderName As String
        Get
            Return _ProviderName
        End Get
    End Property

    Private _ConnectionString As String
    Public ReadOnly Property ConnectionString As String
        Get
            Return _ConnectionString
        End Get
    End Property

    Public Sub New(ByVal providerName As String, ByVal connectionString As String)
        _ProviderName = providerName
        _ConnectionString = connectionString
    End Sub

End Class