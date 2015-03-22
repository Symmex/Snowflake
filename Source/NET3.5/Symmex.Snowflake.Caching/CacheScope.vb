Imports System.Threading

Public Class CacheScope
    Implements IDisposable

#Region "Properties"
    Private Shared _CacheLock As New Object()

    Private Shared _Caches As Dictionary(Of Integer, Cache)
    Private Shared ReadOnly Property Caches As Dictionary(Of Integer, Cache)
        Get
            If _Caches Is Nothing Then
                _Caches = New Dictionary(Of Integer, Cache)()
            End If

            Return _Caches
        End Get
    End Property

    Public Shared ReadOnly Property CurrentCache As Cache
        Get
            Dim value As Cache = Nothing
            Caches.TryGetValue(Thread.CurrentThread.ManagedThreadId, value)
            Return value
        End Get
    End Property
#End Region

#Region "Constructors"
    Public Sub New()
        If Not Caches.ContainsKey(Thread.CurrentThread.ManagedThreadId) Then
            SyncLock _CacheLock
                Caches(Thread.CurrentThread.ManagedThreadId) = New Cache(Me)
            End SyncLock
        End If
    End Sub
#End Region

#Region "IDisposable Support"
    Private _IsDisposed As Boolean ' To detect redundant calls

    Protected Overridable Sub Dispose(disposing As Boolean)
        If Not _IsDisposed Then
            Dim current = CurrentCache

            If current IsNot Nothing AndAlso current.Owner Is Me Then
                SyncLock _CacheLock
                    Caches.Remove(Thread.CurrentThread.ManagedThreadId)
                End SyncLock
            End If
        End If

        _IsDisposed = True
    End Sub

    Public Sub Dispose() Implements IDisposable.Dispose
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

End Class