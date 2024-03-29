USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Children]    Script Date: 31.10.2019 18:17:49 ******/
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
