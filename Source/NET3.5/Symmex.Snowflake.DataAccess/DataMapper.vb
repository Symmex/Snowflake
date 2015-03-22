Imports System.Collections.ObjectModel
Imports Symmex.Snowflake.Common

Public NotInheritable Class DataMapper

    Private Sub New()
    End Sub

    Public Shared Sub MapData(item As IDynamicObject, data As IDataRecord)
        'Get the data properties
        Dim dps = DynamicPropertyManager.GetProperties(item.GetType())

        For index = 0 To data.FieldCount - 1
            Dim name = data.GetName(index)
            Dim dp As IDynamicProperty = Nothing
            If dps.TryGetValue(name, dp) Then
                Dim value As Object

                If data.IsDBNull(index) Then
                    value = dp.DefaultValueResolver
                Else
                    value = data.GetValue(index)
                End If

                item.SetValue(dp, value)
            End If
        Next
    End Sub

End Class