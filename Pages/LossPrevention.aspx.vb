Imports System.Drawing

Public Class LossPrevention
    Inherits System.Web.UI.Page

    Private _conn As System.Data.SqlClient.SqlConnection
    Private _sqlcmd As System.Data.SqlClient.SqlCommand
    Private _sqlrdr As System.Data.SqlClient.SqlDataReader
    Private _suplist As ArrayList
    Private _sqlstr, _suser, _sdomain, _mRecipient, _mBody, _mSubject As String
    Private _stracct, _stracct1 As String
    Private _employee As EmployeeService.EmpInstance
    Private _myCustomer As CustomerService.Cust

    Private Sub LoadEmployeeInfo()
        ' load employee
        _employee = GeneralFormsCommon.getEmployee()
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        Me.lblhName.Text = _employee.FullNameFirstLast
        Me.lblhDate.Text = Format(Date.Now, "Short Date")
        Me.lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()

        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Me.pnlnew.Visible = False
            Me.pnlrestart.Visible = False
            Me.pnlfraud.Visible = False
            'Me.pnllink.Visible = False
            Me.pnldn.Visible = False
            Me.pnlerror.Visible = False
            Me.pnlthx.Visible = False
            'Me.droplink2.Enabled = False
            revresacct.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            revsusacct.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
        End If

    End Sub

    Public Sub GetPanel(ByVal o As Object, ByVal e As EventArgs) Handles radformsel.SelectedIndexChanged
        Me.pnlnew.Visible = False
        Me.pnlrestart.Visible = False
        Me.pnlfraud.Visible = False
        'Me.pnllink.Visible = False
        Me.pnldn.Visible = False

        Select Case Me.radformsel.SelectedItem.Value
            Case "0"
                Me.pnlnew.Visible = True
                'Case "1"
                '    Me.pnlrestart.Visible = True
            Case "2"
                Me.pnlfraud.Visible = True
                'Case "3"
                '    Me.pnllink.Visible = True
            Case "4"
                Me.pnldn.Visible = True
            Case Else
                Me.pnlnew.Visible = True
        End Select
        ResetPanels(Me.radformsel.SelectedItem.Value)
    End Sub

    Public Sub ResetPanels(ByVal mypnl As String)
        Select Case mypnl
            Case "0"
                'Call ResetResPanel()
                Call ResetFraudPanel()
                'Call ResetLinkPanel()
                Call ResetDNPanel()
            Case "1"
                Call ResetNewPanel()
                Call ResetFraudPanel()
                'Call ResetLinkPanel()
                Call ResetDNPanel()
            Case "2"
                Call ResetNewPanel()
                'Call ResetResPanel()
                'Call ResetLinkPanel()
                Call ResetDNPanel()
                'Case "3"
                '    Call ResetNewPanel()
                '    Call ResetResPanel()
                '    Call ResetFraudPanel()
                '    Call ResetDNPanel()
            Case "4"
                Call ResetNewPanel()
                Call ResetFraudPanel()
                'Call ResetLinkPanel()
            Case Else
                Call ResetNewPanel()
                'Call ResetResPanel()
                Call ResetFraudPanel()
                'Call ResetLinkPanel()
                Call ResetDNPanel()
        End Select
    End Sub

    Public Sub ResetNewPanel()
        Me.txtnewfirst.Text = ""
        Me.txtnewlast.Text = ""
        Me.txtnewaddy.Text = ""
        Me.txtnewcity.Text = ""
        Me.dropnewstate.SelectedIndex = 0
        Me.rntnewPhone.Text = ""
        Me.txtnewssn.Text = ""
        Me.txtresdl.Text = ""
        Me.txtnewcomm.Text = ""
    End Sub

    'Public Sub ResetResPanel()
    '    Me.txtresfirst.Text = ""
    '    Me.txtreslast.Text = ""
    '    Me.txtresacct.Text = ""
    '    Me.txtresaddy.Text = ""
    '    Me.txtrescity.Text = ""
    '    Me.dropresstate.SelectedIndex = 0
    '    Me.txtresphone.Text = ""
    '    Me.txtresssn.Text = ""
    '    Me.txtresdl.Text = ""
    '    Me.txtrescomm.Text = ""
    'End Sub

    Public Sub ResetFraudPanel()
        Me.txtsusfirst.Text = ""
        Me.txtsuslast.Text = ""
        Me.txtsusacct.Text = ""
        Me.txtsusaddy.Text = ""
        Me.txtsuscity.Text = ""
        Me.dropsusstate.SelectedIndex = 0
        Me.dropsusrequest.SelectedIndex = 0
        Me.txtsusphone.Text = ""
        Me.txtsusssn.Text = ""
        Me.txtsusdl.Text = ""
        Me.txtsuscomm.Text = ""
    End Sub

    'Public Sub ResetLinkPanel()
    '    Me.txtlfname1.Text = ""
    '    Me.txtllname1.Text = ""
    '    Me.txtlacct1.Text = ""
    '    Me.txtlfname2.Text = ""
    '    Me.txtllname2.Text = ""
    '    Me.txtlacct2.Text = ""
    '    Me.droplink1.SelectedIndex = 0
    '    Me.droplink2.SelectedIndex = 0
    '    Me.txtlinkcomm.Text = ""
    'End Sub

    Public Sub ResetDNPanel()
        Me.txtdnaddy.Text = ""
        Me.txtdncity.Text = ""
        Me.dropdnstate.SelectedIndex = 0
        Me.txtdnzip.Text = ""
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnnewsend.Click
        If Me.Page.IsValid Then
            Try
                Me.pnlerror.Visible = False
                Me.pnlmain.Visible = True
                'GET DB STUFF PREPARED
                _conn = New System.Data.SqlClient.SqlConnection
                _conn.ConnectionString = "Server=CS-REPORTDB\REPORTS;UID=sa;PWD=w0wc$1#@;database=BillingDB;"
                _sqlcmd = New System.Data.SqlClient.SqlCommand
                _sqlcmd.Connection = _conn
                'GET ENVIRONMENT INFO
                _suser = Request.ServerVariables("AUTH_USER")
                _sdomain = InStr(_suser, "\")
                _suser = Mid(_suser, (_sdomain + 1), (Len(_suser) - _sdomain))
                _suser = LCase(_suser)
                'GET E-MAIL STUFF PREPARED
                '  Dim MailClient As New SmtpClient
                Dim mailMsg As New MailMessage

                mailMsg.From = New MailAddress(_employee.Email)
                mailMsg.IsBodyHtml = False
                Select Case o.id
                    Case "btnnewsend"
                        'VERIFY ZIP CODE IS NUMERIC
                        If Not IsNumeric(Me.txtnewzip.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The zip code is not numeric. Please verify the zip code and try again."
                            Exit Sub
                        End If
                        'VERIFY ZIP IS 5 DIGITS
                        If Me.txtnewzip.Text.Length < 5 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Zip Code must be 5 digits."
                            Me.txtnewzip.BackColor = Color.Yellow
                            Exit Sub
                        Else
                            Me.lblsuperr.Text = ""
                            Me.txtnewzip.BackColor = Color.White
                        End If
                        'VERIFY PHONE # IS NUMERIC
                        If Not IsNumeric(Me.rntnewPhone.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # is not numeric. Please verify the phone # and try again."
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS 10 DIGITS
                        If Me.rntnewPhone.Text.Length < 10 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # must be ten digits. Please try again."
                            Exit Sub
                        End If
                        'VERIFY COMMENTS ARE NOT TOO LONG
                        If Me.txtnewcomm.Text.Length > 499 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Comments must be less than 500 characters"
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS NOT CONSECUTIVE NUMBERS
                        Select Case Me.rntnewPhone.Text
                            Case "1111111111"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "2222222222"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "3333333333"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "4444444444"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "5555555555"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "6666666666"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "7777777777"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "8888888888"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "9999999999"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "0000000000"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.rntnewPhone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                        End Select
                        'VERIFY TEXT IS IN SSN FIELD
                        If Me.txtnewssn.Text.Length > 0 Then
                            'VERIFY SSN IS NUMERIC
                            If Not IsNumeric(Me.txtnewssn.Text) Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN is not numeric. Please verify the SSN and try again."
                                Exit Sub
                            End If
                            'VERIFY SSN IS 9 DIGITS
                            If Me.txtnewssn.Text.Length < 9 Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN must be nine digits. Please try again."
                                Exit Sub
                            End If
                        End If
                        If Me.txtnewcomm.Text.Length > 499 Then
                            Me.lblsuperr.Text = "Comments must be less than 500 characters."
                            Exit Sub
                        End If
                        'VERIFY House number is exactly 8 digits
                        'If Me.txtLocationID.Text.Trim.Length <> 8 Then
                        '    Me.lblsuperr.Visible = True
                        '    Me.lblsuperr.Text = "The Location ID must be 8 digits."
                        '    Exit Sub
                        'End If

                        Me._mSubject = "Loss Prevention - New Start Request"
                        Me._mBody = "Loss Prevention - New Start Request" & vbCrLf & _
                            "CCR Name:" & vbTab & _employee.FullNameFirstLast & vbCrLf & _
                            "ICOMS ID:" & vbTab & _employee.IcomsUserID & vbCrLf & _
                            "Supervisor: " & _employee.SupNameFirstLast & vbCrLf & _
                            "----------------------------------------------------" & vbCrLf & vbCrLf & _
                            "First Name:" & vbTab & Me.txtnewfirst.Text & vbCrLf & _
                            "Last Name:" & vbTab & Me.txtnewlast.Text & vbCrLf & _
                            "Address:" & vbTab & Me.txtnewaddy.Text & vbCrLf & _
                            "City:" & vbTab & vbTab & Me.txtnewcity.Text & vbCrLf & _
                            "State:" & vbTab & Me.dropnewstate.SelectedItem.Value & vbCrLf & _
                            "Phone #:" & vbTab & Me.rntnewPhone.Text & vbCrLf & _
                            "SSN:" & vbTab & vbTab & Me.txtnewssn.Text & vbCrLf & _
                            "DL #:" & vbTab & vbTab & Me.txtnewdl.Text & vbCrLf & _
                            "Comments: " & vbCrLf & Me.txtnewcomm.Text

                        _sqlstr = "INSERT INTO Skip_Trace "
                        _sqlstr = _sqlstr & "(RequestType,CCCUser,DateSub,CCRName,CSGOpCode,Supervisor,CFName,"
                        _sqlstr = _sqlstr & "CLName,Kickback,Address,City,State,Zip,PhoneNum,SSN,DLNUM,HouseNum,Comments, d2dRequest, d2dEmail) VALUES "
                        _sqlstr = _sqlstr & "(@type,@user,@date,@ccr,@csg,@sup,@cfname,@clname,@kb,"
                        _sqlstr = _sqlstr & "@addy,@city,@state,@zip,@phnum,@ssn,@dlnum,@housenum,@comm,@d2dReq,@d2dEmail) SELECT @@IDENTITY"
                        _sqlcmd.CommandText = _sqlstr
                        _sqlcmd.Parameters.AddWithValue("@type", "New Start Request")
                        _sqlcmd.Parameters.AddWithValue("@user", _suser)
                        _sqlcmd.Parameters.AddWithValue("@date", Date.Now)
                        _sqlcmd.Parameters.AddWithValue("@ccr", _employee.FullNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@csg", _employee.IcomsUserID)
                        _sqlcmd.Parameters.AddWithValue("@sup", _employee.SupNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@cfname", Me.txtnewfirst.Text)
                        _sqlcmd.Parameters.AddWithValue("@clname", Me.txtnewlast.Text)
                        _sqlcmd.Parameters.AddWithValue("@kb", "0")
                        _sqlcmd.Parameters.AddWithValue("@addy", Me.txtnewaddy.Text)
                        _sqlcmd.Parameters.AddWithValue("@city", Me.txtnewcity.Text)
                        _sqlcmd.Parameters.AddWithValue("@state", Me.dropnewstate.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@zip", Me.txtnewzip.Text)
                        _sqlcmd.Parameters.AddWithValue("@phnum", Me.rntnewPhone.Text)
                        _sqlcmd.Parameters.AddWithValue("@ssn", Me.txtnewssn.Text)
                        _sqlcmd.Parameters.AddWithValue("@dlnum", Me.txtnewdl.Text)
                        _sqlcmd.Parameters.AddWithValue("@housenum", Me.txtLocationID.Text)
                        _sqlcmd.Parameters.AddWithValue("@comm", Me.txtnewcomm.Text)
                        _sqlcmd.Parameters.AddWithValue("@d2dReq", cbD2d.Checked)
                        If cbD2d.Checked Then
                            _sqlcmd.Parameters.AddWithValue("@d2dEmail", txtD2DEmail.Text)
                        Else
                            _sqlcmd.Parameters.AddWithValue("@d2dEmail", DBNull.Value)
                        End If
                        _conn.Open()
                        Dim lpID As Integer = CInt(_sqlcmd.ExecuteScalar())
                        _conn.Close()
                        Me._conn.Dispose()
                        Me._sqlcmd.Dispose()
                        Me.pnlnew.Visible = False
                        Me.pnlmain.Visible = False
                        Me.pnlthx.Visible = True
                        Me.lblD2DConfirmation.Visible = True
                        Me.lblD2DConfirmation.Text = "Door To Door Confirmation Number is " & lpID.ToString & "."
                    Case "btnressend"

                        'VERIFY ZIP CODE IS NUMERIC
                        If Not IsNumeric(Me.txtreszip.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The zip code is not numeric. Please verify the zip code and try again."
                            Exit Sub
                        End If
                        'VERIFY ZIP IS 5 DIGITS
                        If Me.txtreszip.Text.Length < 5 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Zip Code must be 5 digits."
                            Me.txtreszip.BackColor = Color.Yellow
                            Exit Sub
                        Else
                            Me.lblsuperr.Text = ""
                            Me.txtreszip.BackColor = Color.White
                        End If
                        'VERIFY PHONE # IS NUMERIC
                        If Not IsNumeric(Me.txtresphone.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # is not numeric. Please verify the phone # and try again."
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS 10 DIGITS
                        If Me.txtresphone.Text.Length < 10 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # must be ten digits. Please try again."
                            Exit Sub
                        End If
                        'VERIFY COMMENTS ARE NOT TOO LONG
                        If Me.txtrescomm.Text.Length > 499 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Comments must be less than 500 characters"
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS NOT CONSECUTIVE NUMBERS
                        Select Case Me.txtresphone.Text
                            Case "1111111111"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresacct.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "2222222222"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "3333333333"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "4444444444"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "5555555555"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "6666666666"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "7777777777"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "8888888888"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "9999999999"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "0000000000"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtresphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                        End Select
                        'VERIFY SSN HAS TEXT
                        If Me.txtresssn.Text.Length > 0 Then
                            'VERIFY SSN IS NUMERIC
                            If Not IsNumeric(Me.txtresssn.Text) Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN is not numeric. Please verify the SSN and try again."
                                Exit Sub
                            End If
                            'VERIFY SSN IS 9 DIGITS
                            If Me.txtresssn.Text.Length < 9 Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN must be nine digits. Please try again."
                                Exit Sub
                            End If
                        End If
                        If Me.txtrescomm.Text.Length > 499 Then
                            Me.lblsuperr.Text = "Comments must be less than 500 characters."
                            Exit Sub
                        End If
                        'Response.Write("Result: " & stracct)
                        Me._mSubject = "Loss Prevention - Restart Request"
                        Me._mBody = "Loss Prevention - Restart Request" & vbCrLf & _
                            "CCR Name:" & vbTab & _employee.FullNameFirstLast & vbCrLf & _
                            "ICOMS ID:" & vbTab & _employee.IcomsUserID & vbCrLf & _
                            "Supervisor: " & _employee.SupNameFirstLast & vbCrLf & _
                            "----------------------------------------------------" & vbCrLf & vbCrLf & _
                            "First Name:" & vbTab & Me.txtresfirst.Text & vbCrLf & _
                            "Last Name:" & vbTab & Me.txtreslast.Text & vbCrLf & _
                            "Address:" & vbTab & Me.txtresaddy.Text & vbCrLf & _
                            "City:" & vbTab & vbTab & Me.txtrescity.Text & vbCrLf & _
                            "State:" & vbTab & Me.dropresstate.SelectedItem.Value & vbCrLf & _
                            "Phone #:" & vbTab & Me.txtresphone.Text & vbCrLf & _
                            "SSN:" & vbTab & vbTab & Me.txtresssn.Text & vbCrLf & _
                            "DL #:" & vbTab & vbTab & Me.txtresdl.Text & vbCrLf & _
                            "Comments: " & vbCrLf & Me.txtrescomm.Text

                        _sqlstr = "INSERT INTO Skip_Trace "
                        _sqlstr = _sqlstr & "(RequestType,CCCUser,DateSub,CCRName,CSGOpCode,Supervisor,CFName,"
                        _sqlstr = _sqlstr & "CLName,Kickback,AcctNum,Address,City,State,Zip,SameAddress,"
                        _sqlstr = _sqlstr & "PhoneNum,SSN,DLNUM,Comments) VALUES "
                        _sqlstr = _sqlstr & "(@type,@user,@date,@ccr,@csg,@sup,@cfname,@clname,@kb,"
                        _sqlstr = _sqlstr & "@anum,@addy,@city,@state,@zip,@saddy,@phnum,@ssn,@dlnum,@comm)"
                        _sqlcmd.CommandText = _sqlstr
                        _sqlcmd.Parameters.AddWithValue("@type", "Restart Request")
                        _sqlcmd.Parameters.AddWithValue("@user", _suser)
                        _sqlcmd.Parameters.AddWithValue("@date", Date.Now)
                        _sqlcmd.Parameters.AddWithValue("@ccr", _employee.FullNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@csg", _employee.IcomsUserID)
                        _sqlcmd.Parameters.AddWithValue("@sup", _employee.SupNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@cfname", Me.txtresfirst.Text)
                        _sqlcmd.Parameters.AddWithValue("@clname", Me.txtreslast.Text)
                        _sqlcmd.Parameters.AddWithValue("@kb", "0")
                        _sqlcmd.Parameters.AddWithValue("@anum", Me.txtresacct.Text)
                        _sqlcmd.Parameters.AddWithValue("@addy", Me.txtresaddy.Text)
                        _sqlcmd.Parameters.AddWithValue("@city", Me.txtrescity.Text)
                        _sqlcmd.Parameters.AddWithValue("@state", Me.dropresstate.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@zip", Me.txtreszip.Text)
                        _sqlcmd.Parameters.AddWithValue("@saddy", Me.dropsame.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@phnum", Me.txtresphone.Text)
                        _sqlcmd.Parameters.AddWithValue("@ssn", Me.txtresssn.Text)
                        _sqlcmd.Parameters.AddWithValue("@dlnum", Me.txtresdl.Text)
                        _sqlcmd.Parameters.AddWithValue("@comm", Me.txtrescomm.Text)
                        _conn.Open()
                        _sqlcmd.ExecuteNonQuery()
                        _conn.Close()
                        Me._conn.Dispose()
                        Me._sqlcmd.Dispose()
                        Me.pnlrestart.Visible = False
                        Me.pnlmain.Visible = False
                        Me.pnlthx.Visible = True
                    Case "btnsussend"
                        'VERIFY ZIP IS 5 DIGITS
                        If Me.txtsuszip.Text.Length < 5 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Zip Code must be 5 digits."
                            Me.txtsuszip.BackColor = Color.Yellow
                            Exit Sub
                        Else
                            Me.lblsuperr.Text = ""
                            Me.txtsuszip.BackColor = Color.White
                        End If
                        'VERIFY ZIP CODE IS NUMERIC
                        If Not IsNumeric(Me.txtsuszip.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The zip code is not numeric. Please verify the zip code and try again."
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS NUMERIC
                        If Not IsNumeric(Me.txtsusphone.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # is not numeric. Please verify the phone # and try again."
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS 10 DIGITS
                        If Me.txtsusphone.Text.Length < 10 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # must be ten digits. Please try again."
                            Exit Sub
                        End If
                        'VERIFY COMMENTS ARE NOT TOO LONG
                        If Me.txtsuscomm.Text.Length > 499 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Comments must be less than 500 characters"
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS NOT CONSECUTIVE NUMBERS
                        Select Case Me.txtsusphone.Text
                            Case "1111111111"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "2222222222"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "3333333333"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "4444444444"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "5555555555"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "6666666666"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "7777777777"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "8888888888"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "9999999999"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "0000000000"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtsusphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                        End Select
                        'VERIFY SSB FIeLD HAS TEXT
                        If Me.txtsusssn.Text.Length > 0 Then
                            'VERIFY SSN IS NUMERIC
                            If Not IsNumeric(Me.txtsusssn.Text) Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN is not numeric. Please verify the SSN and try again."
                                Exit Sub
                            End If
                            'VERIFY SSN IS 9 DIGITS
                            If Me.txtsusssn.Text.Length < 9 Then
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = "The SSN must be nine digits. Please try again."
                                Exit Sub
                            End If
                        End If
                        If Me.txtsuscomm.Text.Length > 499 Then
                            Me.lblsuperr.Text = "Comments must be less than 500 characters."
                            Exit Sub
                        End If

                        Me._mSubject = "Loss Prevention - Suspected Fraud"
                        Me._mBody = "Loss Prevention - Suspected Fraud" & vbCrLf & _
                            "CCR Name:" & vbTab & _employee.FullNameFirstLast & vbCrLf & _
                            "ICOMS ID:" & vbTab & _employee.IcomsUserID & vbCrLf & _
                            "Supervisor: " & _employee.SupNameFirstLast & vbCrLf & _
                            "----------------------------------------------------" & vbCrLf & vbCrLf & _
                            "First Name:" & vbTab & Me.txtsusfirst.Text & vbCrLf & _
                            "Last Name:" & vbTab & Me.txtsuslast.Text & vbCrLf & _
                            "Address:" & vbTab & Me.txtsusaddy.Text & vbCrLf & _
                            "City:" & vbTab & vbTab & Me.txtsuscity.Text & vbCrLf & _
                            "State:" & vbTab & Me.dropsusstate.SelectedItem.Value & vbCrLf & _
                            "Phone #:" & vbTab & Me.txtsusphone.Text & vbCrLf & _
                            "SSN:" & vbTab & vbTab & Me.txtsusssn.Text & vbCrLf & _
                            "DL #:" & vbTab & vbTab & Me.txtsusdl.Text & vbCrLf & _
                            "Comments: " & vbCrLf & Me.txtsuscomm.Text

                        _sqlstr = "INSERT INTO Skip_Trace "
                        _sqlstr = _sqlstr & "(RequestType,CCCUser,DateSub,CCRName,CSGOpCode,Supervisor,CFName,"
                        _sqlstr = _sqlstr & "CLName,Kickback,AcctNum,Address,City,State,Zip,PhoneNum,SSN,DLNUM,"
                        _sqlstr = _sqlstr & "RequestServices,Comments) VALUES "
                        _sqlstr = _sqlstr & "(@type,@user,@date,@ccr,@csg,@sup,@cfname,@clname,@kb,"
                        _sqlstr = _sqlstr & "@anum,@addy,@city,@state,@zip,@phnum,@ssn,@dlnum,@resserv,@comm)"
                        _sqlcmd.CommandText = _sqlstr
                        _sqlcmd.Parameters.AddWithValue("@type", "Suspected Fraud")
                        _sqlcmd.Parameters.AddWithValue("@user", _suser)
                        _sqlcmd.Parameters.AddWithValue("@date", Date.Now)
                        _sqlcmd.Parameters.AddWithValue("@ccr", _employee.FullNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@csg", _employee.IcomsUserID)
                        _sqlcmd.Parameters.AddWithValue("@sup", _employee.SupNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@cfname", Me.txtsusfirst.Text)
                        _sqlcmd.Parameters.AddWithValue("@clname", Me.txtsuslast.Text)
                        _sqlcmd.Parameters.AddWithValue("@kb", "0")
                        _sqlcmd.Parameters.AddWithValue("@anum", Me.txtsusacct.Text)
                        _sqlcmd.Parameters.AddWithValue("@addy", Me.txtsusaddy.Text)
                        _sqlcmd.Parameters.AddWithValue("@city", Me.txtsuscity.Text)
                        _sqlcmd.Parameters.AddWithValue("@state", Me.dropsusstate.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@zip", Me.txtsuszip.Text)
                        _sqlcmd.Parameters.AddWithValue("@phnum", Me.txtsusphone.Text)
                        _sqlcmd.Parameters.AddWithValue("@ssn", Me.txtsusssn.Text)
                        _sqlcmd.Parameters.AddWithValue("@dlnum", Me.txtsusdl.Text)
                        _sqlcmd.Parameters.AddWithValue("@resserv", Me.dropsusrequest.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@comm", Me.txtsuscomm.Text)
                        _conn.Open()
                        _sqlcmd.ExecuteNonQuery()
                        _conn.Close()
                        Me._conn.Dispose()
                        Me._sqlcmd.Dispose()
                        Me.pnlfraud.Visible = False
                        Me.pnlmain.Visible = False
                        Me.pnlthx.Visible = True
                    Case "btndnsend"
                        'VERIFY PHONE # IS NUMERIC
                        If Not IsNumeric(Me.txtdnphone.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # is not numeric. Please verify the Account # and try again."
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS 10 DIGITS
                        If Me.txtdnphone.Text.Length < 10 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The phone # must be ten digits. Please try again."
                            Exit Sub
                        End If
                        'VERIFY COMMENTS ARE NOT TOO LONG
                        If Me.txtdncomm.Text.Length > 499 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "Comments must be less than 500 characters"
                            Exit Sub
                        End If
                        'VERIFY PHONE # IS NOT CONSECUTIVE NUMBERS
                        Select Case Me.txtdnphone.Text
                            Case "1111111111"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "2222222222"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "3333333333"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "4444444444"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "5555555555"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "6666666666"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "7777777777"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "8888888888"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "9999999999"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                            Case "0000000000"
                                Me.lblsuperr.Visible = True
                                Me.lblsuperr.Text = Me.txtdnphone.Text & " is an invalid Phone #. Please try again."
                                Exit Sub
                        End Select
                        'VERIFY ZIP CODE IS NUMERIC
                        If Not IsNumeric(Me.txtdnzip.Text) Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The zip code is not numeric. Please verify the zip code and try again."
                            Exit Sub
                        End If
                        'VERIFY ZIP CODE IS 5 DIGITS
                        If Me.txtdnzip.Text.Length < 5 Then
                            Me.lblsuperr.Visible = True
                            Me.lblsuperr.Text = "The zip code must be 5 digits. Please try again."
                            Exit Sub
                        End If
                        If Me.txtdncomm.Text.Length > 499 Then
                            Me.lblsuperr.Text = "Comments must be less than 500 characters."
                            Exit Sub
                        End If

                        Me._mSubject = "Loss Prevention - Unblock Address"
                        Me._mBody = "Loss Prevention - Unblock Address" & vbCrLf & _
                            "CCR Name:" & vbTab & _employee.FullNameFirstLast & vbCrLf & _
                            "ICOMS ID:" & vbTab & _employee.IcomsUserID & vbCrLf & _
                            "Supervisor: " & _employee.SupNameFirstLast & vbCrLf & _
                            "----------------------------------------------------" & vbCrLf & vbCrLf & _
                            "First Name:" & vbTab & Me.txtdnfname.Text & vbCrLf & _
                            "Last Name:" & vbTab & Me.txtdnlname.Text & vbCrLf & _
                            "Phone #:" & vbTab & Me.txtdnphone.Text & vbCrLf & _
                            "Address:" & vbTab & Me.txtdnaddy.Text & vbCrLf & _
                            "City:" & vbTab & Me.txtdncity.Text & vbCrLf & _
                            "State:" & vbTab & Me.dropdnstate.SelectedItem.Value & vbCrLf & _
                            "Zip:" & vbTab & vbTab & Me.txtdnzip.Text & vbCrLf & _
                            "Comments:" & vbTab & Me.txtdncomm.Text

                        _sqlstr = "INSERT INTO Skip_Trace "
                        _sqlstr = _sqlstr & "(RequestType,CCCUser,DateSub,CCRName,CSGOpCode,Supervisor,CFName,"
                        _sqlstr = _sqlstr & "CLName,Kickback,PhoneNum,Address,City,State,Zip,SSN,"
                        _sqlstr = _sqlstr & "DLNum,Comments) VALUES "
                        _sqlstr = _sqlstr & "(@type,@user,@date,@ccr,@csg,@sup,@cfname,@clname,@kb,"
                        _sqlstr = _sqlstr & "@phnum,@addy,@city,@state,@zip,@ssn,@dlnum,@comm)"
                        _sqlcmd.CommandText = _sqlstr
                        _sqlcmd.Parameters.AddWithValue("@type", "Unblock Address")
                        _sqlcmd.Parameters.AddWithValue("@user", _suser)
                        _sqlcmd.Parameters.AddWithValue("@date", Date.Now)
                        _sqlcmd.Parameters.AddWithValue("@ccr", _employee.FullNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@csg", _employee.IcomsUserID)
                        _sqlcmd.Parameters.AddWithValue("@sup", _employee.SupNameFirstLast)
                        _sqlcmd.Parameters.AddWithValue("@cfname", Me.txtdnfname.Text)
                        _sqlcmd.Parameters.AddWithValue("@clname", Me.txtdnlname.Text)
                        _sqlcmd.Parameters.AddWithValue("@kb", "0")
                        _sqlcmd.Parameters.AddWithValue("@phnum", Me.txtdnphone.Text)
                        _sqlcmd.Parameters.AddWithValue("@addy", Me.txtdnaddy.Text)
                        _sqlcmd.Parameters.AddWithValue("@city", Me.txtdncity.Text)
                        _sqlcmd.Parameters.AddWithValue("@state", Me.dropdnstate.SelectedItem.Value)
                        _sqlcmd.Parameters.AddWithValue("@zip", Me.txtdnzip.Text)
                        _sqlcmd.Parameters.AddWithValue("@ssn", Me.txtdnssn.Text)
                        _sqlcmd.Parameters.AddWithValue("@dlnum", Me.txtdndl.Text)
                        _sqlcmd.Parameters.AddWithValue("@comm", Me.txtdncomm.Text)
                        _conn.Open()
                        _sqlcmd.ExecuteNonQuery()
                        _conn.Close()
                        Me._conn.Dispose()
                        Me._sqlcmd.Dispose()
                        Me.pnldn.Visible = False
                        Me.pnlmain.Visible = False
                        Me.pnlthx.Visible = True
                End Select
            Catch mailex As Exception
                Me.lblerr.Text = "<br /><b>Error Message:</b> " & mailex.Message & "<br />" & _
                        "<b>Error Source:</b> " & mailex.Source & "<br />" & _
                        "<b>Stack Trace:</b> " & mailex.StackTrace & "<br />"
                Me.pnlerror.Visible = True
                Me.pnlmain.Visible = False
            End Try
        End If
    End Sub

    Public Sub GoBack(ByVal o As Object, ByVal e As EventArgs) Handles btnreturn.Click
        Response.Redirect("./LossPrevention.aspx")
    End Sub

    Public Sub Confirmation(ByVal o As Object, ByVal e As EventArgs) Handles btnthx.Click
        Response.Redirect("./LossPrevention.aspx")
    End Sub

    Protected Sub ibSFGo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibSFGo.Click
        Dim tempAccount As String = txtsusacct.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revsusacct.Validate()
        Me.rfvsusacct.Validate()
        If Not (Me.revsusacct.IsValid And Me.rfvsusacct.IsValid) Then
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
                txtsusacct.Text = ""
                Exit Sub
            End If
        End Try

        If _myCustomer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(_myCustomer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            txtsuslast.Text = String.Empty
            txtsusfirst.Text = String.Empty
            Exit Sub
        End If

        txtsuslast.Text = _myCustomer.LName
        txtsusfirst.Text = _myCustomer.FName
        If Not String.IsNullOrWhiteSpace(_myCustomer.Address.Addr2) Then
            txtsusaddy.Text = _myCustomer.Address.Addr1.Trim() & " " & _myCustomer.Address.Addr2.Trim()
        Else
            txtsusaddy.Text = _myCustomer.Address.Addr1.Trim()
        End If
        txtsuscity.Text = _myCustomer.Address.City
        txtsuszip.Text = _myCustomer.Address.Zip
        dropsusstate.ClearSelection()
        dropsusstate.Items.FindByValue(_myCustomer.Address.State).Selected = True
        txtsusphone.Text = _myCustomer.PrimaryPhone.PhoneOnly
    End Sub
    Protected Sub ibResGo_Click(ByVal sender As Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles ibResGo.Click
        Dim tempAccount As String = txtresacct.Text.Trim
        If String.IsNullOrWhiteSpace(tempAccount) Then
            Exit Sub
        End If
        Me.revresacct.Validate()
        Me.rfvresacct.Validate()
        If Not (Me.revresacct.IsValid And Me.rfvresacct.IsValid) Then
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
                txtresacct.Text = ""
                Exit Sub
            End If
        End Try

        If _myCustomer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(_myCustomer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            txtreslast.Text = String.Empty
            txtresfirst.Text = String.Empty
            Exit Sub
        End If

        txtreslast.Text = _myCustomer.LName
        txtresfirst.Text = _myCustomer.FName
        txtresaddy.Text = _myCustomer.Address.Addr1.Trim() & " " & _myCustomer.Address.Addr2.Trim()
        txtrescity.Text = _myCustomer.Address.City
        txtreszip.Text = _myCustomer.Address.Zip
        dropresstate.ClearSelection()
        dropresstate.Items.FindByValue(_myCustomer.Address.State).Selected = True
        txtresphone.Text = _myCustomer.PrimaryPhone.PhoneOnly
    End Sub
    Protected Sub txtresacct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtresacct.TextChanged
        ibResGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub

    Private Sub ibHseNumber_Click(sender As Object, e As System.Web.UI.ImageClickEventArgs) Handles ibHseNumber.Click
        Try
            Using addressClient As New AddressService.AddressManagementClient
                Try
                    Dim addy As AddressService.AddressIcoms = addressClient.getAddressByLocation(txtLocationID.Text)
                    If Not String.IsNullOrWhiteSpace(addy.Addr2) Then
                        txtnewaddy.Text = addy.Addr1.Trim() & " " & addy.Addr2.Trim()
                    Else
                        txtnewaddy.Text = addy.Addr1.Trim()
                    End If
                    txtnewcity.Text = addy.City
                    Try
                        Dim fullState = String.Empty
                        Select Case addy.State
                            Case "GA"
                                fullState = "Georgia"
                            Case "OH"
                                fullState = "Ohio"
                            Case "IL"
                                fullState = "Illinois"
                            Case "IN"
                                fullState = "Indiana"
                            Case "MI"
                                fullState = "Michigan"
                        End Select
                        dropnewstate.SelectedValue = Nothing
                        dropnewstate.Items.FindByValue(fullState).Selected = True
                    Catch ex As Exception
                    End Try
                    txtnewzip.Text = addy.Zip
                Catch ex As Exception 'TO DISPOSE PROPERLY
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Sub

    Private Sub cbD2D_CheckedChanged(sender As Object, e As System.EventArgs) Handles cbD2d.CheckedChanged
        lblD2D.Visible = cbD2d.Checked
        txtD2DEmail.Visible = cbD2d.Checked
        rfvd2dEmail.Enabled = cbD2d.Checked
    End Sub

    Protected Sub ibtxtDnHouseNumber_Click(sender As Object, e As ImageClickEventArgs) Handles ibtxtDnHouseNumber.Click
        Try
            Using addressClient As New AddressService.AddressManagementClient
                Try
                    Dim addy As AddressService.AddressIcoms = addressClient.getAddressByLocation(txtDnHouseNumber.Text)
                    If Not String.IsNullOrWhiteSpace(addy.Addr2) Then
                        txtdnaddy.Text = addy.Addr1.Trim() & " " & addy.Addr2.Trim()
                    Else
                        txtdnaddy.Text = addy.Addr1.Trim()
                    End If
                    txtdncity.Text = addy.City
                    Try
                        Dim fullState = String.Empty
                        Select Case addy.State
                            Case "GA"
                                fullState = "Georgia"
                            Case "OH"
                                fullState = "Ohio"
                            Case "IL"
                                fullState = "Illinois"
                            Case "IN"
                                fullState = "Indiana"
                            Case "MI"
                                fullState = "Michigan"
                        End Select
                        dropdnstate.SelectedValue = Nothing
                        dropdnstate.Items.FindByValue(fullState).Selected = True
                    Catch ex As Exception
                    End Try
                    txtdnzip.Text = addy.Zip
                Catch ex As Exception 'TO DISPOSE PROPERLY
                End Try
            End Using
        Catch ex As Exception

        End Try
    End Sub
End Class
