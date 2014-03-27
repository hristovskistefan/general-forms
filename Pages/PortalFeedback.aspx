<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PortalFeedback.aspx.vb"
    Inherits="GeneralForms.PortalFeedback" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <link href="/stylesheets/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/report_style.css" rel="stylesheet" type="text/css" />
    <title>General Forms - Portal Feedback</title>
    <style type="text/css">
        .input
        {
            font: arial small;
        }
        /* for IE/Mac */</style>
    <!--[if IE]>
<style type="text/css">
  .clearfix {
    zoom: 1;     /* triggers hasLayout */
    display: block;     /* resets display for IE/Win */
    }  /* Only IE can see inside the conditional comment
    and read this CSS rule. Don't ever use a normal HTML
    comment inside the CC or it will close prematurely. */
</style>
<![endif]-->
</head>
<body style="overflow: hidden;">
    <form id="Form1" runat="server">
    <telerik:RadScriptManager ID="RadScriptManager1" runat="server">
    </telerik:RadScriptManager>
    <div id="Container-Heading" class="clearfix">
        <div id="Container-Title">
            Portal Feedback Form</div>
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
        <asp:Panel ID="pnlform" runat="server">
            <table class="input" cellspacing="0" cellpadding="2" width="450">
                <tr>
                    <td>
                        Advanced:
                    </td>
                    <td>
                        <asp:CheckBox ID="chkAdvanced" runat="server" AutoPostBack="True"></asp:CheckBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Your Name:
                    </td>
                    <td>
                        <asp:TextBox ID="txtname" runat="server" Width="175"></asp:TextBox>
                    </td>
                </tr>
                <tr>
                    <td>
                        Your ICOMS ID:
                    </td>
                    <td>
                        <asp:TextBox ID="txtcsg" runat="server" Width="100" MaxLength="3"></asp:TextBox>
                    </td>
                </tr>
                <asp:Panel ID="pnlInstruct" runat="server" Visible="False">
                    <tr>
                        <td colspan="2">
                            <ul>
                                <li>7 Business Days are required for the following requests: New Process, New Article,
                                    New Portal Section, New User Created, Announcement</li>
                                <li>3 Business Days are required prior to a Meeting or Training session to be scheduled.
                                </li>
                            </ul>
                        </td>
                    </tr>
                </asp:Panel>
                <tr>
                    <td>
                        Feedback Type:
                    </td>
                    <td>
                        <asp:DropDownList ID="droptype" runat="server" Width="175">
                            <asp:ListItem Value="Select One" Selected="False" />
                            <asp:ListItem Value="Article Suggestion" />
                            <asp:ListItem Value="Article Correction" />
                            <asp:ListItem Value="Article Update" />
                            <asp:ListItem Value="Content Suggestion" />
                            <asp:ListItem Value="Fun Stuff Suggestion" />
                            <asp:ListItem Value="Fast Access Suggestion" />
                            <asp:ListItem Value="Learn & Win" />
                            <asp:ListItem Value="Positive Feedback" />
                            <asp:ListItem Value="Improvement Suggestion" />
                            <asp:ListItem Value="Portal Errors" />
                            <asp:ListItem Value="Other Information" />
                            <asp:ListItem Value="Questions" />
                        </asp:DropDownList>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator1" runat="server" Text="<font size=3><b>X</b></font>"
                            InitialValue="Select One" ControlToValidate="droptype"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        Comments: <i>(Max 500 characters)</i><br />
                        <asp:TextBox ID="txtcomm" runat="server" Columns="50" Rows="5" TextMode="MultiLine"></asp:TextBox>
                        <asp:RequiredFieldValidator ID="RequiredFieldValidator2" runat="server" Text="<font size=3><b>X</b></font>"
                            ControlToValidate="txtcomm"></asp:RequiredFieldValidator>
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:Button ID="btnsend" runat="server" Width="125" Text="Submit"></asp:Button>
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Panel ID="pnlthx" runat="server">
            <table class="input" cellspacing="0" cellpadding="2" width="450">
                <tr>
                    <td>
                        Your request was submitted successfully. We will provide feedback to you or address
                        your issue(s) as soon as possible.<br />
                        <br />
                        Thank you
                    </td>
                </tr>
                <tr>
                    <td align="center">
                        <asp:Button ID="btncont" runat="server" Width="125" Text="Continue..." />
                    </td>
                </tr>
            </table>
        </asp:Panel>
        <asp:Label ID="lblerr" runat="server" ForeColor="red" Width="450" />
        <asp:TextBox ID="txtuser" runat="server" Visible="False" />
    </div>
    <User:MB ID="MB" runat="server" />
    </form>
</body>
</html>
