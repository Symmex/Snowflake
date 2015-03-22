Imports System.Collections.ObjectModel

Public Class DataScriptCollection
    Inherits KeyedCollection(Of Guid, DataScript)

    Protected Overrides Function GetKeyForItem(item As DataScript) As Guid
        Return item.Id
    End Function

End Class