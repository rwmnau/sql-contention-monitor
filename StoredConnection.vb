Imports System.Data.SqlClient

Public Class StoredConnection
    Implements System.IComparable

    Public Event ForceTreeRefresh(ByVal server As String)
    Public Event NewNotificationMessage()
    Public Event NewAction()

    Public ReadOnly Property ConnectionError() As String
        Get
            Return _ConnectionError
        End Get
    End Property
    Private _ConnectionError As String = String.Empty

    Public ReadOnly Property CurrentlyBusy() As Boolean
        Get
            Return _CurrentlyBusy
        End Get
    End Property
    Private _CurrentlyBusy As Boolean = False

    Public ReadOnly BlockedProcessList As New SortedList

    Public ReadOnly Connection As SqlConnection
    Public ReadOnly ServerName As String

    Private _SecondsOffset As Integer = Integer.MinValue
    Public ReadOnly Property SecondsOffset() As Integer
        Get
            Return _SecondsOffset
        End Get
    End Property

    Public ForceNotifyOnNextProcess As Boolean = True

    Public Sub RefreshBlockedProcesses() 'Handles RefreshTimer.Tick

        Debug.WriteLine(String.Format("Begin refreshing processes on {0}", ServerName))

        If _CurrentlyBusy Then
            Debug.WriteLine(String.Format("Connection {0} is busy - exiting", ServerName))
            Exit Sub
        End If

        _CurrentlyBusy = True

        If Not RefreshConnection() Then
            _CurrentlyBusy = False
            Debug.WriteLine(String.Format("Connection {0} is {1} - exiting", ServerName, Connection.State.ToString))
            RaiseEvent ForceTreeRefresh(ServerName)
            Exit Sub
        End If

        _ConnectionError = String.Empty

        Dim ds As New DataSet

        Try

            Dim comm As SqlCommand


            If SecondsOffset = Integer.MinValue Then
                ' The time offset hasn't be set yet - do that first
                comm = New SqlCommand("select GETDATE()", Connection)
                Dim RemoteTime As DateTime = comm.ExecuteScalar
                Me._SecondsOffset = DateDiff(DateInterval.Second, Now, RemoteTime)
            End If

            ' Now we can proceed with the block checker
            comm = New SqlCommand("sp_who2", Connection)
            Dim da As New SqlDataAdapter(comm)
            da.Fill(ds)

        Catch ex As SqlException
            Me._ConnectionError = ex.Message
            Exit Sub
        End Try

        Dim LiveSPID As Boolean = False
        Dim TreeRefreshNeeded As Boolean = False

        Dim BlocksResolved As Boolean = False
        Dim NewBlocksDetected As Integer = 0

        For Each dr As DataRow In ds.Tables(0).Rows
            Dim bp As New BlockedProcess(dr)
            bp.TimeAdjust(Me._SecondsOffset)

            If bp.Workstation = "." Then
                bp.SetWorkstationName(ServerName)
            End If

            If bp.BlockingSPID = 0 Then
                ' The process isn't blocked - see if it's a parent

                For Each dr2 As DataRow In ds.Tables(0).Rows
                    Dim bp2 As New BlockedProcess(dr2)
                    bp2.TimeAdjust(Me._SecondsOffset)

                    If bp.SPID = bp2.BlockingSPID AndAlso PastDoNoNotifyWindow(bp.LastBatch) Then
                        ' The process is a parent, so add it
                        LiveSPID = True
                        If Not BlockedProcessList.ContainsKey(bp.SPID) Then
                            BlockedProcessList.Add(bp.SPID, bp)
                            TreeRefreshNeeded = True
                            'NewBlocksDetected += 1
                        End If

                        Exit For
                    End If
                Next

            Else
                ' Auto-add the process, since it's blocked
                If PastDoNoNotifyWindow(bp.LastBatch) Then

                    LiveSPID = True
                    If Not BlockedProcessList.ContainsKey(bp.SPID) Then

                        SyncLock LoadedSettings.Lock_SQLLockHistory
                            LoadedSettings.LockHistoryAddNew(bp)
                        End SyncLock

                        BlockedProcessList.Add(bp.SPID, bp)
                        TreeRefreshNeeded = True
                        NewBlocksDetected += 1
                    End If
                End If
            End If

            If Not LiveSPID Then
                If BlockedProcessList.ContainsKey(bp.SPID) Then
                    bp.Resolve()

                    ' Add an entry to the history process
                    SyncLock LoadedSettings.Lock_SQLLockHistory
                        LoadedSettings.LockHistoryAddNew(bp)
                    End SyncLock

                    BlockedProcessList.Remove(bp.SPID)
                    TreeRefreshNeeded = True
                    BlocksResolved = True
                End If
            End If

            LiveSPID = False

        Next

        If TreeRefreshNeeded Or ForceNotifyOnNextProcess Then
            Debug.WriteLine(String.Format("Connection {0} has changed - refreshing TreeView", ServerName))
            RaiseEvent ForceTreeRefresh(ServerName)
        End If


        If BlocksResolved And My.Settings.NotifyOnClear Then
            Debug.WriteLine(String.Format("Connection {0} has detected resolved blocks", ServerName))
            If My.Settings.NotifyOnClear Then

                SyncLock LoadedSettings.Lock_NotificationQueue
                    LoadedSettings.AddNewNotification(String.Format("Blocks resolved on {0}", ServerName))
                End SyncLock

                RaiseEvent NewNotificationMessage()
            End If
        End If

        If NewBlocksDetected Then

            RaiseEvent NewAction()

            If My.Settings.NotifyOnBlock Then

                Debug.WriteLine(String.Format("Connection {0} has detected {1} new blocks", ServerName, NewBlocksDetected))

                SyncLock LoadedSettings.Lock_NotificationQueue
                    LoadedSettings.AddNewNotification(String.Format("{0} new blocks detected on {1}", NewBlocksDetected, ServerName))
                End SyncLock

                RaiseEvent NewNotificationMessage()

            End If
        End If

        If My.Settings.NotifyOnLockAging Then
            For Each bp As BlockedProcess In BlockedProcessList.Values
                If DateDiff(DateInterval.Second, bp.NextNotifyTime, Now) > 0 And bp.BlockingSPID <> 0 Then
                    ' Time for another notification

                    Dim diff As TimeSpan = Now.Subtract(bp.LastBatch)
                    Dim HoursOld As Integer = diff.Hours
                    diff.Subtract(New TimeSpan(diff.Hours, 0, 0))
                    Dim MinutesOld As Integer = diff.Minutes
                    diff.Subtract(New TimeSpan(0, MinutesOld, 0))
                    Dim SecondsOld As Integer = diff.Seconds

                    SyncLock LoadedSettings.Lock_NotificationQueue
                        LoadedSettings.AddNewNotification(String.Format("Process {0} on {1} has been waiting for {2}", _
                                                                           bp.SPID, ServerName, _
                                                                           String.Format("{0}{1}{2}", _
                                                                            IIf(HoursOld, String.Format("{0} hours, ", HoursOld), String.Empty), _
                                                                            IIf(HoursOld Or MinutesOld, String.Format("{0} minutes, ", MinutesOld), String.Empty), _
                                                                            String.Format("{0} seconds", SecondsOld))))
                    End SyncLock

                    RaiseEvent NewNotificationMessage()

                    If My.Settings.NotifyOnLockAgingReoccuring Then
                        bp.NextNotifyTime = Now.AddSeconds(My.Settings.NotifyOnLockAgainReoccuringAge)
                    Else
                        bp.NextNotifyTime = Now.AddYears(1)
                    End If
                End If
            Next
        End If


        ForceNotifyOnNextProcess = False
        _CurrentlyBusy = False

        Debug.WriteLine(String.Format("Connection {0} is finished processing", ServerName))

    End Sub



    Public Sub New(ByVal Server As String, ByVal username As String, ByVal password As String)

        Debug.WriteLine(String.Format("New connection initiated for {0}", Server))

        AddHandler Me.ForceTreeRefresh, AddressOf FormController.CurrentBlocks.RefreshNodes
        AddHandler Me.NewNotificationMessage, AddressOf FormController.MDIParent.NewNotification
        AddHandler Me.NewAction, AddressOf FormController.MDIParent.ShowNewActionIcon

        ServerName = Server

        Connection = New SqlConnection(String.Format("server={0};{1}", Server, _
                                                     IIf(username = String.Empty, "Trusted_Connection=yes;", _
                                                            String.Format("Userid={0};password={1}", _
                                                                          username, password))))

    End Sub

    Public Sub New(ByVal server As String)
        Me.New(server, String.Empty, String.Empty)
    End Sub

    Public Function CompareTo(ByVal obj As Object) As Integer Implements System.IComparable.CompareTo

        If Not TypeOf obj Is ConnectionState Then Throw New ArgumentException

        Dim c As ConnectionState = CType(obj, ConnectionState)
        If Me.ServerName > obj.servername Then
            Return -1
        Else
            Return 1
        End If

    End Function

    Private Function RefreshConnection() As Boolean

        Try

            If Me.Connection.State = ConnectionState.Closed Then
                Connection.Open()
            ElseIf Me.Connection.State <> ConnectionState.Open Then
                Me._ConnectionError = String.Empty
                Return False
            End If

            Return True

        Catch ex As SqlException
            Me._ConnectionError = ex.Message
            Return False

        Catch ex As Exception
            ' This is an unexpected exception - don't handle it
            Throw
        End Try


    End Function

    ''' <summary>
    ''' Checks to see if the given datetime is past the notification window
    ''' </summary>
    ''' <param name="BatchStart">DateTime to check against window</param>
    ''' <returns></returns>
    ''' <remarks>Also checks to see if the option to respect the window is enabled</remarks>
    Private Function PastDoNoNotifyWindow(ByVal BatchStart As Date) As Boolean
        If My.Settings.RespectMinimumLockAge Then
            Return (DateDiff(DateInterval.Second, BatchStart, Now()) > My.Settings.MinimumLockAge)
        Else
            Return True
        End If
    End Function

End Class



