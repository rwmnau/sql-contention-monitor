
Public Class formMDIParent

#Region " Declarations "

    Private IsFirstForm As Boolean
    Private BalloonCurrentlyShowing As Boolean = False

    Private AppCloseFlag As Boolean = False

    Private Delegate Sub Delegate_0Parameters()
    Private Delegate Sub Delegate_1Parameter_String(ByVal FirstParam As String)

#End Region

    Private Sub formMDIParent_FormClosing(ByVal sender As Object, ByVal e As System.Windows.Forms.FormClosingEventArgs) Handles Me.FormClosing

        If e.CloseReason = CloseReason.UserClosing And Not AppCloseFlag Then
            e.Cancel = True
            CloseFormToTray()
        Else
            Me.niNoBlocks.Visible = False
            Application.Exit()

        End If

    End Sub

#Region " Public Methods "

    Public Sub NewNotification()

        If Not FormController.MDIParent.Equals(Me) Then
            FormController.MDIParent.NewNotification()
            Me.Close()
        ElseIf InvokeRequired Then
            Me.Invoke(New Delegate_0Parameters(AddressOf NewNotification))

        Else

            If Not Me.BalloonCurrentlyShowing Then

                SyncLock LoadedSettings.Lock_NotificationQueue
                    Dim notification As String = LoadedSettings.GetNextNotification
                    If notification <> String.Empty Then
                        GetCurrentlyVisibleIcon.ShowBalloonTip(1500, "SQL Block Activity", _
                             notification, _
                             ToolTipIcon.Info)
                        Me.BalloonCurrentlyShowing = True

                    End If
                End SyncLock

                Me.timerBalloonFailsafe.Start()

            End If

        End If

    End Sub

    Public Sub ShowNewActionIcon()
        If Not FormController.MDIParent.Equals(Me) Then
            FormController.MDIParent.ShowNewActionIcon()
            Me.Close()
        ElseIf InvokeRequired Then
            Me.Invoke(New Delegate_0Parameters(AddressOf ShowNewActionIcon))

        Else
            Me.niNewInfo.Visible = True
            Me.niCurrentBlocks.Visible = False
            Me.niNoBlocks.Visible = False
        End If


    End Sub


