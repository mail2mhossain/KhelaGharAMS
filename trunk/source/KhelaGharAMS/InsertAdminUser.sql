
  INSERT INTO [PDB_METER].[dbo].[Users] ([UserCode]
      ,[FirstName]
      ,[LastName]
      ,[DOB]
      ,[NationalId]
      ,[MobileNo]
      ,[Email]
      ,[InsertedBy]
      ,[InsertedDate]
      ,[LastUpdatedBy]
      ,[LastUpdatedDate]
	  )
   VALUES ('762854','Mosharaf', 'Hossain','1972-07-05','2690246963958','+8801713032885','Aumated',getdate(),'Automated',getdate())

   UPDATE [PDB_METER].[dbo].[Users] 
   SET LoginUser_Id = ''
   WHERE UserCode = '762854'