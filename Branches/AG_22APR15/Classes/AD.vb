Imports System.DirectoryServices

Public Class AD
    Implements IDisposable

    Const ADName As String = "WideOpenWest"
    Private ReadOnly _userName, _password As String
    Private _ldapPath As String = "LDAP://WideOpenWest"

    Public Sub New(ByVal username, ByVal password)
        _userName = username
        _password = password
    End Sub

    Public Function CheckUser(ByVal user As String) As Boolean

        Dim de1 As DirectoryEntry = New DirectoryEntry

        Dim domainAndUsername As String = ADName & "\" & _userName

        de1.Path = _ldapPath
        de1.Username = domainAndUsername
        de1.Password = _password

        Try
            Dim ds1 As DirectorySearcher = New DirectorySearcher(de1)

            ds1.Filter = "(SAMAccountName=" + user + ")"
            ds1.PropertiesToLoad.Add("cn")
            Dim result As SearchResult = ds1.FindOne()

            If IsNothing(result) Then
                Return False
            End If

            _ldapPath = result.Path


            Return True

        Catch ex As Exception
            Return False

        Finally
            de1.Dispose()
        End Try

    End Function



    Public Function ChangePass(ByVal user, ByVal newpass) As Boolean

        Dim deuser As DirectoryEntry = New DirectoryEntry
        Dim domainAndUsername As String = ADName & "\" & _userName

        deuser.Path = "WinNT://WideOpenWest/" + user
        deuser.Username = domainAndUsername
        deuser.Password = _password

        Try
            'deuser = New DirectoryEntry("LDAP://WideOpenWest/CN=" & user & ",OU=WOWCentral,OU=Colorado,DC=wideopenwest,DC=com", domainAndUsername, strPassword, AuthenticationTypes.Secure)
            deuser.Invoke("SetPassword", newpass)
            deuser.CommitChanges()
            Return True

        Catch ex As Exception
            Throw
        Finally
            deuser.Dispose()
        End Try

    End Function

    Public Function GetInfo(ByVal user) As Array
        Dim deuser As DirectoryEntry = New DirectoryEntry
        Dim deuser2 As DirectoryEntry = New DirectoryEntry

        Dim domainAndUsername As String = ADName & "\" & _userName
        Dim arrResults(10) As String

        deuser.Path = _ldapPath
        deuser.Username = domainAndUsername
        deuser.Password = _password



        Dim deSearch As New DirectorySearcher(deuser)
        deSearch.Filter = "(SAMAccountName=" + user + ")"


        deuser2.Path = "WinNT://WideOpenWest/" + user
        deuser2.Username = domainAndUsername
        deuser2.Password = _password


        Try
            deSearch.PropertiesToLoad.Add("sn")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(0) = result.Properties("sn")(0).ToString
        Catch ex As Exception
            arrResults(0) = ""
        End Try

        Try
            deSearch.PropertiesToLoad.Add("givenName")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(1) = result.Properties("givenName")(0).ToString
        Catch ex As Exception
            arrResults(1) = ""
        End Try


        Try
            Dim binIL As Boolean
            binIL = deuser2.InvokeGet("IsAccountLocked")
            If binIL Then
                arrResults(2) = "Yes"
            Else
                arrResults(2) = "No"
            End If


        Catch ex As Exception
            arrResults(2) = ""
        End Try

        Try
            deSearch.PropertiesToLoad.Add("telephoneNumber")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(3) = result.Properties("telephoneNumber")(0).ToString
        Catch ex As Exception
            arrResults(3) = ""
        End Try

        Try
            deSearch.PropertiesToLoad.Add("mail")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(4) = result.Properties("mail")(0).ToString

        Catch ex As Exception
            arrResults(4) = ""
        End Try

        Try
            deSearch.PropertiesToLoad.Add("badPasswordTime")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(5) = DateTime.FromFileTime(result.Properties("badPasswordTime")(0)).ToString

        Catch ex As Exception
            arrResults(5) = ""
        End Try

        Try
            deSearch.PropertiesToLoad.Add("lastLogon")
            Dim result As SearchResult = deSearch.FindOne()
            arrResults(6) = DateTime.FromFileTime(result.Properties("lastLogon")(0)).ToString


        Catch ex As Exception
            arrResults(6) = ""
        End Try

        arrResults(7) = _ldapPath
        Return arrResults

        deuser.Dispose()
        deuser2.Dispose()


    End Function

    Public Function ResetAcct(ByVal user) As Boolean
        Dim deuser2 As New DirectoryEntry
        Dim domainAndUsername As String = ADName & "\" & _userName

        deuser2.Path = "WinNT://WideOpenWest/" + user
        deuser2.Username = domainAndUsername
        deuser2.Password = _password

        Try

            deuser2.InvokeSet("IsAccountLocked", False)
            deuser2.CommitChanges()


            Return True
        Catch ex As Exception

            Return False
        Finally
            deuser2.Dispose()

        End Try


    End Function


#Region "Dispose"

    Private _disposedValue As Boolean = False        ' To detect redundant calls

    ' IDisposable
    Protected Overridable Sub Dispose(ByVal disposing As Boolean)
        If Not Me._disposedValue Then
            If disposing Then
                ' TODO: free unmanaged resources when explicitly called
            End If

            ' TODO: free shared unmanaged resources
        End If
        Me._disposedValue = True
    End Sub




#Region " IDisposable Support "
    ' This code added by Visual Basic to correctly implement the disposable pattern.
    Public Sub Dispose() Implements IDisposable.Dispose
        ' Do not change this code.  Put cleanup code in Dispose(ByVal disposing As Boolean) above.
        Dispose(True)
        GC.SuppressFinalize(Me)
    End Sub
#End Region

#End Region

End Class
