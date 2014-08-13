<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Bankruptcy.aspx.vb" Inherits="GeneralForms.Bankruptcy" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>General Forms - Customer Bankruptcy Form</title>
</head>
<body>
    <form id="Form2" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div class="Container-Heading clearfix">
        <div id="Container-Title">
            Bankruptcy Inquiry Form</div>
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
        <asp:Panel ID="pnlMain" runat="server">
            <table class="input">
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
                    <td colspan="3">
                        <asp:TextBox autocomplete="off" ID="txtAcct" runat="server" Width="80" MaxLength="8" AutoPostBack="True" ></asp:TextBox>
                        <asp:ImageButton ID="ibGo" CausesValidation="false" runat="server" ImageUrl="~/images/SearchGo.gif" />
                        <asp:RegularExpressionValidator ID="revAccount" runat="server" Text="X" ControlToValidate="txtAcct"
                            Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                            ErrorMessage="The account number entered is not valid" />
                        <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="txtAcct"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Account Required"
                            EnableClientScript="False" />
                    </td>
                </tr>
                <tr>
                    <td>
                        First Name:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtCFname" runat="server" Width="160" />
                        <asp:RequiredFieldValidator ControlToValidate="txtCFname" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer first name is required." />
                    </td>
                    <td>
                        Last Name:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtCLname" runat="server" Width="160" />
                        <asp:RequiredFieldValidator ControlToValidate="txtCLname" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer last name is required." />
                    </td>
                </tr>
                <tr>
                    <td colspan="4">
                        <asp:Button runat="server" Text="Continue..." ID="btnContinue" Width="125" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <!--
	FORM PANEL CONTAINS ALL THE RELEVANT INFORMATION CONTROLS
