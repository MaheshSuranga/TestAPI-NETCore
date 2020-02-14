CREATE PROCEDURE [dbo].[CheckEmail]
	@email NVARCHAR(100)
AS
	IF NOT EXISTS (SELECT email from UserDetails WHERE email = @email)
	SELECT '1'
	ELSE
	SELECT '0'