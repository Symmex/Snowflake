Imports System.Collections.ObjectModel
Imports Symmex.Snowflake.Wpf

Public Class BehaviorCollection
    Inherits Collection(Of Behavior)

    Private _Element As DependencyObject
    Public ReadOnly Property Element As DependencyObject
        Get
            Return _Element
        End Get
    End Property

    Public Sub Attach(element As DependencyObject)
        If element Is Nothing Then
            Throw New ArgumentNullException("element")
        End If

        If _Element IsNot Nothing Then
            Throw New InvalidOperationException("Element is already set.")
        End If

        _Element = element

        For Each b In Me
            Me.Attach(element)
        Next
    End Sub

    Protected Overrides Sub InsertItem(index As Integer, item As Behavior)
        MyBase.InsertItem(index, item)

        If Me.Element IsNot Nothing Then
            Me.Attach(item)
        End If
    End Sub

    Public Sub Merge(other As BehaviorCollection)
        For Each b In other
            Me.Add(b)
        Next
    End Sub

    Private Sub Attach(item As Behavior)
        Dim b As New Binding()
        b.Source = Me.Element
        b.Path = New PropertyPath("DataContext")
        b.Mode = BindingMode.OneWay
        BindingOperations.SetBinding(item, FrameworkElement.DataContextProperty, b)

        item.Attach(Me.Element)
    End Sub

End Class