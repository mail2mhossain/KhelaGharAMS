
SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[SubDistricts] ON;


INSERT INTO [KhelaGharAMS].[dbo].[SubDistricts] ([Id], [Name],[District_Id])
SELECT [Id], [Name],[District_Id]
FROM [KG_Setup_Data].[dbo].[SubDistricts]

SET IDENTITY_INSERT [KhelaGharAMS].[dbo].[SubDistricts] OFF;