Imports System.Data.Common
Imports System.Threading
#If TargetFramework >= "4.0" Then
Imports System.Threading.Tasks
#End If

Public Interface IDb
    Inherits IDisposable

    ReadOnly Property Connection As DbConnection
    ReadOnly Property Transaction As DbTransaction
    Property ConnectionRetryCount As Integer
    Property CommandTimeout As Integer
    Sub OpenConnection()
    Function CreateCommand(ByVal text As String) As DbCommand
    Function CreateCommand(ByVal text As String, ByVal type As CommandType) As DbCommand
    Sub BeginTransaction()
    Sub CommitTransaction()

#If TargetFramework >= "4.0" Then
    Function OpenConnectionAsync() As Task
#End If

End Interface