-->
        <asp:Panel ID="pnlForm" runat="server">
            <table class="input" width="600" cellpadding="2" cellspacing="0">
                <tr>
                    <td colspan="2">
                        <hr size="1" width="98%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Case Number:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtCaseNumber" runat="server" Width="160" />
                        <asp:RequiredFieldValidator ControlToValidate="txtCaseNumber" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Case number is required." />
                    </td>
                </tr>
                <tr>
                    <td>
                        Chapter Filed:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlChapter" runat="server" Width="160">
                            <asp:ListItem Selected="True" Value="Select One" />
                            <asp:ListItem Value="Chapter 7" />
                            <asp:ListItem Value="Chapter 11" />
                            <asp:ListItem Value="Chapter 13" />
                            <asp:ListItem Value="Chapter 15" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlChapter" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Chapter filed is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td>
                        State Filed:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlState" runat="server" Width="160">
                            <asp:ListItem Selected="True" Value="Select One" />
                            <asp:ListItem Value="AL" Text="Alabama" />
                            <asp:ListItem Value="AK" Text="Alaska" />
                            <asp:ListItem Value="AZ" Text="Arizona" />
                            <asp:ListItem Value="AR" Text="Arkansas" />
                            <asp:ListItem Value="CA" Text="California" />
                            <asp:ListItem Value="CO" Text="Colorado" />
                            <asp:ListItem Value="CT" Text="Connecticut" />
                            <asp:ListItem Value="DE" Text="Delaware" />
                            <asp:ListItem Value="FL" Text="Florida" />
                            <asp:ListItem Value="GA" Text="Georgia" />
                            <asp:ListItem Value="HI" Text="Hawaii" />
                            <asp:ListItem Value="ID" Text="Idaho" />
                            <asp:ListItem Value="IL" Text="Illinois" />
                            <asp:ListItem Value="IN" Text="Indiana" />
                            <asp:ListItem Value="IA" Text="Iowa" />
                            <asp:ListItem Value="KS" Text="Kansas" />
                            <asp:ListItem Value="KY" Text="Kentucky" />
                            <asp:ListItem Value="LA" Text="Louisiana" />
                            <asp:ListItem Value="ME" Text="Maine" />
                            <asp:ListItem Value="MD" Text="Maryland" />
                            <asp:ListItem Value="MA" Text="Massachusetts" />
                            <asp:ListItem Value="MI" Text="Michigan" />
                            <asp:ListItem Value="MN" Text="Minnesota" />
                            <asp:ListItem Value="MS" Text="Mississippi" />
                            <asp:ListItem Value="MO" Text="Missouri" />
                            <asp:ListItem Value="MT" Text="Montana" />
                            <asp:ListItem Value="NE" Text="Nebraska" />
                            <asp:ListItem Value="NH" Text="New Hampshire" />
                            <asp:ListItem Value="NJ" Text="New Jersey" />
                            <asp:ListItem Value="NM" Text="New Mexico" />
                            <asp:ListItem Value="NY" Text="New York" />
                            <asp:ListItem Value="NV" Text="Nevada" />
                            <asp:ListItem Value="NC" Text="North Carolina" />
                            <asp:ListItem Value="ND" Text="North Dakota" />
                            <asp:ListItem Value="OH" Text="Ohio" />
                            <asp:ListItem Value="OK" Text="Oklahoma" />
                            <asp:ListItem Value="OR" Text="Oregon" />
                            <asp:ListItem Value="PA" Text="Pennsylvania" />
                            <asp:ListItem Value="RI" Text="Rhode Island" />
                            <asp:ListItem Value="SC" Text="South Carolina" />
                            <asp:ListItem Value="SD" Text="South Dakota" />
                            <asp:ListItem Value="TN" Text="Tennessee" />
                            <asp:ListItem Value="TX" Text="Texas" />
                            <asp:ListItem Value="UT" Text="Utah" />
                            <asp:ListItem Value="VT" Text="Vermont" />
                            <asp:ListItem Value="VA" Text="Virginia" />
                            <asp:ListItem Value="WA" Text="Washington" />
                            <asp:ListItem Value="WV" Text="West Virginia" />
                            <asp:ListItem Value="WI" Text="Wisconson" />
                            <asp:ListItem Value="WY" Text="Wyoming" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlState" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="State filed is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Customer faxing in documentation?
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlFaxingDocumentation" runat="server" Width="160">
                            <asp:ListItem Selected="True" Value="Select One" />
                            <asp:ListItem Value="Yes" />
                            <asp:ListItem Value="No" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlFaxingDocumentation" runat="server"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Faxing Documentation selection is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <div class="infobox">
                            Customer may fax documentation to Fax #: 1-888-268-5859</div>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr size="1" width="98%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Customer has questions about paying balance after filing bankruptcy?<br />
                        <asp:DropDownList ID="ddlQuestAfter" runat="server" Width="160">
                            <asp:ListItem Value="Select One" Selected="True" />
                            <asp:ListItem Value="Yes" />
                            <asp:ListItem Value="No" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlQuestAfter" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer has questions selection is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Customer's service has been interrupted or disconnected?<br />
                        <asp:DropDownList ID="ddlDisconnected" runat="server" Width="160">
                            <asp:ListItem Value="Select One" Selected="True" />
                            <asp:ListItem Value="Yes" />
                            <asp:ListItem Value="No" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlDisconnected" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer service interrupted selection is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Customer has questions about retaining service after filing bankruptcy?<br />
                        <asp:DropDownList ID="ddlRetainService" runat="server" Width="160">
                            <asp:ListItem Value="Select One" Selected="True" />
                            <asp:ListItem Value="Yes" />
                            <asp:ListItem Value="No" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlRetainService" runat="server" Display="Dynamic"
                            Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer retaining service selection is required."
                            InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr size="1" width="98%" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Would customer like their attorney contacted?<br />
                        <asp:DropDownList ID="ddlAttorney" runat="server" Width="160" AutoPostBack="true">
                            <asp:ListItem Value="Select" Selected="True" />
                            <asp:ListItem Value="Yes" />
                            <asp:ListItem Value="No" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ControlToValidate="ddlAttorney" runat="server" Text="X"
                            Font-Size="Medium" Font-Bold="true" ForeColor="Red" ErrorMessage="Contact Attorney Selection is required."
                            InitialValue="Select" />
                    </td>
                    <td>
                        <div class="infobox">
                            Please inform the customer that their attorney can contact the WOW! Bankruptcy Dept
                            directly @ 719-388-1018</div>
                    </td>
                </tr>
                <tr>
                    <td>
                        Attorney's Name:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtAttorneyName" runat="server" Width="160" />
                        <asp:RequiredFieldValidator ID="rfvAttorneyName" ControlToValidate="txtAttorneyName"
                            runat="server" Text="X" Font-Size="Medium" Font-Bold="true" Display="Dynamic"
                            ErrorMessage="Attorney name is required." Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td>
                        Attorney's Phone Number:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtAttorneyNumber" runat="server" Width="160" MaxLength="10" />
                        <asp:RequiredFieldValidator ID="rfvAttorneyNumber" ControlToValidate="txtAttorneyNumber"
                            runat="server" Text="X" Font-Size="Medium" Font-Bold="true" Display="Dynamic"
                            ErrorMessage="Attorney phone number is required." Enabled="false" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <hr size="1" width="98%" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Comments: <i>(Max 500 Characters)</i><br />
                        <asp:TextBox autocomplete="off" ID="txtComments" runat="server" TextMode="MultiLine" Rows="5" Width="100%"
                            MaxLength="500" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button Text="Submit" ID="btnSubmit" runat="server" Width="160" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:ValidationSummary HeaderText="Please correct the following:" ID="ValidationSummary1"
                            runat="server" />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel runat="server" ID="pnlThanks">
            <center>
                <table cellpadding="0" cellspacing="0" class="input" width="400">
                    <tr>
                        <td align="center">
                            This information was submitted successfully. Please click the button below to return
                            to the form.
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <hr width="95%" />
                        </td>
                    </tr>
                    <tr>
                        <td align="center">
                            <asp:Button runat="server" Width="100" Text="Return" ID="btnReturn" />
                        </td>
                    </tr>
                </table>
            </center>
        </asp:Panel>
        <asp:Panel ID="pnlNoLongerUsed" runat="server" Visible="false">
            This form is no longer in use.<br />
            If a customer is requesting information about an his/her bankruptcy, please transfer
            to 1018.<br />
            If a customer is filing bankruptcy and would like to notify WOW!, please transfer
            to 1018.<br />
            If you have any questions, please see your supervisor.
        </asp:Panel>
    </div>
    <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
