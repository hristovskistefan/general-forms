<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="Unauthorized.aspx.vb"
    Inherits="GeneralForms.Unauthorized" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml" style="overflow: hidden;">
<head runat="server">
    <link href="/stylesheets/StyleSheet.css" rel="stylesheet" type="text/css" />
    <link href="/report_style.css" rel="stylesheet" type="text/css" />
    <title>General Forms</title>
    <script language="JavaScript" type="text/javascript">
<!--
        function framebreakout() {
            if (top.location != location) {
                top.location.href = document.location.href;
            }
        }
//-->
    </script>
</head>
<body style="overflow: hidden;" onload="framebreakout()">
    <form id="form1" runat="server">
    <div class="title">
        <div class="titleimage">
        </div>
        <div class="titletext">
            General Forms</div>
    </div>
    <div id="Container-Content">
        <div class="sectionTitle">
            Unauthorized</div>
        <b>You are not logged in correctly!</b><br />
        <br />
        The WOW General Forms site requires you to be logged in with a valid <b>WOW! Username</b>
        and <b>password</b>. This is done automatically at the CCC. If you are at another
        location, this is done by using <i>Stop Sign</i> or manually typing in your credentials
        when prompted.
        <br />
        <br />
        You are seeing this message because we were not able to validate the ID you are
        logged in as.  Either your login does not exist in Employee Master or is marked as inactive.<br />
        <br />
        Please check the following:<br />
        <ul>
            <li><b>Sitel Users:</b> Verify that your <i>Stop Sign</i> username is not <b>cl_alabama</b>
                or <b>cl_texas</b>. All Sitel agents must now log into web pages using their <i>stl_username@wideopenwest.com</i>
                credentials. Contact your local tech support for assistance if needed.</li>
            <li>If you are in training, your credentials may not be in our <i>EmployeeMaster</i>
                database yet. Notify your trainer to import you via the New Hire Management Interface
                (NHMI)</li>
            <li>If you are still receiving this error and believe your credentials are valid, please
                contact RM to check that your login is correct and active in the <i>EmployeeMaster</i> database.</li>
        </ul>
    </div>
    </form>
</body>
</html>
