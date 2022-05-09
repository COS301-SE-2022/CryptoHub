import React from "react";
import Post from "./Post";
import { mockPosts } from "./MockUserPost";

const Posts = () => {
  return (
    <div className="w-5/12">
      {mockPosts.map((data, index) => {
        return (
          <Post
            name={data.username}
            content={data.content}
            likes={data.likes}
            comments={data.comments}
          />
        );
      })}
    </div>
  );
};

export default Posts;
