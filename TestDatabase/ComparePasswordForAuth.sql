CREATE PROCEDURE [dbo].[ComparePasswordForAuth]
	@code NVARCHAR(16),
	@pass NVARCHAR(512)
AS
	IF((SELECT pass FROM UserTable WHERE code = @code) = @pass)
	SELECT 1
	ELSE
	SELECT 0
