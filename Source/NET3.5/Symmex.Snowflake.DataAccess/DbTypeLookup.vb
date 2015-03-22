Public Class DbTypeLookup

    Private Shared _Items As Dictionary(Of Type, DbType)
    Private Shared ReadOnly Property Items As Dictionary(Of Type, DbType)
        Get
            If _Items Is Nothing Then
                LoadItems()
            End If

            Return _Items
        End Get
    End Property

    Private Shared Sub LoadItems()
        _Items = New Dictionary(Of Type, DbType)()
        _Items.Add(GetType(Byte), DbType.Byte)
        _Items.Add(GetType(Byte?), DbType.Byte)
        _Items.Add(GetType(SByte), DbType.SByte)
        _Items.Add(GetType(SByte?), DbType.SByte)
        _Items.Add(GetType(Short), DbType.Int16)
        _Items.Add(GetType(Short?), DbType.Int16)
        _Items.Add(GetType(Integer), DbType.Int32)
        _Items.Add(GetType(Integer?), DbType.Int32)
        _Items.Add(GetType(Long), DbType.Int64)
        _Items.Add(GetType(Long?), DbType.Int64)
        _Items.Add(GetType(UShort), DbType.UInt16)
        _Items.Add(GetType(UShort?), DbType.UInt16)
        _Items.Add(GetType(UInteger), DbType.UInt32)
        _Items.Add(GetType(UInteger?), DbType.UInt32)
        _Items.Add(GetType(ULong), DbType.UInt64)
        _Items.Add(GetType(ULong?), DbType.UInt64)
        _Items.Add(GetType(Double), DbType.Double)
        _Items.Add(GetType(Double?), DbType.Double)
        _Items.Add(GetType(Decimal), DbType.Decimal)
        _Items.Add(GetType(Decimal?), DbType.Decimal)
        _Items.Add(GetType(Char), DbType.String)
        _Items.Add(GetType(Char?), DbType.String)
        _Items.Add(GetType(String), DbType.String)
        _Items.Add(GetType(Boolean), DbType.Boolean)
        _Items.Add(GetType(Boolean?), DbType.Boolean)
        _Items.Add(GetType(Byte()), DbType.Binary)
        _Items.Add(GetType(Guid), DbType.Guid)
        _Items.Add(GetType(Guid?), DbType.Guid)
        _Items.Add(GetType(DateTime), DbType.DateTime)
        _Items.Add(GetType(DateTime?), DbType.DateTime)
        _Items.Add(GetType(DateTimeOffset), DbType.DateTimeOffset)
        _Items.Add(GetType(DateTimeOffset?), DbType.DateTimeOffset)
    End Sub

    Public Shared Function GetDbType(ByVal t As Type) As DbType
        Dim dbTypeValue As DbType = DbType.Object
        Items.TryGetValue(t, dbTypeValue)

        Return dbTypeValue
    End Function

End Class