Imports Microsoft.Practices.EnterpriseLibrary.Data
Public Class GenFormsData
    Friend Shared Function ospActive() As Boolean
        Dim db As Database = DatabaseFactory.CreateDatabase("IT")
        Dim cmd As Data.Common.DbCommand = db.GetStoredProcCommand("procOSP_getServerStatus")
        'Hard coded DBID 1 for GooRoo
        db.AddInParameter(cmd, "DBID", DbType.Int32, 1)
        Return CBool(db.ExecuteScalar(cmd))
    End Function
End Class
