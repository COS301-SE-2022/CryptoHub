INSERT INTO users (first_name, last_name, email, password, username)
VALUES ('bruce', 'wayne', 'batman@gmail.com', 'P@55w0rd', 'batman'),
       ('peter', 'parker', 'spiderman@gmail.com', '123four', 'spiderman'),
       ('clark', 'kent', 'superman@gmail.com', 'mypassword', 'superman'),
       ('tony', 'stark', 'ironman@gmail.com', 'avenger', 'ironman'),
       ('stephen', 'strange', 'drstrange@gmail.com', 'doctor', 'drstrange'),
       ('loki', 'odinson', 'mischief@gmail.com', 'evil', 'mischief');

INSERT INTO posts (user_id, post)
VALUES (1, 'I like this coin'),
       (1, 'Doggy Coin is the future'),
       (1, 'Bitcoin is plummeting'),
       (1, 'The is a reflection of the world state'),
       (2, 'we are in need of cryptos more than ever'),
       (2, 'How do i make money'),
       (3, 'Awesome site');

INSERT INTO users_following (user_userid, following_userid)
VALUES (1, 2),
       (1, 3),
       (1, 4),
       (1, 5),
       (2, 1),
       (3, 1);

INSERT INTO users_followers (user_userid, followers_userid)
VALUES (2, 1),
       (3, 1),
       (4, 1),
       (5, 1),
       (1, 2),
       (1, 3);
