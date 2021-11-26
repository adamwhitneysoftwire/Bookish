DROP TABLE [BookCheckout]
GO
DROP TABLE [BookInstance]
GO
DROP TABLE [BookAuthor]
GO
DROP TABLE [Author]
GO
DROP TABLE [Book]
GO
drop table [AspNetRoleClaims]
go
drop table [AspNetUserClaims]
go
drop table [AspNetUserLogins]
go
drop table [AspNetUserRoles]
go
drop table [AspNetRoles]
go
drop table [AspNetUserTokens]
go
drop table [AspNetUsers]
go
drop table [__EFMigrationsHistory]
go

-- ************************************** [Author]
CREATE TABLE [Author]
(
 [Id]   int NOT NULL IDENTITY ,
 [Name] varchar(50) NOT NULL ,


 CONSTRAINT [PK_9] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- ************************************** [Book]
CREATE TABLE [Book]
(
 [Id]    int NOT NULL IDENTITY ,
 [Title] varchar(50) NOT NULL ,
 [Isbn]  bigint NOT NULL ,


 CONSTRAINT [PK_5] PRIMARY KEY CLUSTERED ([Id] ASC)
);
GO

-- ************************************** [BookAuthor]
CREATE TABLE [BookAuthor]
(
 [Id] int NOT NULL IDENTITY ,
 [BookId]       int NOT NULL ,
 [AuthorId]     int NOT NULL ,


 CONSTRAINT [PK_34] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_27] FOREIGN KEY ([BookId])  REFERENCES [Book]([Id]),
 CONSTRAINT [FK_30] FOREIGN KEY ([AuthorId])  REFERENCES [Author]([Id])
);
GO


CREATE NONCLUSTERED INDEX [FK_29] ON [BookAuthor] 
 (
  [BookId] ASC
 )
GO

CREATE NONCLUSTERED INDEX [FK_32] ON [BookAuthor] 
 (
  [AuthorId] ASC
 )
GO

-- ************************************** [BookInstance]
CREATE TABLE [BookInstance]
(
 [Id] int NOT NULL IDENTITY ,
 [BookId]         int NOT NULL ,


 CONSTRAINT [PK_42] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_46] FOREIGN KEY ([BookId])  REFERENCES [Book]([Id])
);
GO


CREATE NONCLUSTERED INDEX [FK_48] ON [BookInstance] 
 (
  [BookId] ASC
 )

GO

 -- ************************************** [BookCheckout]
CREATE TABLE [BookCheckout]
(
 [Id]         int NOT NULL IDENTITY ,
 [UserId]             nvarchar(50) NOT NULL ,
 [InstanceId]     int NOT NULL ,
 [ReturnDate] datetime NOT NULL ,
 [Returned]   bit NOT NULL ,


 CONSTRAINT [PK_51] PRIMARY KEY CLUSTERED ([Id] ASC),
 CONSTRAINT [FK_52] FOREIGN KEY ([UserId])  REFERENCES [AspNetUsers]([Id]),
 CONSTRAINT [FK_55] FOREIGN KEY ([InstanceId])  REFERENCES [BookInstance]([Id])
);
GO


CREATE NONCLUSTERED INDEX [FK_54] ON [BookCheckout] 
 (
  [UserId] ASC
 )

GO

CREATE NONCLUSTERED INDEX [FK_57] ON [BookCheckout] 
 (
  [InstanceId] ASC
 )
GO
