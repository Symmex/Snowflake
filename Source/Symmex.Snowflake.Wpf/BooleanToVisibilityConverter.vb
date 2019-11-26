Imports System.Windows.Markup

<MarkupExtensionReturnType(GetType(BooleanToVisibilityConverter))>
Public Class BooleanToVisibilityConverter
    Inherits MarkupExtension
    Implements IValueConverter

    Public Property TrueVisibility As Visibility = Visibility.Visible
    Public Property FalseVisibility As Visibility = Visibility.Collapsed

    Public Sub New()
    End Sub

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return If(CBool(value), Me.TrueVisibility, Me.FalseVisibility)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Dim v = DirectCast(value, Visibility)
        Return If(v = Me.TrueVisibility, True, False)
    End Function

    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        Return Me
    End Function

End Class