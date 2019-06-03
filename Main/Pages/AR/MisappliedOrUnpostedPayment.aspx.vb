Imports System.Diagnostics.Eventing.Reader
Imports GeneralForms.Classes

Public Class ARMisappliedOrUnpostedPayment
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust
    Private _myCustomer As CustomerService.Cust
    ' Private _division As Integer

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            ResetMain()
            rdpPersonalCheckDateCleared.MaxDate = Date.Today
            rdpmonord3.MaxDate = Date.Today
            rdppctr4.MaxDate = Date.Today
            rdpcred4.MaxDate = Date.Today
            rdpeft4.MaxDate = Date.Today
            rdpcash2.MaxDate = Date.Today
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

    Public Sub GetTerminal(ByVal o As Object, ByVal e As EventArgs) Handles droptermid.SelectedIndexChanged
        Select Case droptermid.SelectedItem.Value
            Case "Yes"
                lbltermid.Text = "Terminal ID #:"
                pnlTermID.Visible = True
                rfvpctr2.ErrorMessage = "Terminal ID # is required."
                revpctr2.Enabled = True
            Case "No"
                lbltermid.Text = "Receipt #:"
                rfvpctr2.ErrorMessage = "Receipt # is required."
                revpctr2.Enabled = False
                pnlTermID.Visible = True
            Case Else
                pnlTermID.Visible = False
        End Select
    End Sub

    Public Sub GetForm()
        rfvAccount.Validate()
        revAccount.Validate()
        cvAccount.Validate()
        If Not (rfvAccount.IsValid AndAlso revAccount.IsValid AndAlso cvAccount.IsValid) Then
            pnlmisapp.Visible = False
        Else
            txtcfname.Text = Replace(txtcfname.Text.Trim.ToUpper, " ", "")
            txtclname.Text = Replace(txtclname.Text.Trim.ToUpper, " ", "")
            txtstate.Text = GeneralFormsCommon.getStateFromDivision(CInt(lblDivision.Text))
            txtAcct.ReadOnly = True
            pnlmisapp.Visible = True
        End If
    End Sub

    Public Sub GetMisControls(ByVal o As Object, ByVal e As EventArgs) Handles radmisapp.SelectedIndexChanged
        Select Case radmisapp.SelectedItem.Value
            Case "0" 'Personal Check
                pnlpctr.Visible = False : pnlchk.Visible = True : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = False

            Case "1" 'Money Order
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = True : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = False

            Case "2" 'Payment Center
                pnlpctr.Visible = True : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = False

            Case "3" 'Credit/Debit Card
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = True : pnlAch.Visible = False

            Case "4" 'EFT Payment
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = True
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = False

            Case "5" 'Cash Payment
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = True : pnlcred.Visible = False : pnlAch.Visible = False

            Case "6" 'ACH (Business Only)
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = True


            Case Else
                pnlpctr.Visible = False : pnlchk.Visible = False : pnleft.Visible = False
                pnlmonord.Visible = False : pnlcash.Visible = False : pnlcred.Visible = False : pnlAch.Visible = False

        End Select
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnmisappsend.Click
        'Removed dashes, spaces, and periods from phone number fields
        pnlerror.Visible = False
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
                'MISAPPLIED/UNPOSTED PAYMENT
                '***************************************
                Select Case radmisapp.SelectedItem.Value
                    Case "0" 'PERSONAL CHECK
                        '***************************************
                        ' MUP - PERSONAL CHECK
                        '***************************************
                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,CheckNum,RoutingNum,BankAcctNum,Amount,DateCleared,Comments,Division,DateMailed,AlternativePhoneNum,Email) VALUES " _
                                                                        & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@CheckNum,@RoutingNum,@BankAcctNum,@Amount,@DateCleared,@Comments,@Division,@DateMailed,@AlternativePhoneNum,@Email); SELECT @@IDENTITY")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Personal Check")
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
                        db.AddInParameter(cmd, "CheckNum", DbType.String, txtPersonalCheckNumber.Text)
                        db.AddInParameter(cmd, "RoutingNum", DbType.String, txtPersonalCheckRTN.Text)
                        db.AddInParameter(cmd, "BankAcctNum", DbType.String, txtPersonalCheckAccount.Text)
                        db.AddInParameter(cmd, "Amount", DbType.String, rntPersonalCheckAmount.Value)
                        db.AddInParameter(cmd, "DateCleared", DbType.String, rdpPersonalCheckDateCleared.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "DateMailed", DbType.String, rdpPersonalCheckDateMailed.SelectedDate)
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        Dim billingId As Int32 = db.ExecuteScalar(cmd)
                        For i As Integer = 1 To 10
                            Dim txtAchAccount As TextBox = Page.FindControl("txtCheckAccount" + i.ToString)
                            Dim txtAchAmount As TextBox = Page.FindControl("txtCheckAmount" + i.ToString)
                            If Not String.IsNullOrWhiteSpace(txtAchAccount.Text) AndAlso Not String.IsNullOrWhiteSpace(txtAchAmount.Text) Then
                                MisappliedPayment.AddExtraAccounts(billingId, txtAchAccount.Text, Convert.ToDecimal(txtAchAmount.Text))
                            End If

                        Next

                    Case "1" 'MONEY ORDER
                        '***************************************
                        ' MUP - MONEY ORDER
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,OrderNumAcct,OrderNum,Amount,DateMailed,Comments,Division,PaymentDate,MoneyOrderType,AlternativePhoneNum,Email) VALUES " _
                         & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@OrderNumAcct,@OrderNum,@Amount,@DateMailed,@Comments,@Division,@PaymentDate,@MoneyOrderType,@AlternativePhoneNum,@Email); SELECT @@IDENTITY")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Money Order")
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
                        db.AddInParameter(cmd, "OrderNumAcct", DbType.String, txtAcct.Text)
                        db.AddInParameter(cmd, "OrderNum", DbType.String, txtmonord1.Text)
                        db.AddInParameter(cmd, "Amount", DbType.String, rntMonord2.Value)
                        db.AddInParameter(cmd, "DateMailed", DbType.String, rdpmonord3.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "PaymentDate", DbType.String, rdpmonordcash.SelectedDate)
                        db.AddInParameter(cmd, "MoneyOrderType", DbType.String, dropmonordtype.SelectedItem.Value)
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        Dim billingId As Int32 = db.ExecuteScalar(cmd)
                        For i As Integer = 1 To 10
                            Dim txtAchAccount As TextBox = Page.FindControl("txtMoAccount" + i.ToString)
                            Dim txtAchAmount As TextBox = Page.FindControl("txtMoAmount" + i.ToString)
                            If Not String.IsNullOrWhiteSpace(txtAchAccount.Text) AndAlso Not String.IsNullOrWhiteSpace(txtAchAmount.Text) Then
                                MisappliedPayment.AddExtraAccounts(billingId, txtAchAccount.Text, Convert.ToDecimal(txtAchAmount.Text))
                            End If

                        Next

                    Case "2" 'PAYMENT CENTER
                        '***************************************
                        ' MUP - PAYMENT CENTER
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,PCAcctNum,IsTerminalID,TerminalID,Amount,PaymentDate,Comments,Division,AlternativePhoneNum,Email) VALUES " _
                         & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@PCAcctNum,@IsTerminalID,@TerminalID,@Amount,@PaymentDate,@Comments,@Division,@AlternativePhoneNum,@Email)")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Payment Center")
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
                        db.AddInParameter(cmd, "PCAcctNum", DbType.String, txtpctr1.Text)
                        db.AddInParameter(cmd, "IsTerminalID", DbType.String, droptermid.SelectedItem.Value)
                        db.AddInParameter(cmd, "TerminalID", DbType.String, txtpctr2.Text)
                        db.AddInParameter(cmd, "Amount", DbType.String, rntPctr3.Value)
                        db.AddInParameter(cmd, "PaymentDate", DbType.String, rdppctr4.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        db.ExecuteNonQuery(cmd)

                    Case "3" 'CREDIT/DEBIT CARD
                        '***************************************
                        ' MUP - CREDIT/DEBIT CARD
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,CardNum,ExpireDate,Amount,DateCleared,Comments,BatchNum,Division,AlternativePhoneNum,Email) VALUES " _
                         & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@CardNum,@ExpireDate,@Amount,@DateCleared,@Comments,@BatchNum,@Division,@AlternativePhoneNum,@Email)")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Credit/Debit Card")
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
                        Dim encryptedCCNum As String
                        Using cryptoClient As New CryptoSvc.CCCCryptoClient
                            encryptedCCNum = cryptoClient.encrypt(txtCCCardNumber.TextWithLiterals, Request.Url.AbsoluteUri, _employee.EmpID)
                        End Using
                        db.AddInParameter(cmd, "CardNum", DbType.String, encryptedCCNum)
                        Dim expDate As String = DatePart(DateInterval.Month, CDate(rmypCCExp.SelectedDate)).ToString("00") & "/" & DatePart(DateInterval.Year, CDate(rmypCCExp.SelectedDate))
                        db.AddInParameter(cmd, "ExpireDate", DbType.String, expDate)
                        db.AddInParameter(cmd, "Amount", DbType.String, rntCred3.Value)
                        db.AddInParameter(cmd, "DateCleared", DbType.String, rdpcred4.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "BatchNum", DbType.String, If(txtcredauth.Text.Length > 0, txtcredauth.Text, "*****"))
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        db.ExecuteNonQuery(cmd)

                    Case "4" 'EFT PAYMENT
                        '***************************************
                        ' MUP - EFT PAYMENT
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,RoutingNum,EFTAcctNum,Amount,DateCleared,Comments,BatchNum,Division,AlternativePhoneNum,Email) VALUES " _
                        & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@RoutingNum,@EFTAcctNum,@Amount,@DateCleared,@Comments,@BatchNum,@Division,@AlternativePhoneNum,@Email)")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "EFT Payment")
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
                        db.AddInParameter(cmd, "RoutingNum", DbType.String, txteft1.Text)
                        db.AddInParameter(cmd, "EFTAcctNum", DbType.String, txteft2.Text)
                        db.AddInParameter(cmd, "Amount", DbType.String, rntEft3.Value)
                        db.AddInParameter(cmd, "DateCleared", DbType.String, rdpeft4.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "BatchNum", DbType.String, If(txteftAuth.Text.Length > 0, txteftAuth.Text, "*****"))
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        db.ExecuteNonQuery(cmd)

                    Case "5" 'CASH PAYMENT
                        '***************************************
                        ' MUP - CASH PAYMENT
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Amount,PaymentDate,PaymentLoc,Comments,Division,AlternativePhoneNum,Email) VALUES " _
                         & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@Amount,@PaymentDate,@PaymentLoc,@Comments,@Division,@AlternativePhoneNum,@Email)")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "Cash Payment")
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
                        db.AddInParameter(cmd, "Amount", DbType.String, rntCash1.Value)
                        db.AddInParameter(cmd, "PaymentDate", DbType.String, rdpcash2.SelectedDate)
                        db.AddInParameter(cmd, "PaymentLoc", DbType.String, dropcash.SelectedItem.Value)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        db.ExecuteNonQuery(cmd)

                    Case "6" 'ACH (Business Only)
                        '***************************************
                        ' MUP - ACH
                        '***************************************

                        cmd = db.GetSqlStringCommand("INSERT INTO Billing (DateSub,RequestType,IssueType,Username,CCRName,CSGOpCode,SalesID,Supervisor,CFName,CLName,AcctNum,PhoneNum,State,Kickback,Amount,PaymentDate,Comments,Division,AlternativePhoneNum,Email) VALUES " _
                         & "(@DateSub,@RequestType,@IssueType,@Username,@CCRName,@CSGOpCode,@SalesID,@Supervisor,@CFName,@CLName,@AcctNum,@PhoneNum,@State,@Kickback,@Amount,@PaymentDate,@Comments,@Division,@AlternativePhoneNum,@Email);SELECT @@IDENTITY")
                        Database.ClearParameterCache()
                        db.AddInParameter(cmd, "DateSub", DbType.DateTime, Date.Now)
                        db.AddInParameter(cmd, "RequestType", DbType.String, "MUP")
                        db.AddInParameter(cmd, "IssueType", DbType.String, "ACH")
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
                        db.AddInParameter(cmd, "Amount", DbType.String, rntAch.Value)
                        db.AddInParameter(cmd, "PaymentDate", DbType.String, rdpAch.SelectedDate)
                        db.AddInParameter(cmd, "Comments", DbType.String, txtmisappcomm.Text)
                        db.AddInParameter(cmd, "Division", DbType.Int32, CInt(lblDivision.Text))
                        db.AddInParameter(cmd, "AlternativePhoneNum", DbType.String, txtAltPhoneNumber.Text)
                        db.AddInParameter(cmd, "Email", DbType.String, txtEmail.Text)
                        Dim billingId As Int32 = db.ExecuteScalar(cmd)
                        MisappliedPayment.AddAchInfo(billingId, txtAchAccountName.Text, txtAchInvoiceNumber.Text)
                        For i As Integer = 1 To 10
                            Dim txtAchAccount As TextBox = Page.FindControl("txtAchAccount" + i.ToString)
                            Dim txtAchAmount As TextBox = Page.FindControl("txtAchAmount" + i.ToString)
                            If Not String.IsNullOrWhiteSpace(txtAchAccount.Text) AndAlso Not String.IsNullOrWhiteSpace(txtAchAmount.Text) Then
                                MisappliedPayment.AddExtraAccounts(billingId, txtAchAccount.Text, Convert.ToDecimal(txtAchAmount.Text))
                            End If

                        Next

                End Select

                Reset()
            Catch mailex As Exception
                pnlerror.Visible = True
                lblerror.Text = "<br /><b>Error Message:</b> " & mailex.Message & "<br />" & _
                            "<b>Error Source:</b> " & mailex.Source & "<br />" & _
                            "<b>Stack Trace:</b> " & mailex.StackTrace & "<br />"
            End Try
        End If
    End Sub

    Public Sub ThankYou(ByVal o As Object, ByVal e As EventArgs) Handles btnthx.Click
        Response.Redirect(Request.Url.ToString)
    End Sub

    Sub Reset()
        pnlthx.Visible = True
        pnlmain.Visible = False
        pnlcash.Visible = False
        pnlchk.Visible = False
        pnlcred.Visible = False
        pnlcred.Visible = False
        pnleft.Visible = False
        pnlerror.Visible = False
        pnlmisapp.Visible = False
        pnlmonord.Visible = False
        pnlpctr.Visible = False
        pnlpctr.Visible = False
        pnlAch.Visible = False
    End Sub

    Protected Sub cvCreditCard_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvCreditCard.ServerValidate
        If GeneralFormsCommon.ValidateMaskedCC(args.Value) = GeneralFormsCommon.CreditCardTypes.Invalid Then
            args.IsValid = False
        Else
            args.IsValid = True
        End If
    End Sub

    Protected Sub cvAccount_ServerValidate(ByVal source As Object, ByVal args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvAccount.ServerValidate
        revAccount.Validate()
        rfvAccount.Validate()
        If Not (revAccount.IsValid And rfvAccount.IsValid) Then
            args.IsValid = True
        Else
            Dim requestType As String = "MUP"

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
            'Using customerClient As New CustomerService.CustomerManagementClient
            '    _myCustomer = customerClient.getByCustomerID(tempAccount)
            'End Using
            Dim icomsAccount = New IcomsAccount(tempAccount)
            If Not icomsAccount.IsAccountValid Then
                Me.MB.ShowMessage("Account number does not exist. Please try again.")
                txtAcct.Text = ""
                Exit Sub
            End If

            txtclname.Text = icomsAccount.LastName
            txtcfname.Text = icomsAccount.FirstName
            lblDivision.Text = icomsAccount.Division

            Dim customerDetails = GeneralFormsCommon.getCustomerDetails(tempAccount)

            txtEmail.Text = customerDetails.Account.ContactEmail
            txtPhoneNumber.Text = customerDetails.Account.DaytimePhoneNumber
            txtAltPhoneNumber.Text = customerDetails.Account.EveningPhoneNumber

            If icomsAccount.IsCommercial Then
                Dim liAch As ListItem = radmisapp.Items.FindByText("ACH (Business Customers ONLY)")
                If liAch Is Nothing Then
                    radmisapp.Items.Add(New ListItem("ACH (Business Customers ONLY)", 6))
                End If
            Else
                Dim liAch As ListItem = radmisapp.Items.FindByText("ACH (Business Customers ONLY)")
                If Not liAch Is Nothing Then
                    radmisapp.Items.Remove(liAch)
                End If
            End If

        Catch ex As Exception
            Dim errorAccount = ex.Message
            If errorAccount = "Customer Database Object is nothing." Then
                Me.MB.ShowMessage("Account number does not exist. Please try again.")
                txtAcct.Text = ""
                Exit Sub
            End If
        End Try

        'If _myCustomer Is Nothing Then
        '    Me.MB.ShowMessage("Lookup timed out.")
        '    Exit Sub
        'End If
        'If IsNothing(_myCustomer.IcomsCustomerID) Then
        '    Me.MB.ShowMessage("Lookup returned nothing.")
        '    ResetMain()
        '    txtclname.Text = String.Empty
        '    txtcfname.Text = String.Empty
        '    Exit Sub
        'End If

        'txtclname.Text = _myCustomer.LName
        'txtcfname.Text = _myCustomer.FName
        'lblDivision.Text = _myCustomer.Address.Division
        GetForm()

    End Sub

    Private Sub ResetMain()
        pnlthx.Visible = False
        pnlmain.Visible = True
        pnlcash.Visible = False
        pnlchk.Visible = False
        pnlcred.Visible = False
        pnlcred.Visible = False
        pnleft.Visible = False
        pnlerror.Visible = False
        pnlmisapp.Visible = False
        pnlmonord.Visible = False
        pnlpctr.Visible = False
        pnlpctr.Visible = False
        pnlAch.Visible = False

    End Sub

    Protected Sub txtcacct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub
End Class
