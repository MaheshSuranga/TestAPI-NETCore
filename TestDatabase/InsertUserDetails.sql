CREATE PROCEDURE [dbo].[InsertUserDetails]
	@code NVARCHAR(16),
	@email NVARCHAR(100),
	@entity NVARCHAR(100)
AS
	INSERT INTO UserDetails
	(code, email, datecreated, entityname)
	VALUES
	(@code, @email, SYSUTCDATETIME(), @entity)
