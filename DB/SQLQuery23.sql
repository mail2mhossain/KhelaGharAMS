/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [Division_ID]
      ,[Division]
      ,[District_ID]
      ,[District]
      ,[SubDistrict_ID]
      ,[SubDistrict]
  FROM [KG_Setup_Data].[dbo].[UpoZila]



SET IDENTITY_INSERT [KG_Setup_Data].[dbo].[SubDistricts] ON;


INSERT INTO [KG_Setup_Data].[dbo].[SubDistricts] ([Id], [Name],[District_Id])
SELECT DISTINCT [SubDistrict_ID], [SubDistrict],[District_ID]
FROM [KG_Setup_Data].[dbo].[UpoZila]

SET IDENTITY_INSERT [KG_Setup_Data].[dbo].[SubDistricts] OFF;


