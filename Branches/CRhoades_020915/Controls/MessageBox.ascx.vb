Public Partial Class MessageBox
    Inherits System.Web.UI.UserControl

    Public Event BtnOk_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event BtnErrorClose_Clicked(ByVal sender As Object, ByVal e As EventArgs)
    Public Event BtnNegative_Clicked(ByVal sender As Object, ByVal e As EventArgs)

    Private _popupUrl As String

    Public Property RedirectUrl() As String
        Get
            Return lblRedirectURL.Text.Trim
        End Get
        Set(ByVal value As String)
            lblRedirectURL.Text = value
        End Set
    End Property

    Public Property PopupUrl() As String
        Get
            Return _popupUrl
        End Get
        Set(ByVal value As String)
            btnPositive.Attributes.Add("onClick", "popWin()")
            Dim sb As New StringBuilder
            sb.Append("<script type=""text/Javascript"">function popWin(){window.open('" & value & "','3PV');}</script>")
            Me.Controls.Add(New LiteralControl(sb.ToString))
            _popupUrl = value
        End Set
    End Property

    Public Sub ShowMessage(ByVal message As String)
        Me.pnlBackground2.Visible = True
        Me.pnlError.Visible = True
        Me.lblError.Text = message
        Me.btnErrorClose.Visible = True
        Me.btnNegative.Visible = False
    End Sub

    Public Sub ShowMessage(ByVal message As String, ByVal okButtonText As String)
        btnPositive.Text = okButtonText
        ShowMessage(message)
    End Sub

    Public Sub ShowError(ByVal ex As Exception)
        Me.pnlBackground2.Visible = True
        Me.pnlError.Visible = True
        Me.lblError.Text = "An error has occurred.  Please contact IT with following text:<br />" _
            & ex.Message & "<br />" & ex.StackTrace
        Me.btnErrorClose.Visible = True
        Me.btnNegative.Visible = False
    End Sub

    Public Sub ShowDialog(ByVal message As String, ByVal question As String)
        ShowDialog(message, question, "Yes", "No")
    End Sub

    Public Sub ShowDialog(ByVal message As String, ByVal question As String, yesText As String, noText As String)
        Me.pnlBackground2.Visible = True
        Me.pnlError.Visible = True
        Me.lblError.Text = message
        Me.lblQuestion.Text = question
        Me.btnPositive.Text = yesText
        Me.btnNegative.Text = noText
        Me.btnErrorClose.Visible = True
        Me.btnNegative.Visible = True
    End Sub

    Protected Sub btnPositive_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnPositive.Click
        If RedirectUrl <> String.Empty Then
            Response.Redirect(RedirectUrl)
        End If
        Me.pnlBackground2.Visible = False
        Me.pnlError.Visible = False
        RaiseEvent BtnOk_Clicked(sender, e)
    End Sub

    Protected Sub btnNegative_Click(ByVal sender As System.Object, ByVal e As System.EventArgs) Handles btnNegative.Click
        If RedirectUrl <> String.Empty Then
            Response.Redirect(RedirectUrl)
        End If
        Me.pnlBackground2.Visible = False
        Me.pnlError.Visible = False
        RaiseEvent BtnNegative_Clicked(sender, e)
    End Sub

    Protected Sub btnErrorClose_Click(ByVal sender As System.Object, ByVal e As System.Web.UI.ImageClickEventArgs) Handles btnErrorClose.Click
        Me.pnlBackground2.Visible = False
        Me.pnlError.Visible = False
        RaiseEvent BtnErrorClose_Clicked(sender, e)
    End Sub
End Class