#End Region


    Private Function GetCurrentlyVisibleIcon() As NotifyIcon
        If Me.niNoBlocks.Visible Then
            Return Me.niNoBlocks
        ElseIf Me.niCurrentBlocks.Visible Then
            Return Me.niCurrentBlocks
        ElseIf Me.niNewInfo.Visible Then
            Return Me.niNewInfo
        Else
            Throw New Exception("No icons are currently visible")
        End If
    End Function

    Private Sub timerBalloonFailsafe_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs)

        Me.timerBalloonFailsafe.Stop()

        Me.BalloonCurrentlyShowing = False
        NewNotification()

    End Sub


    Private Sub timerRefreshConnections_Tick(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Timer1.Tick
        SharedFunctions.RefreshAllConnections()
    End Sub


    Private Sub CloseFormToTray()
        Me.Visible = False
        If Not My.Settings.UserNotifiedAboutTray Then
            Me.niNoBlocks.ShowBalloonTip(1500, My.Application.Info.Title, _
                                         "This application has been minimized to the system tray. To access it, right-click on this icon", _
                                         ToolTipIcon.Info)
            My.Settings.UserNotifiedAboutTray = True
            My.Settings.Save()
        End If
    End Sub

    Public Sub RestoreFormToScreen()

        ' If "New Info" is showing, switch it back
        If Me.niNewInfo.Visible Then
            Me.niNewInfo.Visible = False
            For Each tn As TreeNode In FormController.CurrentBlocks.tvCurrentBlocks.Nodes
                If tn.FirstNode IsNot Nothing AndAlso tn.FirstNode.FirstNode IsNot Nothing Then
                    Me.niCurrentBlocks.Visible = True
                End If
            Next
            Me.niNoBlocks.Visible = Not Me.niCurrentBlocks.Visible
        End If

        ' Show the main form again
        Me.Visible = True
        Me.BringToFront()
        Me.Focus()

    End Sub

#Region " NotifyIcon Handlers "

    Private Sub NotifyIcon_BalloonTipClicked(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles niCurrentBlocks.BalloonTipClicked, niNewInfo.BalloonTipClicked, niNoBlocks.BalloonTipClicked

        RestoreFormToScreen()

    End Sub

    Private Sub NotifyIcon_BalloonTipClosed(ByVal sender As Object, ByVal e As System.EventArgs) _
            Handles niCurrentBlocks.BalloonTipClosed, niNewInfo.BalloonTipClosed, niNoBlocks.BalloonTipClosed


        Me.BalloonCurrentlyShowing = False
        NewNotification()

    End Sub

    Private Sub NotifyIcon_Click(ByVal sender As Object, ByVal e As System.Windows.Forms.MouseEventArgs) _
            Handles niCurrentBlocks.MouseClick, niNewInfo.MouseClick, niNoBlocks.MouseClick

        If e.Button = Windows.Forms.MouseButtons.Left Then
            RestoreFormToScreen()
        End If

    End Sub


#End Region

#Region " Context menu handlers "
    Private Sub cmsTray_Options_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTray_Options.Click
        DisplayChildForm(FormController.Options)
    End Sub

    Private Sub cmsTray_Current_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTray_Current.Click
        DisplayChildForm(FormController.CurrentBlocks)
    End Sub

    Private Sub cmsTray_History_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTray_History.Click

        DisplayChildForm(FormController.BlockHistory)

    End Sub

    Private Sub cmsTray_Exit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles cmsTray_Exit.Click

        ' If any threads need to be reigned in, do that first
        Me.AppCloseFlag = True
        Me.Close()
    End Sub

    Private Sub AboutToolStripMenuItem_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles AboutToolStripMenuItem.Click
        formAbout.ShowDialog()
    End Sub
#End Region



    Public Sub DisplayChildForm(ByRef WhichForm As Form)

        If WhichForm.Equals(FormController.CurrentBlocks) Then
            HighlightTsb(Me.tsbShowCurrentBlocks)
        ElseIf WhichForm.Equals(FormController.BlockHistory) Then
            HighlightTsb(Me.tsbShowHistory)
        ElseIf WhichForm.Equals(FormController.Options) Then
            HighlightTsb(Me.tsbOptions)
        End If

        If Me.WindowState = FormWindowState.Minimized Or Me.Visible = False Then
            Me.RestoreFormToScreen()
        End If

        If WhichForm.WindowState <> FormWindowState.Maximized Then
            WhichForm.WindowState = FormWindowState.Maximized
        End If

        WhichForm.BringToFront()

        If WhichForm.Visible = False Then
            WhichForm.Show()
        End If

    End Sub


    Private Sub tsbShowCurrentBlocks_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbShowCurrentBlocks.MouseDown
        DisplayChildForm(FormController.CurrentBlocks)
    End Sub

    Private Sub tsbShowHistory_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbShowHistory.MouseDown
        DisplayChildForm(FormController.BlockHistory)
    End Sub

    Private Sub tsbOptions_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbOptions.MouseDown
        DisplayChildForm(FormController.Options)
    End Sub

    Private Sub tsbAbout_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles tsbAbout.MouseDown
        formAbout.ShowDialog()
    End Sub

    Private Sub HighlightTsb(ByVal WhichTsb As Windows.Forms.ToolStripButton)
        If WhichTsb.Checked = False Then
            ' We only have to do this if the current button isn't checked, which means we're changing forms
            For Each tsb As ToolStripButton In Me.ToolStrip1.Items
                tsb.Checked = False
            Next

            WhichTsb.Checked = True

        End If

    End Sub

    Private Sub formMDIParent_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load

        Me.MainMenuStrip = New MenuStrip

        Me.Text = My.Application.Info.Title

        FormController.SetMDIParent(Me)

        Dim f As Form
        f = FormController.Options
        f = FormController.CurrentBlocks
        f = FormController.BlockHistory

        DisplayChildForm(FormController.CurrentBlocks)

        Me.niNoBlocks.Visible = True
        LoadedSettings.RefreshSettingsFromFile()

        Me.Timer1.Interval = My.Settings.RefreshFrequency * 1000
        Timer1.Start()


    End Sub
End Class