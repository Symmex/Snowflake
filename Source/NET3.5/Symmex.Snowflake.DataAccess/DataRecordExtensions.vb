Imports System.Runtime.CompilerServices

Public Module DataRecordExtensions

#Region "Boolean"
    <Extension()> _
    Function GetBoolean(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Boolean
        Return dataRecord.GetBoolean(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetBoolean(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Boolean) As Boolean
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetBoolean(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetBoolean(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Boolean) As Boolean
        Return GetBoolean(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableBoolean(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Boolean?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetBoolean(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableBoolean(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Boolean?
        Return GetNullableBoolean(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Byte"
    <Extension()> _
    Function GetByte(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Byte
        Return dataRecord.GetByte(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetByte(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Byte) As Byte
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetByte(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetByte(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Byte) As Byte
        Return GetByte(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableByte(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Byte?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetByte(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableByte(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Byte?
        Return GetNullableByte(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Bytes"
    <Extension()> _
    Sub GetBytes(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal fieldOffset As Long, ByVal buffer() As Byte, ByVal bufferOffset As Integer, ByVal length As Integer)
        dataRecord.GetBytes(dataRecord.GetOrdinal(columnName), fieldOffset, buffer, bufferOffset, length)
    End Sub
#End Region

#Region "Char"
    <Extension()> _
    Function GetChar(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Char
        Return dataRecord.GetChar(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetChar(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Char) As Char
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetChar(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetChar(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Char) As Char
        Return GetChar(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableChar(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Char?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetChar(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableChar(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Char?
        Return GetNullableChar(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Chars"
    <Extension()> _
    Sub GetChars(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal fieldOffset As Long, ByVal buffer() As Char, ByVal bufferOffset As Integer, ByVal length As Integer)
        dataRecord.GetChars(dataRecord.GetOrdinal(columnName), fieldOffset, buffer, bufferOffset, length)
    End Sub
#End Region

#Region "Data"
    <Extension()> _
    Function GetData(ByVal dataRecord As IDataRecord, ByVal columnName As String) As IDataReader
        Return dataRecord.GetData(dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "DataTypeName"
    <Extension()> _
    Function GetDataTypeName(ByVal dataRecord As IDataRecord, ByVal columnName As String) As String
        Return dataRecord.GetDataTypeName(dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "DateTime"
    <Extension()> _
    Function GetDateTime(ByVal dataRecord As IDataRecord, ByVal columnName As String) As DateTime
        Return dataRecord.GetDateTime(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetDateTime(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As DateTime) As DateTime
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetDateTime(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetDateTime(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As DateTime) As DateTime
        Return GetDateTime(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableDateTime(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As DateTime?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetDateTime(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableDateTime(ByVal dataRecord As IDataRecord, ByVal columnName As String) As DateTime?
        Return GetNullableDateTime(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Decimal"
    <Extension()> _
    Function GetDecimal(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Decimal
        Return dataRecord.GetDecimal(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetDecimal(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Decimal) As Decimal
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetDecimal(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetDecimal(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Decimal) As Decimal
        Return GetDecimal(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableDecimal(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Decimal?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetDecimal(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableDecimal(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Decimal?
        Return GetNullableDecimal(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Double"
    <Extension()> _
    Function GetDouble(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Double
        Return dataRecord.GetDouble(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetDouble(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Double) As Double
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetDouble(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetDouble(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Double) As Double
        Return GetDouble(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableDouble(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Double?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetDouble(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableDouble(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Double?
        Return GetNullableDouble(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "FieldType"
    <Extension()> _
    Function GetFieldType(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Type
        Return dataRecord.GetFieldType(dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Float"
    <Extension()> _
    Function GetFloat(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Single
        Return dataRecord.GetFloat(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetFloat(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Single) As Single
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetFloat(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetFloat(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Single) As Single
        Return GetFloat(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableFloat(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Single?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetFloat(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableFloat(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Single?
        Return GetNullableFloat(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Guid"
    <Extension()> _
    Function GetGuid(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Guid
        Return dataRecord.GetGuid(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetGuid(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Guid) As Guid
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetGuid(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetGuid(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Guid) As Guid
        Return GetGuid(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableGuid(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Guid?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetGuid(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableGuid(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Guid?
        Return GetNullableGuid(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Int16"
    <Extension()> _
    Function GetInt16(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int16
        Return dataRecord.GetInt16(dataRecord.GetOrdinal(columnName), 1)
    End Function

    <Extension()> _
    Function GetInt16(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Int16) As Int16
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetInt16(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetInt16(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Int16) As Int16
        Return GetInt16(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableInt16(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Int16?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetInt16(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableInt16(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int16?
        Return GetNullableInt16(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Int32"
    <Extension()> _
    Function GetInt32(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int32
        Return dataRecord.GetInt32(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetInt32(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Int32) As Int32
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetInt32(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetInt32(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Int32) As Int32
        Return GetInt32(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableInt32(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Int32?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetInt32(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableInt32(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int32?
        Return GetNullableInt32(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Int64"
    <Extension()> _
    Function GetInt64(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int64
        Return dataRecord.GetInt64(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetInt64(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Int64) As Int64
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetInt64(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetInt64(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Int64) As Int64
        Return GetInt64(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableInt64(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As Int64?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return dataRecord.GetInt64(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetNullableInt64(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Int64?
        Return GetNullableInt64(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "IsDBNull"
    <Extension()> _
    Public Function IsDBNull(ByVal dataRecord As IDataRecord, ByVal columnName As String) As Boolean
        Return dataRecord.IsDBNull(dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "Name"

#End Region

#Region "String"
    <Extension()> _
    Function GetString(ByVal dataRecord As IDataRecord, ByVal columnName As String) As String
        Return dataRecord.GetString(dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetString(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As String) As String
        If dataRecord.IsDBNull(ordinal) Then
            Return nullValue
        Else
            Return dataRecord.GetString(ordinal)
        End If
    End Function

    <Extension()> _
    Function GetString(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As String) As String
        Return GetString(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function
#End Region

#Region "GetValue"
    <Extension()> _
    Function GetValue(Of T)(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As T) As T
        Dim value As T

        If dataRecord.IsDBNull(ordinal) Then
            value = nullValue
        Else
            value = CType(dataRecord(ordinal), T)
        End If

        Return value
    End Function

    <Extension()> _
    Function GetValue(Of T)(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As T) As T
        Return GetValue(Of T)(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetValue(Of T)(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As T
        Return DirectCast(dataRecord.GetValue(ordinal), T)
    End Function

    <Extension()> _
    Function GetValue(Of T)(ByVal dataRecord As IDataRecord, ByVal columnName As String) As T
        Return GetValue(Of T)(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function

    <Extension()> _
    Function GetValue(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer, ByVal nullValue As Object) As Object
        Dim value As Object

        If dataRecord.IsDBNull(ordinal) Then
            value = nullValue
        Else
            value = dataRecord.GetValue(ordinal)
        End If

        Return value
    End Function

    <Extension()> _
    Function GetValue(ByVal dataRecord As IDataRecord, ByVal columnName As String, ByVal nullValue As Object) As Object
        Return GetValue(dataRecord, dataRecord.GetOrdinal(columnName), nullValue)
    End Function

    <Extension()> _
    Function GetNullableValue(Of T As Structure)(ByVal dataRecord As IDataRecord, ByVal ordinal As Integer) As T?
        If dataRecord.IsDBNull(ordinal) Then
            Return Nothing
        Else
            Return DirectCast(dataRecord.GetValue(ordinal), T)
        End If
    End Function

    <Extension()> _
    Function GetNullableValue(Of T As Structure)(ByVal dataRecord As IDataRecord, ByVal columnName As String) As T?
        Return GetNullableValue(Of T)(dataRecord, dataRecord.GetOrdinal(columnName))
    End Function
#End Region

#Region "ToDictionary"
    <Extension()>
    Public Function ToDictionary(ByVal dataRecord As IDataRecord) As Dictionary(Of String, Object)
        Dim dict As New Dictionary(Of String, Object)()
        For index = 0 To dataRecord.FieldCount - 1
            Dim key = dataRecord.GetName(index)
            Dim value = If(dataRecord.IsDBNull(index), Nothing, dataRecord.GetValue(index))
            dict.Add(key, value)
        Next

        Return dict
    End Function
#End Region

End Module