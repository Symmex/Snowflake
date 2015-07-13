Public Class PreviewMouseLeftButtonDownEventBinding
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
        MyBase.RoutedEvent = UIElement.PreviewMouseLeftButtonDownEvent
    End Sub

    Protected Overrides Function CreateInstanceCore() As Freezable
        Return New PreviewMouseLeftButtonDownEventBinding()
    End Function

End Class