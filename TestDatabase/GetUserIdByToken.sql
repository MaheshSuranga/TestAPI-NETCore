CREATE PROCEDURE [dbo].[GetUserIdByToken]
	@token NVARCHAR(16)
AS
	SELECT UserCode	FROM AuthToken WHERE Token = @token
