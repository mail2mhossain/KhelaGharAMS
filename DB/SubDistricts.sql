USE [KhelaGharAMS]
GO

/****** Object:  Table [dbo].[SubDistricts]    Script Date: 7/17/2014 2:34:22 PM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[SubDistricts](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](150) NULL,
	[Description] [nvarchar](250) NULL,
	[District_Id] [int] NULL,
 CONSTRAINT [PK_dbo.SubDistricts] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]

GO

ALTER TABLE [dbo].[SubDistricts]  WITH CHECK ADD  CONSTRAINT [FK_dbo.SubDistricts_dbo.Districts_District_Id] FOREIGN KEY([District_Id])
REFERENCES [dbo].[Districts] ([Id])
GO

ALTER TABLE [dbo].[SubDistricts] CHECK CONSTRAINT [FK_dbo.SubDistricts_dbo.Districts_District_Id]
GO


