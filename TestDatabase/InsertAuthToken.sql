CREATE PROCEDURE [dbo].[InsertAuthToken]
	@usercode NVARCHAR(16),
	@token NVARCHAR(16)
AS
	INSERT INTO AuthToken
	(UserCode, Token, Date)
	VALUES
	(@token, @usercode, DATEADD(day,2,SYSUTCDATETIME()))
