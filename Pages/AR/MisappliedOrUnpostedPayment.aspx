<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="MisappliedOrUnpostedPayment.aspx.vb" MaintainScrollPositionOnPostback="true"
    Inherits="GeneralForms.ARMisappliedOrUnpostedPayment" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <script type="text/javascript" language="javascript">
        function NOCCValidate(source, arguments) {
            var pattern = new RegExp("\\b(?:4\\d{3}[ -]*\\d{4}[ -]*\\d{4}[ -]*\\d(?:\\d{3})?|5[12345]\\d{2}[ -]*\\d{4}[ -]*\\d{4}[ -]*\\d{4}|6(?:011|5\\d{2})[ -]*\\d{4}[ -]*\\d{4}[ -]*\\d{4}|(0[0-9]|1[0-2]|2[1-9]|3[0-2]|6[1-9]|7[0-2]|80){1}\\d{7})\\b")
            arguments.IsValid = !(pattern.test(arguments.Value));
        }
    </script>
    <style type="text/css">
        input[readonly] {
            cursor: not-allowed;
        }
    </style>
    <title>General Forms</title>
</head>
<body>
    <form id="Form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Account Research - Misapplied or Unposted Payments
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
                                Use this form if the customer has made a payment but it has not posted to their
                            account or has posted for the incorrect amount.<br />
                                Customers must be prepared to fax supporting documentation to the Account Research
                            Department at 1-888-268-5859.<br />
                                This form is also used if the customer states that a payment has posted to their
                            account and that he/she did not make the payment.<br />
                                <br />
                                Inquiries that are within the CCRs scope of customer assistance will be returned
                            to the Supervisor for resolution.<br />
                                <span style="color: Red; font-size: larger; font-style: italic;">All inquiries must
                                be approved by your Supervisor prior to submission.</span>
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
                    <tr>
                        <td>First Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtcfname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator39" runat="server" ControlToValidate="txtcfname"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer First Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                        <td>Last Name:
                        </td>
                        <td>
                            <asp:TextBox autocomplete="off" ID="txtclname" runat="server" Width="160"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtclname"
                                Display="Dynamic" Font-Bold="True" Font-Size="Medium" Text="X" ErrorMessage="Customer Last Name Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>
                    </tr>
                </table>
            </asp:Panel>
            <asp:Panel ID="pnlmisapp" runat="server" Visible="false">
                <table class="input" cellspacing="0" cellpadding="0" width="100%">
                    <tr>
                        <td colspan="2">
                            <b>Type of Payment</b>
                            <asp:RequiredFieldValidator ID="RequiredFieldValidator6" runat="server" Text="X"
                                Font-Size="Medium" Font-Bold="true" ControlToValidate="radmisapp" ErrorMessage="Type of Payment is required." />
                        </td>
                    </tr>
                    <tr>
                        <td valign="top" width="200">
                            <asp:RadioButtonList ID="radmisapp" runat="server" AutoPostBack="True" RepeatDirection="Vertical">
                                <asp:ListItem Value="0" Text="Personal Check" />
                                <asp:ListItem Value="1" Text="Money Order" />
                                <asp:ListItem Value="2" Text="Payment Center" />
                                <asp:ListItem Value="3" Text="Credit/Debit Card" />
                                <asp:ListItem Value="4" Text="EFT Payment" />
                                <asp:ListItem Value="5" Text="Cash Payment" />
                            </asp:RadioButtonList>
                        </td>
                        <td valign="top">
                            <!--Personal Check -->
                            <asp:Panel ID="pnlchk" runat="server">
                                <table>
                                    <tr>
                                        <td width="150">Check Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txtPersonalCheckNumber" runat="server" Width="160" MaxLength="7"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPersonalCheckNumber" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ControlToValidate="txtPersonalCheckNumber"
                                                ErrorMessage="Check Number is required." />
                                            <asp:RegularExpressionValidator ID="revPersonalCheckNumber" runat="server" ControlToValidate="txtPersonalCheckNumber"
                                                Display="Dynamic" ErrorMessage="Check Number must be numeric" Font-Bold="true"
                                                Font-Size="Medium" Text="X" ValidationExpression="^\d*$" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Routing Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txtPersonalCheckRTN" runat="server" Width="160" MaxLength="9"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPersonalCheckRTN" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" ControlToValidate="txtPersonalCheckRTN" ErrorMessage="Routing Number is required." />
                                            <asp:RegularExpressionValidator ID="revPersonalCheckRTN" runat="server" Text="X"
                                                ErrorMessage="Invalid Routing Number" Font-Bold="true" Font-Size="Medium" ValidationExpression="^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$"
                                                ControlToValidate="txtPersonalCheckRTN" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Account Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txtPersonalCheckAccount" runat="server" Width="160" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvPersonalCheckAccount" runat="server" Text="X"
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ErrorMessage="Account Number is required."
                                                ControlToValidate="txtPersonalCheckAccount" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntPersonalCheckAmount" Width="160" runat="server"
                                                EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" Type="Currency" />
                                            <asp:RequiredFieldValidator ID="rfvPersonalCheckAmount" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Payment Amount is required."
                                                ControlToValidate="rntPersonalCheckAmount" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date Cleared:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdpPersonalCheckDateCleared" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator5" runat="server" Text="X"
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ErrorMessage="Date Cleared is required."
                                                ControlToValidate="rdpPersonalCheckDateCleared" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!--Money Order -->
                            <asp:Panel ID="pnlmonord" runat="server">
                                <table>
                                    <tr>
                                        <td width="150">Money Order Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txtmonord1" runat="server" Width="160"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator12" runat="server" Text="X"
                                                ErrorMessage="Money Order Number is required." Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" ControlToValidate="txtmonord1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntMonord2" Width="160" runat="server" Type="Currency" EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                                            <asp:RequiredFieldValidator ID="rfvMonord2" runat="server" Text="X" ErrorMessage="Payment Amount is required."
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="rntMonord2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date Mailed:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdpmonord3" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="rfvmonord3" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Date Mailed is required."
                                                ControlToValidate="rdpmonord3" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!--Payment Center -->
                            <asp:Panel ID="pnlpctr" runat="server">
                                <table>
                                    <tr>
                                        <td width="150">WOW! Acct Number from receipt:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txtpctr1" runat="server" Width="156" MaxLength="16"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator15" runat="server" Text="X"
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtpctr1" /><asp:RegularExpressionValidator
                                                    ID="RegularExpressionValidator2" runat="server" ControlToValidate="txtpctr1"
                                                    Text="X" Font-Bold="true" Font-Size="Medium" Display="Dynamic" ValidationExpression="^\d{8}$"
                                                    ErrorMessage="Account Number must be an 8 digit number." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Customer has Terminal ID Number?:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="droptermid" runat="server" Width="160" AutoPostBack="True">
                                                <asp:ListItem Value="Select One" />
                                                <asp:ListItem Value="Yes" />
                                                <asp:ListItem Value="No" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator16" runat="server" Text="X"
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="droptermid"
                                                InitialValue="Select One" />
                                        </td>
                                    </tr>
                                    <asp:Panel runat="server" Visible="false" ID="pnlTermID">
                                        <tr>
                                            <td>
                                                <asp:Label ID="lbltermid" runat="server"></asp:Label>
                                            </td>
                                            <td>
                                                <asp:TextBox autocomplete="off" ID="txtpctr2" runat="server" Width="160"></asp:TextBox>
                                                <asp:RequiredFieldValidator ID="rfvpctr2" runat="server" Text="X" Font-Bold="true"
                                                    Font-Size="Medium" Display="Dynamic" ControlToValidate="txtpctr2" />
                                                <asp:RegularExpressionValidator ID="revpctr2" runat="server" ControlToValidate="txtpctr2"
                                                    Text="X" Font-Bold="true" Font-Size="Medium" Display="Dynamic" ValidationExpression="^(IL|MI|IN|OH|GA).{4}$"
                                                    ErrorMessage="Invalid Terminal ID" />
                                            </td>
                                        </tr>
                                    </asp:Panel>
                                    <tr>
                                        <td>Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntPctr3" Width="160" runat="server" Type="Currency" EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator18" runat="server" Text="X"
                                                ErrorMessage="Payment Amount is required." Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" ControlToValidate="rntPctr3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Date:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdppctr4" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="rfvpctr4" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Payment Date is required."
                                                ControlToValidate="rdppctr4" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!-- Debit/Credit Card -->
                            <asp:Panel ID="pnlcred" runat="server">
                                <table>
                                    <tr>
                                        <td colspan="2" style="color: red;">Please ask for only the first 6 and last 4 digits to ensure the security of the customer’s credit card number.</td>
                                    </tr>
                                    <tr>
                                        <td width="150">Card Number:
                                        </td>
                                        <td>
                                            <telerik:RadMaskedTextBox ID="txtCCCardNumber" runat="server" Mask="#### ##XX XXXX ####" Autocomplete="off"></telerik:RadMaskedTextBox>
                                            <%--<asp:TextBox autocomplete="off" ID="txtCCCardNumber" runat="server" Width="160" MaxLength="16"></asp:TextBox>
                                            --%><asp:RequiredFieldValidator ID="RequiredFieldValidator20" runat="server" Text="X"
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="txtCCCardNumber" />
                                            <asp:CustomValidator runat="server" Text="X" EnableClientScript="false" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ControlToValidate="txtCCCardNumber" ErrorMessage="Invalid Credit Card Number"
                                                ID="cvCreditCard" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Expiration Date (MM/YYYY Format):
                                        </td>
                                        <td>
                                            <telerik:RadMonthYearPicker ID="rmypCCExp" runat="server" Autocomplete="off"></telerik:RadMonthYearPicker>
                                            <%-- <asp:TextBox autocomplete="off" ID="txtcred2" runat="server" Width="160"></asp:TextBox>--%>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator21" runat="server" Text="X"
                                                ErrorMessage="Expiration Date is required." Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" ControlToValidate="rmypCCExp" />
                                            <%--     <asp:RegularExpressionValidator ID="RegularExpressionValidator1" runat="server" Text="X"
                                                ErrorMessage="Invalid Date." Font-Bold="true" Font-Size="Medium" Display="Dynamic"
                                                ControlToValidate="txtcred2" ValidationExpression="\d{1,2}/\d{4}" />--%>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td width="150">Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntCred3" Width="160" runat="server" Type="Currency" EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                                            <asp:RequiredFieldValidator ID="rfvcred3" runat="server" Text="X" ErrorMessage="Payment Amount is required."
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="rntCred3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date Cleared:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdpcred4" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="rfvcred4" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Date Cleared is required."
                                                ControlToValidate="rdpcred4" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Authorization Number:
                                        </td>
                                        <td>
                                            <!-- Carl Rhoades - 03JUN14 - Increased maxlength from 8 to 12 -->
                                            <asp:TextBox ID="txtcredauth" runat="server" Width="160" MaxLength="12"></asp:TextBox>
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!--EFT Payment -->
                            <asp:Panel ID="pnleft" runat="server">
                                <table>
                                    <tr>
                                        <td width="150">Routing Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txteft1" runat="server" Width="160" MaxLength="9"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="rfvtxteft1" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Routing Number Required" ControlToValidate="txteft1" />
                                            <asp:RegularExpressionValidator ID="revtxteft1" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Invalid Routing Number" ValidationExpression="^((0[0-9])|(1[0-2])|(2[1-9])|(3[0-2])|(6[1-9])|(7[0-2])|80)([0-9]{7})$"
                                                ControlToValidate="txteft1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Account Number:
                                        </td>
                                        <td>
                                            <asp:TextBox autocomplete="off" ID="txteft2" runat="server" Width="160"></asp:TextBox>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator24" runat="server" Text="X"
                                                ErrorMessage="Account Number is required." Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" ControlToValidate="txteft2" />
                                            <asp:RegularExpressionValidator ID="RegularExpressionValidator9" runat="server" ControlToValidate="txteft2"
                                                Text="X" Font-Bold="true" Font-Size="Medium" Display="Dynamic" ValidationExpression="^\d*$"
                                                ErrorMessage="Account Number must be numeric." />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntEft3" Width="160" runat="server" Type="Currency" EmptyMessage="Payment Amount" MinValue="0.01" MaxValue="9999.99" />
                                            <asp:RequiredFieldValidator ID="rfveft3" runat="server" Text="X" ErrorMessage="Payment Amount is required."
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="rntEft3" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Date Cleared:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdpeft4" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="rfveft4" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Date Cleared is required."
                                                ControlToValidate="rdpeft4" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Authorization Number:
                                        </td>
                                        <td>
                                            <!-- Carl Rhoades - 03JUN14 - Increased maxlength from 8 to 12 -->
                                            <asp:TextBox ID="txteftAuth" runat="server" Width="160" MaxLength="12"></asp:TextBox>

                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <!-- Cash Payment -->
                            <asp:Panel ID="pnlcash" runat="server">
                                <table>
                                    <tr>
                                        <td width="150">Payment Amount:
                                        </td>
                                        <td>
                                            <telerik:RadNumericTextBox ID="rntCash1" Width="160" runat="server" MaxValue="9999.99"
                                                MinValue="0.01" Type="Currency" />
                                            <asp:RequiredFieldValidator ID="rfvcash1" runat="server" Text="X" ErrorMessage="Payment Amount is required."
                                                Font-Bold="true" Font-Size="Medium" Display="Dynamic" ControlToValidate="rntCash1" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Date:
                                        </td>
                                        <td>
                                            <telerik:RadDatePicker runat="server" ID="rdpcash2" Calendar-ShowRowHeaders="false" />
                                            <asp:RequiredFieldValidator ID="rfvcash2" runat="server" Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" ErrorMessage="Payment Date is required."
                                                ControlToValidate="rdpcash2" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>Payment Location:
                                        </td>
                                        <td>
                                            <asp:DropDownList ID="dropcash" runat="server" Width="160">
                                                <asp:ListItem Value="Select One" />
                                                <asp:ListItem Value="Payment at Door" />
                                                <asp:ListItem Value="Payment at WOW Office" />
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator ID="RequiredFieldValidator29" runat="server" Text="X"
                                                ErrorMessage="Payment Location is required." Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" ControlToValidate="dropcash" InitialValue="Select One" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">Comments: <i>(Max 500 Characters)</i><br />
                            <asp:TextBox autocomplete="off" ID="txtmisappcomm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                MaxLength="500"></asp:TextBox>
                            <asp:CustomValidator runat="server" ClientValidationFunction="NOCCValidate" ID="cvComments"
                                ErrorMessage="Credit card and bank routing numbers are not allowed in the comments."
                                Text="X" ToolTip="Credit card bank routing numbers are not allowed in the comments."
                                ControlToValidate="txtmisappcomm" CssClass="ValidatorText" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnmisappsend" OnClick="SendIt" runat="server" Width="150" Text="Submit"></asp:Button>
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
