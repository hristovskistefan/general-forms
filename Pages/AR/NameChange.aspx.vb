Public Class ARNameChange
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
        _employee = GeneralFormsCommon.getEmployee()
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        lblhName.Text = _employee.FullNameFirstLast
        lblhDate.Text = Format(Date.Now, "Short Date")
        lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Private Sub rblNameCorrChange_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblNameCorrChange.SelectedIndexChanged
        pnlNameChange.Visible = False
        pnlNameCorr.Visible = False
        Select Case rblNameCorrChange.SelectedValue
            Case "Divorce"
                'Check if Ft. Gordon 30905/Harlem 30814/Grovetown 30813 (Division 58) account and if so, block submission of form
                If Me.hfAccountDivision.Value = "58" Then
                    Me.MB.ShowMessage("A Fort Gordon account was entered.<br />The form cannot be submitted for Fort Gordon accounts requesting a name change for Divorce.<br />Please advise customer to visit the local office to make changes to the name on the account.<br />See Gooroo for more information.")
                    Exit Sub
                End If
                pnlNameChange.Visible = True
                pnlFamilyRelation.Visible = False
                pnlNameChangeData.Visible = True
                lblCertificate.Text = "<br />Advise the customer to fax a copy of the divorce certificate with the WOW! Account Number to 1-888-268-5859.<br />"
                pnlLegal.Visible = False
            Case "Marriage"
                If Me.hfAccountDivision.Value = "58" Then
                    Me.MB.ShowMessage("A Fort Gordon account was entered.<br />The form cannot be submitted for Fort Gordon accounts requesting a name change for Marriage.<br />Please advise customer to visit the local office to make changes to the name on the account.<br />See Gooroo for more information.")
                    Exit Sub
                End If
                pnlNameChange.Visible = True
                pnlFamilyRelation.Visible = False
                pnlNameChangeData.Visible = True
                lblCertificate.Text = "<br />Advise the customer to fax a copy of the marriage certificate with the WOW! Account Number to 1-888-268-5859.<br />"
                pnlLegal.Visible = False
            Case "Death"
                If Me.hfAccountDivision.Value = "58" Then
                    Me.MB.ShowMessage("A Fort Gordon account was entered.<br />The form cannot be submitted for Fort Gordon accounts requesting a name change for Death.<br />Please advise customer to visit the local office to make changes to the name on the account.<br />See Gooroo for more information.")
                    Exit Sub
                End If
                pnlNameChange.Visible = True
                rblfamilyRelation.Visible = True
                pnlFamilyRelation.Visible = True
                rblfamilyRelation.SelectedIndex = -1
                pnlNameChangeData.Visible = False
                pnlLegal.Visible = False
            Case "Correction"
                pnlNameChange.Visible = False
                pnlNameCorr.Visible = True
                pnlLegal.Visible = False
            Case "Legal"
                If Me.hfAccountDivision.Value = "58" Then
                    Me.MB.ShowMessage("A Fort Gordon account was entered.<br />The form cannot be submitted for Fort Gordon accounts requesting a Legal Name Change.<br />Please advise customer to visit the local office to make changes to the name on the account.<br />See Gooroo for more information.")
                    Exit Sub
                End If
                pnlNameChange.Visible = False
                pnlLegal.Visible = True
                lblLegal.Text = "<br />Advise the customer to fax a copy of the court approved Petition to Change Name form showing the name change, with the WOW! Account number, to 1-888-268-5859.<br />"
        End Select
    End Sub

    Private Sub rblPhone_SelectedIndexChanged(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles rblfamilyRelation.SelectedIndexChanged
        If Me.rblfamilyRelation.SelectedValue = "No" Then
            pnlNameChangeData.Visible = False
            lblFamilyRelation.Visible = True
        Else
            pnlNameChangeData.Visible = True
            lblCertificate.Text = "<br />Advise the customer to fax a copy of the death certificate with the WOW! Account Number to 1-888-268-5859.<br />"
            lblFamilyRelation.Visible = False
        End If
    End Sub

    Public Sub GetForm()
        rfvAccount.Validate()
        revAccount.Validate()
        cvAccount.Validate()
        If Not (rfvAccount.IsValid AndAlso revAccount.IsValid AndAlso cvAccount.IsValid) Then
            pnlNameChangeMain.Visible = False
        Else
            txtcfname.Text = Replace(txtcfname.Text.Trim.ToUpper, " ", "")
            txtclname.Text = Replace(txtclname.Text.Trim.ToUpper, " ", "")
            txtstate.Text = GeneralFormsCommon.getStateFromDivision(CInt(lblDivision.Text))
            pnlerror.Visible = False
            txtAcct.ReadOnly = True
            pnlNameChangeMain.Visible = True
        End If
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnNameChangeSubmit.Click, btnNameCorrSubmit.Click
        'Removed dashes, spaces, and periods from phone number fields
        Dim pattern As String = "[- .]"
        Dim replacement As String = ""
        Dim rgx As New Regex(pattern)
        txtNewPhone.Text = rgx.Replace(txtNewPhone.Text, replacement)
        txtAltNum.Text = rgx.Replace(txtAltNum.Text, replacement)
        txtLegalPhoneNumber.Text = rgx.Replace(txtLegalPhoneNumber.Text, replacement)
        If Page.IsValid Then
            Try

                'GET DB STUFF PREPARED

                Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
                Dim cmd As DbCommand

                Select Case o.ID

                    Case "btnNameCorrSubmit"
                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Comments,IssueType,OName,NName,NewSSN,NewPhone,Division) VALUES " _
                           & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@State,@Kickback,@Comments,@IssueType,@OName,@NName,@NewSSN,@NewPhone,@Division)")
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "Name/Due Date Change")
                        db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                        db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstLast)
                        db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                        db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                        db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                        db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                        db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                        db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "PhoneNum", DbType.String, txtNewPhone.Text)
                        db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                        db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                        db.AddInParameter(cmd, "Comments", DbType.String, txtNameCorrComm.Text)
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Name Correction")
                        db.AddInParameter(cmd, "OName", DbType.String, txtCurrNameCorr.Text)
                        db.AddInParameter(cmd, "NName", DbType.String, txtNewNameCorr.Text)
                        db.AddInParameter(cmd, "NewSSN", DbType.String, "")
                        db.AddInParameter(cmd, "NewPhone", DbType.String, txtAltNum.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.ExecuteNonQuery(cmd)
                    Case "btnNameLegalSubmit"
                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Comments,IssueType,OName,NName,NewSSN,NewPhone,Division) VALUES " _
                           & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@State,@Kickback,@Comments,@IssueType,@OName,@NName,@NewSSN,@NewPhone,@Division)")
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "Name/Due Date Change")
                        db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                        db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstLast)
                        db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                        db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                        db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                        db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                        db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                        db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "PhoneNum", DbType.String, txtNewPhone.Text)
                        db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                        db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                        db.AddInParameter(cmd, "Comments", DbType.String, txtNameLegalComm.Text)
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Legal Name Change")
                        db.AddInParameter(cmd, "OName", DbType.String, txtCurrNameLegal.Text)
                        db.AddInParameter(cmd, "NName", DbType.String, txtNewNameLegal.Text)
                        db.AddInParameter(cmd, "NewSSN", DbType.String, "")
                        db.AddInParameter(cmd, "NewPhone", DbType.String, txtAltNum.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.ExecuteNonQuery(cmd)
                    Case "btnNameChangeSubmit"
                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Comments,IssueType,OName,NName,NewSSN,NewPhone,NameChangeReason,Division) VALUES " _
                            & "(@DateSub,@RequestType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@State,@Kickback,@Comments,@IssueType,@OName,@NName,@NewSSN,@NewPhone,@NameChangeReason,@Division)")
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "Name/Due Date Change")
                        db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                        db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstLast)
                        db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                        db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                        db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                        db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                        db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                        db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "PhoneNum", DbType.String, txtNewPhone.Text)
                        db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                        db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                        db.AddInParameter(cmd, "Comments", DbType.String, txtNameChangeComm.Text & Environment.NewLine & "Relationship: " & txtRelationship.Text & Environment.NewLine & "Alternate Phone Number: " & txtAltNum.Text)
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Name Change")
                        db.AddInParameter(cmd, "OName", DbType.String, txtCurrName.Text)
                        db.AddInParameter(cmd, "NName", DbType.String, txtNewName.Text)
                        db.AddInParameter(cmd, "NewSSN", DbType.String, txtNewSSN.Text)
                        db.AddInParameter(cmd, "NewPhone", DbType.String, txtAltNum.Text)
                        db.AddInParameter(cmd, "NameChangeReason", DbType.String, rblNameCorrChange.SelectedValue)
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
            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand

            cmd = db.GetSqlStringCommand("SELECT count(*) FROM Billing WHERE (RequestType = 'Name/Due Date Change' AND IssueType in ('Name Correction','Name Change') AND AcctNum = @AcctNum AND Kickback in (0,3))")
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
        'Check if Ft. Gordon 30905/Harlem 30814/Grovetown 30813 (Division 58) account and if so, block submission of form
        If _myCustomer.Address.Division = "58" Then
            Me.hfAccountDivision.Value = "58"
            Me.MB.ShowMessage("A Fort Gordon account was entered.<br />The Name Change form cannot be submitted for Fort Gordon accounts requesting a name change or correction.<br />Please advise customer to visit the local office to make changes to the name on the account.<br />See Gooroo for more information.")
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
        pnlNameChangeMain.Visible = False
    End Sub

    Private Sub ResetMain()
        pnlthx.Visible = False
        pnlmain.Visible = True
        pnlerror.Visible = False
        pnlNameChangeMain.Visible = False
    End Sub

    Protected Sub txtAcct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub
End Class
