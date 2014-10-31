<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManualPayments.aspx.vb"
    MaintainScrollPositionOnPostback="true" Inherits="GeneralForms.ARManualPayments" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>General Forms</title>
    <style type="text/css">
        input[readonly] {
            cursor: not-allowed;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Account Research - Manual Payments
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
        <div id="Container-Content" style="font-size: .9em;">
            <asp:Panel ID="pnlmain" runat="server">
                <table class="input" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td align="center" colspan="4">
                            <div class="infobox bolder">
                                <span style="color: Red; font-size: larger; font-style: italic;">IMPORTANT - READ FIRST:</span>
                                Use this form <span style="color: Red; font-size: larger; font-style: italic;">ONLY</span>
                                when advised by RM or a Supervisor.
                            </div>
                            <div style="font-weight: bold;">Do NOT ask the customer for a credit card, checking, or savings account number.</div>
                            <div>See Gooroo for more information on how to use this form.</div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="sectionTitle">
                                Customer Information
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>Account Number:&nbsp;
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtAcct" runat="server" Width="80" MaxLength="8" AutoPostBack="true"></asp:TextBox>&nbsp;<asp:ImageButton
                                ID="ibGo" ValidationGroup="vgAcctInfo" CausesValidation="false" runat="server"
                                ImageUrl="~/images/SearchGo.gif" />
                            <asp:RegularExpressionValidator ID="revAccount" runat="server" Text="X" ControlToValidate="txtAcct"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                                ErrorMessage="The account number entered is not valid" ValidationGroup="vgAcctInfo" />
                            <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="txtAcct"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer Account Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                            <asp:CustomValidator runat="server" ValidationGroup="vgAcctInfo" EnableClientScript="false"
                                ControlToValidate="txtAcct" ErrorMessage="Advise the customer that a Manual Payment request has already been submitted for this WOW! account number.<br />See Gooroo for information about Manual Payments."
                                ID="cvAccount" Display="None" />
                            <asp:TextBox autocomplete="off" ID="txtstate" runat="server" ReadOnly="True" Width="160" Visible="False" />
                            <asp:Label ID="lblDivision" runat="server" ReadOnly="True" Width="1" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:&nbsp;
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtcfname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtcfname"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                        <td>&nbsp;Last Name:&nbsp;
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtclname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtclname"
                                Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAltPhone" Visible="false">
                        <tr>
                            <td>Best Contact Phone Number:&nbsp;
                            </td>
                            <td>
                                <asp:TextBox autocomplete="off" ID="txtmanpayphone" runat="server" Width="100" MaxLength="13"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvManPayPhone" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Alternate Phone Number is Required"
                                    ControlToValidate="txtmanpayphone" />
                                <asp:RegularExpressionValidator ID="revManPayPhone" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Phone Number" ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$"
                                    ControlToValidate="txtmanpayphone" />
                                <asp:CustomValidator runat="server" Text="X" EnableClientScript="false" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ControlToValidate="txtmanpayphone" ErrorMessage="Invalid Phone Number"
                                    ID="cvPhone" />
                            </td>
                        </tr>
                        <tr>
                            <td>Payment Amount Requested:&nbsp;
                            </td>
                            <td>
                                <telerik:RadNumericTextBox ID="payamount" Width="110" MaxLength="8" runat="server" Type="Currency"
                                    EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Button ID="btnmanpay" OnClick="SendIt" runat="server" Width="150" Text="Submit"></asp:Button>
                            </td>
                        </tr>
                    </asp:Panel>
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
                                <td class="red" align="center">Submission Confirmation
                                </td>
                            </tr>
                            <tr>
                                <td align="center">The information you submitted has been sent successfully. Please click the button
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
            <asp:ValidationSummary ID="ValidationSummary" HeaderText="Important:"
                runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Important:"
                runat="server" ValidationGroup="vgAcctInfo" />
        </div>
        <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
