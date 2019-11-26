Imports System.ComponentModel
Imports System.Windows.Controls.Primitives
Imports System.Windows.Markup
Imports Symmex.Snowflake.Mvvm

<MarkupExtensionReturnType(GetType(FocusBinding)), TypeConverter(GetType(FocusBindingConverter))>
Public Class FocusBinding
    Inherits MarkupExtension

#Region "Properties"
    Private _Element As FrameworkElement
    Public Property Element As FrameworkElement
        Get
            Return _Element
        End Get
        Set(ByVal value As FrameworkElement)
            If _Element IsNot Nothing Then
                RemoveHandler _Element.DataContextChanged, AddressOf Element_DataContextChanged
            End If

            _Element = value
            If _Element IsNot Nothing Then
                AddHandler _Element.DataContextChanged, AddressOf Element_DataContextChanged
                Me.SetFocusSetter(TryCast(value.DataContext, IFocusSetter))
            End If
        End Set
    End Property

    <ConstructorArgument("propertyName")>
    Public Property PropertyName As String

    Private _FocusSetter As IFocusSetter
#End Region

#Region "Constructors"
    Public Sub New()
    End Sub

    Public Sub New(propertyName As String)
        Me.PropertyName = propertyName
    End Sub
#End Region

#Region "Methods"
    Private Sub Element_DataContextChanged(ByVal sender As Object, ByVal e As DependencyPropertyChangedEventArgs)
        Me.SetFocusSetter(TryCast(e.NewValue, IFocusSetter))
    End Sub

    Private Sub SetFocusSetter(ByVal value As IFocusSetter)
        If _FocusSetter IsNot Nothing Then
            RemoveHandler _FocusSetter.SetFocus, AddressOf FocusSetter_SetFocus
        End If

        _FocusSetter = value
        If _FocusSetter IsNot Nothing Then
            AddHandler _FocusSetter.SetFocus, AddressOf FocusSetter_SetFocus
        End If
    End Sub

    Private Sub FocusSetter_SetFocus(ByVal sender As Object, ByVal e As SetFocusEventArgs)
        If e.PropertyName = _PropertyName Then
            Keyboard.Focus(_Element)
        End If
    End Sub

    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        Return Me
    End Function
#End Region

End Class