CREATE TABLE [dbo].[UserDetails]
(
	[code] NVARCHAR(16) NOT NULL PRIMARY KEY, 
    [email] NVARCHAR(100) NOT NULL, 
    [datecreated] DATETIME NOT NULL, 
    [entityname] NVARCHAR(100) NOT NULL
)
