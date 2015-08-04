<%@ Control Language="vb" AutoEventWireup="false" CodeBehind="MessageBox.ascx.vb"
    Inherits="GeneralForms.MessageBox" %>
<asp:Panel ID="pnlBackground2" runat="server" CssClass="background2" Visible="false">
</asp:Panel>
<asp:Panel ID="pnlError" runat="server" CssClass="popupNoWidth2" Visible="false"
    DefaultButton="btnPositive">
    <div class="form" style="margin-right: 100px">
        <table cellpadding="2" cellspacing="0">
            <tr>
                <td>
                    &nbsp;
                </td>
                <td style="text-align: right">
                    <asp:ImageButton ID="btnErrorClose" runat="server" ImageAlign="right" ImageUrl="~/images/close.jpg"
                        CausesValidation="False" />
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblError" runat="server" Font-Bold="true" ForeColor="red"></asp:Label>
                </td>
            </tr>
            <tr>
                <td>
                    <asp:Label ID="lblQuestion" runat="server" Font-Bold="true"></asp:Label>
                </td>
            </tr>
            <tr>
                <td colspan="2" style="text-align: center">
                    <table>
                        <tr>
                            <td>
                                <asp:Button ID="btnPositive" runat="server" Text="OK" CausesValidation="False" />
                            </td>
                            <td>
                                <asp:Button ID="btnNegative" runat="server" Text="No" CausesValidation="False" Visible="false" />
                            </td>
                        </tr>
                    </table>
                </td>
            </tr>
        </table>
        <asp:Label runat="server" ID="lblRedirectURL" Visible="false" />
    </div>
</asp:Panel>
