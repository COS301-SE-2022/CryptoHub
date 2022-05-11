import React, { useState, useEffect, useContext } from "react";
import Post from "./Post";
import { mockPosts } from "../../mocks/mockUserPost";
import { userContext } from "../../auth/auth";

const Posts = () => {
  const { feedstate } = useContext(userContext)
  const [posts, setPosts] = useState([])
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
        setPosts(data.reverse())
        // if (data.authorized) {
        //   authorise(data.username, data.userId);
        // } else {
        //   setError(true);
        // }
      })
      .catch((error) => {
        console.warn("Error", error);
        setError(true);
        setLoading(false);
      });
  };

  useEffect(() => {
    handleGetAllPosts()
  }, [feedstate])

  return (
    <div className="sm:w-5/12">
      {posts.map((data, index) => {
        return <Post key={index} name={data.username} content={data.post} />
      })}
      
    </div>
  );
};

export default Posts;


// {mockPosts.map((data, index) => {
      //   return (
      //     <Post
      //       key={index}
      //       name={data.username}
      //       content={data.content}
      //       likes={data.likes}
      //       comments={data.comments}
      //       liked={data.liked}
      //       image={data.image}
      //     />
      //   );
      // })}
