Imports Symmex.Snowflake.Common

Public Class ThemeManager
    Inherits ObservableObject

    Private Shared _Instance As ThemeManager
    Friend Shared ReadOnly Property Instance As ThemeManager
        Get
            If _Instance Is Nothing Then
                _Instance = New ThemeManager()
            End If

            Return _Instance
        End Get
    End Property

    Public Shared Property CurrentTheme As ResourceDictionary
        Get
            Return Instance.CurrentThemeCore
        End Get
        Set(value As ResourceDictionary)
            Instance.CurrentThemeCore = value
        End Set
    End Property

    Private _CurrentTheme As ResourceDictionary
    Private Property CurrentThemeCore As ResourceDictionary
        Get
            Return _CurrentTheme
        End Get
        Set(value As ResourceDictionary)
            _CurrentTheme = value
            Me.OnPropertyChanged(String.Empty)
        End Set
    End Property

    Default Public ReadOnly Property Item(key As String) As Object
        Get
            If Me.CurrentThemeCore Is Nothing Then
                Return Nothing
            Else
                Return Me.CurrentThemeCore.Item(key)
            End If
        End Get
    End Property

    Private Sub New()
    End Sub

End Class