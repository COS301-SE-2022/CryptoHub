import React from "react";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";

const Post = () => {
  return (
    <div className="bg-white w-4/12 m-4 p-4 rounded-lg">
      <p className="text-sm font-semibold mb-2">username</p>
      <p className="text-sm">
        Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod
        tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim
        veniam, quis nostrud exercitation ullamco laboris nisi ut aliquip ex ea
        commodo consequat.
      </p>
      <div className="flex flex-row mt-4">
        <button className="text-sm mr-4 flex flex-row">
          <HeartIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">0 likes</p>
        </button>
        <button className="text-sm flex flex-row">
          <ChatIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">0 comments</p>
        </button>
      </div>
    </div>
  );
};

export default Post;
