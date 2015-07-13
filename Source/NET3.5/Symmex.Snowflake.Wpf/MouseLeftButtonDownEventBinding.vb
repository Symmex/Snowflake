Public Class MouseLeftButtonDownEventBinding
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
        MyBase.RoutedEvent = UIElement.MouseLeftButtonDownEvent
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New MouseLeftButtonDownEventBinding()
    End Function

End Class