Imports System.Collections.Generic
Imports System.ComponentModel
Imports System.Linq
Imports System.Text
Imports System.Windows

Public Class SharedResourceDictionary
    Inherits ResourceDictionary

    Private Shared _IsInDesignMode As Boolean

    Shared Sub New()
        _IsInDesignMode = CBool(DesignerProperties.IsInDesignModeProperty.GetMetadata(GetType(DependencyObject)).DefaultValue)
    End Sub

    Private Shared _SharedDictionaries As Dictionary(Of Uri, ResourceDictionary)
    Public Shared ReadOnly Property SharedDictionaries As Dictionary(Of Uri, ResourceDictionary)
        Get
            If _SharedDictionaries Is Nothing Then
                _SharedDictionaries = New Dictionary(Of Uri, ResourceDictionary)()
            End If

            Return _SharedDictionaries
        End Get
    End Property

    Private _Source As Uri
    Public Shadows Property Source As Uri
        Get
            Return _Source
        End Get
        Set(value As Uri)
            _Source = value

            If Not SharedDictionaries.ContainsKey(value) OrElse _IsInDesignMode Then
                MyBase.Source = value

                If Not _IsInDesignMode Then
                    SharedDictionaries.Add(value, Me)
                End If
            Else
                Me.MergedDictionaries.Add(SharedDictionaries(value))
            End If
        End Set
    End Property

End Class