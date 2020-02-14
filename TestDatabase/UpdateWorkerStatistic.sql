CREATE PROCEDURE [dbo].[UpdateWorkerStatistic]
	@code NVARCHAR(16),
	@owner NVARCHAR(16),
	@hourlyrate DECIMAL(18,2),
	@hoursworked DECIMAL(18,2),
	@overtimerate DECIMAL(18,2),
	@overtimeworked DECIMAL(18,2)
AS
	IF (@hourlyrate != 0)
	UPDATE WorkerStatistic SET hourly_rate=@hourlyrate WHERE code=@code AND owner_code=@owner

	IF (@hoursworked != 0)
	UPDATE WorkerStatistic SET hours_worked=(SELECT hours_worked FROM WorkerStatistic WHERE code=@code AND owner_code=@owner) + @hourlyrate WHERE code=@code AND owner_code=@owner

	IF (@overtimerate != 0)
	UPDATE WorkerStatistic SET overtime_rate=@overtimerate WHERE code=@code AND owner_code=@owner

	IF (@overtimeworked != 0)
	UPDATE WorkerStatistic SET overtime_worked=(SELECT overtime_worked FROM WorkerStatistic WHERE code=@code AND owner_code=@owner) + @overtimeworked WHERE code=@code AND owner_code=@owner