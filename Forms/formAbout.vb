Public NotInheritable Class formAbout

    Private Sub formAbout_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        ' Set the title of the form.
        Dim ApplicationTitle As String
        If My.Application.Info.Title <> "" Then
            ApplicationTitle = My.Application.Info.Title
        Else
            ApplicationTitle = System.IO.Path.GetFileNameWithoutExtension(My.Application.Info.AssemblyName)
        End If
        Me.Text = String.Format("About {0}", ApplicationTitle)
        ' Initialize all of the text displayed on the About Box.
        Me.LabelProductName.Text = My.Application.Info.ProductName
        Me.LabelVersion.Text = String.Format("Version {0}", My.Application.Info.Version.ToString)
        Me.LabelCopyright.Text = My.Application.Info.Copyright
        Me.LabelCompanyName.Text = My.Application.Info.CompanyName
        Me.TextBoxDescription.Text = My.Application.Info.Description

        If FormController.MDIParent.Visible = False Then
            ' Center it on the screen
            Me.Left = (Screen.PrimaryScreen.WorkingArea.Width - Me.Width) / 2
            Me.Top = (Screen.PrimaryScreen.WorkingArea.Height - Me.Height) / 2
        Else
            ' Place it over the center of the application
            With FormController.MDIParent
                Me.Left = .Left + (.Width - Me.Width) / 2
                Me.Top = .Top + (.Height - Me.Height) / 2

            End With

        End If
    End Sub

    Private Sub OKButton_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles OKButton.Click
        Me.Close()
    End Sub

End Class
