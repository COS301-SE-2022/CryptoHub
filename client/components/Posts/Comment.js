import React, { useState, useContext, useEffect } from "react";
import { userContext } from "../../auth/auth";
import Link from "next/link";

const Comment = ({ name, hidefollow, id }) => {
  const { user } = useContext(userContext);
  const [clicked, setClicked] = useState(false);
  const [thisUser, setThisUser] = useState({});

  const handleFollowUser = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(
      `http://localhost:7215/api/UserFollower/FollowUser/${id}/${user.id}`,
      options
    )
      .then((response) => {
        setClicked(true);
        response.json();
      })
      .then((data) => {
        setClicked(true);
      })
      .catch(() => {});
  };

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setThisUser(data);
      })
      .catch((error) => {});
  };

  const handleGetComments = () => {
    const options = {
      method: "GET",
    };

    fetch(
      `http://localhost:7215/api/Comment/GetCommentsByPostId/${id}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setThisUser(data);
      })
      .catch((error) => {});
  };

  useEffect(() => {
    // handleGetUser();
  }, []);

  return (
    <div className="flex flex-row p-2 w-full justify-between mb-2 rounded-md">
      <div className="flex flex-row">
        <div className="w-6 h-6 bg-black rounded-3xl translate-y-1"></div>
        <Link href={`/user/${id}`} className="cursor-pointer">
          <p className="text-sm font-semibold translate-y-1 ml-2 cursor-pointer">
            {/* {thisUser.username} */}
            username
          </p>
        </Link>
        <p className="text-sm text-gray-600 translate-y-1 ml-2">
          Thank you for the info.
        </p>
      </div>
    </div>
  );
};

export default Comment;
