USE [KhelaGharAMS]

  INSERT INTO [KhelaGharAMS].[dbo].[Users] ([UserCode]
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
   VALUES ('762854','Mosharaf', 'Hossain','1972-07-05','2690246963958','+8801713032885','mail2mhossain@gmail.com','Aumated',getdate(),'Automated',getdate())

   UPDATE [KhelaGharAMS].[dbo].[Users] 
   SET LoginUser_Id = 'fa77db76-4812-465f-9fbc-5344e2a9b30b'
   WHERE UserCode = '762854'