Imports GeneralForms.CreateOrderService
Imports GeneralForms.OrderUpdateService


Public Class OrderFunctions

    Public Shared Function CreateOrder(ByVal accountNumber As String, ByVal houseNumber As String, isCommercial As Boolean, ByVal icomsUsername As String) As String
        Dim createOrderClient As New CreateOrderService.CreateOrderClient
        Try

            Dim specialRequestRequest As New CreateOrderService.SpecialRequestRequest
            specialRequestRequest.AccountNumber = accountNumber
            specialRequestRequest.HouseNumber = houseNumber
            specialRequestRequest.ExternalUsername = icomsUsername
            specialRequestRequest.SiteId = ConfigurationManager.AppSettings("SiteId")
            If isCommercial Then
                specialRequestRequest.ServiceCode = ConfigurationManager.AppSettings("CommercialCharge")
            Else
                specialRequestRequest.ServiceCode = ConfigurationManager.AppSettings("ResidentialCharge")
            End If

            Dim createOrderResponse As New CreateOrderResponse

            createOrderResponse = CreateOrderClient.SpecialRequest(specialRequestRequest)


            Return createOrderResponse.OrderNumber
        Catch ex As Exception
            createOrderClient.Abort()

            Throw

        Finally
            createOrderClient.Close()

        End Try




    End Function


    Public Shared Sub CheckInOrder(ByVal orderNumber As String, ByVal icomsUsername As String)
        Dim orderUpdateClient As New OrderUpdateClient
        Try

            Dim scheduleRequest As New ScheduleRequest
            scheduleRequest.ExternalPassword = ConfigurationManager.AppSettings("ExternalPassword")
            scheduleRequest.ExternalUsername = icomsUsername
            scheduleRequest.OrderNumber = orderNumber
            scheduleRequest.ReasonCode = ConfigurationManager.AppSettings("ReasonCode")
            scheduleRequest.SiteId = ConfigurationManager.AppSettings("SiteId")
            scheduleRequest.TimeSlot = ConfigurationManager.AppSettings("TimeSlot")

            OrderUpdateClient.Schedule(scheduleRequest)
        Catch ex As Exception
            orderUpdateClient.Abort()
            Throw
        Finally
            orderUpdateClient.Close()
        End Try



    End Sub

    'Public Shared Sub AddInformationToDatabase(ByVal username As String, ByVal orderNumber As String, ByVal customerName As String, _
    '                                           ByVal accountNumber As String, ByVal phoneNumber As String, ByVal address As String, _
    '                                           ByVal city As String, ByVal state As String,)


    'End Sub

End Class
