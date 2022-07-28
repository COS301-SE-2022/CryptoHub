import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";
import Image from "next/image";

const Profile = () => {
  const { user, profilePicture } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const router = useRouter();
  const [followers, setFollowers] = useState([]);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showFollowingModal, setFollowingShowModal] = useState(false);
  const [, setProfilePicture] = useState(null);

  const handleViewFollowing = () => {
    const options = {
      method: "GET",
    };

    fetch(
      `http://localhost:7215/api/UserFollower/GetUserFollowing/${user.id}`,
      options
    )
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

  const handleViewFollowers = () => {
    const options = {
      method: "GET",
    };

    fetch(
      `http://localhost:7215/api/UserFollower/GetUserFollower/${user.id}`,
      options
    )
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

  const handleGetAllPosts = () => {
    setLoading(true);

    const options = {
      method: "GET",
    };

    fetch("http://localhost:7215/api/Post/GetAllPosts", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        let posts = data.reverse();
        let myPosts = posts.filter((post) => {
          return post.userId == user.id;
        });
        setPosts(myPosts);
      })
      .catch((error) => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    if (!user.auth) {
      router.push("/");
    }
  }, [user]);

  useEffect(() => {
    handleGetAllPosts();
  }, []);

  useEffect(() => {
    handleViewFollowers();
    handleViewFollowing();
  }, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-7/12 items-center mt-8">
          {profilePicture == null ? (
            <span className="inline-block h-40 w-40 m-4 rounded-full overflow-hidden bg-gray-100">
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
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left">
              {user.username}
            </p>{" "}
            <br />
            <div className="flex flex-row -translate-y-5">
              <button
                className="mr-3"
                onClick={() => setFollowingShowModal(true)}
              >
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
            <button
              onClick={() => {
                router.push("/editprofile");
              }}
              className="sm:self-start text-sm font-semibold bg-gray-300 px-3 py-1 rounded-md hover:bg-gray-400 transition"
            >
              edit profile
            </button>
          </div>
        </div>
        <div className="bg-gray-400 sm:w-7/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-4/12">
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
                  imageId={data.imageId}
                  postId={data.postId}
                />
              );
            })}
          </div>
        </div>
      </Layout>
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
    </>
  );
};

export default Profile;
