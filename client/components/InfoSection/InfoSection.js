import React, { useContext } from "react";
import Carousel from "./Carousel";
import News from "./News";
import Suggestions from "./Suggestions";
import { userContext } from "../../auth/auth";
import { useRouter } from "next/router";

const InfoSection = () => {
  const { user } = useContext(userContext);
  const router = useRouter();

  return (
    <div className="bg-white sm:w-6/12 m-4 p-4 rounded-lg sm:fixed right-10 overflow-auto max-h-[40rem]">
      {user.auth ? null : (
        <div>
          <p className="font-bold">Create a free account now</p>
        </div>
      )}
      <Carousel />
      <News />
      {user.auth ? <Suggestions /> : null}
    </div>
  );
};

export default InfoSection;
