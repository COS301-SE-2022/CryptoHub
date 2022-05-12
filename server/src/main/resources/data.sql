INSERT INTO users (first_name, last_name, email, password, username)
VALUES ('John', 'Doe', 'JohnDoe@gmail.com', '123', 'John Doe'),
       ('Peter', 'Parker', 'PeterParker@gmail.com', '123four', 'Peter Parker'),
       ('Jenessa', 'Cheyanne', 'JenessaCheyanne@gmail.com', 'mypassword', 'Jenessa Cheyanne'),
       ('Tony', 'Stark', 'TonyStark@gmail.com', 'avenger', 'Ironman'),
       ('Stephen', 'Strange', 'StephenStrange@gmail.com', 'doctor', 'Stephen Strange'),
       ('Midge', 'Abigail', 'MidgeAbigail@gmail.com', 'Password', 'Midge Abigail');

INSERT INTO posts (user_id, post)
VALUES (1, 'I like this coin'),
       (1, 'Doggy Coin is the future'),
       (1, 'Bitcoin is plummeting'),
       (1, 'The is a reflection of the world state'),
       (2, 'we are in need of cryptos more than ever'),
       (2, 'How do i make money'),
       (3, 'Awesome site');

-- INSERT INTO users_following (user_userid, following_userid)
-- VALUES (1, 2),
--        (1, 3),
--        (1, 4),
--        (1, 5),
--        (2, 1),
--        (3, 1);

INSERT INTO users_followers (user_userid, followers_userid)
VALUES (2, 1),
       (3, 1),
       (4, 1),
       (5, 1),
       (1, 2),
       (1, 3);
