<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SeasonalEntry.aspx.vb"
    Inherits="GeneralForms.SeasonalEntry" %>

<%-- This form has been replaced by a seperate application found in
     inetpub/interweb/Seasonal
     and can be access at
     http://interweb/Seasonal
--%>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>General Forms</title>
</head>
<body>
    <form id="form1" runat="server">
        <telerik:RadScriptManager runat="server" ID="ScriptManager1">
        </telerik:RadScriptManager>
        <div class="Container-Heading clearfix">
            <div id="Container-Title">
                Seasonal Process Request
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
        <User:MB ID="MB" runat="server" />

        <iframe src="http://interweb/Seasonal" width="100%" height="100%"></iframe>

        <ul>
            <li>To access this form through Gooroo, please update bookmarks to: <a href="http://home.wideopenwest.com/WebForms/Pages/Seasonal.aspx" alt="http://home.wideopenwest.com/WebForms/Pages/Seasonal.aspx">http://home.wideopenwest.com/WebForms/Pages/Seasonal.aspx</a>
            </li>
            <li>To directly access this form, please update bookmarks to: <a href="http://interweb/Seasonal" alt="http://interweb/Seasonal">http://interweb/Seasonal</a>
            </li>
        </ul>

    </form>




</body>
</html>
