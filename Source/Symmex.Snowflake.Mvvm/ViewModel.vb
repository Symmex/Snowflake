Imports System.ComponentModel
Imports System.Windows
Imports System.Windows.Input
Imports Symmex.Snowflake.Common

Public Class ViewModel
    Inherits DynamicObject
    Implements IFocusSetter

    Public Event SetFocus(sender As Object, e As SetFocusEventArgs) Implements IFocusSetter.SetFocus

    Private _IsEnabled As Boolean = True
    <Browsable(False)>
    Public Overridable Property IsEnabled As Boolean
        Get
            Return _IsEnabled
        End Get
        Set(value As Boolean)
            _IsEnabled = value
            Me.OnPropertyChanged("IsEnabled")
        End Set
    End Property

    Private _ToolTip As Object
    <Browsable(False)>
    Public Overridable Property ToolTip As Object
        Get
            Return _ToolTip
        End Get
        Set(value As Object)
            _ToolTip = value
            Me.OnPropertyChanged("ToolTip")
        End Set
    End Property

    Private _HorizontalAlignment As HorizontalAlignment
    <Browsable(False)>
    Public Property HorizontalAlignment As HorizontalAlignment
        Get
            Return _HorizontalAlignment
        End Get
        Set(value As HorizontalAlignment)
            _HorizontalAlignment = value
            Me.OnPropertyChanged("HorizontalAlignment")
        End Set
    End Property

    Private _VerticalAlignment As VerticalAlignment
    <Browsable(False)>
    Public Property VerticalAlignment As VerticalAlignment
        Get
            Return _VerticalAlignment
        End Get
        Set(value As VerticalAlignment)
            _VerticalAlignment = value
            Me.OnPropertyChanged("VerticalAlignment")
        End Set
    End Property

    Private _LoadedCommand As ICommand
    Public ReadOnly Property LoadedCommand As ICommand
        Get
            If _LoadedCommand Is Nothing Then
                _LoadedCommand = New DelegateCommand(AddressOf Me.OnLoaded)
            End If

            Return _LoadedCommand
        End Get
    End Property

    Protected Overridable Sub OnSetFocus(propertyName As String)
        RaiseEvent SetFocus(Me, New SetFocusEventArgs(propertyName))
    End Sub

    Protected Overridable Sub OnLoaded()
    End Sub

End Class