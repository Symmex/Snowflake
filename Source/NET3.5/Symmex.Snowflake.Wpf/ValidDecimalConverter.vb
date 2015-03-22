Imports System.Windows.Markup
Imports System.Globalization

<MarkupExtensionReturnType(GetType(ValidDecimalConverter))>
Public Class ValidDecimalConverter
    Inherits MarkupExtension
    Implements IValueConverter

    Public Property FallbackValue As Decimal

    Public Function Convert(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.Convert
        Return Me.Convert(value)
    End Function

    Public Function ConvertBack(value As Object, targetType As Type, parameter As Object, culture As Globalization.CultureInfo) As Object Implements IValueConverter.ConvertBack
        Return Me.Convert(value)
    End Function

    Private Function Convert(value As Object) As Decimal
        Dim result = Me.FallbackValue

        Try
            If TypeOf value Is String Then
                Dim strValue = DirectCast(value, String)
                result = Decimal.Parse(strValue, NumberStyles.Any)
            Else
                result = System.Convert.ToDecimal(value)
            End If
        Catch ex As Exception
        End Try

        Return result
    End Function

    Public Overrides Function ProvideValue(serviceProvider As IServiceProvider) As Object
        Return Me
    End Function

End Class