<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="NameChange.aspx.vb" MaintainScrollPositionOnPostback="true"
    Inherits="GeneralForms.ARNameChange" %>

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
            Account Research - Name Change</div>
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
                            Submit a Name Change form to request a change to the name on active WOW! accounts.
                            A name change is defined as a change to the responsible party on the account or
                            a legal change in the customer’s name due to marriage, divorce or death. Use this
                            form also for a correction of up to 3 letters to correct the spelling of a name.
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
                        <asp:label ID="lblDivision" runat="server" ReadOnly="True" Width="1" Visible="False"></asp:label>
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
        <asp:Panel ID="pnlNameChangeMain" runat="server">
            <table class="input" cellspacing="0" cellpadding="0" width="100%">
                <tr>
                    <td>
                        <div class="sectionTitle">
                            Name Change Type</div>
                        <asp:RadioButtonList ID="rblNameCorrChange" runat="server" AutoPostBack="True" RepeatDirection="Vertical">
                            <asp:ListItem Value="Marriage">Marriage (A change in the ownership of an account due to Marriage)</asp:ListItem>
                            <asp:ListItem Value="Divorce">Divorce (A change in the ownership of an account due to Divorce)</asp:ListItem>
                            <asp:ListItem Value="Death">Death (A change in the ownership of an account due to Death)</asp:ListItem>
                            <asp:ListItem Value="Correction">Correction (A change of up to 3 letters in the spelling of a name.  Ex.: Smithe to Smith)</asp:ListItem>
                            <asp:ListItem Value="Legal">Legal Name Change (The customer can provide legal documentation for their name changing. Ex.: John Smith to Joe Smith)</asp:ListItem>
                        </asp:RadioButtonList>
                        <hr />
                    </td>
                </tr>
                <asp:Panel ID="pnlNameChange" runat="server" Visible="False">
                    <asp:Panel ID="pnlFamilyRelation" runat="server" Visible="false">
                        <tr>
                            <td>
                                Is the new customer a Spouse or Direct Family member?<br />
                                <asp:RadioButtonList runat="server" ID="rblfamilyRelation" RepeatDirection="Horizontal"
                                    AutoPostBack="true">
                                    <asp:ListItem Text="Yes" Value="Yes" />
                                    <asp:ListItem Text="No" Value="No" />
                                </asp:RadioButtonList>
                                <asp:Label runat="server" ID="lblFamilyRelation" ForeColor="red" Visible="False">Name Changes are not permitted.</asp:Label>
                            </td>
                        </tr>
                    </asp:Panel>
                    <asp:Panel ID="pnlNameChangeData" runat="server" Visible="False">
                        <tr>
                            <td>
                                <table>
                                    <tr>
                                        <td>
                                            Current Customer Name:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCurrName" runat="server" />
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator32" runat="server" ControlToValidate="txtCurrName"
                                                ErrorMessage="Current Customer Name is required." Text="X" Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Requested Name Change:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewName" runat="server" />
                                            <asp:RequiredFieldValidator ID="Requiredfieldvalidator33" runat="server" ControlToValidate="txtNewName"
                                                ErrorMessage="Requested Name Change is required." Text="X" Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Relationship:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtRelationship" runat="server" /><asp:RequiredFieldValidator ID="rfvRelationship"
                                                runat="server" ControlToValidate="txtRelationship" ErrorMessage="Relationship is required."
                                                Text="X" Font-Bold="true" Font-Size="Medium" Display="Dynamic" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            New Customer SSN:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewSSN" runat="server" />
                                            <asp:RequiredFieldValidator runat="server" ID="Requiredfieldvalidator34" ControlToValidate="txtNewSSN"
                                                ErrorMessage="New Customer SSN is required." Text="X" Font-Bold="true" Font-Size="Medium"
                                                Display="Dynamic" />
                                            <asp:RegularExpressionValidator runat="server" ID="revSSN" ControlToValidate="txtNewSSN"
                                                ValidationExpression="\d{9}">SSN must be 9 digits.</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            New Customer Phone Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtNewPhone" runat="server" />
                                            <asp:RequiredFieldValidator runat="server" ID="Requiredfieldvalidator35" ControlToValidate="txtNewPhone"
                                                ErrorMessage="New Customer Phone Number is required." Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" />
                                            <asp:RegularExpressionValidator runat="server" ID="revPhone" ControlToValidate="txtNewPhone"
                                                ValidationExpression="\d{10}">Phone Number must be 10 digits.</asp:RegularExpressionValidator>
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            Alternate Contact Number:
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtAltNum" runat="server" MaxLength="10"></asp:TextBox>
                                            <asp:RequiredFieldValidator runat="server" ID="rfvAltNum" ControlToValidate="txtAltNum"
                                                ErrorMessage="Alternate Contact Number is required." Text="X" Font-Bold="true"
                                                Font-Size="Medium" Display="Dynamic" />
                                        </td>
                                    </tr>
                                </table>
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblCertificate" Font-Bold="True" ForeColor="Red" runat="server"><br />Advise the customer to fax a copy of the death certificate with the WOW! Account Number to 1-888-268-5859.<br /></asp:Label>
                                <br />
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                Comments:<br />
                                <asp:TextBox ID="txtNameChangeComm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                    MaxLength="500"></asp:TextBox>
                            </td>
                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnNameChangeSubmit" OnClick="SendIt" runat="server" Width="150"
                                    Text="Submit"></asp:Button>
                            </td>
                        </tr>
                    </asp:Panel>
                </asp:Panel>
                <asp:Panel ID="pnlNameCorr" runat="server" Visible="False">
                    <tr>
                        <td>
                            Current Customer Name:<br />
                            <asp:TextBox ID="txtCurrNameCorr" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrNameCorr" runat="server" Text="X" Font-Bold="true" Font-Size="Medium"
                                ErrorMessage="Current Customer Name is required." Display="Dynamic" ControlToValidate="txtCurrNameCorr" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            Correct Spelling:<br />
                            <asp:TextBox ID="txtNewNameCorr" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewNameCorr" runat="server" Text="X"
                                ErrorMessage="Correct Spelling is required." Font-Bold="true" Font-Size="Medium"
                                Display="Dynamic" ControlToValidate="txtNewNameCorr" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Comments:<br />
                            <asp:TextBox ID="txtNameCorrComm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnNameCorrSubmit" OnClick="SendIt" runat="server" Width="150" Text="Submit">
                            </asp:Button>
                        </td>
                    </tr>
                </asp:Panel>
                <asp:Panel ID="pnlLegal" runat="server" Visible="False">
                    <tr>
                        <td>
                            Current Customer Name:<br />
                            <asp:TextBox ID="txtCurrNameLegal" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvCurrNameLegal" runat="server" Text="X" Font-Bold="true" Font-Size="Medium"
                                ErrorMessage="Current Customer Name is required." Display="Dynamic" ControlToValidate="txtCurrNameLegal" />
                        </td>
                    </tr>
                    <tr>
                        <td>
                            New Customer Name:<br />
                            <asp:TextBox ID="txtNewNameLegal" runat="server"></asp:TextBox>
                            <asp:RequiredFieldValidator ID="rfvNewNameLegal" runat="server" Text="X"
                                ErrorMessage="Correct Spelling is required." Font-Bold="true" Font-Size="Medium"
                                Display="Dynamic" ControlToValidate="txtNewNameLegal" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            Comments:<br />
                            <asp:TextBox ID="txtNameLegalComm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"
                                MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblLegal" Font-Bold="True" ForeColor="Red" runat="server"><br />Advise the customer to fax a copy of the legal documentation showing their name change, with the WOW! Account Number, to 1-888-268-5859.<br /></asp:Label>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnNameLegalSubmit" OnClick="SendIt" runat="server" Width="150" Text="Submit">
                            </asp:Button>
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
