Imports Telerik.Web.UI
Imports GeneralForms.Security.CheckAccessHelper

Partial Public Class _Default
    Inherits System.Web.UI.Page
    Private _nsazmanConnectionString As String = DatabaseFactory.CreateDatabase("NetSqlAzMan").ConnectionString
    Public AZHelper As Security.CheckAccessHelper
    Private _employee As EmployeeService.EmpInstance
    Private Function isLocal() As Boolean
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

    Private Sub Page_PreInit(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.PreInit
        Response.Cache.SetCacheability(HttpCacheability.NoCache)
        If Not Page.IsPostBack Then
            Session.Remove("isLocal")
        End If
        If Session("isLocal") Is Nothing Then
            Session("isLocal") = isLocal()
        End If
    End Sub

    Private Sub Page_Init(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Init
        AZHelper = New Security.CheckAccessHelper(_nsazmanConnectionString, Request.LogonUserIdentity)
    End Sub

    Protected Sub Page_Load(ByVal sender As Object, ByVal e As System.EventArgs) Handles Me.Load
        ResetAll()
        _employee = GeneralFormsCommon.getEmployee
        If Not Page.IsPostBack Then
            If Request.ServerVariables("SERVER_NAME").Contains("dev") Then
                lblDevelopment.Visible = True
                pnlTitle.CssClass = "DevContainer"
            End If
        End If
        If Not Page.IsPostBack Then
            If Not GenFormsData.ospActive() AndAlso Not AZHelper.CheckAccess(Operation.accessDirect) Then Response.Redirect("/BlockedApplication.aspx")
            If Request.AppRelativeCurrentExecutionFilePath <> "~/Unauthorized.aspx" AndAlso Not _employee.Active Then Response.Redirect("~/Unauthorized.aspx")
            CheckQueryString()
        End If
    End Sub

    Private Sub CheckQueryString()
        If Not IsNothing(Request.QueryString("ContentUrl")) Then
            Me.RadPane2.ContentUrl = Request.QueryString("ContentUrl")
        End If
        If Not IsNothing(Request.QueryString("form")) Then
            Dim myItem As RadPanelItem = Me.pnlbarMenu.Items(0).Items.FindItemByValue(Request.QueryString("form"))
            If Not IsNothing(myItem) Then
                myItem.Selected = True
            End If
            Navigate(Request.QueryString("form"))
        End If
    End Sub

    Protected Sub pnlbarMenu_ItemClick(ByVal sender As System.Object, ByVal e As Telerik.Web.UI.RadPanelBarEventArgs) Handles pnlbarMenu.ItemClick
        Dim itemClicked As RadPanelItem = e.Item
        If itemClicked.Level > 0 Then
            ResetAll()
            Navigate(itemClicked.Value)
        End If
    End Sub

    Private Sub Navigate(ByVal key As String)
        Select Case key
            Case "Account Research"
                Me.RadPane2.ContentUrl = "http://cccweb/LegacyApps/Forms/bill_inquiry.aspx"
            Case "CreditReferenceLetter"
                Me.RadPane2.ContentUrl = "Pages/AR/CreditReferenceLetter.aspx"
            Case "CustomerRefunds"
                Me.RadPane2.ContentUrl = "Pages/AR/CustomerRefunds.aspx"
            Case "DueDateChange"
                Me.RadPane2.ContentUrl = "Pages/AR/DueDateChange.aspx"
            Case "IncorrectBill"
                Me.RadPane2.ContentUrl = "Pages/AR/IncorrectBill.aspx"
            Case "ManualPayments"
                Me.RadPane2.ContentUrl = "Pages/AR/ManualPayments.aspx"
            Case "MisappliedOrUnpostedPayment"
                Me.RadPane2.ContentUrl = "Pages/AR/MisappliedOrUnpostedPayment.aspx"
            Case "NameChange"
                Me.RadPane2.ContentUrl = "Pages/AR/NameChange.aspx"
            Case "RateBreakdownLetter"
                Me.RadPane2.ContentUrl = "Pages/AR/RateBreakdownLetter.aspx"
            Case "ReplacementStatement"
                Me.RadPane2.ContentUrl = "Pages/AR/ReplacementStatement.aspx"
            Case "Cancel / Reschedule Form"
                Me.RadPane2.ContentUrl = "Pages/Reschedule.aspx" ' WAS "http://cccweb/LegacyApps/Forms/resched__.aspx"
            Case "Channel Request"
                Me.RadPane2.ContentUrl = "http://www1.wowway.com/event/module/moduleKeyword/chanreq/"
            Case "Check Serviceability"
                Me.RadPane2.ContentUrl = "http://wowccc/checkservice/checkservice"
            Case "Customer Survey"
                Me.RadPane2.ContentUrl = "http://CCCWeb/LegacyApps/survey/"
            Case "Equipment Return"
                Me.RadPane2.ContentUrl = "/Collections/Pages/EquipmentReturn/InquiryEntry.aspx"
            Case "Henry Ford E-Mail Form"
                Me.RadPane2.ContentUrl = "http://cccweb/LegacyApps/Forms/cl_hford.aspx"
            Case "Online Sales / Referral"
                Me.RadPane2.ContentUrl = "http://cccreporting/oracle3/forms/referral/index.asp"
            Case "Collections PA", "Payment Arrangements"
                Me.RadPane2.ContentUrl = "/Collections/Pages/PaymentArrangements/PA_Entry.aspx"
            Case "Portal Feedback Form"
                Me.RadPane2.ContentUrl = "http://cccweb/LegacyApps/Forms/portalmanage/portal__feedback.aspx"
            Case "Project Support Opps"
                Me.RadPane2.ContentUrl = "http://cccweb/legacyapps/specialproject/"
            Case "Loss Prevention Form"
                Me.RadPane2.ContentUrl = "Pages/LossPrevention.aspx"
            Case "UPS Mailing Form"
                Me.RadPane2.ContentUrl = "Pages/UPSMailing.aspx"
            Case "WOW-A-Friend Form"
                Me.RadPane2.ContentUrl = "http://64.233.207.29/ReferralTool/"
            Case "WOW! Phone Inquiry"
                Me.RadPane2.ContentUrl = "Pages/PhoneInquiry.aspx"
            Case "Seasonal Request"
                Me.RadPane2.ContentUrl = "Pages/SeasonalEntry.aspx"
            Case "Current Phone Ownership"
                Me.RadPane2.ContentUrl = "Pages/PhonePort.aspx"
            Case "Customer Care Suggestion Box"
                Me.RadPane2.ContentUrl = "Pages/WOWSuggestion.aspx"
            Case "Rate Guarantee Finder"
                Me.RadPane2.ContentUrl = "Pages/RateGuaranteeFinder.aspx"
            Case "Paper Fulfillment Request"
                Me.RadPane2.ContentUrl = "/PaperFulfillment/Pages/Request.aspx"
            Case "Paper Fulfillment Search"
                Me.RadPane2.ContentUrl = "/PaperFulfillment/Pages/Search.aspx"
            Case "FIT"
                Me.RadPane2.ContentUrl = "http://wowccc/fits/"
            Case "IVR"
                Me.RadPane2.ContentUrl = "/IVRReports/IVR_Payment.aspx"
        End Select
    End Sub

    Private Sub ResetAll()
        Me.RadPane2.ContentUrl = "Pages/Home.aspx"
        Me.RadPane2.Scrolling = SplitterPaneScrolling.Y
    End Sub

End Class