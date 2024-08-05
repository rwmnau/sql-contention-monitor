Imports System.Data.SqlClient
Imports System.Net
Imports System.Threading

Class formBlockCurrent

    Private Delegate Sub Delegate_0Parameters()
    Private Delegate Sub Delegate_1Parameter_String(ByVal FirstParam As String)

    Private Sub AddBlockedChildren(ByVal SPID As Integer, ByVal BlockedProcessList As SortedList, ByRef node As TreeNode)
        For Each bp As BlockedProcess In BlockedProcessList.Values
            If bp.BlockingSPID = SPID Then

                Dim child As New TreeNode
                child.Text = String.Format("{2} - {0} from {1}", _
                                            bp.Login, bp.Workstation, bp.SPID)

                child.Tag = bp.SPID
                AddBlockedChildren(bp.SPID, BlockedProcessList, child)
                node.Nodes.Add(child)
            End If
        Next
    End Sub

    Public Sub AddNewConnectionToTreeList(ByVal Servername As String)

        If InvokeRequired Then
            Me.Invoke(New Delegate_1Parameter_String(AddressOf AddNewConnectionToTreeList), Servername)
        Else
            Dim tn As TreeNode
            tn = New TreeNode(Servername)
            tn.Tag = Servername
            Me.tvCurrentBlocks.Nodes.Add(tn)
        End If

    End Sub

    Private NodeHandlerLock As New Object

    Public Sub RefreshNodes(ByVal Server As String)

        If InvokeRequired Then
            ' Added to handle case where refresh happens just as they're pressing the Save button
            If Me.tvCurrentBlocks.Nodes.ContainsKey(Server) And _
                    LoadedSettings.Connections.ContainsKey(Server) Then

                Me.Invoke(New Delegate_1Parameter_String(AddressOf RefreshNodes), Server)
            End If
        Else

            Debug.WriteLine(String.Format("Begin refreshing TreeView for {0}", Server))

            SyncLock NodeHandlerLock

                If Not LoadedSettings.Connections.ContainsKey(Server) Then
                    Debug.WriteLine(String.Format("Server {0} is missing from the connection list", Server))
                    For i As Integer = 0 To Me.tvCurrentBlocks.Nodes.Count - 1
                        If Me.tvCurrentBlocks.Nodes(i).Text = Server Then
                            Me.tvCurrentBlocks.Nodes.RemoveAt(i)
                        End If
                    Next
                    Exit Sub
                End If

                Dim connSQL As StoredConnection = LoadedSettings.Connections.Item(Server)

                Dim ServerNode As New TreeNode(connSQL.ServerName)
                ServerNode.Tag = connSQL.ServerName
                AddBlockedChildren(0, connSQL.BlockedProcessList, ServerNode)

                If ServerNode.Nodes.Count = 0 Then
                    If connSQL.ConnectionError <> String.Empty Then
                        ServerNode.Nodes.Add(connSQL.ConnectionError)
                    End If
                End If

                Dim TotalNodes As Integer = Me.tvCurrentBlocks.Nodes.Count
                For currentNode As Integer = 0 To Me.tvCurrentBlocks.Nodes.Count - 1

                    If Me.tvCurrentBlocks.Nodes(currentNode).Text = Server Then
                        Me.tvCurrentBlocks.Nodes.RemoveAt(currentNode)
                        Me.tvCurrentBlocks.Nodes.Add(ServerNode)
                        Me.tvCurrentBlocks.Sort()

                        If ServerNode.Nodes.Count > 0 Then
                            Me.tvCurrentBlocks.Nodes(currentNode).Expand()
                        End If

                        Exit For
                    End If
                Next

            End SyncLock

            Debug.WriteLine(String.Format("Finished refreshing nodes for {0}", Server))

        End If

    End Sub



    Private Sub tvCurrentBlocks_AfterSelect(ByVal sender As System.Object, ByVal e As System.Windows.Forms.TreeViewEventArgs) Handles tvCurrentBlocks.AfterSelect
        ' Add details of the selected node to this window
        txtProcessDetails.Text = String.Empty

        If tvCurrentBlocks.SelectedNode IsNot Nothing AndAlso tvCurrentBlocks.SelectedNode.Parent IsNot Nothing Then
            Dim node As TreeNode = tvCurrentBlocks.SelectedNode
            Dim sc As StoredConnection

            ' Get the server from the selected node
            ' Search for the first lower-case letter, and then find the backslash before that
            ' That's where the servername ends
            Dim i As Integer = tvCurrentBlocks.SelectedNode.FullPath.IndexOf(" ")
            For j As Integer = i To 0 Step -1
                If Me.tvCurrentBlocks.SelectedNode.FullPath.Substring(j, 1) = "\" Then
                    sc = LoadedSettings.Connections(tvCurrentBlocks.SelectedNode.FullPath.Substring(0, j))
                    Exit For
                End If
            Next

            If IsNothing(sc) Then
                Me.txtProcessDetails.Text = "This block has been resolved"
            Else
                Me.txtProcessDetails.Text = String.Format("Server: {0}", sc.ServerName)

                If tvCurrentBlocks.SelectedNode.Tag IsNot Nothing Then

                    Dim bp As BlockedProcess = sc.BlockedProcessList(tvCurrentBlocks.SelectedNode.Tag)
                    If IsNothing(bp) Then
                        Me.txtProcessDetails.Text = "This block has been resolved"
                    Else
                        Me.txtProcessDetails.Text += SharedFunctions.GetLongDescriptionFromProcess(bp)
                    End If

                End If

            End If
        End If

    End Sub



    Private Sub btnRefresh_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnRefresh.Click
        SharedFunctions.RefreshAllConnections()

    End Sub

    Private Sub formBlockCurrent_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        Me.MdiParent = FormController.MDIParent
        Me.WindowState = FormWindowState.Maximized

    End Sub
End Class
