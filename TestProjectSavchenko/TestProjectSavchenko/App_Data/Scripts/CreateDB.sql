USE [master]
GO

CREATE DATABASE [TestTaskDB] ON  PRIMARY 
( NAME = N'TestTaskDB', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\TestTaskDB.mdf')
 LOG ON 
( NAME = N'TestTaskDB_log', FILENAME = N'C:\Program Files\Microsoft SQL Server\MSSQL10.MSSQLSERVER\MSSQL\DATA\TestTaskDB_log.ldf')
GO

USE [TestTaskDB]
GO
IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[User] (
  [ID] [INT] IDENTITY NOT NULL,
  [EMAIL] [nvarchar](200) UNIQUE NOT NULL,
  [PASSWORD] [nvarchar](100) NOT NULL,
  [PROVINCE] [INT] NULL,
  CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Province]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Province](
	[ID] [INT] IDENTITY NOT NULL,
	[NAME]  [nvarchar](200) NOT NULL,
 CONSTRAINT [PK_Province] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Country]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[Country] (
  [ID] [INT] IDENTITY NOT NULL,
  [NAME]  [nvarchar](200) UNIQUE NOT NULL,
  CONSTRAINT [PK_Country] PRIMARY KEY CLUSTERED 
(
	[ID] ASC
)) ON [PRIMARY]
END
GO

IF NOT EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CountryProvince]') AND type in (N'U'))
BEGIN
CREATE TABLE [dbo].[CountryProvince](
	[COUNTRY_ID] [INT] NOT NULL,
	[PROVINCE_ID] [INT] NOT NULL,
 CONSTRAINT [PK_COUNTRY_PROVINCE] PRIMARY KEY CLUSTERED 
(
	[COUNTRY_ID],[PROVINCE_ID] ASC
)WITH (PAD_INDEX  = OFF, STATISTICS_NORECOMPUTE  = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS  = ON, ALLOW_PAGE_LOCKS  = ON) ON [PRIMARY]
) ON [PRIMARY]
END
GO

CREATE TABLE [dbo].[DatabaseVersion](
	[VersionNumber] [int] NOT NULL,
	[VersionDate] [datetime] NOT NULL,
	[VersionUpdater] [nvarchar](50) NOT NULL
) ON [PRIMARY]

GO

insert into DatabaseVersion (VersionNumber,VersionDate,VersionUpdater) values (0,SYSDATETIME(),'dmitriy.savchenko')
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_CountryProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryProvince]'))
ALTER TABLE [dbo].[CountryProvince]  WITH CHECK ADD  CONSTRAINT [FK_Country_CountryProvince] FOREIGN KEY([COUNTRY_ID])
REFERENCES [dbo].[Country] ([ID])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Country_CountryProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryProvince]'))
ALTER TABLE [dbo].[CountryProvince] CHECK CONSTRAINT [FK_Country_CountryProvince]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_CountryProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryProvince]'))
ALTER TABLE [dbo].[CountryProvince]  WITH CHECK ADD  CONSTRAINT [FK_Province_CountryProvince] FOREIGN KEY([PROVINCE_ID])
REFERENCES [dbo].[Province] ([ID])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_CountryProvince]') AND parent_object_id = OBJECT_ID(N'[dbo].[CountryProvince]'))
ALTER TABLE [dbo].[CountryProvince] CHECK CONSTRAINT [FK_Province_CountryProvince]
GO

IF NOT EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User]  WITH CHECK ADD  CONSTRAINT [FK_Province_User] FOREIGN KEY([PROVINCE])
REFERENCES [dbo].[Province] ([ID])
ON DELETE CASCADE
GO
IF  EXISTS (SELECT * FROM sys.foreign_keys WHERE object_id = OBJECT_ID(N'[dbo].[FK_Province_User]') AND parent_object_id = OBJECT_ID(N'[dbo].[User]'))
ALTER TABLE [dbo].[User] CHECK CONSTRAINT [FK_Province_User]
GO
