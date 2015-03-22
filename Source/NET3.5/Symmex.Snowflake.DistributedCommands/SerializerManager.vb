Imports Symmex.Snowflake.Common

Public Class SerializerManager

    Public Shared ReadOnly Property Current As ISerializer
        Get
            Dim value As ISerializer = Nothing
            If Not ObjectManager.TryResolve(value) Then
                value = New JsonSerializer()
                ObjectManager.Register(Of ISerializer)(value)
            End If

            Return value
        End Get
    End Property

End Class