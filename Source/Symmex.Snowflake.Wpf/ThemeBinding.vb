Imports System.Windows.Markup
Imports System.Windows.Data

Public Class ThemeBinding
    Inherits Binding

    Public Sub New(path As String)
        MyBase.New("[" + path + "]")
        Me.Source = ThemeManager.Instance
    End Sub

End Class