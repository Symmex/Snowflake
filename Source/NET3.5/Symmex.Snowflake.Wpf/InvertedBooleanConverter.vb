Imports System.Windows.Markup

<MarkupExtensionReturnType(GetType(InvertedBooleanConverter))>
Public Class InvertedBooleanConverter
    Inherits MarkupExtension
    Implements IValueConverter

    Public Sub New()
    End Sub

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return Not DirectCast(value, Boolean)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Not DirectCast(value, Boolean)
    End Function

    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        Return Me
    End Function

End Class