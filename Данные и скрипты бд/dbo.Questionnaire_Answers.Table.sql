USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Questionnaire_Answers]    Script Date: 31.10.2019 18:17:49 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire_Answers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Children] [int] NOT NULL,
	[Test_Number] [int] NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Test_Result_Id] [int] NULL,
	[Question1] [int] NOT NULL,
	[Question2] [int] NOT NULL,
	[Question3] [int] NOT NULL,
	[Question4] [int] NOT NULL,
	[Question5] [int] NOT NULL,
	[Question6] [int] NOT NULL,
	[Question7] [int] NOT NULL,
	[Question8] [int] NOT NULL,
	[Question9] [int] NOT NULL,
	[Question10] [int] NOT NULL,
	[Question11] [int] NOT NULL,
	[Question12] [int] NOT NULL,
	[Question13] [int] NOT NULL,
	[Question14] [int] NOT NULL,
	[Question15] [int] NOT NULL,
	[Question16] [int] NULL,
	[Question17] [int] NULL,
	[Question18] [int] NULL,
	[Question19] [int] NULL,
	[Question20] [int] NULL,
	[Question21] [int] NULL,
	[Question22] [int] NULL,
	[Question23] [int] NULL,
	[Question24] [int] NULL,
	[Question25] [int] NULL,
	[Question26] [int] NULL,
	[Question27] [int] NULL,
	[Question28] [int] NULL,
	[Question29] [int] NULL,
	[Question30] [int] NULL,
	[Question31] [int] NULL,
	[Question32] [int] NULL,
	[Question33] [int] NULL,
	[Question34] [int] NULL,
	[Question35] [int] NULL,
	[Question36] [int] NULL,
	[Question37] [int] NULL,
	[Question38] [int] NULL,
	[Question39] [int] NULL,
	[Question40] [int] NULL,
 CONSTRAINT [PK_Questionnaire_Answers] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Questionnaire_Answers]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire_Answers_Children] FOREIGN KEY([Id_Children])
REFERENCES [dbo].[Children] ([Id])
GO
ALTER TABLE [dbo].[Questionnaire_Answers] CHECK CONSTRAINT [FK_Questionnaire_Answers_Children]
GO
