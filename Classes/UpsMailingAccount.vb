Imports System.Data.SqlClient
Imports WOW.Data

Public Class UpsMailingAccount
    Private _accountNumber As String
    Private _houseNumber As String
    Private _isCommercial As Boolean
    Private _hasSpp As Boolean
    Private _hasVideo As Boolean
    Private _hasHsd As Boolean
    Private _hasTelephony As Boolean


    Private _connectionString As String = ConfigurationManager.ConnectionStrings("HA").ConnectionString
    Private _upsConnString As String = ConfigurationManager.ConnectionStrings("UPS").ConnectionString

    Public Sub New(ByVal accountNumber As String)
        _accountNumber = accountNumber

        'Retrieve Charge Off Information
        Dim baseDb As New WOW.Data.BaseDB(_connectionString)

        Try

            'Determine if the account is charged off
            Dim dtChargeOff As New DataTable
            Dim sqlParameters(0) As SqlParameter

            sqlParameters(0) = New SqlParameter("@inAccountNumber", _accountNumber)


            dtChargeOff = baseDb.GetDataTable("ccc.SpcAccountLookup", sqlParameters)
            If dtChargeOff Is Nothing OrElse dtChargeOff.Rows.Count = 0 Then
                Throw New Exception("Account could not be located.")

            End If

            If dtChargeOff.Rows(0)("BillTypeCode").ToString() = "S" Then
                _isCommercial = False
            Else
                _isCommercial = True
            End If

            _houseNumber = dtChargeOff.Rows(0)("houseKey").ToString()

            Dim isActive As Boolean = False

            Dim totalWriteOff As Decimal = 0
            For Each singleRow As DataRow In dtChargeOff.Rows
                If singleRow("CustomerStatusCode") <> "F" Then
                    isActive = True
                End If
                If Not IsDBNull(singleRow("WriteOffDollars")) <> Nothing AndAlso String.IsNullOrWhiteSpace(singleRow("WriteOffDollars")) = False Then
                    totalWriteOff += Decimal.Parse(singleRow("WriteOffDollars"))
                End If

            Next

            If dtChargeOff.Rows(0)("videoFlag").ToString = "Y" Then
                _hasVideo = True
            Else
                _hasVideo = False
            End If

            If dtChargeOff.Rows(0)("hsdFlag").ToString = "Y" Then
                _hasHsd = True
            Else
                _hasHsd = False
            End If

            If dtChargeOff.Rows(0)("telephonyFlag").ToString = "Y" Then
                _hasTelephony = True
            Else
                _hasTelephony = False
            End If


            'If everything is labeled former (isActive variable) and the amount is > 0 then the account is considered charged off
            If Not isActive AndAlso totalWriteOff > 0 Then
                _hasSpp = False
                'hasSpp will remain false.  if the account is charged off then we treat the account as if spp is not on it
            Else

                ReDim sqlParameters(1)
                sqlParameters(0) = New SqlParameter("@accountNumber", _accountNumber)
                sqlParameters(1) = New SqlParameter("@houseNumber", _houseNumber)

                Dim dtServiceCodes As New DataTable

                dtServiceCodes = baseDb.GetDataTable("SpcGetCustomerServices", sqlParameters)

                'Determine if SPP is on the account.
                'Get list of SPP service codes
                Dim sppCodes() As String = ConfigurationManager.AppSettings("SppCodes").Split("|")

                For Each singleRow As DataRow In dtServiceCodes.Rows
                    If sppCodes.Contains(singleRow("ServiceCode").ToString().Trim()) AndAlso Int32.Parse(singleRow("ServiceQuantity").ToString().Trim()) > 0 Then
                        _hasSpp = True
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

    Public Function CreateOrder(ByVal icomsUsername As String, ByVal salesId As String) As String
        Try
            Dim serviceCodes() As String
            'Determine which service code should be on account
            If _isCommercial Then
                serviceCodes = ConfigurationManager.AppSettings("CommercialChargeCDT").Split("|")
            Else
                serviceCodes = ConfigurationManager.AppSettings("ResidentialChargeCDT").Split("|")
            End If

            Dim serviceCode As String = String.Empty

            If _hasVideo Then
                serviceCode = serviceCodes(0)
            ElseIf _hasHsd Then
                serviceCode = serviceCodes(1)
            ElseIf _hasTelephony Then
                serviceCode = serviceCodes(2)

            End If

            If String.IsNullOrWhiteSpace(serviceCode) Then
                Throw New Exception("Account has no LOBs.")

            End If


            Dim orderNumber As String = OrderFunctions.CreateOrder(_accountNumber, _houseNumber, serviceCode, icomsUsername, salesId, _hasSpp)

            OrderFunctions.ScheduleOrder(orderNumber, _accountNumber, _houseNumber, icomsUsername, salesId)

            Return orderNumber
        Catch ex As Exception
            Throw New Exception("There was an error adding the on-time UPS charge to the account.  Please check the account for errors and resubmit your request. " + ex.Message)

        End Try

    End Function

    Public Sub AddInformationToDatabase(ByVal username As String, ByVal orderNumber As String, ByVal customerName As String, _
                                           ByVal accountNumber As String, ByVal phoneNumber As String, ByVal address As String, _
                                           ByVal city As String, ByVal state As String, ByVal zip As String, _
                                           ByVal digitalReceivers As Integer, ByVal dvrReceivers As Integer, _
                                           ByVal hdReceivers As Integer, hdDvrReceivers As Integer, ByVal dtaReceivers As Integer, _
                                           ByVal cableModems As Integer, ByVal phoneModems As Integer, ByVal cableCards As Integer, _
                                           ByVal ultraTvGateways As Integer, ByVal ultraTvMediaPlayer As Integer)
        Dim total As Integer = digitalReceivers + hdReceivers + hdDvrReceivers + dtaReceivers + cableModems + phoneModems + cableCards + ultraTvGateways + ultraTvMediaPlayer

        Dim baseDb = New BaseDB(_upsConnString)
        Dim parameters(19) As SqlParameter
        parameters(0) = New SqlParameter("@ENTERED_BY", username)
        parameters(1) = New SqlParameter("@ORDER_NUMBER", orderNumber)
        parameters(2) = New SqlParameter("@CUSTOMER_NAME", customerName)
        parameters(3) = New SqlParameter("@ACCOUNT_NUMBER", accountNumber)
        parameters(4) = New SqlParameter("@PHONE_NUMBER", phoneNumber)
        parameters(5) = New SqlParameter("@ADDRESS", address)
        parameters(6) = New SqlParameter("@CITY", city)
        parameters(7) = New SqlParameter("@STATE", state)
        parameters(8) = New SqlParameter("@ZIP", zip)
        parameters(9) = New SqlParameter("@DIGITAL_RECEIVERS", digitalReceivers)
        parameters(10) = New SqlParameter("@DVR_RECEIVERS", dvrReceivers)
        parameters(11) = New SqlParameter("@HD_DVR_RECEIVERS", hdDvrReceivers)
        parameters(12) = New SqlParameter("@DTA_RECEIVERS", dtaReceivers)
        parameters(13) = New SqlParameter("@CABLE_MODEMS", cableModems)
        parameters(14) = New SqlParameter("@PHONE_MODEMS", phoneModems)
        parameters(15) = New SqlParameter("@CABLE_CARDS", cableCards)
        parameters(16) = New SqlParameter("@ULTRA_TV_GATEWAYS", hdDvrReceivers)
        parameters(17) = New SqlParameter("@HD_RECEIVERS", hdReceivers)
        parameters(18) = New SqlParameter("@ULTRA_TV_MEDIA_PLAYER", ultraTvMediaPlayer)
        parameters(19) = New SqlParameter("@TOTAL_BOXES_NEEDED", total)

        baseDb.ExecuteProcedure("Warehouse.Insert_Into_UPS_BOX_SEND", parameters)



    End Sub

End Class
