Imports System.Text.RegularExpressions

Public Class ARManualPayments
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust
    Private _myCustomer As CustomerService.Cust
    'Private _division As Integer

    Private Function IsLocal() As Boolean
        If Request.ServerVariables("HTTP_REFERER") Is Nothing Then
            Return True
        End If
        Dim application() As String = Request.ApplicationPath.Split("/"c)
        Dim appName As String = application(application.Length - 1).ToLower
        Dim referer() As String = Request.ServerVariables("HTTP_REFERER").ToLower.Split("/"c)
        Dim current() As String = Request.Url.AbsoluteUri.ToLower.Split("/"c)
        Dim count As Integer = 0
        Do
            If Not current(count).Equals(referer(count)) Then
                Return False
            End If
            count += 1
        Loop While (Not referer(count - 1).Equals(appName))
        Return True
    End Function

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
            pnlmanual.Visible = False
            pnlAltPhone.Visible = False
        Else
            txtcfname.Text = Replace(txtcfname.Text.Trim.ToUpper, " ", "")
            txtclname.Text = Replace(txtclname.Text.Trim.ToUpper, " ", "")
            txtstate.Text = GeneralFormsCommon.getStateFromDivision(CInt(lblDivision.Text))
            pnlmanual.Visible = True
            pnlAltPhone.Visible = True
        End If
    End Sub


    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnmanpay.Click
        If Page.IsValid Then
            Try

                'GET DB STUFF PREPARED

                Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
                Dim cmd As DbCommand

                cmd = db.GetSqlStringCommand("INSERT INTO BILLING (DateSub,RequestType,Reason,Username,CCRName,SalesID,CSGOpCode,Supervisor,CFName,CLName,State,AcctNum,PhoneNum,Amount,CardType,CardNum,ExpireDate,Kickback, EFTType, EFTAcctNum, RoutingNum, Division, IssueType)" _
                      & "  VALUES (@DateSub,@RequestType,@Reason,@Username,@CCRName,@SalesID,@CSGOpCode,@Supervisor,@CFName,@CLName,@State,@AcctNum,@PhoneNum,@Amount,@CardType,@CardNum,@ExpireDate,@Kickback,@EFTType,@EFTAcctNum,@RoutingNum,@Division,@IssueType)")
                Database.ClearParameterCache()
                db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                db.AddInParameter(cmd, "RequestType", DbType.String, "Manual Payment")
                db.AddInParameter(cmd, "Reason", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", "Credit Card", "EFT"))
                db.AddInParameter(cmd, "Username", DbType.String, _employee.NTLogin)
                db.AddInParameter(cmd, "CCRName", DbType.String, _employee.FullNameFirstlast)
                db.AddInParameter(cmd, "SalesID", DbType.String, _employee.IcomsID)
                db.AddInParameter(cmd, "CSGOpCode", DbType.String, _employee.IcomsUserID)
                db.AddInParameter(cmd, "Supervisor", DbType.String, _employee.SupNameFirstLast)
                db.AddInParameter(cmd, "CFName", DbType.String, txtcfname.Text)
                db.AddInParameter(cmd, "CLName", DbType.String, txtclname.Text)
                db.AddInParameter(cmd, "State", DbType.String, txtstate.Text)
                db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)
                db.AddInParameter(cmd, "PhoneNum", DbType.String, txtmanpayphone.Text)
                db.AddInParameter(cmd, "Amount", DbType.String, payamount.Text)
                db.AddInParameter(cmd, "Kickback", DbType.Int32, "0")
                db.AddInParameter(cmd, "CardType", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", ddlCCard.SelectedValue, Nothing))
                Dim encryptedCCNum = String.Empty
                If rblCheckCredit.SelectedValue = "Credit Card" Then
                    Using cryptoClient As New CryptoSvc.CCCCryptoClient
                        encryptedCCNum = cryptoClient.encrypt(txtCCardNum.TextWithLiterals, Request.Url.AbsoluteUri, _employee.EmpID)
                    End Using
                End If
                db.AddInParameter(cmd, "CardNum", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", encryptedCCNum, Nothing))
                db.AddInParameter(cmd, "ExpireDate", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", txtExpDate.Text, Nothing))
                db.AddInParameter(cmd, "EFTType", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", Nothing, rblCheckCredit.SelectedValue))
                db.AddInParameter(cmd, "EFTAcctNum", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", Nothing, txtBank.Text))
                db.AddInParameter(cmd, "RoutingNum", DbType.String, If(rblCheckCredit.SelectedValue = "Credit Card", Nothing, txtRTN2.Text))
                db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                db.AddInParameter(cmd, "IssueType", DbType.String, "Manual Payment")
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

    Protected Sub rblCheckCredit_OnSelectedIndexChanged(ByVal o As Object, ByVal e As EventArgs) Handles rblCheckCredit.SelectedIndexChanged
        If rblCheckCredit.SelectedValue = "Credit Card" Then
            pnlCCard.Visible = True
            pnlBnkAcct.Visible = False
        Else
            pnlCCard.Visible = False
            pnlBnkAcct.Visible = True
        End If
    End Sub

    Protected Sub cvCreditCard_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvCreditCard.ServerValidate
        If GeneralFormsCommon.ValidateMaskedCC(args.Value) = GeneralFormsCommon.CreditCardTypes.Invalid Then
            args.IsValid = False
        Else
            args.IsValid = True
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
            Dim requestType As String = "Manual Payment"

            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand

            cmd = db.GetSqlStringCommand("SELECT count(*) FROM Billing WHERE (RequestType = @RequestType AND AcctNum = @AcctNum AND Kickback in (0,3))")
            db.AddInParameter(cmd, "RequestType", DbType.String, requestType)
            db.AddInParameter(cmd, "AcctNum", DbType.String, txtAcct.Text)

            If CBool(db.ExecuteScalar(cmd)) Then
                args.IsValid = False
                ResetMain()
            Else
                args.IsValid = True
            End If
        End If

    End Sub

    Protected Sub cvPhone_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvPhone.ServerValidate
        Dim myRegex As New Regex("0000000000|1111111111|2222222222|3333333333|4444444444|5555555555|6666666666|7777777777|8888888888|9999999999|0000000000")
        If myRegex.IsMatch(args.Value) Then
            args.IsValid = False
        Else
            args.IsValid = True
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
        pnlmanual.Visible = False
        pnlerror.Visible = False
    End Sub

    Private Sub ResetMain()
        pnlthx.Visible = False
        pnlmain.Visible = True
        pnlerror.Visible = False
        pnlmanual.Visible = False
        pnlAltPhone.Visible = False
    End Sub

    Protected Sub txtAcct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub
End Class
