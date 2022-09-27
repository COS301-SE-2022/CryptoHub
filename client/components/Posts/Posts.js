import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";
import Loader from "../Loader";

const Posts = () => {
  const { feedstate } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [following, setFollowing] = useState([]);
  const router = useRouter();

  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);
  const { user, url } = useContext(userContext);
  const [mainPosts, setMainPosts] = useState([]);

  const handleGetAllPosts = () => {
    setLoading(true);

    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Post/GetAllPosts`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        let posts = data.reverse();
        setPosts(posts);
      })
      .catch(() => {
        setError(true);
        setLoading(false);
      });
  };

  const handleFeedPosts = () => {
    setLoading(true);

    const options = {
      method: "GET",
      headers: {
        "Content-Type": "application/json",
        Authorization: "bearer " + user.token,
      },
    };

    fetch(`${url}/api/Post/GetFeed`, options)
      .then((response) => response.json())
      .then((data) => {
        setLoading(false);
        let posts = data.reverse();
        console.warn("Final feed: ", posts);

        setPosts(posts);
      })
      .catch(() => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    user.auth ? handleFeedPosts() : handleGetAllPosts();
  }, [feedstate]);

  return (
    <div className="sm:w-5/12">
      {loading ? (
        <Loader />
      ) : posts.length == 0 ? (
        <div className="flex flex-col items-center w-full pt-10">
          <button
            onClick={() => {
              router.push("/explore");
            }}
          >
            <p className="text-2xl font-semibold text-indigo-600">
              Explore more posts 🚀
            </p>
          </button>
        </div>
      ) : (
        posts.map((data, index) => {
          return (
            <Post
              key={index}
              name={data.username}
              content={data.content}
              userId={data.userId}
              postId={data.postId}
              imageId={data.imageUrl}
              time={data.dateCreated}
            />
          );
        })
      )}
      {error ? (
        <p className="text-sm text-gray-500 translate-y-16 text-center">
          Failed to load posts
        </p>
      ) : null}
    </div>
  );
};

export default Posts;
