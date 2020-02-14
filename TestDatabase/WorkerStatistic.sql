CREATE TABLE [dbo].[WorkerStatistic]
(
	[code] NVARCHAR(16) NOT NULL PRIMARY KEY, 
    [hourly_rate] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [hours_worked] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [overtime_rate] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [overtime_worked] DECIMAL(18, 2) NOT NULL DEFAULT 0, 
    [owner_code] NVARCHAR(16) NOT NULL
)
