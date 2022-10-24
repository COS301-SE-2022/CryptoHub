CREATE OR REPLACE FUNCTION SUGGESTEDUSER(f_userid int)
returns setof users
Language sql
as
$$
	SELECT --notfollowed.userid--, mutal.followid --users.userid , COUNT(users.userid)
	users.userid,users.firstname,users.lastname,users.username,
	users.password,users.email,users.otp,users.otpexpirationtime,
	users.hasforgottenpassword,users.imageid,users.imageurl,users.roleid
	FROM
	(
		SELECT userid,followid FROM userfollower
		WHERE userid in 
		(
		SELECT userid FROM userfollower
		where userfollower.followid = f_userid
		)
	) as followsme --get users who follow me
	JOIN
	(
		SELECT userid,followid FROM userfollower
		where userfollower.userid = f_userid
	) as mutal
	ON mutal.followid = followsme.followid --get mutal followers
	RIGHT JOIN 
	(
		SELECT userid FROM users
		WHERE userid NOT IN 
		(
			SELECT userid FROM userfollower
			where userfollower.followid = f_userid
		)
		AND userid != f_userid
		ORDER BY userid ASC
	) as notfollowed --get users i do not follow
	ON notfollowed.userid = mutal.followid
	RIGHT JOIN users
	ON notfollowed.userid = users.userid
	WHERE notfollowed.userid IS NOT NULL
	ORDER BY users.userid ASC
	LIMIT 5	
$$