CREATE PROCEDURE [dbo].[GetUserCodeByEmail]
	@email NVARCHAR(100)
AS
	IF EXISTS (SELECT email FROM UserDetails WHERE email = @email)
	SELECT code FROM UserDetails WHERE email = @email
	ELSE
	SELECT 0
