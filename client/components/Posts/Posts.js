import React from "react";
import Post from "./Post";
import { mockPosts } from "../../mocks/mockUserPost";

const Posts = () => {
  return (
    <div className="sm:w-5/12">
      {mockPosts.map((data, index) => {
        return (
          <Post
            name={data.username}
            content={data.content}
            likes={data.likes}
            comments={data.comments}
            liked={data.liked}
          />
        );
      })}
    </div>
  );
};

export default Posts;
