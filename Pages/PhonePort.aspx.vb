Imports System.Web
Imports GeneralForms.PortPS

Partial Public Class PhonePort
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance
    Private _myRB As ResponseBody

    Private Sub LoadEmployeeInfo()
        ' load employee
        _employee = GeneralFormsCommon.getEmployee()
        If Not _employee.Active Then Response.Redirect(VirtualPathUtility.ToAbsolute("~/Unauthorized.aspx"), True)

        'Set ID Bar info
        Me.lblhName.Text = _employee.FullNameFirstLast
        Me.lblhDate.Text = Format(Date.Now, "Short Date")
        Me.lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load, Me.Load
        LoadEmployeeInfo()

        'Put user code to initialize the page here
        Me.lblhName.Text = _employee.FullNameFirstLast
        Me.lblhDate.Text = Format(Date.Now, "Short Date")
        Me.lblhIcomsID.Text = _employee.IcomsUserID
    End Sub

    Protected Sub btnsubmit_Click(ByVal sender As Object, ByVal e As EventArgs) Handles btnsubmit.Click
        Try
            If rblPhoneCount.SelectedValue = "1" Then
                _myRB = callGetCurrent(txtPhone.Text.Trim, txtHousekey.Text.Trim)
                If _myRB.SPID = "9300" Then
                    ViewState.Add("RB", _myRB)
                    ViewState.Add("Phone", txtPhone.Text.Trim)
                    showDsl()
                    Exit Sub
                End If
                If _myRB.RequiresPIN Then
                    ViewState.Add("RB", _myRB)
                    ViewState.Add("Phone", txtPhone.Text.Trim)
                    showPin()
                    Exit Sub
                End If
                If _myRB.RequiresAccountNumber Then
                    ViewState.Add("RB", _myRB)
                    ViewState.Add("Phone", txtPhone.Text.Trim)
                    showAccount()
                    Exit Sub
                End If
                _myRB = callUpdateHouse(txtPhone.Text.Trim, txtHousekey.Text.Trim, Nothing, Nothing, Nothing)
            Else
                _myRB = callUpdateHouse("0000000000", txtHousekey.Text.Trim, Nothing, Nothing, Nothing)
            End If

            If _myRB.SetPortFlag = "Y" Then
                Show3PV(True)
            Else
                Show3PV(False)
            End If
            ResetForm()
        Catch ex As Exception
            Dim msg As MailMessage = New MailMessage("ccc_it@wideopenwest.com", "ccc_it@wideopenwest.com", "Phone Ownership Tool Error", "The phone ownership tool threw an error" _
                & vbCrLf & "User: " & _employee.NTLogin _
                & vbCrLf & "Number(s): " & txtPhone.Text & " " & txtPhone2.Text _
                & vbCrLf & "HouseKey: " & txtHousekey.Text _
                & vbCrLf & "Message: " & ex.Message _
                & vbCrLf & "Stack Trace: " & ex.StackTrace)
            msg.To.Add("d_davis@wideopenwest.com")
            ' Dim Client As SmtpClient = New SmtpClient()
            EmailProxy.Send(msg)
        End Try
    End Sub


    Private Function formatTN(ByVal TN As String) As String
        Return "(" & TN.Substring(0, 3) & ") " & TN.Substring(3, 3) & "-" & TN.Substring(6, 4)
    End Function

    Private Function callUpdateHouse(ByVal TN As String, ByVal houseKey As String, spid As String, account As String, pin As String) As ResponseBody
        ' Dim local variables
        Dim myPortPS As PortPS.PortPS = New PortPS.PortPS()
        Dim myRequest As PortPS.PortPSRequest = New PortPS.PortPSRequest()
        ' Set up the request
        myRequest.RequestBody = New PortPS.RequestBody()
        myRequest.RequestHeader = New PortPS.RequestHeader()
        myRequest.RequestBody.TelephoneNumber = TN
        myRequest.RequestBody.HouseKey = houseKey
        myRequest.RequestBody.AccountNumber = account
        myRequest.RequestBody.PIN = pin
        myRequest.RequestBody.SPID = spid
        ' Get and return the response body
        Try
            Return myPortPS.UpdateHouseForPortedNumber(myRequest).ResponseBody
        Catch ex As System.Web.Services.Protocols.SoapException
            Dim myResponse As New ResponseBody()
            myResponse.SPID = 0
            myResponse.ErrorCode = 999
            myResponse.ErrorMessage = "The web service responded with an error.<br />Exception Details: " & ex.Message
            Return myResponse
            ErrorBlock.Visible = True
        End Try
    End Function

    Private Function callGetCurrent(ByVal TN As String, ByVal houseKey As String) As ResponseBody
        ' Dim local variables
        Dim myPortPS As PortPS.PortPS = New PortPS.PortPS()
        Dim myRequest As PortPS.PortPSRequest = New PortPS.PortPSRequest()
        ' Set up the request
        myRequest.RequestBody = New PortPS.RequestBody()
        myRequest.RequestHeader = New PortPS.RequestHeader()
        myRequest.RequestBody.TelephoneNumber = TN
        myRequest.RequestBody.HouseKey = houseKey
        ' Get and return the response body
        Try
            Return myPortPS.GetCurrentProviderByTN(myRequest).ResponseBody
        Catch ex As System.Web.Services.Protocols.SoapException
            Dim myResponse As New ResponseBody()
            myResponse.SPID = 0
            myResponse.ErrorCode = 999
            myResponse.ErrorMessage = "The web service responded with an error.<br />Exception Details: " & ex.Message
            Return myResponse
            ErrorBlock.Visible = True
        End Try
    End Function

    Protected Sub rblPhoneCount_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rblPhoneCount.SelectedIndexChanged
        ResetAddlInfo()
        If rblPhoneCount.SelectedValue = 1 Then
            trTel2.Visible = False
            revPhone2.Enabled = False
            rfvPhone2.Enabled = False
        Else
            trTel2.Visible = True
            revPhone2.Enabled = True
            rfvPhone2.Enabled = True
        End If
    End Sub

    Private Sub Show3PV(shortPort As Boolean)
        MB.ShowMessage("<b>The customer is " & If(shortPort, String.Empty, "<i>not</i> ") & "eligible for short port. Please use available quota.</b><br /><ul><li>A 3PV transfer is required for this customer in order to complete the port.</li></ul>", _
              "I acknowledge the message above")
        MB.PopupUrl = "https://www.3pv.net/insight/login.aspx"
    End Sub

    Protected Sub btnDslYes_Click(sender As Object, e As EventArgs) Handles btnDSLYes.Click
        _myRB = CType(ViewState("RB"), ResponseBody)
        callUpdateHouse("0000000000", txtHousekey.Text.Trim, _myRB.SPID, Nothing, Nothing)
        Show3PV(False)
        ResetForm()
    End Sub

    Protected Sub btnDslNo_Click(sender As Object, e As EventArgs) Handles btnDSLNo.Click
        _myRB = CType(ViewState("RB"), ResponseBody)
        _myRB = callUpdateHouse(txtPhone.Text.Trim, txtHousekey.Text.Trim, _myRB.SPID, Nothing, Nothing)
        ResetForm()
        Show3PV(_myRB.SetPortFlag = "Y")
        ResetForm()
    End Sub

    Protected Sub btnSubmitAddlInfo_Click(sender As Object, e As EventArgs) Handles btnSubmitAddlInfo.Click
        cvCustAcct.Validate()
        If cvCustAcct.IsValid Then
            _myRB = CType(ViewState("RB"), ResponseBody)
            If ViewState("Phone") <> txtPhone.Text.Trim Then
                btnsubmit_Click(sender, e)
                Exit Sub
            End If
            _myRB = callUpdateHouse(txtPhone.Text.Trim, txtHousekey.Text.Trim, _myRB.SPID, If(String.IsNullOrWhiteSpace(txtAccount.Text), Nothing, txtAccount.Text), If(String.IsNullOrWhiteSpace(txtPIN.Text), Nothing, txtPIN.Text))
            If _myRB.SetPortFlag = "Y" Then
                Show3PV(True)
            Else
                Show3PV(False)
            End If
            ResetForm()
        End If
    End Sub

    Private Sub showDsl()
        pnlAddlInfo.Visible = True
        pnlDSL.Visible = True
        btnsubmit.Visible = False
    End Sub

    Private Sub showAccount()
        ResetAddlInfo()
        pnlAddlInfo.Visible = True
        pnlAccount.Visible = True
        btnsubmit.Visible = False
        btnSubmitAddlInfo.Visible = True
        txtAccount.Text = _myRB.OldProviderAccountNumber
    End Sub

    Private Sub showPin()
        showAccount()
        pnlPIN.Visible = True
        txtPIN.Text = _myRB.OldProviderPIN
    End Sub

    Private Sub ResetAddlInfo()
        btnsubmit.Visible = True
        pnlAddlInfo.Visible = False
        pnlDSL.Visible = False
        pnlAccount.Visible = False
        txtAccount.Text = String.Empty
        pnlPIN.Visible = False
        txtPIN.Text = String.Empty
        btnSubmitAddlInfo.Visible = False
        btnsubmit.Visible = True
    End Sub

    Private Sub ResetForm()
        ResetAddlInfo()
        ViewState.Remove("RB")
        ViewState.Remove("Phone")
        txtHousekey.Text = String.Empty
        txtPhone.Text = String.Empty
        txtPhone2.Text = String.Empty
    End Sub

    Protected Sub cvCustAcct_ServerValidate(source As Object, args As System.Web.UI.WebControls.ServerValidateEventArgs) Handles cvCustAcct.ServerValidate
        args.IsValid = True
        Dim myRegex As New Regex("^(8855(11|31|52|53|75)|123456|111111|222222|333333|444444|555555|666666|777777|888888|999999).*$")
        If myRegex.IsMatch(txtAccount.Text.Trim) Then
            args.IsValid = False
        End If
    End Sub
End Class