'Imports System.Data.SqlClient
Imports System.IO
Imports System.Drawing
Imports System.ServiceModel

'Imports WOW.Data.DataAccess
'Imports System.Net

Partial Public Class PhoneInquiry
    Inherits System.Web.UI.Page
    Private _sql As String
    Private _ostream As StreamWriter
    Private _suplist As ArrayList
    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmployeeInfo()

        If Not Page.IsPostBack Then
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            revAccount2.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
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
        Me.makeAllInvisible()
        ResetControls(Me)
    End Sub

    Public Sub ResetControls(ByVal myControl As Control)
        For Each ctrl As Control In myControl.Controls
            If ctrl.HasControls Then
                ResetControls(ctrl)
            End If
            If TypeOf (ctrl) Is TextBox Then
                If ctrl.ID <> "txtdate" AndAlso ctrl.ID <> "txtname" AndAlso ctrl.ID <> "txtIcomsID" Then
                    CType(ctrl, TextBox).Text = String.Empty
                End If
            End If
            If TypeOf (ctrl) Is RadioButton Then
                CType(ctrl, RadioButton).Checked = False
            End If
            If TypeOf (ctrl) Is RadioButtonList Then
                CType(ctrl, RadioButtonList).ClearSelection()
            End If
        Next

    End Sub

    Private Sub makeAllInvisible()
        Me.pnl3pv.Visible = False
        Me.pnlccr.Visible = False
        Me.pnlGeneralInquiry.Visible = False
        Me.pnlInpError.Visible = False
        Me.pnlfraud.Visible = False
        Me.pnlIncAddress.Visible = False
        Me.pnlvm.Visible = False
        Me.pnlVMCheck1.Visible = False
        Me.pnlVMCheck2.Visible = False
        Me.pnlwrong.Visible = False
        Me.pnlThanks.Visible = False
    End Sub

    Public Sub GetPanel(ByVal o As Object, ByVal e As EventArgs) Handles rblType.SelectedIndexChanged
        Me.rbVMCheck1.Checked = False
        Me.rbVMCheck2.Checked = False
        Me.rbVMCheck3.Checked = False
        Me.rbVMCheck4.Checked = False
        Select Case Me.rblType.SelectedItem.Value
            Case "0"
                makeAllInvisible()
                Me.pnlGeneralInquiry.Visible = True
            Case "1"
                makeAllInvisible()
                Me.pnlInpError.Visible = True
            Case "2"
                makeAllInvisible()
                Me.pnl3pv.Visible = True
            Case "3"
                makeAllInvisible()
                Me.pnlVMCheck1.Visible = True
            Case "4"
                makeAllInvisible()
                Me.pnlIncAddress.Visible = True
        End Select
    End Sub


    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnGeneralInquiry.Click
        Try

            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)
            mailMsg.To.Add("ccctelespec@wideopenwest.com")
            mailMsg.Subject = "WOW! Phone Inquiry from: " & Me.lblhName.Text & " - Type = " & Me.rblType.SelectedItem.Text
            mailMsg.Body = "WOW! Phone Inquiry Submission" & vbCrLf & vbCrLf & _
                "     Date:           " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:            " & Me.lblhName.Text & vbCrLf & _
                "     ICOMS ID:    " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Inquiry Type:   " & Me.rblType.SelectedItem.Value & vbCrLf & _
                "     Customer:       " & Me.txtcustname.Text & vbCrLf & _
                "     City:           " & Me.txtcity.Text & vbCrLf & _
                "     State:          " & Me.txtState.Text.Trim & vbCrLf & _
                "     Zip:            " & Me.txtzip.Text & vbCrLf & _
                "     Phone #:        " & Me.txtphone.Text & vbCrLf & _
                "     Question/Issue: " & vbCrLf & _
                "     " & Me.txtquest.Text
            EmailProxy.Send(mailMsg)

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed.<br />")
            Response.Write(ex.Message)
        End Try

    End Sub

    Public Sub SendInpError(ByVal o As Object, ByVal e As EventArgs) Handles btnInpError.Click
        Try

            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)
            mailMsg.To.Add("Systems_Support@wideopenwest.com")
            mailMsg.To.Add(_employee.SupEmail)
            mailMsg.Subject = "Phone INP Error | Submitted by: " & Me.lblhName.Text & ""
            mailMsg.Body = "Phone INP Error" & vbCrLf & vbCrLf & _
                            "     Date Submitted:        " & Me.lblhDate.Text & vbCrLf & _
                            "     Submitted By:             " & Me.lblhName.Text & vbCrLf & _
                            "     ICOMS ID:                     " & Me.lblhIcomsID.Text & vbCrLf & _
                            "     Supervisor:                   " & _employee.SupNameFirstLast & vbCrLf & _
                            "     Supervisor E-Mail:     " & _employee.SupEmail & vbCrLf & _
                            "     Account #:                   " & Me.txtAcctNum.Text & vbCrLf & _
                            "     Phone #:                      " & Me.txtPhoneINP.Text & vbCrLf & _
                            "     Comments:                   " & Me.txtComments.Text
            EmailProxy.Send(mailMsg)

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed.<br />")
            Response.Write(ex.Message)
        End Try

    End Sub


    Public Sub Send3PV(ByVal o As Object, ByVal e As EventArgs) Handles btn3pv.Click
        Try
            ' Dim MailClient As SmtpClient = New SmtpClient
            Dim mailMsg As New MailMessage
            mailMsg.From = New MailAddress(_employee.Email)
            mailMsg.To.Clear()
            mailMsg.To.Add(New MailAddress("wow_resourcemanage@wideopenwest.com"))
            mailMsg.IsBodyHtml = False
            mailMsg.Subject = "WOW! Phone Inquiry from: " & Me.lblhName.Text & " - Type = " & Me.rblType.SelectedItem.Text
            mailMsg.Body = "WOW! Phone Inquiry Submission" & vbCrLf & vbCrLf & _
                "     Date:           " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:            " & Me.lblhName.Text & vbCrLf & _
                "     Icoms ID:       " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Supervisor:     " & _employee.SupNameFirstLast & vbCrLf & _
                "     E-Mail:         " & _employee.Email & vbCrLf & _
                "     SSN:            " & Me.txtssn.Text & vbCrLf & _
                "     Issue Type:     " & Me.rad3pviss.SelectedItem.Value
            EmailProxy.Send(mailMsg)

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed.<br />")
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub sendCcrError(ByVal o As Object, ByVal e As EventArgs) Handles btnccr.Click
        Try
            ' Dim MailClient As New SmtpClient
            Dim mailMsg As New MailMessage
            mailMsg.From = New MailAddress(_employee.Email)
            mailMsg.To.Clear()
            mailMsg.To.Add(New MailAddress("ccctelespec@wideopenwest.com"))
            mailMsg.IsBodyHtml = False
            mailMsg.Subject = "WOW! Phone Inquiry from: " & Me.lblhName.Text & " - Type = " & Me.rblType.SelectedItem.Text
            mailMsg.Body = "WOW! Phone Inquiry Submission" & vbCrLf & vbCrLf & _
                "     Date:           " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:            " & Me.lblhName.Text & vbCrLf & _
                "     Icoms ID:       " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Supervisor:     " & _employee.SupNameFirstLast & vbCrLf & _
                "     E-Mail:         " & _employee.Email & vbCrLf & _
                "     Phone #:        " & Me.txtccrphone.Text & vbCrLf & _
                "     State:          " & Me.dropccrstate.SelectedItem.Value & vbCrLf & _
                "     LEC #:          " & Me.txtlec.Text & vbCrLf & _
                "     CCR Error:      " & vbCrLf & "     " & Me.txtccriss.Text
            EmailProxy.Send(mailMsg)

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed.<br />")
            Response.Write(ex.Message)
        End Try
    End Sub

    Public Sub SendVM(ByVal o As Object, ByVal e As EventArgs) Handles btnvm.Click
        Try
            If Me.txtvm.Text.Length = 0 Then
                Me.lblerr.Text = "<marquee>Please enter a telephone number.</marquee>"
                Me.lblerr.Visible = True
                Me.txtvm.BackColor = Color.Yellow
                Exit Sub
            Else
                Me.lblerr.Text = ""
                Me.lblerr.Visible = False
                Me.txtvm.BackColor = Color.White
            End If

            Dim db As Database = DatabaseFactory.CreateDatabase("TaskManager")
            Dim cmd As DbCommand = db.GetStoredProcCommand("spc_create_reset_voicemail_password_request")
            db.AddInParameter(cmd, "telephone_number", DbType.String, Me.txtvm.Text.Trim)
            db.AddInParameter(cmd, "account_number", DbType.String, Me.txtvmacct.Text.Trim)
            db.ExecuteNonQuery(cmd)
            If rbVMCheck4.Checked Then
                db.SetParameterValue(cmd, "telephone_number", Me.txtvm2.Text.Trim)
                db.ExecuteNonQuery(cmd)
            End If

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed. Please try again.</b><br />")
            Response.Write(ex.Message)
            Response.Write(ex.StackTrace)
        End Try
    End Sub

    Public Sub SendFraudReport(ByVal o As Object, ByVal e As EventArgs) Handles btnfraud.Click
        Try
            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.From = New MailAddress("ccctelespec@wideopenwest.com")
            mailMsg.IsBodyHtml = False
            mailMsg.Subject = "WOW! Phone Inquiry from: " & Me.lblhName.Text & " - Type = " & Me.rblType.SelectedItem.Text
            mailMsg.To.Add("ccctelespec@wideopenwest.com")
            mailMsg.Body = "WOW! Phone Inquiry Submission" & vbCrLf & vbCrLf & _
                "     Date:           " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:            " & Me.lblhName.Text & vbCrLf & _
                "     Icoms ID:       " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Account #:      " & Me.txtfraudacct.Text & vbCrLf & _
                "     Phone #:        " & Me.txtfraudphone.Text & vbCrLf & _
                "     Comments:       " & Me.txtfraudcomm.Text
            EmailProxy.Send(mailMsg)
            ResetPage()
            Me.pnlThanks.Visible = True
        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed. Please try again.<br />")
            Response.Write(ex.Message)
            Response.Write(ex.StackTrace)
        End Try
    End Sub

    Public Sub getLec(ByVal o As Object, ByVal e As EventArgs) Handles dropccrstate.SelectedIndexChanged
        Select Case Me.dropccrstate.SelectedItem.Value
            Case "IL"
                Me.txtlec.Text = "9239"
            Case "IN"
                Me.txtlec.Text = "9325"
            Case "MI"
                Me.txtlec.Text = "9323"
            Case "OH"
                Me.txtlec.Text = "9321"
            Case Else
                Me.txtlec.Text = ""
        End Select
    End Sub

    Sub CreateHeaders()
        Dim i As Integer
        Try
            Me.lstexport.Items.Add("Date,Submitted By,ICOMS ID,Inquiry Type,Customer,City,State,Zip,Phone," & _
                                      "Question/Issue,Date/Time")
            For i = 0 To Me.lstexport.Items.Count - 1
                Me._ostream.WriteLine(Me.lstexport.Items.Item(i))
            Next
            Me.lstexport.Items.Clear()
        Catch ex As Exception
            Response.Write("<b>An error has occured in creating files.</b><Br>")
            Response.Write("<b>Column headers could not be written.</b><Br>")
            Response.Write("<b>Possible Cause: Insufficient permissions</b>.<Br>")
            Response.Write("<b>If you feel this is incorrect, please contact.</b><Br>")
            Response.Write("<b>Dean Castro at 719=388-1194.</b><Br>")
        End Try
    End Sub

    Private Sub btnIncAddySubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncAddySubmit.Click
        Try
            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)

            mailMsg.Subject = "WOW! Phone Inquiry from: " & Me.lblhName.Text & " - Type = " & Me.rblType.SelectedItem.Text
            mailMsg.To.Add("ccctelespec@wideopenwest.com")
            mailMsg.To.Add("DispatchLeads@wideopenwest.com")
            mailMsg.To.Add("dispatchsups@wideopenwest.com")
            mailMsg.To.Add("teleopslead@wideopenwest.com")
            mailMsg.To.Add("TelephonyOperationsManagement@wideopenwest.com")

            mailMsg.Body = "WOW! Phone Inquiry Submission" & vbCrLf & vbCrLf & _
                    "     Date:                    " & Me.lblhDate.Text & vbCrLf & _
                    "     CCR:                     " & Me.lblhName.Text & vbCrLf & _
                    "     Icoms ID:                " & Me.lblhIcomsID.Text & vbCrLf & _
                    "     Inquiry Type:            " & Me.rblType.SelectedItem.Value & vbCrLf & _
                    "     Customer:                " & Me.txtCustName2.Text & vbCrLf & _
                    "     Account Number:          " & Me.txtAcctNum2.Text & vbCrLf & _
                    "     City:                    " & Me.txtCity2.Text & vbCrLf & _
                    "     State:                   " & Me.txtState2.Text & vbCrLf & _
                    "     Zip:                     " & Me.txtZip2.Text & vbCrLf & _
                    "     Phone #:                 " & Me.txtPhone2.Text & vbCrLf & _
                    "     Alt. Phone #:            " & Me.txtAltPhone.Text & vbCrLf & _
                    "     Correct Address:         " & Me.txtCorrAddress.Text & vbCrLf & _
                    "     Cust Already Installed?: " & Me.ddlCustInst.SelectedValue & vbCrLf & _
                    "     Alt. Date and Time:      " & Me.txtDateTime.Text & vbCrLf
            EmailProxy.Send(mailMsg)
            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occured and your request cannot be processsed.<br />")
            Response.Write(ex.Message)
        End Try
    End Sub

    Protected Sub rbVMCheck_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbVMCheck1.CheckedChanged, rbVMCheck2.CheckedChanged
        If CType(sender, RadioButton).Text = "Yes" Then
            makeAllInvisible()
            pnlVMCheck1.Visible = True
            pnlVMCheck2.Visible = True
            Me.lblNoVM.Text = String.Empty
        Else
            makeAllInvisible()
            pnlVMCheck1.Visible = True
            Me.lblNoVM.Text = "Cannot reset Voicemail password if customer does not have Voicemail service."
            Me.lblNoVM.ForeColor = Color.Red
        End If
    End Sub

    Protected Sub rbVMCheck2_CheckedChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rbVMCheck3.CheckedChanged, rbVMCheck4.CheckedChanged
        If CType(sender, RadioButton).Text = "1" Then
            pnlvm.Visible = True
            pnlTel2.Visible = False
        Else
            pnlvm.Visible = True
            pnlTel2.Visible = True
        End If
    End Sub

    Protected Sub txtcacct2_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcctNum2.TextChanged
        ibGo2_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub

    Protected Sub rad3Pviss_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rad3pviss.SelectedIndexChanged
        Select Case rad3pviss.SelectedValue
            Case "3PV Login", "CSR & 3PV Login"
                pnl3PVlink.Visible = True
            Case Else
                pnl3PVlink.Visible = False
        End Select
    End Sub

    Protected Sub txtAcctNum_TextChanged(sender As Object, e As EventArgs) Handles txtAcctNum.TextChanged
        ibGo_Click(Nothing, New ImageClickEventArgs(0, 0))
    End Sub

    Protected Sub ibGo_Click(sender As Object, e As ImageClickEventArgs) Handles ibGo.Click
        Dim tempAccount As String = txtAcctNum.Text.Trim
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
                _customer = customerClient.getByCustomerID(tempAccount)
            End Using
        Catch ex As Exception
            Dim errorAccount = ex.Message
            If errorAccount = "Customer Database Object is nothing." Then
                Me.MB.ShowMessage("Account number does not exist. Please try again.")
                txtAcctNum.Text = ""
                Exit Sub
            End If
        End Try


        If _customer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(_customer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            Exit Sub
        End If

    End Sub

    Protected Sub txtAcctNum2_TextChanged(sender As Object, e As EventArgs) Handles txtAcctNum2.TextChanged
        ibGo2_Click(Nothing, New ImageClickEventArgs(0, 0))
    End Sub

    Protected Sub ibGo2_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibGo2.Click
        Dim tempAccount As String = txtAcctNum2.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revAccount2.Validate()
        Me.rfvAccount2.Validate()
        If Not (Me.revAccount2.IsValid And Me.rfvAccount2.IsValid) Then
            Me.MB.ShowMessage("Invalid account number format or account number missing.")
            Exit Sub
        End If

        Try
            Using customerClient As New CustomerService.CustomerManagementClient
                _customer = customerClient.getByCustomerID(tempAccount)
            End Using
        Catch ex As Exception
            Dim errorAccount = ex.Message
            If errorAccount = "Customer Database Object is nothing." Then
                Me.MB.ShowMessage("Account number does not exist. Please try again.")
                txtAcctNum.Text = ""
                Exit Sub
            End If
        End Try

        If _customer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(_customer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            Exit Sub
        End If
        txtPhone2.Text = _customer.PrimaryPhone.FormattedPhone
        txtAltPhone.Text = _customer.SecondaryPhone.FormattedPhone
        txtCustName2.Text = _customer.FullNameFirstLast
        txtCity2.Text = _customer.Address.City
        txtState2.Text = _customer.Address.State
        txtZip2.Text = _customer.Address.Zip
    End Sub
End Class