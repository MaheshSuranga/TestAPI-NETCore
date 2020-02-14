CREATE PROCEDURE [dbo].[InsertWorkerStatistic]
	@code NVARCHAR(16),
	@owner NVARCHAR(16)
AS
	INSERT INTO WorkerStatistic
	(code, owner_code)
	Values
	(@code, @owner)
