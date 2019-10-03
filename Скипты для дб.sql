USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Children]    Script Date: 03.10.2019 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Children](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[FIO] [varchar](50) NOT NULL,
	[Id_School] [int] NOT NULL,
	[Id_Class] [int] NOT NULL,
 CONSTRAINT [PK_Children] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 03.10.2019 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Classes](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_School] [int] NOT NULL,
	[Class_Name] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Table_1] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[EGE_Questions]    Script Date: 03.10.2019 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[EGE_Questions](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Question_Text] [ntext] NOT NULL,
 CONSTRAINT [PK_EGE_Questions] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Questionnaire_Answers]    Script Date: 03.10.2019 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire_Answers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Children] [int] NOT NULL,
	[Test_Number] [int] NOT NULL,
	[Date] [datetime] NOT NULL,
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
	[Question40] [int] NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 03.10.2019 16:22:38 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Schools](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[School_Number] [nchar](10) NOT NULL,
 CONSTRAINT [PK_Schools] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Children]  WITH CHECK ADD  CONSTRAINT [FK_Children_Classes] FOREIGN KEY([Id_Class])
REFERENCES [dbo].[Classes] ([Id])
GO
ALTER TABLE [dbo].[Children] CHECK CONSTRAINT [FK_Children_Classes]
GO
ALTER TABLE [dbo].[Children]  WITH CHECK ADD  CONSTRAINT [FK_Children_Schools] FOREIGN KEY([Id_School])
REFERENCES [dbo].[Schools] ([Id])
GO
ALTER TABLE [dbo].[Children] CHECK CONSTRAINT [FK_Children_Schools]
GO
ALTER TABLE [dbo].[Classes]  WITH NOCHECK ADD  CONSTRAINT [FK_Table_1_Schools1] FOREIGN KEY([Id_School])
REFERENCES [dbo].[Schools] ([Id])
GO
ALTER TABLE [dbo].[Classes] NOCHECK CONSTRAINT [FK_Table_1_Schools1]
GO
