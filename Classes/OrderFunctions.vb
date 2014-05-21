Imports GeneralForms.CreateOrderService
Imports GeneralForms.OrderUpdateService
Imports WOW.Data
Imports System.Data.SqlClient


Public Class OrderFunctions

    Public Shared Function CreateOrder(ByVal accountNumber As String, ByVal houseNumber As String, serviceCode As String, ByVal icomsUsername As String, _
                                       ByVal hasSpp As Boolean) As String
        Dim createOrderClient As New CreateOrderService.CreateOrderClient
        Try

            Dim specialRequestRequest As New CreateOrderService.SpecialRequestRequest
            specialRequestRequest.AccountNumber = accountNumber
            specialRequestRequest.HouseNumber = houseNumber
            specialRequestRequest.ExternalUsername = icomsUsername
            specialRequestRequest.SiteId = ConfigurationManager.AppSettings("SiteId")
            specialRequestRequest.ServiceCode = serviceCode


            If hasSpp Then
                specialRequestRequest.PriceOverride = 0
            End If


            Dim createOrderResponse As New CreateOrderResponse

            createOrderResponse = createOrderClient.SpecialRequest(specialRequestRequest)


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

            orderUpdateClient.Schedule(scheduleRequest)
        Catch ex As Exception
            orderUpdateClient.Abort()
            Throw
        Finally
            orderUpdateClient.Close()
        End Try



    End Sub



End Class
