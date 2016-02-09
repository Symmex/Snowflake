Imports System.Runtime.CompilerServices
Imports System.Threading.Tasks

Public Module TaskCompletionSourceExtensions

    <Extension()>
    Public Sub SetResultFromTask(Of T)(tcs As TaskCompletionSource(Of T), resultTask As Task(Of T))
        If resultTask.IsFaulted Then
            tcs.SetException(resultTask.Exception)
        End If

        If resultTask.IsCanceled Then
            tcs.SetCanceled()
        End If

        tcs.SetResult(resultTask.Result)
    End Sub

End Module