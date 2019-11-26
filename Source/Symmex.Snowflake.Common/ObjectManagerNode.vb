Public Class ObjectManagerNode

    Private _Resolvers As Dictionary(Of Object, IResolver)
    Public ReadOnly Property Resolvers As Dictionary(Of Object, IResolver)
        Get
            If _Resolvers Is Nothing Then
                _Resolvers = New Dictionary(Of Object, IResolver)()
            End If

            Return _Resolvers
        End Get
    End Property

    Private _SubNodes As Dictionary(Of Object, ObjectManagerNode)
    Public ReadOnly Property SubNodes As Dictionary(Of Object, ObjectManagerNode)
        Get
            If _SubNodes Is Nothing Then
                _SubNodes = New Dictionary(Of Object, ObjectManagerNode)()
            End If

            Return _SubNodes
        End Get
    End Property

    Public Sub Register(path As ObjectManagerPath, index As Integer, resolver As IResolver)
        Dim key = path.Elements(index)

        If index = path.Elements.Length - 1 Then
            Me.Resolvers.Add(key, resolver)
        Else
            Me.GetSubNode(key, True).Register(path, index + 1, resolver)
        End If
    End Sub

    Public Function Resolve(path As ObjectManagerPath, index As Integer) As Object
        Dim key = path.Elements(index)

        If index = path.Elements.Length - 1 Then
            Return Me.Resolvers(key).Resolve()
        Else
            Return Me.GetSubNode(key).Resolve(path, index + 1)
        End If
    End Function

    Public Function TryResolve(path As ObjectManagerPath, index As Integer, ByRef value As Object) As Boolean
        Dim key = path.Elements(index)

        If index = path.Elements.Length - 1 Then
            Dim resolver As IResolver = Nothing

            If Me.Resolvers.TryGetValue(key, resolver) Then
                value = resolver.Resolve()
                Return True
            Else
                Return False
            End If
        Else
            Dim subNode = Me.GetSubNode(key)

            If subNode Is Nothing Then
                Return False
            Else
                Return subNode.TryResolve(path, index + 1, value)
            End If
        End If
    End Function

    Public Function CanResolve(path As ObjectManagerPath, index As Integer) As Boolean
        Dim key = path.Elements(index)

        If index = path.Elements.Length - 1 Then
            If Me.Resolvers.ContainsKey(key) Then
                Return True
            Else
                Return False
            End If
        Else
            Dim subNode = Me.GetSubNode(key)

            If subNode Is Nothing Then
                Return False
            Else
                Return subNode.CanResolve(path, index + 1)
            End If
        End If
    End Function

    Public Function ResolveAll(path As ObjectManagerPath, index As Integer) As IEnumerable(Of Object)
        If index = path.Elements.Length Then
            Return (From resolver In Me.Resolvers.Values Select resolver.Resolve()).ToList()
        Else
            Dim key = path.Elements(index)
            Dim node = Me.GetSubNode(key)

            If node Is Nothing Then
                Return New List(Of Object)()
            Else
                Return node.ResolveAll(path, index + 1)
            End If
        End If
    End Function

    Public Function GetSubNode(key As Object) As ObjectManagerNode
        Return Me.GetSubNode(key, False)
    End Function

    Public Function GetSubNode(key As Object, create As Boolean) As ObjectManagerNode
        Dim subNode As ObjectManagerNode = Nothing
        If Not Me.SubNodes.TryGetValue(key, subNode) AndAlso create Then
            subNode = New ObjectManagerNode()
            Me.SubNodes.Add(key, subNode)
        End If

        Return subNode
    End Function

    Public Sub Unregister(path As ObjectManagerPath, index As Integer)
        Dim key = path.Elements(index)

        If index = path.Elements.Length - 1 Then
            If Me.Resolvers.ContainsKey(key) Then
                Me.Resolvers.Remove(key)
            End If
        Else
            Dim subNode = Me.GetSubNode(key)
            If subNode IsNot Nothing Then
                subNode.Unregister(path, index + 1)
            End If
        End If
    End Sub

End Class