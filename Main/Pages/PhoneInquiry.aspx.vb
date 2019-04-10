Imports System.IO
Imports System.Drawing
Imports System.ServiceModel

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
            revAccountChangeLdIntl.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            revAccountNumberPhoneInpError.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
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
        Me.pnlChangeLdIntl.Visible = False
        Me.pnlInpError.Visible = False
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
                Me.pnlChangeLdIntl.Visible = True
                'pnlChangeLdIntl is removed
            Case "1"
                makeAllInvisible()
                Me.pnlInpError.Visible = True
                'pnlInpError is removed
            Case "2"
                makeAllInvisible()
                Me.pnl3pv.Visible = True
                'pnl3pv is removed
            Case "3"
                makeAllInvisible()
                Me.pnlVMCheck1.Visible = True
            Case "4"
                makeAllInvisible()
                Me.pnlIncAddress.Visible = True
                'pnlIncAddress is removed
        End Select
    End Sub


    Public Sub SendChangeLdIntl(ByVal o As Object, ByVal e As EventArgs) Handles btnChangeLdIntl.Click
        Try

            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)
            mailMsg.To.Add("ccctelespec@wideopenwest.com")
            'mailMsg.To.Add("c_rhoades@wideopenwest.com")
            mailMsg.Subject = "WOW! Phone Inquiry | " & Me.rblType.SelectedItem.Text & " | Submitted by: " & Me.lblhName.Text
            mailMsg.Body = "WOW! Phone Inquiry:" & vbCrLf & _
                "" & Me.rblType.SelectedItem.Text & vbCrLf & vbCrLf & _
                "     Date:            " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:              " & Me.lblhName.Text & vbCrLf & _
                "     ICOMS ID:    " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Account #:   " & Me.txtAccountChangeLdIntl.Text & vbCrLf & _
                "     Customer:     " & Me.txtCustNameChangeLdIntl.Text & vbCrLf & _
                "     City:               " & Me.txtCityChangeLdIntl.Text & vbCrLf & _
                "     State:            " & Me.txtStateChangeLdIntl.Text.Trim & vbCrLf & _
                "     Zip:                " & Me.txtZipChangeLdIntl.Text & vbCrLf & _
                "     Phone #:      " & Me.txtPhoneChangeLdIntl.Text & vbCrLf & vbCrLf & _
                "Question/Issue: " & vbCrLf & _
                "" & Me.txtCommentsChangeLdIntl.Text
            EmailProxy.Send(mailMsg)

            ResetPage()
            Me.pnlThanks.Visible = True

        Catch ex As Exception
            Response.Write("<b>An error has occurred and your request cannot be processed.</b><br />")
            Response.Write(ex.Message)
        End Try

    End Sub


    Public Sub SendInpError(ByVal o As Object, ByVal e As EventArgs) Handles btnInpError.Click
        Try

            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)
            'mailMsg.To.Add("c_rhoades@wideopenwest.com")
            mailMsg.To.Add("Systems_Support@wideopenwest.com")
            mailMsg.To.Add(_employee.SupEmail)
            mailMsg.Subject = "WOW! Phone Inquiry | " & Me.rblType.SelectedItem.Text & " | Submitted by: " & Me.lblhName.Text
            mailMsg.Body = "WOW! Phone Inquiry:" & vbCrLf & _
                            "" & Me.rblType.SelectedItem.Text & vbCrLf & vbCrLf & _
                            "     Date Submitted:        " & Me.lblhDate.Text & vbCrLf & _
                            "     Submitted By:             " & Me.lblhName.Text & vbCrLf & _
                            "     ICOMS ID:                     " & Me.lblhIcomsID.Text & vbCrLf & _
                            "     Supervisor:                   " & _employee.SupNameFirstLast & vbCrLf & _
                            "     Supervisor E-Mail:     " & _employee.SupEmail & vbCrLf & _
                            "     Account #:                   " & Me.txtAccountNumberPhoneInpError.Text & vbCrLf & _
                            "     Phone #:                      " & Me.txtPhoneINP.Text & vbCrLf & vbCrLf & _
                            "Comments:" & vbCrLf & _
                            "" & Me.txtComments.Text
            'EmailProxy.Send(mailMsg)

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
            mailMsg.Subject = "WOW! Phone Inquiry | " & Me.rblType.SelectedItem.Text & " | Submitted by: " & Me.lblhName.Text
            mailMsg.Body = "WOW! Phone Inquiry:" & vbCrLf & _
                "" & Me.rblType.SelectedItem.Text & vbCrLf & vbCrLf & _
                "     Date:           " & Me.lblhDate.Text & vbCrLf & _
                "     CCR:            " & Me.lblhName.Text & vbCrLf & _
                "     Icoms ID:       " & Me.lblhIcomsID.Text & vbCrLf & _
                "     Supervisor:     " & _employee.SupNameFirstLast & vbCrLf & _
                "     E-Mail:         " & _employee.Email & vbCrLf & _
                "     SSN:            " & Me.txtssn.Text
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

    Private Sub btnIncAddySubmit_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnIncAddySubmit.Click
        Try
            '  Dim MailClient As New SmtpClient
            Dim mailMsg As MailMessage = New MailMessage()
            mailMsg.IsBodyHtml = False
            mailMsg.From = New MailAddress(_employee.Email)

            mailMsg.Subject = "WOW! Phone Inquiry | " & Me.rblType.SelectedItem.Text & " | Submitted by: " & Me.lblhName.Text
            mailMsg.To.Add("ccctelespec@wideopenwest.com")
            mailMsg.To.Add("DispatchLeads@wideopenwest.com")
            mailMsg.To.Add("dispatchsups@wideopenwest.com")
            mailMsg.To.Add("teleopslead@wideopenwest.com")
            mailMsg.To.Add("TelephonyOperationsManagement@wideopenwest.com")

            mailMsg.Body = "WOW! Phone Inquiry:" & vbCrLf & _
                    "" & Me.rblType.SelectedItem.Text & vbCrLf & vbCrLf & _
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

    'Protected Sub rad3Pviss_SelectedIndexChanged(sender As Object, e As EventArgs) Handles rad3pviss.SelectedIndexChanged
    '    Select Case rad3pviss.SelectedValue
    '        Case "3PV Login", "CSR & 3PV Login"
    '            pnl3PVlink.Visible = True
    '        Case Else
    '            pnl3PVlink.Visible = False
    '    End Select
    'End Sub


    ''' ''''''''''''''''''''''''''''
    ''' Change Long Distance/International Provider Back to WOW!
    Protected Sub txtChangeLdIntl_TextChanged(sender As Object, e As EventArgs) Handles txtAccountChangeLdIntl.TextChanged
        ibChangeLdIntl_Click(Nothing, New ImageClickEventArgs(0, 0))
    End Sub
    Protected Sub ibChangeLdIntl_Click(sender As Object, e As ImageClickEventArgs) Handles ibChangeLdIntl.Click
        Dim tempAccount As String = txtAccountChangeLdIntl.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revAccountChangeLdIntl.Validate()
        Me.rfvAccountChangeLdIntl.Validate()
        If Not (Me.revAccountChangeLdIntl.IsValid And Me.rfvAccountChangeLdIntl.IsValid) Then
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
                txtAccountChangeLdIntl.Text = ""
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

        txtPhoneChangeLdIntl.Text = _customer.PrimaryPhone.FormattedPhone
        txtCustNameChangeLdIntl.Text = _customer.FullNameFirstLast
        txtCityChangeLdIntl.Text = _customer.Address.City
        txtStateChangeLdIntl.Text = _customer.Address.State
        txtZipChangeLdIntl.Text = _customer.Address.Zip

    End Sub




    ''' '''''''''''''''''''''''''
    ''' Phone INP Error
    Protected Sub txtAccountNumberPhoneInpError_TextChanged(sender As Object, e As EventArgs) Handles txtAccountNumberPhoneInpError.TextChanged
        ibGo3_Click(Nothing, New ImageClickEventArgs(0, 0))
    End Sub

    Protected Sub ibGo3_Click(sender As Object, e As ImageClickEventArgs) Handles ibGo3.Click
        Dim tempAccount As String = txtAccountNumberPhoneInpError.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revAccountNumberPhoneInpError.Validate()
        Me.rfvAccountNumberPhoneInpError.Validate()
        If Not (Me.revAccountNumberPhoneInpError.IsValid And Me.rfvAccountNumberPhoneInpError.IsValid) Then
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
                txtAccountNumberPhoneInpError.Text = ""
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
        txtAccountChangeLdIntl.ReadOnly = True
        txtAccountNumberPhoneInpError.ReadOnly = True
        txtAcctNum2.ReadOnly = True

    End Sub


    ''' '''''''''''''''''''''''''''''''''''
    ''' Scheduled/Installed at Incorrect Address Panel
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
                txtAcctNum2.Text = ""
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