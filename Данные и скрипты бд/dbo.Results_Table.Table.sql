USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Results_Table]    Script Date: 31.10.2019 18:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Answer] [int] NOT NULL,
	[Result1] [nvarchar](100) NOT NULL,
	[Result2] [nvarchar](100) NULL,
	[Result3] [nvarchar](100) NULL,
	[Result4] [nvarchar](100) NULL,
	[Result5] [nvarchar](100) NULL,
 CONSTRAINT [PK_Results_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Results_Table]  WITH CHECK ADD  CONSTRAINT [FK_Results_Table_Questionnaire_Answers] FOREIGN KEY([Id_Answer])
REFERENCES [dbo].[Questionnaire_Answers] ([Id])
GO
ALTER TABLE [dbo].[Results_Table] CHECK CONSTRAINT [FK_Results_Table_Questionnaire_Answers]
GO
