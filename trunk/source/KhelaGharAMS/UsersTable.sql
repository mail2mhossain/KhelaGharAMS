USE [PDB_METER]
GO

/****** Object:  Table [dbo].[Users]    Script Date: 3/1/2017 10:06:18 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[Users](
	[UserId] [bigint] IDENTITY(1,1) NOT NULL,
	[UserCode] [nvarchar](6) NOT NULL,
	[FirstName] [nvarchar](150) NOT NULL,
	[LastName] [nvarchar](150) NULL,
	[DOB] [datetime] NULL,
	[NationalId] [nvarchar](50) NULL,
	[MobileNo] [nvarchar](15) NULL,
	[Email] [nvarchar](150) NOT NULL,
	[InsertedBy] [nvarchar](max) NOT NULL,
	[InsertedDate] [datetime] NOT NULL,
	[LastUpdatedBy] [nvarchar](max) NOT NULL,
	[LastUpdatedDate] [datetime] NOT NULL,
	[Address_AddressId] [bigint] NULL,
	[LoginUser_Id] [nvarchar](128) NULL,
 CONSTRAINT [PK_dbo.Users] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.Addresses_Address_AddressId] FOREIGN KEY([Address_AddressId])
REFERENCES [dbo].[Addresses] ([AddressId])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.Addresses_Address_AddressId]
GO

ALTER TABLE [dbo].[Users]  WITH CHECK ADD  CONSTRAINT [FK_dbo.Users_dbo.AspNetUsers_LoginUser_Id] FOREIGN KEY([LoginUser_Id])
REFERENCES [dbo].[AspNetUsers] ([Id])
GO

ALTER TABLE [dbo].[Users] CHECK CONSTRAINT [FK_dbo.Users_dbo.AspNetUsers_LoginUser_Id]
GO


