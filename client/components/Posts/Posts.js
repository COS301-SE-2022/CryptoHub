import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { userContext } from "../../auth/auth";

const Posts = () => {
  const { feedstate } = useContext(userContext);
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
        setPosts(data.reverse());
      })
      .catch(() => {
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    handleGetAllPosts();
  }, [feedstate]);

  return (
    <div className="sm:w-5/12">
      {loading ? (
        <p>loading...</p>
      ) : (
        posts.map((data, index) => {
          return <Post key={index} name={data.username} content={data.post} />;
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
