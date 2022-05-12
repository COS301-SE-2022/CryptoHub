INSERT INTO users (first_name, last_name, email, password, username)
VALUES ('John', 'Doe', 'JohnDoe@gmail.com', '123', 'John Doe'),
       ('Peter', 'Parker', 'PeterParker@gmail.com', '123four', 'Peter Parker'),
       ('Jenessa', 'Cheyanne', 'JenessaCheyanne@gmail.com', 'mypassword', 'Jenessa Cheyanne'),
       ('Tony', 'Stark', 'TonyStark@gmail.com', 'avenger', 'Ironman'),
       ('Stephen', 'Strange', 'StephenStrange@gmail.com', 'doctor', 'Stephen Strange'),
       ('Midge', 'Abigail', 'MidgeAbigail@gmail.com', 'Password', 'Midge Abigail');

INSERT INTO posts (user_id, post)
VALUES (1, 'Bitcoin Drops to 2020 Levels of Near $24K'),
       (3, 'Can crypto go green? Major companies are trying — but it’s easier said than done'),
       (1, 'More than $200bn wiped off cryptocurrency market in a day'),
       (4, 'Fort Worth pilots bitcoin mine and dogecoin whipsaws after Elon Musk buys Twitter'),
       (2, 'SEC boosts crypto unit, Ethereum gas fees soar, and Congress’ stance on crypto'),
       (5, 'Blockchain Brawlers earns $357 million in one week, Ethereum merge delayed'),
       (3, 'Andrew Yang explains how crypto and a universal basic income could intersect'),
       (4, 'Fort Worth pilots bitcoin mine and dogecoin whipsaws after Elon Musk buys Twitter'),
       (5, 'Miami wants to become a crypto hub and Coinbase launches an NFT marketplace'),
       (6, 'Bitcoin investors are panicking as a controversial crypto experiment unravels'),
       (4, 'U.S. Treasury sanctions crypto miner BitRiver and Binance limits Russian accounts');

-- INSERT INTO users_following (user_userid, following_userid)
-- VALUES (1, 2),
--        (1, 3),
--        (1, 4),
--        (1, 5),
--        (2, 1),
--        (3, 1);

INSERT INTO users_followers (user_userid, followers_userid)
VALUES (1, 4),
       (2, 1),
       (4, 1),
       (5, 1),
       (1, 2),
       (1, 3),
       (2, 3);