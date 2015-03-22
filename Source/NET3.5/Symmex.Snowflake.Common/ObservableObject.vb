Imports System.Runtime.Serialization
Imports System.ComponentModel

<DataContract()>
Public MustInherit Class ObservableObject
    Implements INotifyPropertyChanged

#Region "Events"
    Public Event PropertyChanged(ByVal sender As Object, ByVal e As System.ComponentModel.PropertyChangedEventArgs) Implements System.ComponentModel.INotifyPropertyChanged.PropertyChanged
#End Region

#Region "Constructors"
    Protected Sub New()
    End Sub
#End Region

#Region "Methods"
    Public Overridable Sub OnPropertyChanged(ByVal propertyName As String)
        RaiseEvent PropertyChanged(Me, New PropertyChangedEventArgs(propertyName))
    End Sub
#End Region

End Class