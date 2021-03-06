/****** Script for SelectTopNRows command from SSMS  ******/


  SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[Areas] ON

  INSERT INTO [KhelaGharAMS].[dbo].[Areas](
  [AreaId]
      ,[Name]
      ,[Description]
      ,[Discriminator]
      ,[Parent_AreaId]
  )
  SELECT  [LocationId]
      ,[Name]
      ,[Description]
      ,[Discriminator]
      ,[Parent_LocationId]
  FROM [KhelaGhar].[dbo].[Locations]

  SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[Areas] OFF


  SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[Designations] ON

  INSERT INTO  [KhelaGharAMS].[dbo].[Designations](
	  [DesignationId]
      ,[Name]
      ,[DesignationType]
      ,[DesignationOrder]
	  )
  SELECT [DesignationId]
      ,[Name]
      ,[DesignationType]
      ,[DesignationOrder]
  FROM [KhelaGhar].[dbo].[Designations] 

  SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[Designations] OFF

  --SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[MasterActivities] ON
  --INSERT INTO [KhelaGharAMS].[dbo].[MasterActivities]([MasterActivityId]
  --    ,[ActivityName])
  --SELECT [ActivityId]
  --    ,[ActivityName]
  --FROM [KhelaGhar].[dbo].[Activities]
  --SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[MasterActivities] OFF