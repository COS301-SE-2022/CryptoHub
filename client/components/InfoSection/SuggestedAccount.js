import React, { useState, useContext, useEffect } from "react";
import { userContext } from "../../auth/auth";
import Link from "next/link";

const SuggestedAccount = ({ name, hidefollow, id, firstname, lastname }) => {
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

  useEffect(() => {
    handleGetUser();
  }, []);

  return (
    <div className="flex flex-row p-2 w-full justify-between bg-gray-100 mb-2 rounded-md">
      <div className="flex flex-row">
        <div className="w-6 h-6 bg-black rounded-3xl"></div>
        {user.id == thisUser.userId ? (
          <div>
            <Link href={`/profile`} className="pointer cursor-pointer">
              <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                {thisUser.username}
              </p>
            </Link>
            <p>{firstname}</p>
          </div>
        ) : (
          <div className="flex flex-row">
            <Link href={`/user/${id}`} className="pointer cursor-pointer">
              <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                {thisUser.username}
              </p>
            </Link>
            <p className="text-xs translate-y-1.5 ml-2 text-gray-600">
              {firstname}
            </p>
            <p className="text-xs translate-y-1.5 ml-0.5 text-gray-600">
              {lastname}
            </p>
          </div>
        )}
      </div>
      {hidefollow ? null : (
        <button onClick={handleFollowUser}>
          {clicked ? (
            <p className="text-sm font-bold text-gray-400 mr-2">Following</p>
          ) : (
            <p className="text-sm font-bold text-indigo-600 mr-2">Follow</p>
          )}
        </button>
      )}
    </div>
  );
};

export default SuggestedAccount;
