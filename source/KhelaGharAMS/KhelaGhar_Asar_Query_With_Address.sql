/****** Script for SelectTopNRows command from SSMS  ******/
SELECT TOP 1000 [AsarId]
      ,Asar.[Name] +  ISNULL(CHAR(13) +[AddressLine], '') + CHAR(13) + Area.Name + ', ' + Parent.Name + ' Jela' + CHAR(13) AS Asar
      ,[AddressLine]
      ,Area.Name
	  ,Parent.Name AS Jela
      ,[Area_AreaId]
  FROM [KhelagharAMS].[dbo].[Asars] AS Asar
  INNER JOIN [KhelagharAMS].[dbo].[Areas] AS Area
  ON Asar.Area_AreaId = Area.AreaId 
  INNER JOIN [KhelagharAMS].[dbo].[Areas] As Parent
  ON Area.Parent_AreaId = Parent.AreaId 
  WHERE Asar.[Discriminator] = 'ShakhaAsar'
  ORDER BY Parent.Name, Area.Name, Asar.Name