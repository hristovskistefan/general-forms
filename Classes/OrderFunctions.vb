Imports GeneralForms.CreateOrderService
Imports GeneralForms.OrderUpdateService


Public Class OrderFunctions

    Public Shared Function CreateOrder(ByVal accountNumber As String, ByVal houseNumber As String, isCommercial As Boolean, ByVal icomsUsername As String) As String

        Dim createOrderClient As New CreateOrderService.CreateOrderClient
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

        createOrderResponse = createOrderClient.SpecialRequest(specialRequestRequest)

        Return createOrderResponse.OrderNumber


    End Function


    Public Shared Sub CheckInOrder(ByVal orderNumber As String, ByVal icomsUsername As String)
        Dim orderUpdateClient As New OrderUpdateClient
        Dim scheduleRequest As New ScheduleRequest
        scheduleRequest.ExternalPassword = ConfigurationManager.AppSettings("ExternalPassword")
        scheduleRequest.ExternalUsername = icomsUsername
        scheduleRequest.OrderNumber = orderNumber
        scheduleRequest.ReasonCode = ConfigurationManager.AppSettings("ReasonCode")
        scheduleRequest.SiteId = ConfigurationManager.AppSettings("SiteId")
        scheduleRequest.TimeSlot = ConfigurationManager.AppSettings("TimeSlot")

        orderUpdateClient.Schedule(scheduleRequest)


    End Sub

End Class
