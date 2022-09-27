import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";
import Loader from "../Loader";

const TagPosts = ({ tag }) => {
  const { feedstate } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [following, setFollowing] = useState([]);
  const router = useRouter();

  const [error, setError] = useState(false);
  const [loading, setLoading] = useState(false);
  const { user, url } = useContext(userContext);

  const handleGetAllPosts = () => {
    setLoading(true);

    const options = {
      method: "GET",
    };

    fetch(`${url}/api/Post/GetPostsByTag/%23${tag}`, options)
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
    handleGetAllPosts();
  }, [feedstate]);

  return (
    <div className="sm:w-5/12">
      <p className="text-2xl text-center mb-3">Posts for #{tag}</p>
      {loading ? (
        <Loader />
      ) : posts.length == 0 ? (
        <div className="flex flex-col items-center w-full pt-10">
          <p className="text-md font-semibold text-grey-600">
            No tagged posts for this cryptocurrency
          </p>
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

export default TagPosts;
