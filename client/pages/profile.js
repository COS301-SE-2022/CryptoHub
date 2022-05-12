import React, { useState, useEffect, useContext } from "react";
import Layout from "../components/Layout";
import Head from "next/head";
import { userContext } from "../auth/auth";
import Posts from "../components/Posts/Posts";
import Post from "../components/Posts/Post";

const Profile = () => {
  const { user } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);

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
    handleGetAllPosts();
  }, []);

  return (
    <>
      <Head>
        <title>CryptoHub</title>
      </Head>
      <Layout>
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-20 h-20 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "50px" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left">
              {user.username}
            </p>{" "}
            <br />
            <div className="flex flex-row -translate-y-5">
              <p className="mr-3">
                <span className="font-semibold">10 </span> following
              </p>
              <p>
                {" "}
                <span className="font-semibold" f>
                  4{" "}
                </span>
                followers
              </p>
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
    </>
  );
};

export default Profile;
