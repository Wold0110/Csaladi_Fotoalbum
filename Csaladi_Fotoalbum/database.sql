-- fotó albumok
CREATE TABLE [dbo].[albums]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] TEXT NOT NULL, 
    [date] DATETIME NOT NULL, 
    [location] INT NOT NULL, 
    [color] INT NOT NULL
)
-- színek
CREATE TABLE [dbo].[colors]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] TEXT NOT NULL, 
    [colorcode] TEXT NOT NULL
)
--helyszínek
CREATE TABLE [dbo].[locations]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] TEXT NOT NULL
)
--emberek
CREATE TABLE [dbo].[people]
(
	[id] INT NOT NULL PRIMARY KEY IDENTITY, 
    [name] TEXT NOT NULL
)
-- ember x album
CREATE TABLE [dbo].[connections]
(
    [id] INT NOT NULL PRIMARY KEY IDENTITY,
	[person] INT NOT NULL, 
    [album] INT NOT NULL
)