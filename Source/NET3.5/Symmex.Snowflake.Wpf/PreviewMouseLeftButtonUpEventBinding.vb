Public Class PreviewMouseLeftButtonUpEventBinding
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
        MyBase.RoutedEvent = UIElement.PreviewMouseLeftButtonUpEvent
    End Sub

End Class
