CREATE TABLE [Transaction] (
    [Id]              BIGINT            NOT NULL IDENTITY (1, 1),
    [AccountId]       INT               NOT NULL,
    [Amount]          DECIMAL (14,3)    NOT NULL,
    [Currency]        INT               NOT NULL,
    [TransactionType] INT               NOT NULL,
    [Date]            DATETIME2 (7)     NOT NULL
    CONSTRAINT [PK_TRANSACTION] PRIMARY KEY CLUSTERED ([Id] ASC)
);