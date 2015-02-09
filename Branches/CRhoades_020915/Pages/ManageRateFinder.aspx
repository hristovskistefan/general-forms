<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="ManageRateFinder.aspx.vb"
    Inherits="GeneralForms.ManageRateFinder" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <telerik:RadScriptManager runat="server" ID="rsmMain" />
    <div class="Container-Heading clearfix">
        <div id="Container-Title">
            Rate Guarantee Management</div>
        <div id="Container-Info" class="DataLabel">
            <span class="DataLabel">Date: </span>
            <asp:Label ID="lblhDate" runat="server" CssClass="Data" />
            <span class="DataLabel">Name: </span>
            <asp:Label ID="lblhName" runat="server" CssClass="Data" />
            <span class="DataLabel">ICOMS ID: </span>
            <asp:Label ID="lblhIcomsID" runat="server" CssClass="Data" />
        </div>
    </div>
    <div class="messageInfo">
        Use the grid below to edit information for current packages. To import data for
        a new year, please use the upload manager below to upload the new packages. Bear
        in mind once you confirm the document, <u>all previous data will be deleted.</u>
    </div>
    <div id="Container-Content">
        Use the following control to upload an excel file containing new package codes.
        <br />
        Please ensure that the column names and tab names match <a href="\\cs-fssvr01\Public">
            the required template.</a>
        <telerik:RadUpload runat="server" AllowedFileExtensions="xls,xlsx" ID="ruMain" InitialFileInputsCount="1">
        </telerik:RadUpload>
    </div>
    </form>
</body>
</html>
