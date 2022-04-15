CREATE TABLE [dbo].[games] (
    [Id]        INT            IDENTITY (1, 1) NOT NULL,
	[DateTime] DATETIME NOT NULL, 
    [firstName] NVARCHAR (50)  NOT NULL,
    [lastName]  NVARCHAR (50)  NOT NULL,
    [gameData]  NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([Id] ASC)
);

