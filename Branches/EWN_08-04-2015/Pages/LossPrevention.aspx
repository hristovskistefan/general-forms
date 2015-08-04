<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="LossPrevention.aspx.vb"
    Inherits="GeneralForms.LossPrevention" %>

<%-- Carl Rhoades - 26AUG14 - Added Georgia to drop down lists --%>
<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
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
    <form id="Form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Loss Prevention Form
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
            <asp:Panel ID="pnlmain" runat="server">
                <table class="input" width="600" cellspacing="0" cellpadding="2">
                    <tr>
                        <td>
                            <!-- Carl Rhoades 06/26/14 - Removed Restart, renamed Former - Unserviceable to Unblock Address -->
                            <asp:RadioButtonList runat="server" AutoPostBack="True" ID="radformsel" RepeatDirection="Horizontal">
                                <asp:ListItem Value="0" Text="New Start Request">New Start Request</asp:ListItem>
                                <asp:ListItem Value="2" Text="Suspected Fraud">Suspected Fraud</asp:ListItem>
                                <asp:ListItem Value="4" Text="Unblock Address">Unblock Address</asp:ListItem>
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <asp:Label ID="lblsuperr" runat="server" ForeColor="red" BackColor="yellow" Font-Size="12pt"
                    Font-Bold="True" />
            </asp:Panel>
            <!--
---------------------
    NEW REQUEST PANEL
---------------------
-->
            <br />
            <asp:Panel ID="pnlnew" runat="server">
                <div class="sectionTitle">
                    New Start Request
                </div>
                <table class="input" width="600" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="4" class="green">
                            <div class="infobox">
                                <b>Do Not set up an install if you are submitting this form.</b> Use this form if
                            the customer is providing information that cannot be validated through Accurint
                            or if there are previous charged off accounts at the address the customer is requesting
                            new service for.
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtnewfirst" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtnewfirst" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtnewlast" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtnewlast" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>ICOMS House Number:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtLocationID" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                            <asp:ImageButton ImageUrl="/Images/SearchGo.gif" ID="ibHseNumber" runat="server" CausesValidation="false" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator38" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtLocationID" />
                            <asp:RegularExpressionValidator ID="revLocationID" runat="server"
                                ErrorMessage="<b>Invalid ICOMS House Number.</b><br />Verify this number matches the House Number from the 10 screen in ICOMS." ControlToValidate="txtLocationID"
                                ValidationExpression="^[0-9]{7,10}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlRecordExists" runat="server" Visible="false">
                        <tr>
                            <td colspan="4">
                                <span style="color: red;">Please correct the following:</span>
                                <ul style="color: red;">
                                    <li>This ICOMS House Number has already been submitted for review on this issue type and has an active issue open for review. You may not submit another request for this account and issue type until the current issue has been resolved.</li>
                                </ul>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>Address:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtnewaddy" runat="server" Width="300" />
                            <asp:RequiredFieldValidator ControlToValidate="txtnewaddy" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtnewcity" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtnewcity" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                        <td>State:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropnewstate" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="Select One" />
                                <asp:ListItem Value="Illinois" />
                                <asp:ListItem Value="Indiana" />
                                <asp:ListItem Value="Michigan" />
                                <asp:ListItem Value="Ohio" />
                                <asp:ListItem Value="Georgia" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="dropnewstate" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" InitialValue="Select One" />
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtnewzip" runat="server" Width="160" MaxLength="5" />
                            <asp:RequiredFieldValidator ControlToValidate="txtnewzip" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td colspan="3">
                            <telerik:RadNumericTextBox runat="server" ID="rntnewPhone" MaxLength="10" ShowSpinButtons="false"
                                ShowButton="false" EmptyMessage="Enter Phone #" NumberFormat-DecimalDigits="0"
                                NumberFormat-GroupSeparator="" Width="160px" autocomplete="off" />
                            <asp:RequiredFieldValidator ControlToValidate="rntnewPhone" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revNewPhone" runat="server" Text="X" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Phone Number"
                                ValidationExpression="^[- .]?(((?!\(000\))(?!\(111\))(?!\(222\))(?!\(333\))(?!\(444\))(?!\(555\))(?!\(666\))(?!\(777\))(?!\(900\))\(\d{3}\) ?)|(?!000)(?!111)(?!222)(?!333)(?!444)(?!555)(?!666)(?!777)(?!900)([2-9]\d{2}\)|[2-9]\d{2}))[- .]?\d{3}[- .]?\d{4}$"
                                ControlToValidate="rntnewPhone" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">SSN:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtnewssn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revNewSSN" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtnewssn" />
                        </td>
                        <td valign="top">DL #:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtnewdl" runat="server" Width="160" Maxlength="20" />
                            <asp:RegularExpressionValidator ID="revTxtNewDl" runat="server"
                                Display="dynamic"
                                ErrorMessage="Driver's License must be at least 5 characters, or contains an invalid character"
                                ControlToValidate="txtnewdl"
                                ValidationExpression="^[a-zA-Z0-9]{5,}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>

                    <tr>
                        <td valign="top">Additional SSN:</td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtNewAdditionalSsn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revNewAdditionalSSN" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtNewAdditionalSsn" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblSsnDlError" runat="server" Visible="false"><span style="color: red; font-weight: bold;">A valid SSN or Driver's License number is required.</span></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:CheckBox runat="server" ID="cbD2d" Checked="false" CausesValidation="false"
                                AutoPostBack="true" Text="Door-To-Door" />
                            <br />
                            <asp:Label runat="server" ID="lblD2D" Text="D2D E-Mail  " Visible="false" />
                            <asp:TextBox autocomplete="off" runat="server" ID="txtD2DEmail" Visible="false" />
                            <asp:RequiredFieldValidator runat="server" ID="rfvd2dEmail" ControlToValidate="txtD2DEmail"
                                Display="Dynamic" Text="X" Font-Bold="true" Enabled="false" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">Comments: <i>(Max 500 characters)</i><br />
                            <asp:TextBox autocomplete="off" TextMode="MultiLine" Columns="50" Rows="5" runat="server" ID="txtnewcomm"
                                Font-Size="Medium" MaxLength="500" />
                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator5" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtnewcomm" />
                            <br />
                            <b>Comments are required, and should detail why the LP form is being submitted</b>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnnewsend" Text="Submit" OnClick="SendIt" runat="server" Width="150" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>

            <!--
