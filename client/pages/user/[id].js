import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import Post from "../../components/Posts/Post";

const User = () => {
  const router = useRouter();
  const { id } = router.query;
  const [user, setUser] = useState({});
  const [posts, setPosts] = useState([]);

  const handleGetUser = () => {
    const options = {
      method: "GET",
    };

    fetch(`http://localhost:7215/api/User/GetUserById/${id}`, options)
      .then((response) => response.json())
      .then((data) => {
        setUser(data);
      })
      .catch((error) => {});
  };

  const handleGetAllPosts = () => {
    const options = {
      method: "GET",
    };

    fetch("http://localhost:7215/api/Post/GetAllPosts", options)
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

  useEffect(() => {
    handleGetUser();
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
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-col">
            <p className="font-semibold text-center sm:text-left">
              {user.username}
            </p>{" "}
            <br />
            {/* <div className="flex flex-row -translate-y-5">
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
            </div> */}
          </div>
        </div>
        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
        <div className="flex flex-col items-center w-full sm:w-6/12">
          <div>
            <p className="text-sm mt-4 text-gray-600">Posts</p>
          </div>
          <div className="w-full">
            {posts.map((data, index) => {
              return (
                <Post
                  key={index}
                  name={data.username}
                  content={data.post1}
                  userId={data.userId}
                />
              );
            })}
          </div>
        </div>
      </Layout>
    </>
  );
};

export default User;
