Imports Symmex.Snowflake.Mvvm

Public Class ToolTipViewModelConverter
    Implements IValueConverter

    Public Function Convert(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.Convert
        Dim vm = DirectCast(value, ToolTipViewModel)
        If vm Is Nothing Then
            Return Nothing
        Else
            Dim view As New ToolTip()
            view.SetBinding(ToolTip.IsOpenProperty, "IsOpen")
            view.SetBinding(ToolTip.ContentProperty, "Content")
            view.DataContext = vm
            Return view
        End If
    End Function

    Public Function ConvertBack(ByVal value As Object, ByVal targetType As System.Type, ByVal parameter As Object, ByVal culture As System.Globalization.CultureInfo) As Object Implements System.Windows.Data.IValueConverter.ConvertBack
        Dim tt = DirectCast(value, ToolTip)
        Return DirectCast(tt.DataContext, ToolTipViewModel)
    End Function

End Class