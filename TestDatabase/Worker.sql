CREATE TABLE [dbo].[Worker]
(
	[code] NVARCHAR(16) NOT NULL PRIMARY KEY, 
    [name] NVARCHAR(100) NOT NULL, 
    [description] NVARCHAR(200) NULL, 
    [location] NVARCHAR(50) NOT NULL, 
    [position] NVARCHAR(100) NOT NULL, 
    [date_created] DATETIME NOT NULL, 
    [owner_code] NVARCHAR(16) NOT NULL
)
