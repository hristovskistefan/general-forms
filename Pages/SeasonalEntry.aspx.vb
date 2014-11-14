Imports Microsoft.Practices.EnterpriseLibrary.Data
Imports System.Net.Mail


Partial Public Class SeasonalEntry
    Inherits System.Web.UI.Page
    Private _employee As EmployeeService.EmpInstance
    Private _myCustomer As CustomerService.Cust

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        LoadEmployeeInfo()
        If Not Page.IsPostBack Then

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


End Class