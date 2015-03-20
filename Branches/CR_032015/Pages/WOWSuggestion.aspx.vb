Partial Public Class WowSuggestion
    Inherits System.Web.UI.Page

    Dim _employee As EmployeeService.EmpInstance

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then
            ResetPage()
        End If
    End Sub

    Private Sub LoadEmployeeInfo()
        ' load employee
        _employee = GeneralFormsCommon.getEmployee()
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        Me.lblhName.Text = _employee.FullNameFirstLast
        Me.lblhDate.Text = Format(Date.Now, "Short Date")
        Me.lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Public Sub ResetPage()
        Me.pnlForm.Visible = True
        Me.pnlSuccess.Visible = False
    End Sub

    Protected Sub btnSubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnSubmit.Click
        Try
            SendMail()
            Me.pnlForm.Visible = False
            Me.pnlSuccess.Visible = True
        Catch ex As Exception
            MB.ShowError(ex)
        End Try
    End Sub

    Private Sub SendMail()
        Dim mailMsg As New MailMessage("CCC_Suggestion_Box@wideopenwest.com", "CCC_Suggestion_Box@wideopenwest.com")
        mailMsg.Subject = "WOW! Suggestion"
        If Not ViewState("MailTo") Is Nothing Then
            mailMsg.ReplyToList.Add(New MailAddress(ViewState("MailTo")))
        End If
        mailMsg.IsBodyHtml = "True"
        Dim body As String
        body = "<table cellpadding=""2"" cellspacing=""0"">"
        body &= "<tr>"
        body &= "<td style=""font-weight:bold;text-align:right"">User Name:</td>"
        body &= "<td>" & _employee.FullNameFirstLast & "</td>"
        body &= "</tr>"
        body &= "<tr>"
        body &= "<td style=""font-weight:bold;text-align:right"">ICOMS ID:</td>"
        body &= "<td>" & _employee.IcomsUserID & "</td>"
        body &= "</tr>"
        body &= "<tr>"
        body &= "<td style=""font-weight:bold;text-align:right"">Suggestion:</td>"
        body &= "<td>" & Me.txtComments.Text & "</td>"
        body &= "</tr>"
        body &= "</table><br />"
        mailMsg.Body = body
        '  Dim MailClient As New SmtpClient
        EmailProxy.Send(mailMsg)
    End Sub

    Protected Sub btnReturn_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnReturn.Click
        ResetPage()
    End Sub
End Class