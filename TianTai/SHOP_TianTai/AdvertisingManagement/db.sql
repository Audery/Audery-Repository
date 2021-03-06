USE [master]
GO
/****** Object:  Database [AdvertisingManagement_RJ]    Script Date: 11/24/2014 16:09:40 ******/
CREATE DATABASE [AdvertisingManagement_RJ] ON  PRIMARY 
( NAME = N'AdvertisingManagement_RJ', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AdvertisingManagement_RJ.mdf' , SIZE = 3072KB , MAXSIZE = UNLIMITED, FILEGROWTH = 1024KB )
 LOG ON 
( NAME = N'AdvertisingManagement_RJ_log', FILENAME = N'D:\Program Files\Microsoft SQL Server\MSSQL10_50.MSSQLSERVER\MSSQL\DATA\AdvertisingManagement_RJ_log.ldf' , SIZE = 2816KB , MAXSIZE = 2048GB , FILEGROWTH = 10%)
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET COMPATIBILITY_LEVEL = 100
GO
IF (1 = FULLTEXTSERVICEPROPERTY('IsFullTextInstalled'))
begin
EXEC [AdvertisingManagement_RJ].[dbo].[sp_fulltext_database] @action = 'enable'
end
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ANSI_NULL_DEFAULT OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ANSI_NULLS OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ANSI_PADDING OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ANSI_WARNINGS OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ARITHABORT OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET AUTO_CLOSE OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET AUTO_CREATE_STATISTICS ON
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET AUTO_SHRINK OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET AUTO_UPDATE_STATISTICS ON
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET CURSOR_CLOSE_ON_COMMIT OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET CURSOR_DEFAULT  GLOBAL
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET CONCAT_NULL_YIELDS_NULL OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET NUMERIC_ROUNDABORT OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET QUOTED_IDENTIFIER OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET RECURSIVE_TRIGGERS OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET  DISABLE_BROKER
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET AUTO_UPDATE_STATISTICS_ASYNC OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET DATE_CORRELATION_OPTIMIZATION OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET TRUSTWORTHY OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET ALLOW_SNAPSHOT_ISOLATION OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET PARAMETERIZATION SIMPLE
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET READ_COMMITTED_SNAPSHOT OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET HONOR_BROKER_PRIORITY OFF
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET  READ_WRITE
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET RECOVERY FULL
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET  MULTI_USER
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET PAGE_VERIFY CHECKSUM
GO
ALTER DATABASE [AdvertisingManagement_RJ] SET DB_CHAINING OFF
GO
EXEC sys.sp_db_vardecimal_storage_format N'AdvertisingManagement_RJ', N'ON'
GO
USE [AdvertisingManagement_RJ]
GO
/****** Object:  Table [dbo].[users]    Script Date: 11/24/2014 16:09:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[users](
	[NetNo] [nvarchar](5) NOT NULL,
	[NetName] [nvarchar](50) NULL,
	[NetKey] [nvarchar](50) NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[ad_content]    Script Date: 11/24/2014 16:09:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_content](
	[ContentID] [int] IDENTITY(1,1) NOT NULL,
	[ConfigID] [int] NULL,
	[Picture] [nvarchar](200) NULL,
	[Url] [nvarchar](200) NULL,
	[NetNo] [nvarchar](5) NULL,
	[Detials] [nvarchar](50) NULL,
	[Domain] [nvarchar](50) NULL,
	[CreatedTime] [datetime] NULL,
	[StopTime] [datetime] NULL,
 CONSTRAINT [PK_AD_CONTENT] PRIMARY KEY CLUSTERED 
(
	[ContentID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'配置类型ID' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'ConfigID'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容资源' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'Picture'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'跳转连接地址' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'Url'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'用户身份编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'NetNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'简要说明' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'Detials'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容到期时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content', @level2type=N'COLUMN',@level2name=N'StopTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'各站点展示的内容表（大小和类型有配置表指定）' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_content'
GO
/****** Object:  Table [dbo].[ad_config]    Script Date: 11/24/2014 16:09:42 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[ad_config](
	[ConfigID] [int] IDENTITY(1,1) NOT NULL,
	[AdName] [nvarchar](50) NULL,
	[NetName] [nvarchar](50) NULL,
	[Width] [int] NULL,
	[Height] [int] NULL,
	[Resource] [int] NULL,
	[Channel] [nvarchar](20) NULL,
	[NetNo] [nvarchar](5) NULL,
	[CreatedTime] [datetime] NULL,
 CONSTRAINT [PK_AD_CONFIG] PRIMARY KEY CLUSTERED 
(
	[ConfigID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'AdName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站名称' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'NetName'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'宽度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'Width'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'高度' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'Height'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'jpg、flash' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'Resource'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'中药，基药，搜索等频道' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'Channel'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'网站编号' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'NetNo'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'创建时间' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config', @level2type=N'COLUMN',@level2name=N'CreatedTime'
GO
EXEC sys.sp_addextendedproperty @name=N'MS_Description', @value=N'内容有101广告系统管理员创建。' , @level0type=N'SCHEMA',@level0name=N'dbo', @level1type=N'TABLE',@level1name=N'ad_config'
GO
/****** Object:  Default [DF_ad_content_CreatedTime]    Script Date: 11/24/2014 16:09:42 ******/
ALTER TABLE [dbo].[ad_content] ADD  CONSTRAINT [DF_ad_content_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
/****** Object:  Default [DF_ad_config_CreatedTime]    Script Date: 11/24/2014 16:09:42 ******/
ALTER TABLE [dbo].[ad_config] ADD  CONSTRAINT [DF_ad_config_CreatedTime]  DEFAULT (getdate()) FOR [CreatedTime]
GO
