import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { userContext } from "../../auth/auth";

const Posts = () => {
  const { feedstate } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [following, setFollowing] = useState([]);

  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);
  const { user } = useContext(userContext);
  const [mainPosts, setMainPosts] = useState([]);

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

    fetch("http://localhost:7215/api/Post/GetFeed", options)
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

  useEffect(() => {
    user.auth ? handleFeedPosts() : handleGetAllPosts();
  }, [feedstate]);

  return (
    <div className="sm:w-5/12">
      {loading ? (
        <p>loading...</p>
      ) : posts.count == 0 ? (
        <div>Explore</div>
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
