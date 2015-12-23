#If TargetFramework >= 4.0 Then
Imports System.Threading.Tasks
#End If

Public Interface IDistributedCommand

    ReadOnly Property ResultType As Type
    Function Execute() As Object

#If TargetFramework >= 4.0 Then
    Function ExecuteAsync() As Task(Of Object)
#End If

End Interface

Public Interface IDistributedCommand(Of T)
    Inherits IDistributedCommand

    Overloads Function Execute() As T

#If TargetFramework >= 4.0 Then
    Overloads Function ExecuteAsync() As Task(Of T)
#End If

End Interface