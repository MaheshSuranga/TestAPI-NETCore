CREATE PROCEDURE [dbo].[UpdateWorker]
	@code NVARCHAR(16),
	@name NVARCHAR(100),
	@description NVARCHAR(200),
	@location NVARCHAR(50),
	@position NVARCHAR(100),
	@owner NVARCHAR(16)
AS
	UPDATE Worker SET name=@name, description=@description, location=@location, position=@position
	WHERE code=@code AND owner_code=@owner
