Public Class ARReplacementStatement
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
            FillMonthDD(dropmonth)
        End If
    End Sub

    Private Sub LoadEmployeeInfo()
        ' load employee
        _employee = GeneralFormsCommon.getEmployee()
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        lblhName.Text = _employee.FullNameFirstLast
        lblhDate.Text = Format(Date.Now, "Short Date")
        lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Protected Sub FillMonthDD(ByVal ddl As DropDownList)
        Dim i As Integer
        Dim month As Integer

        For i = 0 To 1
            month = (Date.Today.AddMonths(-i).Month)

            ddl.Items.Add(MonthName(month).ToString)

        Next
        ddl.Items.Add("Last 3 Months")
        ddl.Items.Add("Last 6 Months")
        If ddl.ID = "dropincbillmonth" Then
            ddl.Items.Add("Phone Scam")
        End If
    End Sub

    Protected Sub rblDelivery_OnSelectedIndexChanged(ByVal o As Object, ByVal e As EventArgs) Handles rblDelivery.SelectedIndexChanged
        If rblDelivery.SelectedValue = "Fax Number" Then
            pnlFax.Visible = True
            pnlEmail.Visible = False
        ElseIf rblDelivery.SelectedValue = "Email" Then
            pnlEmail.Visible = True
            pnlFax.Visible = False
        Else
            pnlEmail.Visible = False
            pnlFax.Visible = False
        End If
    End Sub


    Public Sub GetForm()
        rfvAccount.Validate()
        revAccount.Validate()
        cvAccount.Validate()
        If Not (rfvAccount.IsValid AndAlso revAccount.IsValid AndAlso cvAccount.IsValid) Then
            pnlrepl.Visible = False
        Else
            txtcfname.Text = Replace(txtcfname.Text.Trim.ToUpper, " ", "")
            txtclname.Text = Replace(txtclname.Text.Trim.ToUpper, " ", "")
            txtstate.Text = GeneralFormsCommon.getStateFromDivision(CInt(lblDivision.Text))
            txtAcct.ReadOnly = True
            pnlrepl.Visible = True
        End If
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnreplsend.Click
        If Page.IsValid Then
            Try
                'GET DB STUFF PREPARED

                Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
                Dim cmd As DbCommand

                Select Case o.ID
                    Case "btnreplsend"
                        '***************************************
                        ' REPLACEMENT/ITEMIZED STATEMENT
                        '***************************************

                        Dim strDelivery As String = String.Empty

                        Select Case rblDelivery.SelectedValue
                            Case "Paper Copy"
                                strDelivery = "Paper Copy"
                            Case "Fax Number"
                                strDelivery = "Fax Number: " & txtFaxNum.Text
                            Case "Email"
                                strDelivery = "Email Address: " & txtEmailAddy.Text
                            Case "CD"
                                strDelivery = "CD"
                        End Select

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,State,Kickback,IssueType,TimeFrame,Comments,Delivery,Division) VALUES " _
                                                     & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@State,@Kickback,@IssueType,@TimeFrame,@Comments,@Delivery,@Division)")

                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "Replacement/Itemized Letter")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Replacement Letter")
                        db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                        db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstlast)
                        db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                        db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                        db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                        db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                        db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                        db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                        db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                        db.AddInParameter(cmd, "TimeFrame", DbType.String, dropmonth.SelectedItem.Value)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtreplcomm.Text)
                        db.AddInParameter(cmd, "Delivery", DbType.String, strDelivery)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        Try
                            db.ExecuteNonQuery(cmd)
                        Catch ex As Exception
                            Throw
                        End Try

                    Case "btnItemLetter"

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,State,Kickback,IssueType,TimeFrame,Comments,Division) VALUES " _
                                & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@State,@Kickback,@IssueType,@TimeFrame,@Comments,@Division)")

                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "Replacement/Itemized Letter")
                        db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                        db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstLast)
                        db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                        db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                        db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                        db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                        db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                        db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                        db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Breakdown Letter")
                        db.AddInParameter(cmd, "TimeFrame", DbType.String, "Current Month")
                        db.AddInParameter(cmd, "Comments", DbType.String, txtreplcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.ExecuteNonQuery(cmd)


                End Select
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
            Dim requestType As String = "Replacement/Itemized Letter"
            Dim issueType As String = "Replacement Letter"

            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand

            cmd = db.GetSqlStringCommand("SELECT count(*) FROM Billing WHERE (RequestType = @RequestType AND IssueType = @IssueType AND AcctNum = @AcctNum AND Kickback in (0,3))")
            db.AddInParameter(cmd, "RequestType", DbType.String, requestType)
            db.AddInParameter(cmd, "IssueType", DbType.String, issueType)
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
        txtclname.Text = _myCustomer.LName
        txtcfname.Text = _myCustomer.FName
        lblDivision.Text = _myCustomer.Address.Division
        GetForm()

    End Sub

    Sub Reset()
        pnlthx.Visible = True
        pnlmain.Visible = False
        pnlerror.Visible = False
        pnlrepl.Visible = False
    End Sub

    Private Sub ResetMain()
        pnlthx.Visible = False
        pnlmain.Visible = True
        pnlerror.Visible = False
        pnlrepl.Visible = False
    End Sub

    Protected Sub txtcacct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub
End Class
