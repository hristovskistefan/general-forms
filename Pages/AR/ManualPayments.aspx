<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManualPayments.aspx.vb"
    MaintainScrollPositionOnPostback="true" Inherits="GeneralForms.ARManualPayments" %>

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
                        <td>Account Number:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAcct" runat="server" Width="80" MaxLength="8" AutoPostBack="true"></asp:TextBox><asp:ImageButton
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
                            <asp:Label ID="lblDivision" runat="server" ReadOnly="True" Width="1" Visible="False"></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtcfname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtcfname"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtclname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtclname"
                                Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                    <asp:Panel runat="server" ID="pnlAltPhone" Visible="false">
                        <tr>
                            <td>Alternate Phone Number:
                            </td>
                            <td>
                                <asp:TextBox ID="txtmanpayphone" runat="server" Width="160" MaxLength="10"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvManPayPhone" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Alternate Phone Number is Required"
                                    ControlToValidate="txtmanpayphone" />
                                <asp:RegularExpressionValidator ID="revManPayPhone" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Phone Number" ValidationExpression="^\d{10}$"
                                    ControlToValidate="txtmanpayphone" />
                                <asp:CustomValidator runat="server" Text="X" EnableClientScript="false" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ControlToValidate="txtmanpayphone" ErrorMessage="Invalid Phone Number"
                                    ID="cvPhone" />
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlmanual" runat="server">
                <table>
                    <tr>
                        <td colspan="2">
                            <asp:RadioButtonList ID="rblCheckCredit" runat="server" RepeatDirection="Horizontal"
                                AutoPostBack="True">
                                <asp:ListItem Value="Checking" Selected="True"></asp:ListItem>
                                <asp:ListItem Value="Savings"></asp:ListItem>
                                <asp:ListItem Value="Credit Card"></asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                    <!--
                    <asp:Panel runat="server" ID="pnlCCard" Visible="False">
                        <tr>
                            <td>Credit Card Type:
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlCCard" runat="server">
                                    <asp:ListItem Value="MasterCard"></asp:ListItem>
                                    <asp:ListItem Value="Visa"></asp:ListItem>
                                    <asp:ListItem Value="Discover"></asp:ListItem>
                                </asp:DropDownList>
                            </td>
                        </tr>
                        <tr>
                            <td style="width: 200px;">Credit Card Number:
                            </td>
                            <td style="vertical-align: top;">
                                <telerik:RadMaskedTextBox ID="txtCCardNum" runat="server" Mask="#### #### #### ####"></telerik:RadMaskedTextBox>
                                <%--<asp:TextBox ID="txtCCardNum" runat="server" Width="160" MaxLength="16"></asp:TextBox>
                                --%><asp:RequiredFieldValidator ID="Requiredfieldvalidator36" runat="server"
                                    ErrorMessage="Credit Card Number is required." Text="X"
                                    Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtCCardNum" />
                                <asp:CustomValidator runat="server" Text="X" EnableClientScript="false" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ControlToValidate="txtCCardNum" ErrorMessage="Invalid Credit Card Number"
                                    ID="cvCreditCard" />
                                
                                
                            </td>

                        </tr>
                        <tr>
                            <td>Expiration Date (MM/YYYY):
                            </td>
                            <td>
                                <asp:TextBox ID="txtExpDate" Width="100" runat="server" />
                                <asp:RequiredFieldValidator runat="server" ID="Requiredfieldvalidator37" ControlToValidate="txtExpDate"
                                    ErrorMessage="Expiration Date is required." Text="X" Font-Bold="true" Font-Size="Medium"
                                    Display="Dynamic" />
                                <asp:RegularExpressionValidator runat="server" ID="Regularexpressionvalidator4" ControlToValidate="txtExpDate"
                                    ValidationExpression="\d{2}/\d{4}" ErrorMessage="Expiration Date must be in MM/YYYY format."
                                    Text="X" Font-Bold="true" Font-Size="Medium" Display="Dynamic" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel runat="server" ID="pnlBnkAcct" Visible="True">
                        <tr>
                            <td>Bank Account Number:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtBank" />
                                <asp:RequiredFieldValidator runat="server" ID="rfvtxtBank" ControlToValidate="txtBank"
                                    ErrorMessage="Bank Account Number is required." Text="X" Font-Bold="true" Font-Size="Medium"
                                    Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>Routing Number:
                            </td>
                            <td>
                                <asp:TextBox runat="server" ID="txtRTN2" MaxLength="9"></asp:TextBox>
                                <asp:RequiredFieldValidator ID="rfvRTN2" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Routing Number Required" ControlToValidate="txtRTN2" />
                                <asp:RegularExpressionValidator ID="revRTN2" runat="server" Text="X" Font-Bold="true"
                                    Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Routing Number" ValidationExpression="^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$"
                                    ControlToValidate="txtRTN2" />
                            </td>
                        </tr>
                    </asp:Panel>
                -->
                    <tr>
                        <td>Payment Amount Requested:
                        </td>
                        <td>
                            <telerik:RadNumericTextBox ID="payamount" Width="100" runat="server" Type="Currency"
                                EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Button ID="btnmanpay" OnClick="SendIt" runat="server" Width="150" Text="Submit"></asp:Button>
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
