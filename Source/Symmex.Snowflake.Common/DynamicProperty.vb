Public Class DynamicProperty(Of T)
    Implements IDynamicProperty

#Region "Properties"
    Private _Name As String
    Public ReadOnly Property Name As String Implements IDynamicProperty.Name
        Get
            Return _Name
        End Get
    End Property

    Private _DefaultValueResolver As IResolver(Of T)
    Public ReadOnly Property DefaultValueResolver As IResolver(Of T)
        Get
            Return _DefaultValueResolver
        End Get
    End Property

    Private ReadOnly Property IDynamicProperty_DefaultValueResolver As IResolver Implements IDynamicProperty.DefaultValueResolver
        Get
            Return Me.DefaultValueResolver
        End Get
    End Property
#End Region

#Region "Constructors"
    Public Sub New(name As String)
        _Name = name
    End Sub

    Public Sub New(name As String, defaultValue As T)
        Me.New(name, New InstanceResolver(Of T)(defaultValue))
    End Sub

    Public Sub New(name As String, defaultValueFunction As Func(Of T))
        Me.New(name, New MethodResolver(Of T)(defaultValueFunction))
    End Sub

    Public Sub New(name As String, defaultValueResolver As IResolver(Of T))
        _Name = name
        _DefaultValueResolver = defaultValueResolver
    End Sub
#End Region

End Class