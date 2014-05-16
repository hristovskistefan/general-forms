<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PhoneInquiry.aspx.vb"
    Inherits="GeneralForms.PhoneInquiry" %>

<%@ Register TagPrefix="User" TagName="MB" Src="~/Controls/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <title>General Forms</title>
    <style type="text/css">

p.MsoPlainText
	{margin-bottom:.0001pt;
	font-size:11.0pt;
	font-family:"Calibri","sans-serif";
	        margin-left: 0in;
            margin-right: 0in;
            margin-top: 0in;
        }
    </style>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Phone Inquiry
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
            <div class="sectionTitle">
                Select Inquiry Type
            </div>
            <asp:RadioButtonList ID="rblType" runat="server" AutoPostBack="True">
                <asp:ListItem Value="0" Text="General Phone Inquiry" />
                <asp:ListItem Value="1" Text="Phone INP Error" />
                <asp:ListItem Value="2" Text="3PV Passwords/New Requests/Reset" />
                <asp:ListItem Value="3" Text="WOW! Phone Customer Voicemail Password Reset" />
                <asp:ListItem Value="4" Text="Scheduled/Installed at Incorrect Address" />
            </asp:RadioButtonList>
            <br />
            <!-- General Phone Inquiry PANEL -->
            <asp:Panel ID="pnlGeneralInquiry" runat="server" Visible="false">
                <div class="sectionTitle">
                    Customer Information
                </div>
                <table class="input" cellpadding="2" cellspacing="0">
                   <tr>
                        <td>Account #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountGeneralInquiry" runat="server" Width="82" MaxLength="8" AutoPostBack="true"></asp:TextBox>
							<asp:ImageButton
                                ID="ibGo" ValidationGroup="vgGeneralInquiry" CausesValidation="false" runat="server"
                                ImageUrl="~/images/SearchGo.gif" />
                            <asp:RegularExpressionValidator ID="revGeneralInquiry"
                                ControlToValidate="txtAccountGeneralInquiry"
                                SetFocusOnError="true"
                                runat="server" 
                                ValidationGroup="vgGeneralInquiry"
                                Display="Dynamic" Font-Bold="True" Font-Size="Small"  
                                ErrorMessage="Invalid Account Number" />
                            <asp:RequiredFieldValidator ID="rfvGeneralInquiry"
                                ControlToValidate="txtAccountGeneralInquiry"
                                SetFocusOnError="true"
                                runat="server"                                
                                ValidationGroup="vgGeneralInquiry"
                                Display="Dynamic" Font-Bold="True" Font-Size="Small"
                                ErrorMessage="Account Number Required" />
                        
                        </td>
                    </tr>
                    <tr>
                        <td>Customer Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtcustname" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtcustname" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valcustname" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:
                        </td>
                        <td>
                            <asp:TextBox ID="txtcity" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtcity" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valcity" />
                        </td>
                    </tr>
                  <tr>
                        <td>State:
                        </td>
                        <td>
                            <asp:TextBox ID="txtState" runat="server" Width="180" MaxLength="2" />
                            <asp:RequiredFieldValidator ControlToValidate="txtState" runat="server" ID="rfvState"
                                Text="<font size=3 face=arial><b>X</b></font>" />
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code:
                        </td>
                        <td>
                            <asp:TextBox MaxLength="5" ID="txtzip" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtzip" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valzip" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtphone" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtphone" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valphone" />
                        </td>
                    </tr>
                    <tr>
                        <td>Question/ Issue:
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtquest" runat="server" TextMode="MultiLine" Rows="5" Columns="40" />
                            <asp:RequiredFieldValidator ID="rfvtextquest" ControlToValidate="txtquest"
                                runat="server" Text="<font size=3 face=arial><b>X</b></font>" />
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnGeneralInquiry" runat="server" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
            <!-- INP Error PANEL -->
            <asp:Panel ID="pnlInpError" runat="server"  Visible="false">
               <div class="sectionTitle">
                    Phone INP Error
                </div>
                Account number and Phone number are both required.
                <table class="input" cellpadding="2" cellspacing="0">
                   <tr>
                        <td>Account #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAccountNumberPhoneInpError" runat="server" Width="82" MaxLength="8" AutoPostBack="true"></asp:TextBox>
							<!--<asp:ImageButton
                                ID="ibGo3" ValidationGroup="vgAcctInfo" CausesValidation="false" runat="server"
                                ImageUrl="~/images/SearchGo.gif" />-->
                            <asp:RegularExpressionValidator ID="revAccountNumberPhoneInpError"
                                ControlToValidate="txtAccountNumberPhoneInpError"
                                SetFocusOnError="true"
                                runat="server" 
                                ValidationGroup="vgPhoneINP"
                                Display="Dynamic" Font-Bold="True" Font-Size="Small"  
                                ErrorMessage="Invalid Account Number" />
                            <asp:RequiredFieldValidator ID="rfvAccountNumberPhoneInpError"
                                ControlToValidate="txtAccountNumberPhoneInpError"
                                SetFocusOnError="true"
                                runat="server"                                
                                ValidationGroup="vgPhoneINP"
                                Display="Dynamic" Font-Bold="True" Font-Size="Small"
                                ErrorMessage="Account Number Required" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhoneINP" runat="server" MaxLength="13" Width="115" />
                            <asp:RegularExpressionValidator ID="revPhoneINP"
                                ControlToValidate="txtPhoneINP"
                                SetFocusOnError="true"
                                runat="server" 
                                ValidationGroup="vgPhoneINP"
                                Display="Dynamic" Font-Bold="True" Font-Size="small"
                                ErrorMessage="Invalid Phone Number"
                                ValidationExpression="^[01]?[- .]?(\([2-9]\d{2}\)|[2-9]\d{2})[- .]?\d{3}[- .]?\d{4}$" />
                             <asp:RequiredFieldValidator ID="rfvPhoneINP"
                                ControlToValidate="txtPhoneINP"
                                SetFocusOnError="true"
                                runat="server"                                
                                ValidationGroup="vgPhoneINP"
                                Display="Dynamic" Font-Bold="True" Font-Size="Small"
                                ErrorMessage="Phone Number Required" />
                        </td>
                    </tr>
                    <tr>
                        <td>Comments:<br />
                        </td>
                        <td>
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Rows="4" Columns="40" MaxLength="150" />

                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnInpError" runat="server" CausesValidation="true" ValidationGroup="vgPhoneINP" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
            <!-- 3PV PANEL -->
            <asp:Panel ID="pnl3pv" runat="server" Visible="false">
                <div class="sectionTitle">
                    3PV/CSR Passwords/New Requests/Reset
                </div>
                <asp:Panel ID="pnl3PVlink" runat="server">
                    <p class="MsoPlainText" style="color: red;">
                        For immediate attention and password reset, please contact your local RM/Qhawk teams, otherwise submit the following request form.<br />
                    </p>
                </asp:Panel>
                <table cellpadding="3" cellspacing="0" class="input" width="450">
                    <tr>
                        <td>Last 4 of your SSN:
                        </td>
                        <td>
                            <asp:TextBox ID="txtssn" runat="server" Width="50" MaxLength="4" />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top">Issue Type:
                        </td>
                        <td>
                            <asp:RadioButtonList ID="rad3pviss" runat="server" AutoPostBack="true">
                                <asp:ListItem Value="CSR Login" />
                                <asp:ListItem Value="3PV Login" />
                                <asp:ListItem Value="CSR & 3PV Login" />
                            </asp:RadioButtonList>
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button runat="server" Width="150" ID="btn3pv" Text="Submit" />
                </div>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlVMCheck1" Visible="false">
                <div class="sectionTitle">
                    Voicemail Password Resets
                </div>
                <table cellpadding="3" cellspacing="0" class="input" width="450">
                    <tr>
                        <td>Does the customer have Voicemail on their WOW! Phone?
                        </td>
                        <td>
                            <asp:RadioButton ID="rbVMCheck1" runat="server" GroupName="VMCheck1" AutoPostBack="True"
                                Text="Yes" />
                            <asp:RadioButton ID="rbVMCheck2" runat="server" GroupName="VMCheck1" AutoPostBack="True"
                                Text="No" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblNoVM"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel runat="server" ID="pnlVMCheck2" Visible="false">
                <table cellpadding="3" cellspacing="0" class="input" width="450">
                    <tr>
                        <td>Does the customer have 1 or 2 phone numbers to reset?
                        </td>
                        <td>
                            <asp:RadioButton ID="rbVMCheck3" runat="server" GroupName="VMCheck2" AutoPostBack="True"
                                Text="1" />
                            <asp:RadioButton ID="rbVMCheck4" runat="server" GroupName="VMCheck2" AutoPostBack="True"
                                Text="2" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Label runat="server" ID="lblNoVM0"></asp:Label>
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <!-- VOICE MAIL RESET PANEL -->
            <asp:Panel ID="pnlvm" runat="server" Visible="false">
                <table cellpadding="3" cellspacing="0" class="input" width="450">
                    <tr>
                        <td colspan="2">Verify the telephone number that the customer needs their voicemail password reset
                        for. The Telephone Number can be found in ICOMS.
                        </td>
                    </tr>
                    <tr>
                        <td>Account #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtvmacct" runat="server" MaxLength="8" />
                            <asp:RequiredFieldValidator ControlToValidate="txtvmAcct" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="rfvAcctNumVM" />
                        </td>
                    </tr>
                    <tr>
                        <td>Telephone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtvm" runat="server" MaxLength="10" />
                            <asp:RequiredFieldValidator ControlToValidate="txtvm" runat="server" ErrorMessage="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator4" />
                            <asp:RegularExpressionValidator runat="server" ID="revvm" ControlToValidate="txtvm"
                                ErrorMessage="<font size=3 face=arial><b>X</b></font>" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                        </td>
                    </tr>
                    <asp:Panel ID="pnlTel2" runat="server">
                        <tr>
                            <td>Telephone 2 #:
                            </td>
                            <td>
                                <asp:TextBox ID="txtvm2" runat="server" MaxLength="10" />
                                <asp:RequiredFieldValidator ControlToValidate="txtvm2" runat="server" ErrorMessage="<font size=3 face=arial><b>X</b></font>"
                                    ID="Requiredfieldvalidator8" />
                                <asp:RegularExpressionValidator runat="server" ID="RegularExpressionValidator1" ControlToValidate="txtvm2"
                                    ErrorMessage="<font size=3 face=arial><b>X</b></font>" ValidationExpression="\d{10}"></asp:RegularExpressionValidator>
                            </td>
                        </tr>
                    </asp:Panel>
                </table>
                <div class="SubmitButton">
                    <asp:Button runat="server" Width="150" ID="btnvm" Text="Submit" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlccr" runat="server" Visible="false">
                <div class="sectionTitle">
                    CCR Error Report
                </div>
                <table class="input" cellpadding="3" cellspacing="0" width="450">
                    <tr>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtccrphone" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" ControlToValidate="txtccrphone"
                                runat="server" Text="<font size=3 face=arial><b>X</b></font>" />
                        </td>
                    </tr>
                    <tr>
                        <td>State:
                        </td>
                        <td>
                            <asp:DropDownList ID="dropccrstate" runat="server" Width="180" AutoPostBack="True">
                                <asp:ListItem Selected="True" Value="Select One" />
                                <asp:ListItem Value="IL" />
                                <asp:ListItem Value="IN" />
                                <asp:ListItem Value="MI" />
                                <asp:ListItem Value="OH" />
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" ControlToValidate="dropccrstate"
                                runat="server" Text="<font size=3 face=arial><b>X</b></font>" InitialValue="Select One" />
                        </td>
                    </tr>
                    <tr>
                        <td>LEC #:
                        </td>
                        <td>
                            <asp:TextBox runat="server" ID="txtlec" Width="100" ReadOnly="True" />
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator7" ControlToValidate="txtlec"
                                runat="server" Text="<font size=3 face=arial><b>X</b></font>" />
                        </td>
                    </tr>
                    <tr>
                        <td>CCR Error: <i>Just copy and paste below</i>
                        </td>
                        <td></td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:TextBox ID="txtccriss" runat="server" TextMode="MultiLine" Rows="5" Columns="40" />
                            <asp:RequiredFieldValidator ControlToValidate="txtquest" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="rfvquest" />
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnccr" runat="server" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
            <asp:Panel ID="pnlfraud" runat="server" Visible="false">
                <div class="sectionTitle">
                    Call Forwarding Fraud Report
                </div>
                <table cellpadding="3" cellspacing="0" class="input" width="450">
                    <tr>
                        <td>Account #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtfraudacct" runat="server" MaxLength="16" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtfraudphone" runat="server" MaxLength="10" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Comments:<br />
                            <asp:TextBox ID="txtfraudcomm" runat="server" TextMode="MultiLine" Rows="2" Columns="40" />
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnfraud" runat="server" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
            <!-- Scheduled/Installed at Incorrect Address Panel -->
            <asp:Panel ID="pnlIncAddress" runat="server" Visible="false">
                <div class="sectionTitle">
                    Customer Information
                </div>
                <table class="input" cellpadding="3" cellspacing="0" width="450">
                    <tr>
                        <td>Account #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAcctNum2" runat="server" Width="80" MaxLength="8" AutoPostBack="true"></asp:TextBox><asp:ImageButton
                                ID="ibGo2" ValidationGroup="vgAcctInfo" CausesValidation="false" runat="server"
                                ImageUrl="~/images/SearchGo.gif" />
                            <asp:RegularExpressionValidator ID="revAccount2" runat="server" Text="X" ControlToValidate="txtAcctNum2"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                                ErrorMessage="The account number entered is not valid" ValidationGroup="vgAcctInfo" />
                            <asp:RequiredFieldValidator ID="rfvAccount2" runat="server" ControlToValidate="txtAcctNum2"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer Account Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                    <tr>
                        <td>Customer Name:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCustName2" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtCustName2" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator22" />
                        </td>
                    </tr>
                    <tr>
                        <td>City:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCity2" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtCity2" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator23" />
                        </td>
                    </tr>
                    <tr>
                        <td>State:
                        </td>
                        <td>
                            <asp:TextBox ID="txtState2" runat="server" Width="180" MaxLength="2" />
                            <asp:RequiredFieldValidator ControlToValidate="txtState2" runat="server" ID="Requiredfieldvalidator24"
                                Text="<font size=3 face=arial><b>X</b></font>" />
                        </td>
                    </tr>
                    <tr>
                        <td>Zip Code:
                        </td>
                        <td>
                            <asp:TextBox MaxLength="5" ID="txtZip2" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtZip2" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator25" />
                        </td>
                    </tr>
                    <tr>
                        <td>Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtPhone2" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtPhone2" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator26" />
                        </td>
                    </tr>
                    <tr>
                        <td>Alternate Phone #:
                        </td>
                        <td>
                            <asp:TextBox ID="txtAltPhone" runat="server" MaxLength="10" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtAltPhone" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator28" />
                        </td>
                    </tr>
                    <tr>
                        <td>Correct Address:
                        </td>
                        <td>
                            <asp:TextBox ID="txtCorrAddress" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtCorrAddress" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator29" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">If unable to schedule for originial due date what is an alternative date and time?:
                        </td>
                    </tr>
                    <tr>
                        <td></td>
                        <td>
                            <asp:TextBox ID="txtDateTime" runat="server" Width="180" />
                            <asp:RequiredFieldValidator ControlToValidate="txtDateTime" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="Requiredfieldvalidator210" />
                        </td>
                    </tr>
                    <tr>
                        <td>Customer Already Installed?
                        </td>
                        <td>
                            <asp:DropDownList runat="server" ID="ddlCustInst">
                                <asp:ListItem Value="Yes"></asp:ListItem>
                                <asp:ListItem Value="No"></asp:ListItem>
                            </asp:DropDownList>
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnIncAddySubmit" runat="server" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
            <!-- WRONG PANEL -->
            <asp:Panel ID="pnlwrong" runat="server" Visible="false">
                <div class="sectionTitle">
                    Invalid Selection
                </div>
                You have selected Non-Customer Inquiry. This is not a valid selection for this process.
            Please visit the Telephony Wait List form and complete that form. The customer will
            be notified when service is available in their area.
            <br />
                <br />
                <i>Thank you...</i>
            </asp:Panel>
            <!-- Thank you PANEL -->
            <asp:Panel ID="pnlThanks" runat="server" Visible="false">
                <div class="sectionTitle">
                    Thank You
                </div>
                Thank you for submitting your inquiry. A Specialist will address your issue as quickly as possible.
            </asp:Panel>
            <asp:ListBox ID="lstexport" runat="server" Visible="False" />
        </div>
        <div id="Container-Error">
            <asp:Label ID="lblerr" runat="server" ForeColor="red" Width="450" BackColor="yellow"
                Font-Bold="True" Visible="false" />
        </div>
    <User:MB runat="server" ID="MB" />
    </form>
</body>
</html>
