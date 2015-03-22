Imports System.Windows.Markup
Imports Symmex.Snowflake.Common

<MarkupExtensionReturnType(GetType(Object))>
Public Class ObjectManagerExtension
    Inherits MarkupExtension

    <ConstructorArgument("itemType")>
    Public Property ItemType As Type
    Public Property Key As Object
    Public Property DefaultValue As Object

    Public Overrides Function ProvideValue(ByVal serviceProvider As IServiceProvider) As Object
        Dim value As Object = Nothing

        If Me.Key Is Nothing Then
            ObjectManager.TryResolve(_ItemType, value)
        Else
            ObjectManager.TryResolve(_ItemType, value, Me.Key)
        End If

        Return If(value, Me.DefaultValue)
    End Function

    Public Sub New(ByVal itemType As Type)
        Me._ItemType = itemType
    End Sub

End Class
