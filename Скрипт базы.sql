USE [master]
GO
/****** Object:  Database [Questionare_DB]    Script Date: 14.10.2019 11:16:32 ******/
CREATE DATABASE [Questionare_DB]
 CONTAINMENT = NONE
 ON  PRIMARY 
( NAME = N'Questionare_DB', FILENAME = N'F:\SQL\Questionare_DB.mdf' , SIZE = 8192KB , MAXSIZE = UNLIMITED, FILEGROWTH = 65536KB )
 LOG ON 
( NAME = N'Questionare_DB_log', FILENAME = N'F:\SQL\Questionare_DB_log.ldf' , SIZE = 8192KB , MAXSIZE = 2048GB , FILEGROWTH = 65536KB )
GO
ALTER DATABASE [Questionare_DB] SET COMPATIBILITY_LEVEL = 140
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [Questionare_DB].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [Questionare_DB] SET ANSI_NULL_DEFAULT OFF 
GO
ALTER DATABASE [Questionare_DB] SET ANSI_NULLS OFF 
GO
ALTER DATABASE [Questionare_DB] SET ANSI_PADDING OFF 
GO
ALTER DATABASE [Questionare_DB] SET ANSI_WARNINGS OFF 
GO
ALTER DATABASE [Questionare_DB] SET ARITHABORT OFF 
GO
ALTER DATABASE [Questionare_DB] SET AUTO_CLOSE ON 
GO
ALTER DATABASE [Questionare_DB] SET AUTO_SHRINK OFF 
GO
ALTER DATABASE [Questionare_DB] SET AUTO_UPDATE_STATISTICS ON 
GO
ALTER DATABASE [Questionare_DB] SET CURSOR_CLOSE_ON_COMMIT OFF 
GO
ALTER DATABASE [Questionare_DB] SET CURSOR_DEFAULT  GLOBAL 
GO
ALTER DATABASE [Questionare_DB] SET CONCAT_NULL_YIELDS_NULL OFF 
GO
ALTER DATABASE [Questionare_DB] SET NUMERIC_ROUNDABORT OFF 
GO
ALTER DATABASE [Questionare_DB] SET QUOTED_IDENTIFIER OFF 
GO
ALTER DATABASE [Questionare_DB] SET RECURSIVE_TRIGGERS OFF 
GO
ALTER DATABASE [Questionare_DB] SET  DISABLE_BROKER 
GO
ALTER DATABASE [Questionare_DB] SET AUTO_UPDATE_STATISTICS_ASYNC OFF 
GO
ALTER DATABASE [Questionare_DB] SET DATE_CORRELATION_OPTIMIZATION OFF 
GO
ALTER DATABASE [Questionare_DB] SET TRUSTWORTHY OFF 
GO
ALTER DATABASE [Questionare_DB] SET ALLOW_SNAPSHOT_ISOLATION OFF 
GO
ALTER DATABASE [Questionare_DB] SET PARAMETERIZATION SIMPLE 
GO
ALTER DATABASE [Questionare_DB] SET READ_COMMITTED_SNAPSHOT OFF 
GO
ALTER DATABASE [Questionare_DB] SET HONOR_BROKER_PRIORITY OFF 
GO
ALTER DATABASE [Questionare_DB] SET RECOVERY SIMPLE 
GO
ALTER DATABASE [Questionare_DB] SET  MULTI_USER 
GO
ALTER DATABASE [Questionare_DB] SET PAGE_VERIFY CHECKSUM  
GO
ALTER DATABASE [Questionare_DB] SET DB_CHAINING OFF 
GO
ALTER DATABASE [Questionare_DB] SET FILESTREAM( NON_TRANSACTED_ACCESS = OFF ) 
GO
ALTER DATABASE [Questionare_DB] SET TARGET_RECOVERY_TIME = 60 SECONDS 
GO
ALTER DATABASE [Questionare_DB] SET DELAYED_DURABILITY = DISABLED 
GO
ALTER DATABASE [Questionare_DB] SET QUERY_STORE = OFF
GO
USE [Questionare_DB]
GO
/****** Object:  Table [dbo].[Children]    Script Date: 14.10.2019 11:16:32 ******/
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
/****** Object:  Table [dbo].[Classes]    Script Date: 14.10.2019 11:16:32 ******/
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
/****** Object:  Table [dbo].[EGE_Questions]    Script Date: 14.10.2019 11:16:32 ******/
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
/****** Object:  Table [dbo].[Questionnaire_Answers]    Script Date: 14.10.2019 11:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Questionnaire_Answers](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Children] [int] NOT NULL,
	[Test_Number] [int] NOT NULL,
	[Date] [smalldatetime] NOT NULL,
	[Test_Result_Id] [bit] NULL,
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
/****** Object:  Table [dbo].[Results_Table]    Script Date: 14.10.2019 11:16:32 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Results_Table](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[Id_Answer] [int] NOT NULL,
	[Result1] [nvarchar](40) NOT NULL,
	[Result2] [nvarchar](40) NULL,
	[Result3] [nvarchar](40) NULL,
	[Result4] [nvarchar](40) NULL,
	[Result5] [nvarchar](40) NULL,
 CONSTRAINT [PK_Results_Table] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Schools]    Script Date: 14.10.2019 11:16:32 ******/
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
ALTER TABLE [dbo].[Questionnaire_Answers]  WITH CHECK ADD  CONSTRAINT [FK_Questionnaire_Answers_Children] FOREIGN KEY([Id_Children])
REFERENCES [dbo].[Children] ([Id])
GO
ALTER TABLE [dbo].[Questionnaire_Answers] CHECK CONSTRAINT [FK_Questionnaire_Answers_Children]
GO
ALTER TABLE [dbo].[Results_Table]  WITH CHECK ADD  CONSTRAINT [FK_Results_Table_Questionnaire_Answers] FOREIGN KEY([Id_Answer])
REFERENCES [dbo].[Questionnaire_Answers] ([Id])
GO
ALTER TABLE [dbo].[Results_Table] CHECK CONSTRAINT [FK_Results_Table_Questionnaire_Answers]
GO
USE [master]
GO
ALTER DATABASE [Questionare_DB] SET  READ_WRITE 
GO
