Imports WOW.Data

Namespace Classes
    Public Class DbFunctions
        Public Shared Function GetAccountInformation() As DataTable
            Using db As New BaseDB(ConfigurationManager.ConnectionStrings("HA").ConnectionString)

            End Using
        End Function
    End Class
End Namespace