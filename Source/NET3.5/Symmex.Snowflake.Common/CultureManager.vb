Imports System.Globalization

Public Class CultureManager

    Private Shared _CurrentCultureChangedEvent As EventBroker(Of EventArgs)
    Public Shared ReadOnly Property CurrentCultureChangedEvent As EventBroker(Of EventArgs)
        Get
            If _CurrentCultureChangedEvent Is Nothing Then
                _CurrentCultureChangedEvent = New EventBroker(Of EventArgs)()
            End If

            Return _CurrentCultureChangedEvent
        End Get
    End Property

    Public Shared Property DefaultCulture As CultureInfo = New CultureInfo("en-US")

    Private Shared _CurrentCulture As CultureInfo
    Public Shared Property CurrentCulture As CultureInfo
        Get
            Return _CurrentCulture
        End Get
        Set(value As CultureInfo)
            _CurrentCulture = value
            CurrentCultureChangedEvent.RaiseEvent(Nothing, EventArgs.Empty)
        End Set
    End Property

    Shared Sub New()
        CurrentCulture = System.Threading.Thread.CurrentThread.CurrentCulture
    End Sub

End Class