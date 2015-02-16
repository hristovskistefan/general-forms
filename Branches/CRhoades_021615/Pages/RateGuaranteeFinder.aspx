<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateGuaranteeFinder.aspx.vb"
    Inherits="GeneralForms.RateGuaranteeFinder" %>

<%@ Register TagPrefix="user" TagName="MB" Src="~/Controls/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <title>General Forms</title>
    <style type="text/css">
        table.RateInfo {
            width: 200px;
            border-width: 0px;
            border-spacing: 0px;
            border-collapse: collapse;
        }

            table.RateInfo th {
                border-width: 0px;
                padding: 1px;
                text-align: left;
                border-bottom: solid thin black;
            }

            table.RateInfo td {
                text-align: center;
                border-width: thin;
                padding: 1px;
                border-style: solid;
                background-color: white;
            }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server"></telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Rate Guarantee Finder
            </div>
            <div id="Container-Info" class="DataLabel">
                <span class="DataLabel">Date: </span>
                <asp:Label ID="lblhDate" runat="server" CssClass="Data" />
                <span class="DataLabel">Name: </span>
                <asp:Label ID="lblhName" runat="server" CssClass="Data" />
                <span class="DataLabel">ICOMS ID: </span>
                <asp:Label ID="lblhIcomsID" runat="server" CssClass="Data" />
            </div>
        </div>
        <div id="Container-Content">
            <div class="messageInfo" style="width: auto">
                <ul>
                    <li>Single LOB services, premium channels, and equipment<br />
                        are never rate guaranteed regardless of 
                         Code.</li>
                    <li>Any applied Campaigns will only be available for the
                    <br />
                        length selected and expire off the package accordingly.
                    </li>
                    <li>If applicable, the <i>Broadcast TV Fee</i>, <i>Sports Surcharge</i>, and/or
                    <br />
                        <i>Network Line Fee</i> will need to be calculated separately.
                    </li>
                </ul>
            </div>
            <div class="sectionTitle">
                Select Package Code
            </div>
            <telerik:RadComboBox ID="rcbRateCodes" runat="server" Filter="Contains" AutoPostBack="true"></telerik:RadComboBox>
            <asp:Panel runat="server" ID="pnlPkgCode" Visible="False">
                <br />
                <div class="sectionTitle">
                    Package Code
                <asp:Label runat="server" ID="lblPackageCode" />
                </div>
                <br />
                <asp:Label ID="lblGuarantee" runat="server" style="font-weight: bold;">Guarantee Date:</asp:Label>
                <asp:Label runat="server" ID="lblGuaranteeDate" /><br />
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlInfo" Visible="False" Height="314px">
                <asp:Repeater runat="server" ID="reRates">
                    <HeaderTemplate>
                        <table class="RateInfo">
                            <tr>
                                <th colspan="3">Rate Information:
                                </th>                
                            </tr>
                            <tr>
                                <td>2014
                                </td>
                                <td>2015
                                </td>
                                <td>Increase
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("CurrentRate", "{0:C}")%>
                            </td>
                            <td>
                                <%#Eval("NextYearRate", "{0:C}")%>
                            </td>
                            <td>
                                <%#Eval("RateIncrease", "{0:C}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
                <br />
                <asp:Label ID="lblGuaranteeMidMich" runat="server" style="font-weight: bold;">Guarantee Date Mid-Michigan:</asp:Label>
                <asp:Label runat="server" ID="lblGuaranteeDateMidMich" /><br />
                <asp:Repeater ID="reMIrates" runat="server">
                    <HeaderTemplate>
                        <table class="RateInfo">
                            <tr>
                                <th colspan="3">
                                    Rate Information:
                                </th>
                            </tr>
                            <tr>
                                <td>2014
                                </td>
                                <td>2015
                                </td>
                                <td>Increase
                                </td>
                            </tr>
                    </HeaderTemplate>
                    <ItemTemplate>
                        <tr>
                            <td>
                                <%#Eval("MidMichiganCurrentRate", "{0:C}")%>
                            </td>
                            <td>
                                <%#Eval("MidMichiganNextYearRate", "{0:C}")%>
                            </td>
                            <td>
                                <%#Eval("MidMichiganIncrease", "{0:C}")%>
                            </td>
                        </tr>
                    </ItemTemplate>
                    <FooterTemplate></table></FooterTemplate>
                </asp:Repeater>
            </asp:Panel>
        </div>
    </form>
</body>
</html>
