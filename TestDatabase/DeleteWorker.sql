CREATE PROCEDURE [dbo].[DeleteWorker]
	@code NVARCHAR(16),
	@owner NVARCHAR(16)
AS
	DELETE FROM Worker WHERE code=@code AND owner_code=@owner