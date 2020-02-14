﻿CREATE PROCEDURE [dbo].[VerifyToken]
	@token NVARCHAR(16)
AS
	IF EXISTS (SELECT Token FROM AuthToken WHERE Token = @token)
	IF ((SELECT Date FROM AuthToken WHERE Token = @token) > SYSUTCDATETIME())
	SELECT 1
	ELSE
	SELECT 0
	ELSE
	SELECT 0