import React, { useContext } from "react";
import Posts from "../Posts/Posts";
import InfoSection from "../InfoSection/InfoSection";
import { userContext } from "../../auth/auth";

const Feed = () => {
  const { user } = useContext(userContext);

  return (
    <div className="w-10/12 sm:flex">
      <InfoSection />
      <Posts />
    </div>
  );
};

export default Feed;
