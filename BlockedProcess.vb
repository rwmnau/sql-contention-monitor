Public Class BlockedProcess

    Public NextNotifyTime As Date = Now.AddSeconds(My.Settings.NotifyOnLockAging_Age)

    Public ReadOnly SPID As Integer
    Public ReadOnly Status As String
    Public ReadOnly Login As String
    Public ReadOnly BlockingSPID As Integer
    Public ReadOnly DBName As String
    Public ReadOnly Command As String
    Public ReadOnly ProgramName As String

    Public ReadOnly Property ResolvedAt() As DateTime
        Get
            Return _ResolvedAt
        End Get
    End Property
    Private _ResolvedAt As DateTime

    Public ReadOnly Property Workstation() As String
        Get
            Return _Workstation
        End Get
    End Property
    Private _Workstation As String

    Public ReadOnly Property LastBatch() As DateTime
        Get
            Return _LastBatch
        End Get
    End Property
    Private _LastBatch As DateTime


    Public IsBlocking As List(Of BlockedProcess) = New List(Of BlockedProcess)

    Public ReadOnly BlockDetected As DateTime

    Public Sub New(ByVal dr As DataRow)

        SPID = dr.Item(0)
        Status = Trim(dr.Item(1))
        Login = Trim(dr.Item(2))
        _Workstation = Trim(dr.Item(3))
        BlockingSPID = IIf(Trim(dr.Item(4)) = ".", 0, dr.Item(4))

        If TypeOf dr.Item(5) Is DBNull Then
            DBName = String.Empty
        Else
            DBName = Trim(dr.Item(5))
        End If

        Command = Trim(dr.Item(6))

        _LastBatch = Convert.ToDateTime(String.Format("{0}/{1} {2}", dr.Item(9).ToString.Substring(0, 5), Now.Year, _
                                                      dr.Item(9).ToString.Substring(6, dr.Item(9).ToString.Length - 6)))
        _ResolvedAt = DateAdd(DateInterval.Day, -1, LastBatch)

        ProgramName = Trim(dr.Item(10))

        BlockDetected = Now

    End Sub

    Public Sub Resolve()
        _ResolvedAt = Now
    End Sub

    ''' <summary>
    ''' If workstation name is ".", set it to the server name
    ''' </summary>
    ''' <param name="newname">Name of the server the BP belongs to</param>
    ''' <remarks></remarks>
    Public Sub SetWorkstationName(ByVal newname As String)

        ' If the servername is ".", change it to something meaningful
        If _Workstation = "." Then
            _Workstation = newname
        Else
            Throw New ArgumentException("Can't change the workstation name if it's already descriptive")
        End If
    End Sub

    ''' <summary>
    ''' Adjusts the timestamps to reflect any offset between workstation and server
    ''' </summary>
    ''' <param name="seconds">Seconds of offset to adjust</param>
    ''' <remarks></remarks>
    Public Sub TimeAdjust(ByVal seconds As Integer)
        Me._LastBatch = DateAdd(DateInterval.Second, -seconds, Me.LastBatch)

    End Sub
End Class
