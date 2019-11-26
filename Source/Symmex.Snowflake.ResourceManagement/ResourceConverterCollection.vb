Imports System.Collections.ObjectModel

Public Class ResourceConverterCollection
    Inherits KeyedCollection(Of String, IResourceConverter)

    Protected Overrides Function GetKeyForItem(item As IResourceConverter) As String
        Return item.Extension
    End Function

End Class