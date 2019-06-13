Public Class ARCustomerRefunds
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust
    Private _myCustomer As CustomerService.Cust
    'Private _division As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            ResetMain()
        End If
    End Sub

    Private Sub LoadEmployeeInfo()
        ' load employee
        _employee = GeneralFormsCommon.getEmployee
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        lblhName.Text = _employee.FullNameFirstLast
        lblhDate.Text = Format(Date.Now, "Short Date")
        lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Public Sub GetForm()
        rfvAccount.Validate()
        revAccount.Validate()
        cvAccount.Validate()
        If Not (rfvAccount.IsValid AndAlso revAccount.IsValid AndAlso cvAccount.IsValid) Then
            pnlrefund.Visible = False
        Else
            txtcfname.Text = Replace(txtcfname.Text.Trim.ToUpper, " ", "")
            txtclname.Text = Replace(txtclname.Text.Trim.ToUpper, " ", "")
            txtstate.Text = GeneralFormsCommon.getStateFromDivision(CInt(lblDivision.Text))
            pnlerror.Visible = False
            txtAcct.ReadOnly = True
            pnlrefund.Visible = True
        End If
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnrefsend.Click
        'Removed dashes, spaces, and periods from phone number fields
        Dim pattern As String = "[- .]"
        Dim replacement As String = ""
        Dim rgx As New Regex(pattern)
        txtPhoneNumber.Text = rgx.Replace(txtPhoneNumber.Text, replacement)

        If Page.IsValid Then
            Try

                'GET DB STUFF PREPARED

                Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
                Dim cmd As DbCommand

                '***************************************
                ' CUSTOMER REFUND
                '***************************************
                cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Amount,RefundReason,Comments,Division,AlternativePhoneNum,Email) VALUES " _
                 & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@Amount,@RefundReason,@Comments,@Division,@AlternativePhoneNum,@Email)")
                Database.ClearParameterCache()
                db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                db.AddInParameter(cmd, "RequestType", DbType.String, "Customer Refund")
                db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstLast)
                db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                db.AddInParameter(cmd, "PhoneNum", DbType.String, txtPhoneNumber.Text)
                db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                db.AddInParameter(cmd, "Amount", DbType.String, rntRefundAmount.Value)
                db.AddInParameter(cmd, "RefundReason", DbType.String, txtrefreason.Text)
                db.AddInParameter(cmd, "Comments", DbType.String, txtrefcomm.Text)
                db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhone.Text)
                db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                db.ExecuteNonQuery(cmd)
                Reset()
            Catch mailex As Exception
                lblerror.Visible = True
                lblerror.Text = "<br /><b>Error Message:</b> " & mailex.Message & "<br />" & _
                            "<b>Error Source:</b> " & mailex.Source & "<br />" & _
                            "<b>Stack Trace:</b> " & mailex.StackTrace & "<br />"
            End Try
        End If
    End Sub

    Public Sub ThankYou(ByVal o As Object, ByVal e As EventArgs) Handles btnthx.Click
        Response.Redirect(Request.Url.ToString)
    End Sub

    Protected Sub cvAccount_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvAccount.ServerValidate
        revAccount.Validate()
        rfvAccount.Validate()
        If Not (revAccount.IsValid And rfvAccount.IsValid) Then
            args.IsValid = True
        Else

            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand

            cmd = db.GetSqlStringCommand("SELECT count(*) FROM Billing WHERE (RequestType = @RequestType AND AcctNum = @AcctNum AND Kickback in (0,3))")
            db.AddInParameter(cmd, "RequestType", DbType.String, "Customer Refund")
            db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)

            If CBool(db.ExecuteScalar(cmd)) Then
                args.IsValid = False
                ResetMain()
            Else
                args.IsValid = True
            End If
        End If

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
            ResetMain()
            txtclname.Text = String.Empty
            txtcfname.Text = String.Empty
            Exit Sub
        End If

        Dim customerDetails = GeneralFormsCommon.getCustomerDetails(tempAccount)
        txtEmail.Text = customerDetails.Account.ContactEmail
        txtPhoneNumber.Text = customerDetails.Account.DaytimePhoneNumber
        txtAltPhone.Text = customerDetails.Account.EveningPhoneNumber

        txtclname.Text = _myCustomer.LName
        txtcfname.Text = _myCustomer.FName
        lblDivision.Text = _myCustomer.Address.Division
        GetForm()

    End Sub

    Sub Reset()
        pnlthx.Visible = True
        pnlmain.Visible = False
        pnlerror.Visible = False
        pnlrefund.Visible = False
    End Sub

    Private Sub ResetMain()
        pnlthx.Visible = False
        pnlmain.Visible = True
        pnlerror.Visible = False
        pnlrefund.Visible = False
    End Sub

    Protected Sub txtcacct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub
End Class
