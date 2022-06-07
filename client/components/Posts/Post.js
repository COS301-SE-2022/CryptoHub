import React, { useState, useEffect } from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";
import Link from "next/link";

const Post = ({ name, content, userId, postId }) => {
  const [user, setUser] = useState({});
  const [likes, setLikes] = useState(0); 
  const [comments, setComments] = useState(0); 

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/GetUserById/${userId}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
      })
      .catch((error) => {});
  };

  const handleLikePost = () => {
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        likeId: 0,
        userId: 0,
        postId: 0,
        commentId: 0,
        replyId: 0,
      }),
    };
  };

  useEffect(() => {
    handleGetUser();
  }, []);

  return (
    <div className="bg-white m-4 p-4 rounded-lg">
      <div className="flex flew-row items-center mb-2">
        <div className="w-8 h-8 bg-black rounded-3xl"></div>
        <Link href={`/user/${userId}`} className="pointer cursor-pointer">
          <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
            {user.username}
          </p>
        </Link>
      </div>
      <p className="text-sm">{content}</p>
      <div className="flex flex-row mt-4">
        <button className="text-sm mr-4 flex flex-row">
          <HeartIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{likes} likes</p>
        </button>
        <button className="text-sm flex flex-row">
          <ChatIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{comments} comments</p>
        </button>
      </div>
    </div>
  );
};

export default Post;
