Imports WOW.Data
Imports System.Data.SqlClient

Public Class MisappliedPayment
    Private Shared billingConnString As String = ConfigurationManager.ConnectionStrings("Billing").ConnectionString

    Public Shared Sub AddAchInfo(ByVal billingId As Integer, ByVal bankAccountName As String, ByVal invoiceNumber As String)
        Using db As New BaseDB(billingConnString)
            Dim parameters(2) As SqlParameter
            parameters(0) = New SqlParameter("@billingId", billingId)
            parameters(1) = New SqlParameter("@bankAccountName", bankAccountName)
            parameters(2) = New SqlParameter("@invoiceNumber", invoiceNumber)
            db.ExecuteProcedure("Spc_AchInfo_CreateRecord", parameters)

        End Using
    End Sub

    Public Shared Sub AddExtraAccounts(ByVal billingId As Integer, ByVal accountNumber As String, ByVal amount As Decimal)
        Using db As New BaseDB(billingConnString)
            Dim parameters(2) As SqlParameter
            parameters(0) = New SqlParameter("@billingId", billingId)
            parameters(1) = New SqlParameter("@accountNumber", accountNumber)
            parameters(2) = New SqlParameter("@amount", amount)
            db.ExecuteProcedure("Spc_ExtraAccounts_CreateRecord", parameters)
        End Using
    End Sub

End Class
