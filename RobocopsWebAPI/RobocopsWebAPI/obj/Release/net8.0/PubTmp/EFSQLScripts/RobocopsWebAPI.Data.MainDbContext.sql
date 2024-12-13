IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [Users] (
        [UserId] nvarchar(450) NOT NULL,
        [Email] nvarchar(max) NULL,
        [FullName] nvarchar(max) NULL,
        [Bio] nvarchar(max) NULL,
        [ProfilePicURL] nvarchar(max) NULL,
        [UserCreationDate] datetime2 NOT NULL,
        CONSTRAINT [PK_Users] PRIMARY KEY ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [FriendRequests] (
        [RequestId] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NULL,
        [ReceiverUserId] nvarchar(450) NULL,
        [FriendRequestReceived] bit NOT NULL,
        [RequestApproval] nvarchar(max) NULL,
        CONSTRAINT [PK_FriendRequests] PRIMARY KEY ([RequestId]),
        CONSTRAINT [FK_FriendRequests_Users_ReceiverUserId] FOREIGN KEY ([ReceiverUserId]) REFERENCES [Users] ([UserId]),
        CONSTRAINT [FK_FriendRequests_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [Friends] (
        [FriendshipId] nvarchar(450) NOT NULL,
        [UserId] nvarchar(450) NULL,
        [FriendsUserId] nvarchar(max) NULL,
        [FriendSince] datetime2 NULL,
        CONSTRAINT [PK_Friends] PRIMARY KEY ([FriendshipId]),
        CONSTRAINT [FK_Friends_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [Posts] (
        [PostId] nvarchar(450) NOT NULL,
        [UserId] nvarchar(450) NULL,
        [PostImageURL] nvarchar(max) NULL,
        [PostCaption] nvarchar(max) NULL,
        [PostTimeStamp] datetime2 NOT NULL,
        [IsPinned] bit NULL,
        CONSTRAINT [PK_Posts] PRIMARY KEY ([PostId]),
        CONSTRAINT [FK_Posts_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [Comments] (
        [CommentId] nvarchar(450) NOT NULL,
        [PostId] nvarchar(450) NULL,
        [UserId] nvarchar(450) NULL,
        [CommentText] nvarchar(max) NULL,
        [CommentTimeStamp] datetime2 NOT NULL,
        CONSTRAINT [PK_Comments] PRIMARY KEY ([CommentId]),
        CONSTRAINT [FK_Comments_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]),
        CONSTRAINT [FK_Comments_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE TABLE [Likes] (
        [Id] int NOT NULL IDENTITY,
        [UserId] nvarchar(450) NULL,
        [PostId] nvarchar(450) NULL,
        CONSTRAINT [PK_Likes] PRIMARY KEY ([Id]),
        CONSTRAINT [FK_Likes_Posts_PostId] FOREIGN KEY ([PostId]) REFERENCES [Posts] ([PostId]),
        CONSTRAINT [FK_Likes_Users_UserId] FOREIGN KEY ([UserId]) REFERENCES [Users] ([UserId])
    );
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Comments_PostId] ON [Comments] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Comments_UserId] ON [Comments] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_FriendRequests_ReceiverUserId] ON [FriendRequests] ([ReceiverUserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_FriendRequests_UserId] ON [FriendRequests] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Friends_UserId] ON [Friends] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Likes_PostId] ON [Likes] ([PostId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Likes_UserId] ON [Likes] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    CREATE INDEX [IX_Posts_UserId] ON [Posts] ([UserId]);
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241203173043_1th'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241203173043_1th', N'8.0.10');
END;
GO

COMMIT;
GO

BEGIN TRANSACTION;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206101218_2nd'
)
BEGIN
    ALTER TABLE [Users] ADD [ProfilePicPublicID] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206101218_2nd'
)
BEGIN
    ALTER TABLE [Users] ADD [VideoIntroPublicID] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206101218_2nd'
)
BEGIN
    ALTER TABLE [Users] ADD [VideoIntroURL] nvarchar(max) NULL;
END;
GO

IF NOT EXISTS (
    SELECT * FROM [__EFMigrationsHistory]
    WHERE [MigrationId] = N'20241206101218_2nd'
)
BEGIN
    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20241206101218_2nd', N'8.0.10');
END;
GO

COMMIT;
GO

