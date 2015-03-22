Imports System.Collections.ObjectModel

Public Class DynamicPropertyCollection
    Inherits KeyedCollection(Of String, IDynamicProperty)

    Protected Overrides Function GetKeyForItem(item As IDynamicProperty) As String
        Return item.Name
    End Function

    Public Function TryGetValue(key As String, ByRef value As IDynamicProperty) As Boolean
        Return MyBase.Dictionary.TryGetValue(key, value)
    End Function

End Class