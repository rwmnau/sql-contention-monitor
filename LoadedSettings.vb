Imports System.Threading
Imports System.Security.Cryptography

Public Class LoadedSettings

#Region " Tray notifications "

    ''' <summary>
    ''' List of notifications still pending for the user
    ''' </summary>
    ''' <remarks></remarks>
    Private Shared _NotificationQueue As New List(Of String)
    Public Shared Lock_NotificationQueue As New Object
    Public Shared Function GetNextNotification() As String

        Dim notifymessage As String = String.Empty

        SyncLock Lock_NotificationQueue

            If _NotificationQueue.Count > 0 Then

                notifymessage = _NotificationQueue.Item(0)
                _NotificationQueue.RemoveAt(0)
            End If

        End SyncLock

        Return notifymessage

    End Function
    Public Shared Sub AddNewNotification(ByVal newmessage As String)
        If newmessage.Trim <> String.Empty Then
            SyncLock Lock_NotificationQueue
                _NotificationQueue.Add(newmessage)

                If _NotificationQueue.Count > My.Settings.MaximumQueueDepth Then
                    Debug.WriteLine(String.Format("New message overflowed queue - {0} messages expired", _
                                                    _NotificationQueue.Count - My.Settings.MaximumQueueDepth))

                    Do Until _NotificationQueue.Count = My.Settings.MaximumQueueDepth
                        _NotificationQueue.RemoveAt(0)
                    Loop
                Else
                    Debug.WriteLine(String.Format("New message added - queue depth={0}", _
                                                    _NotificationQueue.Count))

                End If

            End SyncLock

        End If



    End Sub

#End Region

#Region " Stored Connections "

    Public Shared Lock_Connections As New Object
    Private Shared _Connections As New SortedList
    Public Shared ReadOnly Property Connections() As SortedList
        Get
            Return _Connections
        End Get
    End Property
    Public Shared Sub AddNewConnection(ByVal value As StoredConnection)
        SyncLock Lock_Connections
            If Not _Connections.Contains(value.ServerName) Then
                _Connections.Add(value.ServerName, value)
            End If

        End SyncLock
    End Sub

#End Region

#Region " Block history "

    Public Shared Lock_SQLLockHistory As New Object
    Private Shared _LockHistory As Collection = New Collection
    Public Shared Sub LockHistoryClear()
        SyncLock Lock_SQLLockHistory
            _LockHistory.Clear()
        End SyncLock
    End Sub
    Public Shared Sub LockHistoryAddNew(ByVal bp As BlockedProcess)

        SyncLock Lock_SQLLockHistory
            _LockHistory.Add(bp)

            If FormController.BlockHistory IsNot Nothing And Not IsHistoryEventHooked Then
                AddHandler HistoryChanged, AddressOf FormController.BlockHistory.NotifyHistoryRefreshNeeded
                IsHistoryEventHooked = True
            End If

            RaiseEvent HistoryChanged()

        End SyncLock

    End Sub
    Public Shared Function GetLockHistory() As Collection
        Return _LockHistory
    End Function
    Public Shared Sub LockHistoryChangeDepth()
        SyncLock Lock_SQLLockHistory

            If _LockHistory.Count > My.Settings.LockHistoryDepth Then
                Do Until _LockHistory.Count <= My.Settings.LockHistoryDepth
                    _LockHistory.Remove(0)
                Loop
            End If

        End SyncLock

    End Sub

    Public Shared Event HistoryChanged()
    Private Shared IsHistoryEventHooked As Boolean = False

#End Region


    'Public Shared RefreshFrequency As Integer

    'Public Shared NotifyOnBlock As Boolean
    'Public Shared NotifyOnClear As Boolean

    'Public Shared MinimumLockAge As Integer
    'Public Shared RespectMinimumLockAge As Boolean

    Shared Event PrepareToCreateConnection(ByVal ServerName As String)

    ''' <summary>
    ''' Configure settings by loading them from saved location
    ''' </summary>
    ''' <remarks></remarks>
    Public Shared Sub RefreshSettingsFromFile()

        RemoveHandler PrepareToCreateConnection, AddressOf FormController.CurrentBlocks.AddNewConnectionToTreeList
        AddHandler PrepareToCreateConnection, AddressOf FormController.CurrentBlocks.AddNewConnectionToTreeList

        If My.Settings.RefreshFrequency < 1 Then My.Settings.RefreshFrequency = 1

        My.Settings.NotifyOnClear = My.Settings.NotifyOnClear And My.Settings.NotifyOnBlock

        Dim ServerList As String
        If My.Settings.ServerList = String.Empty Then
            ServerList = "LOCALHOST"
        Else
            Try
                ServerList = DPAPI.UnprotectString(My.Settings.ServerList)
            Catch ex As CryptographicException
                ServerList = "LOCALHOST"
            End Try

        End If

        Dim KeysToRemove As New Collection
        SyncLock LoadedSettings.Lock_Connections
            For Each sc As String In LoadedSettings.Connections.Keys
                Dim ConnectionExists As Boolean = False
                For Each sc2 As String In ServerList.ToUpper.Split("|")
                    If sc = sc2 Then
                        ConnectionExists = True
                        Exit For
                    End If
                Next
                If Not ConnectionExists Then
                    KeysToRemove.Add(sc)
                End If
            Next
            For Each sc As String In KeysToRemove
                LoadedSettings.Connections.Remove(sc)
            Next
        End SyncLock

        For Each sc As String In ServerList.ToUpper.Split("|")
            SyncLock LoadedSettings.Lock_Connections

                If sc <> String.Empty And Not LoadedSettings.Connections.ContainsKey(sc) Then
                    ThreadPool.QueueUserWorkItem(AddressOf CreateConnection, sc)
                End If

            End SyncLock
        Next

        FormController.MDIParent.Timer1.Interval = My.Settings.RefreshFrequency * 1000

    End Sub

    Public Shared Sub CreateConnection(ByVal cp As ConnectionParams)

        RaiseEvent PrepareToCreateConnection(cp.ServerName)

        Dim sc As New StoredConnection(cp.ServerName, cp.UserName, cp.Password)

        SyncLock LoadedSettings.Lock_Connections
            LoadedSettings.AddNewConnection(sc)
        End SyncLock

        sc.RefreshBlockedProcesses()

    End Sub

    Public Shared Sub CreateConnection(ByVal ServerName As String)
        CreateConnection(New ConnectionParams(ServerName))
    End Sub

    Public Shared Sub CreateConnection(ByVal ServerName As Object)
        CreateConnection(New ConnectionParams(CType(ServerName.ToString, String)))
    End Sub


    Public Sub New()

        AddHandler HistoryChanged, AddressOf FormController.BlockHistory.NotifyHistoryRefreshNeeded

    End Sub
End Class