---------------------
    FRAUD PANEL
---------------------
-->
            <asp:Panel ID="pnlfraud" runat="server">
                <div class="sectionTitle">
                    Suspected Fraud
                </div>
                <table class="input" width="600" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="4">
                            <div class="infobox">
                                <font color="Red" size="-1"><b>If this supected fraud person is wanting to start or
                                re-start services, please enter this information under the New Start Request button
                                or the Restart Request button. </b></font>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <div class="infobox">
                                <b>Do not set up an install if you are submitting this form.</b> This form is to
                            be used if any information provided can be linked to a previous account with an
                            outstanding balance owed in ICOMS such as same phone#, same SS#, same last name, etc.
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtsusfirst" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsusfirst" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtsuslast" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsuslast" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>Account Number:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtsusacct" runat="server" Width="80" MaxLength="8"></asp:TextBox>
                            <asp:ImageButton ID="ibSFGo" CausesValidation="false" runat="server" ImageUrl="~/images/SearchGo.gif" />
                            <asp:RegularExpressionValidator ID="revsusacct" runat="server" Text="X" ControlToValidate="txtsusacct"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                                ErrorMessage="The account number entered is not valid" />
                            <asp:RequiredFieldValidator ID="rfvsusacct" runat="server" ControlToValidate="txtsusacct"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer Account Required"
                                EnableClientScript="false" />
                        </td>
                    </tr>
                    <asp:Panel ID="pnlRecordExistsFraud" runat="server" Visible="false">
                        <tr>
                            <td colspan="4">
                                <span style="color: red;">Please correct the following:</span>
                                <ul style="color: red;">
                                    <li>This account has already been submitted for review on this issue type and has an active issue open for review. You may not submit another request for this account and issue type until the current issue has been resolved.</li>
                                </ul>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>Address:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtsusaddy" runat="server" Width="300" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsusaddy" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtsuscity" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsuscity" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                        <td>State:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropsusstate" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="Select One" />
                                <asp:ListItem Text="Illinois" Value="IL" />
                                <asp:ListItem Text="Indiana" Value="IN" />
                                <asp:ListItem Text="Michigan" Value="MI" />
                                <asp:ListItem Text="Ohio" Value="OH" />
                                <asp:ListItem Text="Georgia" Value="GA" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="dropsusstate" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" InitialValue="Select One" />
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtsuszip" runat="server" Width="160" MaxLength="5" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsuszip" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                        </td>
                        <td></td>
                        <td></td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtsusphone" runat="server" Width="160" MaxLength="10" />
                            <asp:RequiredFieldValidator ControlToValidate="txtsusphone" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" />
                            <asp:RegularExpressionValidator ID="revSusPhone" runat="server" Text="X" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Phone Number"
                                ValidationExpression="^[- .]?(((?!\(000\))(?!\(111\))(?!\(222\))(?!\(333\))(?!\(444\))(?!\(555\))(?!\(666\))(?!\(777\))(?!\(900\))\(\d{3}\) ?)|(?!000)(?!111)(?!222)(?!333)(?!444)(?!555)(?!666)(?!777)(?!900)([2-9]\d{2}\)|[2-9]\d{2}))[- .]?\d{3}[- .]?\d{4}$"
                                ControlToValidate="txtsusphone" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">SSN:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtsusssn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revSusSnn" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtsusssn" />
                        </td>
                        <td valign="top">DL #:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtsusdl" runat="server" Width="160" Maxlength="20" />
                            <asp:RegularExpressionValidator ID="revSusDl" runat="server"
                                Display="Dynamic" 
                                ErrorMessage="Driver's License must be at least 5 characters, or contains an invalid character"
                                ControlToValidate="txtsusdl"
                                ValidationExpression="^[a-zA-Z0-9]{5,}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>


                    <tr>
                        <td valign="top">Additional SSN:</td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtSusAdditionalSsn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revSusAdditionalSsn" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtSusAdditionalSsn" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblSusSsnDlError" runat="server" Visible="false"><span style="color: red; font-weight: bold;">A valid SSN or Driver's License number is required.</span></asp:Label></td>
                    </tr>
                    <tr>
                        <td>Request Services:
                        </td>
                        <td colspan="3">
                            <asp:DropDownList ID="dropsusrequest" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="Select One" />
                                <asp:ListItem Value="Yes" />
                                <asp:ListItem Value="No" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="dropsusrequest" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" InitialValue="Select One" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">Comments: <i>(Max 500 characters)</i><br />
                            <asp:TextBox autocomplete="off" TextMode="MultiLine" Columns="50" Rows="5" runat="server" ID="txtsuscomm" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4">
                            <asp:Button ID="btnsussend" OnClick="SendIt" Text="Submit" runat="server" Width="150" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!--
