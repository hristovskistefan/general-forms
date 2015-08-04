Imports Microsoft.Practices.EnterpriseLibrary.Data
Partial Public Class RateGuaranteeFinder
    Inherits System.Web.UI.Page
    Private _employee As EmployeeService.EmpInstance

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        Loademployeeinfo()
        If Not Page.IsPostBack Then
            rcbRateCodes_Databind()
        End If
    End Sub

    Public Sub rcbRateCodes_Databind()

        Dim db As Database = DatabaseFactory.CreateDatabase("GeneralForms")
        Dim cmd As DbCommand = db.GetSqlStringCommand("SELECT distinct(PkgCode) FROM " & My.Settings.PackageCodeTable & " ORDER BY PkgCode")
        rcbRateCodes.DataSource = db.ExecuteDataSet(cmd).Tables(0)
        rcbRateCodes.DataTextField = "PkgCode"
        rcbRateCodes.DataValueField = "PkgCode"
        rcbRateCodes.DataBind()
        rcbRateCodes.Items.Insert(0, New RadComboBoxItem("Select", ""))

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

    Protected Sub rcbRateCodes_SelectedIndexChanged(ByVal sender As Object, ByVal e As EventArgs) Handles rcbRateCodes.SelectedIndexChanged
        pnlInfo.Visible = False
        pnlPkgCode.Visible = False
        If rcbRateCodes.SelectedValue = "" Then
            Exit Sub
        End If
        Dim db As Database = DatabaseFactory.CreateDatabase("GeneralForms")
        Dim cmd As DbCommand = db.GetSqlStringCommand("SELECT DBID, PkgCode, PkgDescription, GuaranteeDate, CurrentRate, NextYearRate, RateIncrease, CategoryCode, MidMichiganCurrentRate, MidMichiganNextYearRate, MidMichiganIncrease, FortGordonCurrentRate, FortGordonNextYearRate, FortGordonIncrease FROM " & My.Settings.PackageCodeTable & " where PkgCode = '" & rcbRateCodes.SelectedValue & "'")
        Dim ds As DataTable = db.ExecuteDataSet(cmd).Tables(0)
        Dim int As Integer
        Dim reRatesDisp As Boolean = False
        Dim reMIRatesDisp As Boolean = False
        Dim reFGRatesDisp As Boolean = False
        For Each item As DataRow In ds.Rows
            If item("CurrentRate").ToString <> "" Then
                reRatesDisp = True
            End If
            If item("MidMichiganCurrentRate").ToString <> "" Then
                reMIRatesDisp = True
            End If
            If item("FortGordonCurrentRate").ToString <> "" Then
                reFGRatesDisp = True
            End If
        Next

        If reRatesDisp = True Then
            reRates.DataSource = ds
            reRates.DataBind()
            reRates.Visible = True
            lblGuaranteeDate.Visible = True
            lblGuarantee.Visible = True
        Else
            reRates.Visible = False
            lblGuaranteeDate.Visible = False
            lblGuarantee.Visible = False
        End If

        If reMIRatesDisp = True Then
            reMIrates.DataSource = ds
            reMIrates.DataBind()
            reMIrates.Visible = True
            lblGuaranteeDateMidMich.Visible = True
            lblGuaranteeMidMich.Visible = True
        Else
            reMIrates.Visible = False
            lblGuaranteeDateMidMich.Visible = False
            lblGuaranteeMidMich.Visible = False
        End If

        If reFGRatesDisp = True Then
            reFGrates.DataSource = ds
            reFGrates.DataBind()
            reFGrates.Visible = True
            lblGuaranteeDateFG.Visible = True
            lblGuaranteeFG.Visible = True
        Else
            reFGrates.Visible = False
            lblGuaranteeDateFG.Visible = False
            lblGuaranteeFG.Visible = False
        End If

        lblPackageCode.Text = rcbRateCodes.SelectedItem.Text


        For Each Row As DataRow In ds.Rows
            If Row("GuaranteeDate").ToString.Contains("Michigan") Then
                lblGuaranteeDateMidMich.Text = Row("GuaranteeDate").ToString
            ElseIf Row("GuaranteeDate").ToString.Contains("Fort Gordon") Then
                lblGuaranteeDateFG.Text = Row("GuaranteeDate").ToString
            Else
                lblGuaranteeDate.Text = Row("GuaranteeDate").ToString
            End If
        Next


        pnlPkgCode.Visible = True
        pnlInfo.Visible = True
    End Sub
End Class