Imports System.Windows.Controls.Primitives

Public Class DropDownButton
    Inherits Button

    Public Shared ReadOnly IsDropDownOpenProperty As DependencyProperty = DependencyProperty.Register("IsDropDownOpen", GetType(Boolean), GetType(DropDownButton))
    Public Property IsDropDownOpen As Boolean
        Get
            Return DirectCast(Me.GetValue(IsDropDownOpenProperty), Boolean)
        End Get
        Set(value As Boolean)
            Me.SetValue(IsDropDownOpenProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DropDownContentProperty As DependencyProperty = DependencyProperty.Register("DropDownContent", GetType(Object), GetType(DropDownButton))
    Public Property DropDownContent As Object
        Get
            Return Me.GetValue(DropDownContentProperty)
        End Get
        Set(value As Object)
            Me.SetValue(DropDownContentProperty, value)
        End Set
    End Property

    Public Shared ReadOnly DropDownPlacementProperty As DependencyProperty = DependencyProperty.Register("DropDownPlacement", GetType(PlacementMode), GetType(DropDownButton), New FrameworkPropertyMetadata(PlacementMode.Bottom))
    Public Property DropDownPlacement As PlacementMode
        Get
            Return DirectCast(Me.GetValue(DropDownPlacementProperty), PlacementMode)
        End Get
        Set(value As PlacementMode)
            Me.SetValue(DropDownPlacementProperty, value)
        End Set
    End Property

    Shared Sub New()
        DefaultStyleKeyProperty.OverrideMetadata(GetType(DropDownButton), New FrameworkPropertyMetadata(GetType(DropDownButton)))
    End Sub

    Protected Overrides Sub OnClick()
        MyBase.OnClick()
        Me.IsDropDownOpen = Not Me.IsDropDownOpen
    End Sub

End Class