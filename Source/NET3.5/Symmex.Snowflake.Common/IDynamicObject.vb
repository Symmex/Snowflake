Public Interface IDynamicObject

    Function GetValue(dp As IDynamicProperty) As Object
    Sub SetValue(dp As IDynamicProperty, value As Object)

End Interface