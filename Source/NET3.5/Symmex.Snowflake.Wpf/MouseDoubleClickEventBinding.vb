﻿Public Class MouseDoubleClickEventBinding
    Inherits MouseButtonEventBinding

    Private Shadows Property RoutedEvent As RoutedEvent
        Get
            Return MyBase.RoutedEvent
        End Get
        Set(value As RoutedEvent)
            MyBase.RoutedEvent = value
        End Set
    End Property

    Public Sub New()
        MyBase.RoutedEvent = Control.MouseDoubleClickEvent
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New MouseDoubleClickEventBinding()
    End Function

End Class