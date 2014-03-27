<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="RateBreakdownLetter.aspx.vb"
    MaintainScrollPositionOnPostback="true" Inherits="GeneralForms.ARRateBreakdownLetter" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>General Forms</title>
</head>
<body>
    <form id="Form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="Container-Heading clearfix">
        <div id="Container-Title">
            Account Research - Rate Breakdown Letter</div>
        <div id="Container-Info" class="DataLabel">
            <span class="DataLabel">Date: </span>
            <asp:Label ID="lblhDate" runat="server" CssClass="Data" />
            <span class="DataLabel">Name: </span>
            <asp:Label ID="lblhName" runat="server" CssClass="Data" />
            <span class="DataLabel">ICOMS ID: </span>
            <asp:Label ID="lblhIcomsID" runat="server" CssClass="Data" />
        </div>
    </div>
    <div id="Container-Content" style="font-size: .9em;">
        <asp:Panel ID="pnlmain" runat="server">
            <table class="input" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td align="center" colspan="4">
                        <div class="infobox bolder">
                            Use if the customer subscribes to more than one line of business and requests a
                            rate breakdown letter. Requests for rate breakdown letters can only be mailed to
                            customers and only be requested for the current month.
                        </div>
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <div class="sectionTitle">
                            Customer Information</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Account Number:
                    </td>
                    <td>
                        <asp:TextBox ID="txtAcct" runat="server" Width="160" MaxLength="8" AutoPostBack="true"></asp:TextBox><asp:ImageButton
                            ID="ibGo" ValidationGroup="vgAcctInfo" CausesValidation="false" runat="server"
                            ImageUrl="~/images/SearchGo.gif" />
                        <asp:RegularExpressionValidator ID="revAccount" runat="server" Text="X" ControlToValidate="txtAcct"
                            Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                            ErrorMessage="The account number entered is not valid" ValidationGroup="vgAcctInfo" />
                        <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="txtAcct"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer Account Required"
                            EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        <asp:CustomValidator runat="server" ValidationGroup="vgAcctInfo" EnableClientScript="false"
                            ControlToValidate="txtAcct" ErrorMessage="This account has already been submitted for review on this issue type and has an active issue open for review. You may not submit another request for this account and issue type until the current issue has been resolved."
                            ID="cvAccount" Display="None" />
                        <asp:TextBox ID="txtstate" runat="server" ReadOnly="True" Width="160" Visible="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtcfname" runat="server" Width="160"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtcfname"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                            EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                    </td>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtclname" runat="server" Width="160"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtclname"
                            Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                            EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlItemLetter" runat="server">
            <table class="input" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td>
                        <span class="bolder">Comments:</span><br />
                        <asp:TextBox ID="txtItemLetter" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                            MaxLength="500"></asp:TextBox>
                        <br />
                    </td>
                </tr>
                <tr>
                    <td>
                        <asp:Button ID="btnItemLetter" OnClick="SendIt" runat="server" Width="150" Text="Submit">
                        </asp:Button>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlerror" runat="server">
            <asp:Label Font-Size="11pt" ID="lblerror" runat="server" ForeColor="red" Font-Bold="True" />
        </asp:Panel>
        <asp:Panel ID="pnlthx" runat="server">
            <center>
                <table class="input" cellspacing="0" cellpadding="2" width="400">
                    <tbody>
                        <tr>
                            <td class="red" align="center">
                                Submission Confirmation
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                The information you submitted has been sent successfully. Please click the button
                                below to return.
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btnthx" runat="server" Width="160" Text="Return"></asp:Button>
                            </td>
                        </tr>
                    </tbody>
                </table>
            </center>
        </asp:Panel>
        <asp:ValidationSummary ID="ValidationSummary" HeaderText="Please correct the following:"
            runat="server" />
        <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Please correct the following:"
            runat="server" ValidationGroup="vgAcctInfo" />
    </div>
    <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
