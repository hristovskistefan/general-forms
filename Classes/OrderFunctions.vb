Public Class OrderFunctions

    Public Shared Sub CreateOrder(ByVal accountNumber As String, ByVal houseNumber As String, isCommercial As Boolean)
        Dim customerOrderClient As New CreateOrderService.CreateOrderClient
        Dim customerOrderRequest As New CreateOrderService.CreateOrderRequest
        customerOrderRequest.AccountNumber = accountNumber
        customerOrderRequest.HouseNumber = houseNumber


    End Sub


End Class
