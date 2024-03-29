USE [codeforce_dev]
GO
/****** Object:  Table [dbo].[Coin]    Script Date: 2022/05/30 21:11:36 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostTag]') AND type in (N'U'))
DROP TABLE [dbo].[PostTag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Notification]') AND type in (N'U'))
DROP TABLE [dbo].[Notification]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Message]') AND type in (N'U'))
DROP TABLE [dbo].[Message]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PostReport]') AND type in (N'U'))
DROP TABLE [dbo].[PostReport]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserRole]') AND type in (N'U'))
DROP TABLE [dbo].[UserRole]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserCoin]') AND type in (N'U'))
DROP TABLE [dbo].[UserCoin]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[UserFollower]') AND type in (N'U'))
DROP TABLE [dbo].[UserFollower]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CoinRating]') AND type in (N'U'))
DROP TABLE [dbo].[CoinRating]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Like]') AND type in (N'U'))
DROP TABLE [dbo].[Like]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Reply]') AND type in (N'U'))
DROP TABLE [dbo].[Reply]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Comment]') AND type in (N'U'))
DROP TABLE [dbo].[Comment]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Tag]') AND type in (N'U'))
DROP TABLE [dbo].[Tag]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Post]') AND type in (N'U'))
DROP TABLE [dbo].[Post]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Coin]') AND type in (N'U'))
DROP TABLE [dbo].[Coin]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[User]') AND type in (N'U'))
DROP TABLE [dbo].[User]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Image]') AND type in (N'U'))
DROP TABLE [dbo].[Image]
GO

IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Role]') AND type in (N'U'))
DROP TABLE [dbo].[Role]
GO

/****** Object:  Table [dbo].[Role]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Role](
	[RoleId] [int] NOT NULL,
	[Name] [nvarchar](10) NOT NULL,
 CONSTRAINT [PK_Role] PRIMARY KEY CLUSTERED 
(
	[RoleId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

/****** Object:  Table [dbo].[Image]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Image](
	[ImageId] [int] IDENTITY(1,1) NOT NULL,
	[Name] [nvarchar](250) NOT NULL,
	[Url] [nvarchar](250) NOT NULL,
 CONSTRAINT [PK_Image] PRIMARY KEY CLUSTERED 
(
	[ImageId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
) ON [PRIMARY]
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Coin](
	[CoinId] [int] IDENTITY(1,1) NOT NULL,
	[CoinName] [nvarchar](50) NOT NULL,
	[ImageId] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
 CONSTRAINT [PK_Coin] PRIMARY KEY CLUSTERED 
(
	[CoinId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Coin_ImageId] FOREIGN KEY 
(
	[ImageId]
)REFERENCES [dbo].[Image] (ImageId)
)
GO

/****** Object:  Table [dbo].[User]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[User](
	[UserId] [int] IDENTITY(1,1) NOT NULL,
	[Firstname] [nvarchar](50) NOT NULL,
	[Lastname] [nvarchar](50) NOT NULL,
	[Username] [nvarchar](50) NOT NULL,
	[Email] [nvarchar](50) NOT NULL,
	[Password] [nvarchar](250) NOT NULL,
	[OTP] [int] NULL,
	[OTPExpirationTime] [datetime] NULL,
	[HasForgottenPassword] [bit] NULL,
	[ImageId] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[RoleId] [int] NOT NULL,
 CONSTRAINT [PK_User] PRIMARY KEY CLUSTERED 
(
	[UserId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_User_ImageId] FOREIGN KEY 
(
	[ImageId]
)REFERENCES [dbo].[Image] (ImageId),
CONSTRAINT [FK_User_RoleId] FOREIGN KEY 
(
	[RoleId]
)REFERENCES [dbo].[Role] (RoleId)
)
GO

/****** Object:  Table [dbo].[Post]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Post](
	[PostId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](max) NOT NULL,
	[UserId] [int] NOT NULL,
	[ImageId] [int] NULL,
	[ImageUrl] [nvarchar](max) NULL,
	[SentimentScore] [decimal](4,4) CHECK (SentimentScore >= -1 AND SentimentScore <= 1) NULL,
	[DateCreated] [datetime2] NOT NULL DEFAULT(getutcdate()),
 CONSTRAINT [PK_Post] PRIMARY KEY CLUSTERED 
(
	[PostId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Post_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_Post_ImageId] FOREIGN KEY 
(
	[ImageId]
)REFERENCES [dbo].[Image] (ImageId)
)
GO

/****** Object:  Table [dbo].[Tag]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Tag](
	[TagId] [int] IDENTITY(1,1) NOT NULL,
	[Content] [nvarchar](50) NOT NULL,
 CONSTRAINT [PK_Tag] PRIMARY KEY CLUSTERED 
(
	[TagId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
)
GO

/****** Object:  Table [dbo].[Comment]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Comment](
	[CommentId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
	[Content] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Comment] PRIMARY KEY CLUSTERED 
(
	[CommentId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Comment_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_Comment_PostId] FOREIGN KEY 
(
	[PostId]
)REFERENCES [dbo].[Post] (PostId)
)
GO

/****** Object:  Table [dbo].[Reply]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Reply](
	[ReplyId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CommentId] [int] NOT NULL,
	[Content] [nvarchar](4000) NOT NULL,
 CONSTRAINT [PK_Reply] PRIMARY KEY CLUSTERED 
(
	[ReplyId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Reply_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_Reply_CommentId] FOREIGN KEY 
(
	[CommentId]
)REFERENCES [dbo].[Comment] (CommentId)
)
GO

/****** Object:  Table [dbo].[Like]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Like](
	[LikeId] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PostId] [int] NULL,
    [CommentId] [int] NULL,
    [ReplyId] [int] NULL,
 CONSTRAINT [PK_Like] PRIMARY KEY CLUSTERED 
(
	[LikeId] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Like_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_Like_PostId] FOREIGN KEY 
(
	[PostId]
)REFERENCES [dbo].[Post] (PostId),
CONSTRAINT [FK_Like_CommentId] FOREIGN KEY 
(
	[CommentId]
)REFERENCES [dbo].[Comment] (CommentId),
CONSTRAINT [FK_Like_ReplyId] FOREIGN KEY 
(
	[ReplyId]
)REFERENCES [dbo].[Reply] (ReplyId)
)
GO

/****** Object:  Table [dbo].[UserCoin]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserCoin](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[CoinId] [int] NOT NULL,
 CONSTRAINT [PK_UserCoin] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_UserCoin_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_UserCoin_CoinId] FOREIGN KEY 
(
	[CoinId]
)REFERENCES [dbo].[Coin] (CoinId)
)
GO

/****** Object:  Table [dbo].[CoinRating]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CoinRating](
	[Id] [int] IDENTITY(1,1) NOT NULL,
    [CoinId] [int] NOT NULL,
	[UserId] [int] NOT NULL,
    [Rating] [int] CHECK (Rating >=1 AND Rating <= 5) NOT NULL,
 CONSTRAINT [PK_CoinRating] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_CoinRating_CoinId] FOREIGN KEY 
(
	[CoinId]
)REFERENCES [dbo].[Coin] (CoinId),
CONSTRAINT [FK_CoinRating_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
)
GO

/****** Object:  Table [dbo].[UserFollower]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[UserFollower](
	[id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[FollowId] [int] NOT NULL,
	[FollowDate] [datetime] NOT NULL,
 CONSTRAINT [PK_UserFollower] PRIMARY KEY CLUSTERED 
(
	[id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_UserFollower_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_UserFollower_FollowId] FOREIGN KEY 
(
	[FollowId]
)REFERENCES [dbo].[User] (UserId)
)
GO

/****** Object:  Table [dbo].[Comment]    Script Date: 2022/05/30 21:11:36 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostReport](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[PostId] [int] NOT NULL,
 CONSTRAINT [PK_PostReport] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_PostReport_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
CONSTRAINT [FK_PostReportt_PostId] FOREIGN KEY 
(
	[PostId]
)REFERENCES [dbo].[Post] (PostId)
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Message](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[RecieverId] [int] NOT NULL,
	[Content] [nvarchar](250) NULL,
	[TimeDelivered] [datetime2] NOT NULL DEFAULT(getutcdate()) ,
	[Read] [bit] NOT NULL DEFAULT(0),
 CONSTRAINT [PK_Message] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Message_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId)
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Notification](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[UserId] [int] NOT NULL,
	[SenderId] [int] NOT NULL,
	[LastModified] [datetime2] NOT NULL,
	[IsDeleted] [bit] NOT NULL,
 CONSTRAINT [PK_Notification] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_Notification_UserId] FOREIGN KEY 
(
	[UserId]
)REFERENCES [dbo].[User] (UserId),
)
GO

SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PostTag](
	[Id] [int] IDENTITY(1,1) NOT NULL,
	[PostId] [int] NOT NULL,
	[TagId] [int] NOT NULL,
 CONSTRAINT [PK_PostTag] PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY],
CONSTRAINT [FK_PostTag_PostId] FOREIGN KEY 
(
	[PostId]
)REFERENCES [dbo].[Post] (PostId),
CONSTRAINT [FK_PostTag_TagId] FOREIGN KEY 
(
	[TagId]
)REFERENCES [dbo].[Tag] (TagId),
)
GO