---------------------
    Unblock Address PANEL - was DN Panel
---------------------
-->
            <asp:Panel ID="pnldn" runat="server">
                <table class="input" width="660px" cellspacing="0" cellpadding="2">
                    <tr>
                        <td colspan="2">
                            <div class="sectionTitle">Unblock Address</div>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="4" class="green">
                            <div class="infobox">
                                Use this form if the address is blocked due to fraud and the customer paid the bill, the block was not removed by LP, 
                                or an active customer is attempting to transfer services to a blocked address. 
                                <br />
                                <strong>An ICOMS House Number is required.</strong>
                            </div>
                        </td>
                    </tr>
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtdnfname" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdnfname" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ValidationGroup="vgDisconnected" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtdnlname" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdnlname" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ID="Requiredfieldvalidator1"
                                ValidationGroup="vgDisconnected" />
                        </td>
                    </tr>
                    <tr>

                        <td>ICOMS House Number:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtDnHouseNumber" runat="server" Width="80" MaxLength="10"></asp:TextBox>
                            <asp:ImageButton ImageUrl="/Images/SearchGo.gif" ID="ibtxtDnHouseNumber" runat="server" CausesValidation="false" />
                            <asp:RequiredFieldValidator ID="rfvDnHouseNumber" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtDnHouseNumber" />
                            <asp:RegularExpressionValidator ID="revDnHouseNumber" runat="server"
                                ErrorMessage="<b>Invalid ICOMS House Number.</b><br />Verify this number matches the House Number from the 10 screen in ICOMS."
                                ControlToValidate="txtDnHouseNumber"
                                ValidationExpression="^[0-9]{8,10}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlRecordExistsUnblock" runat="server" Visible="false">
                        <tr>
                            <td colspan="4">
                                <span style="color: red;">Please correct the following:</span>
                                <ul style="color: red;">
                                    <li>This ICOMS House Number has already been submitted for review on this issue type and has an active issue open for review. You may not submit another request for this account and issue type until the current issue has been resolved.</li>
                                </ul>
                            </td>
                        </tr>
                    </asp:Panel>
                    <tr>
                        <td>Address:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtdnaddy" runat="server" Width="300" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdnaddy" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ID="Requiredfieldvalidator2"
                                ValidationGroup="vgDisconnected" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtdncity" runat="server" Width="160" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdncity" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ValidationGroup="vgDisconnected" />
                        </td>
                        <td>State:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropdnstate" runat="server" Width="160">
                                <asp:ListItem Selected="True" Value="Select One" />
                                <asp:ListItem Value="Illinois" />
                                <asp:ListItem Value="Indiana" />
                                <asp:ListItem Value="Michigan" />
                                <asp:ListItem Value="Ohio" />
                                <asp:ListItem Value="Georgia" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="dropdnstate" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" InitialValue="Select One"
                                ID="Requiredfieldvalidator3" ValidationGroup="vgDisconnected" />
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtdnzip" runat="server" Width="160" MaxLength="5" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdnzip" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ValidationGroup="vgDisconnected" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td colspan="3">
                            <asp:TextBox autocomplete="off" ID="txtdnphone" runat="server" Width="160" MaxLength="10" />
                            <asp:RequiredFieldValidator ControlToValidate="txtdnphone" runat="server" Text="X"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" ID="Requiredfieldvalidator4"
                                ValidationGroup="vgDisconnected" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="X" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Phone Number"
                                ValidationExpression="^[- .]?(((?!\(000\))(?!\(111\))(?!\(222\))(?!\(333\))(?!\(444\))(?!\(555\))(?!\(666\))(?!\(777\))(?!\(900\))\(\d{3}\) ?)|(?!000)(?!111)(?!222)(?!333)(?!444)(?!555)(?!666)(?!777)(?!900)([2-9]\d{2}\)|[2-9]\d{2}))[- .]?\d{3}[- .]?\d{4}$"
                                ControlToValidate="txtdnphone" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">SSN:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtdnssn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revDnSsn" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtdnssn" />
                        </td>
                        <td valign="top">DL #:
                        </td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtdndl" runat="server" Width="160" Maxlength="20" />
                            <asp:RegularExpressionValidator ID="revDnDl" runat="server"
                                Display="Dynamic" 
                                ErrorMessage="Driver's License must be at least 5 characters, or contains an invalid character"
                                ControlToValidate="txtdndl"
                                ValidationExpression="^[a-zA-Z0-9]{5,}$">
                            </asp:RegularExpressionValidator>
                        </td>
                    </tr>


                    <tr>
                        <td valign="top">Additional SSN:</td>
                        <td valign="top">
                            <asp:TextBox autocomplete="off" ID="txtDnAdditionalSsn" runat="server" Width="160" MaxLength="11" />
                            <asp:RegularExpressionValidator ID="revDnAdditionalSsn" runat="server" Font-Bold="true"
                                Font-Size="Medium" Display="Dynamic" ErrorMessage="<br />Invalid SSN."
                                ValidationExpression="^(?!000)\d{3}[- .]?\d{2}[- .]?\d{4}$"
                                ControlToValidate="txtDnAdditionalSsn" />
                        </td>
                        <td>&nbsp;</td>
                        <td>&nbsp;</td>
                    </tr>


                    <tr>
                        <td colspan="4">
                            <asp:Label ID="lblDnDlSsnError" runat="server" Visible="false"><span style="color: red; font-weight: bold;">A valid SSN or Driver's License number is required.</span></asp:Label></td>
                    </tr>
                    <tr>
                        <td colspan="4">Comments: <i>(Max 500 characters)</i><br />
                            <asp:TextBox autocomplete="off" TextMode="MultiLine" Columns="50" Rows="5" runat="server" ID="txtdncomm" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button OnClick="SendIt" ID="btndnsend" runat="server" Text="Submit" Width="150" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlerror" runat="server">
                <center>
                    <table class="input" width="400" cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="red" align="center">An Error Has Occurred
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <font color="red">An error has prevented your request from being sent. Pleae review
                                the error message below<br />
                                    <asp:Label ID="lblerr" runat="server" ForeColor="red" />
                                </font>
                            </td>
                        </tr>
                        <tr>
                            <td align="center">Please try to submit this information again. If the error persists, please forward
                            this error message to ccc_it@wideopenwest.com.
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button runat="server" ID="btnreturn" Text="Return" Width="160" />
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
            <asp:Panel ID="pnlthx" runat="server">
                <center>
                    <table class="input" width="400" cellspacing="0" cellpadding="2">
                        <tr>
                            <td class="red" align="center">Submission Confirmation
                            </td>
                        </tr>
                        <tr>
                            <td align="center">The information you submitted has been sent successfully. Please click the button
                            below to return.
                            <br />
                                <asp:Label runat="server" ID="lblD2DConfirmation" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td align="center">
                                <asp:Button runat="server" ID="btnthx" Text="Return" Width="160" />
                            </td>
                        </tr>
                    </table>
                </center>
            </asp:Panel>
        </div>
        <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
