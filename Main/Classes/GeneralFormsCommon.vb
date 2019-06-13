Imports System.Linq
Imports System.Net.Security
Imports System.Security.Cryptography.X509Certificates
Public Class GeneralFormsCommon

    Friend Shared Function getEmployee() As EmployeeService.EmpInstance
        Dim employee As EmployeeService.EmpInstance
        If Not IsNothing(System.Web.HttpContext.Current.Session("EmployeeGF")) Then
            employee = CType(System.Web.HttpContext.Current.Session("EmployeeGF"), EmployeeService.EmpInstance)
        Else
            Using es As New EmployeeService.ManagementClient

                Dim emplogin As String = System.Web.HttpContext.Current.Request.ServerVariables("AUTH_USER").Split("\"c)(1)
                'If emplogin.ToLower = "a_gulbranson" Then
                '    emplogin = "stl_bpace"
                'End If
                employee = es.GetBasicInfoByNTLogin(emplogin)
                System.Web.HttpContext.Current.Session("EmployeeGF") = employee
            End Using
        End If
        Return employee
    End Function

    Friend Shared Function getStateFromDivision(division As Integer) As String
        Dim icomsContext As ICOMSEntities = New ICOMSEntities
        Return (From f As Franchise_UPS In icomsContext.Franchise_UPS Where f.Division = division Select f.State).FirstOrDefault
    End Function

    Friend Shared Function buildAccountValidatorExpression() As String
        Return "^1[0-9]{7}$"
    End Function

    'Friend Shared Function GetCustomerInfo(ByVal account As String) As DataRow
    '    Dim db As Database = DatabaseFactory.CreateDatabase("Warehouse")
    '    Dim cmd As DbCommand = db.GetStoredProcCommand("ccc.usp_AccountLookup", account, Nothing)
    '    Try
    '        Dim dtTemp As DataTable = db.ExecuteDataSet(cmd).Tables(0)
    '        Return If(dtTemp.Rows.Count > 0, dtTemp.Rows(0), Nothing)
    '    Catch ex As Exception
    '        Return Nothing
    '    End Try
    'End Function

#Region "CreditCardValidation"

    Private Shared ReadOnly creditCardPatterns As List(Of String) = New List(Of String) _
    (New String() { _
        "^(4\d{12})|(4\d{15})$", _
        "^5[1-5]\d{14}$", _
        "(^(6011)\d{12}$)|(^(65)\d{14}$)", _
        "^3[47]\d{13}$"})

    Private Shared ReadOnly MaskedCCPatterns As List(Of String) = New List(Of String) _
       (New String() { _
           "^4\d{9}$", _
           "^5[1-5]\d{8}$", _
           "^6011\d{6}$|^65\d{8}$"})

    Private Shared cardToFind = String.Empty

    Friend Shared Function ValidateMaskedCC(ByVal cardNumber As String) As CreditCardTypes
        Dim cardType As CreditCardTypes = CreditCardTypes.Invalid
        cardToFind = Regex.Replace(cardNumber, "[^\d]", String.Empty)
        cardType = CType(MaskedCCPatterns.FindIndex(AddressOf FindPattern), CreditCardTypes)
        If cardType = CreditCardTypes.Invalid Then
            cardNumber = String.Empty
            Return CreditCardTypes.Invalid
        End If
        Return cardType
    End Function

    Friend Shared Function ValidateCC(ByVal cardNumber As String) As CreditCardTypes
        Dim cardType As CreditCardTypes = CreditCardTypes.Invalid
        cardToFind = Regex.Replace(cardNumber, "[^\d]", String.Empty)
        cardType = CType(creditCardPatterns.FindIndex(AddressOf FindPattern), CreditCardTypes)
        If cardType = CreditCardTypes.Invalid Then
            cardNumber = String.Empty
            Return CreditCardTypes.Invalid
        End If
        Dim digits As Char() = cardNumber.ToCharArray
        cardNumber = String.Empty
        Dim digit As Integer
        Dim sum As Integer = 0
        Dim alt As Boolean = False
        Array.Reverse(digits)
        For Each value As Char In digits
            digit = Integer.Parse(value)
            If alt Then
                digit *= 2
                If digit > 9 Then
                    digit -= 9
                End If
            End If
            sum += digit
            alt = Not alt
        Next
        If sum Mod 10 = 0 Then
            Return cardType
        Else
            Return CreditCardTypes.Invalid
        End If

    End Function

    Private Shared Function FindPattern(ByVal value As String) As Boolean
        If Regex.IsMatch(cardToFind, value) Then
            Return True
        Else
            Return False
        End If
    End Function

    Public Enum CreditCardTypes
        Invalid = -1
        Visa
        Mastercard
        Discover
        Amex
    End Enum
#End Region

#Region "PaperlessStatements"
    Friend Shared Function IsPaperLess(ByVal accountNumber As String) As Boolean
        Dim customerCareClient As New CustomerCare.CustomerClient
        Try
            customerCareClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings("UserNameApi")
            customerCareClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings("PasswordApi")
            Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateValidationCallBack
            Dim customerCareService() As CustomerCare.Service = {CustomerCare.Service.Billing}
            Dim strDeliveryMethod As String

            Dim customerDetailsRequest As New CustomerCare.CustomerDetailsByAccountNumberRequest
            customerDetailsRequest.AccountNumber = accountNumber
            customerDetailsRequest.BillingSystem = CustomerCare.BillingSystem.Icoms
            customerDetailsRequest.Services = customerCareService
            customerDetailsRequest.UserName = "testuser"
            Dim customerDetailsResponse As New CustomerCare.CustomerDetailsResponse
            customerDetailsResponse = customerCareClient.CustomerDetailsByAccountNumber(customerDetailsRequest)
            strDeliveryMethod = customerDetailsResponse.Billing.DeliveryMethod
            If strDeliveryMethod.ToUpper() = "EBILL" Then
                Return True
            Else
                Return False
            End If
        Catch ex As Exception
            customerCareClient.Abort()
            Throw New Exception("There was an error retrieving account information about statements.")
        End Try

    End Function
    Private Shared Function CertificateValidationCallBack(ByVal sender As Object, ByVal certificate As X509Certificate, ByVal chain As X509Chain, ByVal sslPolicyErrors As SslPolicyErrors) As Boolean
        Return True
    End Function
#End Region
#Region "Usha lost prevention"
    Friend Shared Function SearchByHouseNumber(ByVal houseNumber As String) As CustomerCare.CustomerSearchResponse
        Dim customerCareClient As New CustomerCare.CustomerClient
        Try
            customerCareClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings("UserNameApi")
            customerCareClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings("PasswordApi")
            Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateValidationCallBack
            Dim customerCareService() As CustomerCare.Service = {CustomerCare.Service.Billing}

            Dim customerDetailsRequest As New CustomerCare.CustomerSearchByHouseNumberRequest
            customerDetailsRequest.HouseNumber = houseNumber
            customerDetailsRequest.BillingSystem = CustomerCare.BillingSystem.Usha

            customerDetailsRequest.UserName = "lpform"
            Dim customerDetailsResponse As New CustomerCare.CustomerSearchResponse
            customerDetailsResponse = customerCareClient.CustomerSearchByHouseNumber(customerDetailsRequest)
            Return customerDetailsResponse

        Catch ex As Exception
            customerCareClient.Abort()
            'Throw New Exception("There was an error retrieving account information by house number.")
            Return Nothing
        End Try
    End Function
#End Region

#Region "GetCustomerDetails"
    Friend Shared Function getCustomerDetails(account As String) As CareService.CustomerDetailsResponse
        Dim customerDetailsResponse As New CareService.CustomerDetailsResponse
        Try
            Using careClient As New CareService.CustomerClient
                careClient.ClientCredentials.UserName.UserName = ConfigurationManager.AppSettings("UserNameApi")
                careClient.ClientCredentials.UserName.Password = ConfigurationManager.AppSettings("PasswordApi")
                Net.ServicePointManager.ServerCertificateValidationCallback = AddressOf CertificateValidationCallBack
                Dim customerCareService() As CareService.Service = {CareService.Service.Account}

                Dim customerDetailsRequest As New CareService.CustomerDetailsByAccountNumberRequest

                customerDetailsRequest.AccountNumber = account
                customerDetailsRequest.Services = customerCareService
                customerDetailsRequest.UserName = "GeneralForms"

                customerDetailsResponse = careClient.CustomerDetailsByAccountNumber(customerDetailsRequest)
            End Using
            Return customerDetailsResponse
        Catch
            customerDetailsResponse = Nothing
        End Try
    End Function
#End Region

End Class
