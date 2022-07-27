import React, { useContext, useState, useEffect } from "react";
import Posts from "../Posts/Posts";
import InfoSection from "../InfoSection/InfoSection";
import { userContext } from "../../auth/auth";
import Post from "../Posts/Post";

const AdminFeed = () => {
  const { user } = useContext(userContext);

  return (
    <div className="flex flex-col w-full sm:w-11/12 sm:flex items-center">
      <p className="text-3xl text-gray-600 font-semibold">Reported Posts</p>
      <AdminPosts />
    </div>
  );
};

export default AdminFeed;

const AdminPosts = () => {
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

  const handleGetReportedAllPosts = () => {
    setLoading(true);

    const options = {
      method: "GET",
    };

    fetch("http://localhost:7215/api/Post/GetAllReportedPosts", options)
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
    handleGetReportedAllPosts();
  }, [feedstate]);

  return (
    <div className="w-full sm:w-5/12">
      {loading ? (
        <p>loading...</p>
      ) : (
        posts.map((data, index) => {
          return (
            <Post
              key={index}
              name={data.username}
              content={data.content}
              userId={data.userId}
              postId={data.postId}
              imageId={data.imageId}
              admin={true}
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
