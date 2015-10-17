Imports System.Globalization
Imports System.Windows.Markup

<MarkupExtensionReturnType(GetType(IsNullConverter))>
Public Class IsNullConverter
    Inherits MarkupExtension
    Implements IValueConverter

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.Convert
        Return value Is Nothing
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As CultureInfo) As Object Implements IValueConverter.ConvertBack
        Throw New InvalidOperationException()
    End Function

    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        Return Me
    End Function

End Class