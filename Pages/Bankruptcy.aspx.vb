Imports System.Data

Public Class Bankruptcy
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance
    Private _customer As CustomerService.Cust
    Private _division As Integer

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
        If DateAndTime.Now > CDate("08/18/2010 09:00:00.000") Then
            pnlMain.Visible = False
            pnlNoLongerUsed.Visible = True
        End If
        'Put user code to initialize the page here
        If Not Page.IsPostBack Then
            Me.pnlThanks.Visible = False
            Me.pnlForm.Visible = False
            revAccount.ValidationExpression = GeneralFormsCommon.buildAccountValidatorExpression
        End If
    End Sub

    Public Sub btnSubmit_Click(ByVal o As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand = db.GetSqlStringCommand("INSERT INTO Bankruptcy (DateSub,CCRName,CCRUser,CCRSup,CFName,CLName,AcctNum,CaseNum,ChapterFiled,StateFiled,FaxDoc,QuestBalPay,QuestDisco,QuestRetService,ContactAtty,AttyName,AttyPhNum,CCRComm,Status) VALUES " _
             & "(@DateSub,@CCRName,@CCRUser,@CCRSup,@CFName,@CLName,@AcctNum,@CaseNum,@ChapterFiled,@StateFiled,@FaxDoc,@QuestBalPay,@QuestDisco,@QuestRetService,@ContactAtty,@AttyName,@AttyPhNum,@CCRComm,@Status)")

            db.AddInParameter(cmd, "@DateSub", DbType.DateTime, Date.Now)
            db.AddInParameter(cmd, "@CCRName", DbType.String, _employee.FullNameFirstlast)
            db.AddInParameter(cmd, "@CCRUser", DbType.String, _employee.NTLogin)
            db.AddInParameter(cmd, "@CCRSup", DbType.String, _employee.SupNameFirstLast)
            db.AddInParameter(cmd, "@CFName", DbType.String, Me.txtCFname.Text)
            db.AddInParameter(cmd, "@CLName", DbType.String, Me.txtCLname.Text)
            db.AddInParameter(cmd, "@AcctNum", DbType.String, Me.txtAcct.Text)
            db.AddInParameter(cmd, "@CaseNum", DbType.String, Me.txtCaseNumber.Text)
            db.AddInParameter(cmd, "@ChapterFiled", DbType.String, Me.ddlChapter.SelectedItem.Value)
            db.AddInParameter(cmd, "@StateFiled", DbType.String, Me.ddlState.SelectedItem.Value)
            db.AddInParameter(cmd, "@FaxDoc", DbType.String, Me.ddlFaxingDocumentation.SelectedItem.Value)
            db.AddInParameter(cmd, "@QuestBalPay", DbType.String, Me.ddlQuestAfter.SelectedItem.Value)
            db.AddInParameter(cmd, "@QuestDisco", DbType.String, Me.ddlDisconnected.SelectedItem.Value)
            db.AddInParameter(cmd, "@QuestRetService", DbType.String, Me.ddlRetainService.SelectedItem.Value)
            db.AddInParameter(cmd, "@ContactAtty", DbType.String, Me.ddlAttorney.SelectedItem.Value)
            db.AddInParameter(cmd, "@AttyName", DbType.String, If(Me.ddlAttorney.SelectedItem.Value = "Yes", Me.txtAttorneyName.Text, "N/A"))
            db.AddInParameter(cmd, "@AttyPhNum", DbType.String, If(Me.ddlAttorney.SelectedItem.Value = "Yes", Me.txtAttorneyNumber.Text, "N/A"))
            db.AddInParameter(cmd, "@CCRComm", DbType.String, Me.txtComments.Text)
            db.AddInParameter(cmd, "@Status", DbType.String, "0")
            db.ExecuteNonQuery(cmd)

            Me.pnlThanks.Visible = True
            Me.pnlForm.Visible = False
            Me.pnlMain.Visible = False

        Catch ex As Exception
            MB.ShowError(ex)
        End Try
    End Sub

    Public Sub btnReturn_Click(ByVal o As Object, ByVal e As EventArgs) Handles btnReturn.Click
        Response.Redirect("./bankruptcy.aspx")
    End Sub

    Public Sub btnContinue_Click(ByVal o As Object, ByVal e As EventArgs) Handles btnContinue.Click
        Me.btnContinue.Visible = False
        Me.pnlForm.Visible = True
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
            Me.btnContinue.Visible = True
            Me.pnlForm.Visible = False
            Exit Sub
        End If

        Using customerClient As New CustomerService.CustomerManagementClient
            _customer = customerClient.getByCustomerID(tempAccount)
        End Using
        If _customer Is Nothing Then
            Me.MB.ShowMessage("Lookup timed out.")
            Me.btnContinue.Visible = True
            Me.pnlForm.Visible = False
            Exit Sub
        End If
        If IsNothing(_customer.IcomsCustomerID) Then
            Me.MB.ShowMessage("Lookup returned nothing.")
            txtCLname.Text = String.Empty
            txtCFname.Text = String.Empty
            Me.btnContinue.Visible = True
            Me.pnlForm.Visible = False
            Exit Sub
        End If
        txtCLname.Text = _customer.LName
        txtCFname.Text = _customer.FName
        ddlState.ClearSelection()
        ddlState.Items.FindByValue(_customer.Address.State).Selected = True
        Me.btnContinue.Visible = False
        Me.pnlForm.Visible = True
        _division = _customer.Address.Division
    End Sub

    Protected Sub ddlAttorney_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles ddlAttorney.SelectedIndexChanged
        If ddlAttorney.SelectedValue = "Yes" Then
            rfvAttorneyNumber.Enabled = True
            rfvAttorneyName.Enabled = True
        Else
            rfvAttorneyNumber.Enabled = False
            rfvAttorneyName.Enabled = False
        End If
    End Sub

    Protected Sub txtAcct_TextChanged(ByVal sender As Object, ByVal e As EventArgs) Handles txtAcct.TextChanged
        ibGo_Click(sender, New ImageClickEventArgs(0, 0))
    End Sub

End Class
