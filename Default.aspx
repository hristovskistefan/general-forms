<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Default.aspx.vb" Inherits="GeneralForms._Default" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden; height: 100%; border: 0px; padding: 0px; margin: 0px;">
<head runat="server">
    <%--       <link href="/stylesheets/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/report_style.css" rel="stylesheet" type="text/css" />--%>
    <style type="text/css">
        .VisibleScrollbar
        {
            overflow: scroll !important;
        }
    </style>
    <title>WOW! General Forms</title>
</head>
<body style="overflow: hidden; height: 100%; border: 0px; padding: 0px; margin: 0px;">
    <form id="form1" runat="server" style="height: 100%; border: 0px; padding: 0px; margin: 0px;">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server" />
        <telerik:RadSplitter ID="rsPage" Orientation="Horizontal" runat="server" Width="100%"
            Height="100%" SplitBarsSize="0" ResizeWithBrowserWindow="true">
            <telerik:RadPane ID="TopPane" runat="server" BorderWidth="0" Height="33px" MinHeight="33"
                Scrolling="None" Width="100%" Locked="true">
                <asp:Panel ID="pnlTitle" CssClass="title" Style="border: 0px; padding: 0px; margin: 0px;" runat="server">
                    <div class="titleimage">
                    </div>
                    <div class="titletext">
                        <asp:Label CssClass="DevTitle" ID="lblDevelopment" runat="server" Text=" /* DEVELOPMENT */ " Visible="false"></asp:Label>General Forms
                    </div>
                </asp:Panel>
            </telerik:RadPane>
            <telerik:RadPane ID="BottomPane" runat="server" Scrolling="None" Height="100%">
                <telerik:RadSplitter ID="RadSplitter1" runat="server" Width="100%" BackColor="White"
                    Height="100%" HeightOffset="35">
                    <telerik:RadPane ID="RadPane1" runat="server" Height="100%" Width="200px">
                        <telerik:RadPanelBar ID="pnlbarMenu" runat="server" Width="200px" CausesValidation="False"
                            ExpandMode="SingleExpandedItem">
                            <Items>
                                <telerik:RadPanelItem runat="server" Value="General Forms" Text="General Forms" Expanded="True">
                                    <Items>
                                        <telerik:RadPanelItem runat="server" Text="Credit Reference Letter" Value="CreditReferenceLetter" />
                                        <telerik:RadPanelItem runat="server" Text="Customer Refunds" Value="CustomerRefunds" />
                                        <telerik:RadPanelItem runat="server" Text="DueDate Change" Value="DueDateChange" />
                                        <telerik:RadPanelItem runat="server" Text="Incorrect Bill" Value="IncorrectBill" />
                                        <telerik:RadPanelItem runat="server" Text="Manual Payments" Value="ManualPayments" />
                                        <telerik:RadPanelItem runat="server" Text="Misapplied/Unposted Pmnt" Value="MisappliedOrUnpostedPayment" />
                                        <telerik:RadPanelItem runat="server" Text="Name Change" Value="NameChange" />
                                        <telerik:RadPanelItem runat="server" Text="Rate Breakdown Letter" Value="RateBreakdownLetter" />
                                        <telerik:RadPanelItem runat="server" Text="Replacement Statement" Value="ReplacementStatement" />
                                        <telerik:RadPanelItem runat="server" Text="Cancel / Reschedule" Value="Cancel / Reschedule Form" />
                                        <telerik:RadPanelItem runat="server" Text="Channel Request" Value="Channel Request" />
                                        <telerik:RadPanelItem runat="server" Text="Check Serviceability" Value="Check Serviceability" />
                                        <telerik:RadPanelItem runat="server" Text="Current Phone Ownership" Value="Current Phone Ownership" />
                                        <telerik:RadPanelItem runat="server" Text="Customer Care Suggestion Box" Value="Customer Care Suggestion Box" />
                                        <telerik:RadPanelItem runat="server" Text="Customer Survey" Value="Customer Survey" />
                                        <telerik:RadPanelItem runat="server" Text="Equipment Return" Value="Equipment Return" />
                                        <telerik:RadPanelItem runat="server" Text="FIT" Value="FIT" NavigateUrl="http://wowccc/fits/" Target="_blank" />
                                        <telerik:RadPanelItem runat="server" Text="IVR Payment Report" Value="http://cccweb/IVRReports/IVR_Payment.aspx" />
                                        <telerik:RadPanelItem runat="server" Text="Loss Prevention" Value="Loss Prevention Form" />
                                        <telerik:RadPanelItem runat="server" Text="Online Sales / Referral" Value="Online Sales / Referral" />
                                        <telerik:RadPanelItem runat="server" Text="Paper Fulfillment Request" Value="Paper Fulfillment Request" />
                                        <telerik:RadPanelItem runat="server" Text="Paper Fulfillment Search" Value="Paper Fulfillment Search" />
                                        <telerik:RadPanelItem runat="server" Text="Payment Arrangements" Value="Collections PA" />
                                        <telerik:RadPanelItem runat="server" Text="Project Support Opps" Value="Project Support Opps" />
                                        <telerik:RadPanelItem runat="server" Text="Rate Guarantee Finder" Value="Rate Guarantee Finder" />
                                        <telerik:RadPanelItem runat="server" Text="Seasonal Request" Value="Seasonal Request" />
                                        <telerik:RadPanelItem runat="server" Text="UPS Mailing" Value="UPS Mailing Form" />
                                        <telerik:RadPanelItem runat="server" Text="WOW-A-Friend" Value="WOW-A-Friend Form" NavigateUrl="http://64.233.207.29/ReferralTool/" Target="_blank" />
                                        <telerik:RadPanelItem runat="server" Text="WOW! Phone Inquiry" Value="WOW! Phone Inquiry" />
                                    </Items>
                                </telerik:RadPanelItem>
                            </Items>
                        </telerik:RadPanelBar>
                    </telerik:RadPane>
                    <telerik:RadSplitBar ID="RadSplitBar1" runat="server" CollapseMode="Forward" EnableResize="False" />
                    <telerik:RadPane ID="RadPane2" runat="server" PersistScrollPosition="true">
                    </telerik:RadPane>
                </telerik:RadSplitter>
            </telerik:RadPane>
        </telerik:RadSplitter>
    </form>
</body>
</html>
