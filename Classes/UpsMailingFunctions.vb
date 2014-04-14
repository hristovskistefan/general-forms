Imports System.Data.SqlClient

Public Class UpsMailingFunctions
    Private _accountNumber As String
    Private _connectionString As String = ConfigurationManager.ConnectionStrings("HA").ConnectionString


    Public Sub New(ByVal accountNumber As String)
        _accountNumber = accountNumber

        'Retrieve Charge Off Information
        Dim baseDb As New WOW.Data.BaseDB(_connectionString)

        Try

            'Determine if the account is charged off
            Dim dtChargeOff As New DataTable
            Dim sqlParameters(1) As SqlParameter

            sqlParameters(0) = New SqlParameter("@accountNumber", _accountNumber)


            dtChargeOff = baseDb.GetDataTable("Spc_GetAccountChargeOffInformation", sqlParameters)
            If dtChargeOff Is Nothing OrElse dtChargeOff.Rows.Count = 0 Then
                Throw New Exception

            End If
            Dim isActive As Boolean = False

            Dim totalWriteOff As Decimal = 0
            For Each singleRow As DataRow In dtChargeOff.Rows
                If singleRow("CustomerStatusCode") <> "F" Then
                    isActive = True
                End If
                totalWriteOff += Decimal.Parse(singleRow("WriteOffDollars"))
            Next
            hasSpp = False

            'If everything is labeled former (isActive variable) and the amount is > 0 then the account is considered charged off
            If Not isActive AndAlso totalWriteOff > 0 Then
                isChargedOff = True
                'hasSpp will remain false.  if the account is charged off then we treat the account as if spp is not on it
            Else
                isChargedOff = False
                ReDim sqlParameters(2)
                sqlParameters(0) = New SqlParameter("@accountNumber", _accountNumber)
                sqlParameters(1) = New SqlParameter("@houseNumber", dtChargeOff.Rows(0)("HouseNumber").ToString)

                Dim dtServiceCodes As New DataTable

                dtServiceCodes = baseDb.GetDataTable("SpcGetCustomerServices", sqlParameters)

                'Determine if SPP is on the account.

                'Get list of SPP service codes
                Dim sppCodes() As String = ConfigurationManager.AppSettings("SppCodes").Split

                For Each singleRow As DataRow In dtServiceCodes.Rows
                    If sppCodes.Contains(singleRow("ServiceCode")) AndAlso Int32.Parse(singleRow("ServiceCode")) > 0 Then
                        hasSpp = True
                        Exit For
                    End If
                Next
            End If

            

        Catch ex As Exception
            Throw New Exception("There was an error retrieving account information.")

        Finally
            baseDb = Nothing

        End Try


    End Sub

    Public Property isChargedOff As Boolean
    Public Property hasSpp As Boolean


End Class
