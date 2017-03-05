USE [KhelaGharAMS]
GO

/****** Object:  Table [dbo].[ConferenceAsars]    Script Date: 3/5/2017 10:58:13 AM ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO

CREATE TABLE [dbo].[ConferenceAsars](
	[ConferenceAsarId] [int] IDENTITY(1,1) NOT NULL,
	[DelegateType] [int] NOT NULL,
	[RegistrationFee] [decimal](18, 2) NOT NULL,
	[ReceiptNo] [nvarchar](150) NULL,
	[ReceiptDate] [datetime] NULL,
	[AuditFields_InsertedBy] [nvarchar](max) NOT NULL,
	[AuditFields_InsertedDateTime] [datetime] NOT NULL,
	[AuditFields_LastUpdatedBy] [nvarchar](max) NOT NULL,
	[AuditFields_LastUpdatedDateTime] [datetime] NOT NULL,
	[Asar_AsarId] [int] NOT NULL,
	[Conference_ConferenceId] [bigint] NOT NULL,
 CONSTRAINT [PK_dbo.ConferenceAsars] PRIMARY KEY CLUSTERED 
(
	[ConferenceAsarId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

GO

ALTER TABLE [dbo].[ConferenceAsars]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ConferenceAsars_dbo.Asars_Asar_AsarId] FOREIGN KEY([Asar_AsarId])
REFERENCES [dbo].[Asars] ([AsarId])
GO

ALTER TABLE [dbo].[ConferenceAsars] CHECK CONSTRAINT [FK_dbo.ConferenceAsars_dbo.Asars_Asar_AsarId]
GO

ALTER TABLE [dbo].[ConferenceAsars]  WITH CHECK ADD  CONSTRAINT [FK_dbo.ConferenceAsars_dbo.Conferences_Conference_ConferenceId] FOREIGN KEY([Conference_ConferenceId])
REFERENCES [dbo].[Conferences] ([ConferenceId])
GO

ALTER TABLE [dbo].[ConferenceAsars] CHECK CONSTRAINT [FK_dbo.ConferenceAsars_dbo.Conferences_Conference_ConferenceId]
GO


