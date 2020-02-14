CREATE PROCEDURE [dbo].[InsertUserCredentials]
	@code NVARCHAR(16),
	@pass NVARCHAR(512)
AS
	INSERT INTO UserTable
	(code, pass)
	VALUES
	(@code, @pass)
