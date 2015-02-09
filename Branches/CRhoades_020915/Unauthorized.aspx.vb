Partial Public Class Unauthorized
    Inherits System.Web.UI.Page

    Private _authLogin As String

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        _authLogin = Session("AuthUser")
        ' Dim MailClient As SmtpClient = New SmtpClient
        Dim mailMsg As New MailMessage
        mailMsg.To.Clear()
        ' MailMsg.To.Add(New MailAddress("Jordan Read <j_read@wideopenwest.com>"))
        mailMsg.To.Add(New MailAddress("Daniel Davis <d_davis@wideopenwest.com>"))
        mailMsg.From = New MailAddress("CCCIT <ccc_it@wideopenwest.com>")
        mailMsg.Subject = "Web Access Error - Invalid Login for " & _authLogin & "(" & Session("AuthUserAC1") & ")"
        mailMsg.Body = _authLogin & "(" & Session("AuthUserAC1") & ") tried to access GeneralForms and was redirected to the Unauthorized page because the user was not found or is not active in the [CS-DBSVR03\SQL2008_Main].[EmployeeMaster].[dbo].[EMPLOYEES] database"
#If DEBUG Then
#Else
        EmailProxy.Send(mailMsg)
#End If
    End Sub

End Class