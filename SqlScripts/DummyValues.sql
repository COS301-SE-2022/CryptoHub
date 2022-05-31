USE [CryptoHubDB]
GO

INSERT INTO [dbo].[User] (Firstname,Lastname,Email,[Password],Username)
VALUES 
('John','Doe','JohnDoe@gmail.com','123four','john'),
('Elon','Musk','elonMusk@gmail.com','1234','elon'),
('Jack','Daniels','JD@gmail.com','password','JD');

INSERT INTO [dbo].[Post] (UserId,Post)
VALUES 
(1,'This Coin is Cool'),
(2,'Lets make Money'),
(3,'Cryptohub is awesome'),
(1,'Bitcoin is the furure'),
(2,'DogeCoin go'),
(3,'Too the moon');

INSERT INTO [dbo].[Like] ([PostId],[UserId])
VALUES 
(1,1),
(1,2),
(1,3),
(2,2),
(3,1),
(4,2),
(5,1);

INSERT INTO [dbo].[Comment]([UserId],[PostId],[Comment])
VALUES
(1,1,'I wantto buy even more'),
(2,1,'yeah its over for this'),
(3,1,'i agree honestly'),
(1,3,'this site is the future');

INSERT INTO [dbo].[UserFollower]([UserId],[FollowId],[FollowDate])
VALUES
(1,2,'2022-05-30'),
(1,3,'2022-05-30'),
(3,1,'2022-05-30'),
(2,3,'2022-05-30');

INSERT INTO [dbo].[Role]([RoleId],[Role])
VALUES
(1,'Super'),
(2,'Admin'),
(3,'User');

