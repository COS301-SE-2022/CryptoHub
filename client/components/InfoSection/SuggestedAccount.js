import React, { useState, useContext, useEffect } from "react";
import { userContext } from "../../auth/auth";
import Link from "next/link";
import Image from "next/image";

const SuggestedAccount = ({
  name,
  hidefollow,
  id,
  firstname,
  lastname,
  username,
  suggestions,
}) => {
  const { user, url } = useContext(userContext);
  const [clicked, setClicked] = useState(false);
  const [thisUser, setThisUser] = useState({});
  const [profilePicture, setProfilePicture] = useState(null);

  const handleFollowUser = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(`${url}/api/UserFollower/FollowUser/${id}/${user.id}`, options)
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

    fetch(`${url}/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setThisUser(data);
        setProfilePicture(data.imageUrl);
      })
      .catch((error) => {});
  };

  useEffect(() => {
    handleGetUser();
  }, []);

  return (
    <div className="flex flex-row p-2 w-full justify-between bg-gray-100 mb-2 rounded-md">
      <div className="flex flex-row">
        {profilePicture == null ? (
          <span className="inline-block h-8 w-8 rounded-full overflow-hidden bg-gray-100">
            <svg
              className="h-full w-full text-gray-300"
              fill="currentColor"
              viewBox="0 0 24 24"
            >
              <path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
            </svg>
          </span>
        ) : (
          <div
            className="rounded-full overflow-hidden"
            style={{
              width: "32px",
              height: "32px",
              position: "relative",
            }}
          >
            <Image src={profilePicture} layout="fill" />
          </div>
        )}
        {user.id == thisUser.userId ? (
          <div className="flex flex-row">
            <Link href={`/profile`} className="pointer cursor-pointer">
              <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                {suggestions ? thisUser.username : username}
              </p>
            </Link>
            <p className="text-xs translate-y-1.5 ml-2 text-gray-600">
              {firstname}
            </p>
            <p className="text-xs translate-y-1.5 ml-0.5 text-gray-600">
              {lastname}
            </p>
          </div>
        ) : (
          <div className="flex flex-row">
            <Link href={`/user/${id}`} className="pointer cursor-pointer">
              <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                {suggestions ? thisUser.username : username}
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
