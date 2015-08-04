Imports System.Data.SqlClient
Imports System.Diagnostics.Eventing.Reader
Imports WOW.Data

Namespace Classes
    Public Class IcomsAccount


        Public Sub New(accountNumber As String)
            Me.AccountNumber = accountNumber
            Try
                Dim dtAccount As DataTable
                Using db As New BaseDB(ConfigurationManager.ConnectionStrings("HA").ConnectionString)
                    Dim parameters(0) As SqlParameter
                    parameters(0) = New SqlParameter("@inAccountNumber", accountNumber)
                    dtAccount = db.GetDataTable("SpcAccountLookup", parameters)
                End Using

                If dtAccount.Rows.Count = 0 Then
                    IsAccountValid = False
                Else
                    IsAccountValid = True
                    FirstName = dtAccount.Rows(0)("FirstName").ToString()
                    LastName = dtAccount.Rows(0)("LastName").ToString()
                    Division = Convert.ToInt32(dtAccount.Rows(0)("Division"))
                    If dtAccount.Rows(0)("BillTypeCode").ToString() = "C" Then
                        IsCommercial = True
                    Else
                        IsCommercial = False
                    End If
                End If
            Catch ex As Exception
                IsAccountValid = False
            End Try
           
        End Sub

        Property AccountNumber As String
        Public Property FirstName As String
        Property LastName As String
        Property Division As Integer
        Property IsCommercial As Boolean
        Property IsAccountValid As Boolean

    End Class
End Namespace