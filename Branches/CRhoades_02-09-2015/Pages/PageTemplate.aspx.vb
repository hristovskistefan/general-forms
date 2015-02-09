Public Class PageTemplate
    Inherits System.Web.UI.Page

    Private _employee As EmployeeService.EmpInstance

    Private Sub Page_Load(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles MyBase.Load
        LoadEmployeeInfo()
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

End Class
