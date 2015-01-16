<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="IncorrectBill.aspx.vb" MaintainScrollPositionOnPostback="true"
    Inherits="GeneralForms.ARIncorrectBill" %>

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
                Account Research - Incorrect Bill
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
                                Use when a ledger appears to have incorrect charges or debits on the account. This
                            may also be used for billing disputes involving credits and debits.<br />
                                <br />
                                <span style="color: Red; font-size: larger; font-style: italic;">All inquiries must
                                be approved by your Supervisor prior to submission.</span>
                                <br />
                                Inquiries that are within the CCRs scope of customer assistance will be returned
                            to the Supervisor for resolution.
                            </div>
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
                        <td style="width: 120px;">Account Number:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtAcct" runat="server" Width="80" MaxLength="8" AutoPostBack="true"></asp:TextBox><asp:ImageButton
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
                            <asp:TextBox autocomplete="off" ID="txtstate" runat="server" ReadOnly="True" Width="160" Visible="False" />
                            <asp:Label ID="lblDivision" runat="server" ReadOnly="True" Width="1" Visible="False"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlincbill" runat="server">
                <table class="input" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td style="width: 120px;">First Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtcfname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtcfname"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                        <td style="width: 164px;">Last Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtclname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtclname"
                                Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                    <tr>
                        <td style="width: 120px;" class="bolder">Which Month:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropincbillmonth" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="Select One" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator31" runat="server" Text="X"
                                ErrorMessage="Month is required." Font-Bold="true" Font-Size="Medium" Display="Dynamic"
                                ControlToValidate="dropincbillmonth" InitialValue="Select One" />
                        </td>
                        <td style="width: 164px;" class="bolder">Best Contact Phone Number:</td>
                        <td>
                            <asp:TextBox ID="txtPhoneNumber" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvPhoneNumber" ErrorMessage="A valid phone number is required." ControlToValidate="txtPhoneNumber" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revPhoneNumber" runat="server" Text="X" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid phone number."
                                ValidationExpression="^[- .]?(((?!\(000\))(?!\(111\))(?!\(222\))(?!\(333\))(?!\(444\))(?!\(555\))(?!\(666\))(?!\(777\))(?!\(900\))\(\d{3}\) ?)|(?!000)(?!111)(?!222)(?!333)(?!444)(?!555)(?!666)(?!777)(?!900)([2-9]\d{2}\)|[2-9]\d{2}))[- .]?\d{3}[- .]?\d{4}$"
                                ControlToValidate="txtPhoneNumber" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="bolder">What specifically is wrong with the bill:
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="X"
                            ErrorMessage="Reason required." Font-Bold="true" Font-Size="Medium" Display="Dynamic"
                            ControlToValidate="txtspecs" /><br />
                            <asp:TextBox autocomplete="off" ID="txtspecs" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                MaxLength="499"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="bolder">Comments:<br />
                            <asp:TextBox autocomplete="off" ID="txtincbillcomm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnincbillsend" OnClick="SendIt" runat="server" Width="150" Text="Submit"></asp:Button>
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
            <asp:ValidationSummary ID="ValidationSummary" HeaderText="Please correct the following:"
                runat="server" />
            <asp:ValidationSummary ID="ValidationSummary1" HeaderText="Please correct the following:"
                runat="server" ValidationGroup="vgAcctInfo" />
        </div>
        <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
