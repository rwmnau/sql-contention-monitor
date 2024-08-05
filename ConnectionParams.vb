<Serializable()> _
Public Class ConnectionParams

    Public Enum ConnectionType As Integer
        Unknown = 0
        Integrated = 1
        SQLAuth = 2
    End Enum

    Public ServerName As String = String.Empty
    Public UserName As String = String.Empty
    Public Password As String = String.Empty
    Public Type As ConnectionType = ConnectionType.Unknown

    ''' <summary>
    ''' Creates a new connection, assuming SQL authentication if a username/password is provided.
    ''' </summary>
    ''' <param name="s">Servername for new connection</param>
    ''' <param name="u">Username (if using SQL authentication)</param>
    ''' <param name="p">Password (if using SQL authentication)</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal s As String, ByVal u As String, ByVal p As String)

        If s = String.Empty Then
            Throw New ArgumentException("Servername cannot be blank")
        End If

        If u = String.Empty Or p = String.Empty Then
            Throw New ArgumentException("Username and password cannot be provided and empty", _
                                        "Instead, to use integrated auth, call without passing a username/password")
        End If

        ServerName = s
        UserName = u
        Password = p

        Type = ConnectionType.SQLAuth

    End Sub

    ''' <summary>
    ''' Creates a new connection, assuming integrated authentication
    ''' </summary>
    ''' <param name="s">Servername for new connection</param>
    ''' <remarks></remarks>
    Public Sub New(ByVal s As String)

        If s = String.Empty Then
            Throw New ArgumentException("Servername cannot be blank")
        End If

        ServerName = s
        Type = ConnectionType.Integrated
    End Sub

    ''' <summary>
    ''' Returns the servername as a string
    ''' </summary>
    ''' <returns></returns>
    ''' <remarks></remarks>
    Public Overrides Function ToString() As String
        Return ServerName
    End Function

End Class
