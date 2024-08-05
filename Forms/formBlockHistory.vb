Public Class formBlockHistory

    Private Delegate Sub Delegate_0Parameters()

    Private Sub btnClose_Click(ByVal sender As System.Object, ByVal e As System.EventArgs)
        Me.Close()
    End Sub

    Private Sub btnClear_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnClear.Click
        LoadedSettings.LockHistoryClear()
        RefreshHistoryList()

    End Sub

    Private Sub formBlockHistory_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MdiParent = FormController.MDIParent
        Me.WindowState = FormWindowState.Maximized

        RefreshHistoryList()

    End Sub

    Private Sub RefreshHistoryList()

        Me.lbHistory.Items.Clear()

        Dim sl As Collection = LoadedSettings.GetLockHistory

        For Each bp As BlockedProcess In sl
            If bp.ResolvedAt < bp.LastBatch Then
                ' Block is outstanding
                Me.lbHistory.Items.Add(String.Format("{0} on {1}: SPID {2} blocked", _
                                                     bp.LastBatch, bp.Workstation, _
                                                     bp.SPID))

            Else
                ' Block has been resolved
                Me.lbHistory.Items.Add(String.Format("{0} on {1}: SPID {2} resolved - waited {3} seconds", _
                                                     bp.ResolvedAt, bp.Workstation, _
                                                     bp.SPID, DateDiff(DateInterval.Second, _
                                                                       bp.LastBatch, _
                                                                       bp.ResolvedAt)))

            End If

        Next

        Me.lbHistory.Sorted = True

    End Sub

    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click

        RefreshHistoryList()
        Me.btnRefresh.Enabled = False

    End Sub

    Public Sub NotifyHistoryRefreshNeeded()

        If Me.InvokeRequired Then
            Me.Invoke(New Delegate_0Parameters(AddressOf NotifyHistoryRefreshNeeded))
        Else
            Me.btnRefresh.Enabled = True
        End If

    End Sub
End Class

