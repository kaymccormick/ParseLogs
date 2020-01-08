
-- --------------------------------------------------
-- Entity Designer DDL Script for SQL Server 2005, 2008, 2012 and Azure
-- --------------------------------------------------
-- Date Created: 01/08/2020 05:07:49
-- Generated from EDMX file: C:\Users\mccor.LAPTOP-T6T0BN1K\source\repos\ParseLogs\ParseLogs\Model2.edmx
-- --------------------------------------------------

SET QUOTED_IDENTIFIER OFF;
GO
USE [parselogs];
GO
IF SCHEMA_ID(N'dbo') IS NULL EXECUTE(N'CREATE SCHEMA [dbo]');
GO

-- --------------------------------------------------
-- Dropping existing FOREIGN KEY constraints
-- --------------------------------------------------


-- --------------------------------------------------
-- Dropping existing tables
-- --------------------------------------------------

IF OBJECT_ID(N'[dbo].[LogEntries]', 'U') IS NOT NULL
    DROP TABLE [dbo].[LogEntries];
GO

-- --------------------------------------------------
-- Creating all tables
-- --------------------------------------------------

-- Creating table 'LogEntries'
CREATE TABLE [dbo].[LogEntries] (
    [Id] int IDENTITY(1,1) NOT NULL
);
GO

-- Creating table 'FileInfoes'
CREATE TABLE [dbo].[FileInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Extension] nvarchar(max)  NOT NULL,
    [Length] bigint  NOT NULL
);
GO

-- Creating table 'Entity1'
CREATE TABLE [dbo].[Entity1] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [Extension] nvarchar(max)  NOT NULL
);
GO

-- Creating table 'FileSystemInfoes'
CREATE TABLE [dbo].[FileSystemInfoes] (
    [Id] int IDENTITY(1,1) NOT NULL,
    [FullName] nvarchar(max)  NOT NULL,
    [Parent] int  NOT NULL,
    [CreationTimeUTC] datetime  NOT NULL,
    [Attributes] bigint  NOT NULL,
    [LastAccessTimeUTC] datetime  NOT NULL,
    [LastWriteTimeUTC] datetime  NOT NULL,
    [ParentInfo_Id] int  NOT NULL
);
GO

-- --------------------------------------------------
-- Creating all PRIMARY KEY constraints
-- --------------------------------------------------

-- Creating primary key on [Id] in table 'LogEntries'
ALTER TABLE [dbo].[LogEntries]
ADD CONSTRAINT [PK_LogEntries]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileInfoes'
ALTER TABLE [dbo].[FileInfoes]
ADD CONSTRAINT [PK_FileInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'Entity1'
ALTER TABLE [dbo].[Entity1]
ADD CONSTRAINT [PK_Entity1]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- Creating primary key on [Id] in table 'FileSystemInfoes'
ALTER TABLE [dbo].[FileSystemInfoes]
ADD CONSTRAINT [PK_FileSystemInfoes]
    PRIMARY KEY CLUSTERED ([Id] ASC);
GO

-- --------------------------------------------------
-- Creating all FOREIGN KEY constraints
-- --------------------------------------------------

-- Creating foreign key on [ParentInfo_Id] in table 'FileSystemInfoes'
ALTER TABLE [dbo].[FileSystemInfoes]
ADD CONSTRAINT [FK_FileSystemInfoFileSystemInfo]
    FOREIGN KEY ([ParentInfo_Id])
    REFERENCES [dbo].[FileSystemInfoes]
        ([Id])
    ON DELETE NO ACTION ON UPDATE NO ACTION;
GO

-- Creating non-clustered index for FOREIGN KEY 'FK_FileSystemInfoFileSystemInfo'
CREATE INDEX [IX_FK_FileSystemInfoFileSystemInfo]
ON [dbo].[FileSystemInfoes]
    ([ParentInfo_Id]);
GO

-- --------------------------------------------------
-- Script has ended
-- --------------------------------------------------