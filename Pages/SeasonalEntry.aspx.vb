Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Net.Mail


Partial Public Class SeasonalEntry
    Inherits System.Web.UI.Page
    Private _employee As EmployeeService.EmpInstance
    Private _myCustomer As CustomerService.Cust

#Region "Email Settings"

    Private _sEmailTo As String = "c_rhoades@wideopenwest.com"
    Private _sEmailFrom As String = "c_rhoades@wideopenwest.com"
    Private _sEmailSubject As String = "Seasonal Request"

#End Region

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            txtAcct.Focus()
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

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsubmit.Click
        Try
            'Check some date data first
            If CDate(rdpBegin.DbSelectedDate.ToString.Trim).Date > CDate(rdpEnd.DbSelectedDate.ToString.Trim).Date _
            Or CDate(rdpBegin.DbSelectedDate.ToString.Trim) < Date.Now.Date _
            Or DateDiff(DateInterval.Day, CDate(rdpBegin.DbSelectedDate.ToString.Trim).Date, CDate(rdpEnd.DbSelectedDate.ToString.Trim).Date) < 30 _
            Or DateDiff(DateInterval.Day, CDate(rdpBegin.DbSelectedDate.ToString.Trim).Date, CDate(rdpEnd.DbSelectedDate.ToString.Trim).Date) > 180 Then
                lblerr.Text = "Begin and End dates are outside of the rules: <br />" & vbCrLf
                lblerr.Text += "&nbsp;&nbsp;&nbsp;* Begin Date must be before End Date <br />" & vbCrLf
                lblerr.Text += "&nbsp;&nbsp;&nbsp;* Begin Date must be today or in the future <br />" & vbCrLf
                lblerr.Text += "&nbsp;&nbsp;&nbsp;* Time span between Begin Date and End Date must be equal to or greater than 30 days  <br />" & vbCrLf
                lblerr.Text += "&nbsp;&nbsp;&nbsp;* Time span between Begin Date and End Date must not exceed 180 days  <br />" & vbCrLf
                lblerr.Visible = True
                Exit Sub
            End If

            If PutInDB() > 0 Then
                lblThankYou.Text = "Thank you for submitting the request for: " & txtFirstName.Text.Trim & " " & txtLastName.Text.Trim
                lblThankYou.Visible = True
                SendEmail()
                ClearForm()
            Else
                lblThankYou.Text = "The form did not submit cortrectly. Please try again and if issue oersists contact IT "
                lblThankYou.Visible = True
            End If


        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Protected Sub ibGo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibGo.Click
        Dim tempAccount As String = txtAcct.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revAccount.Validate()
        Me.rfvAccount.Validate()
        If Not (Me.revAccount.IsValid And Me.rfvAccount.IsValid) Then
            Me.MB.ShowMessage("Invalid account number format or account number missing.")
            Exit Sub
        End If

        Try
            Using customerClient As New CustomerService.CustomerManagementClient
                _myCustomer = customerClient.getByCustomerID(tempAccount)
            End Using
        Catch ex As Exception
            Dim errorAccount = ex.Message
            If errorAccount = "Customer Database Object is nothing." Then
                Me.MB.ShowMessage("Account number does not exist. Please try again.")
                txtAcct.Text = ""
                Exit Sub
            End If
        End Try



        If _myCustomer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(_myCustomer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            txtLastName.Text = String.Empty
            txtFirstName.Text = String.Empty
            Exit Sub
        End If
        txtLastName.Text = _myCustomer.LName
        txtFirstName.Text = _myCustomer.FName
    End Sub

    Protected Sub txtAcctNum_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub

    Private Function PutInDB() As Integer

        Try

            'Response.Write("I am here")

            If txtFirstName.Text.Trim = String.Empty And _
                txtLastName.Text.Trim = String.Empty And _
                txtAcct.Text.Trim = String.Empty Then
                Return 0
            End If

            Dim db As Database = DatabaseFactory.CreateDatabase("SeasonalAudit")
            Dim cmd As DbCommand = db.GetStoredProcCommand("procInsertSeasonalWO")

            db.AddInParameter(cmd, "@OrderStatus", DbType.Int32, 1)
            db.AddInParameter(cmd, "@CustFirstName", DbType.String, txtFirstName.Text.Trim)
            db.AddInParameter(cmd, "@CustLastName", DbType.String, txtLastName.Text.Trim)
            db.AddInParameter(cmd, "@CSGAcctNum", DbType.String, txtAcct.Text.ToString.Trim)
            db.AddInParameter(cmd, "@BeginDate", DbType.DateTime, rdpBegin.DbSelectedDate.ToString.Trim)
            db.AddInParameter(cmd, "@EndDate", DbType.DateTime, rdpEnd.DbSelectedDate.ToString.Trim)

            If rdoBTEPCSG.Checked Then
                db.AddInParameter(cmd, "@BillType", DbType.Int32, 1)
            ElseIf rdoBT.Checked Then
                db.AddInParameter(cmd, "@BillType", DbType.Int32, 2)
            ElseIf rdoEP.Checked Then
                db.AddInParameter(cmd, "@BillType", DbType.Int32, 3)
            End If

            db.AddInParameter(cmd, "@BillToFirstName", DbType.String, txtBTFirstName.Text.Trim)
            db.AddInParameter(cmd, "@BillToLastName", DbType.String, txtBTLastName.Text.Trim)
            db.AddInParameter(cmd, "@BillToAddr1", DbType.String, txtAddr1.Text.Trim)
            db.AddInParameter(cmd, "@BillToAddr2", DbType.String, txtAddr2.Text.Trim)
            db.AddInParameter(cmd, "@BillToCity", DbType.String, txtCity.Text.Trim)
            db.AddInParameter(cmd, "@BillToState", DbType.String, txtState.Text.Trim)
            db.AddInParameter(cmd, "@BillToZip", DbType.String, txtZip.Text.Trim)

            db.AddInParameter(cmd, "@Comments", DbType.String, txtComments.Text.Trim)
            db.AddInParameter(cmd, "@OpNTName", DbType.String, _employee.NTLogin)
            db.AddInParameter(cmd, "@OpCSGID", DbType.String, _employee.IcomsUserID)

            Dim iReturn As Integer = db.ExecuteNonQuery(cmd)

            Return iReturn

        Catch ex As Exception
            ShowError(ex)
            Return 0
        End Try

    End Function

    Private Sub SendEmail()

        Try

            Dim mailMsg As New MailMessage
            mailMsg.To.Add(_sEmailTo)
            mailMsg.From = New MailAddress(_sEmailFrom)
            mailMsg.IsBodyHtml = False
            mailMsg.Subject = _sEmailSubject

            Dim sBody As New StringBuilder
            sBody.Append("New Seasonal Request" & vbCrLf & vbCrLf)
            sBody.Append("Customer First Name:" & vbTab & txtFirstName.Text.Trim & vbCrLf)
            sBody.Append("Customer Last Name:" & vbTab & txtLastName.Text.Trim & vbCrLf)
            sBody.Append("Customer Acct Num: " & vbTab & txtAcct.Text.Trim & vbCrLf)
            sBody.Append("Begin Date:" & vbTab & vbTab & rdpBegin.DbSelectedDate & vbCrLf)
            sBody.Append("End Date:" & vbTab & vbTab & rdpEnd.DbSelectedDate & vbCrLf)

            sBody.Append(vbCrLf & "----------------------------------------" & vbCrLf)
            If rdoBTEPCSG.Checked = True Then
                sBody.Append("Pay Type:" & vbTab & vbTab & "Bill-To or Ezpay is already in CSG" & vbCrLf)
            ElseIf rdoBT.Checked = True Then
                sBody.Append("Pay Type:" & vbTab & vbTab & vbTab & "Bill-To" & vbCrLf)
                sBody.Append("Bill-To First Name:" & vbTab & txtBTFirstName.Text.Trim & vbCrLf)
                sBody.Append("Bill-To Last Name: " & vbTab & txtBTLastName.Text.Trim & vbCrLf)
                sBody.Append("Address 1:" & vbTab & vbTab & vbTab & txtAddr1.Text.Trim & vbCrLf)
                sBody.Append("Address 2:" & vbTab & vbTab & vbTab & txtAddr2.Text.Trim & vbCrLf)
                sBody.Append("City:" & vbTab & vbTab & vbTab & vbTab & txtCity.Text.Trim & vbCrLf)
                sBody.Append("State:" & vbTab & vbTab & vbTab & txtState.Text.Trim & vbCrLf)
                sBody.Append("Zip:" & vbTab & vbTab & vbTab & vbTab & txtZip.Text.Trim & vbCrLf)
            ElseIf rdoEP.Checked = True Then
                sBody.Append("Pay Type:" & vbTab & vbTab & vbTab & "Ezpay" & vbCrLf)
            End If
            sBody.Append("----------------------------------------" & vbCrLf & vbCrLf)

            sBody.Append("Comments:" & vbCrLf & vbTab & txtComments.Text.Trim & vbCrLf & vbCrLf)

            sBody.Append("CSR Name:" & vbTab & vbTab & _employee.FullNameFirstLast & vbCrLf)
            sBody.Append("CSR ICOMS ID:" & vbTab & vbTab & _employee.IcomsUserID & vbCrLf)
            sBody.Append("CSR Username:" & vbTab & vbTab & _employee.NTLogin & vbCrLf & vbCrLf)

            sBody.Append("----------------------------------------" & vbCrLf & vbCrLf)
            sBody.Append("Existing Campaigns:" & vbTab & "Verify the status of existing Campaigns" & vbCrLf)
            sBody.Append("Employee acknowledged message:" & vbCrLf & labelExistingCampaigns.Text & vbCrLf)

            mailMsg.Body = sBody.ToString

            EmailProxy.Send(mailMsg)

        Catch ex As Exception
            ShowError(ex)
        End Try

    End Sub

    Private Sub ShowError(ByVal ex As Exception)

        lblerr.Text += "<b>Message: </b>" & ex.Message & vbCrLf & "<br /><br />"
        lblerr.Text += "<b>Stack Trace: </b>" & ex.StackTrace.ToString.Trim & vbCrLf & "<br />"
        lblerr.Visible = True

    End Sub

    Private Sub ClearForm()
        Try
            Dim cTxt As TextBox
            For Each c As Control In Me.pnlform.Controls
                If IsNothing(TryCast(c, TextBox)) = False Then
                    cTxt = CType(c, TextBox)
                    cTxt.Text = ""
                End If
            Next
            rdpBegin.Clear()
            rdpEnd.Clear()
            rdoBTEPCSG.Checked = True
        Catch ex As Exception
            ShowError(ex)
        End Try
    End Sub

    'Displays pop-ups depending on option selected for existing Campaigns, addes message to email advising which option was selected.
    Private Sub rblExistingCampaign_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rblExistingCampaign.SelectedIndexChanged
        If (rblExistingCampaign.SelectedValue = 0) Then
            labelExistingCampaigns.Text = "'No existing Campaigns'"
            MB.ShowMessage("By continuing, you confirm that there are no existing Campaigns applied ot the customer's account. ")
        ElseIf (rblExistingCampaign.SelectedValue = 1) Then
            labelExistingCampaigns.Text = "'Campaigns are not restored when seasonal ends. By continuing, you confirm that you have advised the customer of this, and told them what their new monthly rate will be when seasonal ends.' "
            MB.ShowMessage("Campaigns are not restored when seasonal ends. By continuing, you confirm that you have advised the customer of this, and told them what their new monthly rate will be when seasonal ends. ")
        ElseIf (rblExistingCampaign.SelectedValue = 2) Then
            labelExistingCampaigns.Text = "'If a Campaign was added as a result of a WOW! Save within the last 30 days, it will be reinstated at the end other the seasonal period.' "
            MB.ShowMessage("If a Campaign was added as a result of a WOW! Save within the last 30 days, it will be reinstated at the end of the the seasonal period. By continuing, you confirm that you have verified existing Campaigns were applied within the past 30 days.")
        End If
    End Sub

End Class