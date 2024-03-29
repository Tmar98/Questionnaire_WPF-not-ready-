USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Classes]    Script Date: 31.10.2019 18:17:49 ******/
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
ALTER TABLE [dbo].[Classes]  WITH NOCHECK ADD  CONSTRAINT [FK_Table_1_Schools1] FOREIGN KEY([Id_School])
REFERENCES [dbo].[Schools] ([Id])
GO
ALTER TABLE [dbo].[Classes] NOCHECK CONSTRAINT [FK_Table_1_Schools1]
GO
