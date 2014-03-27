Imports System.Data
Imports System.Data.SqlClient
Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Net.Mail
Imports System.Linq

Partial Class UPSMailingForm
    Inherits System.Web.UI.Page
    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust
    Private _division As Integer

    Private _suplist As ArrayList
    Private _i As Integer
    Private _user, _acct, _sql As String
    Private _mSubject, _mBody, _mRecipient As String
    Private _prin As String
    Private _sqlrdr As SqlDataReader

#Region " Web Form Designer Generated Code "

    'This call is required by the Web Form Designer.
    <System.Diagnostics.DebuggerStepThrough()> Private Sub InitializeComponent()

    End Sub

    'NOTE: The following placeholder declaration is required by the Web Form Designer.
    'Do not delete or move it.
    Private _designerPlaceholderDeclaration As System.Object

    Private Sub Page_Init(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Init
        'CODEGEN: This method call is required by the Web Form Designer
        'Do not modify it using the code editor.
        InitializeComponent()
    End Sub

#End Region


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

        Dim myCustomer As CustomerService.Cust
        Using customerClient As New CustomerService.CustomerManagementClient
            myCustomer = customerClient.getByCustomerID(tempAccount)
        End Using
        If myCustomer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Exit Sub
        End If
        If IsNothing(myCustomer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            txtLName.Text = String.Empty
            txtFName.Text = String.Empty
            Exit Sub
        End If
        txtLName.Text = myCustomer.LName
        txtFName.Text = myCustomer.FName
        txtaddy.Text = myCustomer.Address.Addr1 & " " & myCustomer.Address.Addr2
        dropstate.SelectedIndex = dropstate.Items.IndexOf(dropstate.Items.FindByValue(myCustomer.Address.State))
        GetCity(New Object, New System.EventArgs)
        dropcity.SelectedIndex = dropcity.Items.IndexOf(dropcity.Items.FindByValue(myCustomer.Address.City.ToUpper))
        If dropcity.SelectedIndex = 0 Then
            Dim matchedcity As String = String.Empty
            Dim matchCity As String = myCustomer.Address.City.ToUpper.Trim
            For x As Integer = matchCity.Length To 6 Step -1
                For Each city As ListItem In dropcity.Items
                    If city.Value.Length >= x AndAlso city.Value.Substring(0, x) = matchCity.Substring(0, x) Then
                        matchedcity = city.Value
                        Exit For
                    End If
                Next
                If Not String.IsNullOrEmpty(matchedcity) Then Exit For
            Next
            If Not String.IsNullOrEmpty(matchedcity) Then
                dropcity.SelectedIndex = dropcity.Items.IndexOf(dropcity.Items.FindByValue(matchedcity))
            End If
        End If
        GetZip(New Object, New System.EventArgs)
        dropzip.SelectedIndex = dropzip.Items.IndexOf(dropzip.Items.FindByValue(myCustomer.Address.Zip))
        _division = myCustomer.Address.Division
    End Sub

    Protected Sub txtAcct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
            Me.lblerr.Visible = False
            Me.pnlout.Visible = False
            Me.pnlthx.Visible = False
            bindStates()
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

    Public Sub GetCity(ByVal o As System.Object, ByVal e As System.EventArgs) Handles dropstate.SelectedIndexChanged
        Select Case Me.dropstate.SelectedItem.Value
            Case "Select One"
                Me.dropcity.Items.Clear()
                Me.dropcity.Enabled = False
                Me.dropzip.Items.Clear()
                Me.dropzip.Enabled = False
            Case Else
                dropcity.DataSource = Nothing
                dropcity.DataBind()
                Dim db As Database = DatabaseFactory.CreateDatabase("CheckService")
                Dim cmd As DbCommand = db.GetSqlStringCommand("SELECT DISTINCT(City) FROM CityStateZip WHERE State = '" & dropstate.SelectedItem.Value & "' ORDER BY City ASC")
                dropcity.DataSource = db.ExecuteDataSet(cmd).Tables(0)
                dropcity.DataTextField = "City"
                dropcity.DataValueField = "City"
                dropcity.DataBind()
                dropcity.Items.Insert(0, "Select One")
                Me.dropcity.Enabled = True
        End Select
    End Sub

    Public Sub GetZip(ByVal o As System.Object, ByVal e As System.EventArgs) Handles dropcity.SelectedIndexChanged
        Select Case Me.dropcity.SelectedItem.Value
            Case "Select One"
                Me.dropzip.Items.Clear()
                Me.dropzip.Enabled = False
            Case Else
                dropzip.DataSource = Nothing
                dropzip.DataBind()
                Dim db As Database = DatabaseFactory.CreateDatabase("CheckService")
                Dim cmd As DbCommand = db.GetSqlStringCommand("SELECT DISTINCT(ZipCode) FROM CityStateZip WHERE City = '" & dropcity.SelectedItem.Value & "' ORDER BY ZipCode ASC")
                dropzip.DataSource = db.ExecuteDataSet(cmd).Tables(0)
                dropzip.DataTextField = "ZipCode"
                dropzip.DataBind()
                dropzip.Items.Insert(0, "Select One")
                Me.dropzip.Enabled = True
        End Select
    End Sub

    Public Sub GetPanel(ByVal o As Object, ByVal e As EventArgs) Handles chkmoved.CheckedChanged
        Select Case Me.chkmoved.Checked
            Case True
                Me.pnlin.Visible = False
                Me.pnlout.Visible = True
            Case False
                Me.pnlin.Visible = True
                Me.pnlout.Visible = False
        End Select
    End Sub

    Public Sub SendIt(ByVal o As Object, ByVal e As EventArgs) Handles btnsend.Click
        If Page.IsValid Then
            Try
                Dim myCustomer As CustomerService.Cust
                Using customerClient As New CustomerService.CustomerManagementClient
                    myCustomer = customerClient.getByCustomerID(txtAcct.Text)
                End Using
                _division = myCustomer.Address.Division

                Dim address, city, state, zip As String
                address = If(Me.chkmoved.Checked, Me.txtaddyout.Text, Me.txtaddy.Text)
                city = If(Me.chkmoved.Checked, Me.txtcity.Text, Me.dropcity.SelectedItem.Value)
                state = If(Me.chkmoved.Checked, Me.txtstate.Text, Me.dropstate.SelectedItem.Value)
                zip = If(Me.chkmoved.Checked, Me.txtzip.Text, Me.dropzip.SelectedItem.Value)

                Dim dbFormCollection As Database = DatabaseFactory.CreateDatabase("Form_Collection")
                Me._sql = "INSERT INTO UPS (DateSub,CCRUser,CName,AcctNum,PhoneNum,Address,State,City,Zip,"
                Me._sql = Me._sql & "AR,DR,DVRR,HDR,HDDVR,CM,PM,CCards,DTA,UTVGateway,UTVMediaPlayer) VALUES "
                Me._sql = Me._sql & "(@date,@user,@cust,@acct,@phn,@addy,@state,@city,@zip,"
                Me._sql = Me._sql & "@ar,@dr,@dvrr,@hdr,@hddvr,@cm,@pm,@cc,@DTA,@UTVG,@UTVM)"
                Dim cmdInsert As DbCommand = dbFormCollection.GetSqlStringCommand(Me._sql)
                dbFormCollection.AddInParameter(cmdInsert, "@date", DbType.DateTime, Date.Now)
                dbFormCollection.AddInParameter(cmdInsert, "@user", DbType.String, Session("AuthUser"))
                dbFormCollection.AddInParameter(cmdInsert, "@cust", DbType.String, Me.txtFName.Text)
                dbFormCollection.AddInParameter(cmdInsert, "@acct", DbType.String, Me.txtAcct.Text)
                dbFormCollection.AddInParameter(cmdInsert, "@phn", DbType.String, Me.txtphone.Text)
                dbFormCollection.AddInParameter(cmdInsert, "@addy", DbType.String, address)
                dbFormCollection.AddInParameter(cmdInsert, "@state", DbType.String, state)
                dbFormCollection.AddInParameter(cmdInsert, "@city", DbType.String, city)
                dbFormCollection.AddInParameter(cmdInsert, "@zip", DbType.String, zip)
                dbFormCollection.AddInParameter(cmdInsert, "@ar", DbType.Int32, CInt(Me.dropana.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@dr", DbType.Int32, CInt(Me.dropdigi.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@dvrr", DbType.Int32, CInt(Me.dropdvr.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@hdr", DbType.Int32, CInt(Me.drophd.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@hddvr", DbType.Int32, CInt(Me.drophddvr.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@cm", DbType.Int32, CInt(Me.dropcable.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@pm", DbType.Int32, CInt(Me.dropphone.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@cc", DbType.Int32, CInt(Me.dropccard.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@DTA", DbType.Int32, CInt(Me.ddlDTA.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@UTVG", DbType.Int32, CInt(Me.ddlUTVGateway.SelectedItem.Value))
                dbFormCollection.AddInParameter(cmdInsert, "@UTVM", DbType.Int32, CInt(Me.ddlUTVMedia.SelectedItem.Value))

                Me._mBody = "New UPS Mailing Request" & vbCrLf & _
                            "************ CUSTOMER INFORMATION ************" & vbCrLf & _
                            "**********************************************" & vbCrLf & _
                            "**    Name:" & vbTab & vbTab & vbTab & vbTab & Me.txtFName.Text.ToUpper & " " & Me.txtLName.Text.ToUpper & vbCrLf & _
                            "**    Account #:" & vbTab & vbTab & vbTab & Me.txtAcct.Text & vbCrLf & _
                            "**    Phone #:" & vbTab & vbTab & vbTab & Me.txtphone.Text & vbCrLf & _
                            "**    Address:" & vbTab & vbTab & vbTab & address.ToUpper & vbCrLf & _
                            "**    City:" & vbTab & vbTab & vbTab & vbTab & city.ToUpper & vbCrLf & _
                            "**    State:" & vbTab & vbTab & vbTab & state.ToUpper & vbCrLf & _
                            "**    Zip:" & vbTab & vbTab & vbTab & vbTab & zip & vbCrLf & _
                            "**********************************************" & vbCrLf & _
                            "************ EQUIPMENT INFORMATION ***********" & vbCrLf & _
                            "**********************************************" & vbCrLf & _
                            "**    Analog Receivers:" & vbTab & vbTab & Me.dropana.SelectedItem.Value & vbCrLf & _
                            "**    Digital Receivers:" & vbTab & Me.dropdigi.SelectedItem.Value & vbCrLf & _
                            "**    DVR Receivers:" & vbTab & vbTab & Me.dropdvr.SelectedItem.Value & vbCrLf & _
                            "**    HD Receivers:" & vbTab & vbTab & Me.drophd.SelectedItem.Value & vbCrLf & _
                            "**    HD DVR Receivers:" & vbTab & vbTab & Me.drophddvr.SelectedItem.Value & vbCrLf & _
                            "**    DTA Receivers:" & vbTab & vbTab & Me.ddlDTA.SelectedItem.Value & vbCrLf & _
                            "**    Cable Modems:" & vbTab & vbTab & Me.dropcable.SelectedItem.Value & vbCrLf & _
                            "**    Phone Modems:" & vbTab & vbTab & Me.dropphone.SelectedItem.Value & vbCrLf & _
                            "**    Cable Cards:" & vbTab & vbTab & Me.dropccard.SelectedItem.Value & vbCrLf & _
                            "**    Ultra TV Gateways:" & vbTab & vbTab & Me.ddlUTVGateway.SelectedItem.Value & vbCrLf & _
                            "**    Ultra TV Media Players:" & vbTab & vbTab & Me.ddlUTVMedia.SelectedItem.Value & vbCrLf & _
                            "**********************************************" & vbCrLf & _
                            "**********************************************"
                dbFormCollection.ExecuteNonQuery(cmdInsert)

                'GET E-MAIL STUFF PREPARED

                Dim mailMsg As New MailMessage()
                mailMsg.IsBodyHtml = False
                mailMsg.From = New MailAddress(_employee.Email)
                mailMsg.Subject = "UPS Mailing Request"
                mailMsg.Body = _mBody

                mailMsg.To.Clear()

                Dim icomsContext As ICOMSEntities = New ICOMSEntities
                Dim emailAddress As String = (From dl As DivisionLookup In icomsContext.DivisionLookups Where dl.Division = _division Select dl.UPS_EmailAddress).FirstOrDefault
                mailMsg.To.Add(emailAddress)

                ' Dim smtp As SmtpClient = New SmtpClient
                EmailProxy.Send(mailMsg)

                Me.pnlthx.Visible = True
                Me.pnlmain.Visible = False
            Catch ex As Exception
                Response.Write(ex.Message & "<br />" & ex.StackTrace)
            End Try
        End If
    End Sub

    Private Sub Click_Continue(ByVal o As Object, ByVal e As EventArgs) Handles btncont.Click
        Response.Redirect("UPSMailing.aspx")
    End Sub

    Private Sub bindStates()
        Using x As New ICOMSEntities
            Dim bla As List(Of MyLookup) = (From f In x.Franchises Select New MyLookup With {.Name = f.State, .Value = f.StateAbbr}).Distinct().OrderBy(Function(o) o.Name).ToList
            dropstate.DataSource = bla
            dropstate.DataTextField = "name"
            dropstate.DataValueField = "value"
            dropstate.DataBind()
            dropstate.Items.Insert(0, New ListItem("Select One", ""))
        End Using
    End Sub

    Friend Class MyLookup
        Public Property Value As String
        Public Property Name As String
        Public Sub New()
        End Sub
    End Class
End Class