Imports System.IO
Imports System.Threading

Public Class formOptions

    Private Delegate Sub Delegate_0Parameters()


    Private Sub btnAdd_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnAdd.Click
        AddServerToList()
    End Sub


    Private Sub AddServerToList()
        If Me.txtAddServer.Text.Trim <> String.Empty And _
        Not Me.lbServerList.Items.Contains(Me.txtAddServer.Text.Trim.ToUpper) Then

            Me.lbServerList.Items.Add(Me.txtAddServer.Text.Trim.ToUpper)
            Me.btnServerListExport.Enabled = True
        End If

        Me.txtAddServer.Text = String.Empty
        Me.txtAddServer.Focus()
    End Sub


    Private Sub btnRemove_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRemove.Click
        If Me.lbServerList.SelectedItem IsNot Nothing Then
            Me.lbServerList.Items.Remove(Me.lbServerList.SelectedItem)
        End If

        If Me.lbServerList.Items.Count > 0 Then
            Me.lbServerList.SelectedIndex = 0
            Me.btnRemove.Focus()
        Else
            Me.btnServerListExport.Enabled = False
        End If
    End Sub


    Private Sub formOptions_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load

        Me.MdiParent = FormController.MDIParent
        Me.WindowState = FormWindowState.Maximized

        ' Copy all the application settings into this box
        If My.Settings.NotifyOnBlock Then
            Me.chkNotifyOnBlock.Checked = True
            Me.chkNotifyOnClear.Enabled = True
            Me.chkNotifyOnClear.Checked = My.Settings.NotifyOnClear
        Else
            Me.chkNotifyOnBlock.Checked = False
            Me.chkNotifyOnClear.Checked = False
            Me.chkNotifyOnClear.Enabled = False
        End If

        Me.chkMinimumLockAge.Checked = My.Settings.RespectMinimumLockAge
        Me.txtMinimumLockAge.Enabled = My.Settings.RespectMinimumLockAge
        Me.txtMinimumLockAge.Text = My.Settings.MinimumLockAge

        Me.txtUpdateSeconds.Text = My.Settings.RefreshFrequency

        Me.lbServerList.Items.Clear()
        SyncLock LoadedSettings.Lock_Connections
            For Each c As String In LoadedSettings.Connections.Keys
                Me.lbServerList.Items.Add(c)
            Next
        End SyncLock

        Me.chkBlockAgeNotify.Checked = My.Settings.NotifyOnLockAging
        Me.txtNotifyAge.Enabled = My.Settings.NotifyOnLockAging
        Me.chkBlockAgeNotifyReoccuring.Enabled = My.Settings.NotifyOnLockAging
        Me.chkBlockAgeNotifyReoccuring.Checked = My.Settings.NotifyOnLockAgingReoccuring
        Me.txtNotifyAgeReoccuring.Enabled = My.Settings.NotifyOnLockAging And My.Settings.NotifyOnLockAgingReoccuring

        Me.txtNotifyAge.Text = My.Settings.NotifyOnLockAging_Age
        Me.txtNotifyAgeReoccuring.Text = My.Settings.NotifyOnLockAgainReoccuringAge

    End Sub


    Private Sub btnSave_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSave.Click

        ' Save all the settings to the Settings object
        My.Settings.NotifyOnBlock = Me.chkNotifyOnBlock.Checked
        My.Settings.NotifyOnClear = Me.chkNotifyOnClear.Checked
        My.Settings.RefreshFrequency = Me.txtUpdateSeconds.Text

        My.Settings.RespectMinimumLockAge = Me.chkMinimumLockAge.Checked
        My.Settings.MinimumLockAge = Me.txtMinimumLockAge.Text

        Dim ServerList As String = String.Empty
        For Each s As String In Me.lbServerList.Items
            serverlist += s + "|"
        Next
        My.Settings.ServerList = DPAPI.ProtectString(serverlist.ToUpper)

        My.Settings.NotifyOnLockAging = Me.chkBlockAgeNotify.Checked
        My.Settings.NotifyOnLockAging_Age = Me.txtNotifyAge.Text
        My.Settings.NotifyOnLockAgingReoccuring = Me.chkBlockAgeNotifyReoccuring.Checked
        My.Settings.NotifyOnLockAgainReoccuringAge = Me.txtNotifyAgeReoccuring.Text

        My.Settings.Save()

        LoadedSettings.RefreshSettingsFromFile()

        ' Make sure server list is in sync with the options page
        Dim listedservers As New Collection
        For Each tn As TreeNode In FormController.CurrentBlocks.tvCurrentBlocks.Nodes
            listedservers.Add(tn.Text)
        Next
        For Each server As String In listedservers
            ThreadPool.QueueUserWorkItem(AddressOf FormController.CurrentBlocks.RefreshNodes, server)
        Next

        FormController.MDIParent.DisplayChildForm(FormController.CurrentBlocks)

    End Sub


    Private Sub chkNotifyOnBlock_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkNotifyOnBlock.CheckedChanged
        If Me.chkNotifyOnBlock.Checked = True Then
            Me.chkNotifyOnClear.Enabled = True
        Else
            Me.chkNotifyOnClear.Checked = False
            Me.chkNotifyOnClear.Enabled = False
        End If
    End Sub


    Private Sub txtAddServer_KeyPress(ByVal sender As Object, ByVal e As System.Windows.Forms.KeyPressEventArgs) Handles txtAddServer.KeyPress
        If e.KeyChar = ControlChars.Cr Then
            AddServerToList()
        End If

    End Sub


    Private Sub chkMinimumLockAge_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkMinimumLockAge.CheckedChanged
        Me.txtMinimumLockAge.Enabled = Me.chkMinimumLockAge.Checked
    End Sub


    Private Sub chkBlockAgeNotify_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBlockAgeNotify.CheckedChanged
        With chkBlockAgeNotify
            Me.chkBlockAgeNotifyReoccuring.Enabled = .Checked
            Me.txtNotifyAge.Enabled = .Checked
            Me.txtNotifyAgeReoccuring.Enabled = Me.chkBlockAgeNotifyReoccuring.Checked

        End With
    End Sub


    Private Sub chkBlockAgeNotifyReoccuring_CheckedChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles chkBlockAgeNotifyReoccuring.CheckedChanged
        Me.txtNotifyAgeReoccuring.Enabled = Me.chkBlockAgeNotifyReoccuring.Checked
    End Sub


    Private Sub btnServerListImport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerListImport.Click
        If Me.FileDialogImport.ShowDialog = Windows.Forms.DialogResult.OK Then
            ImportServerList(Me.FileDialogImport.FileName)
        End If
    End Sub


    Private Sub ImportServerList(ByVal Filename As String)
        Dim sr As New StreamReader(Filename)
        Dim servers() As String = sr.ReadToEnd.Split(ControlChars.CrLf)

        Dim NewServerCount As Integer = 0
        For Each server In servers
            If server.Trim <> String.Empty AndAlso _
                    server.Trim.Substring(0, 1) <> "#" AndAlso _
                    Not server.Contains(" ") Then

                NewServerCount += 1
            End If
        Next

        If NewServerCount = 0 Then
            MessageBox.Show("This file does not contain any servers to import", "Invalid File", MessageBoxButtons.OK)
        Else
            If MessageBox.Show(String.Format("This file contains {0} servers. Merge then with your list?", NewServerCount), _
                               "Add new servers?", MessageBoxButtons.YesNo, MessageBoxIcon.Question) = Windows.Forms.DialogResult.Yes Then
                For Each server As String In servers
                    If Not server.Trim = String.Empty AndAlso Not server.Trim.Substring(0, 1) = "#" AndAlso _
                            Not Me.lbServerList.Items.Contains(server.Trim.ToUpper) Then

                        Me.lbServerList.Items.Add(server.Trim.ToUpper)
                    End If
                Next
            End If

        End If
    End Sub


    Private Sub btnServerListExport_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnServerListExport.Click
        If Me.lbServerList.Items.Count > 0 AndAlso Me.FileDialogExport.ShowDialog = Windows.Forms.DialogResult.OK Then
            ExportServerList(Me.FileDialogExport.FileName)
        End If
    End Sub


    Private Sub ExportServerList(ByVal FileName As String)

        Using sw As New StreamWriter(FileName, False)
            sw.WriteLine(String.Format("#Server list exported on {0}", Now.ToShortDateString))
            For Each server As String In Me.lbServerList.Items
                sw.WriteLine(server)
            Next
            sw.Flush()
        End Using

        MessageBox.Show("Export successful", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information)

    End Sub


End Class