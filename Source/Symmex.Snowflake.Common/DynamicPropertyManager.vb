Public NotInheritable Class DynamicPropertyManager

    Private Shared _ItemsLock As New Object()

    Private Shared _Items As Dictionary(Of Type, DynamicPropertyCollection)
    Private Shared ReadOnly Property Items As Dictionary(Of Type, DynamicPropertyCollection)
        Get
            If _Items Is Nothing Then
                _Items = New Dictionary(Of Type, DynamicPropertyCollection)()
            End If

            Return _Items
        End Get
    End Property

    Public Shared Function GetProperties(ownerType As Type) As DynamicPropertyCollection
        Dim items As New DynamicPropertyCollection()

        Dim currentType = ownerType
        While currentType IsNot GetType(Object)
            Dim dps = GetPropertyCollection(currentType)
            For Each dp In dps
                items.Add(dp)
            Next

            currentType = currentType.BaseType
        End While

        Return items
    End Function

    Private Shared Function GetPropertyCollection(ownerType As Type) As DynamicPropertyCollection
        Dim dps As DynamicPropertyCollection = Nothing

        If Not Items.TryGetValue(ownerType, dps) Then
            dps = New DynamicPropertyCollection()

            Dim dpFields = ownerType.GetFields(Reflection.BindingFlags.Static Or Reflection.BindingFlags.Public Or Reflection.BindingFlags.NonPublic)
            Dim dpType = GetType(IDynamicProperty)
            For Each dpField In dpFields
                If dpType.IsAssignableFrom(dpField.FieldType) Then
                    Dim dp = DirectCast(dpField.GetValue(Nothing), IDynamicProperty)
                    dps.Add(dp)
                End If
            Next

            SyncLock _ItemsLock
                Items(ownerType) = dps
            End SyncLock
        End If

        Return dps
    End Function

End Class