import React, { useState, useEffect } from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";
import { XIcon } from "@heroicons/react/outline";
import Link from "next/link";
import Comment from "./Comment";

const Post = ({ name, content, userId, postId }) => {
  const [user, setUser] = useState({});
  const [likes, setLikes] = useState(0);
  const [comments, setComments] = useState([]);
  const [commentCount, setCommentCount] = useState(0);
  const [liked, setLiked] = useState(false);
  const [showModal, setShowModal] = useState(false);

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
        userId: userId,
        postId: postId,
      }),
    };
    fetch("http://localhost:7215/api/Like/AddLike", options)
      .then((response) => response.json())
      .then((data) => {
        setLiked(true);
      });
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

  const getLikeCount = () => {
    const options = {
      method: "GET",
    };
    fetch(
      `http://localhost:7215/api/Like/GetLikeCountByPostId/${postId}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setLikes(data.count);
      });
  };

  useEffect(() => {
    handleGetUser();
    getLikeCount();
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
        <button onClick={handleLikePost} className="text-sm mr-4 flex flex-row">
          {liked ? (
            <HeartIcon className="h-5 w-5 text-red-500 " />
          ) : (
            <HeartIcon className="h-5 w-5 text-black " />
          )}{" "}
          {""}
          <p className="ml-1">{likes} likes</p>
        </button>
        <button
          onClick={() => setShowModal(true)}
          className="text-sm flex flex-row"
        >
          <ChatIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{commentCount} comments</p>
        </button>
        {showModal ? (
          <>
            <div className="justify-center items-start mt-16 flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
              <div className="relative w-10/12 sm:w-10/12 my-6 mx-auto max-w-3xl">
                <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                  <div className="relative flex-auto">
                    <form method="POST">
                      <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                        <input
                          autoFocus
                          className="h-9 text-sm border rounded-md w-full px-2 py-1 mr-1 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                          type="text"
                          placeholder="Type comment here"
                          // value={}
                          // onChange={}
                        />
                        <button
                          className="h-9 text-sm text-semibold mx-1 sm:mx-3 justify-center flex w-40 border px-1 p-2 rounded-md bg-indigo-600 text-white"
                          type="button"
                        >
                          Comments
                        </button>
                        <button
                          className="px-1 p-1"
                          type="button"
                          onClick={() => setShowModal(false)}
                        >
                          <XIcon className="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                      <div className="flex flex-col p-5">
                        <div>
                          {/* {comments.length == 0 ? (
                            <p className="text-sm text-gray-400">
                              This post has no comments
                            </p>
                          ) : null} */}
                          <Comment />
                        </div>
                      </div>
                    </form>
                  </div>
                  <div className="flex items-center justify-end p-6 border-solid border-slate-200 rounded-b"></div>
                </div>
              </div>
            </div>
            <div className="opacity-25 fixed inset-0 z-40 bg-black"></div>
          </>
        ) : null}
      </div>
    </div>
  );
};

export default Post;
