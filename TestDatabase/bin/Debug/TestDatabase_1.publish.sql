﻿/*
Deployment script for TestDatabase

This code was generated by a tool.
Changes to this file may cause incorrect behavior and will be lost if
the code is regenerated.
*/

GO
SET ANSI_NULLS, ANSI_PADDING, ANSI_WARNINGS, ARITHABORT, CONCAT_NULL_YIELDS_NULL, QUOTED_IDENTIFIER ON;

SET NUMERIC_ROUNDABORT OFF;


GO
:setvar DatabaseName "TestDatabase"
:setvar DefaultFilePrefix "TestDatabase"
:setvar DefaultDataPath "C:\Users\UnicornMAS\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"
:setvar DefaultLogPath "C:\Users\UnicornMAS\AppData\Local\Microsoft\Microsoft SQL Server Local DB\Instances\MSSQLLocalDB"

GO
:on error exit
GO
/*
Detect SQLCMD mode and disable script execution if SQLCMD mode is not supported.
To re-enable the script after enabling SQLCMD mode, execute the following:
SET NOEXEC OFF; 
*/
:setvar __IsSqlCmdEnabled "True"
GO
IF N'$(__IsSqlCmdEnabled)' NOT LIKE N'True'
    BEGIN
        PRINT N'SQLCMD mode must be enabled to successfully execute this script.';
        SET NOEXEC ON;
    END


GO
USE [$(DatabaseName)];


GO
PRINT N'Starting rebuilding table [dbo].[AuthToken]...';


GO
BEGIN TRANSACTION;

SET TRANSACTION ISOLATION LEVEL SERIALIZABLE;

SET XACT_ABORT ON;

CREATE TABLE [dbo].[tmp_ms_xx_AuthToken] (
    [Id]       INT           IDENTITY (1, 1) NOT NULL,
    [Token]    NVARCHAR (16) NOT NULL,
    [UserCode] NVARCHAR (16) NOT NULL,
    [Date]     DATETIME      NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

IF EXISTS (SELECT TOP 1 1 
           FROM   [dbo].[AuthToken])
    BEGIN
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AuthToken] ON;
        INSERT INTO [dbo].[tmp_ms_xx_AuthToken] ([Id], [Token], [UserCode], [Date])
        SELECT   [Id],
                 [Token],
                 [UserCode],
                 [Date]
        FROM     [dbo].[AuthToken]
        ORDER BY [Id] ASC;
        SET IDENTITY_INSERT [dbo].[tmp_ms_xx_AuthToken] OFF;
    END

DROP TABLE [dbo].[AuthToken];

EXECUTE sp_rename N'[dbo].[tmp_ms_xx_AuthToken]', N'AuthToken';

COMMIT TRANSACTION;

SET TRANSACTION ISOLATION LEVEL READ COMMITTED;


GO
PRINT N'Refreshing [dbo].[InsertAuthToken]...';


GO
EXECUTE sp_refreshsqlmodule N'[dbo].[InsertAuthToken]';


GO
PRINT N'Update complete.';


GO
