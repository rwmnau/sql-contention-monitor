Imports System
Imports System.Security.Cryptography

''' <summary>
''' Encrypts and Decrypts information using the current Windows user key
''' </summary>
''' <remarks></remarks>
Public Class DPAPI

    Private Shared EntropyString As String = "Some value I made up"

    ' Create byte array for additional entropy when using Protect/Unprotect method.
    Private Shared Function AdditionalEntropy() As Byte()
        Dim encoder As New System.Text.ASCIIEncoding
        Return encoder.GetBytes(EntropyString)
    End Function

#Region " Encrypt "

    Public Shared Function Protect(ByVal data() As Byte) As Byte()
        Try
            ' Encrypt the data using DataProtectionScope.CurrentUser. The result can be decrypted
            '  only by the same current user.
            Return ProtectedData.Protect(data, AdditionalEntropy, DataProtectionScope.CurrentUser)
        Catch e As CryptographicException
            Console.WriteLine("Data was not encrypted. An error occurred.")
            Console.WriteLine(e.Message.ToString())
            Throw
        End Try

    End Function

    Public Shared Function ProtectString(ByVal data As String) As String

        Dim encoder As New System.Text.ASCIIEncoding
        Return Convert.ToBase64String(Protect(encoder.GetBytes(data)))

    End Function

#End Region

#Region " Decrypt "

    Public Shared Function Unprotect(ByVal data() As Byte) As Byte()
        Try
            'Decrypt the data using DataProtectionScope.CurrentUser.
            Return ProtectedData.Unprotect(data, AdditionalEntropy, DataProtectionScope.CurrentUser)
        Catch e As CryptographicException
            Console.WriteLine("Data was not decrypted. An error occurred.")
            Console.WriteLine(e.Message.ToString())
            Throw
        End Try

    End Function

    Public Shared Function UnprotectString(ByVal data As String) As String

        Dim encoder As New System.Text.ASCIIEncoding
        Dim b() As Byte = Convert.FromBase64String(data)
        Return encoder.GetString(Unprotect(b))

    End Function

#End Region

End Class
