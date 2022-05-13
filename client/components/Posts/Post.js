import React from "react";
import Image from "next/image";
import { HeartIcon, ChatIcon } from "@heroicons/react/outline";
import { HeartIcon as RedHeartIcon } from "@heroicons/react/solid";

const Post = ({ name, content, likes, comments, liked, image }) => {
  return (
    <div className="bg-white m-4 p-4 rounded-lg">
      <div className="flex flew-row items-center mb-2">
        <div className="w-8 h-8 bg-black rounded-3xl"></div>
        <p className="text-sm font-semibold mb-2 translate-y-1 ml-2">{name}</p>
      </div>
      <p className="text-sm">{content}</p>
      <div className="flex flex-row mt-4">
        <button className="text-sm mr-4 flex flex-row">
          <HeartIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{Math.floor(Math.random() * 300)} likes</p>
        </button>
        <button className="text-sm flex flex-row">
          <ChatIcon className="h-5 w-5 text-black " /> {""}
          <p className="ml-1">{Math.floor(Math.random() * 50)} comments</p>
        </button>
      </div>
    </div>
  );
};

export default Post;
