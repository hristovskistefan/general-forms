Public Class LossPreventionDB
    Public Shared Function DBCreateNewStartRequest(lpModel As LPInsertModel) As Boolean
        Try
            Dim db As Database = DatabaseFactory.CreateDatabase("Billing")
            Dim cmd As DbCommand = db.GetStoredProcCommand("procLPNewRequest_InsertIntoSkip_Trace")
            db.AddInParameter(cmd, "RequestType", DbType.String, lpModel.RequestType)
            db.AddInParameter(cmd, "CCCUser", DbType.String, lpModel.CCCUser)
            db.AddInParameter(cmd, "DateSub", DbType.DateTime, lpModel.DateSub)
            db.AddInParameter(cmd, "CCRName", DbType.String, lpModel.CCRName)
            db.AddInParameter(cmd, "CSGOpCode", DbType.String, lpModel.CSGOpCode)
            db.AddInParameter(cmd, "Supervisor", DbType.String, lpModel.Supervisor)
            db.AddInParameter(cmd, "CFName", DbType.String, lpModel.CFName)
            db.AddInParameter(cmd, "CLName", DbType.String, lpModel.CLName)
            db.AddInParameter(cmd, "Kickback", DbType.Int32, lpModel.Kickback)
            db.AddInParameter(cmd, "Address", DbType.String, lpModel.Address)
            db.AddInParameter(cmd, "City", DbType.String, lpModel.City)
            db.AddInParameter(cmd, "State", DbType.String, lpModel.State)
            db.AddInParameter(cmd, "Zip", DbType.String, lpModel.Zip)
            db.AddInParameter(cmd, "PhoneNum", DbType.String, lpModel.PhoneNum)
            db.AddInParameter(cmd, "SSN", DbType.String, lpModel.SSN)
            db.AddInParameter(cmd, "DLNum", DbType.String, lpModel.DLNum)
            db.AddInParameter(cmd, "HouseNum", DbType.String, lpModel.HouseNum)
            db.AddInParameter(cmd, "Comments", DbType.String, lpModel.Comments)
            db.AddInParameter(cmd, "d2dRequest", DbType.Boolean, lpModel.d2dRequest)
            db.AddInParameter(cmd, "d2dEmail", DbType.String, lpModel.d2dEmail)
            db.AddInParameter(cmd, "AdditionalSSN", DbType.String, lpModel.AdditionalSSN)

            Return CBool(db.ExecuteNonQuery(cmd))

        Catch ex As Exception
            Return False
        End Try
    End Function
End Class
