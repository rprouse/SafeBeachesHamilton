CREATE TABLE [dbo].[Beach]
(
	[Id] INT NOT NULL PRIMARY KEY, 
    [Name] NVARCHAR(50) NOT NULL, 
    [Address] NVARCHAR(MAX) NOT NULL, 
    [Location] [sys].[geography] NOT NULL
)