Imports System.ComponentModel

Public Class FocusBindingConverter
    Inherits TypeConverter

    Public Overrides Function ConvertFrom(ByVal context As System.ComponentModel.ITypeDescriptorContext, ByVal culture As System.Globalization.CultureInfo, ByVal value As Object) As Object
        If TypeOf value Is String Then
            Return New FocusBinding() With {.PropertyName = DirectCast(value, String)}
        End If

        Return MyBase.ConvertFrom(context, culture, value)
    End Function

    Public Overrides Function CanConvertFrom(context As ITypeDescriptorContext, sourceType As Type) As Boolean
        If sourceType Is GetType(String) Then
            Return True
        End If

        Return False
    End Function

End Class