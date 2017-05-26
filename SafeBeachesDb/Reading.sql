CREATE TABLE dbo.Reading
(
    [BeachId] INT NOT NULL,
    [Open] SMALLINT NOT NULL DEFAULT 0,
    [DateTested] DATE NULL,
    [DateAdded] DATE NOT NULL,
    [Temperature] INT NULL,
    [Message] NVARCHAR(MAX) NULL,
    CONSTRAINT [PK_Id] PRIMARY KEY (Beachid, DateAdded),
    CONSTRAINT [FK_BeachId] FOREIGN KEY ([BeachId]) REFERENCES [Beach]([Id])
)

GO
EXEC sp_addextendedproperty @name = N'MS_Description',
    @value = N'0 = Closed, 1 = Open, -1 = Untested',
    @level0type = N'SCHEMA',
    @level0name = N'dbo',
    @level1type = N'TABLE',
    @level1name = N'Reading',
    @level2type = N'COLUMN',
    @level2name = N'Open'