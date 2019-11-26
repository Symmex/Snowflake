Imports System.Windows
Imports System.ComponentModel

Public Class ContentViewModel
    Inherits ViewModel

    Private _Content As Object
    Public Overridable Property Content As Object
        Get
            Return _Content
        End Get
        Set(ByVal value As Object)
            _Content = value
            Me.OnPropertyChanged("Content")
        End Set
    End Property

    Private _HorizontalContentAlignment As HorizontalAlignment
    <Browsable(False)>
    Public Property HorizontalContentAlignment As HorizontalAlignment
        Get
            Return _HorizontalContentAlignment
        End Get
        Set(value As HorizontalAlignment)
            _HorizontalContentAlignment = value
            Me.OnPropertyChanged("HorizontalContentAlignment")
        End Set
    End Property

    Private _VerticalContentAlignment As VerticalAlignment
    <Browsable(False)>
    Public Property VerticalContentAlignment As VerticalAlignment
        Get
            Return _VerticalContentAlignment
        End Get
        Set(value As VerticalAlignment)
            _VerticalContentAlignment = value
            Me.OnPropertyChanged("VerticalContentAlignment")
        End Set
    End Property

End Class