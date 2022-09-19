
DROP TABLE IF EXISTS PostTag;
DROP TABLE IF EXISTS Notification;
DROP TABLE IF EXISTS Message;
DROP TABLE IF EXISTS PostReport;
DROP TABLE IF EXISTS UserCoin;
DROP TABLE IF EXISTS UserFollower;
DROP TABLE IF EXISTS CoinRating;
DROP TABLE IF EXISTS Likes;
DROP TABLE IF EXISTS Reply;
DROP TABLE IF EXISTS Comment;
DROP TABLE IF EXISTS Tag;
DROP TABLE IF EXISTS Post;
DROP TABLE IF EXISTS Coin;
DROP TABLE IF EXISTS Users;
DROP TABLE IF EXISTS Image;
DROP TABLE IF EXISTS Role;

CREATE TABLE Role(
	RoleId INT NOT NULL,
	Name VARCHAR(10) NOT NULL,
    CONSTRAINT PK_Role PRIMARY KEY(RoleId)
);

CREATE TABLE Image(
	ImageId SERIAL NOT NULL,
	Name VARCHAR(250) NOT NULL,
	Url VARCHAR(250) NOT NULL,
    CONSTRAINT PK_Image PRIMARY KEY(ImageId)
);

CREATE TABLE Coin(
	CoinId SERIAL NOT NULL,
	CoinName VARCHAR(50) NOT NULL,
	ImageId INT NULL,
	ImageUrl VARCHAR NULL,
    CONSTRAINT PK_Coin PRIMARY KEY(CoinId),
    CONSTRAINT FK_Coin_ImageId 
        FOREIGN KEY (ImageId)
        REFERENCES Image (ImageId)
);

CREATE TABLE Users(
	UserId SERIAL NOT NULL,
	Firstname VARCHAR(50) NOT NULL,
	Lastname VARCHAR(50) NOT NULL,
	Username VARCHAR(50) NOT NULL,
	Email VARCHAR(50) NOT NULL,
	Password VARCHAR(250) NOT NULL,
	OTP INT NULL,
	OTPExpirationTime TIMESTAMP NULL,
	HasForgottenPassword bit NULL,
	ImageId INT NULL,
	ImageUrl VARCHAR NULL,
	RoleId INT NOT NULL,
    CONSTRAINT PK_User PRIMARY KEY(UserId),
    CONSTRAINT FK_User_ImageId 
        FOREIGN KEY (ImageId)
        REFERENCES Image (ImageId),
    CONSTRAINT FK_User_RoleId 
        FOREIGN KEY (RoleId)
        REFERENCES Role (RoleId)
);

CREATE TABLE Post(
	PostId SERIAL NOT NULL,
	Content VARCHAR NOT NULL,
	UserId INT NOT NULL,
	ImageId INT NULL,
	ImageUrl VARCHAR NULL,
	SentimentScore decimal(4,4) CHECK (SentimentScore >= -1 AND SentimentScore <= 1) NULL,
	DateCreated TIMESTAMP NOT NULL DEFAULT(NOW() AT TIME ZONE 'UTC'),
    CONSTRAINT PK_Post PRIMARY KEY(PostId),
    CONSTRAINT FK_Post_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_Post_ImageId 
        FOREIGN KEY (ImageId)
        REFERENCES Image (ImageId)
);

CREATE TABLE Tag(
	TagId SERIAL NOT NULL,
	Content VARCHAR(50) NOT NULL,
    CONSTRAINT PK_Tag PRIMARY KEY(TagId)
);

CREATE TABLE Comment(
	CommentId SERIAL NOT NULL,
	UserId INT NOT NULL,
	PostId INT NOT NULL,
	Content VARCHAR(4000) NOT NULL,
    CONSTRAINT PK_Comment PRIMARY KEY(CommentId),
    CONSTRAINT FK_Comment_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_Comment_PostId 
        FOREIGN KEY (PostId)
        REFERENCES Post (PostId)
);


CREATE TABLE Reply(
	ReplyId SERIAL NOT NULL,
	UserId INT NOT NULL,
	CommentId INT NOT NULL,
	Content VARCHAR(4000) NOT NULL,
    CONSTRAINT PK_Reply PRIMARY KEY(ReplyId),
    CONSTRAINT FK_Reply_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_Reply_CommentId 
        FOREIGN KEY (CommentId)
        REFERENCES Comment (CommentId)
);

