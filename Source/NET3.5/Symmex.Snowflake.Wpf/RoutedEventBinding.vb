Public Class RoutedEventBinding
    Inherits EventBinding

    Public Shared ReadOnly RoutedEventProperty As DependencyProperty = DependencyProperty.Register("RoutedEvent", GetType(RoutedEvent), GetType(RoutedEventBinding))
    Public Property RoutedEvent As RoutedEvent
        Get
            Return DirectCast(Me.GetValue(RoutedEventProperty), RoutedEvent)
        End Get
        Set(value As RoutedEvent)
            Me.SetValue(RoutedEventProperty, value)
        End Set
    End Property

    Public Shared ReadOnly HandledEventsTooProperty As DependencyProperty = DependencyProperty.Register("HandledEventsToo", GetType(Boolean), GetType(RoutedEventBinding))
    Public Property HandledEventsToo As Boolean
        Get
            Return DirectCast(Me.GetValue(HandledEventsTooProperty), Boolean)
        End Get
        Set(value As Boolean)
            Me.SetValue(HandledEventsTooProperty, value)
        End Set
    End Property

    Public Shared ReadOnly OnlyHandleDirectEventsProperty As DependencyProperty = DependencyProperty.Register("OnlyHandleDirectEvents", GetType(Boolean), GetType(RoutedEventBinding))
    Public Property OnlyHandleDirectEvents As Boolean
        Get
            Return DirectCast(Me.GetValue(OnlyHandleDirectEventsProperty), Boolean)
        End Get
        Set(value As Boolean)
            Me.SetValue(OnlyHandleDirectEventsProperty, value)
        End Set
    End Property

    Protected Overrides Sub OnAttached()
        Dim fe = DirectCast(Me.Element, FrameworkElement)
        fe.AddHandler(Me.RoutedEvent, New RoutedEventHandler(AddressOf Me.OnEventRaised), Me.HandledEventsToo)
    End Sub

    Protected Overridable Sub OnEventRaised(sender As Object, e As RoutedEventArgs)
        If Me.OnlyHandleDirectEvents AndAlso Not e.OriginalSource Is Me.Element Then
            Exit Sub
        End If

        Dim parameter = If(Me.CommandParameter, e)

        Me.ExecuteCommand(parameter)
        e.Handled = Me.Handled
    End Sub

End Class