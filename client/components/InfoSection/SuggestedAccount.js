import React, { useState, useContext, useEffect } from "react";
import { userContext } from "../../auth/auth";
import Link from "next/link";

const SuggestedAccount = ({ name, hidefollow, id }) => {
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

    fetch("http://localhost:8082/api/user/follow", options)
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
        <Link href={`/user/${id}`} className="cursor-pointer">
          <p className="text-sm font-semibold translate-y-1 ml-2 cursor-pointer">
            {thisUser.username}
          </p>
        </Link>
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
