import React, { useContext } from "react";
import Posts from "../Posts/Posts";
import InfoSection from "../InfoSection/InfoSection";
import { userContext } from "../../auth/auth";

const AdminFeed = () => {
  const { user } = useContext(userContext);

  return (
    <div className="w-11/12 sm:flex justify-center">
      <Posts />
    </div>
  );
};

export default AdminFeed;
