Imports System.Runtime.CompilerServices
Imports System.Threading
Imports System.Threading.Tasks

Public Module TaskFactoryExtensions

    <Extension()>
    Public Function FromResult(Of TResult)(factory As TaskFactory, result As TResult) As Task(Of TResult)
        Dim completionSource As New TaskCompletionSource(Of TResult)()
        completionSource.SetResult(result)
        Return completionSource.Task
    End Function

    <Extension()>
    Public Function CompletedTask(factory As TaskFactory) As Task
        Dim completionSource As New TaskCompletionSource(Of Boolean)()
        completionSource.SetResult(True)
        Return completionSource.Task
    End Function

    <Extension()>
    Public Function FalutedTask(Of TResult)(ex As Exception) As Task(Of TResult)
        Dim completionSource As New TaskCompletionSource(Of TResult)()
        completionSource.SetException(ex)
        Return completionSource.Task
    End Function

End Module