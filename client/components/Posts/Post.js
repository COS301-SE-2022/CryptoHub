import React, { useState, useEffect, useContext, Fragment } from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";
import { XIcon } from "@heroicons/react/outline";
import Link from "next/link";
import Comment from "./Comment";
import Image from "next/image";
import { userContext } from "../../auth/auth";
import { HeartIcon as RedHeartIcon } from "@heroicons/react/solid";
import { Menu, Transition } from "@headlessui/react";

function classNames(...classes) {
  return classes.filter(Boolean).join(" ");
}

const Post = ({ name, content, userId, postId, imageId, admin, reports }) => {
  const [thisUser, setUser] = useState({});
  const [likes, setLikes] = useState(0);
  const [comments, setComments] = useState([]);
  const [commentCount, setCommentCount] = useState(0);
  const [liked, setLiked] = useState(false);
  const [showModal, setShowModal] = useState(false);
  const [postImage, setPostImage] = useState(null);
  const [comment, setComment] = useState("");
  const [likeId, setLikeId] = useState(null);
  const { user, refreshfeed, alert } = useContext(userContext);

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

  const handleGetPostImage = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/Image/GetById/${imageId}`, options)
      .then((response) => response.json())
      .then((data) => {
        // let image = `data:image/jpeg;base64,${data.blob}`;
        // let image = ;
        setPostImage(data.url);
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
        userId: user.id,
        postId: postId,
      }),
    };
    fetch("http://localhost:7215/api/Like/AddLike", options)
      .then((response) => response.json())
      .then((data) => {
        setLiked(true);
        getLikeCount();
        setLikeId(data.likeId);
      })
      .catch((error) => console.log("Add like error: ", error));
  };

  const checkIfLiked = () => {
    const options = {
      method: "GET",
    };

    fetch(
      `http://localhost:7215/api/Like/GetLikeBy/${user.id}/${postId}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        if (data.userId == user.id) {
          setLiked(true);
        }
      })
      .catch((error) => console.warn("Error: ", error));
  };

  const handleUnlikePost = () => {
    const options = {
      method: "DELETE",
    };
    fetch(
      `http://localhost:7215/api/Like/Delete/${user.id}/${postId}`,
      options
    ).then((response) => {
      if (response.status === 200) {
        setLiked(false);
        getLikeCount();
      }
    });
  };

  const handleAddComment = (e) => {
    e.preventDefault();
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        commentId: 0,
        userId: user.id,
        postId: postId,
        content: comment,
      }),
    };
    fetch("http://localhost:7215/api/Comment/AddComment", options)
      .then((response) => response.json())
      .then((data) => {
        handleGetComments();
      });
  };

  const handleGetComments = () => {
    const options = {
      method: "GET",
    };

    fetch(
      `http://localhost:7215/api/Comment/GetCommentByPostId/${postId}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        setComments(data);
      })
      .catch((error) => {});
  };

  const handleReportPost = () => {
    const options = {
      method: "POST",
      headers: {
        "Content-Type": "application/json",
      },
      body: JSON.stringify({
        userId: user.id,
        postId: postId,
      }),
    };
    fetch(
      `http://localhost:7215/api/Post/Report?postid=${postId}&userid=${user.id}`,
      options
    )
      .then((response) => response.json())
      .then((data) => {
        refreshfeed();
      });
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

  const handleDeletePost = () => {
    const options = {
      method: "DELETE",
    };

    fetch(`http://localhost:7215/api/Post/Delete?id=${postId}`, options).then(
      (response) => {
        if (response.status == 200) {
          refreshfeed();
        }
      }
    );
  };

  useEffect(() => {
    handleGetUser();
    getLikeCount();
    if (imageId != null) {
      handleGetPostImage();
    }
    handleGetComments();
    checkIfLiked();
  }, []);
  {
    /* <Image src={postImage} height="200" width="200" /> */
  }

  const showToast = () => {};

  return (
    <div className="bg-white m-4 p-4 rounded-lg">
      <div className="flex flew-row items-center mb-2 justify-between">
        <div className="flex flex-row">
          <span className="inline-block h-8 w-8 rounded-full overflow-hidden bg-gray-100">
            <svg
              className="h-full w-full text-gray-300"
              fill="currentColor"
              viewBox="0 0 24 24"
            >
              <path d="M24 20.993V24H0v-2.996A14.977 14.977 0 0112.004 15c4.904 0 9.26 2.354 11.996 5.993zM16.002 8.999a4 4 0 11-8 0 4 4 0 018 0z" />
            </svg>
          </span>

          {user.id == thisUser.userId ? (
            <div class="flex justify-between flex-container">
              <div className="flex flex-row items-center ">
                <Link href={`/profile`} className="pointer cursor-pointer">
                  <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                    {thisUser.username}
                  </p>
                </Link>
                {admin && (
                  <p className="text-sm font-semibold mb-2 translate-y-1 ml-5 text-red-600 cursor-pointer">
                    Reports: {reports}
                  </p>
                )}
              </div>
            </div>
          ) : (
            <div class="flex flex-container">
              <div className="flex flex-row items-center ">
                <Link
                  href={`/user/${userId}`}
                  className="pointer cursor-pointer"
                >
                  <p className="text-sm font-semibold mb-2 translate-y-1 ml-2 cursor-pointer">
                    {thisUser.username}
                  </p>
                </Link>
                {admin && (
                  <p className="text-sm font-semibold mb-2 translate-y-1 ml-5 text-red-600 cursor-pointer">
                    Reports: {reports}
                  </p>
                )}
              </div>

              {/* <div>
              <button
                onClick={() => setShowModal(true)}
                className="text-sm flex flex-row"
              >
                <p className="ml-1"> ... </p>
              </button>
            </div> */}
            </div>
          )}
        </div>
        <>
          <div>
            <div className="translate-x-50 text-right">
              <button className="text-sm flex flex-row">
                <Menu as="div" className="ml-1 sm:ml-3 relative">
                  <div>
                    <Menu.Button>
                      <span className="sr-only">Open user menu</span>
                      <div className="-translate-y-4">...</div>
                    </Menu.Button>
                  </div>
                  <Transition
                    as={Fragment}
                    enter="transition ease-out duration-100"
                    enterFrom="transform opacity-0 scale-95"
                    enterTo="transform opacity-100 scale-100"
                    leave="transition ease-in duration-75"
                    leaveFrom="transform opacity-100 scale-100"
                    leaveTo="transform opacity-0 scale-95"
                  >
                    <Menu.Items className="origin-top-right absolute right-0 mt-2 w-40 rounded-md shadow-lg py-1 bg-white ring-1 ring-black ring-opacity-5 focus:outline-none">
                      {!admin && (
                        <Menu.Item>
                          {({ active }) => (
                            <>
                              <button
                                onClick={() => {
                                  handleReportPost();
                                }}
                                className={classNames(
                                  active ? "bg-gray-100" : "",
                                  "block px-2 py-2 text-sm text-gray-700 w-full"
                                )}
                              >
                                Report
                              </button>
                            </>
                          )}
                        </Menu.Item>
                      )}
                      {admin && (
                        <Menu.Item>
                          {({ active }) => (
                            <>
                              <button
                                onClick={() => {
                                  handleDeletePost();
                                }}
                                className={classNames(
                                  active ? "bg-gray-100" : "",
                                  "block px-2 py-2 text-sm text-red-700 font-semibold w-full"
                                )}
                              >
                                Delete Post
                              </button>
                            </>
                          )}
                        </Menu.Item>
                      )}
                    </Menu.Items>
                  </Transition>
                </Menu>
              </button>
            </div>
          </div>
        </>
      </div>

      {postImage == null ? null : (
        <div
          style={{
            width: "100%",
            height: "430px",
            position: "relative",
          }}
        >
          <Image src={postImage} layout="fill" />
        </div>
      )}
      <p className="text-sm">{content}</p>
      <div className="flex flex-row mt-4">
        <button
          onClick={liked ? handleUnlikePost : handleLikePost}
          className="text-sm mr-4 flex flex-row"
        >
          {liked ? (
            <RedHeartIcon className="h-5 w-5 text-red-500" />
          ) : (
            <HeartIcon className="h-5 w-5 text-black" />
          )}{" "}
          {""}
          <p className="ml-1">{likes} likes</p>
        </button>
        <button
          onClick={() => setShowModal(true)}
          className="text-sm flex flex-row"
        >
          <ChatIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{comments.length} comments</p>
        </button>

        {showModal ? (
          <>
            <div className="justify-center items-start mt-16 flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
              <div className="relative w-10/12 sm:w-10/12 my-6 mx-auto max-w-3xl">
                <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                  <div className="relative flex-auto">
                    <form method="POST" onSubmit={handleAddComment}>
                      {user.auth == true ? (
                        <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                          <input
                            autoFocus
                            className="h-9 text-sm border rounded-md w-full px-2 py-1 mr-1 focus:outline-none focus:ring-indigo-500 focus:border-indigo-500"
                            type="text"
                            placeholder="Type comment here"
                            defaultValue={""}
                            onChange={(e) => setComment(e.target.value)}
                          />
                          <button
                            className="h-9 text-sm text-semibold mx-1 sm:mx-3 justify-center flex w-40 border px-1 p-2 rounded-md bg-indigo-600 text-white"
                            type="submit"
                          >
                            Comment
                          </button>
                          <button
                            className="px-1 p-1"
                            type="button"
                            onClick={() => setShowModal(false)}
                          >
                            <XIcon className="h-6 w-6" aria-hidden="true" />
                          </button>
                        </div>
                      ) : (
                        <div>
                          <div className="w-full p-4 flex justify-between">
                            <div className="inline-flex rounded-md shadow">
                              <a
                                href="/signup"
                                className="inline-flex items-center justify-center px-4 py-2 border border-transparent text-base font-medium rounded-md text-white bg-indigo-600 hover:bg-indigo-700"
                              >
                                Sign up now to interact
                              </a>
                            </div>
                            <button
                              className="px-1 p-1"
                              type="button"
                              onClick={() => setShowModal(false)}
                            >
                              <XIcon className="h-6 w-6" aria-hidden="true" />
                            </button>
                          </div>
                        </div>
                      )}
                      <div className="flex flex-col p-5">
                        <div>
                          {/* {comments.length == 0 ? (
                            <p className="text-sm text-gray-400">
                              This post has no comments
                            </p>
                          ) : null} */}
                          <p className="text-semibold text-gray-600 px-1">
                            Comments
                          </p>
                          {console.warn("COMMMENTS: ", comments)}
                          {comments.map((data, index) => {
                            return (
                              <Comment
                                key={index}
                                userId={data.userId}
                                comment={data.content}
                              />
                            );
                          })}
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
