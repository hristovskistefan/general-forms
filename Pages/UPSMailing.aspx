<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="UPSMailing.aspx.vb" Inherits="GeneralForms.UPSMailingForm" %>
<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head runat="server">
    <title>General Forms</title>
    <style type="text/css">
        .style1 {
            height: 23px;
        }
    </style>
</head>
<body>
    <form id="Form1" runat="server">
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                UPS Mailing Form
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
        <div id="Container-Content" class="clearfix">
            <!-- FORM PANEL -->
            <asp:Panel ID="pnlmain" runat="server">
                <table class="input" width="600" cellpadding="2" cellspacing="0">
                    <tr>
                        <td class="sectionTitle" colspan="4">Customer Information
                        </td>
                    </tr>
                    <tr>
                        <td>ICOMS Account:
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
                        </td>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtphone" runat="server" MaxLength="10" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" runat="server" ControlToValidate="txtphone"
                                Display="Dynamic" Text="&lt;font size=3&gt;&lt;b&gt;X&lt;/b&gt;&lt;/font&gt;" />
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtFName" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtFName"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtLName" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" ControlToValidate="txtLName"
                                Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                
                    <tr>
                        <td colspan="4" class="sectionTitle">Address Information
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBox AutoPostBack="True" ID="chkmoved" runat="server" Text="Customer has moved out of serviceable area." />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlin" runat="server">
                        <tr>
                            <td>Address:
                            </td>
                            <td>
                                <asp:TextBox ID="txtaddy" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtaddy" runat="server" Text="<font size=3><b>X</b></font>"
                                    ID="Requiredfieldvalidator1" Display="Dynamic" />
                            </td>
                            <td></td>
                            <td></td>
                        </tr>
                        <tr>
                            <td>State:
                            </td>
                            <td>
                                <asp:DropDownList ID="dropstate" runat="server" AutoPostBack="True">
                                         </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="dropstate" runat="server" InitialValue="Select One"
                                    Text="<font size=3><b>X</b></font>" Display="Dynamic" />
                            </td>
                            <td>City:
                            </td>
                            <td>
                                <asp:DropDownList ID="dropcity" runat="server" AutoPostBack="True" />
                                <asp:RequiredFieldValidator ControlToValidate="dropcity" runat="server" InitialValue="Select One"
                                    Text="<font size=3><b>X</b></font>" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>Zip Code:
                            </td>
                            <td>
                                <asp:DropDownList ID="dropzip" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="dropzip" runat="server" InitialValue="Select One"
                                    Text="<font size=3><b>X</b></font>" Display="Dynamic" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlout" runat="server">
                        <tr>
                            <td>Address:
                            </td>
                            <td>
                                <asp:TextBox ID="txtaddyout" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtaddyout" runat="server" Text="<font size=3><b>X</b></font>"
                                    ID="Requiredfieldvalidator2" Display="Dynamic" />
                            </td>
                            <td>City:
                            </td>
                            <td>
                                <asp:TextBox ID="txtcity" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtcity" runat="server" Text="<font size=3><b>X</b></font>"
                                    ID="Requiredfieldvalidator3" Display="Dynamic" />
                            </td>
                        </tr>
                        <tr>
                            <td>State:
                            </td>
                            <td>
                                <asp:TextBox ID="txtstate" runat="server" />
                                <asp:RequiredFieldValidator ControlToValidate="txtstate" runat="server" Text="<font size=3><b>X</b></font>"
                                    Display="Dynamic" />
                            </td>
                            <td>Zip Code:
                            </td>
                            <td>
                                <asp:TextBox ID="txtzip" runat="server" MaxLength="5" />
                                <asp:RequiredFieldValidator ControlToValidate="txtzip" runat="server" Text="<font size=3><b>X</b></font>"
                                    Display="Dynamic" />
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td colspan="4" class="style1">Equipment Information
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <table width="100%">
                                <tr>
                                    <td>Analog Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropana" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>Digital Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropdigi" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>DVR Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropdvr" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>HD Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drophd" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>HD DVR Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="drophddvr" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>Cable Modem:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropcable" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td nowrap="nowrap">Phone/Wireless Modem:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropphone" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                    <td>Cable Cards:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="dropccard" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                                <tr>
                                    <td>DTA Receiver:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlDTA" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                    <td></td>
                                    <td></td>
                                </tr>
                                <tr>
                                    <td>Ultra TV Gateway:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlUTVGateway" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                        </asp:DropDownList>
                                    </td>
                                    <td nowrap="nowrap">Ultra TV Media Player:
                                    </td>
                                    <td>
                                        <asp:DropDownList ID="ddlUTVMedia" runat="server">
                                            <asp:ListItem Value="0" Selected="True" />
                                            <asp:ListItem Value="1" />
                                            <asp:ListItem Value="2" />
                                            <asp:ListItem Value="3" />
                                            <asp:ListItem Value="4" />
                                            <asp:ListItem Value="5" />
                                            <asp:ListItem Value="6" />
                                            <asp:ListItem Value="7" />
                                            <asp:ListItem Value="8" />
                                            <asp:ListItem Value="9" />
                                            <asp:ListItem Value="10" />
                                        </asp:DropDownList>
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblerr" runat="server" ForeColor="red" Font-Bold="True" BackColor="yellow"
                                Width="100%" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button runat="server" ID="btnsend" Width="125" Text="Submit" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlthx" runat="server">
                <center>
                    <table class="input" width="400" cellpadding="2" cellspacing="0">
                        <tr>
                            <td align="center">
                                <b>The information you submitted was received and will be processed as soon as possible.
                                Click the button below to continue.</b>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button ID="btncont" runat="server" Width="125" Text="Continue..." CssClass="SubmitButton" />
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <asp:Label ID="lblHouseNumber" runat="server" Visible="False"></asp:Label>
        </div>
    <User:MB ID="MB" runat="server" />
    </form></body>
</html>
