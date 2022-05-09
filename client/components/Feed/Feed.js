import React from "react";
import Posts from "../Posts/Posts";
import InfoSection from "../InfoSection/InfoSection";

const Feed = () => {
  return (
    <div className="w-11/12 flex">
      <Posts />
      <InfoSection />
    </div>
  );
};

export default Feed;