CREATE TABLE Likes(
	LikeId SERIAL NOT NULL,
	UserId INT NOT NULL,
	PostId INT NULL,
    CommentId INT NULL,
    ReplyId INT NULL,
    CONSTRAINT PK_Like PRIMARY KEY(LikeId),
    CONSTRAINT FK_Like_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_Like_PostId 
        FOREIGN KEY (PostId)
        REFERENCES Post (PostId),
    CONSTRAINT FK_Like_CommentId 
        FOREIGN KEY (CommentId)
        REFERENCES Comment (CommentId),
    CONSTRAINT FK_Like_ReplyId 
        FOREIGN KEY (ReplyId)
        REFERENCES Reply (ReplyId)
);

CREATE TABLE UserCoin(
	Id SERIAL NOT NULL,
	UserId INT NOT NULL,
	CoinId INT NOT NULL,
    CONSTRAINT PK_UserCoin PRIMARY KEY(Id),
    CONSTRAINT FK_UserCoin_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_UserCoin_CoinId 
        FOREIGN KEY (CoinId)
        REFERENCES Coin (CoinId)
);


CREATE TABLE CoinRating(
	Id SERIAL NOT NULL,
    CoinId INT NOT NULL,
	UserId INT NOT NULL,
    Rating INT CHECK (Rating >=1 AND Rating <= 5) NOT NULL,
    CONSTRAINT PK_CoinRating PRIMARY KEY(Id),
    CONSTRAINT FK_CoinRating_CoinId 
        FOREIGN KEY (CoinId)
        REFERENCES Coin (CoinId),
    CONSTRAINT FK_CoinRating_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId)
);


CREATE TABLE UserFollower(
	id SERIAL NOT NULL,
	UserId INT NOT NULL,
	FollowId INT NOT NULL,
	FollowDate TIMESTAMP NOT NULL,
    CONSTRAINT PK_UserFollower PRIMARY KEY(id),
    CONSTRAINT FK_UserFollower_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_UserFollower_FollowId 
        FOREIGN KEY (FollowId)
        REFERENCES Users (UserId)
);


CREATE TABLE PostReport(
	Id SERIAL NOT NULL,
	UserId INT NOT NULL,
	PostId INT NOT NULL,
    CONSTRAINT PK_PostReport PRIMARY KEY(Id),
    CONSTRAINT FK_PostReport_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId),
    CONSTRAINT FK_PostReportt_PostId 
        FOREIGN KEY (PostId)
        REFERENCES Post (PostId)
);


CREATE TABLE Message(
	Id SERIAL NOT NULL,
	UserId INT NOT NULL,
	RecieverId INT NOT NULL,
	Content VARCHAR(250) NULL,
	TimeDelivered TIMESTAMP NOT NULL DEFAULT(NOW() AT TIME ZONE 'UTC') ,
	Read BOOLEAN NOT NULL DEFAULT(FALSE),
    CONSTRAINT PK_Message PRIMARY KEY(Id),
    CONSTRAINT FK_Message_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId)
);


CREATE TABLE Notification(
	Id SERIAL NOT NULL,
	UserId INT NOT NULL,
	SenderId INT NOT NULL,
	LastModified TIMESTAMP NOT NULL,
	IsDeleted bit NOT NULL,
    CONSTRAINT PK_Notification PRIMARY KEY(Id),
    CONSTRAINT FK_Notification_UserId 
        FOREIGN KEY (UserId)
        REFERENCES Users (UserId)
);


CREATE TABLE PostTag(
	Id SERIAL NOT NULL,
	PostId INT NOT NULL,
	TagId INT NOT NULL,
    CONSTRAINT PK_PostTag PRIMARY KEY(Id),
    CONSTRAINT FK_PostTag_PostId 
        FOREIGN KEY (PostId)
        REFERENCES Post (PostId),
    CONSTRAINT FK_PostTag_TagId 
        FOREIGN KEY (TagId)
        REFERENCES Tag (TagId)
);
