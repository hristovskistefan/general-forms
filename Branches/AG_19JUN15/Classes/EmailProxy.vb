Public Class EmailProxy
    Friend Shared Sub Send(ByVal mailMsg As System.Web.Mail.MailMessage) 'I know it's deprecated...and I don't care, this is for legacy support.
        Dim svcMailMsg As New EmailService.EmailMessage
        svcMailMsg.To = parseSemiColonEmailString(mailMsg.To)
        svcMailMsg.From = mailMsg.From
        svcMailMsg.IsBodyHtml = CBool(mailMsg.BodyFormat)
        svcMailMsg.Subject = mailMsg.Subject
        If Not mailMsg.Cc Is Nothing Then
            svcMailMsg.CC = parseSemiColonEmailString(mailMsg.Cc)
        End If
        If Not mailMsg.Bcc Is Nothing Then
            svcMailMsg.BCC = parseSemiColonEmailString(mailMsg.Bcc)
        End If
        svcMailMsg.Body = mailMsg.Body
        sendEmail(svcMailMsg)
    End Sub

    Friend Shared Sub Send(ByVal mailMsg As System.Net.Mail.MailMessage)
        Dim svcMailMsg As New EmailService.EmailMessage
        svcMailMsg.To = parseAddressCollection(mailMsg.To)
        svcMailMsg.From = mailMsg.From.Address
        svcMailMsg.IsBodyHtml = mailMsg.IsBodyHtml
        svcMailMsg.Subject = mailMsg.Subject
        svcMailMsg.CC = parseAddressCollection(mailMsg.CC)
        svcMailMsg.BCC = parseAddressCollection(mailMsg.Bcc)
        svcMailMsg.Body = mailMsg.Body
        sendEmail(svcMailMsg)
    End Sub

    Private Shared Function parseAddressCollection(ByVal addyCollection As System.Net.Mail.MailAddressCollection) As String()
        Dim addys As New Collections.Generic.List(Of String)
        For Each addy As System.Net.Mail.MailAddress In addyCollection
            addys.Add(addy.Address)
        Next
        Return addys.ToArray
    End Function
    Private Shared Function parseSemiColonEmailString(ByVal srcString As String) As String()
        Dim strArray As String()
        strArray = srcString.Split(";")
        Return strArray
    End Function

    Private Shared Sub sendEmail(ByVal mailMessage As EmailService.EmailMessage)
        Using client As New EmailService.OperationsClient
            client.Open()
            client.SendEmail(mailMessage)
        End Using
    End Sub

End Class


