/****** Script for SelectTopNRows command from SSMS  ******/
SELECT W.[Name] AS 'নাম'
      ,W.[MobileNo] AS 'মোবাইল নং'
      --,W.[Email] AS 'ইমেইল'
	  ,CASE C.[DelegateType]  
			WHEN 1 THEN 'Delegates' --'প্রতিনিধি'   
			ELSE 'পর্যবেক্ষক'   
		END  AS 'প্রতিনিধি/পর্যবেক্ষক' 
	  ,C.DelegateFee 
	  ,C.ReceiptNo
	  ,C.ReceiptDate
	  ,A.[Name] AS 'আসরের নাম'
	  ,A.Discriminator AS 'আসরের '
  FROM [KhelaGharAMS].[dbo].[ConferenceDelegates] AS C
  INNER JOIN [KhelaGharAMS].[dbo].[Workers] AS W
  ON C.[Worker_WorkerId] = W.[WorkerId]
  INNER JOIN [KhelaGharAMS].[dbo].[Asars] A
  ON W.[Asar_AsarId] = A.[AsarId]
  WHERE [Conference_ConferenceId] = 1
  ORDER BY A.[Name], C.[DelegateType] DESC