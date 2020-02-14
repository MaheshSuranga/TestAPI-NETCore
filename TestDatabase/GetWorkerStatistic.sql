CREATE PROCEDURE [dbo].[GetWorkerStatistic]
	@code NVARCHAR(16),
	@owner NVARCHAR(16)
AS
	SELECT hourly_rate, hours_worked, overtime_rate, overtime_worked FROM WorkerStatistic WHERE code=@code AND owner_code=@owner
