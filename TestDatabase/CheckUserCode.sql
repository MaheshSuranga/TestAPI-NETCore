CREATE PROCEDURE [dbo].[CheckUserCode]
	@code NVARCHAR(16)
AS
	IF NOT EXISTS (SELECT code FROM UserTable WHERE code = @code)
	SELECT '1'
	ELSE
	SELECT '0'
