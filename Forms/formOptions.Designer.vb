<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formOptions
    Inherits System.Windows.Forms.Form

    'Form overrides dispose to clean up the component list.
    <System.Diagnostics.DebuggerNonUserCode()> _
    Protected Overrides Sub Dispose(ByVal disposing As Boolean)
        Try
            If disposing AndAlso components IsNot Nothing Then
                components.Dispose()
            End If
        Finally
            MyBase.Dispose(disposing)
        End Try
    End Sub

    'Required by the Windows Form Designer
    Private components As System.ComponentModel.IContainer

    'NOTE: The following procedure is required by the Windows Form Designer
    'It can be modified using the Windows Form Designer.  
    'Do not modify it using the code editor.
    <System.Diagnostics.DebuggerStepThrough()> _
    Private Sub InitializeComponent()
        Me.txtUpdateSeconds = New System.Windows.Forms.TextBox
        Me.Label1 = New System.Windows.Forms.Label
        Me.Label2 = New System.Windows.Forms.Label
        Me.btnSave = New System.Windows.Forms.Button
        Me.chkNotifyOnBlock = New System.Windows.Forms.CheckBox
        Me.chkNotifyOnClear = New System.Windows.Forms.CheckBox
        Me.chkMinimumLockAge = New System.Windows.Forms.CheckBox
        Me.txtMinimumLockAge = New System.Windows.Forms.TextBox
        Me.Label3 = New System.Windows.Forms.Label
        Me.TabControl1 = New System.Windows.Forms.TabControl
        Me.tabServers = New System.Windows.Forms.TabPage
        Me.btnServerListImport = New System.Windows.Forms.Button
        Me.btnServerListExport = New System.Windows.Forms.Button
        Me.btnRemove = New System.Windows.Forms.Button
        Me.btnAdd = New System.Windows.Forms.Button
        Me.txtAddServer = New System.Windows.Forms.TextBox
        Me.lbServerList = New System.Windows.Forms.ListBox
        Me.tabNotifications = New System.Windows.Forms.TabPage
        Me.txtNotifyAge = New System.Windows.Forms.TextBox
        Me.txtNotifyAgeReoccuring = New System.Windows.Forms.TextBox
        Me.Label5 = New System.Windows.Forms.Label
        Me.Label4 = New System.Windows.Forms.Label
        Me.chkBlockAgeNotifyReoccuring = New System.Windows.Forms.CheckBox
        Me.chkBlockAgeNotify = New System.Windows.Forms.CheckBox
        Me.tabSettings = New System.Windows.Forms.TabPage
        Me.FileDialogImport = New System.Windows.Forms.OpenFileDialog
        Me.FileDialogExport = New System.Windows.Forms.SaveFileDialog
        Me.TabControl1.SuspendLayout()
        Me.tabServers.SuspendLayout()
        Me.tabNotifications.SuspendLayout()
        Me.tabSettings.SuspendLayout()
        Me.SuspendLayout()
        '
        'txtUpdateSeconds
        '
        Me.txtUpdateSeconds.Location = New System.Drawing.Point(160, 13)
        Me.txtUpdateSeconds.Name = "txtUpdateSeconds"
        Me.txtUpdateSeconds.Size = New System.Drawing.Size(40, 20)
        Me.txtUpdateSeconds.TabIndex = 0
        '
        'Label1
        '
        Me.Label1.AutoSize = True
        Me.Label1.Location = New System.Drawing.Point(22, 16)
        Me.Label1.Name = "Label1"
        Me.Label1.Size = New System.Drawing.Size(132, 13)
        Me.Label1.TabIndex = 1
        Me.Label1.Text = "Refresh Frequency - every"
        '
        'Label2
        '
        Me.Label2.AutoSize = True
        Me.Label2.Location = New System.Drawing.Point(206, 16)
        Me.Label2.Name = "Label2"
        Me.Label2.Size = New System.Drawing.Size(47, 13)
        Me.Label2.TabIndex = 2
        Me.Label2.Text = "seconds"
        '
        'btnSave
        '
        Me.btnSave.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnSave.Location = New System.Drawing.Point(219, 262)
        Me.btnSave.Name = "btnSave"
        Me.btnSave.Size = New System.Drawing.Size(75, 23)
        Me.btnSave.TabIndex = 8
        Me.btnSave.Text = "Save"
        Me.btnSave.UseVisualStyleBackColor = True
        '
        'chkNotifyOnBlock
        '
        Me.chkNotifyOnBlock.AutoSize = True
        Me.chkNotifyOnBlock.Checked = True
        Me.chkNotifyOnBlock.CheckState = System.Windows.Forms.CheckState.Checked
        Me.chkNotifyOnBlock.Location = New System.Drawing.Point(6, 15)
        Me.chkNotifyOnBlock.Name = "chkNotifyOnBlock"
        Me.chkNotifyOnBlock.Size = New System.Drawing.Size(183, 17)
        Me.chkNotifyOnBlock.TabIndex = 5
        Me.chkNotifyOnBlock.Text = "When SQL process block occurs"
        Me.chkNotifyOnBlock.UseVisualStyleBackColor = True
        '
        'chkNotifyOnClear
        '
        Me.chkNotifyOnClear.AutoSize = True
        Me.chkNotifyOnClear.Location = New System.Drawing.Point(22, 38)
        Me.chkNotifyOnClear.Name = "chkNotifyOnClear"
        Me.chkNotifyOnClear.Size = New System.Drawing.Size(140, 17)
        Me.chkNotifyOnClear.TabIndex = 6
        Me.chkNotifyOnClear.Text = "...and when it's resolved"
        Me.chkNotifyOnClear.UseVisualStyleBackColor = True
        '
        'chkMinimumLockAge
        '
        Me.chkMinimumLockAge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.chkMinimumLockAge.AutoSize = True
        Me.chkMinimumLockAge.Location = New System.Drawing.Point(2, 41)
        Me.chkMinimumLockAge.Name = "chkMinimumLockAge"
        Me.chkMinimumLockAge.Size = New System.Drawing.Size(139, 17)
        Me.chkMinimumLockAge.TabIndex = 9
        Me.chkMinimumLockAge.Text = "Only track locks at least"
        Me.chkMinimumLockAge.UseVisualStyleBackColor = True
        '
        'txtMinimumLockAge
        '
        Me.txtMinimumLockAge.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.txtMinimumLockAge.Location = New System.Drawing.Point(143, 39)
        Me.txtMinimumLockAge.Name = "txtMinimumLockAge"
        Me.txtMinimumLockAge.Size = New System.Drawing.Size(40, 20)
        Me.txtMinimumLockAge.TabIndex = 10
        '
        'Label3
        '
        Me.Label3.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Left), System.Windows.Forms.AnchorStyles)
        Me.Label3.AutoSize = True
        Me.Label3.Location = New System.Drawing.Point(189, 42)
        Me.Label3.Name = "Label3"
        Me.Label3.Size = New System.Drawing.Size(64, 13)
        Me.Label3.TabIndex = 11
        Me.Label3.Text = "seconds old"
        '
        'TabControl1
        '
        Me.TabControl1.Controls.Add(Me.tabServers)
        Me.TabControl1.Controls.Add(Me.tabNotifications)
        Me.TabControl1.Controls.Add(Me.tabSettings)
        Me.TabControl1.Location = New System.Drawing.Point(12, 12)
        Me.TabControl1.Name = "TabControl1"
        Me.TabControl1.SelectedIndex = 0
        Me.TabControl1.Size = New System.Drawing.Size(283, 236)
        Me.TabControl1.TabIndex = 5
        '
        'tabServers
        '
        Me.tabServers.Controls.Add(Me.btnServerListImport)
        Me.tabServers.Controls.Add(Me.btnServerListExport)
        Me.tabServers.Controls.Add(Me.btnRemove)
        Me.tabServers.Controls.Add(Me.btnAdd)
        Me.tabServers.Controls.Add(Me.txtAddServer)
        Me.tabServers.Controls.Add(Me.lbServerList)
        Me.tabServers.Location = New System.Drawing.Point(4, 22)
        Me.tabServers.Name = "tabServers"
        Me.tabServers.Padding = New System.Windows.Forms.Padding(3)
        Me.tabServers.Size = New System.Drawing.Size(275, 210)
        Me.tabServers.TabIndex = 0
        Me.tabServers.Text = "Servers"
        Me.tabServers.UseVisualStyleBackColor = True
        '
        'btnServerListImport
        '
        Me.btnServerListImport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServerListImport.Location = New System.Drawing.Point(187, 153)
        Me.btnServerListImport.Name = "btnServerListImport"
        Me.btnServerListImport.Size = New System.Drawing.Size(85, 23)
        Me.btnServerListImport.TabIndex = 10
        Me.btnServerListImport.Text = "Import List..."
        Me.btnServerListImport.UseVisualStyleBackColor = True
        '
        'btnServerListExport
        '
        Me.btnServerListExport.Anchor = CType((System.Windows.Forms.AnchorStyles.Bottom Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.btnServerListExport.Location = New System.Drawing.Point(187, 182)
        Me.btnServerListExport.Name = "btnServerListExport"
        Me.btnServerListExport.Size = New System.Drawing.Size(85, 23)
        Me.btnServerListExport.TabIndex = 9
        Me.btnServerListExport.Text = "Export List..."
        Me.btnServerListExport.UseVisualStyleBackColor = True
        '
        'btnRemove
        '
        Me.btnRemove.Location = New System.Drawing.Point(187, 61)
        Me.btnRemove.Name = "btnRemove"
        Me.btnRemove.Size = New System.Drawing.Size(85, 23)
        Me.btnRemove.TabIndex = 8
        Me.btnRemove.Text = "Remove"
        Me.btnRemove.UseVisualStyleBackColor = True
        '
        'btnAdd
        '
        Me.btnAdd.Location = New System.Drawing.Point(187, 32)
        Me.btnAdd.Name = "btnAdd"
        Me.btnAdd.Size = New System.Drawing.Size(85, 23)
        Me.btnAdd.TabIndex = 7
        Me.btnAdd.Text = "Add"
        Me.btnAdd.UseVisualStyleBackColor = True
        '
        'txtAddServer
        '
        Me.txtAddServer.Anchor = CType((System.Windows.Forms.AnchorStyles.Top Or System.Windows.Forms.AnchorStyles.Right), System.Windows.Forms.AnchorStyles)
        Me.txtAddServer.Location = New System.Drawing.Point(150, 6)
        Me.txtAddServer.Name = "txtAddServer"
        Me.txtAddServer.Size = New System.Drawing.Size(122, 20)
        Me.txtAddServer.TabIndex = 6
        '
        'lbServerList
        '
        Me.lbServerList.FormattingEnabled = True
        Me.lbServerList.Items.AddRange(New Object() {"LOCALHOST"})
        Me.lbServerList.Location = New System.Drawing.Point(6, 6)
        Me.lbServerList.Name = "lbServerList"
        Me.lbServerList.Size = New System.Drawing.Size(138, 199)
        Me.lbServerList.Sorted = True
        Me.lbServerList.TabIndex = 5
        '
        'tabNotifications
        '
        Me.tabNotifications.Controls.Add(Me.txtNotifyAge)
        Me.tabNotifications.Controls.Add(Me.txtNotifyAgeReoccuring)
        Me.tabNotifications.Controls.Add(Me.Label5)
        Me.tabNotifications.Controls.Add(Me.Label4)
        Me.tabNotifications.Controls.Add(Me.chkBlockAgeNotifyReoccuring)
        Me.tabNotifications.Controls.Add(Me.chkBlockAgeNotify)
        Me.tabNotifications.Controls.Add(Me.chkNotifyOnBlock)
        Me.tabNotifications.Controls.Add(Me.chkNotifyOnClear)
        Me.tabNotifications.Location = New System.Drawing.Point(4, 22)
        Me.tabNotifications.Name = "tabNotifications"
        Me.tabNotifications.Padding = New System.Windows.Forms.Padding(3)
        Me.tabNotifications.Size = New System.Drawing.Size(275, 210)
        Me.tabNotifications.TabIndex = 1
        Me.tabNotifications.Text = "Notifications"
        Me.tabNotifications.UseVisualStyleBackColor = True
        '
        'txtNotifyAge
        '
        Me.txtNotifyAge.Location = New System.Drawing.Point(143, 69)
        Me.txtNotifyAge.Name = "txtNotifyAge"
        Me.txtNotifyAge.Size = New System.Drawing.Size(32, 20)
        Me.txtNotifyAge.TabIndex = 12
        '
        'txtNotifyAgeReoccuring
        '
        Me.txtNotifyAgeReoccuring.Location = New System.Drawing.Point(130, 91)
        Me.txtNotifyAgeReoccuring.Name = "txtNotifyAgeReoccuring"
        Me.txtNotifyAgeReoccuring.Size = New System.Drawing.Size(32, 20)
        Me.txtNotifyAgeReoccuring.TabIndex = 11
        '
        'Label5
        '
        Me.Label5.AutoSize = True
        Me.Label5.Location = New System.Drawing.Point(168, 95)
        Me.Label5.Name = "Label5"
        Me.Label5.Size = New System.Drawing.Size(92, 13)
        Me.Label5.TabIndex = 10
        Me.Label5.Text = "seconds after that"
        '
        'Label4
        '
        Me.Label4.AutoSize = True
        Me.Label4.Location = New System.Drawing.Point(183, 72)
        Me.Label4.Name = "Label4"
        Me.Label4.Size = New System.Drawing.Size(64, 13)
        Me.Label4.TabIndex = 9
        Me.Label4.Text = "seconds old"
        '
        'chkBlockAgeNotifyReoccuring
        '
        Me.chkBlockAgeNotifyReoccuring.AutoSize = True
        Me.chkBlockAgeNotifyReoccuring.Location = New System.Drawing.Point(22, 94)
        Me.chkBlockAgeNotifyReoccuring.Name = "chkBlockAgeNotifyReoccuring"
        Me.chkBlockAgeNotifyReoccuring.Size = New System.Drawing.Size(111, 17)
        Me.chkBlockAgeNotifyReoccuring.TabIndex = 8
        Me.chkBlockAgeNotifyReoccuring.Text = "...and again every"
        Me.chkBlockAgeNotifyReoccuring.UseVisualStyleBackColor = True
        '
        'chkBlockAgeNotify
        '
        Me.chkBlockAgeNotify.AutoSize = True
        Me.chkBlockAgeNotify.Location = New System.Drawing.Point(6, 71)
        Me.chkBlockAgeNotify.Name = "chkBlockAgeNotify"
        Me.chkBlockAgeNotify.Size = New System.Drawing.Size(139, 17)
        Me.chkBlockAgeNotify.TabIndex = 7
        Me.chkBlockAgeNotify.Text = "When a block becomes"
        Me.chkBlockAgeNotify.UseVisualStyleBackColor = True
        '
        'tabSettings
        '
        Me.tabSettings.Controls.Add(Me.txtUpdateSeconds)
        Me.tabSettings.Controls.Add(Me.Label3)
        Me.tabSettings.Controls.Add(Me.Label1)
        Me.tabSettings.Controls.Add(Me.txtMinimumLockAge)
        Me.tabSettings.Controls.Add(Me.chkMinimumLockAge)
        Me.tabSettings.Controls.Add(Me.Label2)
        Me.tabSettings.Location = New System.Drawing.Point(4, 22)
        Me.tabSettings.Name = "tabSettings"
        Me.tabSettings.Size = New System.Drawing.Size(275, 210)
        Me.tabSettings.TabIndex = 2
        Me.tabSettings.Text = "Settings"
        Me.tabSettings.UseVisualStyleBackColor = True
        '
        'FileDialogImport
        '
        Me.FileDialogImport.DefaultExt = "TXT"
        Me.FileDialogImport.FileName = "ServerList.txt"
        '
        'FileDialogExport
        '
        Me.FileDialogExport.DefaultExt = "TXT"
        Me.FileDialogExport.FileName = "ServerList.txt"
        '
        'formOptions
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(306, 297)
        Me.ControlBox = False
        Me.Controls.Add(Me.TabControl1)
        Me.Controls.Add(Me.btnSave)
        Me.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None
        Me.MaximizeBox = False
        Me.MinimizeBox = False
        Me.Name = "formOptions"
        Me.ShowIcon = False
        Me.ShowInTaskbar = False
        Me.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide
        Me.Text = "Options"
        Me.WindowState = System.Windows.Forms.FormWindowState.Maximized
        Me.TabControl1.ResumeLayout(False)
        Me.tabServers.ResumeLayout(False)
        Me.tabServers.PerformLayout()
        Me.tabNotifications.ResumeLayout(False)
        Me.tabNotifications.PerformLayout()
        Me.tabSettings.ResumeLayout(False)
        Me.tabSettings.PerformLayout()
        Me.ResumeLayout(False)

    End Sub
    Friend WithEvents txtUpdateSeconds As System.Windows.Forms.TextBox
    Friend WithEvents Label1 As System.Windows.Forms.Label
    Friend WithEvents Label2 As System.Windows.Forms.Label
    Friend WithEvents btnSave As System.Windows.Forms.Button
    Friend WithEvents chkNotifyOnBlock As System.Windows.Forms.CheckBox
    Friend WithEvents chkNotifyOnClear As System.Windows.Forms.CheckBox
    Friend WithEvents chkMinimumLockAge As System.Windows.Forms.CheckBox
    Friend WithEvents txtMinimumLockAge As System.Windows.Forms.TextBox
    Friend WithEvents Label3 As System.Windows.Forms.Label
    Friend WithEvents TabControl1 As System.Windows.Forms.TabControl
    Friend WithEvents tabServers As System.Windows.Forms.TabPage
    Friend WithEvents btnRemove As System.Windows.Forms.Button
    Friend WithEvents btnAdd As System.Windows.Forms.Button
    Friend WithEvents txtAddServer As System.Windows.Forms.TextBox
    Friend WithEvents lbServerList As System.Windows.Forms.ListBox
    Friend WithEvents tabNotifications As System.Windows.Forms.TabPage
    Friend WithEvents tabSettings As System.Windows.Forms.TabPage
    Friend WithEvents txtNotifyAge As System.Windows.Forms.TextBox
    Friend WithEvents txtNotifyAgeReoccuring As System.Windows.Forms.TextBox
    Friend WithEvents Label5 As System.Windows.Forms.Label
    Friend WithEvents Label4 As System.Windows.Forms.Label
    Friend WithEvents chkBlockAgeNotifyReoccuring As System.Windows.Forms.CheckBox
    Friend WithEvents chkBlockAgeNotify As System.Windows.Forms.CheckBox
    Friend WithEvents btnServerListImport As System.Windows.Forms.Button
    Friend WithEvents btnServerListExport As System.Windows.Forms.Button
    Friend WithEvents FileDialogImport As System.Windows.Forms.OpenFileDialog
    Friend WithEvents FileDialogExport As System.Windows.Forms.SaveFileDialog
End Class
