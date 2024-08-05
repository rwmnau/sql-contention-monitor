Imports System.Text
Imports System.Threading



Public Class SharedFunctions

    Public Shared Function GetLongDescriptionFromProcess(ByVal bp As BlockedProcess) As String

        Dim sb As New StringBuilder

        Dim HoursOld As Integer = DateDiff(DateInterval.Second, bp.LastBatch, Now) \ 3600
        Dim MinutesOld As Integer = (DateDiff(DateInterval.Second, bp.LastBatch, Now) Mod 3600) \ 60
        Dim SecondsOld As Integer = DateDiff(DateInterval.Second, bp.LastBatch, Now) Mod 60

        sb.AppendLine(String.Format("SPID: {0}", bp.SPID))
        sb.AppendLine(String.Format("Workstation: {0}", bp.Workstation))
        sb.AppendLine(String.Format("Username: {0}", bp.Login))
        sb.AppendLine(String.Format("Database: {0}", bp.DBName))
        sb.AppendLine(String.Format("Program Name: {0}", bp.ProgramName))

        sb.AppendLine(String.Format("Block Age: {0}{1}{2}", _
                                    IIf(HoursOld, String.Format("{0} hours, ", HoursOld), String.Empty), _
                                    IIf(HoursOld Or MinutesOld, String.Format("{0} minutes, ", MinutesOld), String.Empty), _
                                    String.Format("{0} seconds", SecondsOld)))

        Return sb.ToString

    End Function

    ''' <summary>
    ''' Iterate list of current connections and refresh them
    ''' </summary>
    ''' <remarks>Spawns threads to refresh each connection</remarks>
    Public Shared Sub RefreshAllConnections()

        SyncLock LoadedSettings.Lock_Connections
            For Each sc As StoredConnection In LoadedSettings.Connections.Values
                If Not sc.CurrentlyBusy Then
                    ThreadPool.QueueUserWorkItem(AddressOf sc.RefreshBlockedProcesses)
                End If
            Next
        End SyncLock

    End Sub

End Class
