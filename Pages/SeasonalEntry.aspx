<%@ Page Language="vb" AutoEventWireup="false" CodeBehind="SeasonalEntry.aspx.vb"
    Inherits="GeneralForms.SeasonalEntry" %>

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
        <div id="Container-Content" style="font-size: .9em;">
            <!-- FORM PANEL -->
            <asp:Panel ID="pnlform" runat="server">
                <table class="input" cellpadding="2" cellspacing="0" width="100%" border="0">
                    <tr>
                        <td class="sectionTitle" colspan="5">Customer Information
                        </td>
                    </tr>
                    <tr>
                        <td>Account #:
                        </td>
                        <td colspan="4">
                            <asp:TextBox ID="txtAcct" runat="server" Width="80" MaxLength="8" AutoPostBack="true"></asp:TextBox><asp:ImageButton
                                ID="ibGo" ValidationGroup="vgAcctInfo" CausesValidation="false" runat="server"
                                ImageUrl="~/images/SearchGo.gif" />
                            <asp:RegularExpressionValidator ID="revAccount" runat="server" Text="X" ControlToValidate="txtAcct"
                                Font-Bold="True" Font-Size="Medium" Display="Dynamic" EnableClientScript="false"
                                ErrorMessage="The account number entered is not valid" ValidationGroup="vgAcctInfo" />
                            <asp:RequiredFieldValidator ID="rfvAccount" runat="server" ControlToValidate="txtAcct"
                                Display="Dynamic" Text="X" Font-Bold="True" Font-Size="Medium" ErrorMessage="Customer Account Required"
                                EnableClientScript="false" ValidationGroup="vgAcctInfo" />
                        </td>

                    </tr>
                    <tr>
                        <td style="width: 150px">Customer's First Name:
                        </td>
                        <td style="width: 180px">
                            <asp:TextBox ID="txtFirstName" runat="server" Width="130" />
                            <asp:RequiredFieldValidator ControlToValidate="txtFirstName" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valFirstName" Display="Dynamic" />
                        </td>
                        <td style="width: 40px">&nbsp;
                        </td>
                        <td style="width: 150px">Customer's Last Name:
                        </td>
                        <td style="width: 180px">
                            <asp:TextBox ID="txtLastName" runat="server" Width="130" />
                            <asp:RequiredFieldValidator ControlToValidate="txtLastName" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="valLastName" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sectionTitle" colspan="5">Seasonal Information
                        </td>
                    </tr>
                    <tr>
                        <td>Begin Date:
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="rdpBegin" runat="server" Width="90" />
                            <asp:RequiredFieldValidator ControlToValidate="rdpBegin" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="RequiredFieldValidator1" Display="Dynamic" />
                        </td>
                        <td>&nbsp;
                        </td>
                        <td>End Date:
                        </td>
                        <td>
                            <telerik:RadDatePicker ID="rdpEnd" runat="server" Width="90" />
                            <asp:RequiredFieldValidator ControlToValidate="rdpEnd" runat="server" Text="<font size=3 face=arial><b>X</b></font>"
                                ID="RequiredFieldValidator2" Display="Dynamic" />
                        </td>
                    </tr>
                    <tr>
                        <td class="sectionTitle" colspan="5">Bill-To or AutoPay Information
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:RadioButton ID="rdoBTEPCSG" GroupName="rgPay" OnClick="javascript:PayShow('rdoBTEPCSG')"
                                runat="server" TextAlign="Right" Text="Bill-To or AutoPay is already in ICOMS:" Checked="true" />
                            <br />
                            <asp:RadioButton ID="rdoBT" GroupName="rgPay" OnClick="javascript:PayShow('rdoBT')"
                                runat="server" TextAlign="Right" Text="Bill-To:" />
                            <br />
                            <asp:RadioButton ID="rdoEP" GroupName="rgPay" OnClick="javascript:PayShow('rdoEP')"
                                runat="server" TextAlign="Right" Text="Ezpay:" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-style: none; border-width: 0px; padding: 0px; margin: 0px">
                            <table id="tBT" style="display: none; width: 100%">
                                <tr>
                                    <td>Customer's First Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBTFirstName" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvBTFirstName" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>Customer's Last Name:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtBTLastName" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvBTLastName" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Address 1:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddr1" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvAddr1" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>Address 2:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtAddr2" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvAddr2" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>City:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtCity" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvCity" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>State:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtState" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvState" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                </tr>
                                <tr>
                                    <td>Zip:
                                    </td>
                                    <td>
                                        <asp:TextBox ID="txtZip" runat="server" Width="130" />
                                        <asp:CustomValidator ID="cvZip" runat="server" ClientValidationFunction="rdoCheck"
                                            ErrorMessage="<font size=3 face=arial><b>X</b></font>"></asp:CustomValidator>
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                    <td>&nbsp;
                                    </td>
                                </tr>
                            </table>
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5" style="border-style: none; border-width: 0px; padding: 0px; margin: 0px">
                            <table id="tEP" style="display: none; width: 100%">
                                <tr>
                                    <td style="border-style: none; border-width: 0px; padding: 0px; margin: 0px; width: 100%; height: 75px">You must enter valid Credit Card or EFT information into the Pymt Method tab on
                                    the Account Info screen.
                                    <br />
                                        The Status must be set to active.
                                    <br />
                                        Inform the customer that if a credit card is entered then EZpay will be active on
                                    their next billing cycle.
                                    <br />
                                        EFT could take up to a full billing cycle to be active.<br />
                                    </td>
                                </tr>
                            </table>
                            <%--                            <table id="tEP" style="display:none;width:100%">
                                <tr>
                                    <td class="subSectionTitle" colspan="5">
                                        Ezpay
                                    </td>
                                </tr>
                                <tr>
                                    <td>Method:</td>
                                    <td>
                                        <asp:TextBox ID="txtMethod" runat="server" Width="180" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtBTFirstName" runat="server" Text="<font size=3 face=arial><b>X</b></font>" ID="RequiredFieldValidator10" Display="Dynamic" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>Type:</td>
                                    <td>
                                        <asp:TextBox ID="txtType" runat="server" Width="180" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtBTLastName" runat="server" Text="<font size=3 face=arial><b>X</b></font>" ID="RequiredFieldValidator11" Display="Dynamic" />
                                    </td>
                                </tr>                    
                                <tr>
                                    <td>Acct #:</td>
                                    <td>
                                        <asp:TextBox ID="txtEPAcctNum" runat="server" Width="180" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAddr1" runat="server" Text="<font size=3 face=arial><b>X</b></font>" ID="RequiredFieldValidator12" Display="Dynamic" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>Exp. Date:</td>
                                    <td>
                                        <asp:TextBox ID="txtExpDate" runat="server" Width="180" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtAddr2" runat="server" Text="<font size=3 face=arial><b>X</b></font>" ID="RequiredFieldValidator13" Display="Dynamic" />
                                    </td>
                                </tr>                                 
                                <tr>
                                    <td>TR #:</td>
                                    <td>
                                        <asp:TextBox ID="txtTR" runat="server" Width="180" />
                                        <asp:RequiredFieldValidator ControlToValidate="txtZip" runat="server" Text="<font size=3 face=arial><b>X</b></font>" ID="RequiredFieldValidator16" Display="Dynamic" />
                                    </td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                    <td>&nbsp;</td>
                                </tr>    
                            </table>
                            --%>
                        </td>
                    </tr>
                    <tr>
                        <td class="sectionTitle" colspan="5">Existing Campaigns
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <!-- move line below into RadioButtonList when needed by Morena -->
                            <!--  <asp:ListItem Text="Customer Has Existing Campaigns from a WOW! Save within the past 30 days." Value="2" /> -->
                            <asp:RadioButtonList runat="server" ID="rblExistingCampaign" AutoPostBack="true">
                               
                                <asp:ListItem Text="Customer Has Existing Campaigns" Value="1" />
                                <asp:ListItem Text="No existing Campaigns" Value="0" />
                            </asp:RadioButtonList>
                            <!-- Holds value for email to indicate the option the CCR selected when submitting the above Campaign option -->
                            <asp:Label runat="server" ID="labelExistingCampaigns" Visible="false"></asp:Label>
                        </td>
                    </tr>
                    
                    <tr>
                        <td class="sectionTitle" colspan="5">Comments
                        </td>
                    </tr>
                    <tr>
                        <td colspan="5">
                            <asp:TextBox ID="txtComments" runat="server" TextMode="MultiLine" Width="98%" Height="50px"
                                MaxLength="500"></asp:TextBox>
                        </td>
                    </tr>
                </table>
                <div class="SubmitButton">
                    <asp:Button ID="btnsubmit" runat="server" Text="Submit" Width="150" />
                </div>
            </asp:Panel>
        </div>
        <!-- Thank you -->
        <div class="sectionTitle">
            <asp:Label runat="server" ID="lblThankYou" Visible="false" EnableViewState="false"></asp:Label>
        </div>
        <div id="Container-Error">
            <asp:Label ID="lblerr" runat="server" ForeColor="red" Width="600" BackColor="yellow"
                Font-Bold="False" Visible="false" EnableViewState="False" />
        </div>
        <User:MB ID="MB" runat="server" />
    </form>
    <script language="javascript" type="text/javascript">
        function PayShow(cID) {
            //alert(cID);
            if (cID == "rdoBT") {
                document.all("tBT").style.display = "";
                document.all("tEP").style.display = "none";
            }
            else if (cID == "rdoEP") {
                document.all("tBT").style.display = "none";
                document.all("tEP").style.display = "";
            }
            else {
                document.all("tBT").style.display = "none";
                document.all("tEP").style.display = "none";
            }

        }

        function rdoCheck(Source, args) {

            if (document.getElementById('lblThankYou') != null) {
                document.all('lblThankYou').innerText = '';
            }
            var rdo = document.getElementById('rdoBT');

            if (rdo.checked != false) {
                if (Source.id == 'cvBTFirstName') {
                    if (document.all("txtBTFirstName").value == "") {
                        args.IsValid = false
                    }
                }
                else if (Source.id == 'cvBTLastName') {
                    if (document.all("txtBTFirstName").value == "") {
                        args.IsValid = false
                    }
                }
                else if (Source.id == 'cvAddr1') {
                    if (document.all("txtAddr1").value == "") {
                        args.IsValid = false
                    }
                }
                    //            else if (Source.id == 'cvAddr2') {
                    //                if (document.all("txtAddr2").value == "") {
                    //                    args.IsValid = false
                    //                }
                    //            }
                else if (Source.id == 'cvCity') {
                    if (document.all("txtCity").value == "") {
                        args.IsValid = false
                    }
                }
                else if (Source.id == 'cvState') {
                    if (document.all("txtState").value == "") {
                        args.IsValid = false
                    }
                }
                else if (Source.id == 'cvZip') {
                    if (document.all("txtZip").value == "") {
                        args.IsValid = false
                    }
                }
            }
            else {
                args.IsValid = true;
            }
        }

    </script>
</body>
</html>
