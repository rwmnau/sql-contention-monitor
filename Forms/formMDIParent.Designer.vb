<Global.Microsoft.VisualBasic.CompilerServices.DesignerGenerated()> _
Partial Class formMDIParent
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
        Me.components = New System.ComponentModel.Container
        Dim resources As System.ComponentModel.ComponentResourceManager = New System.ComponentModel.ComponentResourceManager(GetType(formMDIParent))
        Me.ToolStrip1 = New System.Windows.Forms.ToolStrip
        Me.tsbShowCurrentBlocks = New System.Windows.Forms.ToolStripButton
        Me.tsbShowHistory = New System.Windows.Forms.ToolStripButton
        Me.tsbOptions = New System.Windows.Forms.ToolStripButton
        Me.niNoBlocks = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.ContextMenuStrip1 = New System.Windows.Forms.ContextMenuStrip(Me.components)
        Me.AboutToolStripMenuItem = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTray_Options = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem3 = New System.Windows.Forms.ToolStripSeparator
        Me.cmsTray_Current = New System.Windows.Forms.ToolStripMenuItem
        Me.cmsTray_History = New System.Windows.Forms.ToolStripMenuItem
        Me.ToolStripMenuItem4 = New System.Windows.Forms.ToolStripSeparator
        Me.cmsTray_Exit = New System.Windows.Forms.ToolStripMenuItem
        Me.timerBalloonFailsafe = New System.Windows.Forms.Timer(Me.components)
        Me.Timer1 = New System.Windows.Forms.Timer(Me.components)
        Me.niNewInfo = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.niCurrentBlocks = New System.Windows.Forms.NotifyIcon(Me.components)
        Me.tsbAbout = New System.Windows.Forms.ToolStripButton
        Me.ToolStrip1.SuspendLayout()
        Me.ContextMenuStrip1.SuspendLayout()
        Me.SuspendLayout()
        '
        'ToolStrip1
        '
        Me.ToolStrip1.Dock = System.Windows.Forms.DockStyle.Left
        Me.ToolStrip1.ImageScalingSize = New System.Drawing.Size(48, 48)
        Me.ToolStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.tsbShowCurrentBlocks, Me.tsbShowHistory, Me.tsbOptions, Me.tsbAbout})
        Me.ToolStrip1.Location = New System.Drawing.Point(0, 0)
        Me.ToolStrip1.Name = "ToolStrip1"
        Me.ToolStrip1.Size = New System.Drawing.Size(53, 354)
        Me.ToolStrip1.TabIndex = 0
        Me.ToolStrip1.Text = "ToolStrip1"
        '
        'tsbShowCurrentBlocks
        '
        Me.tsbShowCurrentBlocks.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbShowCurrentBlocks.Image = CType(resources.GetObject("tsbShowCurrentBlocks.Image"), System.Drawing.Image)
        Me.tsbShowCurrentBlocks.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbShowCurrentBlocks.Name = "tsbShowCurrentBlocks"
        Me.tsbShowCurrentBlocks.Size = New System.Drawing.Size(50, 52)
        Me.tsbShowCurrentBlocks.Text = "Current Blocks"
        '
        'tsbShowHistory
        '
        Me.tsbShowHistory.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbShowHistory.Image = CType(resources.GetObject("tsbShowHistory.Image"), System.Drawing.Image)
        Me.tsbShowHistory.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbShowHistory.Name = "tsbShowHistory"
        Me.tsbShowHistory.Size = New System.Drawing.Size(50, 52)
        Me.tsbShowHistory.Text = "Block History"
        '
        'tsbOptions
        '
        Me.tsbOptions.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbOptions.Image = CType(resources.GetObject("tsbOptions.Image"), System.Drawing.Image)
        Me.tsbOptions.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbOptions.Name = "tsbOptions"
        Me.tsbOptions.Size = New System.Drawing.Size(50, 52)
        Me.tsbOptions.Text = "Application Options"
        '
        'niNoBlocks
        '
        Me.niNoBlocks.ContextMenuStrip = Me.ContextMenuStrip1
        Me.niNoBlocks.Icon = CType(resources.GetObject("niNoBlocks.Icon"), System.Drawing.Icon)
        Me.niNoBlocks.Text = "No current blocks"
        '
        'ContextMenuStrip1
        '
        Me.ContextMenuStrip1.Items.AddRange(New System.Windows.Forms.ToolStripItem() {Me.AboutToolStripMenuItem, Me.cmsTray_Options, Me.ToolStripMenuItem3, Me.cmsTray_Current, Me.cmsTray_History, Me.ToolStripMenuItem4, Me.cmsTray_Exit})
        Me.ContextMenuStrip1.Name = "ContextMenuStrip1"
        Me.ContextMenuStrip1.Size = New System.Drawing.Size(187, 126)
        '
        'AboutToolStripMenuItem
        '
        Me.AboutToolStripMenuItem.Name = "AboutToolStripMenuItem"
        Me.AboutToolStripMenuItem.Size = New System.Drawing.Size(186, 22)
        Me.AboutToolStripMenuItem.Text = "&About..."
        '
        'cmsTray_Options
        '
        Me.cmsTray_Options.Name = "cmsTray_Options"
        Me.cmsTray_Options.Size = New System.Drawing.Size(186, 22)
        Me.cmsTray_Options.Text = "&Options..."
        '
        'ToolStripMenuItem3
        '
        Me.ToolStripMenuItem3.Name = "ToolStripMenuItem3"
        Me.ToolStripMenuItem3.Size = New System.Drawing.Size(183, 6)
        '
        'cmsTray_Current
        '
        Me.cmsTray_Current.Name = "cmsTray_Current"
        Me.cmsTray_Current.Size = New System.Drawing.Size(186, 22)
        Me.cmsTray_Current.Text = "&Current Contention..."
        '
        'cmsTray_History
        '
        Me.cmsTray_History.Name = "cmsTray_History"
        Me.cmsTray_History.Size = New System.Drawing.Size(186, 22)
        Me.cmsTray_History.Text = "Contention &History"
        '
        'ToolStripMenuItem4
        '
        Me.ToolStripMenuItem4.Name = "ToolStripMenuItem4"
        Me.ToolStripMenuItem4.Size = New System.Drawing.Size(183, 6)
        '
        'cmsTray_Exit
        '
        Me.cmsTray_Exit.Name = "cmsTray_Exit"
        Me.cmsTray_Exit.Size = New System.Drawing.Size(186, 22)
        Me.cmsTray_Exit.Text = "E&xit"
        '
        'timerBalloonFailsafe
        '
        Me.timerBalloonFailsafe.Interval = 10000
        '
        'Timer1
        '
        '
        'niNewInfo
        '
        Me.niNewInfo.ContextMenuStrip = Me.ContextMenuStrip1
        Me.niNewInfo.Icon = CType(resources.GetObject("niNewInfo.Icon"), System.Drawing.Icon)
        Me.niNewInfo.Text = "New locks to view"
        '
        'niCurrentBlocks
        '
        Me.niCurrentBlocks.ContextMenuStrip = Me.ContextMenuStrip1
        Me.niCurrentBlocks.Icon = CType(resources.GetObject("niCurrentBlocks.Icon"), System.Drawing.Icon)
        Me.niCurrentBlocks.Text = "Locks currently active"
        '
        'tsbAbout
        '
        Me.tsbAbout.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Image
        Me.tsbAbout.Image = CType(resources.GetObject("tsbAbout.Image"), System.Drawing.Image)
        Me.tsbAbout.ImageTransparentColor = System.Drawing.Color.Magenta
        Me.tsbAbout.Name = "tsbAbout"
        Me.tsbAbout.Size = New System.Drawing.Size(50, 52)
        Me.tsbAbout.Text = "About"
        '
        'formMDIParent
        '
        Me.AutoScaleDimensions = New System.Drawing.SizeF(6.0!, 13.0!)
        Me.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font
        Me.ClientSize = New System.Drawing.Size(552, 354)
        Me.Controls.Add(Me.ToolStrip1)
        Me.IsMdiContainer = True
        Me.Name = "formMDIParent"
        Me.Text = "SQL Contention Monitor"
        Me.ToolStrip1.ResumeLayout(False)
        Me.ToolStrip1.PerformLayout()
        Me.ContextMenuStrip1.ResumeLayout(False)
        Me.ResumeLayout(False)
        Me.PerformLayout()

    End Sub
    Friend WithEvents ToolStrip1 As System.Windows.Forms.ToolStrip
    Friend WithEvents tsbShowCurrentBlocks As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbShowHistory As System.Windows.Forms.ToolStripButton
    Friend WithEvents tsbOptions As System.Windows.Forms.ToolStripButton
    Friend WithEvents niNoBlocks As System.Windows.Forms.NotifyIcon
    Friend WithEvents ContextMenuStrip1 As System.Windows.Forms.ContextMenuStrip
    Friend WithEvents AboutToolStripMenuItem As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTray_Options As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem3 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsTray_Current As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents cmsTray_History As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents ToolStripMenuItem4 As System.Windows.Forms.ToolStripSeparator
    Friend WithEvents cmsTray_Exit As System.Windows.Forms.ToolStripMenuItem
    Friend WithEvents timerBalloonFailsafe As System.Windows.Forms.Timer
    Friend WithEvents Timer1 As System.Windows.Forms.Timer
    Friend WithEvents niNewInfo As System.Windows.Forms.NotifyIcon
    Friend WithEvents niCurrentBlocks As System.Windows.Forms.NotifyIcon
    Friend WithEvents tsbAbout As System.Windows.Forms.ToolStripButton
End Class
