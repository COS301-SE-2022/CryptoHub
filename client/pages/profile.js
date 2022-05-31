import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Post from "../components/Posts/Post";
import { useRouter } from "next/router";
import { XIcon } from "@heroicons/react/outline";
import SuggestedAccount from "../components/InfoSection/SuggestedAccount";

const Profile = () => {
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [, setError] = useState(false);
  const [, setLoading] = useState(false);
  const router = useRouter();
  const [followers, setFollowers] = useState([]);
  const [following, setFollowing] = useState([]);
  const [showModal, setShowModal] = useState(false);
  const [showFollowingModal, setFollowingShowModal] = useState(false);

  const handleViewFollowing = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:8082/api/user/getfollowing/${user.id}`, options)
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

    fetch(`http://localhost:8082/api/user/getfollowers/${user.id}`, options)
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

    fetch("http://localhost:8082/api/post/getallposts", options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        console.warn(data);
        setPosts(data.reverse());
      })
      .catch((error) => {
        console.warn("Error", error);
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
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
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
          </div>
        </div>
        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Posts</p>
          </div>
          <div className="w-full">
            {posts.map((data, index) => {
              return data.username == user.username ? (
                <Post key={index} name={data.username} content={data.post} />
              ) : null;
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
                                name={data.userName}
                                hidefollow={true}
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
                                name={data.userName}
                                hidefollow={true}
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
