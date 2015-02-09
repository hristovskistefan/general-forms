<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="PageTemplate.aspx.vb"
    Inherits="GeneralForms.PageTemplate" %>

<%@ Register Src="~/Controls/MessageBox.ascx" TagName="MB" TagPrefix="User" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head id="Head1" runat="server">
    <link href="/stylesheets/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/report_style.css" rel="stylesheet" type="text/css" />
    <title>General Forms</title>
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
            Loss Prevention Form</div>
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
    </div>
    <User:MB ID="MB" runat="server" />
     </form>
</body>
</html>
