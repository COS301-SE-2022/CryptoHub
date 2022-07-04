import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";

const ExplorePosts = () => {
  const { feedstate } = useContext(userContext);
  const [posts, setPosts] = useState([]);
  const [error, setError] = useState(false);
  const [following, setFollowing] = useState([]);
  const [loading, setLoading] = useState(false);
  const { user } = useContext(userContext);
  const router = useRouter();
  const [refresh, setRefresh] = useState(false);
  const [explorePosts, setExplorePosts] = useState([]);

  useEffect(() => {
    if (user.auth) {
      const options = {
        method: "GET",
      };

      fetch("http://localhost:7215/api/Post/GetAllPosts", options)
        .then((response) => response.json())
        .then((data) => {
          let posts = data.reverse();
          console.warn("Posts", posts);
          let myPosts = posts.filter((post) => {
            return post.userId != user.id;
          });
          setPosts(myPosts);
          fetch(
            `http://localhost:7215/api/UserFollower/GetUserFollowing/${user.id}`,
            options
          )
            .then((response) => response.json())
            .then((data) => {
              console.warn("Following: ", data);
              setFollowing(data);
              setRefresh(true);
            })
            .catch(() => {
              console.warn("Fail");
            });
        })
        .catch(() => {
          console.warn("Fail 2");
        });
    } else {
      router.push("/");
    }
  }, []);

  useEffect(() => {
    try {
      let suggested = posts.filter((post) => {
        return !following.find((p) => {
          return p.userId == post.userId;
        });
      });

      let final = suggested.filter((acc) => {
        return acc.userId != user.id;
      });

      console.warn("Final", final);
      setExplorePosts(final);
    } catch {}
  }, [refresh]);

  return (
    <div className=" w-full sm:w-5/12">
      {loading ? (
        <p>loading...</p>
      ) : (
        explorePosts.map((data, index) => {
          return (
            <Post
              key={index}
              name={data.username}
              content={data.post1}
              userId={data.userId}
              postId={data.postId}
              imageId={data.imageId}
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

export default ExplorePosts;
