Public Class Reschedule
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then
            rdpJobDate.MinDate = Date.Now
            rdpJobDate.SelectedDate = Date.Now
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

    Public Sub btnSubmit_Click(ByVal o As Object, ByVal e As EventArgs) Handles btnSubmit.Click
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("IL-SVR-DB02_CCCToTech")
            Dim cmd As DbCommand = db.GetSqlStringCommand("INSERT INTO MESSAGE (APP_USER,ENTRY_TIME,TECH_ID,JOB_DATE,JOB_NUMBER,NEW_JOB_STATUS,MESSAGE_SENT_TIME) VALUES (@APP_USER,@ENTRY_TIME,@TECH_ID,@JOB_DATE,@JOB_NUMBER,@NEW_JOB_STATUS,GETDATE())")

            db.AddInParameter(cmd, "@APP_USER", DbType.String, _employee.NTLogin)
            db.AddInParameter(cmd, "@ENTRY_TIME", DbType.String, Date.Now.ToString("MMM dd yyyy"))
            db.AddInParameter(cmd, "@TECH_ID", DbType.String, Me.txtTechNumber.Text)
            db.AddInParameter(cmd, "@JOB_DATE", DbType.DateTime, Me.rdpJobDate.SelectedDate)
            db.AddInParameter(cmd, "@JOB_NUMBER", DbType.String, Me.txtWONumber.Text)
            db.AddInParameter(cmd, "@NEW_JOB_STATUS", DbType.String, Me.ddlCancelReschedule.SelectedItem.Value)
            db.ExecuteNonQuery(cmd)

            Me.MB.ShowMessage("Your submission was received without error. Thank you!")
            Call Reset()
        Catch ex As Exception
            MB.ShowError(ex)
        End Try

    End Sub

    Public Sub Reset()
        Me.rdpJobDate.Clear()
        Me.txtWONumber.Text = ""
        Me.ddlCancelReschedule.ClearSelection()
        Me.txtTechNumber.Text = ""
    End Sub

End Class


