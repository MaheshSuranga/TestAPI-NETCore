CREATE PROCEDURE [dbo].[InsertWorker]
	@code NVARCHAR(16),
	@name NVARCHAR(100),
	@description NVARCHAR(200),
	@location NVARCHAR(50),
	@position NVARCHAR(100),
	@owner NVARCHAR(16)
AS
	INSERT INTO Worker
	(code, name, description, location, position, owner_code, date_created)
	VALUES
	(@code, @name, @description, @location, @position, @owner, SYSUTCDATETIME())
