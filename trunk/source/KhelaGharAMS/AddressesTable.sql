USE [KhelaGharAMS]
GO

/****** Object:  Table [dbo].[Addresses]    Script Date: 3/1/2017 10:07:10 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Addresses](
	[AddressId] [bigint] IDENTITY(1,1) NOT NULL,
	[Street1] [nvarchar](250) NOT NULL,
	[Street2] [nvarchar](250) NULL,
	[PostalCode] [nvarchar](100) NULL,
	[City] [nvarchar](150) NULL,
	[InsertedBy] [nvarchar](max) NOT NULL,
	[InsertedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
 CONSTRAINT [PK_dbo.Addresses] PRIMARY KEY CLUSTERED 
(
	[AddressId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO


