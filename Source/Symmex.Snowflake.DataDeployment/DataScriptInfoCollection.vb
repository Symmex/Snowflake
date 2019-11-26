Imports System.Collections.ObjectModel

Public Class DataScriptInfoCollection
    Inherits KeyedCollection(Of Guid, DataScriptInfo)

    Protected Overrides Function GetKeyForItem(item As DataScriptInfo) As Guid
        Return item.Id
    End Function

End Class