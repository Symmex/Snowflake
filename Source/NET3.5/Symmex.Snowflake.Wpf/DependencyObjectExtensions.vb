Imports System.Runtime.CompilerServices

Public Module DependencyObjectExtensions

    <Extension()>
    Public Function GetValue(Of T)(obj As DependencyObject, prop As DependencyProperty) As T
        Return DirectCast(obj.GetValue(prop), T)
    End Function

End Module