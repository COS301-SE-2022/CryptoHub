import { useRouter } from "next/router";
import React, { useState, useEffect, useContext } from "react";
import Head from "next/head";
import Layout from "../../components/Layout";
import { userContext } from "../../auth/auth";
import Post from "../../components/Posts/Post";

const User = () => {
  const router = useRouter();
  const { id } = router.query;
  const [thisUser, setUser] = useState({});
  const [posts, setPosts] = useState([]);
  const [clicked, setClicked] = useState(false);
  const [following, setFollowing] = useState([]);
  const [isFollowing, setIsFollowing] = useState(false);

  const { user } = useContext(userContext);

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

  const handleFollowUser = () => {
    const options = {
      method: "POST",
      headers: { "Content-Type": "application/json" },
      body: JSON.stringify({
        userId: user.id,
        followerId: id,
      }),
    };

    fetch(
      `http://localhost:7215/api/UserFollower/FollowUser/${id}/${user.id}`,
      options
    )
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

  const checkIfSameUser = () => {
    return user.id == id;
  };

  const checkFollowing = () => {
    fetch(`http://localhost:7215/api/UserFollower/GetUserFollowing/${user.id}`)
      .then((response) => response.json())
      .then((data) => {
        // setFollowing(data);
        data.map((d) => {
          if (d.userId == id) {
            setIsFollowing(true);
          }
        });
      });
  };

  useEffect(() => {
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
        <div className="flex flex-col sm:flex-row w-full sm:w-6/12 items-center mt-8">
          <div
            className="w-32 h-32 bg-black sm:mr-10 mb-5"
            style={{ borderRadius: "100%" }}
          ></div>
          <div className="flex flex-row">
            <p className="font-semibold text-center sm:text-left">
              {thisUser.username}
            </p>{" "}
            {user.auth ? (
              isFollowing ? (
                <p className="text-sm ml-5 text-black bg-gray-400 rounded-md px-3 py-1">
                  Following
                </p>
              ) : (
                <button onClick={handleFollowUser}>
                  <p className="text-sm text-white ml-5 bg-indigo-600 rounded-md px-3 py-1 hover:bg-indigo-500 transition">
                    Follow
                  </p>
                </button>
              )
            ) : null}
            <br />
          </div>
        </div>
        <div className="bg-gray-400 sm:w-6/12" style={{ height: "1px" }}></div>
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
                  content={data.post1}
                  userId={data.userId}
                  imageId={data.imageId}
                  postId={data.postId}
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
