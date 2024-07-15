CREATE TABLE [dbo].[Room] (
    [roomID]       INT            IDENTITY (1, 1) NOT NULL,
    [roomName]     NVARCHAR (MAX) NULL,
    [roomTypeCode] NVARCHAR (MAX) NULL,
    [guest]        INT            NOT NULL,
    CONSTRAINT [PK_Room] PRIMARY KEY CLUSTERED ([roomID] ASC),
	CONSTRAINT [FK_Room] FOREIGN KEY ([roomTypeCode]) REFERENCES [Room] ([roomTypeCode]) ON DELETE CASCADE
);

