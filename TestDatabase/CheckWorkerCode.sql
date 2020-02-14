CREATE PROCEDURE [dbo].[CheckWorkerCode]
	@code NVARCHAR(16)
AS
	IF NOT EXISTS (SELECT code FROM Worker WHERE code=@code)
	SELECT '1'
	ELSE
	SELECT '0'
