Public Class ObjectManager

    Private Shared _Root As ObjectManagerNode
    Private Shared ReadOnly Property Root As ObjectManagerNode
        Get
            If _Root Is Nothing Then
                _Root = New ObjectManagerNode()
            End If

            Return _Root
        End Get
    End Property

    Public Shared Sub Register(Of T)(instance As T)
        Dim typeKey = GetType(T)
        Register(typeKey, instance)
    End Sub

    Public Shared Sub Register(Of T)(key As Object, instance As T)
        Dim path As New ObjectManagerPath(GetType(T), key)
        Register(path, instance)
    End Sub

    Public Shared Sub Register(Of T)(path As ObjectManagerPath, instance As T)
        Register(Of T)(path, New InstanceResolver(Of T)(instance))
    End Sub

    Public Shared Sub Register(Of T)(method As Func(Of T))
        Dim typeKey = GetType(T)
        Register(typeKey, method)
    End Sub

    Public Shared Sub Register(Of T)(key As Object, method As Func(Of T))
        Dim path As New ObjectManagerPath(GetType(T), key)
        Register(path, method)
    End Sub

    Public Shared Sub Register(Of T)(path As ObjectManagerPath, method As Func(Of T))
        Register(Of T)(path, New MethodResolver(Of T)(method))
    End Sub

    Public Shared Sub Register(Of T)(resolver As IResolver(Of T))
        Dim typeKey = GetType(T)
        Register(typeKey, resolver)
    End Sub

    Public Shared Sub Register(Of T)(key As Object, resolver As IResolver(Of T))
        Dim path As New ObjectManagerPath(GetType(T), key)
        Register(path, resolver)
    End Sub

    Public Shared Sub Register(Of T)(path As ObjectManagerPath, resolver As IResolver(Of T))
        Root.Register(path, 0, resolver)
    End Sub

    Public Shared Sub Unregister(Of T)()
        Dim typeKey = GetType(T)
        Unregister(Of T)(typeKey)
    End Sub

    Public Shared Sub Unregister(Of T)(key As Object)
        Dim path As New ObjectManagerPath(GetType(T), key)
        Unregister(Of T)(path)
    End Sub

    Public Shared Sub Unregister(Of T)(path As ObjectManagerPath)
        Root.Unregister(path, 0)
    End Sub

    Public Shared Function Resolve(Of T)() As T
        Dim typeKey = GetType(T)
        Return Resolve(Of T)(typeKey)
    End Function

    Public Shared Function Resolve(Of T)(key As Object) As T
        Dim path As New ObjectManagerPath(GetType(T), key)
        Return Resolve(Of T)(path)
    End Function

    Public Shared Function Resolve(Of T)(path As ObjectManagerPath) As T
        Return DirectCast(Root.Resolve(path, 0), T)
    End Function

    Public Shared Function CanResolve(Of T)() As Boolean
        Dim typeKey = GetType(T)
        Return CanResolve(Of T)(typeKey)
    End Function

    Public Shared Function CanResolve(Of T)(key As Object) As Boolean
        Dim itemType = GetType(T)
        Return CanResolve(itemType, key)
    End Function

    Public Shared Function TryResolve(Of T)() As T
        Dim typeKey = GetType(T)
        Return TryResolve(Of T)(typeKey)
    End Function

    Public Shared Function TryResolve(Of T)(key As Object) As T
        Dim item As T
        TryResolve(key, item)
        Return item
    End Function

    Public Shared Function TryResolve(Of T)(ByRef value As T) As Boolean
        Dim typeKey = GetType(T)
        Return TryResolve(typeKey, value)
    End Function

    Public Shared Function TryResolve(Of T)(key As Object, ByRef value As T) As Boolean
        Dim path As New ObjectManagerPath(GetType(T), key)
        Dim objectValue As Object = Nothing

        If Root.TryResolve(path, 0, objectValue) Then
            value = DirectCast(objectValue, T)
            Return True
        Else
            Return False
        End If
    End Function

    Public Shared Function Resolve(itemType As Type) As Object
        Return Resolve(itemType, itemType)
    End Function

    Public Shared Function Resolve(itemType As Type, key As Object) As Object
        Dim path As New ObjectManagerPath(itemType, key)
        Return Root.Resolve(path, 0)
    End Function

    Public Shared Function CanResolve(itemType As Type) As Boolean
        Return CanResolve(itemType, itemType)
    End Function

    Public Shared Function CanResolve(itemType As Type, key As Object) As Boolean
        Dim path As New ObjectManagerPath(itemType, key)
        Return Root.CanResolve(path, 0)
    End Function

    Public Shared Function TryResolve(itemType As Type, ByRef value As Object) As Boolean
        Return TryResolve(itemType, value, itemType)
    End Function

    Public Shared Function TryResolve(itemType As Type, ByRef value As Object, key As Object) As Boolean
        Dim path As New ObjectManagerPath(itemType, key)
        Return Root.TryResolve(path, 0, value)
    End Function

    Public Shared Function ResolveAll(Of T)() As IEnumerable(Of T)
        Dim path As New ObjectManagerPath(GetType(T))
        Return ResolveAll(Of T)(path)
    End Function

    Public Shared Function ResolveAll(Of T)(path As ObjectManagerPath) As IEnumerable(Of T)
        Return Root.ResolveAll(path, 0).Cast(Of T)()
    End Function

End Class