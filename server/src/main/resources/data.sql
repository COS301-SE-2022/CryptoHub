INSERT INTO users (userid,first_name, last_name,email,password,username) VALUES
    (1,'bruce','wayne','batman@gmail.com','P@55w0rd','batman'),
    (2,'peter','parker','spiderman@gmail.com','123four','spiderman'),
    (3,'clark','kent','superman@gmail.com','mypassword','superman'),
    (4,'tony','stark','ironman@gmail.com','avenger','ironman'),
    (5,'stephen','strange','drstrange@gmail.com','doctor','drstrange'),
    (6,'loki','odinson','mischief@gmail.com','evil','mischief');

INSERT INTO posts (postid, user_id,post )  VALUES
    (1,1,'I like this coin'),
    (2,1,'Doggy Coin is the future'),
    (3,1,'Bitcoin is plummeting'),
    (5,1,'The is a reflection of the world state'),
    (6,2,'we are in need of cryptos more than ever'),
    (7,2,'How do i make money'),
    (8,3,'Awesome site')