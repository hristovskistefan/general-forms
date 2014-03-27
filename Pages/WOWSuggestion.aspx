<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="WOWSuggestion.aspx.vb" Inherits="GeneralForms.WowSuggestion" %>
<%@ Register TagPrefix="User" TagName="MB" Src="~/Controls/MessageBox.ascx" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <title>General Forms</title>
</head>
<body style="overflow: hidden;">
    <form id="form1" runat="server">
    <div class="Container-Heading clearfix">
        <div id="Container-Title">Customer Care Suggestion Box</div>
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
          <asp:Panel ID="pnlForm" runat="server">
            <span style="font-weight: bold">Please type your question or suggestion and click Submit.<br />
                <br />
                This Suggestion box should only be used to submit the following types of feedback:
            </span>
            <ul>
                <li>Operations at the CCC</li>
                <li>Business-related strategies</li>
                <li>Topics related to company visions and goals</li>
                <li>Strategic focus</li>
                <li>Operation efficiencies</li>
                <li>Cost reduction</li>
                <li>Culture Customer Experience</li>
            </ul>
                      <asp:TextBox ID="txtComments" runat="server" TextMode="multiline" MaxLength="500"
                Height="100px" Width="450px"></asp:TextBox>
            <asp:RequiredFieldValidator ID="RequiredFieldValidator3" runat="server" ControlToValidate="txtComments"
                ErrorMessage="X" ValidationGroup="Submit"></asp:RequiredFieldValidator><br />
            <asp:Button ID="btnSubmit" runat="server" Text="Submit" ValidationGroup="Submit" />
        </asp:Panel>
        <asp:Panel ID="pnlSuccess" runat="server" Visible="false">
            <div style="color: Red">
                The suggestion has been successfully submitted.<br />
                <br />
            </div>
            <asp:Button ID="btnReturn" runat="server" Text="Enter Another Suggestion" Width="168px" />
        </asp:Panel>
    </div>
    <User:MB runat="server" ID="MB" />
    </form>
</body>
</html>
