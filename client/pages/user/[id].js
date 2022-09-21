import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import Post from "../../components/Posts/Post";
import Image from "next/image";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../../components//InfoSection/SuggestedAccount";

const User = () => {
  const router = useRouter();
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [posts, setPosts] = useState([]);
  const [clicked, setClicked] = useState(false);
  const [isFollowing, setIsFollowing] = useState(false);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const [followers, setFollowers] = useState([]);
  const [showFollowingModal, setFollowingShowModal] = useState(false);
  const [following, setFollowing] = useState([]);
  const { user, url } = useContext(userContext);
  const [profilePicture, setProfilePicture] = useState(null);
  const [showModal, setShowModal] = useState(false);

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        //console.log("userID:", user);
        setUser(data);
        setProfilePicture(data.imageUrl);
      })
      .catch((error) => {});
  };

  const checkIfSameUser = () => {
    return user.id == id;
  };

  const handleGetAllPosts = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Post/GetAllPosts`, options)
      .then((response) => response.json())
      .then((data) => {
        let posts = data.reverse();
        let myPosts = posts.filter((post) => {
          return post.userId == id;
        });
        setPosts(myPosts);
      })
      .catch(() => {});
  };

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
        setIsFollowing(true);
      })
      .catch(() => {});
  };

  const checkFollowing = () => {
    fetch(`${url}/api/UserFollower/GetUserFollowing/${id}`)
      .then((response) => response.json())
      .then((data) => {
        console.log(data);
        data.map((d) => {
          if (d.userId == id) {
            setIsFollowing(true);
          }
        });
      });
  };

  const handleViewFollowers = (UsersID) => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/UserFollower/GetUserFollower/${UsersID}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowers(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  const handleViewFollowing = (UsersID) => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/UserFollower/GetUserFollowing/${UsersID}`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        setFollowing(data);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  const handleGetUserByID = () => {
    const options = {
      method: "GET",
    };

    fetch(`${url}/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        console.log("data");
        handleViewFollowers(data.userId);
        handleViewFollowing(data.userId);
      })
      .catch((error) => {});
  };

  useEffect(() => {
    handleGetUserByID();
    handleGetUser();

    handleGetAllPosts();
    checkFollowing();
    id == undefined && router.push("/");
  }, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-7/12 items-center mt-8">
          {profilePicture == null ? (
            <span className="inline-block h-40 w- m-4 rounded-full overflow-hidden bg-gray-100">
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
              className="rounded-full overflow-hidden m-4"
              style={{
                width: "170px",
                height: "170px",
                position: "relative",
              }}
            >
              <Image src={profilePicture} layout="fill" />
            </div>
          )}
          <div className="flex flex-row">
            <p className="font-semibold text-center sm:text-left">
              {thisUser.username}
            </p>{" "}
            <br />
          </div>
          {user.auth ? (
            isFollowing ? (
              <>
                <p className="text-sm ml-5 text-black bg-gray-400 rounded-md px-3 py-1">
                  Following
                </p>
                <button
                  onClick={() => {
                    router.push(`/messages/${id}`);
                  }}
                >
                  <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                    Message
                  </p>
                </button>
              </>
            ) : (
              <>
                <button onClick={handleFollowUser}>
                  <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                    Follow
                  </p>
                </button>
                <button
                  onClick={() => {
                    router.push(`/messages/${id}`);
                  }}
                >
                  <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                    Message
                  </p>
                </button>
              </>
            )
          ) : null}
        </div>

        <div className="flex flex-row -translate-y-16">
          <button className="mr-3" onClick={() => setFollowingShowModal(true)}>
            <span className="font-semibold">{`${following.length} `}</span>{" "}
            following
          </button>
          <button onClick={() => setShowModal(true)}>
            {" "}
            <span className="font-semibold" f>
              {`${followers.length} `}
            </span>
            followers
          </button>
        </div>
        <div className="bg-gray-400 sm:w-7/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-5/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Posts</p>
          </div>
          <div className="w-full">
            {posts.map((data, index) => {
              return (
                <Post
                  key={index}
                  name={data.username}
                  content={data.content}
                  userId={data.userId}
                  imageId={data.imageUrl}
                  postId={data.postId}
                />
              );
            })}
            {showModal ? (
              <>
                <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                  <div className="relative w-11/12 sm:w-6/12 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                      <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                        <h2>Followers</h2>
                        <button
                          className="px-1 p-1"
                          type="button"
                          onClick={() => setShowModal(false)}
                        >
                          <XIcon className="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                      <div className="relative flex-auto">
                        <form method="POST">
                          <div className="px-4 py-5 bg-white space-y-6 sm:p-6">
                            <div>
                              <div className="mt-1">
                                {followers.map((data, index) => {
                                  return (
                                    <SuggestedAccount
                                      key={index}
                                      name={data.username}
                                      hidefollow={true}
                                      id={data.userId}
                                      suggestions={true}
                                    />
                                  );
                                })}
                              </div>
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
            {showFollowingModal ? (
              <>
                <div className="justify-center items-center flex overflow-x-hidden overflow-y-auto fixed inset-0 z-50 outline-none focus:outline-none">
                  <div className="relative w-11/12 sm:w-6/12 my-6 mx-auto max-w-3xl">
                    <div className="border-0 rounded-lg shadow-sm relative flex flex-col w-full bg-white outline-none focus:outline-none">
                      <div className="flex items-start justify-between p-5 border-solid border-slate-200 rounded-t">
                        <h2>Following</h2>
                        <button
                          className="px-1 p-1"
                          type="button"
                          onClick={() => setFollowingShowModal(false)}
                        >
                          <XIcon className="h-6 w-6" aria-hidden="true" />
                        </button>
                      </div>
                      <div className="relative flex-auto">
                        <form method="POST">
                          <div className="px-4 py-5 bg-white space-y-6 sm:p-6">
                            <div>
                              <div className="mt-1">
                                {following.map((data, index) => {
                                  return (
                                    <SuggestedAccount
                                      key={index}
                                      name={data.username}
                                      hidefollow={true}
                                      id={data.userId}
                                      suggestions={true}
                                    />
                                  );
                                })}
                              </div>
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
      </Layout>
    </>
  );
};

export default User;
