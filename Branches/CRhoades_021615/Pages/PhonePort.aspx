<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PhonePort.aspx.vb" Inherits="GeneralForms.PhonePort" %>

<%@ Register TagName="MB" TagPrefix="User" Src="~/Controls/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <title>General Forms</title>
    <style type="text/css">
        input[readonly] {
            cursor: not-allowed;
        }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Current Phone Ownership Lookup
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
            <!-- FORM PANEL -->
            <asp:Panel ID="pnlform" runat="server">
                <div class="sectionTitle">
                    Number of phones to port
                </div>
                <asp:RadioButtonList runat="server" ID="rblPhoneCount" RepeatDirection="Horizontal"
                    AutoPostBack="true">
                    <asp:ListItem Text="1" Value="1" Selected="True" />
                    <asp:ListItem Text="2" Value="2" />
                </asp:RadioButtonList>
                <div class="sectionTitle">
                    Customer Information
                </div>
                <table class="input" cellpadding="2" cellspacing="0">
                    <tr>
                        <td>Telephone #:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtPhone" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtphone" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="rfvPhone" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revPhone" runat="server" ErrorMessage="&lt;font size=3 face=arial&gt;&lt;b&gt;Must be 10 digits&lt;/b&gt;&lt;/font&gt;"
                                ControlToValidate="txtPhone" Display="Dynamic" ToolTip="The telephone number must be 10 digits long"
                                ValidationExpression="^(\d{10})?$"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr runat="server" id="trTel2" visible="false">
                        <td>Telephone #:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtPhone2" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtphone2" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="rfvPhone2" Display="Dynamic" Enabled="false" />
                            <asp:RegularExpressionValidator ID="revPhone2" runat="server" ErrorMessage="&lt;font size=3 face=arial&gt;&lt;b&gt;Must be 10 digits&lt;/b&gt;&lt;/font&gt;"
                                ControlToValidate="txtPhone2" Display="Dynamic" ToolTip="The telephone number must be 10 digits long"
                                ValidationExpression="^(\d{10})?$" Enabled="false"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <tr>
                        <td>House #:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtHousekey" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" ControlToValidate="txtHousekey"
                                Display="Dynamic" ErrorMessage="&lt;font size=3 face=arial&gt;&lt;b&gt;X&lt;/b&gt;&lt;/font&gt;"></asp:RequiredFieldValidator>
                        </td>
                    </tr>
                </table>
                <asp:Panel ID="pnlAddlInfo" runat="server" Visible="False" class="messageInfo">
                    This phone number requires additional information to complete the port
                </asp:Panel>
                <asp:Panel ID="pnlDSL" runat="server" Visible="False">
                    Ask the customer if this line has DSL on it.<br />
                    Is this a DSL line?
                <asp:Button ID="btnDSLYes" runat="server" Text="Yes" />
                    &nbsp;<asp:Button ID="btnDSLNo" runat="server" Text="No" />
                </asp:Panel>
                <asp:Panel ID="pnlAccount" runat="server" Width="220" Visible="False">
                    <div style="width: 100px; text-align: right">
                        <asp:CustomValidator ControlToValidate="txtAccount" runat="server" ID="cvCustAcct" Display="None" ErrorMessage="Invalid Account number. <br />Please do not enter fake Acct#/PIN values.<br />Please do not enter an ICOMS account number.<br />If the customer does not know the account, leave blank.<br />" />
                        Account:
                <asp:TextBox autocomplete="off" ID="txtAccount" runat="server"></asp:TextBox>
                    </div>
                </asp:Panel>
                <asp:Panel ID="pnlPIN" runat="server" Width="220" Visible="False">
                    <div style="width: 120px; text-align: right">
                        PIN:
                <asp:TextBox autocomplete="off" ID="txtPIN" runat="server"></asp:TextBox>
                    </div>
                </asp:Panel>
                <div class="SubmitButton">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="150" />
                    <asp:Button ID="btnSubmitAddlInfo" runat="server" Text="Submit" Width="150" Visible="False" />
                </div>
                <asp:ValidationSummary runat="server" />
            </asp:Panel>
            <!-- Thank you PANEL -->
            <asp:Panel ID="pnlThanks" runat="server" Visible="false">
                <br />
                <div class="sectionTitle">
                    Thank You
                </div>
                <b>
                    <asp:Label runat="server" ID="lblThankYou"></asp:Label></b>
            </asp:Panel>
        </div>
        <div id="Container-Error">
            <asp:Label ID="lblerr" runat="server" ForeColor="red" Width="450" BackColor="yellow"
                Font-Bold="True" Visible="false" EnableViewState="False" />
        </div>
        <div id="ErrorBlock" style="clear: both" runat="server" visible="false">
            CompanyName:
        <asp:Label ID="lblCompanyName" runat="server" /><br />
            ErrorCode:
        <asp:Label ID="lblErrorCode" runat="server" /><br />
            ErrorMessage:
        <asp:Label ID="lblErrorMessage" runat="server" />
        </div>
        <div id="ResponseBlock" style="clear: both" runat="server" visible="false">
            CompanyName:
        <asp:Label ID="lblResponseCompanyName" runat="server" /><br />
            sPID:
        <asp:Label ID="lblResponseSPID" runat="server" /><br />
            ErrorCode:
        <asp:Label ID="lblResponseErrorCode" runat="server" /><br />
            ErrorMessage:
        <asp:Label ID="lblResponseErrorMessage" runat="server" />
        </div>
        <User:MB runat="server" ID="MB"></User:MB>
        <User:MB runat="server" ID="MBDSL"></User:MB>
    </form>
</body>
</html>
