CREATE PROCEDURE [dbo].[GetWorker]
	@code NVARCHAR(16),
	@owner NVARCHAR(16)
AS
	SELECT name,description,location,position,date_created FROM Worker WHERE code=@code AND owner_code=@owner
