Imports System.Configuration

Public Class ConfigurationDbSettingsFactory
    Implements IDbSettingsFactory

    Private _Name As String

    Public Sub New(ByVal name As String)
        _Name = name
    End Sub

    Public Function GetSettings() As DbSettings Implements IDbSettingsFactory.GetSettings
        Dim settings = ConfigurationManager.ConnectionStrings(_Name)
        Return New DbSettings(settings.ProviderName, settings.ConnectionString)
    End Function

End Class