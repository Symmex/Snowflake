Public Class CompositeKey

    Private _Components As Object()

    Public Sub New(ParamArray components As Object())
        If components Is Nothing Then
            Throw New ArgumentNullException("components")
        End If

        If components.Length = 0 Then
            Throw New ArgumentException("Atleast 1 component must be provided.")
        End If

        _Components = components
    End Sub

    Public Overrides Function GetHashCode() As Integer
        Dim code As Integer = Me.GetType().GetHashCode()

        For Each c In _Components
            If c IsNot Nothing Then
                code = code Xor c.GetHashCode()
            End If
        Next

        Return code
    End Function

    Public Overrides Function Equals(obj As Object) As Boolean
        If obj.GetType() IsNot Me.GetType() Then
            Return False
        End If

        Dim other = DirectCast(obj, CompositeKey)

        Dim otherComponents = other._Components
        If otherComponents.Length <> _Components.Length Then
            Return False
        End If

        For index = 0 To otherComponents.Length - 1
            If Not Object.Equals(otherComponents(index), _Components(index)) Then
                Return False
            End If
        Next

        Return True
    End Function

End Class