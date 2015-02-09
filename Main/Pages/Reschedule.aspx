<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Reschedule.aspx.vb" Inherits="GeneralForms.Reschedule" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <title>General Forms - Cancel/Reschedule Job</title>
</head>
<body style="overflow: hidden;">
    <form id="Form1" runat="server">
        <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
        </telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Same Day Cancel/Reschedule Job
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
            <table class="input" cellpadding="2" cellspacing="0">
                <tr>
                    <td>Date of Scheduled Job:
                    </td>
                    <td>
                        <telerik:RadDatePicker ID="rdpJobDate" runat="server" Width="100px" Calendar-ShowRowHeaders="false"
                            DateInput-DisplayDateFormat="M/d/yyyy" DateInput-DateFormat="M/d/yyyy">
                        </telerik:RadDatePicker>
                        <asp:RequiredFieldValidator ID="rfvJobDate" ControlToValidate="rdpJobDate" runat="server"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Job Date Required" />
                    </td>
                    <td rowspan="5" width="200" valign="top">
                        <div class="infobox">
                            <font color="red"><b>This is a notification tool only. Appointments must be canceled,
                            rescheduled and noted in the customer's ICOMS record. </b></font>
                        </div>
                    </td>
                </tr>
                <tr>
                    <td>Work Order Number:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtWONumber" runat="server" Width="100" MaxLength="9" />
                        <asp:RequiredFieldValidator ID="rfvWONumber" ControlToValidate="txtWONumber" runat="server"
                            Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Job Number Required" />
                    </td>
                </tr>
                <tr>
                    <td>Tech Number:
                    </td>
                    <td>
                        <asp:TextBox autocomplete="off" ID="txtTechNumber" runat="server" Width="100" MaxLength="5" />
                        <asp:RequiredFieldValidator ID="rfvTechNumber" ControlToValidate="txtTechNumber"
                            runat="server" Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium"
                            ErrorMessage="Tech Number Required" />
                    </td>
                </tr>
                <tr>
                    <td>Cancel or Reschedule:
                    </td>
                    <td>
                        <asp:DropDownList ID="ddlCancelReschedule" runat="server" Width="100">
                            <asp:ListItem Value="Select One" Selected="True" />
                            <asp:ListItem Value="Cancel" />
                            <asp:ListItem Value="Reschedule" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="rfvCancelReschedule" ControlToValidate="ddlCancelReschedule"
                            runat="server" Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium"
                            ErrorMessage="Tech Number Required" InitialValue="Select One" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button runat="server" Text="Submit" ID="btnSubmit" />
                    </td>
                </tr>
                <tr>
                    <td colspan="3">
                        <asp:ValidationSummary runat="server" ID="ValidationSummary1" />
                    </td>
                </tr>
            </table>
        </div>
        <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
