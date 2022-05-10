import React from "react";
import Posts from "../Posts/Posts";
import InfoSection from "../InfoSection/InfoSection";

const Feed = () => {
  return (
    <div className="w-11/12 sm:flex">
      <InfoSection />
      <Posts />
    </div>
  );
};

export default Feed;
